using SSHammerhead.ImageHandling;
using System.IO;

namespace SSHammerhead.Console
{
    /// <summary>
    /// An image provider for WPF.
    /// </summary>
    public class WpfImageProvider : IImageProvider
    {
        #region Implementation of IImageProvider

        /// <summary>
        /// Get the image as a stream.
        /// </summary>
        /// <param name="key">The image key.</param>
        /// <returns>The image as a stream.</returns>
        public MemoryStream GetImageAsStream(string key)
        {
            var fileBytes = File.ReadAllBytes(key);
            return new MemoryStream(fileBytes);
        }

        #endregion
    }
}
