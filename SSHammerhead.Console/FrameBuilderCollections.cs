using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders.Modern;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders.Console;
using SSHammerhead.Targets.Console.FrameBuilders;
using ConsoleTitleFrameBuilder = SSHammerhead.Targets.Console.FrameBuilders.ConsoleTitleFrameBuilder;

namespace SSHammerhead.Console
{
    /// <summary>
    /// Contains the frame builder collections for the game.
    /// </summary>
    internal static class FrameBuilderCollections
    {
        /// <summary>
        /// Get the frame builders for Naomi.
        /// </summary>
        internal static FrameBuilderCollection Naomi
        {
            get
            {
                var gridStringBuilder = new GridStringBuilder();

                return new FrameBuilderCollection(
                    new ConsoleTitleFrameBuilder(gridStringBuilder),
                    new ConsoleModernSceneFrameBuilder(gridStringBuilder, new ConsoleHighDetailRoomMapBuilder(gridStringBuilder)),
                    new ConsoleModernRegionMapFrameBuilder(gridStringBuilder, new ConsoleRegionMapBuilder(gridStringBuilder)),
                    new ConsoleModernCommandListFrameBuilder(gridStringBuilder),
                    new ConsoleModernHelpFrameBuilder(gridStringBuilder),
                    new ConsoleModernCompletionFrameBuilder(gridStringBuilder),
                    new ConsoleModernGameOverFrameBuilder(gridStringBuilder),
                    new ConsoleModernAboutFrameBuilder(gridStringBuilder),
                    new ConsoleModernReactionFrameBuilder(gridStringBuilder),
                    new ConsoleModernConversationFrameBuilder(gridStringBuilder),
                    new ConsoleModernNoteFrameBuilder(gridStringBuilder),
                    new ConsoleModernHistoryFrameBuilder(gridStringBuilder),
                    new ConsoleLoginFrameBuilder(gridStringBuilder),
                    new ConsoleScannerFrameBuilder(gridStringBuilder),
                    new ConsoleModernNarrativeFrameBuilder(gridStringBuilder),
                    new ConsoleVisualFrameBuilder(gridStringBuilder),
                    new ConsoleRadioFrameBuilder(gridStringBuilder));
            }
        }

        /// <summary>
        /// Get the frame builders for the bot.
        /// </summary>
        internal static FrameBuilderCollection Bot
        {
            get
            {
                var gridStringBuilder = new GridStringBuilder();
                var botStringBuilder = new GridStringBuilder('[', ']', '+');

                return new FrameBuilderCollection(
                    new ConsoleTitleFrameBuilder(gridStringBuilder),
                    new BotConsoleSceneFrameBuilder(botStringBuilder, new BotConsoleRoomMapBuilder(botStringBuilder)),
                    new BotConsoleRegionMapFrameBuilder(botStringBuilder, new BotConsoleRegionMapBuilder(botStringBuilder)),
                    new ConsoleModernCommandListFrameBuilder(gridStringBuilder),
                    new ConsoleModernHelpFrameBuilder(gridStringBuilder),
                    new ConsoleModernCompletionFrameBuilder(gridStringBuilder),
                    new ConsoleModernGameOverFrameBuilder(gridStringBuilder),
                    new ConsoleModernAboutFrameBuilder(gridStringBuilder),
                    new BotConsoleReactionFrameBuilder(botStringBuilder),
                    new ConsoleModernConversationFrameBuilder(gridStringBuilder),
                    new ConsoleModernNoteFrameBuilder(gridStringBuilder),
                    new ConsoleModernHistoryFrameBuilder(gridStringBuilder),
                    new ConsoleModernNarrativeFrameBuilder(gridStringBuilder),
                    new BotVisualFrameBuilder(),
                    new ConsoleRadioFrameBuilder(gridStringBuilder));
            }
        }

        /// <summary>
        /// Get the frame builders for Anne.
        /// </summary>
        internal static FrameBuilderCollection Anne
        {
            get
            {
                var gridStringBuilder = new GridStringBuilder('¦', '¦', '¬');

                return new FrameBuilderCollection(
                    new ConsoleTitleFrameBuilder(gridStringBuilder),
                    new ConsoleModernSceneFrameBuilder(gridStringBuilder, new ConsoleHighDetailRoomMapBuilder(gridStringBuilder)),
                    new ConsoleModernRegionMapFrameBuilder(gridStringBuilder, new ConsoleRegionMapBuilder(gridStringBuilder) { HorizontalBoundary = '.', VerticalBoundary = '~' }),
                    new ConsoleModernCommandListFrameBuilder(gridStringBuilder),
                    new ConsoleModernHelpFrameBuilder(gridStringBuilder),
                    new ConsoleModernCompletionFrameBuilder(gridStringBuilder),
                    new ConsoleModernGameOverFrameBuilder(gridStringBuilder),
                    new ConsoleModernAboutFrameBuilder(gridStringBuilder),
                    new ConsoleModernReactionFrameBuilder(gridStringBuilder),
                    new ConsoleModernConversationFrameBuilder(gridStringBuilder),
                    new ConsoleModernNoteFrameBuilder(gridStringBuilder),
                    new ConsoleModernHistoryFrameBuilder(gridStringBuilder),
                    new ConsoleModernVisualFrameBuilder(gridStringBuilder),
                    new ConsoleRadioFrameBuilder(gridStringBuilder));
            }
        }

        /// <summary>
        /// Get the frame builders for Alex.
        /// </summary>
        internal static FrameBuilderCollection Alex => Naomi;

        /// <summary>
        /// Get the frame builders for Marina.
        /// </summary>
        internal static FrameBuilderCollection Marina => Naomi;

        /// <summary>
        /// Get the frame builders for Scott.
        /// </summary>
        internal static FrameBuilderCollection Scott => Naomi;

        /// <summary>
        /// Get the frame builders for Zhiying.
        /// </summary>
        internal static FrameBuilderCollection Zhiying => Naomi;
    }
}
