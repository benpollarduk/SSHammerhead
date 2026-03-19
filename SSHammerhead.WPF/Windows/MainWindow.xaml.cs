using Microsoft.Extensions.Configuration;
using NetAF.Events;
using NetAF.Interpretation;
using NetAF.Logging.Notes;
using NetAF.Logic;
using NetAF.Logic.Callbacks;
using NetAF.Logic.Modes;
using NetAF.Targets.Markup;
using NetAF.Targets.WPF.Controls;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Audio;
using SSHammerhead.Configuration;
using SSHammerhead.WPF.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SSHammerhead.WPF.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private Game? game;
        private TextBox? promptTextbox;
        private bool firstRun = true;
        private Button? notesButton;

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

        /// <summary>
        /// Get the pending notes. 
        /// </summary>
        public ObservableCollection<NoteEntry> PendingNotes
        {
            get { return (ObservableCollection<NoteEntry>)GetValue(PendingNotesProperty); }
            private set { SetValue(PendingNotesProperty, value); }
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

        /// <summary>
        /// Identifies the MainWindow.v property.
        /// </summary>
        public static readonly DependencyProperty PendingNotesProperty = DependencyProperty.Register(nameof(PendingNotes), typeof(ObservableCollection<NoteEntry>), typeof(MainWindow), new PropertyMetadata(new ObservableCollection<NoteEntry>()));

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
            EventBus.Subscribe<NoteAdded>(x =>
            {
                Dispatcher.Invoke(() =>
                {
                    PendingNotes.Add(x.Note);

                    if (notesButton != null && FindResource("FlashButtonAnimationStoryboard") is System.Windows.Media.Animation.Storyboard resourceStoryboard)
                    {
                        var storyboard = resourceStoryboard.Clone();
                        System.Windows.Media.Animation.Storyboard.SetTarget(storyboard, notesButton);
                        storyboard.Begin();
                    }
                });
            });
            EventBus.Subscribe<RoomEntered>(x =>
            {
                if (App.Settings.AutoSave)
                    AutoSave.Save(game, out _);
            });

            App.Settings.FramePropertiesChanged += (_, _) => game?.Mode.Render(game);
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
            var hostedGrid = WindowControl?.HostedContent as Grid;
            var teminal = hostedGrid?.Children.OfType<NetAFMarkupTerminal>().FirstOrDefault();

            var sceneInterpreter = new InputInterpreter
            (
                new GlobalCommandInterpreter(),
                new ExecutionCommandInterpreter(),
                new CustomCommandInterpreter(),
                new SceneCommandInterpreter()
            );

            var adapter = new MarkupAdapter(teminal);
            var frameBuilders = FrameBuilderCollections.NaomiMarkup;
            var displaySize = new NetAF.Assets.Size(80, 30);
            var configuration = new GameConfiguration(adapter, frameBuilders, displaySize);

            // change configuration prevent using the normal persistence interpreter as this is handled by custom commands
            configuration.InterpreterProvider.Register(typeof(SceneMode), sceneInterpreter);

            // create additional setup to handle autosaves
            GameSetupCallback additionalSetup = g =>
            {
                // subsequent restarts should not autoload
                if (!firstRun)
                    return;

                // if autosave is enabled, attempt to load the autosave file and apply it to the game
                if (App.Settings.AutoSave)
                {
                    // if that worked ensure the start room is set
                    if (AutoSave.Apply(g, out _))
                        g.Overworld.CurrentRegion.SetStartRoom(g.Overworld.CurrentRegion.CurrentRoom);
                }

                // only the first time, additional restarts should begin at the very start
                firstRun = false;
            };

            GameExecutor.Execute(TroubleAboardTheSSHammerhead.Create(configuration, presentation, true, additionalSetup));
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

        private WindowControl ShowNotification(string title, UIElement notification)
        {
            var window = new WindowControl()
            {
                Title = title,
                HostedContent = notification,
                CornerRadius = (CornerRadius)FindResource("WindowCornerRadius")
            };

            window.Closed += (_,_) =>
            {
                ActiveNotification = null;
                FocusOnPromptTextBox();
            };

            ActiveNotification = window;
            return window;
        }

        private void FocusOnPromptTextBox()
        {
            if (promptTextbox == null)
                return;

            promptTextbox.Focus();
            Keyboard.Focus(promptTextbox);
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

            var persistenceControl = new PersistenceControl(game);

            // close on loaded
            persistenceControl.FileManager.RestorePointLoaded += (_, _) =>
            {
                ActiveNotification = null;
                FocusOnPromptTextBox();
            };

            ShowNotification("Save/Load", persistenceControl);
        }

        private void FullScreenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void OpenNoteManagerCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var noteManagerViewer = new NoteManagerViewer();
            noteManagerViewer.RecentEntries = PendingNotes;

            var notification = ShowNotification("Recent Notes", noteManagerViewer);
            notification.Closed += (_, _) => PendingNotes.Clear();
        }

        #endregion

        #region EventHandlers

        private void NotesButton_Loaded(object sender, RoutedEventArgs e)
        {
            notesButton = sender as Button;
        }

        private void NetAFPrompt_KeyPressed(object sender, Key e)
        {
            HandleInput(e);
        }

        private void ButtonLayout_ButtonSelected(object sender, EventArgs e)
        {
            HandleInput(null);
            FocusOnPromptTextBox();
        }

        private void WindowControl_Closed(object sender, EventArgs e)
        {
            Close();
        }

        private void NetAFPrompt_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is not NetAFPrompt prompt)
                return;

            if (prompt.Content is not TextBox textbox)
                return;

            promptTextbox = textbox;
            FocusOnPromptTextBox();
        }

        #endregion
    }
}