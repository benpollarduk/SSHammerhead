using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Rendering
{
    /// <summary>
    /// Provides shared render properties for the scanner frame builders.
    /// </summary>
    public static class ScannerRenderProperties
    {
        /// <summary>
        /// Create the console render properties used by the scanner frame builders.
        /// </summary>
        /// <returns>The scanner console render properties.</returns>
        public static ConsoleRenderProperties CreateRenderProperties()
        {
            var scannerColor = new AnsiColor(200, 255, 200);
            var properties = ConsoleRenderProperties.Default.Clone() as ConsoleRenderProperties;
            properties.LeftBoundary = '*';
            properties.RightBoundary = '*';
            properties.HorizontalDivider = '*';
            properties.BorderColor = scannerColor;
            properties.MainTextColor = scannerColor;
            properties.InputColor = scannerColor;
            properties.OptionColor = scannerColor;
            properties.SecondaryTextColor = scannerColor;
            properties.OptionDescriptionColor = scannerColor;
            properties.BackgroundColor = AnsiColor.Black;
            properties.Prompt = string.Empty;
            properties.PromptColor = scannerColor;
            properties.ShowPrompt = false;
            return properties;
        }

    }
}
