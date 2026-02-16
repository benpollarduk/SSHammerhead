using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Assets.Regions.Ship.Rooms.L0;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L1
{
    internal class CentralHull : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Central Hull";

        private const string Description = "This area appears to be roughly in the centre of the ship. " +
            "Dimly lit corridors lead to the north, east, south and west, and a ladder leads back down to the engine room. " +
            "This feels like a hub of sorts.";

        private const string Introduction = "Climbing up the ladded you emerge on the main level of the ship in its central hull.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            RoomTransitionCallback enter = t =>
            {
                GameExecutor.ExecutingGame?.NoteManager.Expire(Airlock.SevenLogName);
                GameExecutor.ExecutingGame?.NoteManager.Expire(Laptop.ScottManagementLogName);
                GameExecutor.ExecutingGame?.NoteManager.Expire(Laptop.ScottViewLogName);

                return RoomTransitionReaction.Silent;
            };

            return new Room(Name, Description, Introduction, [new Exit(Direction.North), new Exit(Direction.South, true), new Exit(Direction.West), new Exit(Direction.East, true), new Exit(Direction.Down)], enterCallback: enter);
        }

        #endregion
    }
}
