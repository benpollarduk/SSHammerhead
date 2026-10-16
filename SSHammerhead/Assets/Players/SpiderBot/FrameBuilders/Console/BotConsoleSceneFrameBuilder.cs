using System;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Assets.Players.SpiderBot.FrameBuilders.Console
{
    /// <summary>
    /// Provides a builder for console scene frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="roomMapBuilder">A builder to use for room maps.</param>
    /// <param name="renderPrompt">Specify if the prompt should be rendered.</param>
    public sealed class BotConsoleSceneFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, IRoomMapBuilder roomMapBuilder, bool renderPrompt = true) : ISceneFrameBuilder
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

            if (roomMapBuilder is IConsoleRoomMapBuilder consoleRoomMapBuilder)
                consoleRoomMapBuilder.BuildRoomMap(room, viewPoint, options, new Point2D(size.Width / 2 - 4, lastY + 2), out _, out lastY);
            else
                roomMapBuilder?.BuildRoomMap(room, viewPoint, options);

            if (contextualCommands != null && contextualCommands.Length > 0)
            {
                const int requiredSpaceForDivider = 3;
                const int requiredSpaceForCommandHeader = 2;
                int requiredSpaceForPrompt = renderPrompt ? 3 : 1;
                var requiredYToFitAllCommands = size.Height - requiredSpaceForCommandHeader - requiredSpaceForPrompt - requiredSpaceForDivider - contextualCommands.Length;
                var yStart = Math.Max(requiredYToFitAllCommands, lastY);
                lastY = yStart;

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

            if (renderPrompt)
            {
                gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, properties.HorizontalDivider, properties.BorderColor);
                gridStringBuilder.DrawWrapped(">>>", leftMargin, availableHeight, availableWidth, properties.InputColor, out _, out _);
            }

            return new GridTextFrame(gridStringBuilder, 6, availableHeight, properties.BackgroundColor);
        }

        #endregion
    }
}
