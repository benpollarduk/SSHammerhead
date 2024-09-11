using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items
{
    public class Hammer : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Hammer";
        private const string Description = "A small utility hammer use for small engineering tasks.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
