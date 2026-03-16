using NetAF.Assets;
using NetAF.Commands;
using NetAF.Logic;
using NetAF.Utilities;
using System;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    public class Radio : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Radio";
        private const string Description = "A small, old style portable radio/casette player. In space there are no radio stations to listen to but luckily there is a casette loaded.";
        private const string IsPlayingVariableName = "Radio_IsPlaying";
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

        #endregion

        #region StaticMethods

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

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            CustomCommand radioOn = null;
            CustomCommand radioOff = null;

            radioOn = new CustomCommand(new CommandHelp("Radio On", "Turn the radio on."), true, false, (g, a) =>
            {
                g.VariableManager.Add(IsPlayingVariableName, true.ToString());
                radioOff.IsPlayerVisible = true;
                radioOn.IsPlayerVisible = false;

                return new Reaction(ReactionResult.Error, "You turn the radio on.");
            });

            radioOff = new CustomCommand(new CommandHelp("Radio Off", "Turn the radio off."), false, false, (g, a) =>
            {
                g.VariableManager.Add(IsPlayingVariableName, false.ToString());
                radioOff.IsPlayerVisible = false;
                radioOn.IsPlayerVisible = true;

                return new Reaction(ReactionResult.Error, "You turn the radio off.");
            });

            lastGeneratedRadio = new(Name, Description, true, commands: [radioOn, radioOff]);
            return lastGeneratedRadio;
        }

        #endregion
    }
}
