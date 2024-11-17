using NetAF.Assets;
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
                return new Interaction(InteractionResult.NeitherItemOrTargetExpired, item);
            })
            {
                IsPlayerVisible = false,
            };
        }

        #endregion
    }
}
