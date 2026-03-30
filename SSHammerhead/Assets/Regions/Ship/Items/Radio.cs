using NAudio.Wave;
using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Targets.Console.Rendering;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Ship.Items.Casettes;
using SSHammerhead.Audio;
using SSHammerhead.Logic.Modes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    public class Radio : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Radio";
        private const string Description = "A small, old style portable radio/casette player. In space there are no radio stations to listen to but luckily there is a casette loaded.";
        private const string IsPlayingVariableName = "Radio_IsPlaying";
        private const string CurrentCasetteVariableName = "Radio_CurrentCasette";
        private const float IncrediblyDistant = 0.1f;
        private const float VeryClose = 1f;
        private const float TooFar = 0f;
        private const int MaxPathLength = 5;

        #endregion

        #region StaticProperties

        private static Item lastGeneratedRadio;
        private static Prompt Off => new("off");
        private static Prompt On => new("on");
        private static Prompt View => new("view");
        private static string casetteTemplateAsString;
        private static WaveOutEvent backgroundMusicWaveOut;
        private static AudioFileReader backgroundMusicReader;
        private static ProximityFilter backgroundProximityFilter;
        private static bool shouldLoopBackgroundMusic;
        private static Timer positionUpdateTimer;

        internal static Dictionary<string, float> Composition => new()
        {
            { "Aluminum", 79.25f },
            { "Plastics", 10.12f },
            { "Paper", 0.01f },
            { "Copper", 3.25f },
            { "Silver", 1.45f },
            { "Lead", 0.04f },
        };

        #endregion

        #region StaticMethods

        /// <summary>
        /// Determine if the radio is playing.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>True if the radio is playing, else false.</returns>
        public static bool IsPlaying(Game game)
        {
            return game?.VariableManager?.Get(IsPlayingVariableName).InsensitiveEquals(true.ToString()) ?? false;
        }

        /// <summary>
        /// Start the radio.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="volume">The volume of the sound playback as a normalised value between 0 and 1.</param>
        /// <param name="proximity">The proximity to the source as a normalised value between 0 and 1. The higher the value the closer the proximity.</param>
        public static void Start(Game game, float volume = 1, float proximity = 1)
        {
            Stop(game);

            var casette = GetCurrentlyLoadedCasette(game);

            backgroundMusicWaveOut = new WaveOutEvent();
            backgroundMusicReader = new AudioFileReader(casette.ResourceName);
            
            backgroundMusicReader.CurrentTime = GetCasettePosition(game, casette);

            backgroundMusicReader.Volume = 1;
            shouldLoopBackgroundMusic = true;

            backgroundProximityFilter = new ProximityFilter(backgroundMusicReader);
            backgroundProximityFilter.UpdateProximity(proximity, true);
            backgroundProximityFilter.UpdateVolume(volume * proximity, true);

            backgroundMusicWaveOut.Init(backgroundProximityFilter);

            backgroundMusicWaveOut.PlaybackStopped += (sender, args) =>
            {
                if (shouldLoopBackgroundMusic && backgroundMusicReader != null)
                {
                    backgroundMusicReader.CurrentTime = TimeSpan.Zero;
                    backgroundMusicWaveOut?.Play();
                }
            };

            backgroundMusicWaveOut.Play();

            positionUpdateTimer = new Timer(_ =>
            {
                if (backgroundMusicReader != null)
                    SetCasettePosition(game, GetCurrentlyLoadedCasette(game), backgroundMusicReader.CurrentTime);

            }, null, 0, 1000);
        }

        /// <summary>
        /// Adjust the radio.
        /// </summary>
        /// <param name="volume">The volume of the sound playback as a normalised value between 0 and 1.</param>
        /// <param name="proximity">The proximity to the source as a normalised value between 0 and 1. The higher the value the closer the proximity.</param>
        public static void Adjust(float volume = 1, float proximity = 1)
        {
            if (backgroundProximityFilter != null)
            {
                backgroundProximityFilter.UpdateProximity(proximity);
                backgroundProximityFilter.UpdateVolume(volume * proximity);
            }
        }

        /// <summary>
        /// Stop the radio.
        /// </summary>
        /// <param name="game">The game.</param>
        public static void Stop(Game game)
        {
            positionUpdateTimer?.Dispose();
            positionUpdateTimer = null;

            shouldLoopBackgroundMusic = false;

            if (backgroundMusicWaveOut != null)
            {
                backgroundMusicWaveOut.Stop();
                backgroundMusicWaveOut.Dispose();
                backgroundMusicWaveOut = null;
            }

            if (backgroundMusicReader != null)
            {
                SetCasettePosition(game, GetCurrentlyLoadedCasette(game), backgroundMusicReader.CurrentTime);
                backgroundMusicReader.Dispose();
                backgroundMusicReader = null;
            }

            backgroundProximityFilter = null;
        }

        /// <summary>
        /// Change casette.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="casette">The new casette.</param>
        internal static void ChangeCasette(Game game, Casette casette)
        {
            var wasPlaying = IsPlaying(game);

            if (wasPlaying)
                Stop(game);

            game.VariableManager.Add(CurrentCasetteVariableName, casette.Info.Album);

            casetteTemplateAsString = GetCurrentlyLoadedCasette(game).GetVisualTemplate().ToString();

            if (wasPlaying)
                Start(game);
        }

        /// <summary>
        /// Get the visual representing the radio.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="variation">The variation.</param>
        /// <returns>The visual.</returns>
        public static GridVisualBuilder GetVisual(Game game, CasetteVariation variation)
        {
            var casette = GetCurrentlyLoadedCasette(game);

            if (string.IsNullOrEmpty(casetteTemplateAsString))
                casetteTemplateAsString = casette.GetVisualTemplate().ToString();

            var template = GridVisualBuilder.FromString(casetteTemplateAsString);

            return casette.AddVisualDetails(template, variation);
        }

        /// <summary>
        /// Get information about the song that is currently playing.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The information about the currently playing song.</returns>
        public static NowPlaying NowPlaying(Game game)
        {
            var casette = GetCurrentlyLoadedCasette(game);
            var time = backgroundMusicReader?.CurrentTime ?? TimeSpan.Zero;
            return new NowPlaying(casette.Info.Artist, casette.Info.Album, casette.GetSongAtTime(time).Name);
        }

        /// <summary>
        /// Get a variable name for the position on a casette.
        /// </summary>
        /// <param name="casette">The casette.</param>
        /// <returns>The variable name.</returns>
        private static string GetCasettePositionVariableName(Casette casette)
        {
            return $"Radio_CasettePosition_{casette.Info.Artist}_{casette.Info.Album}";
        }

        /// <summary>
        /// Get a variable name for if a casette is owned.
        /// </summary>
        /// <param name="casette">The casette.</param>
        /// <returns>The variable name.</returns>
        private static string GetCasetteOwnedVariableName(Casette casette)
        {
            return $"Radio_CasetteOwned_{casette.Info.Artist}_{casette.Info.Album}";
        }

        /// <summary>
        /// Get the position of a casette.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="casette">The casette.</param>
        /// <returns>The position.</returns>
        private static TimeSpan GetCasettePosition(Game game, Casette casette)
        {
            if (game == null)
                return TimeSpan.Zero;

            var key = GetCasettePositionVariableName(casette);

            if (game.VariableManager.ContainsVariable(key))
            {
                var value = game.VariableManager.Get(key);

                if (int.TryParse(value, out var milliseconds))
                    return TimeSpan.FromMilliseconds(milliseconds);
            }

            if (casette == Casettes.Casettes.MartynAndBen)
                return TimeSpan.FromMilliseconds(27000);
            else if (casette == Casettes.Casettes.Demons)
                return TimeSpan.FromMilliseconds(65000);

            return TimeSpan.Zero;
        }

        /// <summary>
        /// Set the position of a casette.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="casette">The casette.</param>
        /// <param name="position">The position.</param>
        private static void SetCasettePosition(Game game, Casette casette, TimeSpan position)
        {
            if (game == null)
                return;

            var key = GetCasettePositionVariableName(casette);
            game.VariableManager.Add(key, position.TotalMilliseconds.ToString());
        }

        /// <summary>
        /// Get the available casettes.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>An array containing all available casettes.</returns>
        public static Casette[] AvailableCasettes(Game game)
        {
            if (game == null)
                return [Casettes.Casettes.MartynAndBen];

            List<Casette> toCheck =
            [
                Casettes.Casettes.Demons,
            ];

            List<Casette> owned = [.. toCheck.Where(x => game.VariableManager.ContainsVariable(GetCasetteOwnedVariableName(x)))];

            // always add default
            owned.Insert(0, Casettes.Casettes.MartynAndBen);
            owned.Insert(0, Casettes.Casettes.Demons);

            return [.. owned];
        }

        /// <summary>
        /// Make a casette available.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="casette">The casette.</param>
        public static void MakeCasetteAvailable(Game game, Casette casette)
        {
            game.VariableManager.Add(GetCasetteOwnedVariableName(casette), true.ToString());
        }

        /// <summary>
        /// Get the currently playing casette.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The currently loaded casette.</returns>
        public static Casette GetCurrentlyLoadedCasette(Game game)
        {
            var available = AvailableCasettes(game);

            if (available.Length == 0)
                return null;

            if (game.VariableManager.ContainsVariable(CurrentCasetteVariableName))
            {
                var value = game.VariableManager.Get(CurrentCasetteVariableName);

                var match = available.FirstOrDefault(x => x.Info.Album.InsensitiveEquals(value));

                if (match != null)
                    return match;
            }

            return available[0];
        }

        /// <summary>
        /// Determine the proximity to the radio, as a normalised value.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The distance as a normalised value between 0 and 1. The higher the value the closer the proximity.</returns>
        public static float DetermineProximity(Game game)
        {
            if (game == null)
                return TooFar;

            if (lastGeneratedRadio == null)
                return TooFar;

            if (!game.VariableManager.ContainsVariable(IsPlayingVariableName))
                return TooFar;

            if (!game.VariableManager.Get(IsPlayingVariableName).Equals("True", StringComparison.InvariantCultureIgnoreCase))
                return TooFar;

            // if player has item
            if (game.Player.FindItem(Name, out _))
                return VeryClose;

            // calculate by region
            if (game.Overworld.CurrentRegion == null)
                return TooFar;

            // base on region
            if (!game.Overworld.CurrentRegion.TryLocateItem(lastGeneratedRadio, out var radioRoom))
                return IncrediblyDistant;

            // base on path length
            if (!game.Overworld.CurrentRegion.TryFindShortestPath(game.Overworld.CurrentRegion.CurrentRoom, radioRoom, out var path))
                return IncrediblyDistant;

            // get length
            var length = path.Count - 1;

            // check max path length
            if (length > MaxPathLength)
                return IncrediblyDistant;

            // determine based on path length
            return VeryClose - (length / (float)MaxPathLength);
        }

        private static bool IsPrompt(string arg, Prompt prompt)
        {
            return prompt.Entry.Equals(arg, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            CustomCommand adjustCommand = null;

            adjustCommand = new CustomCommand(new CommandHelp("Radio", "Adjust the radio."), true, false, (g, a) =>
            {
                if (g == null)
                    return new(ReactionResult.Error, "No game specified.");

                if (a == null || a.Length == 0)
                {
                    g.ChangeMode(new RadioMode());
                    return new(ReactionResult.GameModeChanged, string.Empty);
                }

                var arg = string.Join(" ", a);

                if (IsPrompt(arg, View))
                {
                    g.ChangeMode(new RadioMode());
                    return new(ReactionResult.GameModeChanged, string.Empty);
                }

                if (IsPrompt(arg, On))
                {
                    Start(g);
                    adjustCommand.AddPrompt(Off);
                    adjustCommand.RemovePrompt(On);
                    return new(ReactionResult.Silent, string.Empty);
                }

                if (IsPrompt(arg, Off))
                {
                    Stop(g);
                    adjustCommand.AddPrompt(On);
                    adjustCommand.RemovePrompt(Off);
                    return new(ReactionResult.Silent, string.Empty);
                }

                return new(ReactionResult.Error, $"Unrecognised argument {arg}.");
            });

            adjustCommand.AddPrompt(View);

            if (IsPlaying(GameExecutor.ExecutingGame))
                adjustCommand.AddPrompt(Off);
            else
                adjustCommand.AddPrompt(On);

            lastGeneratedRadio = new(Name, Description, true, commands: [adjustCommand]);
            return lastGeneratedRadio;
        }

        #endregion
    }
}
