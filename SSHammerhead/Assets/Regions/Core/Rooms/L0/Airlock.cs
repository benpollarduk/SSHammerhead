using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L0
{
    internal class Airlock : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Airlock";
        private static readonly string Description = "The airlock is a small, mostly empty, chamber with two thick doors. " +
            "One leads in to the ship, the other back to deep space.";

        #endregion

        #region StaticMethods

        private static CustomCommand[] CreateControlPannelCommands(Room room)
        {
            var redButtonCommand = new CustomCommand(new CommandHelp("Press red", "Press the red button on the control panel."), true, (game, arguments) =>
            {
                room.FindExit(Direction.West, true, out var west);
                west.Unlock();
                const string result = "You press the red button on the control panel. The airlock door that leads to outer space opens and in an instant you are sucked out. As you drift in to outer space the SS Hammerhead becomes smaller and smaller until you can no longer see it. You die all alone.";
                return new Reaction(ReactionResult.Fatal, result);
            });

            var greenButtonCommand = new CustomCommand(new CommandHelp("Press green", "Press the green button on the control panel."), true, (game, arguments) =>
            {
                room.FindExit(Direction.East, true, out var east);
                east.Unlock();
                return new Reaction(ReactionResult.OK, "You press the green button on the control panel. The airlock door that leads to The SS Hammerhead opens.");
            });

            return [redButtonCommand, greenButtonCommand];
        }

        #endregion

        #region Implementation of IAssetTemplate<Room>

        public Room Instantiate()
        {
            Room room = null;

            var spaceExit = new Exit(Direction.West, true) 
            { 
                Description = new Description("An incredibly sturdy metal door with  small reinforced glass porthole." +
                "Peering through the porthole you can see stars in all directions, surrounded by the void of space.")
            };

            var shipExit = new Exit(Direction.East, true)
            {
                Description = new ConditionalDescription(
                    "A locked and incredibly sturdy metal door, presumably leading in to the ship.",
                    "The sturdy metal door that separates the airlock from the engine room.",
                    () => room.FindExit(Direction.East, false, out var e) && e.IsLocked)
            };
            
            room = new Room(Name, Description, spaceExit, shipExit);

            var controlPanel = new Item("Control Panel", "A small wall mounted control panel. Written on the top of the panel in a formal font are the words \"Airlock Control\". It has two buttons, green and red. Above the green button is written \"Enter\" and above the red \"Exit\".") { Commands = CreateControlPannelCommands(room) };

            controlPanel.Interaction = (item) =>
            {
                if (Hammer.Name.EqualsIdentifier(item.Identifier))
                {
                    room.RemoveItem(controlPanel);
                    room.AddItem(new BrokenControlPanel().Instantiate());
                    return new InteractionResult(InteractionEffect.ItemMorphed, item, $"Slamming the {Hammer.Name} in to the control panel causes it to hiss and smoke pours out. Other than the odd spark it is now lifeless.");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            room.AddItem(controlPanel);

            return room;
        }

        #endregion
    }
}
