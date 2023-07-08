using System;
using System.Reflection;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.SSHammerHead.Assets.Players;
using BP.AdventureFramework.SSHammerHead.TextManagement;

namespace BP.AdventureFramework.SSHammerHead
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // buffer all text
                Lookup.Text.BufferJsonResource(Assembly.GetExecutingAssembly(), "BP.AdventureFramework.SSHammerhead.strings.json");

                OverworldCreationCallback overworldCreator = p =>
                {
                    var overworldName = "CTY-1 Galaxy";
                    var ship = Assets.Regions.SSHammerHead.SSHammerHead.Create(p);
                    var overworld = new Overworld(overworldName, Lookup.Text[overworldName]);
                    overworld.AddRegion(ship);
                    return overworld;
                };

                var creator = Game.Create(Lookup.Text["Title"],
                    Lookup.Text["Introduction"],
                    Lookup.Text["About"],
                    x => overworldCreator(x),
                    Player.Create,
                    g => CompletionCheckResult.NotComplete);

                Game.Execute(creator);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught running game: {e.Message}");
                Console.ReadKey();
            }
        }
    }
}
