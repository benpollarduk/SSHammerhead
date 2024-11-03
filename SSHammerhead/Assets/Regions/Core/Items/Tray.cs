using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Items
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
            return new(Name, Description);
        }

        #endregion
    }
}
