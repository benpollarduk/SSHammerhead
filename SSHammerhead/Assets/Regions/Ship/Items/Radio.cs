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
using System.ComponentModel;

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
        private static readonly CasetteProperties casetteProperties = CasetteProperties.Default;
        private static readonly string casetteTemplateAsString = CasetteVisual.GetTapeTemplate(casetteProperties).ToString();
        private static Prompt Off => new("off");
        private static Prompt On => new("on");
        private static Prompt View => new("view");
        private static readonly CasetteInfo current = Casettes.Casettes.MartynAndBen;

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
            set { GameExecutor.ExecutingGame?.VariableManager?.Add(IsPlayingVariableName, value.ToString()); }
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get the visual representing the radio.
        /// </summary>
        /// <param name="variation">The variation.</param>
        /// <returns>The visual.</returns>
        public static GridVisualBuilder GetVisual(CasetteVariation variation)
        {
            var template = GridVisualBuilder.FromString(casetteTemplateAsString);

            return variation switch
            {
                CasetteVariation.Zero => CasetteVisual.AddDetails0(template, casetteProperties),
                CasetteVariation.One => CasetteVisual.AddDetails1(template, casetteProperties),
                CasetteVariation.Two => CasetteVisual.AddDetails2(template, casetteProperties),
                CasetteVariation.Three => CasetteVisual.AddDetails3(template, casetteProperties),
                _ => template
            };
        }

        /// <summary>
        /// Get the name of the track currently playing.
        /// </summary>
        /// <returns>The name of the currently playing track.</returns>
        public static SongInfo NowPlaying()
        {
            var time = AudioPlayer.GetBackgroundMusicPosition();
            return current.GetSongAtTime(time);
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
