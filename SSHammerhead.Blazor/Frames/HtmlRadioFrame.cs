using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Markup;
using SSHammerhead.Assets.Regions.Ship.Items;

namespace SSHammerhead.Blazor.Frames
{
    /// <summary>
    /// Provides a frame for displaying the radio.
    /// </summary>
    /// <param name="builder">A builder to use for the string layout.</param>
    /// <param name="contextualCommands">The contextual commands to display.</param>
    internal class HtmlRadioFrame(HtmlBuilder builder, CommandHelp[] contextualCommands) : IUpdatableFrame
    {
        #region StaticFields

        private static string Visual1 = MarkupAdapter.ConvertGridVisualBuilderToMarkupString(Radio.GetVisual(AnsiColor.Black, '+'));
        private static string Visual2 = MarkupAdapter.ConvertGridVisualBuilderToMarkupString(Radio.GetVisual(AnsiColor.Black, 'x'));

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
                return;

            count++;
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

            builder.H1("Radio");
            builder.Br();

            var visualAsMarkup = (count % 2) switch
            {
                0 => Visual1,
                _ => Visual2,
            };

            builder.Raw(visualAsMarkup);

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
    }
}
