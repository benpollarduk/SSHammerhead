﻿using NetAF.Assets;
using NetAF.Logic;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Ship.Rooms.L0;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class PostIt : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Post-it";
        private readonly string Description = $"A small square yellow piece of paper with a sticky edge, commonly written on and used to display reminders.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"A message has been scrawled on this one in black sharpie:{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"\"To whomever finds this, know that I had to abandon the ship and the others. I didn't want to but my duty bound me. Before I left I locked down access to the central hull. I didn't trust the electronic locking system would remain locked, after all these things can be hacked, so I used a padlock to lock the hatch.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"Once I'd done that I stuffed the key through the grating in the side of the maintenance shaft used by the spider bot where it was out of my reach in case I had a change of heart and decided to try to save the ship and the guys.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"I know that is far beyond my reach but I worry that will have a change of heart. Now I plan to take the escape pod and leave the ship behind. I would rather take my chances adrift in space alone.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"Anne\"";

        internal const string PostItLogName = "PostIt";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Paper", 94f },
            { "Ink", 1.02f },
            { "Glue", 3.6f }
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            ExaminationCallback examination = new(request =>
            {
                GameExecutor.ExecutingGame?.NoteManager.Add(PostItLogName, $"A padlock locks the hatch in the {EngineRoom.Name} blocking access to the floor above. Anne hid the key inside a maintenance shaft.");
                return ExaminableObject.DefaultExamination(request);
            });

            return new Item(Name, Description, true, examination: examination);
        }

        #endregion
    }
}
