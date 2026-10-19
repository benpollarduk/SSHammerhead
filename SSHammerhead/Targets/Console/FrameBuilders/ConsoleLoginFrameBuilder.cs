using NetAF.Assets;
using NetAF.Commands.Global;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using SSHammerhead.Logic.Modes;
using SSHammerhead.Rendering.FrameBuilders;

namespace SSHammerhead.Targets.Console.FrameBuilders
{
    /// <summary>
    /// Provides a builder for console scene frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleLoginFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : IBotLoginFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder = gridStringBuilder;

        #endregion

        #region Implementation of IBotLoginFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(LoginStage stage, Size size)
        {
            var cursorX = 0;
            var cursorY = 3;
            var availableWidth = size.Width - 4;
            const int leftMargin = 2;

            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            gridStringBuilder.DrawWrapped("BOT::MODE::LOGIN", leftMargin, 1, availableWidth, properties.MainTextColor, out _, out var lastY);
            gridStringBuilder.DrawHorizontalDivider(lastY + 1, properties.HorizontalDivider, properties.BorderColor);
            lastY += 2;

            if (stage is LoginStage.UserName or LoginStage.Password)
            {
                gridStringBuilder.DrawWrapped($"TYPE {End.CommandHelp.Command.ToUpper()} TO EXIT, OR", leftMargin, lastY, availableWidth, properties.OptionColor, out _, out _);
                lastY += 1;
                cursorY = 4;
            }

            switch (stage)
            {
                case LoginStage.UserName:
                    cursorX = 18;
                    gridStringBuilder.DrawWrapped("ENTER USERNAME:", leftMargin, lastY, availableWidth, properties.OptionColor, out _, out _);
                    break;
                case LoginStage.InvalidUserName:
                    cursorX = 19;
                    gridStringBuilder.DrawWrapped("INVALID USERNAME", leftMargin, lastY, availableWidth, properties.OptionColor, out _, out _);
                    break;
                case LoginStage.Password:
                    cursorX = 18;
                    gridStringBuilder.DrawWrapped("ENTER PASSWORD:", leftMargin, lastY, availableWidth, properties.OptionColor, out _, out _);
                    break;
                case LoginStage.InvalidPassword:
                    cursorX = 19;
                    gridStringBuilder.DrawWrapped("INVALID PASSWORD", leftMargin, lastY, availableWidth, properties.OptionColor, out _, out _);
                    break;
                case LoginStage.StartMaintenance:
                    cursorX = 36;
                    gridStringBuilder.DrawWrapped("TYPE 'START' TO START MAINTENANCE", leftMargin, lastY, availableWidth, properties.OptionColor, out _, out _);
                    break;
            }

            return new GridTextFrame(gridStringBuilder, cursorX, cursorY, true, properties.BackgroundColor, properties.InputColor);
        }

        #endregion
    }
}
