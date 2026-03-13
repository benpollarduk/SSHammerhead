using SSHammerhead.WPF.Audio;
using System.Media;

namespace SSHammerhead.WPF
{
    internal static class AudioPlayer
    {
        #region StaticProperties

        private static readonly SoundPlayer GeneralSoundPlayer = new();

        #endregion

        #region StaticMethods

        public static void PlaySoundEffect(SoundEffect soundEffect)
        {
        }

        #endregion
    }
}
