using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.Core.Rooms.L0
{
    internal class EngineRoom : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Engine Room";
        private static readonly string Description = "This area hosts the large engine that used to power the SS HammerHead. It is now dormant and eerily silent, the fusion mechanism long since powered down. The room itself is very industrial, with metal walkways surrounding the perimeter of the room and the engine itself. A ladder leads upwards from one of these walkways.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.Up), new Exit(Direction.East), new Exit(Direction.West));
        }

        #endregion
    }
}
