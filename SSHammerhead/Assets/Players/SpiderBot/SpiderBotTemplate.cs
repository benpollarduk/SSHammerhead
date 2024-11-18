using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Commands.Persistence;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Console;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.SpiderBot.FrameBuilders;

namespace SSHammerhead.Assets.Players.SpiderBot
{
    public class SpiderBotTemplate : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static AnsiColor DisplayColor => AnsiColor.Green;
        public static Identifier Identifier => new(Name);
        private const string Name = "Bot";
        private const string Description = "A first generation spider bot, main purpose low scale maintenance operations.";

        public static FrameBuilderCollection FrameBuilderCollection
        {
            get
            {
                var gridLayoutBuilder = new GridStringBuilder();
                var botLayoutBuilder = new GridStringBuilder('[', ']', '+');

                return new FrameBuilderCollection(
                    new ConsoleTitleFrameBuilder(gridLayoutBuilder),
                    new BotConsoleSceneFrameBuilder(botLayoutBuilder, new BotConsoleRoomMapBuilder(botLayoutBuilder)),
                    new BotConsoleRegionMapFrameBuilder(botLayoutBuilder, new BotConsoleRegionMapBuilder(botLayoutBuilder)),
                    new ConsoleHelpFrameBuilder(gridLayoutBuilder),
                    new ConsoleCompletionFrameBuilder(gridLayoutBuilder),
                    new ConsoleGameOverFrameBuilder(gridLayoutBuilder),
                    new ConsoleAboutFrameBuilder(gridLayoutBuilder),
                    new ConsoleReactionFrameBuilder(gridLayoutBuilder),
                    new ConsoleConversationFrameBuilder(gridLayoutBuilder));
            }
        }

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            return new PlayableCharacter(Name, Description, false, commands:
            [
                new CustomCommand(new CommandHelp("Abort", $"Abort remote control"), true, true, (game, arguments) =>
                {
                    return PlayableCharacterManager.Switch(Naomi.NaomiTemplate.Identifier, game);
                }),
                new Save() { IsPlayerVisible = false },
                new LoadWithRestore() { IsPlayerVisible = false }
            ]);
        }

        #endregion
    }
}
