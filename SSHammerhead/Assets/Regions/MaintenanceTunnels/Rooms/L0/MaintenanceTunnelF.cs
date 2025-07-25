﻿using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.MaintenanceTunnels.Items;
using SSHammerhead.Assets.Regions.Ship;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Assets.Regions.Ship.Rooms.L0;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels.Rooms.L0
{
    internal class MaintenanceTunnelF : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Maintenance Tunnel F";
        private const string Description = "A small maintenance tunnel to allow the maintenance bots to traverse the ship.";
        public const string ShuntCommandName = "Shunt";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            CustomCommand shunt = null;

            shunt = new CustomCommand(new CommandHelp(ShuntCommandName, $"Shunt the {PadlockKey.Name} towards the vent."), false, false, (game, arguments) =>
            {
                shunt.IsPlayerVisible = false;

                if (game.Player.FindItem(PadlockKey.Name, out var key))
                    game.Player.RemoveItem(key);
                else if (game.Overworld.CurrentRegion.CurrentRoom.FindItem(PadlockKey.Name, out key))
                    game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(key);

                if (key != null)
                {   
                    game.Overworld.FindRegion(SSHammerHead.Name, out var ship);

                    if (ship.TryFindRoom(SupplyRoom.Name, out var supplyRoom))
                        supplyRoom.AddItem(key);

                    game.NoteManager.Expire(PostIt.PostItLogName);
                }

                return new Reaction(ReactionResult.Inform, $"The spider bot lurches forward and shunts the {PadlockKey.Name} down the vent. It falls in to the {SupplyRoom.Name}.");
            });

            return new(Name, Description, [new Exit(Direction.East)], items: [new PadlockKey().Instantiate()], examination: request => new Examination(Description), commands: [shunt]);
        }

        #endregion
    }
}
