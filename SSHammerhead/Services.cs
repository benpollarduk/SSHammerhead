using SSHammerhead.ImageHandling;

namespace SSHammerhead
{
    /// <summary>
    /// Provides services.
    /// </summary>
    public static class Services
    {
        /// <summary>
        /// Get or set the image provider.
        /// </summary>
        public static IImageProvider ImageProvider { get; set; }
    }
}
