using NetAF.Commands;
using NetAF.Logic;
using SSHammerhead.Logic.Modes;

namespace SSHammerhead.Commands.MaintenancePanel
{
    /// <summary>
    /// Represents the Login Reset To Password command.
    /// </summary>
    internal sealed class LoginResetToPassword : ICommand
    {
        #region Implementation of ICommand

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help => new CommandHelp(string.Empty, string.Empty);

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (game.Mode is BotLoginMode loginMode)
                loginMode.Stage = LoginStage.Password;

            return new(ReactionResult.Silent, string.Empty);
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}
