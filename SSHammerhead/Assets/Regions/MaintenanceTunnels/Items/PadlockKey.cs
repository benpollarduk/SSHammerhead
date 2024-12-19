using NetAF.Assets;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels.Items
{
    public class PadlockKey : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Padlock Key";
        private const string Description = "A small key that looks as if it fits a padlock.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
