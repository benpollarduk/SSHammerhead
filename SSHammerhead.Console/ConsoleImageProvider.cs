using SSHammerhead.ImageHandling;

namespace SSHammerhead.Console
{
    /// <summary>
    /// An image provider for the System.Console.
    /// </summary>
    public class ConsoleImageProvider : IImageProvider
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
