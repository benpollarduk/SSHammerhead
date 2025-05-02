using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class StasisPodE() : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = "Stasis Pod (E)";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The fifth of the Stasis pods.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod closed but unlocked.";

        #endregion
    }
}
