﻿using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L1
{
    internal class PortWingInner : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Port Wing Inner";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.East), new Exit(Direction.West));
        }

        #endregion
    }
}