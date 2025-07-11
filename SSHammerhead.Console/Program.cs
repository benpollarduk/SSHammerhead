using NetAF.Logic;
using NetAF.Targets.Console;
using SSHammerhead;
using SSHammerhead.Configuration;
using SSHammerhead.Console;

try
{
    var presentation = new Presentation(
        FrameBuilderCollections.Naomi, 
        FrameBuilderCollections.Bot, 
        FrameBuilderCollections.Anne, 
        FrameBuilderCollections.Alex, 
        FrameBuilderCollections.Marina, 
        FrameBuilderCollections.Scott,
        FrameBuilderCollections.Zhiying);
    GameExecutor.Execute(TroubleAboardTheSSHammerhead.Create(new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Naomi, new(80, 50)), presentation), new ConsoleExecutionController());
}
catch (Exception e)
{
    Console.WriteLine($"Exception caught running game: {e.Message}");
    Console.ReadKey();
}