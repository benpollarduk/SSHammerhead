using NetAF.Assets;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels.Items
{
    public class PadlockKey : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Padlock Key";
        private const string Description = "A small key that looks as if it fits a padlock.";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Stainless steel", 100f }
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
