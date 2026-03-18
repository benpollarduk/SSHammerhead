using NetAF.Logic;
using NetAF.Persistence;
using NetAF.Persistence.Json;

namespace SSHammerhead.WPF
{
    /// <summary>
    /// Provides auto-save functionality.
    /// </summary>
    internal static class AutoSave
    {
        #region Constants

        /// <summary>
        /// Get the name of autosave.
        /// </summary>
        public const string Name = "Auto";

        /// <summary>
        /// Get the path to the autosave.
        /// </summary>
        public static string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NetAF", TroubleAboardTheSSHammerhead.Title, $"{Name}.netaf");

        #endregion

        #region StaticMethods

        /// <summary>
        /// Autosave a game.
        /// </summary>
        /// <param name="game">The game to autosave.</param>
        /// <param name="message">A message detailing the result of the save, if the save was unsuccessful. If the save was successful this will be empty.</param>
        /// <returns>True if the save was successful else false.</returns>
        public static bool Save(Game? game, out string message)
        {
            if (game == null)
            {
                message = "No game to autosave.";
                return false;
            }

            var newRestorePoint = RestorePoint.Create(Name, game);

            return JsonSave.ToFile(Path, newRestorePoint, out message);
        }

        /// <summary>
        /// Try and load the autosave.
        /// </summary>
        /// <param name="restorePoint">The loaded restore point.</param>
        /// <param name="message">A message detailing the result of the load, if the load was unsuccessful. If the load was successful this will be empty.</param>
        /// <returns>True if the load was successful else false.</returns>
        public static bool TryLoad(out RestorePoint? restorePoint, out string message)
        {
            if (!System.IO.File.Exists(Path))
            {
                restorePoint = null;
                message = "No autosave found.";
                return false;
            }

            return JsonSave.FromFile(Path, out restorePoint, out message);
        }

        /// <summary>
        /// Determine if there is a valid autosave file.
        /// </summary>
        /// <returns>True if there is a valid autosave file, esle false.</returns>
        public static bool HasValidFile()
        {
            return TryLoad(out var restorePoint, out _) && restorePoint != null;
        }

        /// <summary>
        /// Try and apply the autosave.
        /// </summary>
        /// <param name="game">The game to apply the autosave to.</param>
        /// <param name="message">A message detailing the result of the load, if the load was unsuccessful. If the load was successful this will be empty.</param>
        /// <returns>True if the load was successful else false.</returns>
        public static bool Apply(Game game, out string message)
        {
            // try and start from autosave
            if (!TryLoad(out var restorePoint, out message) || restorePoint == null)
                return false;

            game.RestoreFrom(restorePoint.Game);

            GameExecutor.Update();

            return true;
        }

        #endregion
    }
}
