using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Players.Anne
{
    public class AnneTemplate : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static Identifier Identifier => new(Name);
        private const string Name = "Anne";
        private const string Description = "You are still Naomi, but in this simulation you look like Anne, a 57 year old engine mechanic.";

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            return new PlayableCharacter(Name, Description);
        }

        #endregion
    }
}
