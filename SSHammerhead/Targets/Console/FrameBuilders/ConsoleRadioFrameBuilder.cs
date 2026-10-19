using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Logic;
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
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleRadioFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : IRadioFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

        #endregion

        #region Implementation of IRadioFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(CommandHelp[] contextualCommands, Size size)
        {
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            const int leftMargin = 2;
            var availableWidth = size.Width - 4;
            var availableHeight = size.Height - 2;
            var title = "Radio";
            var currentSong = Radio.IsPlaying(GameExecutor.ExecutingGame) ? Radio.NowPlaying(GameExecutor.ExecutingGame).ToString() : "Off";

            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, properties.TitleColor, out var lastX, out var lastY);
            gridStringBuilder.DrawUnderline(lastX + 1 - title.Length, lastY + 1, title.Length, properties.TitleColor);

            lastY += 3;

            gridStringBuilder.DrawWrapped($"Now playing: {currentSong}", leftMargin, lastY, availableWidth, properties.MainTextColor, out lastX, out lastY);

            var tracklist = $"Tracklist: {string.Join(", ", Radio.GetCurrentlyLoadedCasette(GameExecutor.ExecutingGame).Info.Songs.Select(x => x.Name))}.";
            gridStringBuilder.DrawWrapped(tracklist, leftMargin, lastY + 1, availableWidth, properties.MainTextColor, out lastX, out lastY);
            
            var imageYStart = lastY + 3;
            var commandSpace = 0;

            if (contextualCommands?.Any() ?? false)
            {
                const int requiredSpaceForDivider = 2;
                const int requiredSpaceForCommandHeader = 3;
                int requiredSpaceForPrompt = 3;
                commandSpace = requiredSpaceForCommandHeader + requiredSpaceForPrompt + requiredSpaceForDivider + contextualCommands.Length;
                var requiredYToFitAllCommands = size.Height - commandSpace;

                gridStringBuilder.DrawHorizontalDivider(requiredYToFitAllCommands, properties.HorizontalDivider, properties.BorderColor);
                gridStringBuilder.DrawWrapped(CommandTitle, leftMargin, requiredYToFitAllCommands + 2, availableWidth, properties.OptionColor, out _, out lastY);

                var maxCommandLength = contextualCommands.Max(x => x.DisplayCommand.Length);
                const int padding = 4;
                var dashStartX = leftMargin + maxCommandLength + padding;
                var descriptionStartX = dashStartX + 2;
                lastY++;

                for (var index = 0; index < contextualCommands.Length; index++)
                {
                    var contextualCommand = contextualCommands[index];
                    gridStringBuilder.DrawWrapped(contextualCommand.DisplayCommand, leftMargin, lastY + 1, availableWidth, properties.OptionColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped("-", dashStartX, lastY, availableWidth, properties.OptionColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped(contextualCommand.Description.EnsureFinishedSentence(), descriptionStartX, lastY, availableWidth, properties.OptionColor, out _, out lastY);
                }
            }

            gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, properties.HorizontalDivider, properties.BorderColor);
            gridStringBuilder.DrawWrapped(">", leftMargin, availableHeight, availableWidth, properties.InputColor, out _, out _);

            var output = new GridVisualBuilder(properties.BackgroundColor, properties.TitleColor);
            output.Resize(size);

            output.Overlay(0, 0, gridStringBuilder);

            var imageBuilder = Radio.GetVisual(GameExecutor.ExecutingGame, CasetteVariation.Zero);
            output.Overlay(leftMargin, imageYStart, imageBuilder);

            return new GridVisualFrame(output, 4, availableHeight) { ShowCursor = true };
        }

        #endregion
    }
}
