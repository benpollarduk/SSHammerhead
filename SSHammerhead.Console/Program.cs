using NetAF.Logic;
using NetAF.Logic.Configuration;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console;
using SSHammerhead;

try
{
    Game.Execute(TroubleAboardTheSSHammerHead.Create(new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50))));
}
catch (Exception e)
{
    Console.WriteLine($"Exception caught running game: {e.Message}");
    Console.ReadKey();
}