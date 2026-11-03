using SSHammerhead.ImageHandling;
using SSHammerhead.ResourceHandling;

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
            return EmbeddedResources.Read(key);
        }

        #endregion
    }
}
