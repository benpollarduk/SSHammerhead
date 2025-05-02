using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L1
{
    internal class MedicalRoom : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Medical Room";
        private const string Description = "Todo.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.East, true), new Exit(Direction.South)]);
        }

        #endregion
    }
}
