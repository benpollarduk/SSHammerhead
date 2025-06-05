using NetAF.Commands;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.SpiderBot;

namespace SSHammerhead.Commands.Dev
{
    internal class DevB : IAssetTemplate<CustomCommand>
    {
        #region Implementation of IAssetTemplate<Item>

        public CustomCommand Instantiate()
        {
            return new CustomCommand(new CommandHelp("dev-b", "Switch Spider Bot"), false, true, (game, arguments) =>
            {
                return PlayableCharacterManager.Switch(SpiderBotTemplate.Identifier, game);
            });
        }

        #endregion
    }
}
