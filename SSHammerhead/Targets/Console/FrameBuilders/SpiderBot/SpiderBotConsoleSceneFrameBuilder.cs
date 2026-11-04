using System;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Targets.Console.FrameBuilders.SpiderBot
{
    /// <summary>
    /// Provides a builder for console scene frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="roomMapBuilder">A builder to use for room maps.</param>
    /// <param name="renderPrompt">Specify if the prompt should be rendered.</param>
    public sealed class SpiderBotConsoleSceneFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, IRoomMapBuilder roomMapBuilder, bool renderPrompt = true) : ISceneFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder = gridStringBuilder;
        private readonly IRoomMapBuilder roomMapBuilder = roomMapBuilder;

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, CommandHelp[] contextualCommands, string[] keywords, bool showMap, RoomMapRenderOptions options, Size size)
        {
            var availableWidth = size.Width - 4;
            var availableHeight = size.Height - 2;
            const int leftMargin = 2;
            const int linePadding = 2;

            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            // display the scene

            gridStringBuilder.DrawWrapped("BOT::MODE::CAMERA", leftMargin, 1, availableWidth, properties.MainTextColor, out _, out var lastY);
            gridStringBuilder.DrawHorizontalDivider(lastY + 1, properties.HorizontalDivider, properties.BorderColor);
            lastY += 2;

            gridStringBuilder.DrawWrapped($"BOT::LOCATION::{room.Identifier.Name.ToUpperInvariant()}", leftMargin, lastY, availableWidth, properties.MainTextColor, out _, out lastY);
            gridStringBuilder.DrawHorizontalDivider(lastY + 1, properties.HorizontalDivider, properties.BorderColor);
            lastY += 2;

            var yMapSpaceStart = lastY;
            var yPrompt = availableHeight - 1;
            var yCommandStart = yPrompt;
            var mapSize = roomMapBuilder?.RenderedSize ?? new Size(0, 0);

            if (contextualCommands != null && contextualCommands.Length > 0)
            {
                const int requiredSpaceForDivider = 3;
                const int requiredSpaceForCommandHeader = 2;
                int requiredSpaceForPrompt = renderPrompt ? 3 : 1;
                var requiredYToFitAllCommands = size.Height - requiredSpaceForCommandHeader - requiredSpaceForPrompt - requiredSpaceForDivider - contextualCommands.Length;
                yCommandStart = Math.Max(requiredYToFitAllCommands, lastY + mapSize.Height);
                lastY = yCommandStart;

                gridStringBuilder.DrawHorizontalDivider(lastY + linePadding, properties.HorizontalDivider, properties.BorderColor);
                gridStringBuilder.DrawWrapped("BOT::TASKS:", leftMargin, lastY + 3, availableWidth, properties.OptionColor, out _, out lastY);

                lastY++;

                for (var index = 0; index < contextualCommands.Length; index++)
                {
                    var contextualCommand = contextualCommands[index];
                    gridStringBuilder.DrawWrapped(contextualCommand.DisplayCommand.ToUpper(), leftMargin, lastY + 1, availableWidth, properties.OptionColor, out _, out lastY);
                    
                    // only continue if not run out of space - the 1 is for the border the ...
                    if (index < contextualCommands.Length - 1 && lastY + 1 + requiredSpaceForPrompt >= size.Height - 1)
                    {
                        gridStringBuilder.DrawWrapped("...", leftMargin, lastY + 1, availableWidth, properties.OptionColor, out _, out lastY);
                        break;
                    }
                }
            }

            if (roomMapBuilder is IConsoleRoomMapBuilder consoleRoomMapBuilder)
            {
                var mapX = size.Width / 2 - mapSize.Width / 2;
                var mapY = yMapSpaceStart + ((yCommandStart - yMapSpaceStart) / 2) - mapSize.Height / 2;
                consoleRoomMapBuilder.BuildRoomMap(room, viewPoint, options, new Point2D(mapX, mapY), out _, out _);
            }
            else
            {    
                roomMapBuilder?.BuildRoomMap(room, viewPoint, options);
            }

            if (renderPrompt)
            {
                gridStringBuilder.DrawHorizontalDivider(yPrompt, properties.HorizontalDivider, properties.BorderColor);
                gridStringBuilder.DrawWrapped(properties.Prompt, leftMargin, availableHeight, availableWidth, properties.PromptColor, out _, out _);
            }

            var promptLengthAndPadding = properties.ShowPrompt ? properties.Prompt.Length + 1 : 0;
            return new GridTextFrame(gridStringBuilder, leftMargin + promptLengthAndPadding, availableHeight, true, properties.BackgroundColor, properties.InputColor);
        }

        #endregion
    }
}
