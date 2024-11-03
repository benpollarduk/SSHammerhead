using NetAF.Assets;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.SpiderBot;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    public class MaintenanceControlPanel : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Maintenance Control Panel";
        private const string Description = "A small control panel for the Spider Bot maintenance system. " +
            "It has a very basic black and green display but it is functional enough to allow the user to remotely control the Spider Bot system.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new(Name, Description, commands:
            [
                new CustomCommand(new CommandHelp("Start Maintenance", "Use the Spider Bot."), true, true, (game, arguments) =>
                {
                    return PlayableCharacterManager.Switch(SpiderBotTemplate.Identifier, game);
                })
            ]);
        }

        #endregion
    }
}
