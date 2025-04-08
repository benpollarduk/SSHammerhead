using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class Blueprint : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Blueprint";
        private const string Description = "A wall mounted blueprint of the ship, entire SS Hammerhead. " +
            "The blueprint shows the ship from various angles with details of each area.";

        #endregion

        #region StaticProperties

        private static readonly Dictionary<string, float> Composition = new()
        {
            { "Paper", 99.3f },
            { "Ink", 0.7f }
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
            return new Item(Name, Description, commands: CreateCommands(), interaction: (item) =>
            {
                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                    return Scanner.PerformScan(Name, new(Composition));

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
