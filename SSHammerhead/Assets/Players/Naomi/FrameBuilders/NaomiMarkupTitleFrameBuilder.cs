using NetAF.Assets;
using NetAF.Imaging;
using NetAF.Imaging.Textures;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Markup;
using NetAF.Targets.Markup.Rendering;
using SSHammerhead.ImageHandling;
using System;
using System.Diagnostics;

namespace SSHammerhead.Assets.Players.Naomi.FrameBuilders
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="imageProvider">A provider to use images.</param>
    public sealed class NaomiMarkupTitleFrameBuilder(MarkupBuilder builder, IImageProvider imageProvider) : ITitleFrameBuilder
    {
        #region Implementation of ITitleFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, string description, Size size)
        {
            builder.Clear();

            builder.Heading(title, HeadingLevel.H1);
            builder.Newline();
            builder.WriteLine(description);

            try
            {
                var stream = imageProvider.GetImageAsStream("Images/space.png");
                var imageBuilder = VisualHelper.FromImage(stream, size, CellAspectRatio.Console, new NoTexturizer());
                var imageAsHtml = MarkupAdapter.ConvertGridVisualBuilderToMarkupString(imageBuilder);
                builder.Raw(imageAsHtml);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception caught appending image: {e.Message}");
            }

            builder.Newline();

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
