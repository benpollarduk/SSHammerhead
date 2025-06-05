using NetAF.Commands;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.MaintenanceTunnels.Items;
using SSHammerhead.Assets.Regions.Ship.Items;

namespace SSHammerhead.Commands.Dev
{
    internal class DevAllItems : IAssetTemplate<CustomCommand>
    {
        #region Implementation of IAssetTemplate<Item>

        public CustomCommand Instantiate()
        {
            return new CustomCommand(new CommandHelp("dev-allitems", "Attain all items"), false, true, (game, arguments) =>
            {
                var items = new[]
                {
                    new Hammer().Instantiate(),
                    new Laptop().Instantiate(),
                    new PadlockKey().Instantiate(),
                    new PostIt().Instantiate(),
                    new Assets.Regions.Ship.Items.Scanner().Instantiate(),
                    new USBDrive().Instantiate(),
                };

                foreach (var item in items) 
                {
                    if (game.Player.FindItem(item.Identifier.Name, out _))
                        continue;

                    game.Player.AddItem(item);
                }

                return new Reaction(ReactionResult.Inform, "Acquired all items.");
            });
        }

        #endregion
    }
}
