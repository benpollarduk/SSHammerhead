using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Utilities;
using System;

namespace SSHammerhead.Assets.Regions.Ship.Visuals
{
    internal class SpaceView(Size size) : IAssetTemplate<Visual>
    {
        #region StaticMethods

        /// <summary>
        /// Build the visual.
        /// </summary>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The builder.</returns>
        private static GridVisualBuilder BuildVisual(Size size)
        {
            var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.BrightWhite);
            gridVisualBuilder.Resize(size);

            BuildRandomSpace(gridVisualBuilder);
            BuildConstellation(gridVisualBuilder);

            return gridVisualBuilder;
        }

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

        #region Implementation of IAssetTemplate<Visual>

        public Visual Instantiate()
        {
            return new Visual(string.Empty, string.Empty, BuildVisual(size));
        }

        #endregion
    }
}
