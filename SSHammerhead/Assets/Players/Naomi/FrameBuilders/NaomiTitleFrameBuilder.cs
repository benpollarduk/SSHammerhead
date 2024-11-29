using NetAF.Assets;
using NetAF.Imaging;
using NetAF.Imaging.Textures;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Rendering.Console.FrameBuilders
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class NaomiTitleFrameBuilder(GridStringBuilder gridStringBuilder) : ITitleFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; } = AnsiColor.Black;

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the title color.
        /// </summary>
        public AnsiColor TitleColor { get; set; } = AnsiColor.BrightWhite;

        /// <summary>
        /// Get or set the description color.
        /// </summary>
        public AnsiColor DescriptionColor { get; set; } = AnsiColor.BrightWhite;

        #endregion

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
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = size.Width - 4;

            gridStringBuilder.DrawCentralisedWrapped(title, 2, availableWidth, TitleColor, out var lastX, out var lastY);
            gridStringBuilder.DrawUnderline(lastX + 1 - title.Length, lastY + 1, title.Length, TitleColor);

            gridStringBuilder.DrawWrapped(description, 2, lastY + 3, availableWidth, DescriptionColor, out _, out lastY);

            var imageBuilder = VisualHelper.FromImage("Images/space.jpg", new(availableWidth, size.Height - 4), CellAspectRatio.Console, new BrightnessTexturizer(BrightnessTexturizer.Hatching, 40));

            var output = new GridVisualBuilder(BackgroundColor, TitleColor);
            output.Resize(size);

            output.Overlay(0, 0, gridStringBuilder);
            output.Overlay((size.Width / 2) - (imageBuilder.DisplaySize.Width / 2), (size.Height / 2) - (imageBuilder.DisplaySize.Height / 2) + lastY / 2, imageBuilder);

            return new GridVisualFrame(output) { ShowCursor = false };
        }

        #endregion
    }
}
