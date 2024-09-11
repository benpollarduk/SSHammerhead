using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L1
{
    internal class StarboardWing : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Starboard Wing";
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
