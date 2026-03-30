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

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        public IInterpreter Interpreter { get; } = new RadioCommandInterpreter();

        /// <summary>
        /// Get the type of mode this provides.
        /// </summary>
        public GameModeType Type { get; } = GameModeType.Interactive;

        /// <summary>
        /// Render the current state of a game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Render(Game game)
        {
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<IRadioFrameBuilder>().Build(Interpreter?.GetContextualCommandHelp(game) ?? [], game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
