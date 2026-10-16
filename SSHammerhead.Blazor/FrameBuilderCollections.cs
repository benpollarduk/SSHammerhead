using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders.Common;
using NetAF.Targets.Console.Rendering.FrameBuilders.Modern;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders.Console;
using SSHammerhead.Blazor.FrameBuilders;
using SSHammerhead.Rendering;
using SSHammerhead.Targets.Console.FrameBuilders;
using ConsoleTitleFrameBuilder = SSHammerhead.Targets.Console.FrameBuilders.ConsoleTitleFrameBuilder;

namespace SSHammerhead.Blazor
{
    /// <summary>
    /// Contains the frame builder collections for the game.
    /// </summary>
    internal static class FrameBuilderCollections
    {
        #region ConsoleEmu

        /// <summary>
        /// Get the frame builders for Naomi, using console emulation.
        /// </summary>
        internal static FrameBuilderCollection NaomiConsoleEmulation
        {
            get
            {
                var consoleRenderProperties = ConsoleRenderProperties.Default;
                var mapRenderProperties = ConsoleMapRenderProperties.Default;
                var gridStringBuilder = new GridStringBuilder();

                var botRenderProperties = SpiderBotRenderProperties.CreateRenderProperties();
                var scannerRenderProperties = ScannerRenderProperties.CreateRenderProperties();

                return new FrameBuilderCollection(
                    new ConsoleTitleFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernSceneFrameBuilder(gridStringBuilder, consoleRenderProperties, new ConsoleHighDetailRoomMapBuilder(gridStringBuilder, mapRenderProperties), false),
                    new ConsoleModernRegionMapFrameBuilder(gridStringBuilder, consoleRenderProperties, new ConsoleRegionMapBuilder(gridStringBuilder, mapRenderProperties), false),
                    new ConsoleModernCommandListFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernHelpFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernCompletionFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernGameOverFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernAboutFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernReactionFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernConversationFrameBuilder(gridStringBuilder, consoleRenderProperties, false),
                    new ConsoleModernNoteFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernHistoryFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleLoginFrameBuilder(gridStringBuilder, botRenderProperties),
                    new ConsoleScannerFrameBuilder(gridStringBuilder, scannerRenderProperties),
                    new ConsoleModernNarrativeFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernVisualFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleRadioFrameBuilder(gridStringBuilder, consoleRenderProperties));
            }
        }

        /// <summary>
        /// Get the frame builders for the bot, using console emulation.
        /// </summary>
        internal static FrameBuilderCollection BotConsoleEmulation
        {
            get
            {
                var consoleRenderProperties = ConsoleRenderProperties.Default;
                var gridStringBuilder = new GridStringBuilder();
                var botStringBuilder = new GridStringBuilder();

                var botRenderProperties = SpiderBotRenderProperties.CreateRenderProperties();
                var botMapProperties = SpiderBotRenderProperties.CreateMapRenderProperties();

                return new FrameBuilderCollection(
                    new ConsoleTitleFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new BotConsoleSceneFrameBuilder(botStringBuilder, botRenderProperties, new BotConsoleRoomMapBuilder(botStringBuilder, botMapProperties), false),
                    new BotConsoleRegionMapFrameBuilder(botStringBuilder, botRenderProperties, new BotConsoleRegionMapBuilder(botStringBuilder, botMapProperties), false),
                    new ConsoleModernCommandListFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernHelpFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernCompletionFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernGameOverFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernAboutFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new BotConsoleReactionFrameBuilder(botStringBuilder, botRenderProperties),
                    new ConsoleModernConversationFrameBuilder(gridStringBuilder, consoleRenderProperties, false),
                    new ConsoleModernNoteFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernHistoryFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new BotVisualFrameBuilder(),
                    new ConsoleRadioFrameBuilder(gridStringBuilder, consoleRenderProperties));
            }
        }

