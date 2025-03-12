using System.IO;

namespace SSHammerhead.ImageHandling
{
    /// <summary>
    /// Represents any object that can provide images.
    /// </summary>
    public interface IImageProvider
    {
        /// <summary>
        /// Get the image as a stream.
        /// </summary>
        /// <param name="key">The image key.</param>
        /// <returns>The image as a stream.</returns>
        MemoryStream GetImageAsStream(string key);
    }
}
