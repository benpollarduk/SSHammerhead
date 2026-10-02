using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Interpretation;
using NetAF.Logic;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Commands.MaintenancePanel;
using SSHammerhead.Logic.Modes;
using System.Collections.Generic;
using System.Linq;

namespace SSHammerhead.Interpretation
{
    /// <summary>
    /// Provides an object that can interpret radio commands.
    /// </summary>
    public sealed class RadioCommandInterpreter : IInterpreter
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
        public InterpretationResult Interpret(string input, Game game)
        {
            StringUtilities.SplitInputToCommandAndArguments(input, out var command, out var arguments);

            if (End.CommandHelp.Equals(command))
                return new(true, new End());

            if (Off.CommandHelp.Equals(command))
                return new(true, new Off());

            if (On.CommandHelp.Equals(command))
                return new(true, new On());

            if (Change.CommandHelp.Equals(command))
                return new(true, new Change(string.Join(" ", arguments)));

            return InterpretationResult.Fail;
        }

        /// <inheritdoc/>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> commands = [];

            if (game.Mode is RadioMode)
            {
                if (Radio.IsPlaying(game))
                    commands.Add(Off.CommandHelp);
                else
                    commands.Add(On.CommandHelp);

                var playing = Radio.GetCurrentlyLoadedCasette(game);
                var available = Radio.AvailableCasettes(game);

                if (available.Any(x => x != playing))
                    commands.Add(Change.CommandHelp);

                commands.Add(new CommandHelp(End.CommandHelp.Command, "Exit radio", CommandCategory.Custom));
            }

            return [.. commands];
        }

        #endregion
    }
}
