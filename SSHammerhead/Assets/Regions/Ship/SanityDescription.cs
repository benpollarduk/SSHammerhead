using NetAF.Assets;
using NetAF.Logic;
using SSHammerhead.Assets.Players.Naomi;

namespace SSHammerhead.Assets.Regions.Ship
{
    /// <summary>
    /// Get a description based on the sanity level of the character in the executing game.
    /// </summary>
    /// <param name="descriptions">The descriptions, indexed by sanity level.</param>
    internal class SanityDescription(string[] descriptions) : IDescription
    {
        /// <summary>
        /// Get the description.
        /// </summary>
        /// <returns>The description.</returns>
        public string GetDescription()
        {
            if (descriptions == null)
                return "No descriptions.";

            var game = GameExecutor.ExecutingGame;

            if (game == null)
                return "No game.";

            var player = game.Player;

            if (player == null)
                return "No player.";

            if (!player.Identifier.Equals(NaomiTemplate.Name))
                return $"Not {NaomiTemplate.Name}.";

            var sanity = player.Attributes?.GetValue(NaomiTemplate.SanityAttributeName) ?? 0;

            if (sanity < 0 || sanity >= descriptions.Length)
                return $"No description provided for sanity level {sanity}.";

            return descriptions[sanity];
        }
    }
}
