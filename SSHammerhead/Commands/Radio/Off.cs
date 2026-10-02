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

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            Radio.Stop(game);
            return new(ReactionResult.Silent, "You turn the radio off.");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}
