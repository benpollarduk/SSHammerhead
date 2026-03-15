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

        private static readonly Random random = new Random();
        private static WaveOutEvent? backgroundMusicWaveOut;
        private static AudioFileReader? backgroundMusicReader;
        private static ProximityFilter? backgroundProximityFilter;
        private static bool shouldLoopBackgroundMusic;

        private static readonly string[] KeySoundPaths =
        [
            "Resources/Audio/Effects/key1.wav",
            "Resources/Audio/Effects/key2.wav",
            "Resources/Audio/Effects/key3.wav",
            "Resources/Audio/Effects/key4.wav",
            "Resources/Audio/Effects/key6.wav",
            "Resources/Audio/Effects/key7.wav",
            "Resources/Audio/Effects/key8.wav",
            "Resources/Audio/Effects/key9.wav",
            "Resources/Audio/Effects/key10.wav",
            "Resources/Audio/Effects/key11.wav",
            "Resources/Audio/Effects/key12.wav",
            "Resources/Audio/Effects/key13.wav",
            "Resources/Audio/Effects/key15.wav"
        ];

        #endregion

        #region StaticMethods

        /// <summary>
        /// Start background music.
        /// </summary>
        /// <param name="volume">The volume of the sound playback as a normalised value between 0 and 1.</param>
        /// <param name="proximity">The proximity to the source as a normalised value between 0 and 1. The higher the value the closer the proximity.</param>
        public static void StartBackgroundMusic(float volume = 1, float proximity = 1)
        {
            StopBackgroundMusic();

            backgroundMusicWaveOut = new WaveOutEvent();
            backgroundMusicReader = new AudioFileReader("Resources/Audio/Music/radio.mp3");

            var totalMilliseconds = backgroundMusicReader.TotalTime.TotalMilliseconds;

            if (totalMilliseconds > 0)
            {
                var startMilliseconds = random.NextDouble() * totalMilliseconds;
                backgroundMusicReader.CurrentTime = TimeSpan.FromMilliseconds(startMilliseconds);
            }

            backgroundMusicReader.Volume = 1;
            shouldLoopBackgroundMusic = true;

            backgroundProximityFilter = new ProximityFilter(backgroundMusicReader);
            backgroundProximityFilter.UpdateProximity(proximity, true);
            backgroundProximityFilter.UpdateVolume(volume * proximity, true);

            backgroundMusicWaveOut.Init(backgroundProximityFilter);

            backgroundMusicWaveOut.PlaybackStopped += (sender, args) =>
            {
                if (shouldLoopBackgroundMusic && backgroundMusicReader != null)
                {
                    backgroundMusicReader.CurrentTime = TimeSpan.Zero;
                    backgroundMusicWaveOut?.Play();
                }
            };

            backgroundMusicWaveOut.Play();
        }

        /// <summary>
        /// Adjust the background music.
        /// </summary>
        /// <param name="volume">The volume of the sound playback as a normalised value between 0 and 1.</param>
        /// <param name="proximity">The proximity to the source as a normalised value between 0 and 1. The higher the value the closer the proximity.</param>
        public static void AdjustBackgroundMusic(float volume = 1, float proximity = 1)
        {
            if (backgroundProximityFilter != null)
            {
                backgroundProximityFilter.UpdateProximity(proximity);
                backgroundProximityFilter.UpdateVolume(volume * proximity);
            }
        }

        /// <summary>
        /// Stop the background music.
        /// </summary>
        public static void StopBackgroundMusic()
        {
            shouldLoopBackgroundMusic = false;

            if (backgroundMusicWaveOut != null)
            {
                backgroundMusicWaveOut.Stop();
                backgroundMusicWaveOut.Dispose();
                backgroundMusicWaveOut = null;
            }

            if (backgroundMusicReader != null)
            {
                backgroundMusicReader.Dispose();
                backgroundMusicReader = null;
            }

            backgroundProximityFilter = null;
        }

        /// <summary>
        /// Play a sound effect.
        /// </summary>
        /// <param name="soundEffect">The sound effect to play.</param>
        /// <param name="volume">The volume of the sound playback as a normalised value between 0 and 1.</param>
        public static void PlaySoundEffect(SoundEffect soundEffect, float volume = 1)
        {
            switch (soundEffect)
            {
                case SoundEffect.KeyPressCharacterRandom:

                    PlayKeyPress(volume);

                    break;

                case SoundEffect.KeyPressEnter:

                    PlayFromFile("Resources/Audio/Effects/key5.wav", volume);

                    break;

                case SoundEffect.KeyPressSpace:

                    PlayFromFile("Resources/Audio/Effects/key14.wav", volume);

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
