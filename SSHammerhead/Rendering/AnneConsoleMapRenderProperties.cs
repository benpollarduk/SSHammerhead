using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Rendering
{
    /// <summary>
    /// Provides shared render properties for the Anne's map frame builders.
    /// </summary>
    public static class AnneConsoleMapRenderProperties
    {
        /// <summary>
        /// Create the console render properties used by the Anne's map frame builders.
        /// </summary>
        /// <returns>The Anne's map console render properties.</returns>
        public static ConsoleMapRenderProperties CreateRenderProperties()
        {
            var properties = (ConsoleMapRenderProperties)ConsoleMapRenderProperties.Default.Clone();
            properties.HorizontalBoundary = '.';
            properties.VerticalBoundary = '~';
            return properties;
        }

    }
}
