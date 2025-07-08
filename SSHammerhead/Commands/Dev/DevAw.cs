using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.Naomi;
using SSHammerhead.Assets.Regions.Stasis.Awaji;

namespace SSHammerhead.Commands.Dev
{
    internal class DevAw(Region instance) : IAssetTemplate<CustomCommand>
    {
        #region Implementation of IAssetTemplate<CustomCommand>

        public CustomCommand Instantiate()
        {
            return new CustomCommand(new CommandHelp("dev-aw", "Jump to Awaji"), false, true, (game, arguments) =>
            {
                var reaction = PlayableCharacterManager.Switch(NaomiTemplate.Identifier, game);

                if (reaction.Result == ReactionResult.Error)
                    return reaction;

                game.Overworld.Move(instance);
                return new Reaction(ReactionResult.Silent, $"Jumped to {Awaji.Name}.");
            });
        }

        #endregion
    }
}
