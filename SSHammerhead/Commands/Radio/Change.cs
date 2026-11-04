using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Logic;
using System.Linq;

namespace SSHammerhead.Commands.Radio
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

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (string.IsNullOrEmpty(album))
                return new(ReactionResult.Error, "Specify a casette by album name.");

            var available = Assets.Regions.Ship.Items.Radio.AvailableCasettes(game);
            var casette = available.FirstOrDefault(x => x.Info.Album.InsensitiveEquals(album));

            if (casette == null)
                return new(ReactionResult.Error, $"{album} is not a known casette.");

            Assets.Regions.Ship.Items.Radio.ChangeCasette(game, casette);
            return new(ReactionResult.Silent, $"Casette changed to {casette.Info.Album} by {casette.Info.Artist}.");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            var playing = Assets.Regions.Ship.Items.Radio.GetCurrentlyLoadedCasette(game);
            var available = Assets.Regions.Ship.Items.Radio.AvailableCasettes(game);
            return [.. available.Where(x => x != playing).Select(x => new Prompt(x.Info.Album))];
        }

        #endregion
    }
}
