using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.Core.Items
{
    public class EmptyTray : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Empty Tray";
        private const string Description = "There is nothing else of interest in the tray.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description);
        }

        #endregion
    }
}
