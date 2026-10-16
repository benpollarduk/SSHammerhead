using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Assets.Players.SpiderBot.FrameBuilders
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
            properties.LowerLevel = ':';
            properties.Player = '#';
            properties.Focus = '+';
            properties.CurrentFloorIndicator = '*';
            properties.FocusedBoundaryColor = SpiderBotTemplate.DisplayColor;
            properties.VisitedBoundaryColor = SpiderBotTemplate.DisplayColor;
            properties.UnvisitedBoundaryColor = SpiderBotTemplate.DisplayColor;
            properties.PlayerColor = SpiderBotTemplate.DisplayColor;
            properties.LockedExitColor = SpiderBotTemplate.DisplayColor;
            properties.LowerLevelColor = SpiderBotTemplate.DisplayColor;
            properties.ShowLowerFloors = true;
            return properties;
        }
    }
}
