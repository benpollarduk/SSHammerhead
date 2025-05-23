﻿using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using System;
using System.Linq;

namespace SSHammerhead.Assets.Players.SpiderBot.FrameBuilders
{
    /// <summary>
    /// Provides a builder of color region map frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="regionMapBuilder">A builder for region maps.</param>
    /// <param name="renderPrompt">Specify if the prompt should be rendered.</param>
    public sealed class BotConsoleRegionMapFrameBuilder(GridStringBuilder gridStringBuilder, IRegionMapBuilder regionMapBuilder, bool renderPrompt = true) : IRegionMapFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder = gridStringBuilder;

        #endregion

        #region Properties

        /// <summary>
        /// Get the region map builder.
        /// </summary>
        private IRegionMapBuilder RegionMapBuilder { get; } = regionMapBuilder;

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; }

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the title color.
        /// </summary>
        public AnsiColor TitleColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the commands color.
        /// </summary>
        public AnsiColor CommandsColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the input color.
        /// </summary>
        public AnsiColor InputColor { get; set; } = SpiderBotTemplate.DisplayColor;

        #endregion

        #region Implementation of IRegionMapFrameBuilder

        /// <summary>
        /// Get if this frame builder supports panning.
        /// </summary>
        public bool SupportsPan => false;

        /// <summary>
        /// Get if this frame builder supports zooming.
        /// </summary>
        public bool SupportsZoom => false;

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="focusPosition">The position to focus on.</param>
        /// <param name="detail">The level of detail to use.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Region region, Point3D focusPosition, RegionMapDetail detail, CommandHelp[] contextualCommands, Size size)
        {
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = size.Width - 4;
            var availableHeight = size.Height - 2;
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped("BOT::MODE::MAP", leftMargin, 1, availableWidth, TitleColor, out _, out var lastY);
            gridStringBuilder.DrawHorizontalDivider(lastY + 1, BorderColor);

            gridStringBuilder.DrawWrapped($"BOT::REGION::{region.Identifier.Name.ToUpper()}", leftMargin, lastY + 2, availableWidth, TitleColor, out _, out lastY);
            gridStringBuilder.DrawHorizontalDivider(lastY + 1, BorderColor);

            int commandSpace = 0;
            var mapStartY = lastY + 2;

            if (contextualCommands?.Any() ?? false)
            {
                const int requiredSpaceForDivider = 1;
                const int requiredSpaceForCommandHeader = 2;
                int requiredSpaceForPrompt = renderPrompt ? 3 : 1;
                var requiredYToFitAllCommands = size.Height - requiredSpaceForCommandHeader - requiredSpaceForPrompt - requiredSpaceForDivider - contextualCommands.Length;
                var yStart = Math.Max(requiredYToFitAllCommands, lastY);
                lastY = yStart;

                gridStringBuilder.DrawHorizontalDivider(lastY, BorderColor);
                gridStringBuilder.DrawWrapped("BOT::TASKS:", leftMargin, lastY + 1, availableWidth, CommandsColor, out _, out lastY);

                lastY++;

                for (var index = 0; index < contextualCommands.Length; index++)
                {
                    var contextualCommand = contextualCommands[index];
                    gridStringBuilder.DrawWrapped(contextualCommand.DisplayCommand.ToUpper(), leftMargin, lastY + 1, availableWidth, CommandsColor, out _, out lastY);
                
                    // only continue if not run out of space - the 1 is for the border the ...
                    if (index < contextualCommands.Length - 1 && lastY + 1 + requiredSpaceForPrompt >= size.Height - 1)
                    {
                        gridStringBuilder.DrawWrapped("...", leftMargin, lastY + 1, availableWidth, CommandsColor, out _, out lastY);
                        break;
                    }
                }
            }

            var startMapPosition = new Point2D(leftMargin, mapStartY);
            var mapSize = new Size(availableWidth, size.Height - 4 - commandSpace);

            if (RegionMapBuilder is IConsoleRegionMapBuilder consoleRegionMapBuilder)
                consoleRegionMapBuilder.BuildRegionMap(region, focusPosition, RegionMapDetail.Basic, startMapPosition, mapSize);
            else
                RegionMapBuilder?.BuildRegionMap(region, focusPosition, RegionMapDetail.Basic);

            if (renderPrompt)
            {
                gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, BorderColor);
                gridStringBuilder.DrawWrapped(">", leftMargin, availableHeight, availableWidth, InputColor, out _, out _);
            }

            return new GridTextFrame(gridStringBuilder, 4, availableHeight, BackgroundColor) { ShowCursor = true };
        }

        #endregion
    }
}
