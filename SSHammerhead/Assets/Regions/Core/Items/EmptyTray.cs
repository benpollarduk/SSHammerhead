using NetAF.Assets;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class EmptyTray : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Empty Tray";
        private const string Description = "There is nothing else of interest in the tray.";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Plastic", 99.1f },
            { "Steel alloy", 0.3f }
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description) { IsPlayerVisible = false };
        }

        #endregion
    }
}
