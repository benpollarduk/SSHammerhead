using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels.L0
{
    internal class MaintenanceTunnelA : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Maintenance Tunnel A";
        private const string Description = "A small maintenance tunnel to allow the maintenance bots to traverse the ship.";
        private const string Introduction = "Initializing system...";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new(Name, Description, Introduction, [new Exit(Direction.East)], examination: request => new Examination(Description));
        }

        #endregion
    }
}
