using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    public class Laptop : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Laptop";
        private const string Description = "A tough, industrial laptop. An old model, scuffed around the edges and covered with worn stickers of the previous owners favorite bands.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true, interaction: (item) =>
            {
                if (USBDrive.Name.EqualsIdentifier(item.Identifier))
                {
                    var usbInteraction = $"Loading the {USBDrive.Name} into the laptop causes a window showing the files on the {USBDrive.Name} to pop up. There is a single file, README.txt. " +
                    $"You open it and a text editor window is shown displaying the following:{StringUtilities.Newline}{StringUtilities.Newline}{StringUtilities.Newline}" +
                    "\"Another day being responsible for the everyday maintenance of this hunk we call home. At least the maintenance control system with the spider bot gives me " +
                    "more free time these days. All that free time gives me more time to hang out and listen to music, chilling out looking at that the stars, particularly " +
                    $"that constellation outside of the air lock. There is something about it that calls to me.{StringUtilities.Newline}{StringUtilities.Newline}{StringUtilities.Newline}" +
                    "Anyway, back to work. Good old Scott, - always working like a dog!\"";
                    return new Interaction(InteractionResult.NoChange, item, usbInteraction);
                }

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
