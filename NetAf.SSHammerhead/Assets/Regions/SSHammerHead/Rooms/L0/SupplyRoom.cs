using NetAF.Assets.Locations;
using NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Items;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L0
{
    internal class SupplyRoom : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Supply Room";
        private static readonly string Description = "The supply room is the rough shape and size of the air lock, but has been used by the crew as a makeshift supply room, containing everything from spare parts for the ship to first aid kits.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            var room = new Room(Name, Description, new Exit(Direction.West));
            room.AddItem(new Blueprint().Instantiate());
            room.AddItem(new Tray().Instantiate());
            return room;
        }

        #endregion
    }
}
