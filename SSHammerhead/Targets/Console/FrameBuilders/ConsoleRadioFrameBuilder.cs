using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Assets.Regions.Ship.Items.Casettes;
using SSHammerhead.Rendering.FrameBuilders;
using System.Linq;

namespace SSHammerhead.Targets.Console.FrameBuilders
{
    /// <summary>
    /// Provides a builder of radio frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class ConsoleRadioFrameBuilder(GridStringBuilder gridStringBuilder) : IRadioFrameBuilder
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
        public AnsiColor TitleColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the information color.
        /// </summary>
        public AnsiColor InformationColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the commands color.
        /// </summary>
        public AnsiColor CommandsColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the input color.
        /// </summary>
        public AnsiColor InputColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

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
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            const int leftMargin = 2;
            var availableWidth = size.Width - 4;
            var availableHeight = size.Height - 2;
            var title = "Radio";
            var currentSong = Radio.IsPlaying ? Radio.NowPlaying() : "Off";

            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, TitleColor, out var lastX, out var lastY);
            gridStringBuilder.DrawUnderline(lastX + 1 - title.Length, lastY + 1, title.Length, TitleColor);

            lastY += 3;

            gridStringBuilder.DrawWrapped($"Now playing: {currentSong}", leftMargin, lastY, availableWidth, InformationColor, out lastX, out lastY);

            var imageYStart = lastY + 3;
            var commandSpace = 0;

            if (contextualCommands?.Any() ?? false)
            {
                const int requiredSpaceForDivider = 2;
                const int requiredSpaceForCommandHeader = 3;
                int requiredSpaceForPrompt = 3;
                commandSpace = requiredSpaceForCommandHeader + requiredSpaceForPrompt + requiredSpaceForDivider + contextualCommands.Length;
                var requiredYToFitAllCommands = size.Height - commandSpace;

                gridStringBuilder.DrawHorizontalDivider(requiredYToFitAllCommands, BorderColor);
                gridStringBuilder.DrawWrapped(CommandTitle, leftMargin, requiredYToFitAllCommands + 2, availableWidth, CommandsColor, out _, out lastY);

                var maxCommandLength = contextualCommands.Max(x => x.DisplayCommand.Length);
                const int padding = 4;
                var dashStartX = leftMargin + maxCommandLength + padding;
                var descriptionStartX = dashStartX + 2;
                lastY++;

                for (var index = 0; index < contextualCommands.Length; index++)
                {
                    var contextualCommand = contextualCommands[index];
                    gridStringBuilder.DrawWrapped(contextualCommand.DisplayCommand, leftMargin, lastY + 1, availableWidth, CommandsColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped("-", dashStartX, lastY, availableWidth, CommandsColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped(contextualCommand.Description.EnsureFinishedSentence(), descriptionStartX, lastY, availableWidth, CommandsColor, out _, out lastY);
                }
            }

            gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, BorderColor);
            gridStringBuilder.DrawWrapped(">", leftMargin, availableHeight, availableWidth, InputColor, out _, out _);

            var output = new GridVisualBuilder(BackgroundColor, TitleColor);
            output.Resize(size);

            output.Overlay(0, 0, gridStringBuilder);

            var imageBuilder = Radio.GetVisual(CasetteVariation.Zero);
            output.Overlay(leftMargin, imageYStart, imageBuilder);

            return new GridVisualFrame(output, 4, availableHeight) { ShowCursor = true };
        }

        #endregion
    }
}
