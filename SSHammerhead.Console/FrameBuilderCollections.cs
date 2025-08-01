﻿using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using SSHammerhead.Assets.Players.Naomi.FrameBuilders;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders;

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
                    new NaomiConsoleTitleFrameBuilder(gridStringBuilder, new ConsoleImageProvider()),
                    new ConsoleSceneFrameBuilder(gridStringBuilder, new ConsoleRoomMapBuilder(gridStringBuilder)),
                    new ConsoleRegionMapFrameBuilder(gridStringBuilder, new ConsoleRegionMapBuilder(gridStringBuilder)),
                    new ConsoleCommandListFrameBuilder(gridStringBuilder),
                    new ConsoleHelpFrameBuilder(gridStringBuilder),
                    new ConsoleCompletionFrameBuilder(gridStringBuilder),
                    new ConsoleGameOverFrameBuilder(gridStringBuilder),
                    new ConsoleAboutFrameBuilder(gridStringBuilder),
                    new ConsoleReactionFrameBuilder(gridStringBuilder),
                    new ConsoleConversationFrameBuilder(gridStringBuilder),
                    new ConsoleNoteFrameBuilder(gridStringBuilder),
                    new ConsoleHistoryFrameBuilder(gridStringBuilder),
                    new NaomiConsoleLoginFrameBuilder(gridStringBuilder),
                    new NaomiConsoleScannerFrameBuilder(gridStringBuilder));
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
                    new ConsoleCommandListFrameBuilder(gridStringBuilder),
                    new ConsoleHelpFrameBuilder(gridStringBuilder),
                    new ConsoleCompletionFrameBuilder(gridStringBuilder),
                    new ConsoleGameOverFrameBuilder(gridStringBuilder),
                    new ConsoleAboutFrameBuilder(gridStringBuilder),
                    new BotConsoleReactionFrameBuilder(botStringBuilder),
                    new ConsoleConversationFrameBuilder(gridStringBuilder),
                    new ConsoleNoteFrameBuilder(gridStringBuilder),
                    new ConsoleHistoryFrameBuilder(gridStringBuilder));
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
                    new ConsoleSceneFrameBuilder(gridStringBuilder, new ConsoleRoomMapBuilder(gridStringBuilder)),
                    new ConsoleRegionMapFrameBuilder(gridStringBuilder, new ConsoleRegionMapBuilder(gridStringBuilder) { HorizontalBoundary = '.', VerticalBoundary = '~' }),
                    new ConsoleCommandListFrameBuilder(gridStringBuilder),
                    new ConsoleHelpFrameBuilder(gridStringBuilder),
                    new ConsoleCompletionFrameBuilder(gridStringBuilder),
                    new ConsoleGameOverFrameBuilder(gridStringBuilder),
                    new ConsoleAboutFrameBuilder(gridStringBuilder),
                    new ConsoleReactionFrameBuilder(gridStringBuilder),
                    new ConsoleConversationFrameBuilder(gridStringBuilder),
                    new ConsoleNoteFrameBuilder(gridStringBuilder),
                    new ConsoleHistoryFrameBuilder(gridStringBuilder));
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
