using NetAF.Assets.Locations;
using NetAF.Logic;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.Naomi;
using SSHammerhead.Assets.Players.SpiderBot;
using SSHammerhead.Assets.Regions.Core;
using SSHammerhead.Assets.Regions.Core.Rooms.L0;
using SSHammerhead.Assets.Regions.MaintenanceTunnels;
using SSHammerhead.Assets.Regions.MaintenanceTunnels.L0;

namespace SSHammerhead
{
    internal static class TroubleAboardTheSSHammerHead
    {
        #region Constants

        private const string Title = "Trouble aboard the SS HammerHead";

        private const string Introduction = "After years of absence, the SS Hammerhead reappeared in the delta quadrant of the CTY-1 solar system.\n\n"
                                            + "A ship was hurriedly prepared and scrambled and made contact 27 days later.\n\n"
                                            + "You enter the outer most airlock and it closes behind you. "
                                            + "With a sense of foreboding you see your ship detach from the airlock and retreat to a safe distance.";

        private const string Description = "This is a short demo game using NetAF.";

        #endregion

        #region StaticMethods

        private static EndCheckResult CheckForGameOver(Game game)
        {
            if (!game.Player.IsAlive)
                return new EndCheckResult(true, "You are dead.", "You have succumb to the horrors of space.");

            return EndCheckResult.NotEnded;
        }

        internal static GameCreationCallback Create()
        {
            static Overworld overworldCreator()
            {
                var overworldName = "CTY-1 Galaxy";
                var ship = new SSHammerHead().Instantiate();
                var maintenanceTunnels = new MaintenanceTunnels().Instantiate();
                var overworld = new Overworld(overworldName, "A solar system in deep space, part of the SR389 galaxy.");
                overworld.AddRegion(ship);
                overworld.AddRegion(maintenanceTunnels);
                return overworld;
            }

            static void setup(Game g)
            {
                // get start positions
                g.Overworld.FindRegion(SSHammerHead.Name, out var sshh);
                g.Overworld.FindRegion(MaintenanceTunnels.Name, out var tunnels);
                sshh.TryFindRoom(Airlock.Name, out var naomiStart);
                tunnels.TryFindRoom(MaintenanceTunnelA.Name, out var botStart);

                // setup players
                PlayableCharacterManager.Add(new PlayableCharacterRecord(g.Player, sshh, naomiStart, NaomiTemplate.FrameBuilderCollection, NaomiTemplate.ErrorPrefix));
                PlayableCharacterManager.Add(new PlayableCharacterRecord(new SpiderBotTemplate().Instantiate(), tunnels, botStart, SpiderBotTemplate.FrameBuilderCollection, SpiderBotTemplate.ErrorPrefix));

                // setup for current player
                PlayableCharacterManager.ApplyConfiguration(g.Player, g);
            }

            return Game.Create(
                new GameInfo(Title, Description, "Ben Pollard"),
                Introduction,
                AssetGenerator.Custom(overworldCreator, () => new NaomiTemplate().Instantiate()),
                new GameEndConditions(GameEndConditions.NotEnded, CheckForGameOver),
                GameConfiguration.Default,
                setup);

        }

        #endregion
    }
}
