using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class USBDrive : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "USB Drive";
        private const string Description = "A small 1GB USB drive.";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Aluminum", 21.25f },
            { "Copper", 1.3f },
            { "Zinc", 1.31f },
            { "Plastic", 57.7f },
            { "Silver", 0.02f },
            { "Gold", 0.001f },
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true, interaction: (item) =>
            {
                if (Hammer.Name.EqualsIdentifier(item.Identifier))
                    return new Interaction(InteractionResult.NoChange, item, $"The {Name} is too tough to destroy like that.");

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
