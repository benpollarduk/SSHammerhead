using NetAF.Commands;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.Naomi;

namespace SSHammerhead.Commands.Dev
{
    internal class DevN : IAssetTemplate<CustomCommand>
    {
        #region Implementation of IAssetTemplate<Item>

        public CustomCommand Instantiate()
        {
            return new CustomCommand(new CommandHelp("dev-n", "Switch Naomi"), false, true, (game, arguments) =>
            {
                return PlayableCharacterManager.Switch(NaomiTemplate.Identifier, game);
            });
        }

        #endregion
    }
}
