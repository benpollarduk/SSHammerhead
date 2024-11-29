using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Logic.Modes;
using NetAF.Rendering.Console.FrameBuilders;
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
            var redButtonCommand = new CustomCommand(new CommandHelp("Press red", "Press the red button on the control panel."), true, true, (game, arguments) =>
            {
                room.FindExit(Direction.West, true, out var west);
                west.Unlock();
                game.Player.Kill();
                return new Reaction(ReactionResult.Inform, "You press the red button on the control panel. The airlock door that leads to outer space opens and in an instant you are sucked out. As you drift in to outer space the SS Hammerhead becomes smaller and smaller until you can no longer see it. You die all alone.");
            });

            var greenButtonCommand = new CustomCommand(new CommandHelp("Press green", "Press the green button on the control panel."), true, true, (game, arguments) =>
            {
                room.FindExit(Direction.East, true, out var east);
                east.Unlock();
                return new Reaction(ReactionResult.Inform, "You press the green button on the control panel. The airlock door that leads to The SS Hammerhead opens.");
            });

            return [redButtonCommand, greenButtonCommand];
        }

        #endregion

        #region Implementation of IAssetTemplate<Room>

        public Room Instantiate()
        {
            Room room = null;

            Description spaceExitDescription = new("An incredibly sturdy metal door with  small reinforced glass porthole." +
                                                   "Peering through the porthole you can see stars in all directions, surrounded by the void of space.");

            var spaceExit = new Exit(Direction.West, true, description: spaceExitDescription);

            var shipExitDescription = new ConditionalDescription("A locked and incredibly sturdy metal door, presumably leading in to the ship.",
                                                                 "The sturdy metal door that separates the airlock from the engine room.",
                                                                 () => room.FindExit(Direction.East, false, out var e) && e.IsLocked);

            var shipExit = new Exit(Direction.East, true, description: shipExitDescription);

            var introduction = "You enter the outer most airlock, and it closes behind you. With a sense of foreboding you see your ship detach from the airlock and retreat to a safe distance.";

            room = new Room(Name, Description, introduction, exits: [spaceExit, shipExit], commands:
            [
                new CustomCommand(new CommandHelp("Peer", "Peer through the porthole in the outer airlock door."), true, true, (g, _) =>
                {
                    var builder = new NaomiSpaceViewFrameBuilder(new NetAF.Rendering.Console.GridStringBuilder());
                    g.ChangeMode(new VisualMode(builder.Build(string.Empty, string.Empty, new NetAF.Rendering.Console.GridVisualBuilder(NetAF.Rendering.Console.AnsiColor.Black, NetAF.Rendering.Console.AnsiColor.White), g.Configuration.DisplaySize)));
                    return new Reaction(ReactionResult.GameModeChanged, string.Empty);
                })
            ]);

            var brokenControlPanel = new BrokenControlPanel().Instantiate();
            room.AddItem(brokenControlPanel);

            var controlPanel = new Item("Control Panel", "A small wall mounted control panel. Written on the top of the panel in a formal font are the words \"Airlock Control\". It has two buttons, green and red. Above the green button is written \"Enter\" and above the red \"Exit\".", commands: CreateControlPannelCommands(room), interaction: (item) =>
            {
                if (Hammer.Name.EqualsIdentifier(item.Identifier))
                {
                    brokenControlPanel.IsPlayerVisible = true;
                    return new Interaction(InteractionResult.TargetExpires, item, $"Slamming the {Hammer.Name} in to the control panel causes it to hiss and smoke pours out. Other than the odd spark it is now lifeless.");
                }

                return new Interaction(InteractionResult.NoChange, item);
            });

            room.AddItem(controlPanel);

            return room;
        }

        #endregion
    }
}
