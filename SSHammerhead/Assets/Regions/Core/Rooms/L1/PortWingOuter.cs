using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L1
{
    internal class PortWingOuter : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Port Wing Outer";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.East)]);
        }

        #endregion
    }
}
