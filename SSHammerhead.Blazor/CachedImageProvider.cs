using SSHammerhead.ImageHandling;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace SSHammerhead.Blazor
{
    /// <summary>
    /// An image provider that caches images and servers them as required.
    /// </summary>
    /// <param name="baseUrl">The base Url.</param>
    public class CachedImageProvider(string baseUrl) : IImageProvider
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
        public async Task CacheImageAsync(string key)
        {
            if (cache.ContainsKey(key))
                return;

            var imageUrl = baseUrl + key;
            cache[key] = await DownloadImageAsync(imageUrl);
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Download an image, asynchronously.
        /// </summary>
        /// <param name="url">The Url of the image.</param>
        /// <returns>The memory stream containing the image.</returns>
        private static async Task<MemoryStream> DownloadImageAsync(string url)
        {
            try
            {
                byte[] bytes;

                using (var client = new HttpClient())
                    bytes = await client.GetByteArrayAsync(url);

                return new MemoryStream(bytes);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception caught downloading image async: {e.Message}");
                return new();
            }
        }

        #endregion

        #region Implementation of IImageProvider

        /// <summary>
        /// Get the image as a stream.
        /// </summary>
        /// <param name="key">The image key.</param>
        /// <returns>The image as a stream.</returns>
        public MemoryStream GetImageAsStream(string key)
        {
            cache.TryGetValue(key, out var stream);

            if (stream != null)
                return new MemoryStream(stream.ToArray());
            else
                return new MemoryStream();
        }

        #endregion
    }
}
