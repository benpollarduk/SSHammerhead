using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using SSHammerhead.Assets.Regions.Core.Items;

namespace SSHammerhead.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build scanner frames.
    /// </summary>
    internal interface IScannerFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="targets">The targets.</param>
        /// <param name="composition">The composition of the current target.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(IExaminable[] targets, Composition composition, Size size);
    }
}
