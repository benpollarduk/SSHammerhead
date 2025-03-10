﻿using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using System;

namespace SSHammerhead.Assets.Players.Naomi.FrameBuilders
{
    /// <summary>
    /// Provides a builder of space view frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class NaomiConsoleSpaceViewFrameBuilder(GridStringBuilder gridStringBuilder)
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

        #endregion

        #region Methods

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="gridVisualBuilder">The grid visual builder</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(GridVisualBuilder gridVisualBuilder, Size size)
        {
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            var output = new GridVisualBuilder(BackgroundColor, AnsiColor.BrightWhite);
            output.Resize(size);

            gridVisualBuilder.Resize(new Size(size.Width - 2, size.Height - 2));

            BuildRandomSpace(gridVisualBuilder);
            BuildConstellation(gridVisualBuilder);

            output.Overlay(0, 0, gridStringBuilder);
            output.Overlay(1, 1, gridVisualBuilder);

            return new GridVisualFrame(output) { ShowCursor = false };
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Build random space.
        /// <param name="gridVisualBuilder">The grid visual builder..</param>
        /// </summary>
        private static void BuildRandomSpace(GridVisualBuilder gridVisualBuilder)
        {
            const int sparseness = 8;
            const int spaceVariation = 12;
            const int starVariation = 140;
            AnsiColor[] spaceColors = [new AnsiColor(spaceVariation, 0, 0), new AnsiColor(0, spaceVariation, 0), new AnsiColor(0, 0, spaceVariation)];
            AnsiColor[] starColors = [new AnsiColor(160, 160, starVariation), new AnsiColor(160, starVariation, 160), new AnsiColor(starVariation, 160, 160), new AnsiColor(25, 25, 25), new AnsiColor(50, 50, 50), new AnsiColor(75, 75, 75)];
            char[] starCharacters = ['.', '+', ':'];
            var random = new Random();

            for (var y = 0; y < gridVisualBuilder.DisplaySize.Height; y++)
            {
                for (var x = 0; x < gridVisualBuilder.DisplaySize.Width; x++)
                {
                    if (random.Next(0, sparseness) == sparseness - 1)
                        gridVisualBuilder.SetCell(x, y, starCharacters[random.Next(0, starCharacters.Length)], starColors[random.Next(0, starColors.Length)], spaceColors[random.Next(0, spaceColors.Length)]);
                    else
                        gridVisualBuilder.SetCell(x, y, spaceColors[random.Next(0, spaceColors.Length)]);
                }
            }
        }

        /// <summary>
        /// Build constellation.
        /// <param name="gridVisualBuilder">The grid visual builder..</param>
        /// </summary>
        private static void BuildConstellation(GridVisualBuilder gridVisualBuilder)
        {
            AnsiColor numberColor = AnsiColor.BrightWhite;
            char numberCharacter = '*';

            var centerX = gridVisualBuilder.DisplaySize.Width / 2;
            var centerY = gridVisualBuilder.DisplaySize.Height / 2;

            // build in center
            gridVisualBuilder.SetCell(centerX - 5, centerY - 3, numberCharacter, numberColor);
            gridVisualBuilder.SetCell(centerX - 2, centerY - 3, numberCharacter, numberColor);
            gridVisualBuilder.SetCell(centerX + 1, centerY - 2, numberCharacter, numberColor);
            gridVisualBuilder.SetCell(centerX + 2, centerY, numberCharacter, numberColor);
            gridVisualBuilder.SetCell(centerX + 3, centerY - 2, numberCharacter, numberColor);
            gridVisualBuilder.SetCell(centerX + 2, centerY - 1, numberCharacter, numberColor);
            gridVisualBuilder.SetCell(centerX - 1, centerY + 1, numberCharacter, numberColor);
            gridVisualBuilder.SetCell(centerX - 3, centerY + 2, numberCharacter, numberColor);
            gridVisualBuilder.SetCell(centerX - 4, centerY + 3, numberCharacter, numberColor);
            gridVisualBuilder.SetCell(centerX - 5, centerY + 4, numberCharacter, numberColor);
        }

        #endregion
    }
}
