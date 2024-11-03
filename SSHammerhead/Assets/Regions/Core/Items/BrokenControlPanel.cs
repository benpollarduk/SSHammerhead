using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Extensions;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    public class BrokenControlPanel : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Broken Control Panel";
        private const string Description = "The beaten up and broken remains of a control panel.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new(Name, Description, interaction: (item) =>
            {
                if (Hammer.Name.EqualsIdentifier(item.Identifier))
                {
                    return new InteractionResult(InteractionEffect.FatalEffect, item, $"Once again you swing the {Hammer.Name} in to the remains of the control panel. You must have hit a high voltage wire inside because you are suddenly electrocuted. You are electrocuted to death.");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            })
            {
                IsPlayerVisible = false,
            };
        }

        #endregion
    }
}
