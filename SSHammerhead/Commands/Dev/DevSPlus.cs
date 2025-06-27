using NetAF.Commands;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Naomi;

namespace SSHammerhead.Commands.Dev
{
    internal class DevSPlus : IAssetTemplate<CustomCommand>
    {
        #region Implementation of IAssetTemplate<CustomCommand>

        public CustomCommand Instantiate()
        {
            return new CustomCommand(new CommandHelp("dev-s+", "Increase sanity by 1"), false, true, (game, arguments) =>
            {
                game.Player.Attributes.Add(NaomiTemplate.SanityAttributeName, 1);
                return new Reaction(ReactionResult.Inform, $"Sanity is now {game.Player.Attributes.GetValue(NaomiTemplate.SanityAttributeName)}");
            });
        }

        #endregion
    }
}
