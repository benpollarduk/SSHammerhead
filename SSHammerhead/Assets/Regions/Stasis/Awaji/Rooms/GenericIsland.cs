using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Stasis.Awaji.Rooms
{
    internal class GenericIsland(string name) : IAssetTemplate<Room>
    {
        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(name, string.Empty);
        }

        #endregion
    }
}
