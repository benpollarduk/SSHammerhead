using NetAF.Logic;
using NetAF.Logic.Configuration;
using NetAF.Targets.Console;
using SSHammerhead;
using SSHammerhead.Console;

try
{
    Game.Execute(TroubleAboardTheSSHammerHead.Create(new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Naomi, new(80, 50)), FrameBuilderCollections.Naomi, FrameBuilderCollections.Bot));
}
catch (Exception e)
{
    Console.WriteLine($"Exception caught running game: {e.Message}");
    Console.ReadKey();
}