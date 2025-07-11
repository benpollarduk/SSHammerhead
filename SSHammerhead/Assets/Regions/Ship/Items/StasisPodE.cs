using NetAF.Utilities;
using SSHammerhead.Assets.Players.Alex;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class StasisPodE() : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = $"Stasis Pod ({AlexTemplate.Name})";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The last of the Stasis pods, belonging to {AlexTemplate.Name}.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod is closed but unlocked.";

        #endregion
    }
}
