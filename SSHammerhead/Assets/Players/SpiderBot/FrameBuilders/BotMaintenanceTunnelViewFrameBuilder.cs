using NetAF.Assets;
using NetAF.Assets.Locations;
using SSHammerhead.Assets.Players.SpiderBot;
using SSHammerhead.Assets.Regions.MaintenanceTunnels.Items;

namespace NetAF.Rendering.Console.FrameBuilders
{
    /// <summary>
    /// Provides a builder of maintenance tunnel view frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class BotMaintenanceTunnelViewFrameBuilder(GridStringBuilder gridStringBuilder)
    {
        #region Properties

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; } = AnsiColor.Black;

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the text color.
        /// </summary>
        public AnsiColor TextColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the scan color.
        /// </summary>
        public AnsiColor ScanColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the padlock key color.
        /// </summary>
        public AnsiColor PadlockKeyColor { get; set; } = NetAFPalette.NetAFRed;

        /// <summary>
        /// Get or set the section diagonal character.
        /// </summary>
        public char SectionDiagonalCharacter { get; set; } = '.';

        /// <summary>
        /// Get or set the section vertical character.
        /// </summary>
        public char SectionVerticalCharacter { get; set; } = '.';

        /// <summary>
        /// Get or set the section horizontal character.
        /// </summary>
        public char SectionHorizontalCharacter { get; set; } = '.';

        /// <summary>
        /// Get or set the access ID character.
        /// </summary>
        public char AccessIDCharacter { get; set; } = '#';

        #endregion

        #region Methods

        /// <summary>
        /// Draw a section of tunnel.
        /// </summary>
        /// <param name="left">The left of the tunnel.</param>
        /// <param name="top">The top of the tunnel.</param>
        /// <param name="right">The right of the tunnel.</param>
        /// <param name="bottom">The bottom of the tunnel.</param>
        /// <param name="section">The size of the section.</param>
        /// <param name="foreground">The foreground color.</param>
        private void DrawSection(int left, int top, int right, int bottom, int section, AnsiColor foreground)
        {
            // diagonal 1
            for (var modifier = 0; modifier < section; modifier++)
                gridStringBuilder.SetCell(left + modifier * 2, top + modifier, SectionDiagonalCharacter, foreground);

            // diagonal 2
            for (var modifier = 0; modifier < section; modifier++)
                gridStringBuilder.SetCell(right - modifier * 2, top + modifier, SectionDiagonalCharacter, foreground);

            // diagonal 3
            for (var modifier = 0; modifier < section; modifier++)
                gridStringBuilder.SetCell(left + modifier * 2, bottom - modifier, SectionDiagonalCharacter, foreground);

            // diagonal 4
            for (var modifier = 0; modifier < section; modifier++)
                gridStringBuilder.SetCell(right - modifier * 2, bottom - modifier, SectionDiagonalCharacter, foreground);

            // box top and bottom
            for (var x = left + section * 2; x < right - section * 2; x++)
            {
                gridStringBuilder.SetCell(x, top + section, SectionHorizontalCharacter, foreground);
                gridStringBuilder.SetCell(x, bottom - section, SectionHorizontalCharacter, foreground);
            }

            // box left and right
            for (var y = top + section; y < bottom - section; y++)
            {
                gridStringBuilder.SetCell(left + section * 2, y, SectionVerticalCharacter, foreground);
                gridStringBuilder.SetCell(right - section * 2, y, SectionVerticalCharacter, foreground);
            }
        }

        /// <summary>
        /// Draw the padlock key.
        /// </summary>
        /// <param name="left">The left of the padlock key.</param>
        /// <param name="bottom">The bottom of the padlock key.</param>
        /// <param name="foreground">The foreground color.</param>
        private void DrawPadlockKey(int left, int bottom, AnsiColor foreground)
        {
            // draw key
            gridStringBuilder.SetCell(left, bottom, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left, bottom - 1, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 1, bottom, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 2, bottom, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 2, bottom - 1, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 3, bottom, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 4, bottom, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 5, bottom, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 6, bottom, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 7, bottom, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 8, bottom, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 6, bottom - 1, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 7, bottom - 1, AccessIDCharacter, foreground);
            gridStringBuilder.SetCell(left + 8, bottom - 1, AccessIDCharacter, foreground);
        }

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Room room, Size size)
        {
            var availableWidth = size.Width - 4;
            const int leftMargin = 2;
            var top = 5;
            var bottom = size.Height - 2;
            var left = 1;
            var right = size.Width - 2;

            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            // display the scene

            gridStringBuilder.DrawWrapped("BOT::MODE::SCAN", leftMargin, 1, availableWidth, TextColor, out _, out var lastY);
            gridStringBuilder.DrawHorizontalDivider(lastY + 1, BorderColor);
            lastY += 2;

            gridStringBuilder.DrawWrapped($"BOT::LOCATION::{room.Identifier.Name.ToUpperInvariant()}", leftMargin, lastY, availableWidth, TextColor, out _, out lastY);
            gridStringBuilder.DrawHorizontalDivider(lastY + 1, BorderColor);

            int section = 5;
            const int sections = 4;

            for (var i = 0; i < sections; i++)
            {
                DrawSection(left, top, right, bottom, section, ScanColor);

                if (i == 2 && room.FindItem(PadlockKey.Name, out _))
                    DrawPadlockKey((size.Width / 2) - 5, bottom - 1, PadlockKeyColor);

                left += section * 2;
                top += section;
                right -= section * 2;
                bottom -= section;
                section--;
            }

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
