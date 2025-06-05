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
            "The overall feel is still industrial but there is a marked change in the amount of equipment and technology present. Six stasis chambers " +
            "line the walls, three to the north and three to the south. The crew would have been unconscious inside these chambers for the majority of the ships descent " +
            "into deep space.";


        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            var scanner = new Scanner().Instantiate();
            scanner.IsPlayerVisible = false;

            var stasisPodC = new StasisPodC(x =>
            {
                if (!scanner.IsPlayerVisible)
                {
                    scanner.IsPlayerVisible = true;
                    x.Scene.Examiner.AddItem(scanner);
                    x.Scene.Room.RemoveItem(scanner);
                    return new Examination($"Inside the Stasis Pod is a {Scanner.Make} {Scanner.Model}, and expensive device for determining the composition of objects. You take it.");
                }

                return ExaminableObject.DefaultExamination.Invoke(x);
            }).Instantiate();

            return new Room(Name, Description, [new Exit(Direction.East), new Exit(Direction.West, true), new Exit(Direction.North)], items: [scanner, new StasisPodA().Instantiate(), new StasisPodB().Instantiate(), stasisPodC, new StasisPodD().Instantiate(), new StasisPodE().Instantiate(), new StasisPodF().Instantiate()]);
        }

        #endregion
    }
}
