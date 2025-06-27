using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Commands.Dev
{
    internal class DevUnlockExits : IAssetTemplate<CustomCommand>
    {
        #region Implementation of IAssetTemplate<CustomCommand>

        public CustomCommand Instantiate()
        {
            return new CustomCommand(new CommandHelp("dev-unlock-all", "Unlock all exits"), false, true, (game, arguments) =>
            {
                var unlocked = 0;

                List<Exit> exits = new();

                foreach (var region in game.Overworld.Regions)
                {
                    foreach (var room in region.ToMatrix().ToRooms())
                    {
                        foreach (var exit in room.Exits)
                        {
                            if (exit.IsLocked && region.GetAdjoiningRoom(exit.Direction, room) != null)
                            {
                                exit.Unlock();
                                unlocked++;
                            }
                        }
                    }
                }

                return new Reaction(ReactionResult.Inform, $"Unlocked {unlocked} exits.");
            });
        }

        #endregion
    }
}
