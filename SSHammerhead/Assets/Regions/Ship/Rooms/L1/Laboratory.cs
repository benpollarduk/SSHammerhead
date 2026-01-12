using NetAF.Assets.Locations;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Ship.Items;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L1
{
    internal class Laboratory : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Laboratory";

        private static readonly string Description = $"The {Name} is a world of contrast to the rest of the ship. Harsh fluorescent lights illuminate a small lab in pristine condition. " + 
            "Whoever was responsible for this room took their job seriously. Shelves line the wall filled with organised glassware and jars of various liquids and substances. " + 
            "A stainless steel island workbench occupies the center of the room.";
        
        private static readonly string Introduction = $"The door to the {Name} slides open. You go to enter but hesitate. The hairs on the back of your neck bristle as they stand on end. " +
            $"You feel a set of eyes on you but freeze feeling unsure how to continue.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"You spin round in an attempt to see what was watching you, but there is nothing there.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"You feel uneasy, but continue into the {Name}.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, Introduction, [new Exit(Direction.East, true)], [new LabNotebook().Instantiate()]);
        }

        #endregion
    }
}
