using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.SSHammerhead.Assets.Players;
using NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Items;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Players
{
    public class Naomi : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static Identifier Identifier => new Identifier(Name);
        private const string Name = "Naomi";
        private const string Description = "You, Naomi Watts, are a 45 year old shuttle mechanic.";

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            var player = new PlayableCharacter(Name, Description, new Hammer().Instantiate(), new Mirror().Instantiate())
            {
                Interaction = (i) =>
                {
                    if (i == null)
                        return new InteractionResult(InteractionEffect.NoEffect, null);

                    if (Hammer.Name.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.FatalEffect, i, "You swing wildly at your own head. The first few blows connect and knock you down. You are dead.");

                    if (Mirror.Name.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, i, "Peering in to the mirror you can see yourself looking back through your helmets visor.");

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                },
                Commands =
                [
                    new CustomCommand(new CommandHelp("Bot", "Switch to the bot."), true, (game, arguments) =>
                    {
                        return PlayableCharacterManager.Switch(SpiderBot.Identifier, game);
                    })
                ]
            };

            return player;
        }

        #endregion
    }
}
