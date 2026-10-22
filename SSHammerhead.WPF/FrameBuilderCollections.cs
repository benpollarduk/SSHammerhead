using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Markup.Rendering;
using NetAF.Targets.Markup.Rendering.FrameBuilders;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders.Console;
using SSHammerhead.Rendering;
using SSHammerhead.Targets.Console.FrameBuilders;
using SSHammerhead.WPF.FrameBuilders;
using System.Windows;
using MarkupTitleFrameBuilder = SSHammerhead.WPF.FrameBuilders.MarkupTitleFrameBuilder;

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
                var renderProperties = MarkupRenderProperties.Default;
                var mapRenderProperties = ConsoleMapRenderProperties.Default;
                var botRenderProperties = SpiderBotRenderProperties.CreateRenderProperties();
                var scannerRenderProperties = ScannerRenderProperties.CreateRenderProperties();
                var gridStringBuilder = new GridStringBuilder();
                var markupBuilder = new MarkupBuilder();

                return new FrameBuilderCollection(
                    new MarkupTitleFrameBuilder(markupBuilder),
                    new MarkupSceneFrameBuilder(markupBuilder, renderProperties, new MarkupRoomMapBuilder(markupBuilder, mapRenderProperties)),
                    new MarkupRegionMapFrameBuilder(markupBuilder, new MarkupRegionMapBuilder(markupBuilder, mapRenderProperties)),
                    new MarkupCommandListFrameBuilder(markupBuilder),
                    new MarkupHelpFrameBuilder(markupBuilder),
                    new MarkupCompletionFrameBuilder(markupBuilder),
                    new MarkupGameOverFrameBuilder(markupBuilder),
                    new MarkupAboutFrameBuilder(markupBuilder),
                    new MarkupReactionFrameBuilder(markupBuilder, renderProperties),
                    new MarkupConversationFrameBuilder(markupBuilder, renderProperties),
                    new MarkupNoteFrameBuilder(markupBuilder),
                    new MarkupHistoryFrameBuilder(markupBuilder) { MaxEntries = 10 },
                    new ConsoleLoginFrameBuilder(gridStringBuilder, botRenderProperties),
                    new ConsoleScannerFrameBuilder(gridStringBuilder, scannerRenderProperties),
                    new MarkupNarrativeFrameBuilder(markupBuilder),
                    new MarkupVisualFrameBuilder(markupBuilder),
                    new MarkupRadioFrameBuilder(markupBuilder, Application.Current.Dispatcher));
            }
        }

        /// <summary>
        /// Get the frame builders for the bot, using markup.
        /// </summary>
        internal static FrameBuilderCollection BotMarkup
        {
            get
            {
                var botStringBuilder = new GridStringBuilder();
                var markupBuilder = new MarkupBuilder();
                var botRenderProperties = SpiderBotRenderProperties.CreateRenderProperties();
                var botMapProperties = SpiderBotRenderProperties.CreateMapRenderProperties();
                var markupRenderProperties = MarkupRenderProperties.Default;

                return new FrameBuilderCollection(
                    new MarkupTitleFrameBuilder(markupBuilder),
                    new BotConsoleSceneFrameBuilder(botStringBuilder, botRenderProperties, new BotConsoleRoomMapBuilder(botStringBuilder, botMapProperties), false),
                    new BotConsoleRegionMapFrameBuilder(botStringBuilder, botRenderProperties, new BotConsoleRegionMapBuilder(botStringBuilder, botMapProperties), false),
                    new MarkupCommandListFrameBuilder(markupBuilder),
                    new MarkupHelpFrameBuilder(markupBuilder),
                    new MarkupCompletionFrameBuilder(markupBuilder),
                    new MarkupGameOverFrameBuilder(markupBuilder),
                    new MarkupAboutFrameBuilder(markupBuilder),
                    new BotConsoleReactionFrameBuilder(botStringBuilder, botRenderProperties),
                    new MarkupConversationFrameBuilder(markupBuilder, markupRenderProperties),
                    new MarkupNoteFrameBuilder(markupBuilder),
                    new MarkupHistoryFrameBuilder(markupBuilder) { MaxEntries = 10 },
                    new MarkupNarrativeFrameBuilder(markupBuilder),
                    new BotVisualFrameBuilder(),
                    new MarkupRadioFrameBuilder(markupBuilder, Application.Current.Dispatcher));
            }
        }

        /// <summary>
        /// Get the frame builders for Anne, using markup.
        /// </summary>
        internal static FrameBuilderCollection AnneMarkup
        {
            get
            {
                var renderProperties = MarkupRenderProperties.Default;
                var markupBuilder = new MarkupBuilder();
                var anneMapProperties = AnneConsoleMapRenderProperties.CreateRenderProperties();

                return new FrameBuilderCollection(
                    new NetAF.Targets.Markup.Rendering.FrameBuilders.MarkupTitleFrameBuilder(markupBuilder),
                    new MarkupSceneFrameBuilder(markupBuilder, renderProperties, new MarkupRoomMapBuilder(markupBuilder, ConsoleMapRenderProperties.Default)),
                    new MarkupRegionMapFrameBuilder(markupBuilder, new MarkupRegionMapBuilder(markupBuilder, anneMapProperties)),
                    new MarkupCommandListFrameBuilder(markupBuilder),
                    new MarkupHelpFrameBuilder(markupBuilder),
                    new MarkupCompletionFrameBuilder(markupBuilder),
                    new MarkupGameOverFrameBuilder(markupBuilder),
                    new MarkupAboutFrameBuilder(markupBuilder),
                    new MarkupReactionFrameBuilder(markupBuilder, renderProperties),
                    new MarkupConversationFrameBuilder(markupBuilder, renderProperties),
                    new MarkupNoteFrameBuilder(markupBuilder),
                    new MarkupHistoryFrameBuilder(markupBuilder) { MaxEntries = 10 },
                    new MarkupNarrativeFrameBuilder(markupBuilder),
                    new MarkupVisualFrameBuilder(markupBuilder),
                    new MarkupRadioFrameBuilder(markupBuilder, Application.Current.Dispatcher));
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
