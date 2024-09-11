using System;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.SSHammerHead.Assets.Players;

namespace BP.AdventureFramework.SSHammerHead
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                OverworldCreationCallback overworldCreator = () =>
                {
                    var overworldName = "CTY-1 Galaxy";
                    var ship = new Assets.Regions.SSHammerHead.SSHammerHead().Instantiate();
                    var overworld = new Overworld(overworldName, "A solar system in deep space, part of the SR389 galaxy.");
                    overworld.AddRegion(ship);
                    return overworld;
                };

                var creator = Game.Create("Trouble aboard the SS HammerHead",
                    "After years of absence, the SS Hammerhead reappeared in the delta quadrant of the CTY-1 solar system.\n\nA ship was hurriedly prepared and scrambled and made contact 27 days later.\n\nYou enter the outer most airlock and it closes behind you. With a sense of foreboding you see your ship detach from the airlock and retreat to a safe distance.",
                    "This is a short demo game using the BP.AdventureFramework.",
                    overworldCreator,
                    new Player().Instantiate,
                    g => EndCheckResult.NotEnded,
                    g => EndCheckResult.NotEnded);

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
