using NetAF.Assets;
using NetAF.Imaging.Textures;
using NetAF.Imaging;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Html.Rendering;
using SSHammerhead.ImageHandling;
using System.Diagnostics;
using System;
using NetAF.Targets.Html;

namespace SSHammerhead.Assets.Players.Naomi.FrameBuilders
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="imageProvider">A provider to use images.</param>
    public sealed class NaomiHtmlTitleFrameBuilder(HtmlBuilder builder, IImageProvider imageProvider) : ITitleFrameBuilder
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

            builder.H1(title);
            builder.Br();
            builder.P(description);

            try
            {
                var imageSize = new Size(size.Width, size.Height / 2);
                var stream = imageProvider.GetImageAsStream("Images/space.png");
                var imageBuilder = VisualHelper.FromImage(stream, imageSize, CellAspectRatio.Console, new NoTexturizer());
                var imageAsHtml = HtmlAdapter.ConvertGridVisualBuilderToHtmlString(imageBuilder);
                builder.Raw(imageAsHtml);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception caught appending image: {e.Message}");
            }

            builder.Br();
            builder.Br();

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
