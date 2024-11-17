using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Logic;
using System;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Players.Management
{
    /// <summary>
    /// Provides functionality for managing playable characters.
    /// </summary>
    internal static class PlayableCharacterManager
    {
        #region StaticFields

        private static readonly List<PlayableCharacterRecord> records = [];

        #endregion

        #region StaticProperties

        private static PlayableCharacterRecord GetRecord(Identifier identifier) => records.Find(x => x.Instance.Identifier.Equals(identifier));

        #endregion

        #region StaticMethods

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
            var newPlayerRecord = GetRecord(player.Identifier);

            // get previous player location
            var previous = Array.Find(game.GetInactivePlayerLocations(), x => x.PlayerIdentifier.Equals(player.Identifier));

            // if a previous location found
            if (previous != default(PlayableCharacterLocation))
            {
                // switch player
                game.ChangePlayer(newPlayerRecord.Instance, false);
            }
            else
            {
                // switch player
                game.ChangePlayer(newPlayerRecord.Instance, false);

                // set start location
                var room = newPlayerRecord.StartRoom;
                var roomPosition = newPlayerRecord.StartRegion.GetPositionOfRoom(room);
                game.Overworld.Move(newPlayerRecord.StartRegion);
                var jumpResult = newPlayerRecord.StartRegion.JumpToRoom(roomPosition.Position);

                // check the jump worked
                if (!jumpResult)
                    return new Reaction(ReactionResult.Error, $"Could not switch to {newPlayerRecord.Instance.Identifier.Name}.");
            }

            // apply configuration
            ApplyConfiguration(newPlayerRecord.Instance, game);

            return new Reaction(ReactionResult.Inform, $"Switched to {newPlayerRecord.Instance.Identifier.Name}.");
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

        #endregion
    }
}
