using NetAF.Commands;
using NetAF.Logic;
using SSHammerhead.Logic.Modes;

namespace SSHammerhead.Commands
{
    /// <summary>
    /// Represents the Login Username command.
    /// </summary>
    internal sealed class LoginUserName : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Scott", string.Empty, CommandCategory.Custom);

        #endregion

        #region Implementation of ICommand

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
            {
                switch (loginMode.Stage)
                {
                    case LoginStage.UserName:
                        loginMode.Stage = LoginStage.Password;
                        break;
                }
            }

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
