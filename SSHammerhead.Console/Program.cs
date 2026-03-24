using NetAF.Events;
using NetAF.Logic;
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
    GameExecutor.Execute(TroubleAboardTheSSHammerhead.Create(configuration, presentation, true), new ConsoleExecutionController());
}
catch (Exception e)
{
    Console.WriteLine($"Exception caught running game: {e.Message}");
    Console.ReadKey();
}