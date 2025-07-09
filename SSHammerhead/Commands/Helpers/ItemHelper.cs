using SSHammerhead.Assets.Regions.MaintenanceTunnels.Items;
using SSHammerhead.Assets.Regions.Ship.Items;

namespace SSHammerhead.Commands.Helpers
{
    /// <summary>
    /// Provides helper functions for items.
    /// </summary>
    internal static class ItemHelper
    {
        #region StaticProperties

        /// <summary>
        /// Get all items.
        /// </summary>
        public static readonly ItemTemplateIdentifier[] All =
        {
            new ItemTemplateIdentifier(Hammer.Name, new Hammer()),
            new ItemTemplateIdentifier(Laptop.Name, new Laptop()),
            new ItemTemplateIdentifier(PadlockKey.Name, new PadlockKey()),
            new ItemTemplateIdentifier(PostIt.Name, new PostIt()),
            new ItemTemplateIdentifier(Assets.Regions.Ship.Items.Scanner.Name, new Assets.Regions.Ship.Items.Scanner()),
            new ItemTemplateIdentifier(StasisPodManual.Name, new StasisPodManual()),
            new ItemTemplateIdentifier(USBDrive.Name, new USBDrive())
        };

        #endregion
    }
}