using SSHammerhead.ImageHandling;
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
            var fileBytes = File.ReadAllBytes(key);
            return new MemoryStream(fileBytes);
        }

        #endregion
    }
}
