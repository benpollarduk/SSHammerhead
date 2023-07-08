using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items
{
    public class Blueprint : ItemTemplate<Blueprint>
    {
        #region Constants

        internal const string Name = "Blueprint";
        private const string Description = "A wall mounted blueprint of the ship, entire SS Hammerhead. The blueprint shows the ship from various angles with details of each area.";

        #endregion

        #region Methods

        private static CustomCommand[] CreateCommands()
        {
            var checkCommand = new CustomCommand(new CommandHelp($"Check {Name}", $"Check the {Name} in detail."), true, (game, arguments) =>
            {
                game.Overworld.CurrentRegion.VisibleWithoutDiscovery = true;
                return new Reaction(ReactionResult.OK, $"You check the {Name} in detail. You know understand the internal layout of the ship.");
            });

            return new[] { checkCommand };
        }

        #endregion

        #region Overrides of ItemTemplate<Mirror>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The region.</returns>
        protected override Item OnCreate()
        {
            return new Item(Name, Description) { Commands = CreateCommands() };
        }

        #endregion
    }
}
