﻿using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Ship.Rooms.L2
{
    internal class BridgeStarboard : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Bridge (Starboard)";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.West)]);
        }

        #endregion
    }
}
