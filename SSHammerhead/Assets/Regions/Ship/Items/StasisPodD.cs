﻿using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class StasisPodD() : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = "Stasis Pod (D)";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The forth of the Stasis pods.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod is closed but unlocked.";

        #endregion
    }
}