        /// <summary>
        /// Get the frame builders for Anne, using console emulation.
        /// </summary>
        internal static FrameBuilderCollection AnneConsoleEmulation
        {
            get
            {
                var consoleRenderProperties = ConsoleRenderProperties.Default;
                var gridStringBuilder = new GridStringBuilder();

                var anneRegionMapProperties = (ConsoleMapRenderProperties)ConsoleMapRenderProperties.Default.Clone();
                anneRegionMapProperties.VerticalBoundary = '¦';
                anneRegionMapProperties.HorizontalBoundary = '¬';

                return new FrameBuilderCollection(
                    new ConsoleTitleFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernSceneFrameBuilder(gridStringBuilder, consoleRenderProperties, new ConsoleHighDetailRoomMapBuilder(gridStringBuilder, anneRegionMapProperties), false),
                    new ConsoleModernRegionMapFrameBuilder(gridStringBuilder, consoleRenderProperties, new ConsoleRegionMapBuilder(gridStringBuilder, anneRegionMapProperties), false),
                    new ConsoleModernCommandListFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernHelpFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernCompletionFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernGameOverFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernAboutFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernReactionFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernConversationFrameBuilder(gridStringBuilder, consoleRenderProperties, false),
                    new ConsoleModernNoteFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernHistoryFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernNarrativeFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernVisualFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleRadioFrameBuilder(gridStringBuilder, consoleRenderProperties));
            }
        }

        /// <summary>
        /// Get the frame builders for Alex, using console emulation.
        /// </summary>
        internal static FrameBuilderCollection AlexConsoleEmulation => NaomiConsoleEmulation;

        /// <summary>
        /// Get the frame builders for Marina, using console emulation.
        /// </summary>
        internal static FrameBuilderCollection MarinaConsoleEmulation => NaomiConsoleEmulation;

        /// <summary>
        /// Get the frame builders for Scott, using console emulation.
        /// </summary>
        internal static FrameBuilderCollection ScottConsoleEmulation => NaomiConsoleEmulation;

        /// <summary>
        /// Get the frame builders for Zhiying, using console emulation.
        /// </summary>
        internal static FrameBuilderCollection ZhiyingConsoleEmulation => NaomiConsoleEmulation;

        #endregion

        #region Html

        /// <summary>
        /// Get the frame builders for Naomi, using HTML.
        /// </summary>
        internal static FrameBuilderCollection NaomiHtml
        {
            get
            {
                var htmlRenderProperties = HtmlRenderProperties.Default;
                var mapRenderProperties = ConsoleMapRenderProperties.Default;
                var gridStringBuilder = new GridStringBuilder();
                var htmlBuilder = new HtmlBuilder();

                var botRenderProperties = SpiderBotRenderProperties.CreateRenderProperties();
                var scannerRenderProperties = ScannerRenderProperties.CreateRenderProperties();

                return new FrameBuilderCollection(
                    new NetAF.Targets.Html.Rendering.FrameBuilders.HtmlTitleFrameBuilder(htmlBuilder),
                    new HtmlSceneFrameBuilder(htmlBuilder, htmlRenderProperties, new HtmlRoomMapBuilder(htmlBuilder, mapRenderProperties)),
                    new HtmlRegionMapFrameBuilder(htmlBuilder, new HtmlRegionMapBuilder(htmlBuilder, mapRenderProperties)),
                    new HtmlCommandListFrameBuilder(htmlBuilder),
                    new HtmlHelpFrameBuilder(htmlBuilder),
                    new HtmlCompletionFrameBuilder(htmlBuilder),
                    new HtmlGameOverFrameBuilder(htmlBuilder),
                    new HtmlAboutFrameBuilder(htmlBuilder),
                    new HtmlReactionFrameBuilder(htmlBuilder, htmlRenderProperties),
                    new HtmlConversationFrameBuilder(htmlBuilder),
                    new HtmlNoteFrameBuilder(htmlBuilder),
                    new HtmlHistoryFrameBuilder(htmlBuilder) { MaxEntries = 10 },
                    new ConsoleLoginFrameBuilder(gridStringBuilder, botRenderProperties),
                    new ConsoleScannerFrameBuilder(gridStringBuilder, scannerRenderProperties),
                    new HtmlNarrativeFrameBuilder(htmlBuilder),
                    new HtmlVisualFrameBuilder(htmlBuilder),
                    new HtmlRadioFrameBuilder(htmlBuilder));
            }
        }

