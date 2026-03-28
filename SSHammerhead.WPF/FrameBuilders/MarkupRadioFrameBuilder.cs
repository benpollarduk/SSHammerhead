using NetAF.Assets;
using NetAF.Commands;
using NetAF.Rendering;
using NetAF.Targets.Markup.Rendering;
using SSHammerhead.Rendering.FrameBuilders;
using SSHammerhead.WPF.Frames;
using System.Windows.Threading;

namespace SSHammerhead.WPF.FrameBuilders
{
    /// <summary>
    /// Provides a builder of radio frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="dispatcher">The dispatcher to use for updating the frame.</param>
    public sealed class MarkupRadioFrameBuilder(MarkupBuilder builder, Dispatcher dispatcher) : IRadioFrameBuilder
    {
        #region Implementation of IRadioFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(CommandHelp[] contextualCommands, Size size)
        {
            return new MarkupRadioFrame(builder, contextualCommands, dispatcher);
        }

        #endregion
    }
}
