using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.SpiderBot;
using SSHammerhead.Assets.Regions.MaintenanceTunnels.Items;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels.Visuals
{
    internal class TunnelView(Room room, Size size) : IAssetTemplate<Visual>
    {
        #region StaticProperties

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        private static AnsiColor BackgroundColor { get; set; } = AnsiColor.Black;

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        private static AnsiColor BorderColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the text color.
        /// </summary>
        private static AnsiColor TextColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the scan color.
        /// </summary>
        private static AnsiColor ScanColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the padlock key color.
        /// </summary>
        private static AnsiColor PadlockKeyColor { get; set; } = NetAFPalette.NetAFRed;

        /// <summary>
        /// Get or set the section diagonal character.
        /// </summary>
        private const char SectionDiagonalCharacter = '.';

        /// <summary>
        /// Get or set the section vertical character.
        /// </summary>
        private const char SectionVerticalCharacter = '.';

        /// <summary>
        /// Get or set the section horizontal character.
        /// </summary>
        private const char SectionHorizontalCharacter = '.';

        /// <summary>
        /// Get or set the access ID character.
        /// </summary>
        private const char AccessIDCharacter = '#';

        #endregion

        #region StaticMethods

        /// <summary>
        /// Draw a section of tunnel.
        /// </summary>
        /// <param name="visualBuilder">The visual builder.</param>
        /// <param name="left">The left of the tunnel.</param>
        /// <param name="left">The left of the tunnel.</param>
        /// <param name="top">The top of the tunnel.</param>
        /// <param name="right">The right of the tunnel.</param>
        /// <param name="bottom">The bottom of the tunnel.</param>
        /// <param name="section">The size of the section.</param>
        /// <param name="foreground">The foreground color.</param>
        private static void DrawSection(GridVisualBuilder visualBuilder, int left, int top, int right, int bottom, int section, AnsiColor foreground)
        {
            // diagonal 1
            for (var modifier = 0; modifier < section; modifier++)
                visualBuilder.SetCell(left + modifier * 2, top + modifier, SectionDiagonalCharacter, foreground);

            // diagonal 2
            for (var modifier = 0; modifier < section; modifier++)
                visualBuilder.SetCell(right - modifier * 2, top + modifier, SectionDiagonalCharacter, foreground);

            // diagonal 3
            for (var modifier = 0; modifier < section; modifier++)
                visualBuilder.SetCell(left + modifier * 2, bottom - modifier, SectionDiagonalCharacter, foreground);

            // diagonal 4
            for (var modifier = 0; modifier < section; modifier++)
                visualBuilder.SetCell(right - modifier * 2, bottom - modifier, SectionDiagonalCharacter, foreground);

            // box top and bottom
            for (var x = left + section * 2; x < right - section * 2; x++)
            {
                visualBuilder.SetCell(x, top + section, SectionHorizontalCharacter, foreground);
                visualBuilder.SetCell(x, bottom - section, SectionHorizontalCharacter, foreground);
            }

            // box left and right
            for (var y = top + section; y < bottom - section; y++)
            {
                visualBuilder.SetCell(left + section * 2, y, SectionVerticalCharacter, foreground);
                visualBuilder.SetCell(right - section * 2, y, SectionVerticalCharacter, foreground);
            }
        }

        /// <summary>
        /// Draw the padlock key.
        /// </summary>
        /// <param name="visualBuilder">The visual builder.</param>
        /// <param name="left">The left of the padlock key.</param>
        /// <param name="bottom">The bottom of the padlock key.</param>
        /// <param name="foreground">The foreground color.</param>
        private static void DrawPadlockKey(GridVisualBuilder visualBuilder, int left, int bottom, AnsiColor foreground)
        {
            // draw key
            visualBuilder.SetCell(left, bottom, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left, bottom - 1, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 1, bottom, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 2, bottom, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 2, bottom - 1, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 3, bottom, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 4, bottom, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 5, bottom, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 6, bottom, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 7, bottom, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 8, bottom, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 6, bottom - 1, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 7, bottom - 1, AccessIDCharacter, foreground);
            visualBuilder.SetCell(left + 8, bottom - 1, AccessIDCharacter, foreground);
        }

        /// <summary>
        /// Draw the padlock key.
        /// </summary>
        /// <param name="visualBuilder">The visual builder.</param>
        /// <param name="text">The text.</param>
        /// <param name="x">The x location.</param>
        /// <param name="y">The y location.</param>
        /// <param name="foreground">The foreground color.</param>
        private static void DrawText(GridVisualBuilder visualBuilder, string text, int x, int y, AnsiColor foreground)
        {
            for (var i = 0; i < text.Length; i++)
                visualBuilder.SetCell(x + i, y, text[i], foreground);
        }

        /// <summary>
        /// Fill a horizontal line.
        /// </summary>
        /// <param name="visualBuilder">The visual builder.</param>
        /// <param name="character">The character.</param>
        /// <param name="y">The y location.</param>
        /// <param name="foreground">The foreground color.</param>
        private static void FillLine(GridVisualBuilder visualBuilder, char character, int y, AnsiColor foreground)
        {
            for (var i = 0; i < visualBuilder.DisplaySize.Width; i++)
                visualBuilder.SetCell(i, y, character, foreground);
        }

        /// <summary>
        /// Fill a vertical line.
        /// </summary>
        /// <param name="visualBuilder">The visual builder.</param>
        /// <param name="character">The character.</param>
        /// <param name="x">The x location.</param>
        /// <param name="foreground">The foreground color.</param>
        private static void FillVertcialLine(GridVisualBuilder visualBuilder, char character, int x, AnsiColor foreground)
        {
            for (var i = 0; i < visualBuilder.DisplaySize.Height; i++)
                visualBuilder.SetCell(x, i, character, foreground);
        }

        /// <summary>
        /// Build the visual.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The builder.</returns>
        private static GridVisualBuilder BuildVisual(Room room, Size size)
        {
            const int leftMargin = 2;
            var top = 5;
            var bottom = size.Height - 2;
            var left = 1;
            var right = size.Width - 2;

            var gridVisualBuilder = new GridVisualBuilder(BackgroundColor, TextColor);
            gridVisualBuilder.Resize(size);

            // display the scene

            FillLine(gridVisualBuilder, '+', 0, BorderColor);
            DrawText(gridVisualBuilder, "BOT::MODE::SCAN", leftMargin, 1, TextColor);
            FillLine(gridVisualBuilder, '+', 2, BorderColor);
            DrawText(gridVisualBuilder, $"BOT::LOCATION::{room.Identifier.Name.ToUpperInvariant()}", leftMargin, 3, TextColor);
            FillLine(gridVisualBuilder, '+', 4, BorderColor);

            int section = 5;
            const int sections = 4;

            for (var i = 0; i < sections; i++)
            {
                DrawSection(gridVisualBuilder, left, top, right, bottom, section, ScanColor);

                if (i == 2 && room.FindItem(PadlockKey.Name, out _))
                    DrawPadlockKey(gridVisualBuilder, size.Width / 2 - 5, bottom - 1, PadlockKeyColor);

                left += section * 2;
                top += section;
                right -= section * 2;
                bottom -= section;
                section--;
            }

            FillLine(gridVisualBuilder, '+', size.Height - 1, BorderColor);
            FillVertcialLine(gridVisualBuilder, '[', 0, BorderColor);
            FillVertcialLine(gridVisualBuilder, ']', size.Width - 1, BorderColor);

            return gridVisualBuilder;
        }

        #endregion

        #region Implementation of IAssetTemplate<Visual>

        public Visual Instantiate()
        {
            return new Visual(string.Empty, string.Empty, BuildVisual(room ,size));
        }

        #endregion
    }
}
