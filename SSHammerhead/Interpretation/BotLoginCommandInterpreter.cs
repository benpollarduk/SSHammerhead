﻿using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Interpretation;
using NetAF.Logic;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Assets.Regions.Ship.Rooms.L0;
using SSHammerhead.Commands.MaintenancePanel;
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

            if (game.Mode is BotLoginMode mode)
            {
                switch (mode.Stage)
                {
                    case LoginStage.InvalidUserName:

                        return new(true, new LoginResetToUserName());

                    case LoginStage.InvalidPassword:

                        return new(true, new LoginResetToPassword());

                    case LoginStage.UserName:

                        if (LoginUserName.CommandHelp.Equals(input))
                            return new(true, new LoginUserName());
                        else
                            return new(true, new LoginInvalidUserName());

                    case LoginStage.Password:

                        if (LoginPassword.CommandHelp.Equals(input))
                            return new(true, new LoginPassword());
                        else
                            return new(true, new LoginInvalidPassword());

                    case LoginStage.StartMaintenance:

                        if (LoginStartMaintenance.CommandHelp.Equals(input))
                            return new(true, new LoginStartMaintenance());

                        break;
                }
            }

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

            if (game.Mode is BotLoginMode loginMode)
            {
                if (loginMode.Stage == LoginStage.UserName && game.NoteManager.ContainsEntry(Laptop.ScottManagementLogName))
                    commands.Add(LoginUserName.CommandHelp);

                if (loginMode.Stage == LoginStage.Password && game.NoteManager.ContainsEntry(Airlock.SevenLogName))
                    commands.Add(LoginPassword.CommandHelp);

                if (loginMode.Stage == LoginStage.StartMaintenance)
                    commands.Add(LoginStartMaintenance.CommandHelp);

                commands.Add(new CommandHelp(End.CommandHelp.Command, "Abort login", CommandCategory.Information));
            }

            return [.. commands];
        }

        #endregion
    }
}
