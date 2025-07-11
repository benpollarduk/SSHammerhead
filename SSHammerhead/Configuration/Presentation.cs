using NetAF.Rendering.FrameBuilders;

namespace SSHammerhead.Configuration
{
    /// <summary>
    /// Provides a presentation for a game.
    /// </summary>
    /// <param name="naomi">The frame builder collection for Naomi.</param>
    /// <param name="bot">The frame builder collection for the bot.</param>
    /// <param name="anne">The frame builder collection for Anne.</param>
    /// <param name="alex">The frame builder collection for Alex.</param>
    /// <param name="marina">The frame builder collection for Marina.</param>
    /// <param name="scott">The frame builder collection for Scott.</param>
    /// <param name="zhiying">The frame builder collection for Zhiying.</param>
    public class Presentation(FrameBuilderCollection naomi, FrameBuilderCollection bot, FrameBuilderCollection anne, FrameBuilderCollection alex, FrameBuilderCollection marina, FrameBuilderCollection scott, FrameBuilderCollection zhiying)
    {
        #region Properties

        /// <summary>
        /// Get the frame builder collection for Naomi.
        /// </summary>
        public FrameBuilderCollection Naomi => naomi;

        /// <summary>
        /// Get the frame builder collection for the bot.
        /// </summary>
        public FrameBuilderCollection Bot => bot;

        /// <summary>
        /// Get the frame builder collection for Anne.
        /// </summary>
        public FrameBuilderCollection Anne => anne;

        /// <summary>
        /// Get the frame builder collection for Alex.
        /// </summary>
        public FrameBuilderCollection Alex => alex;

        /// <summary>
        /// Get the frame builder collection for Marina.
        /// </summary>
        public FrameBuilderCollection Marina => marina;

        /// <summary>
        /// Get the frame builder collection for Scott.
        /// </summary>
        public FrameBuilderCollection Scott => scott;

        /// <summary>
        /// Get the frame builder collection for Zhiying.
        /// </summary>
        public FrameBuilderCollection Zhiying => zhiying;

        #endregion
    }
}
