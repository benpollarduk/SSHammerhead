using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Logic;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Ship.Items;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L1
{
    internal class BridgeTunnelEntry : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Bridge Tunnel (Entry)";
        private const string TrueDescription = "The entry to the bridge tunnel. The north exit is blocked by a laser barrier.";
        private const string FalseDescription = "The entry to the bridge tunnel. The north exit is no longer blocked by the laser barrier.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            Room room = null;

            RoomTransitionReaction exitTransition(RoomTransition roomTransition)
            {
                if (roomTransition.Direction != Direction.North)
                    return new RoomTransitionReaction(Reaction.Silent, true);

                if (!roomTransition.Room.FindItem(LaserBarrier.Name, out _, false))
                    return new RoomTransitionReaction(Reaction.Silent, true);

                GameExecutor.ExecutingGame?.Player.Kill();
                return new RoomTransitionReaction(new Reaction(ReactionResult.Inform, "You carelessly walk through the laser barrier. You feel a sudden pain searing through your entire body then collapse into hundreds of small chunks."), false);
            }

            var description = new ConditionalDescription(TrueDescription, FalseDescription, () => room.FindItem(LaserBarrier.Name, out _, false));
            room = new Room(new Identifier(Name), description, [new Exit(Direction.West, true), new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.North)], [new LaserBarrier().Instantiate()], exitCallback: exitTransition);
            return room;
        }

        #endregion
    }
}
