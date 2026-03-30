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

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    public class Radio : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Radio";
        private const string Description = "A small, old style portable radio/casette player. In space there are no radio stations to listen to but luckily there is a casette loaded.";
        internal const string IsPlayingVariableName = "Radio_IsPlaying";
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

        internal static Dictionary<string, float> Composition => new()
        {
            { "Aluminum", 79.25f },
            { "Plastics", 10.12f },
            { "Paper", 0.01f },
            { "Copper", 3.25f },
            { "Silver", 1.45f },
            { "Lead", 0.04f },
        };

        /// <summary>
        /// Get or set if this radio is playing.
        /// </summary>
        public static bool IsPlaying
        {
            get { return GameExecutor.ExecutingGame?.VariableManager?.Get(IsPlayingVariableName).InsensitiveEquals(true.ToString()) ?? false; }
            set 
            {
                GameExecutor.ExecutingGame?.VariableManager?.Add(IsPlayingVariableName, value.ToString());

                if (value)
                    Start();
                else
                    Stop();
            }
        }

        /// <summary>
        /// Get the current casette.
        /// </summary>
        public static Casette CurrentCasette { get; private set; } = Casettes.Casettes.MartynAndBen;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get the current position in the background music.
        /// </summary>
        /// <returns>The current position.</returns>
        public static TimeSpan GetBackgroundMusicPosition()
        {
            return backgroundMusicReader?.CurrentTime ?? TimeSpan.Zero;
        }

        /// <summary>
        /// Start the radio.
        /// </summary>
        /// <param name="volume">The volume of the sound playback as a normalised value between 0 and 1.</param>
        /// <param name="proximity">The proximity to the source as a normalised value between 0 and 1. The higher the value the closer the proximity.</param>
        public static void Start(float volume = 1, float proximity = 1)
        {
            Stop();

            backgroundMusicWaveOut = new WaveOutEvent();
            backgroundMusicReader = new AudioFileReader(CurrentCasette.ResourceName);
            backgroundMusicReader.CurrentTime = TimeSpan.FromMilliseconds(CurrentCasette.PositionInMilliseconds);

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
        public static void Stop()
        {
            shouldLoopBackgroundMusic = false;

            if (backgroundMusicWaveOut != null)
            {
                backgroundMusicWaveOut.Stop();
                backgroundMusicWaveOut.Dispose();
                backgroundMusicWaveOut = null;
            }

            if (backgroundMusicReader != null)
            {
                backgroundMusicReader.Dispose();
                backgroundMusicReader = null;
            }

            backgroundProximityFilter = null;
        }

        /// <summary>
        /// Change casette.
        /// </summary>
        /// <param name="casette">The new casette.</param>
        protected static void ChangeCasette(Casette casette)
        {
            if (!string.IsNullOrEmpty(casetteTemplateAsString))
                return;

            CurrentCasette = casette;

            casetteTemplateAsString = CurrentCasette.GetVisualTemplate().ToString();
        }

        /// <summary>
        /// Get the visual representing the radio.
        /// </summary>
        /// <param name="variation">The variation.</param>
        /// <returns>The visual.</returns>
        public static GridVisualBuilder GetVisual(CasetteVariation variation)
        {
            if (string.IsNullOrEmpty(casetteTemplateAsString))
                casetteTemplateAsString = CurrentCasette.GetVisualTemplate().ToString();

            var template = GridVisualBuilder.FromString(casetteTemplateAsString);

            return CurrentCasette.AddVisualDetails(template, variation);
        }

        /// <summary>
        /// Get the name of the track currently playing.
        /// </summary>
        /// <returns>The name of the currently playing track.</returns>
        public static SongInfo NowPlaying()
        {
            var time = GetBackgroundMusicPosition();
            return CurrentCasette.GetSongAtTime(time);
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
                    IsPlaying = true;
                    adjustCommand.AddPrompt(Off);
                    adjustCommand.RemovePrompt(On);
                    return new(ReactionResult.Silent, string.Empty);
                }

                if (IsPrompt(arg, Off))
                {
                    IsPlaying = false;
                    adjustCommand.AddPrompt(On);
                    adjustCommand.RemovePrompt(Off);
                    return new(ReactionResult.Silent, string.Empty);
                }

                return new(ReactionResult.Error, $"Unrecognised argument {arg}.");
            });

            adjustCommand.AddPrompt(View);

            if (IsPlaying)
                adjustCommand.AddPrompt(Off);
            else
                adjustCommand.AddPrompt(On);

            lastGeneratedRadio = new(Name, Description, true, commands: [adjustCommand]);
            return lastGeneratedRadio;
        }

        #endregion
    }
}
