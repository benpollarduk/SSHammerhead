using NetAF.Interpretation;
using NetAF.Logic.Modes;
using NetAF.Logic;
using SSHammerhead.Interpretation;
using SSHammerhead.Rendering.FrameBuilders;
using SSHammerhead.Assets.Regions.Core.Items;
using NetAF.Assets;

namespace SSHammerhead.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for scanner.
    /// </summary>
    public sealed class ScannerMode : IGameMode
    {
        #region Properties

        /// <summary>
        /// Get or set the selected composition to display.
        /// </summary>
        public Composition Composition { get; set; }

        /// <summary>
        /// Get or set the targets.
        /// </summary>
        public IExaminable[] Targets { get; set; }

        /// <summary>
        /// Get or set if the composition should be forgotten after render.
        /// </summary>
        public bool ForgetCompositionAfterRender { get; set; } = true;

        #endregion

        #region Implementation of IGameMode

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        public IInterpreter Interpreter { get; } = new ScannerCommandInterpreter();

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
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<IScannerFrameBuilder>().Build(Targets, Composition, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);

            if (ForgetCompositionAfterRender)
                Composition = null;
        }

        #endregion
    }
}
