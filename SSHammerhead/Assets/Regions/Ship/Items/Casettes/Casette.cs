using NetAF.Assets;
using NetAF.Targets.Console.Rendering;
using System;

namespace SSHammerhead.Assets.Regions.Ship.Items.Casettes
{
    /// <summary>
    /// Represents a casette.
    /// </summary>
    /// <param name="info">The casette info.</param>
    public record Casette(CasetteInfo Info, CasetteVisualProperties properties)
    {
        /// <summary>
        /// Get the song at a specified time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>The song at the specified time.</returns>
        public SongInfo GetSongAtTime(TimeSpan time)
        {
            var total = TimeSpan.Zero;

            for (var i = 0; i < Info.Songs.Length; i++)
            {
                if (time < Info.Songs[i].Duration)
                    return Info.Songs[i];

                total += Info.Songs[i].Duration;
            }

            return Info.Songs[^1];
        }

        /// <summary>
        /// Get a visual template of the casettte.
        /// </summary>
        /// <returns>The visual builder.</returns>
        public GridVisualBuilder GetVisualTemplate()
        {
            var visualBuilder = new GridVisualBuilder(properties.Background, properties.Background);
            visualBuilder.Resize(new Size(29, 15));

            var bodyVarier = new IntensityVariationGenerator(10, 10);
            var labelVarier = new IntensityVariationGenerator(5, 5);
            var windowVarier = new IntensityVariationGenerator(5, 5);

            // cassette body
            visualBuilder.DrawRectangle(0, 0, 29, 15, properties.BodyBorder, properties.Body, bodyVarier, bodyVarier);

            // label
            visualBuilder.DrawRectangle(2, 2, 25, 3, properties.Label, properties.Label, labelVarier, labelVarier);
            visualBuilder.DrawText(3, 3, properties.LabelText, properties.LabelForeground);

            // screws
            visualBuilder.SetCell(1, 1, properties.ScrewCharacter, properties.ScrewForeground, properties.Screw);
            visualBuilder.SetCell(27, 1, properties.ScrewCharacter, properties.ScrewForeground, properties.Screw);
            visualBuilder.SetCell(1, 13, properties.ScrewCharacter, properties.ScrewForeground, properties.Screw);
            visualBuilder.SetCell(27, 13, properties.ScrewCharacter, properties.ScrewForeground, properties.Screw);

            // window
            visualBuilder.DrawRectangle(2, 6, 25, 7, properties.WindowBorder, properties.Window, windowVarier, windowVarier);

            return visualBuilder;
        }

        /// <summary>
        /// Add variation details for a visual of the casette.
        /// </summary>
        /// <param name="visualBuilder">The visual builder.</param>
        /// <param name="variation">The variation.</param>
        /// <returns>The visual builder with the variation details added.</returns>
        public GridVisualBuilder AddVisualDetails(GridVisualBuilder visualBuilder, CasetteVariation variation)
        {
            return variation switch
            {
                CasetteVariation.Zero => AddVisualDetails0(visualBuilder),
                CasetteVariation.One => AddVisualDetails1(visualBuilder),
                CasetteVariation.Two => AddVisualDetails2(visualBuilder),
                CasetteVariation.Three => AddVisualDetails3(visualBuilder),
                _ => throw new NotImplementedException()
            };
        }

        private GridVisualBuilder AddVisualDetails0(GridVisualBuilder visualBuilder)
        {
            // spools
            visualBuilder.DrawRectangle(8, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(7, 9, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(9, 9, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(8, 10, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(20, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(19, 9, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(21, 9, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(20, 10, 1, 1, properties.Spool, properties.Spool);

            // spool centers
            visualBuilder.SetCell(8, 9, '+', properties.SpoolForeground, properties.Spool);
            visualBuilder.SetCell(20, 9, '+', properties.SpoolForeground, properties.Spool);

            // tape
            visualBuilder.DrawRectangle(4, 9, 1, 1, properties.Tape, properties.Tape);
            visualBuilder.DrawRectangle(24, 9, 1, 1, properties.Tape, properties.Tape);

            return visualBuilder;
        }

        private GridVisualBuilder AddVisualDetails1(GridVisualBuilder visualBuilder)
        {
            // spools
            visualBuilder.DrawRectangle(7, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(9, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(7, 10, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(9, 10, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(19, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(21, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(19, 10, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(21, 10, 1, 1, properties.Spool, properties.Spool);

            // spool centers
            visualBuilder.SetCell(8, 9, 'x', properties.SpoolForeground, properties.Spool);
            visualBuilder.SetCell(20, 9, 'x', properties.SpoolForeground, properties.Spool);

            // tape
            visualBuilder.DrawRectangle(4, 8, 1, 1, properties.Tape, properties.Tape);
            visualBuilder.DrawRectangle(4, 9, 1, 1, properties.Tape, properties.Tape);
            visualBuilder.DrawRectangle(24, 9, 1, 1, properties.Tape, properties.Tape);
            visualBuilder.DrawRectangle(24, 10, 1, 1, properties.Tape, properties.Tape);

            return visualBuilder;
        }

        private GridVisualBuilder AddVisualDetails2(GridVisualBuilder visualBuilder)
        {
            // spools
            visualBuilder.DrawRectangle(8, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(7, 9, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(9, 9, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(8, 10, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(20, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(19, 9, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(21, 9, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(20, 10, 1, 1, properties.Spool, properties.Spool);

            // spool centers
            visualBuilder.SetCell(8, 9, '+', properties.SpoolForeground, properties.Spool);
            visualBuilder.SetCell(20, 9, '+', properties.SpoolForeground, properties.Spool);

            // tape
            visualBuilder.DrawRectangle(4, 8, 1, 1, properties.Tape, properties.Tape);
            visualBuilder.DrawRectangle(24, 10, 1, 1, properties.Tape, properties.Tape);

            return visualBuilder;
        }

        private GridVisualBuilder AddVisualDetails3(GridVisualBuilder visualBuilder)
        {
            // spools
            visualBuilder.DrawRectangle(7, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(9, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(7, 10, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(9, 10, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(19, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(21, 8, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(19, 10, 1, 1, properties.Spool, properties.Spool);
            visualBuilder.DrawRectangle(21, 10, 1, 1, properties.Spool, properties.Spool);

            // spool centers
            visualBuilder.SetCell(8, 9, 'x', properties.SpoolForeground, properties.Spool);
            visualBuilder.SetCell(20, 9, 'x', properties.SpoolForeground, properties.Spool);

            // tape
            visualBuilder.DrawRectangle(4, 9, 1, 1, properties.Tape, properties.Tape);
            visualBuilder.DrawRectangle(4, 10, 1, 1, properties.Tape, properties.Tape);
            visualBuilder.DrawRectangle(24, 8, 1, 1, properties.Tape, properties.Tape);
            visualBuilder.DrawRectangle(24, 9, 1, 1, properties.Tape, properties.Tape);

            return visualBuilder;
        }
    }
}
