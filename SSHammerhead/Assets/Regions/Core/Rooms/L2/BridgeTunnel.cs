using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L2
{
    internal class BridgeTunnel : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Bridge Tunnel";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            return new Room(Name, Description, [new Exit(Direction.North), new Exit(Direction.Down)], interaction: (item) =>
            {
                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                    return Scanner.PerformScan(new(Name, SSHammerHead.DefaultRoomComposition));

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
