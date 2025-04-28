using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using SSHammerhead.Assets.Players.SpiderBot.Logic.Modes;

namespace SSHammerhead.Assets.Players.SpiderBot.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build bot login frames.
    /// </summary>
    internal interface IBotLoginFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="stage">The login stage.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(LoginStage stage, Size size);
    }
}
