﻿using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Naomi;

namespace SSHammerhead.Assets.Players.Anne
{
    public class AnneTemplate : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static Identifier Identifier => new(Name);
        public const string Name = "Anne";
        private const string Description = $"You are still {NaomiTemplate.Name}, but in this simulation you look like {Name}, a 57 year old engine mechanic.";

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            return new PlayableCharacter(Name, Description);
        }

        #endregion
    }
}
