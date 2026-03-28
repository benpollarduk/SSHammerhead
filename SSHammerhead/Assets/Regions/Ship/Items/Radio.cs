using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Targets.Console.Rendering;
using NetAF.Utilities;
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
        /// Get if this radio is playing.
        /// </summary>
        public static bool IsPlaying
        {
            get { return GameExecutor.ExecutingGame?.VariableManager?.Get(IsPlayingVariableName).InsensitiveEquals(true.ToString()) ?? false; }
            private set { GameExecutor.ExecutingGame?.VariableManager?.Add(IsPlayingVariableName, value.ToString()); }
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get a visual representing the radio.
        /// </summary>
        /// <param name="background">The background color.</param>
        /// <param name="spoolCenters">The spool centers.</param>
        /// <returns></returns>
        public static GridVisualBuilder GetVisual(AnsiColor background, char spoolCenters = '+')
        {
            var visualBuilder = new GridVisualBuilder(background, AnsiColor.White);
            visualBuilder.Resize(new NetAF.Assets.Size(30, 20));

            // cassette Body
            visualBuilder.DrawBorder(0, 2, 29, 15, AnsiColor.BrightBlack);
            visualBuilder.DrawRectangle(1, 3, 27, 13, AnsiColor.White, AnsiColor.White);

            // label
            visualBuilder.DrawRectangle(2, 4, 25, 3, AnsiColor.BrightBlack, AnsiColor.BrightBlack);
            visualBuilder.DrawText(3, 5, "THIS FIRE", AnsiColor.White);

            // screws
            visualBuilder.SetCell(1, 3, 'o', AnsiColor.BrightBlack, AnsiColor.White);
            visualBuilder.SetCell(26, 3, 'o', AnsiColor.BrightBlack, AnsiColor.White);
            visualBuilder.SetCell(1, 15, 'o', AnsiColor.BrightBlack, AnsiColor.White);
            visualBuilder.SetCell(26, 15, 'o', AnsiColor.BrightBlack, AnsiColor.White);

            // window
            visualBuilder.DrawRectangle(2, 8, 25, 7, AnsiColor.Black, AnsiColor.Black);
            visualBuilder.DrawBorder(2, 8, 25, 7, AnsiColor.BrightBlack);

            // spools
            visualBuilder.DrawCircle(8, 11, 2, AnsiColor.BrightBlack, AnsiColor.Black);
            visualBuilder.DrawCircle(20, 11, 2, AnsiColor.BrightBlack, AnsiColor.Black);

            // spool centers
            visualBuilder.SetCell(8, 11, spoolCenters, AnsiColor.White, AnsiColor.Black);
            visualBuilder.SetCell(20, 11, spoolCenters, AnsiColor.White, AnsiColor.Black);

            // tape
            visualBuilder.DrawRectangle(4, 11, 1, 1, AnsiColor.Red, AnsiColor.Black);
            visualBuilder.DrawRectangle(24, 11, 1, 1, AnsiColor.Red, AnsiColor.Black);

            return visualBuilder;
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
