using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L2
{
    internal class Bridge : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Bridge";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.West), new Exit(Direction.East), new Exit(Direction.South));
        }

        #endregion
    }
}
