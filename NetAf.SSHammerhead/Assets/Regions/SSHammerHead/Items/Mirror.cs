using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Items
{
    public class Mirror : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Mirror";
        private const string Description = "A thin telescopic pole with small mirror on the end, enables you to see in tight spaces.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
