using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Stasis.Awaji.Rooms
{
    internal class Torii : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Torii";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, string.Empty);
        }

        #endregion
    }
}
