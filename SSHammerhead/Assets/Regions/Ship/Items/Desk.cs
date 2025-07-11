using NetAF.Assets;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class Desk : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Desk";
        private const string Description = "A messy desk sat in the corner of the room. It is covered in books, old manuals, instruction booklets and other technical documents.";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Aluminum", 79.25f },
            { "Steel", 0.33f },
            { "Plastics", 10.12f },
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            var examination = new ExaminationCallback(r =>
            {
                if (!r.Scene.Room.FindItem(StasisPodManual.Name, out var manual, true) || manual.IsPlayerVisible)
                    return new Examination(Description);

                manual.IsPlayerVisible = true;
                return new Examination($"Amongst the various documentation on the desk is a fairly thick document that captures your interest, a {StasisPodManual.Name}.");
            });
            return new(Name, Description, examination: examination);
        }

        #endregion
    }
}
