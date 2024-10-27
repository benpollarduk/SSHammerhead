using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.SSHammerhead.Assets.Players;
using NetAF.SSHammerhead.Assets.Players.FrameBuilders;
using NetAF.SSHammerhead.Assets.Regions.MaintenanceTunnels;
using NetAF.SSHammerhead.Assets.Regions.MaintenanceTunnels.L0;
using NetAF.SSHammerHead.Assets.Players;
using NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L0;
using NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L2;
using System.Collections.Generic;

namespace NetAF.SSHammerhead
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
            OverworldCreationCallback overworldCreator = () =>
            {
                var overworldName = "CTY-1 Galaxy";
                var ship = new SSHammerHead.Assets.Regions.SSHammerHead.SSHammerHead().Instantiate();
                var maintenanceTunnels = new MaintenanceTunnels().Instantiate();
                var overworld = new Overworld(overworldName, "A solar system in deep space, part of the SR389 galaxy.");
                overworld.AddRegion(ship);
                overworld.AddRegion(maintenanceTunnels);
                return overworld;
            };

            GameSetupCallback setup = g =>
            {
                g.Overworld.FindRegion(SSHammerHead.Assets.Regions.SSHammerHead.SSHammerHead.Name, out var sshh);
                g.Overworld.FindRegion(MaintenanceTunnels.Name, out var tunnels);
                sshh.TryFindRoom(Airlock.Name, out var naomiStart);
                tunnels.TryFindRoom(MaintenanceTunnelA.Name, out var botStart);

                Dictionary<string, PlayableCharacterRecord> records = new()
                {
                    { Naomi.Identifier.IdentifiableName, new PlayableCharacterRecord(g.Player, sshh, naomiStart, FrameBuilderCollections.Naomi) },
                    { SpiderBot.Identifier.IdentifiableName, new PlayableCharacterRecord(new SpiderBot().Instantiate(), tunnels, botStart, FrameBuilderCollections.Bot) },
                };

                PlayableCharacterManager.Setup(records);
            };

            return Game.Create(
                new GameInfo(Title, Description, "Ben Pollard"), 
                Introduction, 
                AssetGenerator.Custom(overworldCreator, () => new Naomi().Instantiate()),
                new GameEndConditions(GameEndConditions.NotEnded, CheckForGameOver), 
                GameConfiguration.Default,
                setup);

        }

        #endregion
    }
}
