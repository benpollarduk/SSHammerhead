using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace BP.AdventureFramework.SSHammerHead.TextManagement
{
    /// <summary>
    /// Provides a static text lookup.
    /// </summary>
    public class Lookup
    {
        #region StaticProperties

        /// <summary>
        /// Get the text look up.
        /// </summary>
        public static Lookup Text { get; } = new Lookup();

        #endregion

        #region Fields

        private static readonly Dictionary<string, string> entries = new Dictionary<string, string>();

        #endregion

        #region Properties

        /// <summary>
        /// Look up a value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        public string this[string key] => entries[key];

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Lookup class.
        /// </summary>
        private Lookup()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Buffer a resource in to memory.
        /// </summary>
        /// <param name="assembly">The assembly that the resource is embedded within.</param>
        /// <param name="resourceName">The fully qualified name of the resource.</param>
        public void BufferJsonResource(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    return;

                string json;

                using (var reader = new StreamReader(stream))
                    json = reader.ReadToEnd();

                var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                if (dictionary == null) 
                    return;

                foreach (var pair in dictionary)
                    entries.Add(pair.Key, pair.Value);
            }
        }

        #endregion
    }
}
