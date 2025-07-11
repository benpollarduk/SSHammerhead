using NetAF.Utilities;
using SSHammerhead.Assets.Players.Zhiying;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class StasisPodB() : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = $"Stasis Pod ({ZhiyingTemplate.Name})";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The second of the Stasis pods, belonging to {ZhiyingTemplate.Name}.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod is closed but unlocked.";

        #endregion
    }
}
