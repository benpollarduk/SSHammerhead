using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Markup.Rendering;
using NetAF.Targets.Markup.Rendering.FrameBuilders;
using SSHammerhead.Assets.Players.Naomi.FrameBuilders;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders;
using SSHammerhead.Console;

namespace SSHammerhead.WPF
{
    /// <summary>
    /// Contains the frame builder collections for the game.
    /// </summary>
    internal static class FrameBuilderCollections
    {
        /// <summary>
        /// Get the frame builders for Naomi, using markup.
        /// </summary>
        internal static FrameBuilderCollection NaomiMarkup
        {
            get
            {
                var gridStringBuilder = new GridStringBuilder();
                var markupBuilder = new MarkupBuilder();

                return new FrameBuilderCollection(
                    new NaomiMarkupTitleFrameBuilder(markupBuilder, new WpfImageProvider()),
                    new MarkupSceneFrameBuilder(markupBuilder, new MarkupRoomMapBuilder(markupBuilder)),
                    new MarkupRegionMapFrameBuilder(markupBuilder, new MarkupRegionMapBuilder(markupBuilder)),
                    new MarkupCommandListFrameBuilder(markupBuilder),
                    new MarkupHelpFrameBuilder(markupBuilder),
                    new MarkupCompletionFrameBuilder(markupBuilder),
                    new MarkupGameOverFrameBuilder(markupBuilder),
                    new MarkupAboutFrameBuilder(markupBuilder),
                    new MarkupReactionFrameBuilder(markupBuilder),
                    new MarkupConversationFrameBuilder(markupBuilder),
                    new MarkupNoteFrameBuilder(markupBuilder),
                    new MarkupHistoryFrameBuilder(markupBuilder) { MaxEntries = 10 },
                    new NaomiConsoleLoginFrameBuilder(gridStringBuilder),
                    new NaomiConsoleScannerFrameBuilder(gridStringBuilder),
                    new MarkupNarrativeFrameBuilder(markupBuilder),
                    new MarkupVisualFrameBuilder(markupBuilder));
            }
        }

        /// <summary>
        /// Get the frame builders for the bot, using markup.
        /// </summary>
        internal static FrameBuilderCollection BotMarkup
        {
            get
            {
                var botStringBuilder = new GridStringBuilder('[', ']', '+');
                var markupBuilder = new MarkupBuilder();

                return new FrameBuilderCollection(
                    new MarkupTitleFrameBuilder(markupBuilder),
                    new BotConsoleSceneFrameBuilder(botStringBuilder, new BotConsoleRoomMapBuilder(botStringBuilder), false),
                    new BotConsoleRegionMapFrameBuilder(botStringBuilder, new BotConsoleRegionMapBuilder(botStringBuilder), false),
                    new MarkupCommandListFrameBuilder(markupBuilder),
                    new MarkupHelpFrameBuilder(markupBuilder),
                    new MarkupCompletionFrameBuilder(markupBuilder),
                    new MarkupGameOverFrameBuilder(markupBuilder),
                    new MarkupAboutFrameBuilder(markupBuilder),
                    new BotConsoleReactionFrameBuilder(botStringBuilder),
                    new MarkupConversationFrameBuilder(markupBuilder),
                    new MarkupNoteFrameBuilder(markupBuilder),
                    new MarkupHistoryFrameBuilder(markupBuilder) { MaxEntries = 10 },
                    new MarkupNarrativeFrameBuilder(markupBuilder),
                    new BotVisualFrameBuilder());
            }
        }

        /// <summary>
        /// Get the frame builders for Anne, using markup.
        /// </summary>
        internal static FrameBuilderCollection AnneMarkup
        {
            get
            {
                var markupBuilder = new MarkupBuilder();

                return new FrameBuilderCollection(
                    new MarkupTitleFrameBuilder(markupBuilder),
                    new MarkupSceneFrameBuilder(markupBuilder, new MarkupRoomMapBuilder(markupBuilder)),
                    new MarkupRegionMapFrameBuilder(markupBuilder, new MarkupRegionMapBuilder(markupBuilder) { HorizontalBoundary = '.', VerticalBoundary = '~' }),
                    new MarkupCommandListFrameBuilder(markupBuilder),
                    new MarkupHelpFrameBuilder(markupBuilder),
                    new MarkupCompletionFrameBuilder(markupBuilder),
                    new MarkupGameOverFrameBuilder(markupBuilder),
                    new MarkupAboutFrameBuilder(markupBuilder),
                    new MarkupReactionFrameBuilder(markupBuilder),
                    new MarkupConversationFrameBuilder(markupBuilder),
                    new MarkupNoteFrameBuilder(markupBuilder),
                    new MarkupHistoryFrameBuilder(markupBuilder) { MaxEntries = 10 },
                    new MarkupNarrativeFrameBuilder(markupBuilder),
                    new MarkupVisualFrameBuilder(markupBuilder));
            }
        }

        /// <summary>
        /// Get the frame builders for Alex, using markup.
        /// </summary>
        internal static FrameBuilderCollection AlexMarkup => NaomiMarkup;

        /// <summary>
        /// Get the frame builders for Marina, using markup.
        /// </summary>
        internal static FrameBuilderCollection MarinaMarkup => NaomiMarkup;

        /// <summary>
        /// Get the frame builders for Scott, using markup.
        /// </summary>
        internal static FrameBuilderCollection ScottMarkup => NaomiMarkup;

        /// <summary>
        /// Get the frame builders for Zhiying, using markup.
        /// </summary>
        internal static FrameBuilderCollection ZhiyingMarkup => NaomiMarkup;
    }
}
