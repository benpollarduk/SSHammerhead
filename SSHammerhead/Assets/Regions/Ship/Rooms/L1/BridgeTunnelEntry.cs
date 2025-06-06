﻿using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L1
{
    internal class BridgeTunnelEntry : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Bridge Tunnel (Entry)";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.West, true), new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.North)]);
        }

        #endregion
    }
}
