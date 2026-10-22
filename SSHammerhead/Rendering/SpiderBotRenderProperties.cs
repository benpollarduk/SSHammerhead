using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Rendering
{
    /// <summary>
    /// Provides shared render properties for the spider bot frame builders.
    /// </summary>
    public static class SpiderBotRenderProperties
    {
        private static AnsiColor DisplayColor = new AnsiColor(100, 255, 100);

        /// <summary>
        /// Create the console render properties used by the spider bot frame builders.
        /// </summary>
        /// <returns>The spider bot console render properties.</returns>
        public static ConsoleRenderProperties CreateRenderProperties()
        {
            var properties = ConsoleRenderProperties.Default.Clone() as ConsoleRenderProperties;
            properties.LeftBoundary = '[';
            properties.RightBoundary = ']';
            properties.HorizontalDivider = '+';
            properties.BorderColor = DisplayColor;
            properties.MainTextColor = DisplayColor;
            properties.InputColor = DisplayColor;
            properties.OptionColor = DisplayColor;
            properties.SecondaryTextColor = DisplayColor;
            properties.OptionDescriptionColor = DisplayColor;
            properties.TitleColor = DisplayColor;
            properties.CommandColor = DisplayColor;
            properties.CommandDescriptionColor = DisplayColor;
            properties.HighlightTextColor = DisplayColor;
            properties.HighlightKeywords = false;
            properties.Prompt = ">>>";
            properties.PromptColor = DisplayColor;
            properties.ShowPrompt = true;
            return properties;
        }

        /// <summary>
        /// Create the console map render properties used by the spider bot frame builders.
        /// </summary>
        /// <returns>The spider bot console map render properties.</returns>
        public static ConsoleMapRenderProperties CreateMapRenderProperties()
        {
            var properties = ConsoleMapRenderProperties.Default.Clone() as ConsoleMapRenderProperties;
            properties.LockedExit = 'x';
            properties.EmptySpace = ' ';
            properties.VerticalBoundary = '.';
            properties.HorizontalBoundary = '.';
            properties.VerticalExitBorder = '.';
            properties.HorizontalExitBorder = '.';
            properties.LowerLevel = ':';
            properties.Player = '#';
            properties.Focus = '+';
            properties.CurrentFloorIndicator = '*';
            properties.BoundaryColor = DisplayColor;
            properties.PointOfInterestColor = DisplayColor;
            properties.FocusedBoundaryColor = DisplayColor;
            properties.VisitedBoundaryColor = DisplayColor;
            properties.UnvisitedBoundaryColor = DisplayColor;
            properties.VisitedExitColor = DisplayColor;
            properties.UnvisitedExitColor = DisplayColor;
            properties.PlayerColor = DisplayColor;
            properties.LockedExitColor = DisplayColor;
            properties.LowerLevelColor = DisplayColor;
            properties.ShowLowerFloors = true;
            return properties;
        }
    }
}
