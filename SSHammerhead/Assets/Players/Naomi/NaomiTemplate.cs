using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Extensions;
using NetAF.Utilities;
using NetAF.Commands.Persistence;
using NetAF.Commands;
using NetAF.Assets.Attributes;
using SSHammerhead.Commands.Persist;
using SSHammerhead.Assets.Regions.Ship.Items;

namespace SSHammerhead.Assets.Players.Naomi
{
    public class NaomiTemplate : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static Identifier Identifier => new(Name);
        private const string Name = "Naomi";
        private const string Description = "You, Naomi Martin, are a 32-year-old shuttle mechanic.";
        public const string SanityAttributeName = "Sanity";

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            CustomCommand[] commands =
            [
                new Save() { IsPlayerVisible = false },
                new LoadWithRestore() { IsPlayerVisible = false }
            ];

            var naomi = new PlayableCharacter(Name, Description, [new Hammer().Instantiate()], commands: commands, interaction: (i) =>
            {
                if (i == null)
                    return new Interaction(InteractionResult.NoChange, null);

                if (Hammer.Name.EqualsIdentifier(i.Identifier))
                    return new Interaction(InteractionResult.TargetExpires, i, "You swing wildly at your own head. The first few blows connect and knock you down. You are dead.");

                return new Interaction(InteractionResult.NoChange, i);
            });

            naomi.Attributes.Add(new Attribute(SanityAttributeName, "Sanity level.", 1, 5, false), 1);

            return naomi;
        }

        #endregion
    }
}
