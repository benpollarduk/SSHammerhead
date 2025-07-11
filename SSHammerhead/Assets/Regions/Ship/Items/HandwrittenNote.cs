using NetAF.Assets;
using NetAF.Logic;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class HandwrittenNote : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Handwritten Note";
        private readonly string Description = $"The note is a hurriedly written affair, scribbled in messy handwriting:{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"\"We hurriedly set up this barrier to try and buy some time, god alone knows if it will work. To prevent any one of us from being able to unlock the barrier " +
            $"we all selected a 2 character hexadecimal code, first Scott, then Zhiying, me, then Marina and finally Alex. We entered our codes, then entered stasis and left the codes " +
            $"safe within each of our dreams so that we didn't forget them. If anyone finds this please DO NOT, repeat DO NOT try and unlock the barrier, I implore you!\"";

        internal const string HandwrittenNoteLogName = "HandwrittenNote";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Paper", 96.21f },
            { "Ink", 2.17f }
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            ExaminationCallback examination = new(request =>
            {
                GameExecutor.ExecutingGame?.NoteManager.Add(HandwrittenNoteLogName, $"Each of the 5 crew members selected a 2 character hexadecimal code and hid them somewhere in their stasis dreams. Together these form an unlock code for the {LaserBarrier.Name}.");
                return ExaminableObject.DefaultExamination(request);
            });

            return new Item(Name, Description, true, examination: examination);
        }

        #endregion
    }
}
