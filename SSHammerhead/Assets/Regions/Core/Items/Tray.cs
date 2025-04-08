using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class Tray(ExaminationCallback examination) : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Tray";
        private const string Description = "A tray containing a range of different cables that have become intertwined.";

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
            return new(Name, Description, examination: examination, interaction: (item) =>
            {
                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                    return Scanner.PerformScan(Name, new(Composition));

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
