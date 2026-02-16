using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Assets.Players.SpiderBot.FrameBuilders
{
    /// <summary>
    /// Provides a builder of visual frames.
    /// </summary>
    /// <param name="resizeMode">The mode to use when the design size and the render size differ and the content needs to be resized.</param>
    public sealed class BotVisualFrameBuilder(VisualResizeMode resizeMode = VisualResizeMode.Scale) : IVisualFrameBuilder
    {
        #region Implementation of IVisualFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Visual visual, Size size)
        {
            return new GridVisualFrame(visual.VisualBuilder) { ShowCursor = false };
        }

        #endregion
    }
}
