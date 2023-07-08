using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items
{
    public class USBDrive : ItemTemplate<USBDrive>
    {
        #region Constants

        internal const string Name = "USB Drive";
        private const string Description = "A small 1GB USB drive.";

        #endregion

        #region Overrides of ItemTemplate<Mirror>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The region.</returns>
        protected override Item OnCreate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
