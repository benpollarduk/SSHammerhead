using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L1
{
    internal class BridgeTunnelVertical : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Bridge Tunnel (Vertical)";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.South), new Exit(Direction.Up)]);
        }

        #endregion
    }
}
