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
        private static Dictionary<string, PlayableCharacterRecord> Records = new()
        {
            { Naomi.Identifier.IdentifiableName, new PlayableCharacterRecord(null, new RoomPosition(null, -1, -3, -2), FrameBuilders.FrameBuilderCollections.Naomi) },
            { MaintenanceBot.Identifier.IdentifiableName, new PlayableCharacterRecord(new MaintenanceBot().Instantiate(), new RoomPosition(null, 2, 1, 0), FrameBuilders.FrameBuilderCollections.Bot) },
        };

        /// <summary>
        /// Switch to a player.
        /// </summary>
        /// <param name="playerIdentifier">The identifier of the player to switch to.</param>
        /// <param name="game">The executing game.</param>
        /// <returns>The reaction.</returns>
        public static Reaction Switch(Identifier playerIdentifier, Game game)
        {
            // store current player location
            Records[game.Player.Identifier.IdentifiableName].Instance = game.Player;
            Records[game.Player.Identifier.IdentifiableName].RoomPosition = game.Overworld.CurrentRegion.GetPositionOfRoom(game.Overworld.CurrentRegion.CurrentRoom);

            // set location
            var roomPosition = Records[playerIdentifier.IdentifiableName].RoomPosition;
            var jumpResult = game.Overworld.CurrentRegion.JumpToRoom(roomPosition.X, roomPosition.Y, roomPosition.Z);

            // check the jump worked
            if (!jumpResult)
                return new Reaction(ReactionResult.Error, $"Could not switch to {playerIdentifier.Name}.");

            // switch player
            game.ChangePlayer(Records[playerIdentifier.IdentifiableName].Instance);

            // change appearance
            game.FrameBuilders = Records[playerIdentifier.IdentifiableName].FrameBuilderCollection;

            return new Reaction(ReactionResult.OK, $"Switched to {playerIdentifier.Name}.");
        }
    }
}
