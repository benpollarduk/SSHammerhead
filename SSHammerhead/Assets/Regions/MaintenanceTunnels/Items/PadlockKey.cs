using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;
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

        private static readonly Dictionary<string, float> Composition = new()
        {
            { "Stainless steel", 100f }
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true, interaction: (item) =>
            {
                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                    return Scanner.PerformScan(Name, new(Composition));

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
