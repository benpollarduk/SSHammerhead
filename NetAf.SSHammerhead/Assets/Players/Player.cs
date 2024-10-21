using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Extensions;
using NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Items;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Players
{
    public class Player : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

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
                }
            };

            return player;
        }

        #endregion
    }
}
