using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Assets.Players.SpiderBot.FrameBuilders
{
    /// <summary>
    /// Provides a builder for console reaction frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class BotConsoleReactionFrameBuilder(GridStringBuilder gridStringBuilder) : IReactionFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder = gridStringBuilder;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; }

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the text color.
        /// </summary>
        public AnsiColor TextColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the input color.
        /// </summary>
        public AnsiColor InputColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the commands color.
        /// </summary>
        public AnsiColor CommandsColor { get; set; } = SpiderBotTemplate.DisplayColor;

        #endregion

        #region Implementation of IReactionFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title to display to the user.</param>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="isError">If the message is an error.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, string message, bool isError, Size size)
        {
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = size.Width - 4;
            const int leftMargin = 2;
            var lastY = 1;

            gridStringBuilder.DrawWrapped("BOT::MODE::REACT", leftMargin, lastY, availableWidth, TextColor, out _, out lastY);
            gridStringBuilder.DrawHorizontalDivider(lastY + 1, BorderColor);
            lastY += 2;

            if (!string.IsNullOrEmpty(title))
            {
                gridStringBuilder.DrawWrapped($"BOT::LOCATION::{title.ToUpperInvariant()}", leftMargin, lastY, availableWidth, TextColor, out _, out lastY);
                gridStringBuilder.DrawHorizontalDivider(lastY + 1, BorderColor);
                lastY += 2;
            }

            gridStringBuilder.DrawWrapped(message.ToUpper().EnsureFinishedSentence(), leftMargin, lastY, availableWidth, TextColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
