using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.Naomi;
using SSHammerhead.Assets.Regions.Stasis.SaucepanLand;

namespace SSHammerhead.Commands.Dev
{
    internal class DevSP(Region instance) : IAssetTemplate<CustomCommand>
    {
        #region Implementation of IAssetTemplate<Item>

        public CustomCommand Instantiate()
        {
            return new CustomCommand(new CommandHelp("dev-sp", "Jump to Saucepan Land"), false, true, (game, arguments) =>
            {
                var reaction = PlayableCharacterManager.Switch(NaomiTemplate.Identifier, game);

                if (reaction.Result == ReactionResult.Error)
                    return reaction;

                game.Overworld.Move(instance);
                return new Reaction(ReactionResult.Silent, $"Jumped to {SaucepanLand.Name}.");
            });
        }

        #endregion
    }
}
