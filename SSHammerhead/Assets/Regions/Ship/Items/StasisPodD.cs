using NetAF.Utilities;
using SSHammerhead.Assets.Players.Alex;
using SSHammerhead.Assets.Players.Marina;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class StasisPodD() : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = $"Stasis Pod ({MarinaTemplate.Name})";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The forth of the Stasis pods, belonging to {MarinaTemplate.Name}.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod is closed but unlocked.";

        #endregion
    }
}
