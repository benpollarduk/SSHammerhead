﻿using NetAF.Assets;
using NetAF.Commands;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class Blueprint : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Blueprint";
        private const string Description = "A wall mounted blueprint of the ship, entire SS Hammerhead. " +
            "The blueprint shows the ship from various angles with details of each area.";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Paper", 99.31f },
            { "Ink", 0.65f }
        };

        #endregion

        #region Methods

        private static CustomCommand[] CreateCommands()
        {
            var checkCommand = new CustomCommand(new CommandHelp($"Check {Name}", $"Check the {Name} in detail."), true, true, (game, arguments) =>
            {
                game.Overworld.CurrentRegion.IsVisibleWithoutDiscovery = true;
                return new Reaction(ReactionResult.Inform, $"You check the {Name} in detail. You know understand the internal layout of the ship.");
            });

            return [checkCommand];
        }

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, commands: CreateCommands());
        }

        #endregion
    }
}
