using NetAF.Commands;
using NetAF.Logic;
using SSHammerhead.Logic.Modes;

namespace SSHammerhead.Commands.MaintenancePanel
{
    /// <summary>
    /// Represents the Login Invalid User Name command.
    /// </summary>
    internal sealed class LoginInvalidUserName : ICommand
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
                loginMode.Stage = LoginStage.InvalidUserName;

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
