using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class BrokenControlPanel : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Broken Control Panel";
        private const string Description = "The beaten up and broken remains of a control panel.";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Steel", 13.47f },
            { "Copper", 4.3f },
            { "Zinc", 3.31f },
            { "Plastic", 36.7f },
            { "Silver", 1.2f },
            { "Gold", 0.01f },
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new(Name, Description, interaction: (item) =>
            {
                if (Hammer.Name.EqualsIdentifier(item.Identifier))
                    return new Interaction(InteractionResult.PlayerDies, item, $"Once again you swing the {Hammer.Name} in to the remains of the control panel. You must have hit a high voltage wire inside because you are suddenly electrocuted. You are electrocuted to death.");

                return new Interaction(InteractionResult.NoChange, item);
            })
            {
                IsPlayerVisible = false,
            };
        }

        #endregion
    }
}
