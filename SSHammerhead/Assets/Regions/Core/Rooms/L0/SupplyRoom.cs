using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L0
{
    internal class SupplyRoom : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Supply Room";
        private static readonly string Description = "The supply room is the rough shape and size of the air lock, " +
            "but has been used by the crew as a makeshift supply room, containing everything from spare parts for the ship to first aid kits. " +
            "On one wall is a small control panel, the outer bezel proudly displaying the Spider Bot branding.";

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
                return new Examination($"A tray containing a range of different cables that have become intertwined. Amongst the jumble is a small {USBDrive.Name}, you empty the contents of the tray on to the shelf in front of you. It seems unusual to leave the {USBDrive.Name} here so you take it.");
            }).Instantiate();

            var room = new Room(Name, Description, [new Exit(Direction.West)]);
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
