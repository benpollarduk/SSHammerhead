using NetAF.Commands;
using NetAF.Logic;
using SSHammerhead.Assets.Regions.Ship.Items;

namespace SSHammerhead.Commands.MaintenancePanel
{
    /// <summary>
    /// Represents the Radio on command.
    /// </summary>
    internal sealed class On : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new CommandHelp("On", "Turn the radio on.", CommandCategory.Custom);

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            Radio.Start(game);
            return new(ReactionResult.Silent, "You turn the radio on.");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}
