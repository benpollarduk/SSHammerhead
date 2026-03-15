using Microsoft.Extensions.Configuration;
using NetAF.Events;
using NetAF.Interpretation;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Targets.Markup;
using NetAF.Targets.WPF.Controls;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Audio;
using SSHammerhead.Configuration;
using SSHammerhead.WPF.Controls;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace SSHammerhead.WPF.Windows
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
            private set { SetValue(IsPersistenceAvailableProperty, value); }
        }

        /// <summary>
        /// Get the active notification. 
        /// </summary>
        public UIElement? ActiveNotification
        {
            get { return (UIElement?)GetValue(ActiveNotificationProperty); }
            private set { SetValue(ActiveNotificationProperty, value); }
        }

        #endregion

        #region DependencyProperties

        /// <summary>
        /// Identifies the MainWindow.IsPersistenceAvailable property.
        /// </summary>
        public static readonly DependencyProperty IsPersistenceAvailableProperty = DependencyProperty.Register(nameof(IsPersistenceAvailable), typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        /// <summary>
        /// Identifies the MainWindow.ActiveNotification property.
        /// </summary>
        public static readonly DependencyProperty ActiveNotificationProperty = DependencyProperty.Register(nameof(ActiveNotification), typeof(UIElement), typeof(MainWindow));

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

            // have to dynamically find the terminal because it is nested in a window control which prevents naming
            var hostedGrid = WindowControl.HostedContent as Grid;
            var teminal = hostedGrid?.Children.OfType<NetAFMarkupTerminal>().FirstOrDefault();
            var configuration = new GameConfiguration(new MarkupAdapter(teminal), FrameBuilderCollections.NaomiMarkup, new NetAF.Assets.Size(80, 30));

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

            GameExecutor.Execute(TroubleAboardTheSSHammerhead.Create(configuration, presentation, true));
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

        private void ShowNotification(string title, UIElement notification)
        {
            var window = new WindowControl()
            {
                Title = title,
                HostedContent = notification
            };

            window.Closed += (_,_) => ActiveNotification = null;

            ActiveNotification = window;
        }

        #endregion

        #region CommandCallbacks

        private void OpenSettingsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowNotification("Settings", new ApplicationSettingsControl());
        }

        private void OpenPersistenceCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (game == null)
                return;

            ShowNotification("Save/Load", new PersistenceControl(game));
        }

        private void FullScreenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
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

        private void WindowControl_Closed(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}