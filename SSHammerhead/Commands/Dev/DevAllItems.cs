using NetAF.Commands;
using NetAF.Utilities;
using SSHammerhead.Commands.Helpers;

namespace SSHammerhead.Commands.Dev
{
    internal class DevAllItems : IAssetTemplate<CustomCommand>
    {
        #region Implementation of IAssetTemplate<Item>

        public CustomCommand Instantiate()
        {
            return new CustomCommand(new CommandHelp("dev-allitems", "Attain all items"), false, true, (game, arguments) =>
            {
                foreach (var template in ItemHelper.All) 
                {
                    if (game.Player.FindItem(template.Name, out _))
                        continue;

                    game.Player.AddItem(template.Item.Instantiate());
                }

                return new Reaction(ReactionResult.Inform, "Acquired all items.");
            });
        }

        #endregion
    }
}
