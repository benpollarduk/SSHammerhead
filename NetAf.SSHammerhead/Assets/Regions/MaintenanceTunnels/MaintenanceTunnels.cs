using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.SSHammerhead.Assets.Regions.MaintenanceTunnels.L0;
using NetAF.Utilities;
using System.Linq;

namespace NetAF.SSHammerhead.Assets.Regions.MaintenanceTunnels
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
                [0, 0, 0] = new MaintenanceTunnelA().Instantiate(),
                [1, 0, 0] = new MaintenanceTunnelB().Instantiate()
            };

            // start in maintenance tunnel A
            return regionMaker.Make(regionMaker.GetRoomPositions().FirstOrDefault(r => MaintenanceTunnelA.Name.EqualsIdentifier(r.Room.Identifier)));
        }

        #endregion
    }
}
