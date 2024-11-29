using Microsoft.VisualBasic;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using System.Net.Http.Headers;

namespace SSHammerhead.Assets.Regions.MaintenanceTunnels.L0
{
    internal class MaintenanceTunnelC : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Maintenance Tunnel C";
        private const string DescriptionWhenNoScrew = "A small maintenance tunnel to allow the maintenance bots to traverse the ship.";
        private const string DescriptionWhenScrew = "A small maintenance tunnel to allow the maintenance bots to traverse the ship. There is a small screw on the floor.";
        private const string Screw = "Screw";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            Room room = null;
            ConditionalDescription description = new(DescriptionWhenScrew, DescriptionWhenNoScrew, () => room.Attributes.GetValue(Screw) > 0);
            room = new(new Identifier(Name), description, [new Exit(Direction.South), new Exit(Direction.West)], examination: request =>
            {
                if (room.Attributes.GetValue(Screw) > 0)
                {
                    room.Attributes.Remove(Screw);
                    request.Scene.Examiner.Attributes.Add(Screw, 1);
                    return new Examination($"{Screw} obtained.");
                }

                return new Examination(DescriptionWhenNoScrew);
            });
            room.Attributes.Add(Screw, 1);
            return room;
        }

        #endregion
    }
}
