using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.Core.Rooms.L1
{
    internal class Booster : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Booster";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.North));
        }

        #endregion
    }
}
