using NetAF.Assets;
using NetAF.Commands;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using SSHammerhead.Rendering.FrameBuilders;

namespace SSHammerhead.Assets.Players.Naomi.FrameBuilders.Console
{
    /// <summary>
    /// Provides a builder of radio frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class NaomiConsoleRadioFrameBuilder(GridStringBuilder gridStringBuilder) : IRadioFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the border character
        /// </summary>
        public char BorderCharacter { get; set; } = '*';

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; } = AnsiColor.Black;

        #endregion

        #region Implementation of IRadioFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(CommandHelp[] contextulCommands, Size size)
        {
            var availableWidth = size.Width - 4;
            var availableHeight = size.Height - 2;

            gridStringBuilder.Resize(size);

            return new GridTextFrame(gridStringBuilder, availableWidth, availableHeight, BackgroundColor);
        }

        #endregion
    }
}
