using NetAF.Assets;
using NetAF.Commands;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace SSHammerhead.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build radio frames.
    /// </summary>
    public interface IRadioFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(CommandHelp[] contextualCommands, Size size);
    }
}
