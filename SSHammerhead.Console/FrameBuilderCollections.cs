using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders.Common;
using NetAF.Targets.Console.Rendering.FrameBuilders.Modern;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders.Console;
using SSHammerhead.Rendering;
using SSHammerhead.Targets.Console.FrameBuilders;
using ConsoleModernTitleFrameBuilder = SSHammerhead.Targets.Console.FrameBuilders.ConsoleModernTitleFrameBuilder;

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
                var consoleRenderProperties = ConsoleRenderProperties.Default;
                var botRenderProperties = SpiderBotRenderProperties.CreateRenderProperties();
                var mapRenderProperties = ConsoleMapRenderProperties.Default;
                var scannerRenderProperties = ScannerRenderProperties.CreateRenderProperties();
                var gridStringBuilder = new GridStringBuilder();

                return new FrameBuilderCollection(
                    new ConsoleModernTitleFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernSceneFrameBuilder(gridStringBuilder, consoleRenderProperties, new ConsoleHighDetailRoomMapBuilder(gridStringBuilder, mapRenderProperties)),
                    new ConsoleModernRegionMapFrameBuilder(gridStringBuilder, consoleRenderProperties, new ConsoleRegionMapBuilder(gridStringBuilder, mapRenderProperties)),
                    new ConsoleModernCommandListFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernHelpFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernCompletionFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernGameOverFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernAboutFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernReactionFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernConversationFrameBuilder(gridStringBuilder, consoleRenderProperties),
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
        /// Get the frame builders for the bot.
        /// </summary>
        internal static FrameBuilderCollection Bot
        {
            get
            {
                var consoleRenderProperties = ConsoleRenderProperties.Default;
                var gridStringBuilder = new GridStringBuilder();
                var botStringBuilder = new GridStringBuilder();
                var botRenderProperties = SpiderBotRenderProperties.CreateRenderProperties();
                var botMapProperties = SpiderBotRenderProperties.CreateMapRenderProperties();

                return new FrameBuilderCollection(
                    new ConsoleModernTitleFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new BotConsoleSceneFrameBuilder(botStringBuilder, botRenderProperties, new BotConsoleRoomMapBuilder(botStringBuilder, botMapProperties)),
                    new BotConsoleRegionMapFrameBuilder(botStringBuilder, botRenderProperties, new BotConsoleRegionMapBuilder(botStringBuilder, botMapProperties)),
                    new ConsoleModernCommandListFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernHelpFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernCompletionFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernGameOverFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernAboutFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new BotConsoleReactionFrameBuilder(botStringBuilder, botRenderProperties),
                    new ConsoleModernConversationFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernNoteFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernHistoryFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernNarrativeFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new BotVisualFrameBuilder(),
                    new ConsoleRadioFrameBuilder(gridStringBuilder, consoleRenderProperties));
            }
        }

        /// <summary>
        /// Get the frame builders for Anne.
        /// </summary>
        internal static FrameBuilderCollection Anne
        {
            get
            {
                var consoleRenderProperties = ConsoleRenderProperties.Default;
                var gridStringBuilder = new GridStringBuilder();
                var anneMapProperties = AnneConsoleMapRenderProperties.CreateRenderProperties();

                return new FrameBuilderCollection(
                    new ConsoleModernTitleFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernSceneFrameBuilder(gridStringBuilder, consoleRenderProperties, new ConsoleHighDetailRoomMapBuilder(gridStringBuilder, anneMapProperties)),
                    new ConsoleModernRegionMapFrameBuilder(gridStringBuilder, consoleRenderProperties, new ConsoleRegionMapBuilder(gridStringBuilder, anneMapProperties)),
                    new ConsoleModernCommandListFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernHelpFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernCompletionFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernGameOverFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernAboutFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernReactionFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernConversationFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernNoteFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernHistoryFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleModernVisualFrameBuilder(gridStringBuilder, consoleRenderProperties),
                    new ConsoleRadioFrameBuilder(gridStringBuilder, consoleRenderProperties));
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
