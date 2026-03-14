using SSHammerhead.WPF.Audio;
using NAudio.Wave;

namespace SSHammerhead.WPF
{
    /// <summary>
    /// Provides functionality for playing audio.
    /// </summary>
    public static class AudioPlayer
    {
        #region StaticProperties

        private static Random random = new Random();

        private static readonly string[] KeySoundPaths =
        [
            "Resources/Sounds/Effects/key1.wav",
            "Resources/Sounds/Effects/key2.wav",
            "Resources/Sounds/Effects/key3.wav",
            "Resources/Sounds/Effects/key4.wav",
            "Resources/Sounds/Effects/key6.wav",
            "Resources/Sounds/Effects/key7.wav",
            "Resources/Sounds/Effects/key8.wav",
            "Resources/Sounds/Effects/key9.wav",
            "Resources/Sounds/Effects/key10.wav",
            "Resources/Sounds/Effects/key11.wav",
            "Resources/Sounds/Effects/key12.wav",
            "Resources/Sounds/Effects/key13.wav",
            "Resources/Sounds/Effects/key15.wav"
        ];

        #endregion

        #region StaticMethods

        /// <summary>
        /// Play a sound effect.
        /// </summary>
        /// <param name="soundEffect">The sound effect to play.</param>
        /// <param name="volume">The volume of the sound playback as a normalised value betwee 0 and 1.</param>
        public static void PlaySoundEffect(SoundEffect soundEffect, float volume = 1)
        {
            switch (soundEffect)
            {
                case SoundEffect.KeyPressCharacterRandom:

                    PlayKeyPress(volume);

                    break;

                case SoundEffect.KeyPressEnter:

                    PlayFromFile("Resources/Sounds/Effects/key5.wav", volume);

                    break;

                case SoundEffect.KeyPressSpace:

                    PlayFromFile("Resources/Sounds/Effects/key14.wav", volume);

                    break;

                default:

                    throw new NotImplementedException();
            }
        }

        private static void PlayKeyPress(float volume)
        {
            var index = random.Next(KeySoundPaths.Length);
            var path = KeySoundPaths[index];
            PlayFromFile(path, volume);
        }

        private static void PlayFromFile(string path, float volume)
        {
            var waveOut = new WaveOutEvent();
            var audioFile = new AudioFileReader(path)
            {
                Volume = volume
            };

            waveOut.Init(audioFile);

            waveOut.PlaybackStopped += (sender, args) =>
            {
                audioFile.Dispose();
                waveOut.Dispose();
            };

            waveOut.Play();
        }

        #endregion
    }
}
