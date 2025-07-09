using NetAF.Rendering.FrameBuilders;

namespace SSHammerhead.Configuration
{
    /// <summary>
    /// Provides a presentation for a game.
    /// </summary>
    /// <param name="naomi">The frame builder collection for Naomi.</param>
    /// <param name="bot">The frame builder collection for the bot.</param>
    /// <param name="anne">The frame builder collection for Anne.</param>
    public class Presentation(FrameBuilderCollection naomi, FrameBuilderCollection bot, FrameBuilderCollection anne)
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

        #endregion
    }
}
