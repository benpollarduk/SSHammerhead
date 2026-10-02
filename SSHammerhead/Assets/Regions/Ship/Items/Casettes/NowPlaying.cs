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

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Artist}, {Album}: {Song}";
        }

        #endregion
    }
}
