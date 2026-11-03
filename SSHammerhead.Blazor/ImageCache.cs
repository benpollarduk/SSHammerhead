namespace SSHammerhead.Blazor
{
    /// <summary>
    /// Provides a cache of images.
    /// </summary>
    internal static class ImageCache
    {
        #region StaticFields

        private static CachedImageProvider? provider;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get the provider for cached images.
        /// </summary>
        /// <returns>The provider.</returns>
        public static CachedImageProvider GetProvider()
        {
            provider ??= new CachedImageProvider();
            return provider;
        }

        /// <summary>
        /// Cancel all images.
        /// </summary>
        /// <returns>The task.</returns>
        public static async Task CacheAllImages()
        {
            string[] images = ["Resources/Images/space.png", "Resources/Images/torii.png"];

            foreach (var image in images)
                await GetProvider().CacheImageAsync(image);
        }

        #endregion
    }
}
