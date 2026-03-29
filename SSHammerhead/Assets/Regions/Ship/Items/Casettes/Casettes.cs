using System;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Ship.Items.Casettes
{
    /// <summary>
    /// Provides the available casettes.
    /// </summary>
    public static class Casettes
    {
        /// <summary>
        /// Get the casette info for Martyn and Ben.
        /// </summary>
        public static CasetteInfo MartynAndBen = new CasetteInfo("Martyn and Ben", "Demos", new Queue<SongInfo>([new SongInfo("Time Has Come", TimeSpan.FromSeconds(226)), new SongInfo("The Last Of Us", TimeSpan.FromSeconds(246))])); 
    }
}
