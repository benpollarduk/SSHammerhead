﻿using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L1
{
    internal class StarboardWingInner : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Starboard Wing Inner";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.East), new Exit(Direction.West)], interaction: (item) =>
            {
                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                    return Scanner.PerformScan(Name, new(SSHammerHead.DefaultRoomComposition));

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
