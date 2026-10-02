using NetAF.Interpretation;
using NetAF.Logic.Modes;
using NetAF.Logic;
using SSHammerhead.Interpretation;
using SSHammerhead.Rendering.FrameBuilders;

namespace SSHammerhead.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for bot login.
    /// </summary>
    public sealed class BotLoginMode : IGameMode
    {
        #region Properties

        /// <summary>
        /// Get or set the stage.
        /// </summary>
        public LoginStage Stage { get; set; }

        #endregion

        #region Implementation of IGameMode

        /// <inheritdoc/>
        public IInterpreter Interpreter { get; } = new BotLoginCommandInterpreter();

        /// <inheritdoc/>
        public GameModeType Type { get; } = GameModeType.Interactive;

        /// <inheritdoc/>
        public void Render(Game game)
        {
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<IBotLoginFrameBuilder>().Build(Stage, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
