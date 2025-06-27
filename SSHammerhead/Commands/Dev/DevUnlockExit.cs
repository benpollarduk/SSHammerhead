using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Scene;
using NetAF.Utilities;

namespace SSHammerhead.Commands.Dev
{
    internal class DevUnlockExit : IAssetTemplate<CustomCommand>
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
            return new CustomCommand(new CommandHelp("dev-unlock", "Unlock an exit in a direction"), false, true, (game, arguments) =>
            {
                if (arguments.Length != 1)
                    return new Reaction(ReactionResult.Error, "Only accepts 1 argument.");

                if (!TryParseToDirection(arguments[0], out var direction))
                    return new Reaction(ReactionResult.Error, $"{arguments[0]} is not a direction.");

                if (!game.Overworld.CurrentRegion.CurrentRoom.FindExit(direction, true, out var exit))
                    return new Reaction(ReactionResult.Error, $"No exit {direction}.");

                if (!exit.IsLocked)
                    return new Reaction(ReactionResult.Error, "Exit is not locked.");

                exit.Unlock();

                return new Reaction(ReactionResult.Inform, "Unlocked exit.");
            });
        }

        #endregion
    }
}
