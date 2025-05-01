using NetAF.Assets;
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

        internal static Dictionary<string, float> Composition => new()
        {
            { "Plastic", 99.1f },
            { "Steel alloy", 0.3f }
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
