﻿using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;
using SSHammerhead.Assets.Regions.MaintenanceTunnels.Items;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Rooms.L0
{
    internal class EngineRoom : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Engine Room";

        private static readonly string NoPostItDescription = "This area hosts the large engine that used to power the SS HammerHead. It is now dormant and eerily silent, " +
            "the fusion mechanism long since powered down. The room itself is very industrial, with metal walkways surrounding the perimeter of the room and the engine itself. " +
            "A ladder leads upwards from one of these walkways towards the central hull.";
        
        private static readonly string PostItDescription = NoPostItDescription + $"{StringUtilities.Newline}{StringUtilities.Newline}A yellow {PostIt.Name} is stuck to the door frame.";

        private const string Introduction = "With a sense of trepidation you enter the ship.";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            Room room = null;
            Exit up = null;
            ConditionalDescription hatchDescription = new("A hatch leading up into the higher levels of the ship. A large padlock is securing it shut.", "A hatch leading up into the higher levels of the ship.", () => up.IsLocked);

            up = new Exit(Direction.Up, true, new Identifier("Hatch"), hatchDescription, interaction: (item) =>
            {
                Dictionary<string, float> composition = new()
                {
                    { "Steel", 67.47f },
                    { "Aluminum", 23.13f },
                    { "Plastic", 6.7f }
                };

                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                    return Scanner.PerformScan(Name, new(composition));

                if (PadlockKey.Name.EqualsIdentifier(item.Identifier))
                {
                    up.Unlock();
                    return new Interaction(InteractionResult.ItemExpires, item, $"You slot the {PadlockKey.Name} into the padlock locking the hatch and twist. The padlock springs open.");
                }

                return new Interaction(InteractionResult.NoChange, item);
            });


            var roomDescription = new ConditionalDescription(PostItDescription, NoPostItDescription, () => room.FindItem(PostIt.Name, out _));
            room = new Room(new Identifier(Name), roomDescription, new Description(Introduction), [up, new Exit(Direction.East), new Exit(Direction.West)], items: [new Laptop().Instantiate(), new PostIt().Instantiate()], interaction: (item) =>
            {
                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                    return Scanner.PerformScan(Name, new(SSHammerHead.DefaultRoomComposition));

                return new Interaction(InteractionResult.NoChange, item);
            });

            return room;
        }

        #endregion
    }
}
