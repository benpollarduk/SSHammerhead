using System;
using System.Collections.Generic;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Rooms.L0;
using SSHammerhead.Assets.Regions.Core.Rooms.L1;
using SSHammerhead.Assets.Regions.Core.Rooms.L2;

namespace SSHammerhead.Assets.Regions.Core
{
    internal class SSHammerHead : IAssetTemplate<Region>
    {
        #region Constants

        internal const string Name = "SS Hammerhead";
        private static readonly string Description = "The star ship Hammerhead.";

        #endregion

        #region StaticMethods

        internal static Dictionary<string, float> DefaultRoomComposition => new ()
        {
            { "Steel", 7.68f },
            { "Aluminum", 44.82f },
            { "Glass", 6.37f },
            { "Rubber", 5.12f },
            { "Plastics", 21.23f },
            { "Perspex", 12.1f },
        };

        #endregion

        #region Implementation of IAssetTemplate<Region>

        public Region Instantiate()
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                // L2
                [0, 0, 0] = new Bridge().Instantiate(),
                [-1, 0, 0] = new BridgePort().Instantiate(),
                [1, 0, 0] = new BridgeStarboard().Instantiate(),
                [0, -1, 0] = new BridgeTunnel().Instantiate(),
                // L1
                [0, -1, -1] = new BridgeTunnelVertical().Instantiate(),
                [0, -2, -1] = new BridgeTunnelEntry().Instantiate(),
                [0, -3, -1] = new CentralHull().Instantiate(),
                [0, -4, -1] = new Booster().Instantiate(),
                [-1, -2, -1] = new MedicalRoom().Instantiate(),
                [1, -2, -1] = new StarboardWing().Instantiate(),
                [-2, -3, -1] = new Laboratory().Instantiate(),
                [-1, -3, -1] = new StasisChamber().Instantiate(),
                [1, -3, -1] = new StarboardWingInner().Instantiate(),
                [2, -3, -1] = new StarboardWingOuter().Instantiate(),
                // L0
                [0, -3, -2] = new EngineRoom().Instantiate(),
                [-1, -3, -2] = new Airlock().Instantiate(),
                [1, -3, -2] = new SupplyRoom().Instantiate()
            };

            // start in airlock
            var start = Array.Find(regionMaker.GetRoomPositions(), r => Airlock.Name.EqualsIdentifier(r.Room.Identifier));
            return regionMaker.Make(start);
        }

        #endregion
    }
}
