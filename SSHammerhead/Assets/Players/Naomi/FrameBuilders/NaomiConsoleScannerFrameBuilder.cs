using NetAF.Assets;
using NetAF.Commands.Global;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Rendering.FrameBuilders;
using System;
using System.Linq;

namespace SSHammerhead.Assets.Players.Naomi.FrameBuilders
{
    /// <summary>
    /// Provides a builder of scanner frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="renderPrompt">Specify if the prompt should be rendered.</param>
    public sealed class NaomiConsoleScannerFrameBuilder(GridStringBuilder gridStringBuilder, bool renderPrompt = true) : IScannerFrameBuilder
    {
        #region StaticProperties

        /// <summary>
        /// Get the display color.
        /// </summary>
        private static AnsiColor DisplayColor { get; } = new AnsiColor(200, 255, 200);

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the border character
        /// </summary>
        public char BorderCharacter { get; set; } = '*';

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; } = AnsiColor.Black;

        #endregion

        #region StaticMethods

        private static string FormatNumber(double number)
        {
            return Math.Round(number, 2, MidpointRounding.ToZero).ToString("F2");
        }

        #endregion

        #region Implementation of IScannerFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="targets">The targets.</param>
        /// <param name="composition">The composition of the current target.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(IExaminable[] targets, Composition composition, Size size)
        {
            var availableWidth = size.Width - 4;
            var availableHeight = size.Height - 2;

            gridStringBuilder.Resize(size);

            var top = 4;
            var bottom = 0;
            var left = 4;
            var right = availableWidth - 4;

            gridStringBuilder.DrawCentralisedWrapped($"<=={Scanner.Make.ToUpper()}==>", top + 1, availableWidth, DisplayColor, out _, out var _);
            gridStringBuilder.DrawCentralisedWrapped($"<=={Scanner.Model.ToUpper()}==>", top + 2, availableWidth, DisplayColor, out var lastX, out var lastY);
            lastY += 2;

            if (composition == null)
            {
                gridStringBuilder.DrawCentralisedWrapped("SELECT A TARGET TO SCAN:", lastY, availableWidth, DisplayColor, out _, out lastY);

                if (targets?.Any() ?? false)
                {
                    foreach (var target in targets)
                        gridStringBuilder.DrawCentralisedWrapped($"{target.Identifier.Name.ToUpper()}", lastY + 1, availableWidth, DisplayColor, out lastX, out lastY);
                }
            }
            else
            {
                gridStringBuilder.DrawCentralisedWrapped($"{composition.Name.ToUpper()}:", lastY, availableWidth, DisplayColor, out lastX, out lastY);

                lastY++;

                foreach (var element in composition.Elements.OrderByDescending(x => x.Value))
                    gridStringBuilder.DrawCentralisedWrapped($"{element.Key.ToUpper()}: {FormatNumber(element.Value)}%", lastY + 1, availableWidth, DisplayColor, out lastX, out lastY);

                var remaining = 100 - composition.Elements.Values.Sum();

                if (remaining > 0)
                    gridStringBuilder.DrawCentralisedWrapped($"UNKNOWN: {FormatNumber(remaining)}%", lastY + 1, availableWidth, DisplayColor, out lastX, out lastY);
            }

            if (renderPrompt)
                gridStringBuilder.DrawCentralisedWrapped("_______________",  lastY + 2, availableWidth, DisplayColor, out lastX, out lastY);

            gridStringBuilder.DrawWrapped($"{End.CommandHelp.Command.ToUpper()} TO EXIT", left + 2, lastY + 2, availableWidth, DisplayColor, out _, out lastY);

            bottom = lastY + 1;
            var cursorX = lastX - 14;
            var cursorY = lastY;

            for (var i = left; i <= right; i++)
            {
                gridStringBuilder.SetCell(i, top, BorderCharacter, DisplayColor);
                gridStringBuilder.SetCell(i, bottom, BorderCharacter, DisplayColor);
            }

            for (var i = top; i <= bottom; i++)
            {
                gridStringBuilder.SetCell(left, i, BorderCharacter, DisplayColor);
                gridStringBuilder.SetCell(right, i, BorderCharacter, DisplayColor);
            }

            return new GridTextFrame(gridStringBuilder, renderPrompt ? cursorX : 0, renderPrompt ? cursorY : availableHeight, BackgroundColor);
        }

        #endregion
    }
}
