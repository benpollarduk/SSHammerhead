using NetAF.Assets;
using NetAF.Commands;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using SSHammerhead.Rendering.FrameBuilders;
using SSHammerhead.Targets.Console.Frames;

namespace SSHammerhead.Targets.Console.FrameBuilders
{
    /// <summary>
    /// Provides a builder of radio frames.
    /// </summary>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleRadioFrameBuilder(ConsoleRenderProperties properties) : IRadioFrameBuilder
    {
        #region Implementation of IRadioFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(CommandHelp[] contextualCommands, Size size)
        {
            return new ConsoleRadioFrame(contextualCommands, size, properties);
        }

        #endregion
    }
}
