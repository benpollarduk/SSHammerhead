using NetAF.Commands;
using NetAF.Logic;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.SpiderBot;
using SSHammerhead.Logic.Modes;

namespace SSHammerhead.Commands.MaintenancePanel
{
    /// <summary>
    /// Represents the Login Invalid Start Maintenance command.
    /// </summary>
    internal sealed class LoginStartMaintenance : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Start", string.Empty, CommandCategory.Custom);

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help => CommandHelp;

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (game.Mode is BotLoginMode)
                return PlayableCharacterManager.Switch(SpiderBotTemplate.Identifier, game);

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
