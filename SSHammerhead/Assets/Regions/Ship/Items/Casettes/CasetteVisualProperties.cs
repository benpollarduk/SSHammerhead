using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Assets.Regions.Ship.Items.Casettes
{
    /// <summary>
    /// Provides visual properties for a casette.
    /// </summary>
    /// <param name="Background">The background color.</param>
    /// <param name="Body">The body color.</param>
    /// <param name="BodyBorder">The body border color.</param>
    /// <param name="Tape">The tape color.</param>
    /// <param name="Window">The window color.</param>
    /// <param name="WindowBorder">The window border color.</param>
    /// <param name="LabelText">The label text.</param>
    /// <param name="Label">The label color.</param>
    /// <param name="LabelForeground">The label foreground color.</param>
    /// <param name="Spool">The spool color.</param>
    /// <param name="SpoolForeground">The spool foreground color.</param>
    /// <param name="ScrewCharacter">The screw character.</param>
    /// <param name="Screw">The screw background color.</param>
    /// <param name="ScrewForeground">The screw foreground color.</param>
    public record CasetteVisualProperties
    (
        AnsiColor Background,
        AnsiColor Body,
        AnsiColor BodyBorder,
        AnsiColor Tape,
        AnsiColor Window,
        AnsiColor WindowBorder,
        string LabelText,
        AnsiColor Label,
        AnsiColor LabelForeground,
        AnsiColor Spool,
        AnsiColor SpoolForeground,
        char ScrewCharacter,
        AnsiColor Screw,
        AnsiColor ScrewForeground
    );
}
