using NetAF.Utilities;
using SSHammerhead.Assets.Players.Scott;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class StasisPodA() : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = $"Stasis Pod ({ScottTemplate.Name})";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The first of the Stasis pods, belonging to {ScottTemplate.Name}.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod is closed but unlocked.";

        #endregion
    }
}
