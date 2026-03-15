using Microsoft.Extensions.Configuration;
using NetAF.Events;
using NetAF.Interpretation;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Targets.Markup;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Audio;
using SSHammerhead.Configuration;
using System.Windows;
using System.Windows.Input;

namespace SSHammerhead.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private Game? game;

        #endregion

        #region Properties

        /// <summary>
        /// Get if persistence is available. 
        /// </summary>
        public bool IsPersistenceAvailable
        {
            get { return (bool)GetValue(IsPersistenceAvailableProperty); }
            set { SetValue(IsPersistenceAvailableProperty, value); }
        }

        #endregion

        #region DependencyProperties

        /// <summary>
        /// Identifies the MainWindow.IsPersistenceAvailable property.
        /// </summary>
        public static readonly DependencyProperty IsPersistenceAvailableProperty = DependencyProperty.Register(nameof(IsPersistenceAvailable), typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        #endregion

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

            SubscribeToEvents();
            SetupAndBeginGame();
        }

        #endregion

        #region Methods

        private void SubscribeToEvents()
        {
            void Update(Game game) => IsPersistenceAvailable = game?.Mode is SceneMode;

            EventBus.Subscribe<GameStarted>(x =>
            {
                game = x.Game;
                Update(game);
                AudioPlayer.StartBackgroundMusic(App.Settings.BackgroundMusicVolume, Radio.DetermineProximity(x.Game));
            });
            EventBus.Subscribe<GameFinished>(_ =>
            {
                game = null;
                IsPersistenceAvailable = false;
                AudioPlayer.StopBackgroundMusic();
            });
            EventBus.Subscribe<GameUpdated>(x =>
            {
                Update(x.Game);
                AudioPlayer.AdjustBackgroundMusic(App.Settings.BackgroundMusicVolume, Radio.DetermineProximity(x.Game));
            });
        }

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

        private void HandleInput(Key? key)
        {
            if (!App.Settings.UseSoundEffects)
                return;

            var soundEffect = SoundEffect.KeyPressCharacterRandom;

            if (key == Key.Space)
                soundEffect = SoundEffect.KeyPressSpace;
            else if (key == Key.Enter)
                soundEffect = SoundEffect.KeyPressEnter;

            AudioPlayer.PlaySoundEffect(soundEffect, App.Settings.SoundEffectVolume);
        }

        #endregion

        #region CommandCallbacks

        private void OpenSettingsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var settingsWindow = new ApplicationSettingsWindow() { Owner = this, ShowInTaskbar = false };
            settingsWindow.ShowDialog();
        }

        private void OpenPersistenceCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (game == null)
                return;

            var persistenceWindow = new PersistenceWindow(game) { Owner = this, ShowInTaskbar = false };
            persistenceWindow.ShowDialog();
        }

        #endregion

        #region EventHandlers

        private void NetAFPrompt_KeyPressed(object sender, Key e)
        {
            HandleInput(e);
        }

        private void ButtonLayout_ButtonSelected(object sender, EventArgs e)
        {
            HandleInput(null);
        }

        #endregion
    }
}