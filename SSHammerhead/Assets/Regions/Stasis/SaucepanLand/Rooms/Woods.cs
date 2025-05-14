using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Stasis.SaucepanLand.Rooms
{
    internal class Woods : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Woods";
        private const string Description = "Description needs to be added.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            RoomTransitionCallback backToTrunk = new(t =>
            {
                if (t.Region.TryFindRoom(Trunk.Name, out var trunk))
                {
                    var location = t.Region.GetPositionOfRoom(trunk);
                    return new(t.Region.JumpToRoom(location.Position), false);
                }

                return new RoomTransitionReaction(new Reaction(ReactionResult.Error, $"{Trunk.Name} not found in {t.Region.Identifier.Name}."), false);
            });

            return new Room(Name, Description, [new Exit(Direction.North), new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.West)], exitCallback: backToTrunk);
        }

        #endregion
    }
}
