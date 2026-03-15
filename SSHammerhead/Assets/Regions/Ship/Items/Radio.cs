using NetAF.Assets;
using NetAF.Commands;
using NetAF.Logic;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Ship.Rooms.L0;
using SSHammerhead.Assets.Regions.Ship.Rooms.L1;
using SSHammerhead.Assets.Regions.Ship.Rooms.L2;
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

        #endregion

        #region StaticProperties

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
                return 0f;

            if (!game.VariableManager.ContainsVariable(IsPlayingVariableName))
                return 0f;

            if (!game.VariableManager.Get(IsPlayingVariableName).Equals("True", StringComparison.InvariantCultureIgnoreCase))
                return 0f;

            // if player has item
            if (game.Player.FindItem(Name, out _))
                return 1f;

            // calculate by region
            if (game.Overworld.CurrentRegion == null)
                return 0f;

            if (!game.Overworld.CurrentRegion.Identifier.Name.Equals(SSHammerHead.Name))
                return 0.1f;

            // TODO: base on path
            var room = game.Overworld.CurrentRegion.CurrentRoom;

            return room?.Identifier?.Name switch
            {
                Bridge.Name => 0.4f,
                BridgePort.Name => 0.3f,
                BridgeStarboard.Name => 0.3f,
                BridgeTunnel.Name => 0.5f,
                BridgeTunnelVertical.Name => 0.65f,
                BridgeTunnelEntry.Name => 0.8f,
                CentralHull.Name => 1f,
                Booster.Name => 0.65f,
                MedicalRoom.Name => 0.65f,
                StarboardWing.Name => 0.65f,
                Laboratory.Name => 0.5f,
                StasisChamber.Name => 0.8f,
                StarboardWingInner.Name => 0.8f,
                StarboardWingOuter.Name => 0.65f,
                EngineRoom.Name => 0.5f,
                Airlock.Name => 0.4f,
                SupplyRoom.Name => 0.4f,
                _ => 0f
            };
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
                g.VariableManager.Add(IsPlayingVariableName, true.ToString());
                radioOff.IsPlayerVisible = false;
                radioOn.IsPlayerVisible = true;

                return new Reaction(ReactionResult.Error, "You turn the radio off.");
            });

            return new(Name, Description, true, commands: [radioOn, radioOff]);
        }

        #endregion
    }
}
