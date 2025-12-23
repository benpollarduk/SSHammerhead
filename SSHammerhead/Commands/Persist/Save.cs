using NetAF.Commands;
using NetAF.Logic;
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
        public Save() : base(new CommandHelp("Save", "Save the game state to a file. The path should be specified as an absolute path"), true, true, SaveGameToFile) { }

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
            return new NetAF.Commands.Persistence.Save(args.Length != 0 ? args[0] : string.Empty).Invoke(game);
        }

        #endregion
    }
}
