using NetAF.Assets;
using NetAF.Targets.Console.Rendering;
using System;

namespace SSHammerhead.Assets.Regions.Ship.Items.Casettes
{
    /// <summary>
    /// Represents a casette.
    /// </summary>
    /// <param name="info">The casette info.</param>
    /// <param name="visualProperties">The visual properties.</param>
    /// <param name="resourceName">The resource name of the audio.</param>
    /// <param name="positionInMilliseconds">The position of the audio, in milliseconds.</param>
    public class Casette(CasetteInfo info, CasetteVisualProperties visualProperties, string resourceName, int positionInMilliseconds)
    {
        #region Properties

        /// <summary>
        /// Get the casettes info.
        /// </summary>
        public CasetteInfo Info { get; } = info;

        /// <summary>
        /// Get the casette visual properties.
        /// </summary>
        public CasetteVisualProperties VisualProperties { get; } = visualProperties;

        /// <summary>
        /// Get the resource name of the audio.
        /// </summary>
        public string ResourceName { get; } = resourceName;

        /// <summary>
        /// Get the position of the audio, in milliseconds.
        /// </summary>
        public int PositionInMilliseconds { get; } = positionInMilliseconds;

        #endregion

        #region Methods

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
            var visualBuilder = new GridVisualBuilder(VisualProperties.Background, VisualProperties.Background);
            visualBuilder.Resize(new Size(29, 15));

            var bodyVarier = new IntensityVariationGenerator(10, 10);
            var labelVarier = new IntensityVariationGenerator(5, 5);
            var windowVarier = new IntensityVariationGenerator(5, 5);

            // cassette body
            visualBuilder.DrawRectangle(0, 0, 29, 15, VisualProperties.BodyBorder, VisualProperties.Body, bodyVarier, bodyVarier);

            // label
            visualBuilder.DrawRectangle(2, 2, 25, 3, VisualProperties.Label, VisualProperties.Label, labelVarier, labelVarier);
            visualBuilder.DrawText(3, 3, VisualProperties.LabelText, VisualProperties.LabelForeground);

            // screws
            visualBuilder.SetCell(1, 1, VisualProperties.ScrewCharacter, VisualProperties.ScrewForeground, VisualProperties.Screw);
            visualBuilder.SetCell(27, 1, VisualProperties.ScrewCharacter, VisualProperties.ScrewForeground, VisualProperties.Screw);
            visualBuilder.SetCell(1, 13, VisualProperties.ScrewCharacter, VisualProperties.ScrewForeground, VisualProperties.Screw);
            visualBuilder.SetCell(27, 13, VisualProperties.ScrewCharacter, VisualProperties.ScrewForeground, VisualProperties.Screw);

            // window
            visualBuilder.DrawRectangle(2, 6, 25, 7, VisualProperties.WindowBorder, VisualProperties.Window, windowVarier, windowVarier);

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
            visualBuilder.DrawRectangle(8, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(7, 9, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(9, 9, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(8, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(20, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(19, 9, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(21, 9, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(20, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);

            // spool centers
            visualBuilder.SetCell(8, 9, '+', VisualProperties.SpoolForeground, VisualProperties.Spool);
            visualBuilder.SetCell(20, 9, '+', VisualProperties.SpoolForeground, VisualProperties.Spool);

            // tape
            visualBuilder.DrawRectangle(4, 9, 1, 1, VisualProperties.Tape, VisualProperties.Tape);
            visualBuilder.DrawRectangle(24, 9, 1, 1, VisualProperties.Tape, VisualProperties.Tape);

            return visualBuilder;
        }

        private GridVisualBuilder AddVisualDetails1(GridVisualBuilder visualBuilder)
        {
            // spools
            visualBuilder.DrawRectangle(7, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(9, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(7, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(9, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(19, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(21, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(19, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(21, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);

            // spool centers
            visualBuilder.SetCell(8, 9, 'x', VisualProperties.SpoolForeground, VisualProperties.Spool);
            visualBuilder.SetCell(20, 9, 'x', VisualProperties.SpoolForeground, VisualProperties.Spool);

            // tape
            visualBuilder.DrawRectangle(4, 8, 1, 1, VisualProperties.Tape, VisualProperties.Tape);
            visualBuilder.DrawRectangle(4, 9, 1, 1, VisualProperties.Tape, VisualProperties.Tape);
            visualBuilder.DrawRectangle(24, 9, 1, 1, VisualProperties.Tape, VisualProperties.Tape);
            visualBuilder.DrawRectangle(24, 10, 1, 1, VisualProperties.Tape, VisualProperties.Tape);

            return visualBuilder;
        }

        private GridVisualBuilder AddVisualDetails2(GridVisualBuilder visualBuilder)
        {
            // spools
            visualBuilder.DrawRectangle(8, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(7, 9, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(9, 9, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(8, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(20, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(19, 9, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(21, 9, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(20, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);

            // spool centers
            visualBuilder.SetCell(8, 9, '+', VisualProperties.SpoolForeground, VisualProperties.Spool);
            visualBuilder.SetCell(20, 9, '+', VisualProperties.SpoolForeground, VisualProperties.Spool);

            // tape
            visualBuilder.DrawRectangle(4, 8, 1, 1, VisualProperties.Tape, VisualProperties.Tape);
            visualBuilder.DrawRectangle(24, 10, 1, 1, VisualProperties.Tape, VisualProperties.Tape);

            return visualBuilder;
        }

        private GridVisualBuilder AddVisualDetails3(GridVisualBuilder visualBuilder)
        {
            // spools
            visualBuilder.DrawRectangle(7, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(9, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(7, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(9, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(19, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(21, 8, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(19, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);
            visualBuilder.DrawRectangle(21, 10, 1, 1, VisualProperties.Spool, VisualProperties.Spool);

            // spool centers
            visualBuilder.SetCell(8, 9, 'x', VisualProperties.SpoolForeground, VisualProperties.Spool);
            visualBuilder.SetCell(20, 9, 'x', VisualProperties.SpoolForeground, VisualProperties.Spool);

            // tape
            visualBuilder.DrawRectangle(4, 9, 1, 1, VisualProperties.Tape, VisualProperties.Tape);
            visualBuilder.DrawRectangle(4, 10, 1, 1, VisualProperties.Tape, VisualProperties.Tape);
            visualBuilder.DrawRectangle(24, 8, 1, 1, VisualProperties.Tape, VisualProperties.Tape);
            visualBuilder.DrawRectangle(24, 9, 1, 1, VisualProperties.Tape, VisualProperties.Tape);

            return visualBuilder;
        }

        #endregion
    }
}
