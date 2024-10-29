using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.Core.Rooms.L1
{
    internal class BridgeTunnelVertical : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Bridge Tunnel (Vertical)";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.South), new Exit(Direction.Up));
        }

        #endregion
    }
}
