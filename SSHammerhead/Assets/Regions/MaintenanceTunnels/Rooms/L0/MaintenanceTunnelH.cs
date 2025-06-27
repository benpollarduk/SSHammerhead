using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels.Rooms.L0
{
    internal class MaintenanceTunnelH : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Maintenance Tunnel H";
        private const string Description = "A small maintenance tunnel to allow the maintenance bots to traverse the ship.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new(Name, Description, [new Exit(Direction.West)], examination: request => new Examination(Description));
        }

        #endregion
    }
}
