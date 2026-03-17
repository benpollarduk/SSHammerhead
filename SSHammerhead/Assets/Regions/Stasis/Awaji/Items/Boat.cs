using NetAF.Assets;
using NetAF.Commands;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Stasis.Awaji.Rooms;

namespace SSHammerhead.Assets.Regions.Stasis.Awaji.Items
{
    internal class Boat : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Boat";
        private readonly string Description = "A small rowing boat. A pair of oars are resting in the rolicks.";
        private const string RowCommand = "Row";
        private const string GetInCommand = "Get In";
        private const string GetOutCommand = "Get Out";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            Item item = null;

            CustomCommand getInCommand = null;
            CustomCommand getOutCommand = null;
            CustomCommand rowCommand = null;

            getInCommand = new CustomCommand(new CommandHelp(GetInCommand, "Get in to the boat."), true, false, (g, a) =>
            {
                getInCommand.IsPlayerVisible = false;
                getOutCommand.IsPlayerVisible = true;
                rowCommand.IsPlayerVisible = true;

                return new Reaction(ReactionResult.Inform, "You get in to the boat.");
            });

            getOutCommand = new CustomCommand(new CommandHelp(GetOutCommand, "Get out of the boat."), false, false, (g, a) =>
            {
                getInCommand.IsPlayerVisible = true;
                getOutCommand.IsPlayerVisible = false;
                rowCommand.IsPlayerVisible = false;

                return new Reaction(ReactionResult.Inform, "You get out of the boat.");
            });

            rowCommand = new CustomCommand(new CommandHelp(RowCommand, "Get out of the boat."), false, false, (g, a) =>
            {
                if (g.Overworld.CurrentRegion.CurrentRoom.Identifier.Equals(Island.Name))
                {
                    if (g.Overworld.CurrentRegion.TryFindRoom(Torii.Name, out var torii))
                    {
                        g.Overworld.CurrentRegion.CurrentRoom.RemoveItem(item);
                        torii.AddItem(item);
                        var position = g.Overworld.CurrentRegion.GetPositionOfRoom(torii);
                        g.Overworld.CurrentRegion.JumpToRoom(position.Position);
                        return new Reaction(ReactionResult.Inform, $"You row to the {Torii.Name}.");
                    }
                }
                else
                {
                    if (g.Overworld.CurrentRegion.TryFindRoom(Island.Name, out var island))
                    {
                        g.Overworld.CurrentRegion.CurrentRoom.RemoveItem(item);
                        island.AddItem(item);
                        var position = g.Overworld.CurrentRegion.GetPositionOfRoom(island);
                        g.Overworld.CurrentRegion.JumpToRoom(position.Position);
                        return new Reaction(ReactionResult.Inform, $"You row to the {Island.Name}.");
                    }
                }

                return new Reaction(ReactionResult.Inform, "You can't row anywhere.");
            });

            CustomCommand[] commands =
            [
                getInCommand,
                getOutCommand, 
                rowCommand
            ];

            item = new Item(Name, Description, false, commands: commands);
            return item;
        }

        #endregion
    }
}
