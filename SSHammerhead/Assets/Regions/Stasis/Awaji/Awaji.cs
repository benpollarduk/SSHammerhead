using System;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Stasis.Awaji.Rooms;

namespace SSHammerhead.Assets.Regions.Stasis.Awaji
{
    internal class Awaji : IAssetTemplate<Region>
    {
        #region Constants

        internal const string Name = "Awaji";
        private static readonly string Description = "An small island in Wakayama bay.";

        #endregion

        #region Implementation of IAssetTemplate<Region>

        public Region Instantiate()
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                [0, 0, 0] = new Island().Instantiate(),
                [3, 2, 0] = new GenericIsland("Island").Instantiate(),
                [-2, 4, 0] = new GenericIsland("Island").Instantiate(),
                [-3, 3, 0] = new GenericIsland("Island").Instantiate(),
                [1, -2, 0] = new Torii().Instantiate(),
            };

            // start on island
            var start = Array.Find(regionMaker.GetRoomPositions(), r => Island.Name.EqualsIdentifier(r.Room.Identifier));
            var region = regionMaker.Make(start);
            region.IsVisibleWithoutDiscovery = true;
            return region;
        }

        #endregion
    }
}
