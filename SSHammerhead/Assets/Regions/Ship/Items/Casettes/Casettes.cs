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
            "DEMOS",
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
        public static Casette MartynAndBen = new Casette(new CasetteInfo("Martyn and Ben", "Demos", MartynAndBenSongs), MartynAndBenVisualProperties, "Resources/Audio/Music/demos.mp3");

        private static CasetteVisualProperties DemonsVisualProperties => new CasetteVisualProperties
        (
            AnsiColor.Black,
            AnsiColor.BrightBlue,
            AnsiColor.BrightBlue,
            AnsiColor.Red,
            AnsiColor.White,
            AnsiColor.Red,
            "DEMONS",
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
            new SongInfo("Only Lovers", TimeSpan.FromSeconds(239)),
            new SongInfo("Thinking Of You", TimeSpan.FromSeconds(226)), //3.52
            new SongInfo("Wrong Things", TimeSpan.FromSeconds(226)), //2.50
            new SongInfo("I Shouldn't Be With You", TimeSpan.FromSeconds(226)), //4.47
            new SongInfo("Lose It", TimeSpan.FromSeconds(246)) //3.17
        ];

        /// <summary>
        /// Get the casette info for Demons.
        /// </summary>
        public static Casette Demons = new Casette(new CasetteInfo("ThisFire", "Demons", DemonsSongs), DemonsVisualProperties, "Resources/Audio/Music/demons.mp3");
    }
}
