using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class StasisPodA() : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = "Stasis Pod (A)";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The first of the Stasis pods.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod closed but unlocked.";

        #endregion
    }
}
