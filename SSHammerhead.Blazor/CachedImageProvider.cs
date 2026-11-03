using SSHammerhead.ImageHandling;
using SSHammerhead.ResourceHandling;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace SSHammerhead.Blazor
{
    /// <summary>
    /// An image provider that caches images and servers them as required.
    /// </summary>
    public class CachedImageProvider : IImageProvider
    {
        #region Fields

        private readonly ConcurrentDictionary<string, MemoryStream> cache = new();

        #endregion

        #region Methods

        /// <summary>
        /// Cache an image, asynchronously.
        /// </summary>
        /// <param name="key">The image key.</param>
        /// <returns>The task.</returns>
        public Task CacheImageAsync(string key)
        {
            if (cache.ContainsKey(key))
                return Task.CompletedTask;

            try
            {
                cache[key] = EmbeddedResources.Read(key);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception caught caching embedded image: {e.Message}");
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Implementation of IImageProvider

        /// <inheritdoc/>
        public MemoryStream GetImageAsStream(string key)
        {
            if (!cache.TryGetValue(key, out var stream) || stream == null)
            {
                try
                {
                    stream = EmbeddedResources.Read(key);
                    cache[key] = stream;
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Exception caught reading embedded image: {e.Message}");
                    return new MemoryStream();
                }
            }

            return new MemoryStream(stream.ToArray());
        }

        #endregion
    }
}
