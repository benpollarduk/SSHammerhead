using NetAF.Logic;
using NetAF.Targets.Console;
using SSHammerhead;
using SSHammerhead.Console;

try
{
    GameExecutor.Execute(TroubleAboardTheSSHammerHead.Create(new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Naomi, new(80, 50)), FrameBuilderCollections.Naomi, FrameBuilderCollections.Bot), new ConsoleExecutionController());
}
catch (Exception e)
{
    Console.WriteLine($"Exception caught running game: {e.Message}");
    Console.ReadKey();
}