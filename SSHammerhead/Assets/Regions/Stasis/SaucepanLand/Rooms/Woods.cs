using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;
using System;

namespace SSHammerhead.Assets.Regions.Stasis.SaucepanLand.Rooms
{
    internal class Woods : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Woods";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            RoomTransitionCallback backToTrunk = new(_ =>
            {
                var region = GameExecutor.ExecutingGame.Overworld.CurrentRegion;

                if (region.TryFindRoom(Trunk.Name, out var trunk))
                {
                    var location = region.GetPositionOfRoom(trunk);
                    var result = region.JumpToRoom(location.Position);

                    if (result)
                    {
                        Console.WriteLine("Jumped.");
                    }
                }
            });

            return new Room(Name, Description, [new Exit(Direction.North), new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.West)], exitCallback: backToTrunk);
        }

        #endregion
    }
}
