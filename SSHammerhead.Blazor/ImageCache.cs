using SSHammerhead.ImageHandling;

namespace SSHammerhead.Blazor
{
    /// <summary>
    /// Provides a cache of images
    /// </summary>
    internal static class ImageCache
    {
        #region StaticProperties

        /// <summary>
        /// Get the provider of cached images.
        /// </summary>
        internal static readonly CachedImageProvider Provider = new(@"https://benpollarduk.github.io/SSHammerhead/");

        #endregion

        #region StaticMethods

        /// <summary>
        /// Cancel all images.
        /// </summary>
        /// <returns>The task.</returns>
        public static async Task CacheAllImages()
        {
            string[] images = ["Images/space.png"];

            foreach (var image in images)
                await Provider.CacheImageAsync(image);
        }

        #endregion
    }
}
