using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Rendering.FrameBuilders;

namespace SSHammerhead.Assets.Players.Management
{
    /// <summary>
    /// Initializes a new instance of the PlayableCharacterRecord record.
    /// </summary>
    /// <param name="Instance">The instance.</param>
    /// <param name="StartRegion">The start region.</param>
    /// <param name="StartRoom">The start room.</param>
    /// <param name="FrameBuilderCollection">The frame builder collection.</param>
    /// <param name="ErrorPrefix">The error prefix.</param>
    internal record PlayableCharacterRecord(PlayableCharacter Instance, Region StartRegion, Room StartRoom, FrameBuilderCollection FrameBuilderCollection, string ErrorPrefix);
}
