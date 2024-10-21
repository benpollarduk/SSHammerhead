using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L1
{
    internal class BridgeTunnelEntry : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Bridge Tunnel (Entry)";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.West), new Exit(Direction.South), new Exit(Direction.East));
        }

        #endregion
    }
}
