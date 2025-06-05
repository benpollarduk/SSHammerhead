using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L1
{
    internal class StarboardWing : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Starboard Wing";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.West)]);
        }

        #endregion
    }
}
