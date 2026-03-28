using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Interpretation;
using NetAF.Logic;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Commands.MaintenancePanel;
using SSHammerhead.Logic.Modes;
using System.Collections.Generic;

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

            if (RadioOff.Off.Equals(input))
                return new(true, new RadioOff());

            if (RadioOn.On.Equals(input))
                return new(true, new RadioOn());

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

            if (game.Mode is RadioMode)
            {
                if (Radio.IsPlaying)
                    commands.Add(RadioOff.Off);
                else
                    commands.Add(RadioOn.On);

                commands.Add(new CommandHelp(End.CommandHelp.Command, "Exit radio", CommandCategory.Custom));
            }

            return [.. commands];
        }

        #endregion
    }
}
