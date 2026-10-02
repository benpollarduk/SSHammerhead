using NetAF.Interpretation;
using NetAF.Logic.Modes;
using NetAF.Logic;
using SSHammerhead.Interpretation;
using SSHammerhead.Rendering.FrameBuilders;

namespace SSHammerhead.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for the radio.
    /// </summary>
    public sealed class RadioMode : IGameMode
    {
        #region Implementation of IGameMode

        /// <inheritdoc/>
        public IInterpreter Interpreter { get; } = new RadioCommandInterpreter();

        /// <inheritdoc/>
        public GameModeType Type { get; } = GameModeType.Interactive;

        /// <inheritdoc/>
        public void Render(Game game)
        {
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<IRadioFrameBuilder>().Build(Interpreter?.GetContextualCommandHelp(game) ?? [], game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
