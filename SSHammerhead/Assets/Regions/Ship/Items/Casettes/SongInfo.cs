using System;

namespace SSHammerhead.Assets.Regions.Ship.Items.Casettes
{
    /// <summary>
    /// Provides information about a song.
    /// </summary>
    /// <param name="Name">The name of the song.</param>
    /// <param name="Duration">The songs duration.</param>
    public record SongInfo(string Name, TimeSpan Duration);
}
