using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.MaintenanceTunnels.Items;
using SSHammerhead.Assets.Regions.Ship.Rooms.L0;
using SSHammerhead.Assets.Regions.Ship.Rooms.L1;
using SSHammerhead.Assets.Regions.Ship.Rooms.L2;
using System;
using System.Linq;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class Scanner : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Scanner";
        private const string Description = "A small hand held scanner designed by Scantek for analysing the composition of objects.";
        internal const string Make = "Scantek";
        internal const string Model = "Object Scanner 2400n";

        #endregion

        #region StaticProperties

        private static readonly Composition[] Compositions =
        [
            new(Blueprint.Name, Blueprint.Composition),
            new(BrokenControlPanel.Name, BrokenControlPanel.Composition),
            new(EmptyTray.Name, EmptyTray.Composition),
            new(Hammer.Name, Hammer.Composition),
            new(Laptop.Name, Laptop.Composition),
            new(LockedMaintenanceControlPanel.Name, LockedMaintenanceControlPanel.Composition),
            new(MaintenanceControlPanel.Name, MaintenanceControlPanel.Composition),
            new(PadlockKey.Name, PadlockKey.Composition),
            new(PostIt.Name, PostIt.Composition),
            new(StasisPodA.Name, StasisPod.Composition),
            new(StasisPodB.Name, StasisPod.Composition),
            new(StasisPodC.Name, StasisPod.Composition),
            new(StasisPodD.Name, StasisPod.Composition),
            new(StasisPodE.Name, StasisPod.Composition),
            new(StasisPodF.Name, StasisPod.Composition),
            new(Tray.Name, Tray.Composition),
            new(USBDrive.Name, USBDrive.Composition),
            new(LaserBarrier.Name, LaserBarrier.Composition),
            new(EngineRoom.HatchName, EngineRoom.HatchComposition),
            new(Airlock.Name, SSHammerHead.DefaultRoomComposition),
            new(EngineRoom.Name, SSHammerHead.DefaultRoomComposition),
            new(SupplyRoom.Name, SSHammerHead.DefaultRoomComposition),
            new(Booster.Name, SSHammerHead.DefaultRoomComposition),
            new(BridgeTunnelEntry.Name, SSHammerHead.DefaultRoomComposition),
            new(BridgeTunnelVertical.Name, SSHammerHead.DefaultRoomComposition),
            new(CentralHull.Name, SSHammerHead.DefaultRoomComposition),
            new(MedicalRoom.Name, SSHammerHead.DefaultRoomComposition),
            new(StasisChamber.Name, SSHammerHead.DefaultRoomComposition),
            new(Laboratory.Name, SSHammerHead.DefaultRoomComposition),
            new(StarboardWing.Name, SSHammerHead.DefaultRoomComposition),
            new(StarboardWingInner.Name, SSHammerHead.DefaultRoomComposition),
            new(StarboardWingOuter.Name, SSHammerHead.DefaultRoomComposition),
            new(Bridge.Name, SSHammerHead.DefaultRoomComposition),
            new(BridgePort.Name, SSHammerHead.DefaultRoomComposition),
            new(BridgeStarboard.Name, SSHammerHead.DefaultRoomComposition),
            new(BridgeTunnel.Name, SSHammerHead.DefaultRoomComposition)
        ];

        #endregion

        #region StaticMethods

        private static Composition FindComposition(IExaminable examinable)
        {
            var identifierName = examinable.Identifier.Name;
            return Array.Find(Compositions, x => x.Name.InsensitiveEquals(identifierName));
        }

        internal static bool CanScan(IExaminable examinable)
        {
            return FindComposition(examinable) != null;
        }

        internal static Composition Scan(IExaminable examinable)
        {
            return FindComposition(examinable) ?? new(examinable.Identifier.Name, null);
        }

        internal static IExaminable[] GetScannableExaminables(Game game)
        {
            return game.GetAllPlayerVisibleExaminables().Where(CanScan).ToArray();
        }

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            CustomCommand[] commands =
            [
                new CustomCommand(new CommandHelp("Scan", "Scan an item."), true, true, (g, _) => new Commands.Scanner.Scan(null).Invoke(g))
            ];

            return new Item(Name, Description, true, commands: commands, interaction: (item) =>
            {
                if (Name.EqualsIdentifier(item.Identifier))
                    return new Interaction(InteractionResult.NoChange, item, $"The {Name} cannot possibly scan itself!");

                if (Hammer.Name.EqualsIdentifier(item.Identifier))
                    return new Interaction(InteractionResult.NoChange, item, $"The {Name} is rubberised and is resistant to your feeble attempt to smash it.");

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
