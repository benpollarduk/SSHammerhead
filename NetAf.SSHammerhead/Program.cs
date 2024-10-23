using System;
using NetAF.Logic;
using NetAF.SSHammerhead;

namespace NetAF.SSHammerHead
{
    internal static class Program
    {
        private static void Main(string[] args)
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
