using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.Logic;
using SSHammerhead.Assets.Regions.Core.Items;
using SSHammerhead.Commands.Scanner;
using SSHammerhead.Logic.Modes;
using System;
using System.Collections.Generic;

namespace SSHammerhead.Interpretation
{
    /// <summary>
    /// Provides an object that can interpret scanner commands.
    /// </summary>
    public sealed class ScannerCommandInterpreter : IInterpreter
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

            var match = Array.Find(Scanner.GetScannableExaminables(game), x => x.Identifier.Name.InsensitiveEquals(input));
            return new InterpretationResult(true, new Scan(match));
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> commands = [];

            if (game.Mode is ScannerMode)
            {
                commands.Add(new CommandHelp(End.CommandHelp.Command, "Exit scanner", CommandCategory.Information));

                foreach (var examinable in Scanner.GetScannableExaminables(game))
                    commands.Add(new CommandHelp(examinable.Identifier.Name, string.Empty, CommandCategory.Custom));
            }

            return [.. commands];
        }

        #endregion
    }
}
