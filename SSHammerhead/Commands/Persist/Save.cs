using NetAF.Commands;
using NetAF.Logic;
using NetAF.Persistence;
namespace SSHammerhead.Commands.Persist
{
    /// <summary>
    /// Represents the Save command.
    /// </summary>
    public class Save : CustomCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Save class.
        /// </summary>
        public Save() : base(NetAF.Commands.Persistence.Save.CommandHelp, true, true, SaveGameToFile) { }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Save the game to a file.
        /// </summary>
        /// <param name="game">The game to save.</param>
        /// <param name="args">The arguments. The file path must be the first element in the array.</param>
        /// <returns>The reaction.</returns>
        private static Reaction SaveGameToFile(Game game, string[] args)
        {
            var name = args?.Length > 0 ? string.Join(" ", args) : null;

            if (string.IsNullOrEmpty(name))
                return new(ReactionResult.Error, "No name provided.");

            if (!RestorePointManager.Save(game, name, out _, out string message))
                return new(ReactionResult.Error, $"Failed to save '{args}'. {message}");

            return new(ReactionResult.Inform, "Saved.");
        }

        #endregion
    }
}
