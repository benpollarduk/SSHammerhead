using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Targets.Console.Rendering;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Naomi.FrameBuilders;
using SSHammerhead.Assets.Regions.Core.Items;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L0
{
    internal class Airlock : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Airlock";
        private static readonly string Description = "The airlock is a small, mostly empty, chamber with two thick doors. " +
            "One leads into the ship, the other back to deep space.";

        internal const string SevenLogName = "Seven";

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

            CustomCommand greenButtonCommand = null;
            greenButtonCommand = new CustomCommand(new CommandHelp("Press green", "Press the green button on the control panel."), true, true, (game, arguments) =>
            {
                room.FindExit(Direction.East, true, out var east);
                east.Unlock();
                greenButtonCommand.IsPlayerVisible = false;
                return new Reaction(ReactionResult.Inform, "You press the green button on the control panel. The airlock door that leads to The SS Hammerhead opens.");
            });

            return [redButtonCommand, greenButtonCommand];
        }

        #endregion

        #region Implementation of IAssetTemplate<Room>

        public Room Instantiate()
        {
            Room room = null;

            Description spaceExitDescription = new("An incredibly sturdy metal door with  small reinforced glass porthole. " +
                                                   "Peering through the porthole you can see stars in all directions, surrounded by the void of space. One constellation in particular jumps out - " +
                                                   "a constellation in the shape of the number 7.");

            var spaceExit = new Exit(Direction.West, true, description: spaceExitDescription, commands:
            [
                new CustomCommand(new CommandHelp("Peer", "Peer through the porthole in the outer airlock door."), true, true, (g, _) =>
                {
                    var builder = new NaomiConsoleSpaceViewFrameBuilder(new GridStringBuilder());
                    g.ChangeMode(new VisualMode(builder.Build(new GridVisualBuilder(AnsiColor.Black, AnsiColor.White), g.Configuration.DisplaySize)));

                    g.NoteManager.Add(SevenLogName, "The constellation outside the airlock appears as a 7.");
                    g.NoteManager.Expire(Laptop.ScottViewLogName);

                    return new Reaction(ReactionResult.GameModeChanged, string.Empty);
                })
            ]);

            var shipExitDescription = new ConditionalDescription("A locked and incredibly sturdy metal door, presumably leading in to the ship.",
                                                                 "The sturdy metal door that separates the airlock from the engine room.",
                                                                 () => room.FindExit(Direction.East, false, out var e) && e.IsLocked);

            var shipExit = new Exit(Direction.East, true, description: shipExitDescription);

            var introduction = "You enter the outer most airlock, and it closes behind you. With a sense of foreboding you see your ship detach from the airlock and retreat to a safe distance.";

            room = new Room(Name, Description, introduction, exits: [spaceExit, shipExit], interaction: (item) =>
            {
                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                    return Scanner.PerformScan(Name, new(SSHammerHead.DefaultRoomComposition));

                return new Interaction(InteractionResult.NoChange, item);
            });

            var brokenControlPanel = new BrokenControlPanel().Instantiate();
            room.AddItem(brokenControlPanel);

            var controlPanel = new Item("Control Panel", "A small wall mounted control panel. Written on the top of the panel in a formal font are the words \"Airlock Control\". It has two buttons, green and red. Above the green button is written \"Enter\" and above the red \"Exit\".", commands: CreateControlPannelCommands(room), interaction: (item) =>
            {
                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                {
                    Dictionary<string, float> composition = new()
                    {
                        { "Steel", 13.47f },
                        { "Copper", 4.3f },
                        { "Zinc", 3.31f },
                        { "Plastic", 36.7f },
                        { "Silver", 1.2f },
                        { "Gold", 0.01f },
                    };

                    return Scanner.PerformScan("Control Panel", new(composition));
                }

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
