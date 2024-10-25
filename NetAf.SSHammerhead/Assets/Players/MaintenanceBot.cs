using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.SSHammerhead.Assets.Players;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Players
{
    public class MaintenanceBot : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static Identifier Identifier => new Identifier(Name);
        private const string Name = "Bot";
        private const string Description = "You are a first generation maintenance bot.";

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            return new PlayableCharacter(Name, Description)
            {
                Commands =
                [
                    new CustomCommand(new CommandHelp("Naomi", "Switch to Naomi."), true, (game, arguments) =>
                    {
                        return PlayableCharacterManager.Switch(Naomi.Identifier, game);
                    })
                ]
            };
        }

        #endregion
    }
}
