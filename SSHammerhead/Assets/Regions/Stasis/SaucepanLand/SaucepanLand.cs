using System;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Stasis.SaucepanLand.Rooms;

namespace SSHammerhead.Assets.Regions.Stasis.SaucepanLand
{
    internal class SaucepanLand : IAssetTemplate<Region>
    {
        #region Constants

        internal const string Name = "SaucepanLand";
        private static readonly string Description = "Enid Blytons SaucepanLand.";

        #endregion

        #region Implementation of IAssetTemplate<Region>

        public Region Instantiate()
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                // L8
                [0, 2, 0] = new Field1().Instantiate(),
                [1, 2, 0] = new Field2().Instantiate(),
                [1, 1, 0] = new Field3().Instantiate(),
                [0, 1, 0] = new Field4().Instantiate(),
                // L7
                [0, 1, -1] = new Cliff().Instantiate(),
                [1, 2, -1] = new Ladder().Instantiate(),
                // L6
                [1, 2, -2] = new TreeTop().Instantiate(),
                [0, 1, -2] = new Lawn().Instantiate(),
                [0, 0, -2] = new House().Instantiate(),
                // L5
                [1, 2, -3] = new OutsideMoonFacesHouse().Instantiate(),
                [0, 2, -3] = new MoonFacesHouse().Instantiate(),
                // L4
                [1, 2, -4] = new OutsideSilkysHouse().Instantiate(),
                [0, 2, -4] = new SilkysHouse().Instantiate(),
                // L3
                [1, 2, -5] = new OutsideMrWhatzisnamesHouse().Instantiate(),
                [0, 2, -5] = new MrWhatzisnamesHouse().Instantiate(),
                // L2
                [1, 2, -6] = new OutsideDameWashalotsHouse().Instantiate(),
                [0, 2, -6] = new DameWashalotsHouse().Instantiate(),
                // L1
                [1, 2, -7] = new OutsideAngryPixiesHouse().Instantiate(),
                [0, 2, -7] = new AngryPixiesHouse().Instantiate(),
                // L0
                [1, 2, -8] = new Trunk().Instantiate(),
                [0, 2, -8] = new Woods().Instantiate(),
                [2, 2, -8] = new Woods().Instantiate(),
                [1, 3, -8] = new Woods().Instantiate(),
                [1, 1, -8] = new Woods().Instantiate(),
                [-1, 2, -8] = new Woods().Instantiate(),
                [3, 2, -8] = new Woods().Instantiate(),
                [1, 4, -8] = new Woods().Instantiate(),
                [1, 0, -8] = new Woods().Instantiate()
            };

            // start in house
            var start = Array.Find(regionMaker.GetRoomPositions(), r => House.Name.EqualsIdentifier(r.Room.Identifier));
            return regionMaker.Make(start);
        }

        #endregion
    }
}
