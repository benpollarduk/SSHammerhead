using NetAF.Assets;
using NetAF.Extensions;
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

        private static readonly Dictionary<string, float> Composition = new()
        {
            { "Plastic", 99.1f },
            { "Steel alloy", 0.3f }
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, interaction: (item) =>
            {
                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                    return Scanner.PerformScan(Name, new(Composition));

                return new Interaction(InteractionResult.NoChange, item);
            })
            { 
                IsPlayerVisible = false 
            };
        }

        #endregion
    }
}
