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
            var jumpResult = Records[playerIdentifier.IdentifiableName].Region.JumpToRoom(roomPosition.X, roomPosition.Y, roomPosition.Z);

            // check the jump worked
            if (!jumpResult)
                return new Reaction(ReactionResult.Error, $"Could not switch to {playerIdentifier.Name}.");

            // switch player
            game.ChangePlayer(Records[playerIdentifier.IdentifiableName].Instance);

            // apply configuration
            ApplyConfiguration(playerIdentifier, game);

            return new Reaction(ReactionResult.OK, $"Switched to {playerIdentifier.Name}.");
        }

        /// <summary>
        /// Apply a configuration for a player.
        /// </summary>
        /// <param name="playerIdentifier">The identifier of the player to switch to.</param>
        /// <param name="game">The executing game.</param>
        public static void ApplyConfiguration(Identifier playerIdentifier, Game game)
        {
            // get record
            var record = Records[playerIdentifier.IdentifiableName];

            // set error prefix
            game.Configuration.ErrorPrefix = record.ErrorPrefix;

            // change appearance
            game.Configuration.FrameBuilders = record.FrameBuilderCollection;

        }
    }
}
