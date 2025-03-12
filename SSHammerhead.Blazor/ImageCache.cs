using SSHammerhead.ImageHandling;

namespace SSHammerhead.Blazor
{
    /// <summary>
    /// Provides a cache of images
    /// </summary>
    internal static class ImageCache
    {
        #region Constants

        /// <summary>
        /// Get the base url.
        /// </summary>
        private static string BaseUrl = @"https://benpollarduk.github.io/SSHammerhead/";

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get the provider of cached images.
        /// </summary>
        internal static readonly CachedImageProvider Provider = new(BaseUrl);

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
