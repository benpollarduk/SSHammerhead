using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Interpretation;
using NetAF.Logic;
using SSHammerhead.Commands;
using SSHammerhead.Logic.Modes;
using System.Collections.Generic;

namespace SSHammerhead.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting bot login commands.
    /// </summary>
    public sealed class BotLoginCommandInterpreter : IInterpreter
    {
        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        [
            End.CommandHelp,
        ];

        #endregion

        #region Implementation of IInterpreter

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public CommandHelp[] SupportedCommands { get; } = DefaultSupportedCommands;

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            if (End.CommandHelp.Equals(input))
                return new(true, new End());

            if (LoginUserName.CommandHelp.Equals(input))
                return new(true, new LoginUserName());

            if (LoginPassword.CommandHelp.Equals(input))
                return new(true, new LoginPassword());

            return InterpretationResult.Fail;
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> commands = [];

            if (game.Mode is BotLoginMode)
            {
                commands.Add(new CommandHelp(End.CommandHelp.Command, "Abort login", CommandCategory.RegionMap));
            }

            return [.. commands];
        }

        #endregion
    }
}
