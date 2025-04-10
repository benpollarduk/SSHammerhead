using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;
using SSHammerhead.Assets.Regions.Core.Rooms.L0;
using SSHammerhead.Assets.Regions.MaintenanceTunnels.Items;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels.L0
{
    internal class MaintenanceTunnelF : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Maintenance Tunnel F";
        private const string Description = "A small maintenance tunnel to allow the maintenance bots to traverse the ship.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            CustomCommand shunt = null;

            shunt = new CustomCommand(new CommandHelp("Shunt", $"Shunt the {PadlockKey.Name} towards the vent."), true, false, (game, arguments) =>
            {
                shunt.IsPlayerVisible = false;

                if (game.Player.FindItem(PadlockKey.Name, out var key))
                    game.Player.RemoveItem(key);
                else if (game.Overworld.CurrentRegion.CurrentRoom.FindItem(PadlockKey.Name, out key))
                    game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(key);

                if (key != null)
                {   
                    game.Overworld.FindRegion(Core.SSHammerHead.Name, out var ship);

                    if (ship.TryFindRoom(SupplyRoom.Name, out var supplyRoom))
                        supplyRoom.AddItem(key);

                    game.LogManager.Expire(PostIt.PostItLogName);
                }

                return new Reaction(ReactionResult.Inform, $"The spider bot lurches forward and shunts the {PadlockKey.Name} down the vent. It falls in to the {SupplyRoom.Name}.");
            });

            return new(Name, Description, [new Exit(Direction.East)], items: [new PadlockKey().Instantiate()], examination: request => new Examination(Description), commands: [shunt]);
        }

        #endregion
    }
}
