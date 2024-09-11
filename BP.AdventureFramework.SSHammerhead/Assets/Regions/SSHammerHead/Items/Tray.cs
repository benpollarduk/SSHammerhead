using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items
{
    public class Tray : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Tray";
        private const string Description = "A tray containing a range of different cables that have become intertwined.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            var item = new Item(Name, Description);

            item.Examination = x =>
            {
                if (Name.EqualsExaminable(x))
                {
                    item.Morph(new EmptyTray().Instantiate());
                    pC.AquireItem(new USBDrive().Instantiate());
                    return new ExaminationResult($"A tray containing a range of different cables that have become intertwined. Amongst the jumble is a small {USBDrive.Name}, you empty the contents of the tray on to the shelf in front of you. It seems unusual to leave the {USBDrive.Name} here so you take it.");
                }

                return new ExaminationResult("There is nothing else of interest in the tray.");
            };

            return item;
        }

        #endregion
    }
}
