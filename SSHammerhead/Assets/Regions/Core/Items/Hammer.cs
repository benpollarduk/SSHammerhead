using NetAF.Assets;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    public class Hammer : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Hammer";
        private const string Description = "A small utility hammer use for light engineering tasks.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
