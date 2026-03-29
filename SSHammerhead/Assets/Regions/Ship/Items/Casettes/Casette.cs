using NetAF.Assets;
using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Assets.Regions.Ship.Items.Casettes
{
    internal static class Casette
    {
        internal static GridVisualBuilder GetTapeTemplate(CasetteProperties casetteProperties)
        {
            var visualBuilder = new GridVisualBuilder(casetteProperties.Background, casetteProperties.Background);
            visualBuilder.Resize(new Size(29, 15));

            var bodyVarier = new IntensityVariationGenerator(10, 10);
            var labelVarier = new IntensityVariationGenerator(5, 5);
            var windowVarier = new IntensityVariationGenerator(5, 5);

            // cassette body
            visualBuilder.DrawRectangle(0, 0, 29, 15, casetteProperties.BodyBorder, casetteProperties.Body, bodyVarier, bodyVarier);

            // label
            visualBuilder.DrawRectangle(2, 2, 25, 3, casetteProperties.LabelBackground, casetteProperties.LabelBackground, labelVarier, labelVarier);
            visualBuilder.DrawText(3, 3, casetteProperties.Label, casetteProperties.LabelForeground);

            // screws
            visualBuilder.SetCell(1, 1, casetteProperties.ScrewCharacter, casetteProperties.ScrewCharacterForeground, casetteProperties.ScrewCharacterBackground);
            visualBuilder.SetCell(27, 1, casetteProperties.ScrewCharacter, casetteProperties.ScrewCharacterForeground, casetteProperties.ScrewCharacterBackground);
            visualBuilder.SetCell(1, 13, casetteProperties.ScrewCharacter, casetteProperties.ScrewCharacterForeground, casetteProperties.ScrewCharacterBackground);
            visualBuilder.SetCell(27, 13, casetteProperties.ScrewCharacter, casetteProperties.ScrewCharacterForeground, casetteProperties.ScrewCharacterBackground);

            // window
            visualBuilder.DrawRectangle(2, 6, 25, 7, casetteProperties.WindowBorder, casetteProperties.Window, windowVarier, windowVarier);

            return visualBuilder;
        }

        internal static GridVisualBuilder AddDetails0(GridVisualBuilder visualBuilder, CasetteProperties casetteProperties)
        {
            // spools
            visualBuilder.DrawRectangle(8, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(7, 9, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(9, 9, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(8, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(20, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(19, 9, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(21, 9, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(20, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);

            // spool centers
            visualBuilder.SetCell(8, 9, '+', casetteProperties.SpoolForeground, casetteProperties.SpoolBackground);
            visualBuilder.SetCell(20, 9, '+', casetteProperties.SpoolForeground, casetteProperties.SpoolBackground);

            // tape
            visualBuilder.DrawRectangle(4, 9, 1, 1, casetteProperties.Tape, casetteProperties.Tape);
            visualBuilder.DrawRectangle(24, 9, 1, 1, casetteProperties.Tape, casetteProperties.Tape);

            return visualBuilder;
        }

        internal static GridVisualBuilder AddDetails1(GridVisualBuilder visualBuilder, CasetteProperties casetteProperties)
        {
            // spools
            visualBuilder.DrawRectangle(7, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(9, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(7, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(9, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(19, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(21, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(19, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(21, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);

            // spool centers
            visualBuilder.SetCell(8, 9, 'x', casetteProperties.SpoolForeground, casetteProperties.SpoolBackground);
            visualBuilder.SetCell(20, 9, 'x', casetteProperties.SpoolForeground, casetteProperties.SpoolBackground);

            // tape
            visualBuilder.DrawRectangle(4, 8, 1, 1, casetteProperties.Tape, casetteProperties.Tape);
            visualBuilder.DrawRectangle(4, 9, 1, 1, casetteProperties.Tape, casetteProperties.Tape);
            visualBuilder.DrawRectangle(24, 9, 1, 1, casetteProperties.Tape, casetteProperties.Tape);
            visualBuilder.DrawRectangle(24, 10, 1, 1, casetteProperties.Tape, casetteProperties.Tape);

            return visualBuilder;
        }

        internal static GridVisualBuilder AddDetails2(GridVisualBuilder visualBuilder, CasetteProperties casetteProperties)
        {
            // spools
            visualBuilder.DrawRectangle(8, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(7, 9, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(9, 9, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(8, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(20, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(19, 9, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(21, 9, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(20, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);

            // spool centers
            visualBuilder.SetCell(8, 9, '+', casetteProperties.SpoolForeground, casetteProperties.SpoolBackground);
            visualBuilder.SetCell(20, 9, '+', casetteProperties.SpoolForeground, casetteProperties.SpoolBackground);

            // tape
            visualBuilder.DrawRectangle(4, 8, 1, 1, casetteProperties.Tape, casetteProperties.Tape);
            visualBuilder.DrawRectangle(24, 10, 1, 1, casetteProperties.Tape, casetteProperties.Tape);

            return visualBuilder;
        }

        internal static GridVisualBuilder AddDetails3(GridVisualBuilder visualBuilder, CasetteProperties casetteProperties)
        {
            // spools
            visualBuilder.DrawRectangle(7, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(9, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(7, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(9, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(19, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(21, 8, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(19, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);
            visualBuilder.DrawRectangle(21, 10, 1, 1, casetteProperties.SpoolBackground, casetteProperties.SpoolBackground);

            // spool centers
            visualBuilder.SetCell(8, 9, 'x', casetteProperties.SpoolForeground, casetteProperties.SpoolBackground);
            visualBuilder.SetCell(20, 9, 'x', casetteProperties.SpoolForeground, casetteProperties.SpoolBackground);

            // tape
            visualBuilder.DrawRectangle(4, 9, 1, 1, casetteProperties.Tape, casetteProperties.Tape);
            visualBuilder.DrawRectangle(4, 10, 1, 1, casetteProperties.Tape, casetteProperties.Tape);
            visualBuilder.DrawRectangle(24, 8, 1, 1, casetteProperties.Tape, casetteProperties.Tape);
            visualBuilder.DrawRectangle(24, 9, 1, 1, casetteProperties.Tape, casetteProperties.Tape);

            return visualBuilder;
        }
    }
}
