using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L1
{
    internal class StarboardWingInner : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Starboard Wing Inner";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.East), new Exit(Direction.West, true)]);
        }

        #endregion
    }
}
