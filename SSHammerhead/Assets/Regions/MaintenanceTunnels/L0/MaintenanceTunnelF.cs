﻿using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels.L0
{
    internal class MaintenanceTunnelF : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Maintenance Tunnel F";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new(Name, Description, new Exit(Direction.East));
        }

        #endregion
    }
}