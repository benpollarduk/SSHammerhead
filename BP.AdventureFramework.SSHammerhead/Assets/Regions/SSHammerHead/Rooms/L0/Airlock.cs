using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items;
using BP.AdventureFramework.SSHammerHead.TextManagement;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L0
{
    internal class Airlock : RoomTemplate<Airlock>
    {
        #region Constants

        internal const string Name = "Airlock";
        private static readonly string Description = Lookup.Text[Name];

        #endregion

        #region Overrides of RoomTemplate<Airlock>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            var room = new Room(Name, Description, new Exit(Direction.East, true), new Exit(Direction.West, true));
            room.AddItem(ControlPanel.Create(pC, room));
            return room;
        }

        #endregion
    }
}
