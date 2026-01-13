using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Ship.Items;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L0
{
    internal class SupplyRoom : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Supply Room";

        private static readonly string[] Descriptions =
        [
            "The supply room is the rough shape and size of the air lock, but has been used by the crew as a makeshift supply room, containing everything from spare parts for the ship to first aid kits. On one wall is a small control panel, the outer bezel proudly displaying the Spider Bot branding.",
            "The supply room feels similar to the air lock and has been used by the crew as a supply room, containing all sorts from spare parts for the ship to first aid kits. On one wall is a small control panel, the branding looks familiar but you can't place it.",
            "Just a boring supply room, it looks like it hasn't been cleaned in a long time. It also feels much colder in here but there is no obvious reason why. A small panel screen on the wall makes you feel uneasy to look at, as if it is giving you a headache.",
            "The supply room feels like a prison. Everything in the room looks filthy. On the wall a panel screen appears to sneer at you. Sticky red stains cover it as if someone has bled on it and the blood has been left to dry.",
            "Oh crap, oh crap, help."
        ];

        private const string Introduction = "As you enter the dark room the strip light flickers a handful of times then thankfully remains lit.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            var emptyTray = new EmptyTray().Instantiate();

            Item tray = null;
            tray = new Tray(x =>
            {
                emptyTray.IsPlayerVisible = true;
                tray.IsPlayerVisible = false;
                x.Scene.Examiner.AddItem(new USBDrive().Instantiate());
                return new Examination($"A tray containing a range of different cables that have become intertwined. Among the jumble is a small {USBDrive.Name}, you empty the contents of the tray on to the shelf in front of you. It seems unusual to leave the {USBDrive.Name} here so you take it.");
            }).Instantiate();

            var room = new Room(new Identifier(Name), new SanityDescription(Descriptions), new Description(Introduction), [new Exit(Direction.West)]);
            room.AddItem(new Blueprint().Instantiate());
            room.AddItem(tray);
            room.AddItem(emptyTray);
            room.AddItem(new LockedMaintenanceControlPanel().Instantiate());
            room.AddItem(new MaintenanceControlPanel().Instantiate());
            return room;
        }

        #endregion
    }
}
