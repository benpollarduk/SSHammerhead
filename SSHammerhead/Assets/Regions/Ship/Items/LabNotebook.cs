using NetAF.Assets;
using NetAF.Logic;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Zhiying;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class LabNotebook : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Lab Notebook";

        private const string PhotonicSpores = "Photonic Spores";

        private readonly string Description = $"A lab book. Full of notes, diagrams and tables of data. The name on the front reads: {ZhiyingTemplate.Name}. " + 
            $"Flipping through it looks like {ZhiyingTemplate.Name} was compiling information on {PhotonicSpores}, but there is too much information so sift through manually.";

        internal const string LabNotebookLogName = "LabNotebook";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Paper", 87.12f },
            { "Cardboard", 6.25f },
            { "Ink", 1.02f },
            { "Glue", 3.62f }
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            ExaminationCallback examination = new(request =>
            {
                GameExecutor.ExecutingGame?.NoteManager.Add(LabNotebookLogName, $"{ZhiyingTemplate.Name} was the ships molecular biologist. She was collecting vast amounts of data about {PhotonicSpores}.");
                return ExaminableObject.DefaultExamination(request);
            });

            return new Item(Name, Description, true, examination: examination);
        }

        #endregion
    }
}
