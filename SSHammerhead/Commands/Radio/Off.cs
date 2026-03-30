using NetAF.Commands;
using NetAF.Logic;
using SSHammerhead.Assets.Regions.Ship.Items;

namespace SSHammerhead.Commands.MaintenancePanel
{
    /// <summary>
    /// Represents the Radio off command.
    /// </summary>
    internal sealed class Off : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new CommandHelp("Off", "Turn the radio off.", CommandCategory.Custom);

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
            Radio.Stop(game);
            return new(ReactionResult.Silent, "You turn the radio off.");
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
