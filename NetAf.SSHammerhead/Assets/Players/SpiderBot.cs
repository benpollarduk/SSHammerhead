using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Rendering.FrameBuilders.Color;
using NetAF.SSHammerhead.Assets.Players;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Players
{
    public class SpiderBot : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static AnsiColor DisplayColor => AnsiColor.Red;
        public static Identifier Identifier => new Identifier(Name);
        private const string Name = "Bot";
        private const string Description = "A first generation spider bot, main purpose low scale maintenance operations.";

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
                        return PlayableCharacterManager.Switch(Naomi.Identifier, game);
                    })
                ]
            };
        }

        #endregion
    }
}
