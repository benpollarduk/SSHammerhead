using NetAF.Interpretation;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Targets.Markup;
using SSHammerhead.Configuration;
using System.Windows;
using Microsoft.Extensions.Configuration;

namespace SSHammerhead.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region StaticProperties

        public static IConfiguration? Config { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            SetupAndBeginGame();
        }

        #endregion

        #region Methods

        private void SetupAndBeginGame()
        {
            var presentation = new Presentation
            (
                FrameBuilderCollections.NaomiMarkup,
                FrameBuilderCollections.BotMarkup,
                FrameBuilderCollections.AnneMarkup,
                FrameBuilderCollections.AlexMarkup,
                FrameBuilderCollections.MarinaMarkup,
                FrameBuilderCollections.ScottMarkup,
                FrameBuilderCollections.ZhiyingMarkup
            );

            var configuration = new GameConfiguration(new MarkupAdapter(Terminal), FrameBuilderCollections.NaomiMarkup, new NetAF.Assets.Size(80, 30));

            var sceneInterpreter = new InputInterpreter
            (
                new FrameCommandInterpreter(),
                new GlobalCommandInterpreter(),
                new ExecutionCommandInterpreter(),
                new CustomCommandInterpreter(),
                new SceneCommandInterpreter()
            );

            // change configuration prevent using the normal persistence interpreter as this is handled by custom commands
            configuration.InterpreterProvider.Register(typeof(SceneMode), sceneInterpreter);

            GameExecutor.Execute(TroubleAboardTheSSHammerhead.Create(configuration, presentation));
        }

        private void HandleInput()
        {
            if (!App.Settings.UseSoundEffects)
                return;

            AudioPlayer.PlaySoundEffect(Audio.SoundEffect.KeyPressRandom);
        }

        #endregion

        #region CommandCallbacks

        private void OpenSettingsCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var settingsWindow = new ApplicationSettingsWindow() { Owner = this, ShowInTaskbar = false };
            settingsWindow.ShowDialog();
        }

        private void OpenPersistenceCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var persistenceWindow = new PersistenceWindow() { Owner = this, ShowInTaskbar = false };
            persistenceWindow.ShowDialog();
        }

        #endregion

        #region EventHandlers

        private void NetAFPrompt_KeyPressed(object sender, System.Windows.Input.Key e)
        {
            HandleInput();
        }

        private void ButtonLayout_ButtonSelected(object sender, EventArgs e)
        {
            HandleInput();
        }

        #endregion
    }
}