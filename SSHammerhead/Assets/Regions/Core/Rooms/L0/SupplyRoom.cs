using NetAF.Assets.Locations;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L0
{
    internal class SupplyRoom : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Supply Room";
        private static readonly string Description = "The supply room is the rough shape and size of the air lock, " +
            "but has been used by the crew as a makeshift supply room, containing everything from spare parts for the ship to first aid kits. " +
            "On one wall is a small control panel, the outer bezel proudly displaying the Spider Bot branding.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            var room = new Room(Name, Description, new Exit(Direction.West));
            room.AddItem(new Blueprint().Instantiate());
            room.AddItem(new Tray().Instantiate());
            room.AddItem(new MaintenanceControlPanel().Instantiate());
            return room;
        }

        #endregion
    }
}
