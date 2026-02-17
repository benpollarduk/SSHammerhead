using NetAF.Interpretation;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Targets.Markup;
using SSHammerhead.Configuration;
using System.Windows;

namespace SSHammerhead.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

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

        #endregion
    }
}