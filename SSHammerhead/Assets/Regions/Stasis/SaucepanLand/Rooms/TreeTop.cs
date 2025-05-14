using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Stasis.SaucepanLand.Rooms
{
    internal class TreeTop : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Tree Top";
        private const string Description = "Description needs to be added.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.Up), new Exit(Direction.Down)]);
        }

        #endregion
    }
}
