using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Scene;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Commands.Helpers;

namespace SSHammerhead.Commands.Dev
{
    internal class DevGiveItem : IAssetTemplate<CustomCommand>
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
