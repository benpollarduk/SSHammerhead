using NetAF.Logic;
using NetAF.Targets.Console;
using SSHammerhead;
using SSHammerhead.Configuration;
using SSHammerhead.Console;

try
{
    GameExecutor.Execute(TroubleAboardTheSSHammerHead.Create(new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Naomi, new(80, 50)), new Presentation(FrameBuilderCollections.Naomi, FrameBuilderCollections.Bot, FrameBuilderCollections.Anne)), new ConsoleExecutionController());
}
catch (Exception e)
{
    Console.WriteLine($"Exception caught running game: {e.Message}");
    Console.ReadKey();
}