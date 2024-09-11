using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L2
{
    internal class BridgeStarboard : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Bridge (Starboard)";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.West));
        }

        #endregion
    }
}
