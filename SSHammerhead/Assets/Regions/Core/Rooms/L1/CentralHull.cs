using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L1
{
    internal class CentralHull : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Central Hull";

        private const string Description = "This area appears to be roughly in the centre of the ship. " +
            "Dimly lit corridors lead to the north, east, south and west, and a ladder leads back down to the engine room. " +
            "This feels like a hub of sorts.";

        private const string Introduction = "As you climb the ladder through the hatch you hear a scuttling behind you... you swing your head around but whatever it is has gone.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, Introduction, [new Exit(Direction.North, true), new Exit(Direction.South, true), new Exit(Direction.West), new Exit(Direction.East, true), new Exit(Direction.Down)]);
        }

        #endregion
    }
}
