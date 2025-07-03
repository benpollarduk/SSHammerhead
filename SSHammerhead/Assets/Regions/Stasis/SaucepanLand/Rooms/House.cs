using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Stasis.SaucepanLand.Rooms
{
    internal class House : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "House";
        private const string Description = "Description needs to be added.";
        private readonly string Introduction = $"You get into the stasis pod, strap yourself in and relax. The push of a button on the control panel by your right hand engages stasis mode. Slowly you loose consciousness.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"Groggily you open your eyes to find yourself in a land completely detached from the ship. It is eerily quiet and disconcertingly doesn't feel real at all...";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, Introduction, [new Exit(Direction.North)]);
        }

        #endregion
    }
}
