using SSHammerhead.ImageHandling;
using SSHammerhead.ResourceHandling;
using System.IO;

namespace SSHammerhead.WPF
{
    /// <summary>
    /// An image provider for WPF.
    /// </summary>
    public class WpfImageProvider : IImageProvider
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
