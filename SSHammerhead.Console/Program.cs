using NetAF.Events;
using NetAF.Interpretation;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Targets.Console;
using SSHammerhead;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Audio;
using SSHammerhead.Configuration;
using SSHammerhead.Console;

try
{
    Services.ImageProvider = new ConsoleImageProvider();

    EventBus.Subscribe<GameStarted>(x => AudioPlayer.StartBackgroundMusic(1, Radio.DetermineProximity(x.Game)));
    EventBus.Subscribe<GameFinished>(_ => AudioPlayer.StopBackgroundMusic());
    EventBus.Subscribe<GameUpdated>(x => AudioPlayer.AdjustBackgroundMusic(1, Radio.DetermineProximity(x.Game)));

    var presentation = new Presentation
    (
        FrameBuilderCollections.Naomi, 
        FrameBuilderCollections.Bot, 
        FrameBuilderCollections.Anne, 
        FrameBuilderCollections.Alex, 
        FrameBuilderCollections.Marina, 
        FrameBuilderCollections.Scott,
        FrameBuilderCollections.Zhiying
    );
    
    var configuration = new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Naomi, NetAF.Assets.Size.Dynamic);

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

    GameExecutor.Execute(TroubleAboardTheSSHammerhead.Create(configuration, presentation, true), new ConsoleExecutionController());
}
catch (Exception e)
{
    Console.WriteLine($"Exception caught running game: {e.Message}");
    Console.ReadKey();
}