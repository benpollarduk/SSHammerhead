using NetAF.Commands;
using NetAF.Logic;

namespace SSHammerhead.Commands.Radio
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
            Assets.Regions.Ship.Items.Radio.Start(game);
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
