using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Utilities;
using System;

namespace SSHammerhead.Assets.Regions.Ship.Visuals
{
    internal class SpaceView(Size size) : IAssetTemplate<Animation>
    {
        #region StaticMethods

        /// <summary>
        /// Build the animation.
        /// </summary>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The builders.</returns>
        private static GridVisualBuilder[] BuildAnimation(Size size)
        {
            var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.BrightWhite);
            gridVisualBuilder.Resize(size);

            BuildRandomSpace(gridVisualBuilder);
            BuildConstellation(gridVisualBuilder);

            var variations = 10;
            var builders = new GridVisualBuilder[variations];

            builders[0] = gridVisualBuilder;

            for (var i = 1; i < variations; i++)
                builders[i] = RandomAdjustSpace(builders[0], 30);

            return builders;
        }

        /// <summary>
        /// Build random space.
        /// <param name="gridVisualBuilder">The grid visual builder.</param>
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
        /// Randomly adjust space.
        /// <param name="gridVisualBuilder">The grid visual builder to adjust.</param>
        /// <param name="maximumVariance">The maximum variance for the adjustment.</param>
        /// </summary>
        /// <returns>The adjusted grid visual builder.</returns>
        private static GridVisualBuilder RandomAdjustSpace(GridVisualBuilder gridVisualBuilder, int maximumVariance)
        {
            var newBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.BrightWhite);
            newBuilder.Resize(gridVisualBuilder.DisplaySize);

            var random = new Random();

            for (var y = 0; y < gridVisualBuilder.DisplaySize.Height; y++)
            {
                for (var x = 0; x < gridVisualBuilder.DisplaySize.Width; x++)
                {
                    var character = gridVisualBuilder.GetCharacter(x, y);
                    var background = gridVisualBuilder.GetCellBackgroundColor(x, y);
                    var foreground = gridVisualBuilder.GetCellForegroundColor(x, y);

                    var adjust = random.Next(0, 10) <= 7;
                    var variance = random.Next(-maximumVariance, maximumVariance);

                    var adjustedR = adjust ? Math.Clamp(foreground.R + variance, 0, 255) : foreground.R;
                    var adjustedG = adjust ? Math.Clamp(foreground.G + variance, 0, 255) : foreground.G;
                    var adjustedB = adjust ? Math.Clamp(foreground.B + variance, 0, 255) : foreground.B;

                    newBuilder.SetCell(x, y, character, new AnsiColor((byte)adjustedR, (byte)adjustedG, (byte)adjustedB), background);
                }
            }

            return newBuilder;
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

        #region Implementation of IAssetTemplate<Animation>

        public Animation Instantiate()
        {
            return new Animation(string.Empty, string.Empty, BuildAnimation(size), new AnimationOptions(AnimationOptions.Forever, 5));
        }

        #endregion
    }
}
