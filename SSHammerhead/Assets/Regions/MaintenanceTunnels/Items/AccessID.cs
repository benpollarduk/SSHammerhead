using NetAF.Assets;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels.Items
{
    public class AccessID() : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Access ID";
        private const string Description = "A small credit card sized ID card. On it is the name 'Anne Sinclair' and a picture of a smiling brunette lady who appears to be in her early 40s.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
