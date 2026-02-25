using System;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Stasis.Space.Rooms;

namespace SSHammerhead.Assets.Regions.Stasis.Space
{
    internal class Space : IAssetTemplate<Region>
    {
        #region Constants

        internal const string Name = "Space";
        private static readonly string Description = "The vast void of space.";

        #endregion

        #region Implementation of IAssetTemplate<Region>

        public Region Instantiate()
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                [0, 0, 0] = new GenericSpace("Space").Instantiate(),
            };

            var start = Array.Find(regionMaker.GetRoomPositions(), r => "Space".EqualsIdentifier(r.Room.Identifier));
            var region = regionMaker.Make(start);
            region.IsVisibleWithoutDiscovery = true;
            return region;
        }

        #endregion
    }
}
