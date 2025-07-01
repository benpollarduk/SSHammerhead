using NetAF.Commands;
using NetAF.Commands.Scene;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Commands.Helpers;

namespace SSHammerhead.Commands.Dev
{
    internal class DevGiveItem : IAssetTemplate<CustomCommand>
    {
        #region Implementation of IAssetTemplate<Item>

        public CustomCommand Instantiate()
        {
            return new CustomCommand(new CommandHelp("dev-give-item", "Give a named item"), false, true, (game, arguments) =>
            {
                var name = string.Concat(arguments, ' ');

                foreach (var template in ItemHelper.All)
                {
                    if (name.InsensitiveEquals(template.Name))
                    {
                        if (game.Player.FindItem(template.Name, out _))
                            return new Reaction(ReactionResult.Inform, $"Already got {template.Name}.");

                        game.Player.AddItem(template.Item.Instantiate());
                        return new Reaction(ReactionResult.Inform, $"Acquired {template.Name}.");
                    }
                }

                return new Reaction(ReactionResult.Error, "Unknown item.");
            });
        }

        #endregion
    }
}
