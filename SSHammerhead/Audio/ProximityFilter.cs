using NAudio.Dsp;
using NAudio.Wave;
using System;

namespace SSHammerhead.Audio
{
    /// <summary>
    /// Provides a filter that adjusts the audio based on a proximity to a source.
    /// </summary>
    internal class ProximityFilter : ISampleProvider
    {
        #region Fields

        private readonly ISampleProvider sourceProvider;
        private readonly BiQuadFilter[] filters;
        private float targetProximity = 1f;
        private float currentProximity = 1f;
        private float targetVolume = 1f;
        private float currentVolume = 1f;
        private readonly float[][] echoBuffers;
        private readonly int[] echoLengths;
        private int echoPosition = 0;
        private readonly float maxCutoff;

        #endregion

        #region Properties

        /// <summary>
        /// Get the wave format.
        /// </summary>
        public WaveFormat WaveFormat => sourceProvider.WaveFormat;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ProximityFilter class.
        /// </summary>
        /// <param name="sourceProvider">The source provider.</param>
        public ProximityFilter(ISampleProvider sourceProvider)
        {
            this.sourceProvider = sourceProvider;
            filters = new BiQuadFilter[sourceProvider.WaveFormat.Channels];

            // protect against Nyquist limits based on source sample rate
            maxCutoff = Math.Min(20000f, sourceProvider.WaveFormat.SampleRate / 2.1f);

            for (int i = 0; i < filters.Length; i++)
                filters[i] = BiQuadFilter.LowPassFilter(sourceProvider.WaveFormat.SampleRate, maxCutoff, 1f);

            echoBuffers = new float[sourceProvider.WaveFormat.Channels][];
            echoLengths = new int[sourceProvider.WaveFormat.Channels];

            for (int i = 0; i < sourceProvider.WaveFormat.Channels; i++)
            {
                echoLengths[i] = (int)(sourceProvider.WaveFormat.SampleRate * 0.2);
                echoBuffers[i] = new float[echoLengths[i]];
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update the proximity.
        /// </summary>
        /// <param name="proximity">The new proximity.</param>
        /// <param name="instant">If the transition is instant.</param>
        public void UpdateProximity(float proximity, bool instant = false)
        {
            targetProximity = Math.Clamp(proximity, 0f, 1f);

            if (instant) 
                currentProximity = targetProximity;
        }

        /// <summary>
        /// Update the volume.
        /// </summary>
        /// <param name="volume">The new volume.</param>
        /// <param name="instant">If the transition is instant.</param>
        public void UpdateVolume(float volume, bool instant = false)
        {
            targetVolume = Math.Clamp(volume, 0f, 1f);

            if (instant) 
                currentVolume = targetVolume;
        }

        /// <summary>
        /// Reads audio samples from the source into the specified buffer, applying low-pass filtering and echo based on the current proximity setting.
        /// </summary>
        /// <param name="buffer">The array of floats that receives the audio samples read from the source.</param>
        /// <param name="offset">The zero-based index in the buffer at which to begin writing the samples.</param>
        /// <param name="count">The maximum number of samples to read from the source into the buffer.</param>
        /// <returns>The number of samples actually read into the buffer. This value may be less than the requested count if the end of the audio source is reached.</returns>
        public int Read(float[] buffer, int offset, int count)
        {
            var read = sourceProvider.Read(buffer, offset, count);

            // handle looping directly in the read loop to avoid NAudio PlaybackStopped bugs
            if (read == 0 && sourceProvider is AudioFileReader fileReader)
            {
                fileReader.Position = 0;
                read = sourceProvider.Read(buffer, offset, count);
            }

            if (read == 0) 
                return 0;

            // transition over a maximum of 0.5 seconds
            var samplesPerHalfSecond = WaveFormat.SampleRate * 0.5f;
            var maxDelta = (read / (float)WaveFormat.Channels) / samplesPerHalfSecond;

            if (currentProximity < targetProximity)
                currentProximity = Math.Min(currentProximity + maxDelta, targetProximity);
            else if (currentProximity > targetProximity)
                currentProximity = Math.Max(currentProximity - maxDelta, targetProximity);

            if (currentVolume < targetVolume)
                currentVolume = Math.Min(currentVolume + maxDelta, targetVolume);
            else if (currentVolume > targetVolume)
                currentVolume = Math.Max(currentVolume - maxDelta, targetVolume);

            // map proximity 1 to max filter, 0 to 800Hz (muffled)
            float cutoff = 800f * (float)Math.Pow(maxCutoff / 800f, currentProximity);
            cutoff = Math.Clamp(cutoff, 100f, maxCutoff);

            for (int i = 0; i < filters.Length; i++)
                filters[i].SetLowPassFilter(WaveFormat.SampleRate, cutoff, 1f);

            var channels = WaveFormat.Channels;

            // as proximity decreases, increase reverb mix heavily
            var reverbMix = (1f - currentProximity) * 0.6f;

            for (int i = 0; i < read; i++)
            {
                int channel = i % channels;
                float sample = filters[channel].Transform(buffer[offset + i]);

                int echoIndex = echoPosition % echoLengths[channel];
                float echoSample = echoBuffers[channel][echoIndex];

                // re-circulate echo with 0.4 feedback
                echoBuffers[channel][echoIndex] = sample + echoSample * 0.4f;

                // final output handles volume scaling smoothly
                buffer[offset + i] = (sample * (1f - reverbMix) + echoSample * reverbMix) * currentVolume;

                if (channel == channels - 1)
                    echoPosition++;
            }

            return read;
        }

        #endregion
    }
}
