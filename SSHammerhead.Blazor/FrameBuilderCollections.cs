using NetAF.Rendering.Console.FrameBuilders;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders;

namespace SSHammerhead.Blazor
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
                var gridLayoutBuilder = new GridStringBuilder();

                return new FrameBuilderCollection(
                    new NaomiTitleFrameBuilder(gridLayoutBuilder, "wwwroot/"),
                    new ConsoleSceneFrameBuilder(gridLayoutBuilder, new ConsoleRoomMapBuilder(gridLayoutBuilder), false),
                    new ConsoleRegionMapFrameBuilder(gridLayoutBuilder, new ConsoleRegionMapBuilder(gridLayoutBuilder), false),
                    new ConsoleCommandListFrameBuilder(gridLayoutBuilder),
                    new ConsoleHelpFrameBuilder(gridLayoutBuilder),
                    new ConsoleCompletionFrameBuilder(gridLayoutBuilder),
                    new ConsoleGameOverFrameBuilder(gridLayoutBuilder),
                    new ConsoleAboutFrameBuilder(gridLayoutBuilder),
                    new ConsoleReactionFrameBuilder(gridLayoutBuilder),
                    new ConsoleConversationFrameBuilder(gridLayoutBuilder, false));
            }
        }

        /// <summary>
        /// Get the frame builders for the bot.
        /// </summary>
        internal static FrameBuilderCollection Bot
        {
            get
            {
                var gridLayoutBuilder = new GridStringBuilder();
                var botLayoutBuilder = new GridStringBuilder('[', ']', '+');

                return new FrameBuilderCollection(
                    new ConsoleTitleFrameBuilder(gridLayoutBuilder),
                    new BotConsoleSceneFrameBuilder(botLayoutBuilder, new BotConsoleRoomMapBuilder(botLayoutBuilder), false),
                    new BotConsoleRegionMapFrameBuilder(botLayoutBuilder, new BotConsoleRegionMapBuilder(botLayoutBuilder), false),
                    new ConsoleCommandListFrameBuilder(gridLayoutBuilder),
                    new ConsoleHelpFrameBuilder(gridLayoutBuilder),
                    new ConsoleCompletionFrameBuilder(gridLayoutBuilder),
                    new ConsoleGameOverFrameBuilder(gridLayoutBuilder),
                    new ConsoleAboutFrameBuilder(gridLayoutBuilder),
                    new BotConsoleReactionFrameBuilder(botLayoutBuilder),
                    new ConsoleConversationFrameBuilder(gridLayoutBuilder, false));
            }
        }
    }
}
