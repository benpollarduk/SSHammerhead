using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Naomi;

namespace SSHammerhead.Assets.Players.Scott
{
    public class ScottTemplate : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static Identifier Identifier => new(Name);
        public const string Name = "Scott";
        private const string Description = $"You are still {NaomiTemplate.Name}, but in this simulation you look like {Name}.";

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            return new PlayableCharacter(Name, Description);
        }

        #endregion
    }
}
