using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L2
{
    internal class Bridge : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Bridge";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.West), new Exit(Direction.East), new Exit(Direction.South)]);
        }

        #endregion
    }
}
