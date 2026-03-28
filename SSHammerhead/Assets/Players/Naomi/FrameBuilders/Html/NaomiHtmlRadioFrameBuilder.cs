using NetAF.Assets;
using NetAF.Commands;
using NetAF.Imaging;
using NetAF.Imaging.Textures;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Html;
using NetAF.Targets.Html.Rendering;
using SSHammerhead.Rendering.FrameBuilders;
using System;
using System.Diagnostics;

namespace SSHammerhead.Assets.Players.Naomi.FrameBuilders.Html
{
    /// <summary>
    /// Provides a builder of radio frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="contextualCommands">The contextual commands to display.</param>
    public sealed class NaomiHtmlRadioFrameBuilder(HtmlBuilder builder) : IRadioFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the border character
        /// </summary>
        public char BorderCharacter { get; set; } = '*';

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; } = AnsiColor.Black;

        #endregion

        #region Implementation of IRadioFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(CommandHelp[] contextualCommands, Size size)
        {
            builder.Clear();

            try
            {
                var stream = Services.ImageProvider.GetImageAsStream("Resources/Images/space.png");
                var imageBuilder = VisualHelper.FromImage(stream, size, CellAspectRatio.Console, new NoTexturizer());
                var imageAsHtml = HtmlAdapter.ConvertGridVisualBuilderToHtmlString(imageBuilder);
                builder.Raw(imageAsHtml);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception caught appending image: {e.Message}");
            }

            builder.Br();

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
