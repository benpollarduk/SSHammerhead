using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.SSHammerHead.TextManagement;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L0
{
    internal class EngineRoom : RoomTemplate<EngineRoom>
    {
        #region Constants

        private const string Name = "Engine Room";
        private static readonly string Description = Lookup.Text[Name];

        #endregion

        #region Overrides of RoomTemplate<EngineRoom>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            return new Room(Name, Description, new Exit(Direction.Up), new Exit(Direction.East), new Exit(Direction.West));
        }

        #endregion
    }
}
