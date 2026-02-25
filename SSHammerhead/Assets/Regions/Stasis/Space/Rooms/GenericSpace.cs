using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Stasis.Space.Rooms
{
    internal class GenericSpace(string name) : IAssetTemplate<Room>
    {
        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(name, string.Empty);
        }

        #endregion
    }
}
