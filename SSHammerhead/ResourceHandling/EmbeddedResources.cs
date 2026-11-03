using System;
using System.IO;
using System.Reflection;

namespace SSHammerhead.ResourceHandling
{
    /// <summary>
    /// Provides access to resources embedded in this assembly.
    /// </summary>
    public static class EmbeddedResources
    {
        #region StaticProperties

        private static readonly Assembly ResourceAssembly = typeof(EmbeddedResources).Assembly;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Open an embedded resource as a stream.
        /// </summary>
        /// <param name="key">The resource key, for example "Resources/Images/space.png".</param>
        /// <returns>The resource stream.</returns>
        public static Stream Open(string key)
        {
            var normalized = Normalize(key);
            var stream = ResourceAssembly.GetManifestResourceStream(normalized);

            if (stream == null)
                throw new FileNotFoundException($"Embedded resource not found: {normalized}", normalized);

            return stream;
        }

        /// <summary>
        /// Read an embedded resource into a new MemoryStream.
        /// </summary>
        /// <param name="key">The resource key, for example "Resources/Images/space.png".</param>
        /// <returns>A memory stream containing the resource.</returns>
        public static MemoryStream Read(string key)
        {
            using var stream = Open(key);
            var memory = new MemoryStream();
            stream.CopyTo(memory);
            memory.Position = 0;
            return memory;
        }

        private static string Normalize(string key)
        {
            return key.Replace('\\', '/');
        }

        #endregion
    }
}
