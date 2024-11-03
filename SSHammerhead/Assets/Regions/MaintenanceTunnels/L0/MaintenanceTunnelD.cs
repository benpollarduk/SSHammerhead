using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels.L0
{
    internal class MaintenanceTunnelD : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Maintenance Tunnel D";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new(Name, Description, [new Exit(Direction.North), new Exit(Direction.South)]);
        }

        #endregion
    }
}
