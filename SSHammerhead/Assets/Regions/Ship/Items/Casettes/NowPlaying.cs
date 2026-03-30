namespace SSHammerhead.Assets.Regions.Ship.Items.Casettes
{
    /// <summary>
    /// Provides information about a song. 
    /// </summary>
    /// <param name="Artist">The artist.</param>
    /// <param name="Album">The album.</param>
    /// <param name="Song">The song.</param>
    public record NowPlaying(string Artist, string Album, string Song)
    {
        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Artist}, {Album}: {Song}";
        }

        #endregion
    }
}
