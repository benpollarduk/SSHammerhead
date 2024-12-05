using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    public class USBDrive : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "USB Drive";
        private const string Description = "A small 1GB USB drive.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
