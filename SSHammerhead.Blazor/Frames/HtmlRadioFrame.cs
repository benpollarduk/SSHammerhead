using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Targets.Html;
using NetAF.Targets.Html.Rendering;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Assets.Regions.Ship.Items.Casettes;

namespace SSHammerhead.Blazor.Frames
{
    /// <summary>
    /// Provides a frame for displaying the radio.
    /// </summary>
    /// <param name="builder">A builder to use for the string layout.</param>
    /// <param name="contextualCommands">The contextual commands to display.</param>
    internal class HtmlRadioFrame(HtmlBuilder builder, CommandHelp[] contextualCommands) : IUpdatableFrame
    {
        #region Fields

        private int count = 0;
        private Timer? timer;
        private TimerCallback? timerCallback;
        private TimeSpan updateFrequency = TimeSpan.FromMilliseconds(500);
        private Casette? currentCasette;
        private string[]? visuals = [];

        #endregion

        #region Methods

        private void UpdadateFrame(object? state)
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
            return builder.ToString();
        }

        #endregion

        /// <inheritdoc/>
        public event EventHandler<IFrame>? Updated;

        /// <inheritdoc/>
        public void Render(IFramePresenter presenter)
        {
            builder.Clear();

            var currentSong = Radio.IsPlaying(GameExecutor.ExecutingGame) ? Radio.NowPlaying(GameExecutor.ExecutingGame).ToString() : "Off";

            builder.H1("Radio");
            builder.Br();

            builder.H3("Now Playing");
            builder.P(currentSong);
            builder.Br();

            var loadedCasette = Radio.GetCurrentlyLoadedCasette(GameExecutor.ExecutingGame);

            if (currentCasette != loadedCasette)
                visuals = null;

            if (visuals == null)
            {
                visuals =
                [
                    HtmlAdapter.ConvertGridVisualBuilderToHtmlString(Radio.GetVisual(GameExecutor.ExecutingGame, CasetteVariation.Zero)),
                    HtmlAdapter.ConvertGridVisualBuilderToHtmlString(Radio.GetVisual(GameExecutor.ExecutingGame, CasetteVariation.One)),
                    HtmlAdapter.ConvertGridVisualBuilderToHtmlString(Radio.GetVisual(GameExecutor.ExecutingGame, CasetteVariation.Two)),
                    HtmlAdapter.ConvertGridVisualBuilderToHtmlString(Radio.GetVisual(GameExecutor.ExecutingGame, CasetteVariation.Three)),
                ];
            }

            currentCasette = loadedCasette;

            var visualAsMarkup = visuals[count];

            builder.Raw(visualAsMarkup);

            builder.Br();

            var tracklist = $"{string.Join(", ", loadedCasette.Info.Songs.Select(x => x.Name))}.";

            builder.H3("Tracklist");
            builder.P(tracklist);
            builder.Br();

            if (contextualCommands.Length > 0)
            {
                builder.H4("Commands");

                for (var index = 0; index < contextualCommands.Length; index++)
                {
                    var contextualCommand = contextualCommands[index];
                    builder.P($"{contextualCommand.DisplayCommand} - {contextualCommand.Description.EnsureFinishedSentence()}");
                }
            }

            if (presenter is IUpdatableFramePresenter updatablePresenter)
                updatablePresenter.PresentUpdate(ToString());
            else
                presenter.Present(ToString());
        }

        /// <inheritdoc/>
        public void Start()
        {
            timerCallback = new TimerCallback(UpdadateFrame);
            timer = new Timer(timerCallback, null, updateFrequency, updateFrequency);
        }

        /// <inheritdoc/>
        public void Stop()
        {
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
            timer?.Dispose();
            timer = null;
        }
    }
}
