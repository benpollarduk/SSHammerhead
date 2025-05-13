using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Stasis.SaucepanLand.Rooms
{
    internal class Lawn : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Lawn";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.South), new Exit(Direction.Up)]);
        }

        #endregion
    }
}
