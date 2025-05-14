using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Stasis.SaucepanLand.Rooms
{
    internal class OutsideDameWashalotsHouse : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Outside Dame Washalots House";
        private const string Description = "Description needs to be added.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.West)]);
        }

        #endregion
    }
}
