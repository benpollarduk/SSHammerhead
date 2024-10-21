using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L1
{
    internal class StarboardWingOuter : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Starboard Wing Outer";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.West));
        }

        #endregion
    }
}
