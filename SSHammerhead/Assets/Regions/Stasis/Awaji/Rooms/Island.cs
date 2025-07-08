using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Stasis.Awaji.Rooms
{
    internal class Island : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Island";
        private const string Description = "The island is a small rocky affair. A few dozed meters away stood in the sea is a wooden red gate which you instantly recognise as a Torri, found by Shinto Shrines in Japan.";
        private readonly string Introduction = $"You get into the stasis pod, strap yourself in and relax. The push of a button on the control panel by your right hand engages stasis mode. Slowly you loose consciousness.{StringUtilities.Newline}{StringUtilities.Newline}. " +
            $"Groggily you open your eyes to find yourself on a tiny island in a bay, completely detached from the ship. You can hear the waves gently breaking against the rocks and the distant call of sea gulls.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, Introduction);
        }

        #endregion
    }
}
