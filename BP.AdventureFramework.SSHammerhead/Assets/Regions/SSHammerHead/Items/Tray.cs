using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items
{
    public class Tray : ItemTemplate<Tray>
    {
        #region Constants

        internal const string Name = "Tray";
        private const string Description = "A tray containing a range of different cables that have become intertwined.";

        #endregion

        #region Overrides of ItemTemplate<Tray>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <param name="room">The room.</param>
        /// <returns>The item.</returns>
        protected override Item OnCreate(PlayableCharacter pC, Room room)
        {
            var item = new Item(Name, Description);

            item.Examination = x =>
            {
                if (Name.EqualsExaminable(x))
                {
                    item.Morph(EmptyTray.Create());
                    pC.AquireItem(USBDrive.Create());
                    return new ExaminationResult($"A tray containing a range of different cables that have become intertwined. Amongst the jumble is a small {USBDrive.Name}, you empty the contents of the tray on to the shelf in front of you. It seems unusual to leave the {USBDrive.Name} here so you take it.", ExaminationResults.DescriptionReturned);
                }

                return new ExaminationResult("There is nothing else of interest in the tray.", ExaminationResults.DescriptionReturned);
            };

            return item;
        }

        #endregion
    }
}
