using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items
{
    public class EmptyTray : ItemTemplate<EmptyTray>
    {
        #region Constants

        internal const string Name = "Empty Tray";
        private const string Description = "There is nothing else of interest in the tray.";

        #endregion

        #region Overrides of ItemTemplate<EmptyTray>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The item.</returns>
        protected override Item OnCreate()
        {
            return new Item(Name, Description);
        }

        #endregion
    }
}
