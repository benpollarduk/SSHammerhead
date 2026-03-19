using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Ship.Items;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L1
{
    internal class StasisChamber : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Stasis Chamber";
        private const string Description = "The Stasis Chamber leads into the main scientific area of the ship, located in the port wing. " +
            "The overall feel is still industrial but there is a marked change in the amount of equipment and technology present. Five stasis chambers " +
            "line the walls, three to the north and three to the south. The crew would have been unconscious inside these chambers for the majority of the ships descent " +
            "into deep space.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            Item[] items = [new StasisPodA().Instantiate(), new StasisPodB().Instantiate(), new StasisPodC().Instantiate(), new StasisPodD().Instantiate(), new StasisPodE().Instantiate()];
            return new Room(Name, Description, [new Exit(Direction.East), new Exit(Direction.West, true), new Exit(Direction.North)], items: items);
        }

        #endregion
    }
}
