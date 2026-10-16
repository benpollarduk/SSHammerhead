using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Assets.Players.SpiderBot.FrameBuilders.Console
{
    /// <summary>
    /// Provides a builder for console reaction frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class BotConsoleReactionFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : IReactionFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder = gridStringBuilder;

        #endregion

        #region Implementation of IReactionFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string message, bool isError, string[] keywords, Size size)
        {
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            var availableWidth = size.Width - 4;
            const int leftMargin = 2;
            var lastY = 1;

            gridStringBuilder.DrawWrapped("BOT::MODE::REACT", leftMargin, lastY, availableWidth, properties.MainTextColor, out _, out lastY);
            gridStringBuilder.DrawHorizontalDivider(lastY + 1, properties.HorizontalDivider, properties.BorderColor);
            lastY += 2;

            if (!string.IsNullOrEmpty(title))
            {
                gridStringBuilder.DrawWrapped($"BOT::LOCATION::{title.ToUpperInvariant()}", leftMargin, lastY, availableWidth, properties.MainTextColor, out _, out lastY);
                gridStringBuilder.DrawHorizontalDivider(lastY + 1, properties.HorizontalDivider, properties.BorderColor);
                lastY += 2;
            }

            gridStringBuilder.DrawWrapped(message.ToUpper().EnsureFinishedSentence(), leftMargin, lastY, availableWidth, properties.MainTextColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, properties.BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
