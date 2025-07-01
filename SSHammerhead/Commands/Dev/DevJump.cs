using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Scene;
using NetAF.Extensions;
using NetAF.Utilities;

namespace SSHammerhead.Commands.Dev
{
    internal class DevJump : IAssetTemplate<CustomCommand>
    {
        #region StaticMethods

        private static bool TryParseToDirection(string text, out Direction direction)
        {
            if (Move.NorthCommandHelp.Equals(text))
            {
                direction = Direction.North;
                return true;
            }

            if (Move.EastCommandHelp.Equals(text))
            {
                direction = Direction.East;
                return true;
            }

            if (Move.SouthCommandHelp.Equals(text))
            {
                direction = Direction.South;
                return true;
            }

            if (Move.WestCommandHelp.Equals(text))
            {
                direction = Direction.West;
                return true;
            }

            if (Move.UpCommandHelp.Equals(text))
            {
                direction = Direction.Up;
                return true;
            }

            if (Move.DownCommandHelp.Equals(text))
            {
                direction = Direction.Down;
                return true;
            }

            direction = Direction.East;
            return false;
        }

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public CustomCommand Instantiate()
        {
            return new CustomCommand(new CommandHelp("dev-jump", "Jump to a named room"), false, true, (game, arguments) =>
            {
                var roomName = string.Concat(arguments, ' ');

                foreach (var region in game.Overworld.Regions)
                {
                    foreach (var room in region.ToMatrix().ToRooms())
                    {
                        if (roomName.EqualsExaminable(room))
                        {
                            if (game.Overworld.CurrentRegion != region)
                                game.Overworld.Move(region);

                            region.JumpToRoom(region.GetPositionOfRoom(room).Position);

                            return new Reaction(ReactionResult.Inform, $"Jumped to {room.Identifier.Name}.");
                        }
                    }
                }

                return new Reaction(ReactionResult.Error, $"Failed to jump to {roomName}.");
            });
        }

        #endregion
    }
}
