using NetAF.Assets;
using NetAF.Commands;
using NetAF.Logic;
using SSHammerhead.Logic.Modes;

namespace SSHammerhead.Commands.Scanner
{
    /// <summary>
    /// Represents the Scan command.
    /// </summary>
    /// <param name="examinable">The scanned object.</param>
    internal sealed class Scan(IExaminable examinable) : ICommand
    {
        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => new CommandHelp(string.Empty, string.Empty);

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (game.Mode is ScannerMode scannerMode)
            {
                scannerMode.Targets = Assets.Regions.Ship.Items.Scanner.GetScannableExaminables(game);

                if (examinable != null)
                    scannerMode.Composition = Assets.Regions.Ship.Items.Scanner.Scan(examinable);

                return new Reaction(ReactionResult.Silent, string.Empty);
            }
            else
            {
                scannerMode = new ScannerMode() { Targets = Assets.Regions.Ship.Items.Scanner.GetScannableExaminables(game) };

                if (examinable != null)
                    scannerMode.Composition = Assets.Regions.Ship.Items.Scanner.Scan(examinable);

                game.ChangeMode(scannerMode);

                return new Reaction(ReactionResult.GameModeChanged, string.Empty);
            }
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}
