using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L1
{
    internal class MedicalRoom : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Medical Room";
        private const string Description = "The medical room is an area of the ship dedicated to caring for the well-being of the crew.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.East), new Exit(Direction.South)]);
        }

        #endregion
    }
}
