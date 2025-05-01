using NetAF.Assets;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class StasisPodA(ExaminationCallback examination) : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Stasis Pod (A)";
        private const string Description = "The first of the Stasis pods.";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Plastics", 47.21f },
            { "Aluminum", 34.39f },
            { "Steel", 3.27f },
            { "Perspex", 1.65f },
            { "Copper", 7.35f },
            { "Silver", 3.21f },
            { "Gold", 0.23f },
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new(Name, Description, examination: examination);
        }

        #endregion
    }
}
