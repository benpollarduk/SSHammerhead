using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders;
using SSHammerhead.Assets.Players.Naomi.FrameBuilders;
using NetAF.Targets.Html.Rendering.FrameBuilders;
using NetAF.Targets.Html.Rendering;

namespace SSHammerhead.Blazor
{
    /// <summary>
    /// Contains the frame builder collections for the game.
    /// </summary>
    internal static class FrameBuilderCollections
    {
        /// <summary>
        /// Get the frame builders for Naomi, using console emulation.
        /// </summary>
        internal static FrameBuilderCollection NaomiConsoleEmulation
        {
            get
            {
                var gridStringBuilder = new GridStringBuilder();

                return new FrameBuilderCollection(
                    new NaomiConsoleTitleFrameBuilder(gridStringBuilder, ImageCache.GetProvider()),
                    new ConsoleSceneFrameBuilder(gridStringBuilder, new ConsoleRoomMapBuilder(gridStringBuilder), false),
                    new ConsoleRegionMapFrameBuilder(gridStringBuilder, new ConsoleRegionMapBuilder(gridStringBuilder), false),
                    new ConsoleCommandListFrameBuilder(gridStringBuilder),
                    new ConsoleHelpFrameBuilder(gridStringBuilder),
                    new ConsoleCompletionFrameBuilder(gridStringBuilder),
                    new ConsoleGameOverFrameBuilder(gridStringBuilder),
                    new ConsoleAboutFrameBuilder(gridStringBuilder),
                    new ConsoleReactionFrameBuilder(gridStringBuilder),
                    new ConsoleConversationFrameBuilder(gridStringBuilder, false),
                    new ConsoleNoteFrameBuilder(gridStringBuilder),
                    new ConsoleHistoryFrameBuilder(gridStringBuilder));
            }
        }

        /// <summary>
        /// Get the frame builders for the bot, using console emulation.
        /// </summary>
        internal static FrameBuilderCollection BotConsoleEmulation
        {
            get
            {
                var gridStringBuilder = new GridStringBuilder();
                var botStringBuilder = new GridStringBuilder('[', ']', '+');

                return new FrameBuilderCollection(
                    new ConsoleTitleFrameBuilder(gridStringBuilder),
                    new BotConsoleSceneFrameBuilder(botStringBuilder, new BotConsoleRoomMapBuilder(botStringBuilder), false),
                    new BotConsoleRegionMapFrameBuilder(botStringBuilder, new BotConsoleRegionMapBuilder(botStringBuilder), false),
                    new ConsoleCommandListFrameBuilder(gridStringBuilder),
                    new ConsoleHelpFrameBuilder(gridStringBuilder),
                    new ConsoleCompletionFrameBuilder(gridStringBuilder),
                    new ConsoleGameOverFrameBuilder(gridStringBuilder),
                    new ConsoleAboutFrameBuilder(gridStringBuilder),
                    new BotConsoleReactionFrameBuilder(botStringBuilder),
                    new ConsoleConversationFrameBuilder(gridStringBuilder, false),
                    new ConsoleNoteFrameBuilder(gridStringBuilder),
                    new ConsoleHistoryFrameBuilder(gridStringBuilder));
            }
        }

        /// <summary>
        /// Get the frame builders for Naomi, using HTML.
        /// </summary>
        internal static FrameBuilderCollection NaomiHtml
        {
            get
            {
                var htmlBuilder = new HtmlBuilder();

                return new FrameBuilderCollection(
                    new NaomiHtmlTitleFrameBuilder(htmlBuilder, ImageCache.GetProvider()),
                    new HtmlSceneFrameBuilder(htmlBuilder, new HtmlRoomMapBuilder(htmlBuilder) { MaxSize = new(40, 7) }),
                    new HtmlRegionMapFrameBuilder(htmlBuilder, new HtmlRegionMapBuilder(htmlBuilder) { MaxSize = new(40, 30) }),
                    new HtmlCommandListFrameBuilder(htmlBuilder),
                    new HtmlHelpFrameBuilder(htmlBuilder),
                    new HtmlCompletionFrameBuilder(htmlBuilder),
                    new HtmlGameOverFrameBuilder(htmlBuilder),
                    new HtmlAboutFrameBuilder(htmlBuilder),
                    new HtmlReactionFrameBuilder(htmlBuilder),
                    new HtmlConversationFrameBuilder(htmlBuilder),
                    new HtmlNoteFrameBuilder(htmlBuilder),
                    new HtmlHistoryFrameBuilder(htmlBuilder) { MaxEntries = 10 });
            }
        }

        /// <summary>
        /// Get the frame builders for the bot, using HTML.
        /// </summary>
        internal static FrameBuilderCollection BotHtml
        {
            get
            {
                var botStringBuilder = new GridStringBuilder('[', ']', '+');
                var htmlBuilder = new HtmlBuilder();

                return new FrameBuilderCollection(
                    new HtmlTitleFrameBuilder(htmlBuilder),
                    new BotConsoleSceneFrameBuilder(botStringBuilder, new BotConsoleRoomMapBuilder(botStringBuilder), false),
                    new BotConsoleRegionMapFrameBuilder(botStringBuilder, new BotConsoleRegionMapBuilder(botStringBuilder), false),
                    new HtmlCommandListFrameBuilder(htmlBuilder),
                    new HtmlHelpFrameBuilder(htmlBuilder),
                    new HtmlCompletionFrameBuilder(htmlBuilder),
                    new HtmlGameOverFrameBuilder(htmlBuilder),
                    new HtmlAboutFrameBuilder(htmlBuilder),
                    new BotConsoleReactionFrameBuilder(botStringBuilder),
                    new HtmlConversationFrameBuilder(htmlBuilder),
                    new HtmlNoteFrameBuilder(htmlBuilder),
                    new HtmlHistoryFrameBuilder(htmlBuilder) { MaxEntries = 10 });
            }
        }
    }
}
