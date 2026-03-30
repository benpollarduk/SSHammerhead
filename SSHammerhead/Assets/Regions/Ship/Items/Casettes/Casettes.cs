using NetAF.Targets.Console.Rendering;
using System;

namespace SSHammerhead.Assets.Regions.Ship.Items.Casettes
{
    /// <summary>
    /// Provides the available casettes.
    /// </summary>
    public static class Casettes
    {
        private static CasetteVisualProperties MartynAndBenVisualProperties => new CasetteVisualProperties
        (
            AnsiColor.Black,
            AnsiColor.White,
            AnsiColor.White,
            AnsiColor.Red,
            AnsiColor.BrightBlack,
            AnsiColor.Black,
            "CASETTE",
            AnsiColor.BrightBlack,
            AnsiColor.White,
            AnsiColor.Black,
            AnsiColor.White,
            'o',
            AnsiColor.White,
            AnsiColor.BrightBlack
        );

        private static SongInfo[] MartynAndBenSongs =
        [
            new SongInfo("Time Has Come", TimeSpan.FromSeconds(226)),
            new SongInfo("The Last Of Us", TimeSpan.FromSeconds(246))
        ];

        /// <summary>
        /// Get the casette info for Martyn and Ben.
        /// </summary>
        public static Casette MartynAndBen = new Casette(new CasetteInfo("Martyn and Ben", "Demos", MartynAndBenSongs), MartynAndBenVisualProperties);

        private static CasetteVisualProperties DemonsVisualProperties => new CasetteVisualProperties
        (
            AnsiColor.Black,
            AnsiColor.BrightBlue,
            AnsiColor.BrightBlue,
            AnsiColor.Red,
            AnsiColor.White,
            AnsiColor.Red,
            "THIS FIRE",
            AnsiColor.White,
            AnsiColor.White,
            AnsiColor.Black,
            AnsiColor.White,
            '*',
            AnsiColor.BrightBlue,
            AnsiColor.Red
        );

        private static SongInfo[] DemonsSongs =
        [
            new SongInfo("Time Has Come", TimeSpan.FromSeconds(226)),
            new SongInfo("The Last Of Us", TimeSpan.FromSeconds(246))
        ];

        /// <summary>
        /// Get the casette info for Demons.
        /// </summary>
        public static Casette Demons = new Casette(new CasetteInfo("ThisFire", "Demons", DemonsSongs), DemonsVisualProperties);
    }
}
