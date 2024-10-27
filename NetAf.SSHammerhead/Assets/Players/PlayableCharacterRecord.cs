using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.SSHammerhead.Assets.Players
{
    internal class PlayableCharacterRecord
    {
        public PlayableCharacter Instance { get; set; }
        public Region Region { get; set; }
        public Room Room { get; set; }
        public FrameBuilderCollection FrameBuilderCollection { get; set; }
        public string ErrorPrefix { get; set; }

        public PlayableCharacterRecord(PlayableCharacter instance, Region region, Room room, FrameBuilderCollection frameBuilderCollection, string errorPrefix)
        {
            Instance = instance;
            Region = region;
            Room = room;
            FrameBuilderCollection = frameBuilderCollection;
            ErrorPrefix = errorPrefix;
        }
    }
}
