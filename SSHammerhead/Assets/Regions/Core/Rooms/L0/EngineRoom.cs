using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L0
{
    internal class EngineRoom : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Engine Room";
        private static readonly string NoPostItDescription = "This area hosts the large engine that used to power the SS HammerHead. It is now dormant and eerily silent, " +
            "the fusion mechanism long since powered down. The room itself is very industrial, with metal walkways surrounding the perimeter of the room and the engine itself. " +
            "A ladder leads upwards from one of these walkways towards the central hull.";
        private static readonly string PostItDescription = NoPostItDescription + $"{StringUtilities.Newline}{StringUtilities.Newline}A yellow {PostIt.Name} is stuck to the door frame.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            Room room = null;

            var description = new ConditionalDescription(PostItDescription, NoPostItDescription, () => room.FindItem(PostIt.Name, out _));
            room = new Room(new Identifier(Name), description, [new Exit(Direction.Up, true), new Exit(Direction.East), new Exit(Direction.West)], items: [new Laptop().Instantiate(), new PostIt().Instantiate()]);

            return room;
        }

        #endregion
    }
}
