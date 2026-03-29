using System;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Ship.Items.Casettes
{
    /// <summary>
    /// Provides information about a casette.
    /// </summary>
    /// <param name="Artist">The artist.</param>
    /// <param name="Album">The album.</param>
    /// <param name="Songs">The songs.</param>
    public record CasetteInfo(string Artist, string Album, Queue<SongInfo> Songs)
    {
        /// <summary>
        /// Get the song at a specified time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>The song at the specified time.</returns>
        public SongInfo GetSongAtTime(TimeSpan time)
        {
            var songs = Songs.ToArray();
            var total = TimeSpan.Zero;

            for (var i = 0; i < songs.Length; i++)
            {
                if (time < songs[i].Duration)
                    return songs[i];

                total += songs[i].Duration;
            }

            return songs[^1];
        }
    }
}
