using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Logic;
using NetAF.Rendering.FrameBuilders;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.Naomi;
using SSHammerhead.Assets.Players.SpiderBot;
using SSHammerhead.Assets.Regions.Core;
using SSHammerhead.Assets.Regions.Core.Items;
using SSHammerhead.Assets.Regions.Core.Rooms.L0;
using SSHammerhead.Assets.Regions.Core.Rooms.L2;
using SSHammerhead.Assets.Regions.MaintenanceTunnels;
using SSHammerhead.Assets.Regions.MaintenanceTunnels.L0;
using SSHammerhead.Assets.Regions.Stasis.SaucepanLand;
using System.Linq;

namespace SSHammerhead
{
    public static class TroubleAboardTheSSHammerHead
    {
        #region Constants

        private const string Title = "Trouble Aboard the SS HammerHead";

        private const string Introduction = "After years of absence, the SS Hammerhead reappeared in the delta quadrant of the CTY-1 solar system.\n\n" +
            "A ship was hurriedly prepared and scrambled and made contact 27 days later.\n\n" +
            "What secrets does the SS Hammerhead hold?";

        private const string Description = "This is a short demo game using NetAF.";

        #endregion

        #region StaticMethods

        private static EndCheckResult CheckForGameOver(Game game)
        {
            if (!game.Player.IsAlive)
                return new EndCheckResult(true, "You are dead.", "You have succumbed to the horrors of space.");

            return EndCheckResult.NotEnded;
        }

        private static EndCheckResult CheckForCompletion(Game game)
        {
            if (game.Overworld.CurrentRegion.CurrentRoom.Identifier.Equals(Bridge.Name))
                return new EndCheckResult(true, "Ending reached.", "You have reached the end of the game! More to be developed soon.");

            return EndCheckResult.NotEnded;
        }

        public static GameCreator Create(GameConfiguration configuration, FrameBuilderCollection naomiFrameBuilders, FrameBuilderCollection botFrameBuilders)
        {
            static Overworld overworldCreator()
            {
                var overworldName = "CTY-1 Galaxy";
                var ship = new SSHammerHead().Instantiate();
                var maintenanceTunnels = new MaintenanceTunnels().Instantiate();
                var saucepanLand = new SaucepanLand().Instantiate();

                CustomCommand[] commands =
                [
                    new CustomCommand(new CommandHelp("dev-n", "Switch Naomi"), false, true, (game, arguments) =>
                    {
                        return PlayableCharacterManager.Switch(NaomiTemplate.Identifier, game);
                    }),
                    new CustomCommand(new CommandHelp("dev-b", "Switch Spider Bot"), false, true, (game, arguments) =>
                    {
                        return PlayableCharacterManager.Switch(SpiderBotTemplate.Identifier, game);
                    }),
                    new CustomCommand(new CommandHelp("dev-s+", "Increase sanity by 1"), false, true, (game, arguments) =>
                    {
                        game.Player.Attributes.Add(NaomiTemplate.SanityAttributeName, 1);
                        return new Reaction(ReactionResult.Inform, $"Sanity is now {game.Player.Attributes.GetValue(NaomiTemplate.SanityAttributeName)}");
                    }),
                    new CustomCommand(new CommandHelp("dev-s-", "Decrease sanity by 1"), false, true, (game, arguments) =>
                    {
                        game.Player.Attributes.Subtract(NaomiTemplate.SanityAttributeName, 1);
                        return new Reaction(ReactionResult.Inform, $"Sanity is now {game.Player.Attributes.GetValue(NaomiTemplate.SanityAttributeName)}");
                    }),
                    new CustomCommand(new CommandHelp("dev-sp", "Jump to Saucepan Land"), false, true, (game, arguments) =>
                    {
                        var reaction = PlayableCharacterManager.Switch(NaomiTemplate.Identifier, game);

                        if (reaction.Result == ReactionResult.Error)
                            return reaction;

                        game.Overworld.Move(saucepanLand);
                        return new Reaction(ReactionResult.Silent, $"Jumped to {SaucepanLand.Name}.");
                    }),
                ];

                var overworld = new Overworld(overworldName, "A solar system in deep space, part of the SR389 galaxy.", commands: commands);
                overworld.AddRegion(ship);
                overworld.AddRegion(maintenanceTunnels);
                overworld.AddRegion(saucepanLand);

                return overworld;
            }

            static void setup(Game g, FrameBuilderCollection naomiFrameBuilders, FrameBuilderCollection botFrameBuilders)
            {
                // get start positions
                g.Overworld.FindRegion(SSHammerHead.Name, out var sshh);
                g.Overworld.FindRegion(MaintenanceTunnels.Name, out var tunnels);
                sshh.TryFindRoom(Airlock.Name, out var naomiStart);
                tunnels.TryFindRoom(MaintenanceTunnelA.Name, out var botStart);

                // get bot instance
                var bot = new SpiderBotTemplate().Instantiate();

                // clear previous
                PlayableCharacterManager.Clear();

                // setup players
                PlayableCharacterManager.Add(new PlayableCharacterRecord(g.Player, sshh, naomiStart, naomiFrameBuilders));
                PlayableCharacterManager.Add(new PlayableCharacterRecord(bot, tunnels, botStart, botFrameBuilders));

                // setup for current player
                PlayableCharacterManager.ApplyConfiguration(g.Player, g);

                // register any items that aren't present in creation
                g.Catalog.Register(new USBDrive());
                g.Catalog.Register(bot);
            }

            return Game.Create(
                new GameInfo(Title, Description, "Ben Pollard"),
                Introduction,
                AssetGenerator.Custom(overworldCreator, () => new NaomiTemplate().Instantiate()),
                new GameEndConditions(CheckForCompletion, CheckForGameOver),
                configuration,
                g => setup(g, naomiFrameBuilders, botFrameBuilders));

        }

        #endregion
    }
}
