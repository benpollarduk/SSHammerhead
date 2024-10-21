using System.Linq;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L0;
using NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L1;
using NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L2;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.SSHammerHead
{
    internal class SSHammerHead : IAssetTemplate<Region>
    {
        #region Constants

        private const string Name = "SS Hammerhead";
        private static readonly string Description = "The star ship Hammerhead.";

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
                [-1, -2, -1] = new PortWing().Instantiate(),
                [1, -2, -1] = new StarboardWing().Instantiate(),
                [-2, -3, -1] = new PortWingOuter().Instantiate(),
                [-1, -3, -1] = new PortWingInner().Instantiate(),
                [1, -3, -1] = new StarboardWingInner().Instantiate(),
                [2, -3, -1] = new StarboardWingOuter().Instantiate(),
                // L0
                [0, -3, -2] = new EngineRoom().Instantiate(),
                [-1, -3, -2] = new Airlock().Instantiate(),
                [1, -3, -2] = new SupplyRoom().Instantiate()
            };

            // start in airlock
            return regionMaker.Make(regionMaker.GetRoomPositions().FirstOrDefault(r => Airlock.Name.EqualsIdentifier(r.Room.Identifier)));
        }

        #endregion
    }
}
