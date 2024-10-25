using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.SSHammerhead.Assets.Players
{
    internal class PlayableCharacterRecord
    {
        public PlayableCharacter Instance { get; set; }
        public RoomPosition RoomPosition { get; set; }
        public FrameBuilderCollection FrameBuilderCollection { get; set; }

        public PlayableCharacterRecord(PlayableCharacter instance, RoomPosition roomPosition, FrameBuilderCollection frameBuilderCollection)
        {
            Instance = instance;
            RoomPosition = roomPosition;
            FrameBuilderCollection = frameBuilderCollection;
        }
    }
}
