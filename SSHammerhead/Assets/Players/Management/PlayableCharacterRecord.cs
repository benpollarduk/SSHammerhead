using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Rendering.FrameBuilders;

namespace SSHammerhead.Assets.Players.Management
{
    /// <summary>
    /// Initializes a new instance of the PlayableCharacterRecord record.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <param name="region">The region.</param>
    /// <param name="room">The room.</param>
    /// <param name="frameBuilderCollection">The frame builder collection.</param>
    /// <param name="errorPrefix">The error prefix.</param>
    internal class PlayableCharacterRecord(PlayableCharacter instance, Region region, Room room, FrameBuilderCollection frameBuilderCollection, string errorPrefix)
    {
        /// <summary>
        /// Get the instance.
        /// </summary>
        internal PlayableCharacter Instance { get; } = instance;

        /// <summary>
        /// Get or set the region.
        /// </summary>
        internal Region Region { get; set; } = region;

        /// <summary>
        /// Get or set the room.
        /// </summary>
        internal Room Room { get; set; } = room;

        /// <summary>
        /// Get or set the frame builder collection.
        /// </summary>
        internal FrameBuilderCollection FrameBuilderCollection { get; set; } = frameBuilderCollection;

        /// <summary>
        /// Get the error prefix.
        /// </summary>
        internal string ErrorPrefix { get; set; } = errorPrefix;
    }
}
