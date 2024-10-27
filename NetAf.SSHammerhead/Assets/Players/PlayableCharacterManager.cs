using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.SSHammerHead.Assets.Players;
using System.Collections.Generic;

namespace NetAF.SSHammerhead.Assets.Players
{
    /// <summary>
    /// Provides functionality for managing playable characters.
    /// </summary>
    internal static class PlayableCharacterManager
    {
        private static Dictionary<string, PlayableCharacterRecord> Records = new();

        public static void Setup(Dictionary<string, PlayableCharacterRecord> e)
        {
            Records = e;
        }

        /// <summary>
        /// Switch to a player.
        /// </summary>
        /// <param name="playerIdentifier">The identifier of the player to switch to.</param>
        /// <param name="game">The executing game.</param>
        /// <returns>The reaction.</returns>
        public static Reaction Switch(Identifier playerIdentifier, Game game)
        {
            // store current player location
            Records[game.Player.Identifier.IdentifiableName].Region = game.Overworld.CurrentRegion;
            Records[game.Player.Identifier.IdentifiableName].Room = game.Overworld.CurrentRegion.CurrentRoom;

            // set location
            var room = Records[playerIdentifier.IdentifiableName].Room;
            var roomPosition = Records[playerIdentifier.IdentifiableName].Region.GetPositionOfRoom(room);
            game.Overworld.Move(Records[playerIdentifier.IdentifiableName].Region);
            var jumpResult = game.Overworld.CurrentRegion.JumpToRoom(roomPosition.X, roomPosition.Y, roomPosition.Z);

            // check the jump worked
            if (!jumpResult)
                return new Reaction(ReactionResult.Error, $"Could not switch to {playerIdentifier.Name}.");

            // switch player
            game.ChangePlayer(Records[playerIdentifier.IdentifiableName].Instance);

            // change appearance
            game.ChangeFrameBuilders(Records[playerIdentifier.IdentifiableName].FrameBuilderCollection, false);

            return new Reaction(ReactionResult.OK, $"Switched to {playerIdentifier.Name}.");
        }
    }
}
