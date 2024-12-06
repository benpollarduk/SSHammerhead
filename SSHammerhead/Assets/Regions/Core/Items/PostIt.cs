using NetAF.Assets;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    public class PostIt : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Post-it";
        private readonly string Description = $"A small square yellow piece of paper with a sticky edge, commonly written on and used to display reminders.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"A message has been scrawled on this one in black sharpie:{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"\"To whomever finds this, know that I had to abandon the ship and the others. I didn't want to but my duty bound me. Before I left I locked down access to the central hull.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"Once I'd done that I stuffed my access ID through the grating in the side of the maintenance shaft used by the spider bot where it was out of my reach in case I had a change of heart and decided to try to save the ship and the guys.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"I know that is far beyond my reach but I worry that will have a change of heart. Now I plan to take the escape pod and leave the ship behind. I would rather take my chances adrift in space alone.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"Anne.\"";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
