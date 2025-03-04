try
{
    Game.Execute(TroubleAboardTheSSHammerHead.Create(ConsoleGameConfiguration.Default));
}
catch (Exception e)
{
    Console.WriteLine($"Exception caught running game: {e.Message}");
    Console.ReadKey();
}