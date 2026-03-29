using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Targets.Console.Rendering;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Ship.Items.Casettes;
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

        internal static Dictionary<string, float> Composition => new()
        {
            { "Aluminum", 79.25f },
            { "Plastics", 10.12f },
            { "Paper", 0.01f },
            { "Copper", 3.25f },
            { "Silver", 1.45f },
            { "Lead", 0.04f },
        };

        private static Prompt Off => new("off");
        private static Prompt On => new("on");
        private static Prompt View => new("view");

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
            var properties = CasetteProperties.Default;
            var template = Casette.GetTapeTemplate(properties);

            return variation switch
            {
                CasetteVariation.Zero => Casette.AddDetails0(template, properties),
                CasetteVariation.One => Casette.AddDetails1(template, properties),
                CasetteVariation.Two => Casette.AddDetails2(template, properties),
                CasetteVariation.Three => Casette.AddDetails3(template, properties),
                _ => template
            };
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
