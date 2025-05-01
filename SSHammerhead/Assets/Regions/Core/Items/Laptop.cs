using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Rooms.L0;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class Laptop : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Laptop";
        private const string Description = "A tough, industrial laptop. An old model, scuffed around the edges and covered with worn stickers of the previous owners favorite bands.";

        internal const string ScottManagementLogName = "ScottManagment";
        internal const string ScottViewLogName = "ScottView";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Steel", 5.68f },
            { "Aluminum", 4.82f },
            { "Glass", 21.1f },
            { "Copper", 4.3f },
            { "Zinc", 3.64f },
            { "Plastic", 66.4f },
            { "Silver", 0.2f },
            { "Gold", 0.03f }
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true, interaction: (item) =>
            {
                if (Hammer.Name.EqualsIdentifier(item.Identifier))
                    return new Interaction(InteractionResult.TargetExpires, item, $"The {Name} shatters into pieces. Less tech to constantly annoy you, but you never know when you may have needed that.");

                if (USBDrive.Name.EqualsIdentifier(item.Identifier))
                {
                    GameExecutor.ExecutingGame?.NoteManager.Add(ScottManagementLogName, "Scott manages the maintenance system.");
                    GameExecutor.ExecutingGame?.NoteManager.Add(ScottViewLogName, "Scott likes looking at the stars.");

                    if (GameExecutor.ExecutingGame?.NoteManager?.ContainsEntry(Airlock.SevenLogName) ?? false)
                        GameExecutor.ExecutingGame?.NoteManager.Expire(Airlock.SevenLogName);

                    var usbInteraction = $"Loading the {USBDrive.Name} into the laptop causes a window showing the files on the {USBDrive.Name} to pop up. There is a single file, README.txt. " +
                    $"You open it and a text editor window is shown displaying the following:{StringUtilities.Newline}{StringUtilities.Newline}{StringUtilities.Newline}" +
                    "\"Another day being responsible for the everyday maintenance of this hunk we call home. At least the maintenance control system with the spider bot gives me " +
                    "more free time these days. More time to hang out and listen to music, chilling out looking at that the stars, particularly " +
                    $"that constellation outside of the air lock. There is something about it that calls to me.{StringUtilities.Newline}{StringUtilities.Newline}{StringUtilities.Newline}" +
                    "Anyway, back to work. Good old Scott - always working like a dog!\"";

                    return new Interaction(InteractionResult.NoChange, item, usbInteraction);
                }

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
