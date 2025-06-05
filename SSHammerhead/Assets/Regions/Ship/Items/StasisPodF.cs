using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class StasisPodF() : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = "Stasis Pod (F)";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The sixth of the Stasis pods.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod closed but unlocked.";

        #endregion
    }
}
