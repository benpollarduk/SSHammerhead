using NetAF.Assets;
using NetAF.Commands.Global;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Rendering.FrameBuilders;
using System;
using System.Linq;

namespace SSHammerhead.Targets.Console.FrameBuilders
{
    /// <summary>
    /// Provides a builder of scanner frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleScannerFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : IScannerFrameBuilder
    {
        #region StaticMethods

        private static string FormatNumber(double number)
        {
            return Math.Round(number, 2, MidpointRounding.ToZero).ToString("F2");
        }

        #endregion

        #region Implementation of IScannerFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(IExaminable[] targets, Composition composition, Size size)
        {
            var availableWidth = size.Width - 4;
            var availableHeight = size.Height - 2;

            gridStringBuilder.Resize(size);

            var top = 4;
            var bottom = 0;
            var left = 4;
            var right = availableWidth - 4;

            gridStringBuilder.DrawCentralisedWrapped($"<=={Scanner.Make.ToUpper()}==>", top + 1, availableWidth, properties.TitleColor, out _, out var _);
            gridStringBuilder.DrawCentralisedWrapped($"<=={Scanner.Model.ToUpper()}==>", top + 2, availableWidth, properties.TitleColor, out var lastX, out var lastY);
            lastY += 2;

            if (composition == null)
            {
                gridStringBuilder.DrawCentralisedWrapped("SELECT A TARGET TO SCAN:", lastY, availableWidth, properties.MainTextColor, out _, out lastY);

                if (targets?.Any() ?? false)
                {
                    foreach (var target in targets)
                        gridStringBuilder.DrawCentralisedWrapped($"{target.Identifier.Name.ToUpper()}", lastY + 1, availableWidth, properties.MainTextColor, out lastX, out lastY);
                }
            }
            else
            {
                gridStringBuilder.DrawCentralisedWrapped($"{composition.Name.ToUpper()}:", lastY, availableWidth, properties.MainTextColor, out lastX, out lastY);

                lastY++;

                foreach (var element in composition.Elements.OrderByDescending(x => x.Value))
                    gridStringBuilder.DrawCentralisedWrapped($"{element.Key.ToUpper()}: {FormatNumber(element.Value)}%", lastY + 1, availableWidth, properties.MainTextColor, out lastX, out lastY);

                var remaining = 100 - composition.Elements.Values.Sum();

                if (remaining > 0)
                    gridStringBuilder.DrawCentralisedWrapped($"UNKNOWN: {FormatNumber(remaining)}%", lastY + 1, availableWidth, properties.MainTextColor, out lastX, out lastY);
            }

            gridStringBuilder.DrawCentralisedWrapped("_______________", lastY + 2, availableWidth, properties.MainTextColor, out lastX, out lastY);
            gridStringBuilder.DrawWrapped($"{End.CommandHelp.Command.ToUpper()} TO EXIT", left + 2, lastY + 2, availableWidth, properties.MainTextColor, out _, out lastY);

            bottom = lastY + 1;
            var cursorX = lastX - 14;
            var cursorY = lastY - 2;

            for (var i = left; i <= right; i++)
            {
                gridStringBuilder.SetCell(i, top, properties.HorizontalDivider, properties.BorderColor);
                gridStringBuilder.SetCell(i, bottom, properties.HorizontalDivider, properties.BorderColor);
            }

            for (var i = top; i <= bottom; i++)
            {
                gridStringBuilder.SetCell(left, i, properties.HorizontalDivider, properties.BorderColor);
                gridStringBuilder.SetCell(right, i, properties.HorizontalDivider, properties.BorderColor);
            }

            return new GridTextFrame(gridStringBuilder, cursorX, cursorY, true, properties.BackgroundColor, properties.InputColor);
        }

        #endregion
    }
}
