using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Logic;
using SSHammerhead.Assets.Regions.Ship.Items;
using System.Linq;

namespace SSHammerhead.Commands.MaintenancePanel
{
    /// <summary>
    /// Represents the Radio change command.
    /// </summary>
    /// <param name="album">The name of the album to change to.</param>
    internal sealed class Change(string album) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new CommandHelp("Change", "Change casette.", CommandCategory.Custom);

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help => CommandHelp;

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (string.IsNullOrEmpty(album))
                return new(ReactionResult.Error, "Specify a casette by album name.");

            var available = Radio.AvailableCasettes(game);
            var casette = available.FirstOrDefault(x => x.Info.Album.InsensitiveEquals(album));

            if (casette == null)
                return new(ReactionResult.Error, $"{album} is not a known casette.");

            Radio.ChangeCasette(game, casette);
            return new(ReactionResult.Inform, $"Casette changed to {casette.Info.Album} by {casette.Info.Artist}.");
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            var playing = Radio.GetCurrentlyLoadedCasette(game);
            var available = Radio.AvailableCasettes(game);
            return [.. available.Where(x => x != playing).Select(x => new Prompt(x.Info.Album))];
        }

        #endregion
    }
}
