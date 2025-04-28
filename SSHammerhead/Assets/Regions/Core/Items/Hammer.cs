using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class Hammer : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Hammer";
        private const string Description = "A small utility hammer use for light engineering tasks.";

        #endregion

        #region StaticProperties

        private static readonly Dictionary<string, float> Composition = new()
        {
            { "Stainless steel", 69.9f },
            { "Rubber", 28.3f }
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true, interaction: (item) =>
            {
                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                    return Scanner.PerformScan(new(Name, Composition));

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
