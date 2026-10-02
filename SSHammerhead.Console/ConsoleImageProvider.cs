using SSHammerhead.ImageHandling;

namespace SSHammerhead.Console
{
    /// <summary>
    /// An image provider for the System.Console.
    /// </summary>
    public class ConsoleImageProvider : IImageProvider
    {
        #region Implementation of IImageProvider

        /// <inheritdoc/>
        public MemoryStream GetImageAsStream(string key)
        {
            var fileBytes = File.ReadAllBytes(key);
            return new MemoryStream(fileBytes);
        }

        #endregion
    }
}
