using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;
using NetAF.Commands.Persistence;
using NetAF.Commands;

namespace SSHammerhead.Assets.Players.Naomi
{
    public class NaomiTemplate : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static Identifier Identifier => new(Name);
        private const string Name = "Naomi";
        private const string Description = "You, Naomi Martin, are a 32-year-old shuttle mechanic.";

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            CustomCommand[] commands =
            [
                new Save() { IsPlayerVisible = false },
                new LoadWithRestore() { IsPlayerVisible = false }
            ];

            return new(Name, Description, [new Hammer().Instantiate()], commands: commands, interaction: (i) =>
            {
                if (i == null)
                    return new Interaction(InteractionResult.NoChange, null);

                if (Hammer.Name.EqualsIdentifier(i.Identifier))
                    return new Interaction(InteractionResult.TargetExpires, i, "You swing wildly at your own head. The first few blows connect and knock you down. You are dead.");

                return new Interaction(InteractionResult.NoChange, i);
            });
        }

        #endregion
    }
}
