using NetAF.Assets;
using NetAF.Imaging;
using NetAF.Imaging.Textures;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using System;
using System.Diagnostics;

namespace SSHammerhead.Targets.Console.FrameBuilders
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleModernTitleFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : ITitleFrameBuilder
    {
        #region Implementation of ITitleFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string description, Size size)
        {
            gridStringBuilder.Resize(size);

            var availableWidth = size.Width - 4;

            gridStringBuilder.DrawCentralisedWrapped(title, 2, availableWidth, properties.TitleColor, out var lastX, out var lastY);
            gridStringBuilder.DrawWrapped(description, 2, lastY + 3, availableWidth, properties.MainTextColor, out _, out lastY);

            var output = new GridVisualBuilder(properties.BackgroundColor, properties.TitleColor);
            output.Resize(size);

            output.Overlay(0, 0, gridStringBuilder);

            try
            {
                var stream = Services.ImageProvider.GetImageAsStream("Resources/Images/space.png");
                var imageBuilder = VisualHelper.FromImage(stream, new(availableWidth, size.Height - 4), CellAspectRatio.Console, new NoTexturizer());
                output.Overlay(size.Width / 2 - imageBuilder.DisplaySize.Width / 2, size.Height / 2 - imageBuilder.DisplaySize.Height / 2 + lastY / 2, imageBuilder);
            }
            catch (Exception e) 
            {
                Debug.WriteLine($"Exception caught appending image: {e.Message}");
            }

            return new GridVisualFrame(output) { ShowCursor = false };
        }

        #endregion
    }
}
