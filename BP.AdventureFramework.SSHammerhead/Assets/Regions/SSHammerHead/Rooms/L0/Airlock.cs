using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L0
{
    internal class Airlock : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Airlock";
        private static readonly string Description = "The airlock is a small, mostly empty, chamber with two thick doors. One leads in to the ship, the other back to deep space.";

        public Room Instantiate()
        {
            var room = new Room(Name, Description, new Exit(Direction.East, true), new Exit(Direction.West, true));
            room.AddItem(new ControlPanel().Instantiate());
            return room;
        }

        #endregion
    }
}
