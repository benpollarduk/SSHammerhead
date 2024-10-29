using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Color;
using NetAF.SSHammerhead.Assets.Players.Management;
using NetAF.SSHammerhead.Assets.Players.SpiderBot.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.SSHammerhead.Assets.Players.SpiderBot
{
    public class SpiderBot : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static AnsiColor DisplayColor => AnsiColor.Green;
        public static Identifier Identifier => new(Name);
        public const string ErrorPrefix = "ERROR";
        private const string Name = "Bot";
        private const string Description = "A first generation spider bot, main purpose low scale maintenance operations.";

        public static FrameBuilderCollection FrameBuilderCollection
        {
            get
            {
                var gridLayoutBuilder = new GridStringBuilder();
                var botLayoutBuilder = new GridStringBuilder('[', ']', '+');

                return new FrameBuilderCollection(
                    new ColorTitleFrameBuilder(gridLayoutBuilder),
                    new BotColorSceneFrameBuilder(botLayoutBuilder, new BotColorRoomMapBuilder()),
                    new BotColorRegionMapFrameBuilder(botLayoutBuilder, new BotColorRegionMapBuilder()),
                    new ColorHelpFrameBuilder(gridLayoutBuilder),
                    new ColorCompletionFrameBuilder(gridLayoutBuilder),
                    new ColorGameOverFrameBuilder(gridLayoutBuilder),
                    new ColorAboutFrameBuilder(gridLayoutBuilder),
                    new ColorTransitionFrameBuilder(gridLayoutBuilder),
                    new ColorConversationFrameBuilder(gridLayoutBuilder));
            }
        }

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            return new PlayableCharacter(Name, Description, false)
            {
                Commands =
                [
                    new CustomCommand(new CommandHelp("Abort", $"Abort remote control"), true, (game, arguments) =>
                    {
                        return PlayableCharacterManager.Switch(Naomi.Naomi.Identifier, game);
                    })
                ]
            };
        }

        #endregion
    }
}
