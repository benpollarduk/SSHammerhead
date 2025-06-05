using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class StasisPodB() : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = "Stasis Pod (B)";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The second of the Stasis pods.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod closed but unlocked.";

        #endregion
    }
}
