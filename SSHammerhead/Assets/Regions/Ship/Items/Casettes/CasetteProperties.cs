using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Assets.Regions.Ship.Items.Casettes
{
    internal record CasetteProperties
    (
        AnsiColor Background,
        AnsiColor Body,
        AnsiColor BodyBorder,
        AnsiColor Tape,
        AnsiColor Window,
        AnsiColor WindowBorder,
        string Label,
        AnsiColor LabelBackground,
        AnsiColor LabelForeground,
        AnsiColor SpoolBackground,
        AnsiColor SpoolForeground,
        char ScrewCharacter,
        AnsiColor ScrewCharacterBackground,
        AnsiColor ScrewCharacterForeground
    )
    {
        internal static CasetteProperties Default => new CasetteProperties
        (
            AnsiColor.Black, 
            AnsiColor.White, 
            AnsiColor.BrightBlack, 
            AnsiColor.Red, 
            AnsiColor.Black, 
            AnsiColor.BrightBlack, 
            "CASETTE",
            AnsiColor.BrightBlack, 
            AnsiColor.White, 
            AnsiColor.Black,
            AnsiColor.White, 
            'o', 
            AnsiColor.White, 
            AnsiColor.BrightBlack
        );

        internal static CasetteProperties ThisFire => new CasetteProperties
        (
            AnsiColor.Black,
            AnsiColor.BrightBlue,
            AnsiColor.Red,
            AnsiColor.Red,
            AnsiColor.White,
            AnsiColor.Red,
            "THIS FIRE",
            AnsiColor.BrightBlack,
            AnsiColor.White,
            AnsiColor.Black,
            AnsiColor.White,
            '*',
            AnsiColor.BrightBlue,
            AnsiColor.Red
        );
    }
}
