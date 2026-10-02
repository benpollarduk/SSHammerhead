using NetAF.Assets;
using NetAF.Commands;
using NetAF.Rendering;
using NetAF.Targets.Html.Rendering;
using SSHammerhead.Blazor.Frames;
using SSHammerhead.Rendering.FrameBuilders;

namespace SSHammerhead.Blazor.FrameBuilders
{
    /// <summary>
    /// Provides a builder of radio frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class HtmlRadioFrameBuilder(HtmlBuilder builder) : IRadioFrameBuilder
    {
        #region Implementation of IRadioFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(CommandHelp[] contextualCommands, Size size)
        {
            return new HtmlRadioFrame(builder, contextualCommands);
        }

        #endregion
    }
}
