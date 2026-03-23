using NetAF.Commands;
using NetAF.Logic;
using NetAF.Persistence;
using SSHammerhead.Assets.Players.Management;

namespace SSHammerhead.Commands.Persist
{
    /// <summary>
    /// Represents the LoadWithRestore command.
    /// </summary>
    public class LoadWithRestore : CustomCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LoadWithRestore class.
        /// </summary>
        public LoadWithRestore() : base(NetAF.Commands.Persistence.Load.CommandHelp, true, true, LoadGameFromFile) { }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Load the game from a file.
        /// </summary>
        /// <param name="game">The game to load.</param>
        /// <param name="args">The arguments. The name must be the first element in the array.</param>
        /// <returns>The reaction.</returns>
        private static Reaction LoadGameFromFile(Game game, string[] args)
        {
            var name = args?.Length > 0 ? string.Join(" ", args) : null;

            if (string.IsNullOrEmpty(name))
                return new(ReactionResult.Error, "No name provided.");

            if (!RestorePointManager.Exists(game, name))
                return new(ReactionResult.Error, $"'{name}' does not exist.");

            if (!RestorePointManager.Apply(game, name, out string message))
                return new(ReactionResult.Error, $"Failed to load '{name}'. {message}");

            // setup for current player
            PlayableCharacterManager.ApplyConfiguration(game.Player, game);

            return new(ReactionResult.Inform, "Loaded.");
        }

        #endregion
    }
}
