using System;
using NetAF.Logic;

namespace SSHammerhead
{
    internal static class Program
    {
        private static void Main(string[] _)
        {
            try
            {
                Game.Execute(TroubleAboardTheSSHammerHead.Create());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught running game: {e.Message}");
                Console.ReadKey();
            }
        }
    }
}
