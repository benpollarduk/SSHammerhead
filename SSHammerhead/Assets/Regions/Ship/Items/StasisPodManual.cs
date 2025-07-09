using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Utilities;
using System;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class StasisPodManual : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Stasis Pod Manual";
        private readonly string Description = $"INTRODUCTION: A stasis pod provides a safe environment for an astronaut during travel over large distances.{StringUtilities.Newline}" +
            $"To keep the astronauts mind active the stasis chamber generates an immersive simulation of an environment that suits the individual and helps to preserve their mental health over extended periods of stasis.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"TROUBLESHOOTING: If the stasis pod won't power up check the breaker first.";

        internal const string StasisPodManualLogName = "StasisPodManual";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Paper", 82.25f },
            { "Card", 10.23f },
            { "Ink", 1.02f },
            { "Glue", 3.6f }
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            ExaminationCallback examination = new(request =>
            {
                if (!GameExecutor.ExecutingGame?.NoteManager.ContainsEntry(StasisPodManualLogName) ?? false)
                {
                    if (request.Scene.Room.FindItem(StasisPodC.Name, out var podC))
                    {
                        var command = Array.Find(podC.Commands, x => x.Help.Command.InsensitiveEquals(StasisPodC.FlipBreakerCommandName));

                        if (command != null)
                            command.IsPlayerVisible = true;

                        GameExecutor.ExecutingGame?.NoteManager.Add(StasisPodManualLogName, "Resetting the breaker will power up a disabled stasis pod.");
                    }
                }
               
                return ExaminableObject.DefaultExamination(request);
            });

            return new Item(Name, Description, true, examination: examination);
        }

        #endregion
    }
}
