using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    public class Blueprint : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Blueprint";
        private const string Description = "A wall mounted blueprint of the ship, entire SS Hammerhead. " +
            "The blueprint shows the ship from various angles with details of each area.";

        #endregion

        #region Methods

        private static CustomCommand[] CreateCommands()
        {
            var checkCommand = new CustomCommand(new CommandHelp($"Check {Name}", $"Check the {Name} in detail."), true, true, (game, arguments) =>
            {
                game.Overworld.CurrentRegion.VisibleWithoutDiscovery = true;
                return new Reaction(ReactionResult.OK, $"You check the {Name} in detail. You know understand the internal layout of the ship.");
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
