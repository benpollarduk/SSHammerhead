using NetAF.Assets;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class Hammer : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Hammer";
        private const string Description = "A small utility hammer use for light engineering tasks.";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Stainless steel", 69.9f },
            { "Rubber", 28.3f }
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
