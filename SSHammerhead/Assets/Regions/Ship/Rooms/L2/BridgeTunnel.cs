using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L2
{
    internal class BridgeTunnel : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Bridge Tunnel";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.North), new Exit(Direction.Down)]);
        }

        #endregion
    }
}
