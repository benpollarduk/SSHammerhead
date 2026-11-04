using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Targets.Console.FrameBuilders.SpiderBot
{
    /// <summary>
    /// Provides a builder of visual frames.
    /// </summary>
    public sealed class BotVisualFrameBuilder : IVisualFrameBuilder
    {
        #region Implementation of IVisualFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(Visual visual, Size size)
        {
            return new GridVisualFrame(visual.VisualBuilder) { ShowCursor = false };
        }

        #endregion
    }
}
