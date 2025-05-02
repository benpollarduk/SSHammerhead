using NetAF.Assets;
using NetAF.Commands.Global;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using SSHammerhead.Assets.Players.SpiderBot;
using SSHammerhead.Logic.Modes;
using SSHammerhead.Rendering.FrameBuilders;

namespace SSHammerhead.Assets.Players.Naomi.FrameBuilders
{
    /// <summary>
    /// Provides a builder for console scene frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class NaomiConsoleLoginFrameBuilder(GridStringBuilder gridStringBuilder) : IBotLoginFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder = gridStringBuilder;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; }

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the text color.
        /// </summary>
        public AnsiColor TextColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the input color.
        /// </summary>
        public AnsiColor InputColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the commands color.
        /// </summary>
        public AnsiColor CommandsColor { get; set; } = SpiderBotTemplate.DisplayColor;

        #endregion

        #region Implementation of IBotLoginFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="stage">The login stage.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(LoginStage stage, Size size)
        {
            var cursorX = 0;
            var cursorY = 3;
            var availableWidth = size.Width - 4;
            const int leftMargin = 2;

            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            gridStringBuilder.DrawWrapped("BOT::MODE::LOGIN", leftMargin, 1, availableWidth, TextColor, out _, out var lastY);
            gridStringBuilder.DrawHorizontalDivider(lastY + 1, BorderColor);
            lastY += 2;

            if (stage is LoginStage.UserName or LoginStage.Password)
            {
                gridStringBuilder.DrawWrapped($"TYPE {End.CommandHelp.Command.ToUpper()} TO EXIT, OR", leftMargin, lastY, availableWidth, CommandsColor, out _, out _);
                lastY += 1;
                cursorY = 4;
            }

            switch (stage)
            {
                case LoginStage.UserName:
                    cursorX = 18;
                    gridStringBuilder.DrawWrapped("ENTER USERNAME:", leftMargin, lastY, availableWidth, CommandsColor, out _, out _);
                    break;
                case LoginStage.InvalidUserName:
                    cursorX = 19;
                    gridStringBuilder.DrawWrapped("INVALID USERNAME", leftMargin, lastY, availableWidth, CommandsColor, out _, out _);
                    break;
                case LoginStage.Password:
                    cursorX = 18;
                    gridStringBuilder.DrawWrapped("ENTER PASSWORD:", leftMargin, lastY, availableWidth, CommandsColor, out _, out _);
                    break;
                case LoginStage.InvalidPassword:
                    cursorX = 19;
                    gridStringBuilder.DrawWrapped("INVALID PASSWORD", leftMargin, lastY, availableWidth, CommandsColor, out _, out _);
                    break;
                case LoginStage.StartMaintenance:
                    cursorX = 36;
                    gridStringBuilder.DrawWrapped("TYPE 'START' TO START MAINTENANCE", leftMargin, lastY, availableWidth, CommandsColor, out _, out _);
                    break;
            }

            return new GridTextFrame(gridStringBuilder, cursorX, cursorY, BackgroundColor);
        }

        #endregion
    }
}
