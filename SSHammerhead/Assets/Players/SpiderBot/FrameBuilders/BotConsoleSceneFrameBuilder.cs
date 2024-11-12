using System;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Console;
using NetAF.Rendering.Frames;

namespace SSHammerhead.Assets.Players.SpiderBot.FrameBuilders
{
    /// <summary>
    /// Provides a builder for console scene frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="roomMapBuilder">A builder to use for room maps.</param>
    public sealed class BotConsoleSceneFrameBuilder(GridStringBuilder gridStringBuilder, IRoomMapBuilder roomMapBuilder) : ISceneFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder = gridStringBuilder;
        private readonly IRoomMapBuilder roomMapBuilder = roomMapBuilder;

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

        /// <summary>
        /// Get or set if messages should be displayed in isolation.
        /// </summary>
        public bool DisplayMessagesInIsolation { get; set; } = true;

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="room">Specify the Room.</param>
        /// <param name="viewPoint">Specify the viewpoint from the room.</param>
        /// <param name="player">Specify the player.</param>
        /// <param name="message">Any additional message.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="keyType">The type of key to use.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, string message, CommandHelp[] contextualCommands, KeyType keyType, int width, int height)
        {
            var availableWidth = width - 4;
            var availableHeight = height - 2;
            const int leftMargin = 2;
            const int linePadding = 2;
            var displayMessage = !string.IsNullOrEmpty(message);
            var acceptInput = !(DisplayMessagesInIsolation && displayMessage);

            gridStringBuilder.Resize(new Size(width, height));

            gridStringBuilder.DrawBoundary(BorderColor);

            if (DisplayMessagesInIsolation && displayMessage)
            {
                // display the message in isolation

                gridStringBuilder.DrawWrapped("BOT::MODE::DIAGNOSTICS", leftMargin, 1, availableWidth, TextColor, out _, out var lastY);
                gridStringBuilder.DrawHorizontalDivider(lastY + 1, BorderColor);
                lastY += 2;

                var logStyleMessage = $"BOT::LOG >>> {message.ToUpper().EnsureFinishedSentence()}";
                gridStringBuilder.DrawWrapped(logStyleMessage, leftMargin, lastY, availableWidth, TextColor, out _, out lastY);
            }
            else
            {
                // display the scene

                gridStringBuilder.DrawWrapped("BOT::MODE::CAMERA", leftMargin, 1, availableWidth, TextColor, out _, out var lastY);
                gridStringBuilder.DrawHorizontalDivider(lastY + 1, BorderColor);
                lastY += 2;

                gridStringBuilder.DrawWrapped($"BOT::LOCATION::{room.Identifier.Name.ToUpperInvariant()}", leftMargin, lastY, availableWidth, TextColor, out _, out lastY);
                gridStringBuilder.DrawHorizontalDivider(lastY + 1, BorderColor);
                lastY += 2;

                roomMapBuilder?.BuildRoomMap(room, viewPoint, keyType, width / 2 - 4, lastY + 8, out _, out lastY);

                if (!DisplayMessagesInIsolation && displayMessage)
                {
                    gridStringBuilder.DrawHorizontalDivider(lastY + 3, BorderColor);
                    gridStringBuilder.DrawWrapped(message.EnsureFinishedSentence(), leftMargin, lastY + 5, availableWidth, TextColor, out _, out lastY);
                }

                if (contextualCommands != null && contextualCommands.Length > 0)
                {
                    const int requiredSpaceForDivider = 3;
                    const int requiredSpaceForPrompt = 3;
                    const int requiredSpaceForCommandHeader = 2;
                    var requiredYToFitAllCommands = height - requiredSpaceForCommandHeader - requiredSpaceForPrompt - requiredSpaceForDivider - contextualCommands.Length;
                    var yStart = Math.Max(requiredYToFitAllCommands, lastY);
                    lastY = yStart;

                    gridStringBuilder.DrawHorizontalDivider(lastY + linePadding, BorderColor);
                    gridStringBuilder.DrawWrapped("BOT::TASKS:", leftMargin, lastY + 3, availableWidth, CommandsColor, out _, out lastY);

                    var maxCommandLength = contextualCommands.Max(x => x.Command.Length);
                    const int padding = 4;
                    var dashStartX = leftMargin + maxCommandLength + padding;
                    var descriptionStartX = dashStartX + 2;
                    lastY++;

                    for (var index = 0; index < contextualCommands.Length; index++)
                    {
                        var contextualCommand = contextualCommands[index];
                        gridStringBuilder.DrawWrapped(contextualCommand.Command.ToUpper(), leftMargin, lastY + 1, availableWidth, CommandsColor, out _, out lastY);
                        gridStringBuilder.DrawWrapped("-", dashStartX, lastY, availableWidth, CommandsColor, out _, out lastY);
                        gridStringBuilder.DrawWrapped(contextualCommand.Description.ToUpper().EnsureFinishedSentence(), descriptionStartX, lastY, availableWidth, CommandsColor, out _, out lastY);

                        // only continue if not run out of space - the 1 is for the border the ...
                        if (index < contextualCommands.Length - 1 && lastY + 1 + requiredSpaceForPrompt >= height)
                        {
                            gridStringBuilder.DrawWrapped("...", leftMargin, lastY + 1, availableWidth, CommandsColor, out _, out lastY);
                            break;
                        }
                    }
                }

                gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, BorderColor);
                gridStringBuilder.DrawWrapped(">>>", leftMargin, availableHeight, availableWidth, InputColor, out _, out _);
            }

            return new GridTextFrame(gridStringBuilder, 6, availableHeight, BackgroundColor) { AcceptsInput = acceptInput, ShowCursor = acceptInput };
        }

        #endregion
    }
}
