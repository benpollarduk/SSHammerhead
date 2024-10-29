using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.Core.Rooms.L2
{
    internal class BridgePort : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Bridge (Port)";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.East));
        }

        #endregion
    }
}
