using NetAF.Commands;
using NetAF.Logic;
using SSHammerhead.Logic.Modes;

namespace SSHammerhead.Commands.MaintenancePanel
{
    /// <summary>
    /// Represents the Login Invalid Password command.
    /// </summary>
    internal sealed class LoginInvalidPassword : ICommand
    {
        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => new CommandHelp(string.Empty, string.Empty);

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (game.Mode is BotLoginMode loginMode)
                loginMode.Stage = LoginStage.InvalidPassword;

            return new(ReactionResult.Silent, string.Empty);
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}
