using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Commands.Persistence;
using NetAF.Logic.Modes;
using NetAF.Rendering.Console.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Management;

namespace SSHammerhead.Assets.Players.SpiderBot
{
    public class SpiderBotTemplate : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static AnsiColor DisplayColor { get; } = new AnsiColor(100, 255, 100);
        public static Identifier Identifier => new(Name);
        private const string Name = "Bot";
        private const string Description = "A first generation spider bot, main purpose low scale maintenance operations.";

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            return new PlayableCharacter(Name, Description, false, false, commands:
            [
                new CustomCommand(new CommandHelp("Abort", $"Abort remote control"), true, true, (game, arguments) =>
                {
                    return PlayableCharacterManager.Switch(Naomi.NaomiTemplate.Identifier, game);
                }),
                new CustomCommand(new CommandHelp("Scan", "Scan the environment."), true, true, (g, _) =>
                {
                    var builder = new BotMaintenanceTunnelViewFrameBuilder(new GridStringBuilder());
                    g.ChangeMode(new VisualMode(builder.Build(g.Overworld.CurrentRegion.CurrentRoom, g.Configuration.DisplaySize)));
                    return new Reaction(ReactionResult.GameModeChanged, string.Empty);
                }),
                new Save() { IsPlayerVisible = false },
                new LoadWithRestore() { IsPlayerVisible = false }
            ]);
        }

        #endregion
    }
}
