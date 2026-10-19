using NetAF.Targets.Console.Rendering;
using SSHammerhead.Assets.Players.SpiderBot;

namespace SSHammerhead.Rendering
{
    /// <summary>
    /// Provides shared render properties for the spider bot frame builders.
    /// </summary>
    public static class SpiderBotRenderProperties
    {
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
            properties.BorderColor = SpiderBotTemplate.DisplayColor;
            properties.MainTextColor = SpiderBotTemplate.DisplayColor;
            properties.InputColor = SpiderBotTemplate.DisplayColor;
            properties.OptionColor = SpiderBotTemplate.DisplayColor;
            properties.SecondaryTextColor = SpiderBotTemplate.DisplayColor;
            properties.OptionDescriptionColor = SpiderBotTemplate.DisplayColor;
            properties.TitleColor = SpiderBotTemplate.DisplayColor;
            properties.CommandColor = SpiderBotTemplate.DisplayColor;
            properties.CommandDescriptionColor = SpiderBotTemplate.DisplayColor;
            properties.HighlightTextColor = SpiderBotTemplate.DisplayColor;
            properties.HighlightKeywords = false;
            properties.Prompt = ">>>";
            properties.PromptColor = SpiderBotTemplate.DisplayColor;
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
            properties.BoundaryColor = SpiderBotTemplate.DisplayColor;
            properties.PointOfInterestColor = SpiderBotTemplate.DisplayColor;
            properties.FocusedBoundaryColor = SpiderBotTemplate.DisplayColor;
            properties.VisitedBoundaryColor = SpiderBotTemplate.DisplayColor;
            properties.UnvisitedBoundaryColor = SpiderBotTemplate.DisplayColor;
            properties.VisitedExitColor = SpiderBotTemplate.DisplayColor;
            properties.UnvisitedExitColor = SpiderBotTemplate.DisplayColor;
            properties.PlayerColor = SpiderBotTemplate.DisplayColor;
            properties.LockedExitColor = SpiderBotTemplate.DisplayColor;
            properties.LowerLevelColor = SpiderBotTemplate.DisplayColor;
            properties.ShowLowerFloors = true;
            return properties;
        }
    }
}
