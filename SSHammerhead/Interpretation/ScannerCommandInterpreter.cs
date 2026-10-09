using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.Logic;
using SSHammerhead.Assets.Regions.Ship.Items;
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

        /// <inheritdoc/>
        public CommandHelp[] SupportedCommands { get; } = DefaultSupportedCommands;

        /// <inheritdoc/>
        public List<CommandHelp> ExcludedCommands { get; } = [];

        /// <inheritdoc/>
        public InterpretationResult Interpret(string input, Game game)
        {
            if (End.CommandHelp.Equals(input))
                return new(true, new End());

            var match = Array.Find(Scanner.GetScannableExaminables(game), x => x.Identifier.Name.InsensitiveEquals(input));
            return new InterpretationResult(true, new Scan(match));
        }

        /// <inheritdoc/>
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
