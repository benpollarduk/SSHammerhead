using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Logic;
using System.Collections.Generic;
using System.Data;

namespace NetAF.SSHammerhead.Assets.Players.Management
{
    /// <summary>
    /// Provides functionality for managing playable characters.
    /// </summary>
    internal static class PlayableCharacterManager
    {
        private static readonly List<PlayableCharacterRecord> records = [];

        private static PlayableCharacterRecord GetRecord(Identifier identifier) => records.Find(x => x.Instance.Identifier.Equals(identifier));

        /// <summary>
        /// Add a record.
        /// </summary>
        /// <param name="identifier">The identifier for the player.</param>
        /// <param name="record">The record.</param>
        public static void Add(PlayableCharacterRecord record)
        {
            records.Add(record);
        }

        /// <summary>
        /// Switch to a player.
        /// </summary>
        /// <param name="identifier">The identifier of the player to switch to.</param>
        /// <param name="game">The executing game.</param>
        /// <returns>The reaction.</returns>
        public static Reaction Switch(Identifier identifier, Game game)
        {
            return Switch(GetRecord(identifier).Instance, game);
        }

        /// <summary>
        /// Switch to a player.
        /// </summary>
        /// <param name="player">The  player to switch to.</param>
        /// <param name="game">The executing game.</param>
        /// <returns>The reaction.</returns>
        public static Reaction Switch(PlayableCharacter player, Game game)
        {
            // get records
            var currentPlayerRecord = GetRecord(game.Player.Identifier);
            var newPlayerRecord = GetRecord(player.Identifier);

            // store current player location
            currentPlayerRecord.Region = game.Overworld.CurrentRegion;
            currentPlayerRecord.Room = game.Overworld.CurrentRegion.CurrentRoom;

            // set location
            var room = newPlayerRecord.Room;
            var roomPosition = newPlayerRecord.Region.GetPositionOfRoom(room);
            game.Overworld.Move(newPlayerRecord.Region);
            var jumpResult = newPlayerRecord.Region.JumpToRoom(roomPosition.X, roomPosition.Y, roomPosition.Z);

            // check the jump worked
            if (!jumpResult)
                return new Reaction(ReactionResult.Error, $"Could not switch to {newPlayerRecord.Instance.Identifier.Name}.");

            // switch player
            game.ChangePlayer(newPlayerRecord.Instance);

            // apply configuration
            ApplyConfiguration(newPlayerRecord.Instance, game);

            return new Reaction(ReactionResult.OK, $"Switched to {newPlayerRecord.Instance.Identifier.Name}.");
        }

        /// <summary>
        /// Apply a configuration for a playable character.
        /// </summary>
        /// <param name="player">The player to apply configuration for.</param>
        /// <param name="game">The executing game.</param>
        public static void ApplyConfiguration(PlayableCharacter player, Game game)
        {
            // get record
            var record = GetRecord(player.Identifier);

            // set error prefix
            game.Configuration.ErrorPrefix = record.ErrorPrefix;

            // change appearance
            game.Configuration.FrameBuilders = record.FrameBuilderCollection;
        }
    }
}
