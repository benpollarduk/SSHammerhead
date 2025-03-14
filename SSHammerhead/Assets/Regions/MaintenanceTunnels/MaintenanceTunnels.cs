using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.MaintenanceTunnels.L0;
using System;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels
{
    internal class MaintenanceTunnels : IAssetTemplate<Region>
    {
        #region Constants

        internal const string Name = "Maintenance Tunnels";
        private static readonly string Description = "The maintenance tunnels.";

        #endregion

        #region Implementation of IAssetTemplate<Region>

        public Region Instantiate()
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                // L0
                [0, 3, 0] = new MaintenanceTunnelA().Instantiate(),
                [1, 3, 0] = new MaintenanceTunnelB().Instantiate(),
                [2, 3, 0] = new MaintenanceTunnelC().Instantiate(),
                [2, 2, 0] = new MaintenanceTunnelD().Instantiate(),
                [2, 1, 0] = new MaintenanceTunnelE().Instantiate(),
                [1, 1, 0] = new MaintenanceTunnelF().Instantiate(),
                [3, 2, 0] = new MaintenanceTunnelG().Instantiate(),
                [4, 2, 0] = new MaintenanceTunnelH().Instantiate(),
                [2, 0, 0] = new MaintenanceTunnelI().Instantiate(),
            };

            // start in maintenance tunnel A
            var start = Array.Find(regionMaker.GetRoomPositions(), r => MaintenanceTunnelA.Name.EqualsIdentifier(r.Room.Identifier));
            return regionMaker.Make(start);
        }

        #endregion
    }
}
