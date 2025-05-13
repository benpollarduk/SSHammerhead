using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Stasis.SaucepanLand.Rooms
{
    internal class House : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "House";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.North)]);
        }

        #endregion
    }
}
