using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace SSHammerhead.Blazor
{
    /// <summary>
    /// Provides a cache of images
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
            if (provider == null)
            {
                var builder = WebAssemblyHostBuilder.CreateDefault();
                var host = builder.Build();
                var navigationManager = host.Services.GetRequiredService<NavigationManager>();
                var baseUri = navigationManager.BaseUri;
                provider = new CachedImageProvider(baseUri);
            }

            return provider;
        }

        /// <summary>
        /// Cancel all images.
        /// </summary>
        /// <returns>The task.</returns>
        public static async Task CacheAllImages()
        {
            string[] images = ["Images/space.png"];

            foreach (var image in images)
                await GetProvider().CacheImageAsync(image);
        }

        #endregion
    }
}