        /// <summary>
        /// Get the frame builders for the bot, using HTML.
        /// </summary>
        internal static FrameBuilderCollection BotHtml
        {
            get
            {
                var htmlRenderProperties = HtmlRenderProperties.Default;
                var botStringBuilder = new GridStringBuilder();
                var htmlBuilder = new HtmlBuilder();

                var botRenderProperties = SpiderBotRenderProperties.CreateRenderProperties();
                var botMapProperties = ConsoleMapRenderProperties.Default;

                return new FrameBuilderCollection(
                    new NetAF.Targets.Html.Rendering.FrameBuilders.HtmlTitleFrameBuilder(htmlBuilder),
                    new BotConsoleSceneFrameBuilder(botStringBuilder, botRenderProperties, new BotConsoleRoomMapBuilder(botStringBuilder, botMapProperties), false),
                    new BotConsoleRegionMapFrameBuilder(botStringBuilder, botRenderProperties, new BotConsoleRegionMapBuilder(botStringBuilder, botMapProperties), false),
                    new HtmlCommandListFrameBuilder(htmlBuilder),
                    new HtmlHelpFrameBuilder(htmlBuilder),
                    new HtmlCompletionFrameBuilder(htmlBuilder),
                    new HtmlGameOverFrameBuilder(htmlBuilder),
                    new HtmlAboutFrameBuilder(htmlBuilder),
                    new BotConsoleReactionFrameBuilder(botStringBuilder, botRenderProperties),
                    new HtmlConversationFrameBuilder(htmlBuilder),
                    new HtmlNoteFrameBuilder(htmlBuilder),
                    new HtmlHistoryFrameBuilder(htmlBuilder) { MaxEntries = 10 },
                    new HtmlNarrativeFrameBuilder(htmlBuilder),
                    new BotVisualFrameBuilder(),
                    new HtmlRadioFrameBuilder(htmlBuilder));
            }
        }

        /// <summary>
        /// Get the frame builders for Anne, using HTML.
        /// </summary>
        internal static FrameBuilderCollection AnneHtml
        {
            get
            {
                var htmlRenderProperties = HtmlRenderProperties.Default;
                var htmlBuilder = new HtmlBuilder();

                var anneRegionMapProperties = (ConsoleMapRenderProperties)ConsoleMapRenderProperties.Default.Clone();
                anneRegionMapProperties.HorizontalBoundary = '.';
                anneRegionMapProperties.VerticalBoundary = '~';

                return new FrameBuilderCollection(
                    new NetAF.Targets.Html.Rendering.FrameBuilders.HtmlTitleFrameBuilder(htmlBuilder),
                    new HtmlSceneFrameBuilder(htmlBuilder, htmlRenderProperties, new HtmlRoomMapBuilder(htmlBuilder, anneRegionMapProperties)),
                    new HtmlRegionMapFrameBuilder(htmlBuilder, new HtmlRegionMapBuilder(htmlBuilder, anneRegionMapProperties)),
                    new HtmlCommandListFrameBuilder(htmlBuilder),
                    new HtmlHelpFrameBuilder(htmlBuilder),
                    new HtmlCompletionFrameBuilder(htmlBuilder),
                    new HtmlGameOverFrameBuilder(htmlBuilder),
                    new HtmlAboutFrameBuilder(htmlBuilder),
                    new HtmlReactionFrameBuilder(htmlBuilder, htmlRenderProperties),
                    new HtmlConversationFrameBuilder(htmlBuilder),
                    new HtmlNoteFrameBuilder(htmlBuilder),
                    new HtmlHistoryFrameBuilder(htmlBuilder) { MaxEntries = 10 },
                    new HtmlNarrativeFrameBuilder(htmlBuilder),
                    new HtmlVisualFrameBuilder(htmlBuilder),
                    new HtmlRadioFrameBuilder(htmlBuilder));
            }
        }

        /// <summary>
        /// Get the frame builders for Alex, using HTML.
        /// </summary>
        internal static FrameBuilderCollection AlexHtml => NaomiHtml;

        /// <summary>
        /// Get the frame builders for Marina, using HTML.
        /// </summary>
        internal static FrameBuilderCollection MarinaHtml => NaomiHtml;

        /// <summary>
        /// Get the frame builders for Scott, using HTML.
        /// </summary>
        internal static FrameBuilderCollection ScottHtml => NaomiHtml;

        /// <summary>
        /// Get the frame builders for Zhiying, using HTML.
        /// </summary>
        internal static FrameBuilderCollection ZhiyingHtml => NaomiHtml;

        #endregion
    }
}
