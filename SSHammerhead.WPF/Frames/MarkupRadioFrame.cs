using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Targets.Markup;
using NetAF.Targets.Markup.Rendering;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Assets.Regions.Ship.Items.Casettes;
using System.Windows.Threading;

namespace SSHammerhead.WPF.Frames
{
    /// <summary>
    /// Provides a frame for displaying the radio.
    /// </summary>
    /// <param name="builder">A builder to use for the string layout.</param>
    /// <param name="contextualCommands">The contextual commands to display.</param>
    /// <param name="dispatcher">The dispatcher to use for updating the frame.</param>
    internal class MarkupRadioFrame(MarkupBuilder builder, CommandHelp[] contextualCommands, Dispatcher dispatcher) : IUpdatableFrame
    {
        #region StaticFields

        private static string[] Visuals = 
        [
            MarkupAdapter.ConvertGridVisualBuilderToMarkupString(Radio.GetVisual(CasetteVariation.Zero)),
            MarkupAdapter.ConvertGridVisualBuilderToMarkupString(Radio.GetVisual(CasetteVariation.One)),
            MarkupAdapter.ConvertGridVisualBuilderToMarkupString(Radio.GetVisual(CasetteVariation.Two)),
            MarkupAdapter.ConvertGridVisualBuilderToMarkupString(Radio.GetVisual(CasetteVariation.Three)),
        ];

        #endregion

        #region Fields

        private int count = 0;
        private Timer? timer;
        private TimerCallback? timerCallback;
        private TimeSpan updateFrequency = TimeSpan.FromMilliseconds(500);

        #endregion

        #region Methods

        private void UpdadateFrame(object? state)
        {
            if (!Radio.IsPlaying)
            {
                count = 0;
                return;
            }

            if (count < Visuals.Length - 1)
                count++;
            else
                count = 0;

            Updated?.Invoke(this, this);
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return builder.ToString();
        }

        #endregion

        #region Implementation of IUpdatableFrame

        /// <summary>
        /// Occurs when the frame is updated.
        /// </summary>
        public event EventHandler<IFrame>? Updated;

        /// <summary>
        /// Render this frame on a presenter.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        public void Render(IFramePresenter presenter)
        {
            builder.Clear();

            var currentSong = Radio.IsPlaying ? Radio.NowPlaying().ToString() : "Off";

            builder.Heading("Radio", HeadingLevel.H1);
            builder.Newline();

            builder.WriteLine($"Now playing: {currentSong}");
            builder.Newline();
            builder.Newline();

            var visualAsMarkup = Visuals[count];

            builder.Raw(visualAsMarkup);

            builder.Newline();

            var bold = new TextStyle(Bold: true);

            if (contextualCommands.Length > 0)
            {
                builder.Heading("Commands", HeadingLevel.H2);

                for (var index = 0; index < contextualCommands.Length; index++)
                {
                    var contextualCommand = contextualCommands[index];
                    builder.Write(contextualCommand.DisplayCommand, bold);
                    builder.WriteLine($" - {contextualCommand.Description.EnsureFinishedSentence()}");
                }
            }

            dispatcher?.Invoke(() =>
            {
                if (presenter is IUpdatableFramePresenter updatablePresenter)
                    updatablePresenter.PresentUpdate(ToString());
                else
                    presenter.Present(ToString());
            });
        }

        /// <summary>
        /// Start updating.
        /// </summary>
        public void Start()
        {
            timerCallback = new TimerCallback(UpdadateFrame);
            timer = new Timer(timerCallback, null, updateFrequency, updateFrequency);
        }

        /// <summary>
        /// Stop updating.
        /// </summary>
        public void Stop()
        {
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
            timer?.Dispose();
            timer = null;
        }

        #endregion
    }
}
