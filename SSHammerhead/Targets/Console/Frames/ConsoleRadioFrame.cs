using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Assets.Regions.Ship.Items.Casettes;
using System;
using System.Linq;
using System.Threading;

namespace SSHammerhead.Targets.Console.Frames
{
    /// <summary>
    /// Provides a frame for displaying the radio.
    /// </summary>
    /// <param name="contextualCommands">The contextual commands to display.</param>
    /// <param name="size">The size of the frame.</param>
    /// <param name="properties">The render properties.</param>
    internal class ConsoleRadioFrame(CommandHelp[] contextualCommands, Size size, ConsoleRenderProperties properties) : IUpdatableFrame, IConsoleFrame
    {
        #region Fields

        private int count = 0;
        private Timer timer;
        private TimerCallback timerCallback;
        private TimeSpan updateFrequency = TimeSpan.FromMilliseconds(500);
        private Casette currentCasette;
        private GridVisualBuilder[] visuals = [];

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

        #endregion

        #region Methods

        private void UpdateFrame(object state)
        {
            if (!Radio.IsPlaying(GameExecutor.ExecutingGame))
            {
                count = 0;
                return;
            }

            if (count < visuals?.Length - 1)
                count++;
            else
                count = 0;

            Updated?.Invoke(this, this);
        }

        #endregion

        #region Overrides of Object

        /// <inheritdoc/>
        public override string ToString()
        {
            var builder = new GridStringBuilder();
            builder.Resize(size);

            builder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            const int leftMargin = 2;
            var availableWidth = size.Width - 4;
            var availableHeight = size.Height - 2;
            var title = "Radio";
            var currentSong = Radio.IsPlaying(GameExecutor.ExecutingGame) ? Radio.NowPlaying(GameExecutor.ExecutingGame).ToString() : "Off";

            builder.DrawWrapped(title, leftMargin, 2, availableWidth, properties.TitleColor, out var lastX, out var lastY);
            builder.DrawUnderline(lastX + 1 - title.Length, lastY + 1, title.Length, properties.TitleColor);

            lastY += 3;

            builder.DrawWrapped($"Now playing: {currentSong}", leftMargin, lastY, availableWidth, properties.MainTextColor, out lastX, out lastY);

            var tracklist = $"Tracklist: {string.Join(", ", Radio.GetCurrentlyLoadedCasette(GameExecutor.ExecutingGame).Info.Songs.Select(x => x.Name))}.";
            builder.DrawWrapped(tracklist, leftMargin, lastY + 1, availableWidth, properties.MainTextColor, out lastX, out lastY);

            var imageYStart = lastY + 3;
            var commandSpace = 0;

            if (contextualCommands?.Any() ?? false)
            {
                const int requiredSpaceForDivider = 2;
                const int requiredSpaceForCommandHeader = 3;
                int requiredSpaceForPrompt = 3;
                commandSpace = requiredSpaceForCommandHeader + requiredSpaceForPrompt + requiredSpaceForDivider + contextualCommands.Length;
                var requiredYToFitAllCommands = size.Height - commandSpace;

                builder.DrawHorizontalDivider(requiredYToFitAllCommands, properties.HorizontalDivider, properties.BorderColor);
                builder.DrawWrapped(CommandTitle, leftMargin, requiredYToFitAllCommands + 2, availableWidth, properties.OptionColor, out _, out lastY);

                var maxCommandLength = contextualCommands.Max(x => x.DisplayCommand.Length);
                const int padding = 4;
                var dashStartX = leftMargin + maxCommandLength + padding;
                var descriptionStartX = dashStartX + 2;
                lastY++;

                for (var index = 0; index < contextualCommands.Length; index++)
                {
                    var contextualCommand = contextualCommands[index];
                    builder.DrawWrapped(contextualCommand.DisplayCommand, leftMargin, lastY + 1, availableWidth, properties.OptionColor, out _, out lastY);
                    builder.DrawWrapped("-", dashStartX, lastY, availableWidth, properties.OptionColor, out _, out lastY);
                    builder.DrawWrapped(contextualCommand.Description.EnsureFinishedSentence(), descriptionStartX, lastY, availableWidth, properties.OptionColor, out _, out lastY);
                }
            }

            builder.DrawHorizontalDivider(availableHeight - 1, properties.HorizontalDivider, properties.BorderColor);
            builder.DrawWrapped(">", leftMargin, availableHeight, availableWidth, properties.InputColor, out _, out _);

            CursorLeft = 4;
            CursorTop = availableHeight;

            var output = new GridVisualBuilder(properties.BackgroundColor, properties.TitleColor);
            output.Resize(size);

            output.Overlay(0, 0, builder);

            var loadedCasette = Radio.GetCurrentlyLoadedCasette(GameExecutor.ExecutingGame);

            if (currentCasette != loadedCasette)
                visuals = null;

            if (visuals == null)
            {
                visuals =
                [
                    Radio.GetVisual(GameExecutor.ExecutingGame, CasetteVariation.Zero),
                    Radio.GetVisual(GameExecutor.ExecutingGame, CasetteVariation.One),
                    Radio.GetVisual(GameExecutor.ExecutingGame, CasetteVariation.Two),
                    Radio.GetVisual(GameExecutor.ExecutingGame, CasetteVariation.Three),
                ];
            }

            currentCasette = loadedCasette;

            var casetteVisualBuilder = visuals[count];

            output.Overlay(leftMargin, imageYStart, casetteVisualBuilder);

            return builder.ToString();
        }

        #endregion

        #region Implementation of IUpdatableFrame

        /// <inheritdoc/>
        public event EventHandler<IFrame> Updated;

        /// <inheritdoc/>
        public void Render(IFramePresenter presenter)
        {
            if (presenter is IUpdatableFramePresenter updatablePresenter)
                updatablePresenter.PresentUpdate(ToString());
            else
                presenter.Present(ToString());
        }

        /// <inheritdoc/>
        public void Start()
        {
            Stop();

            timerCallback = new TimerCallback(UpdateFrame);
            timer = new Timer(timerCallback, null, updateFrequency, updateFrequency);
        }

        /// <inheritdoc/>
        public void Stop()
        {
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
            timer?.Dispose();
            timer = null;
        }

        /// <inheritdoc/>
        public int CursorLeft { get; private set; }

        /// <inheritdoc/>
        public int CursorTop { get; private set; }

        /// <inheritdoc/>
        public bool ShowCursor { get; set; } = true;

        /// <inheritdoc/>
        public AnsiColor InputColor => properties.InputColor;

        /// <inheritdoc/>
        public AnsiCell GetCell(int x, int y)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
