using NetAF.Commands;
using NetAF.Logic;
using SSHammerhead.Assets.Regions.Core.Items;
using SSHammerhead.Assets.Regions.Core.Rooms.L0;
using SSHammerhead.Logic.Modes;

namespace SSHammerhead.Commands
{
    /// <summary>
    /// Represents the Login Password command.
    /// </summary>
    internal sealed class LoginPassword : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("7", string.Empty, CommandCategory.Custom);

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
                    case LoginStage.Password:

                        if (game.Overworld.CurrentRegion.CurrentRoom.FindItem(MaintenanceControlPanel.Name, out var controlPanel, true))
                        {
                            game.Overworld.CurrentRegion.CurrentRoom.FindItem(LockedMaintenanceControlPanel.Name, out var lockedControlPanel, false);

                            lockedControlPanel.IsPlayerVisible = false;
                            controlPanel.IsPlayerVisible = true;

                            game.NoteManager.Expire(Airlock.SevenLogName);
                            game.NoteManager.Expire(Laptop.ScottManagementLogName);

                            loginMode.Stage = LoginStage.StartMaintenance;

                            return new Reaction(ReactionResult.Silent, string.Empty);
                        }

                        return new Reaction(ReactionResult.Silent, string.Empty);
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
