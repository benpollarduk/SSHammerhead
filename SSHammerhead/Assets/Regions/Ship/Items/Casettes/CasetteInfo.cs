namespace SSHammerhead.Assets.Regions.Ship.Items.Casettes
{
    /// <summary>
    /// Provides information about a casette.
    /// </summary>
    /// <param name="Artist">The artist.</param>
    /// <param name="Album">The album.</param>
    /// <param name="Songs">The songs.</param>
    public record CasetteInfo(string Artist, string Album, SongInfo[] Songs);
}
