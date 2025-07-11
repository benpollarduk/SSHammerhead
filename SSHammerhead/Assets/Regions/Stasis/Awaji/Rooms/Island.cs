using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Anne;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.Naomi;
using SSHammerhead.Assets.Regions.Ship.Items;

namespace SSHammerhead.Assets.Regions.Stasis.Awaji.Rooms
{
    internal class Island : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Island";
        private const string Description = "The island is a small rocky outcrop rising a few meters above sea level. A dozen meters away stands a red wooden gate which you instantly recognise as a Torri, found by Shinto Shrines in Japan. A few other small outcrops populate the surrounding sea, but are too far away to swim to.";
        internal static readonly string Introduction = $"You get into the stasis pod, strap yourself in and relax. The push of a button on the control panel by your right hand engages stasis mode. Slowly you loose consciousness.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"Groggily you open your eyes to find yourself on a tiny island in a bay, completely detached from the ship. You can hear the waves gently breaking against the rocks and the distant call of sea gulls.";

        internal const string IslandCodeLogName = "IslandCode";

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            var escape = new CustomCommand(new CommandHelp("Escape", $"Escape."), true, true, (g, _) =>
            {
                g.NoteManager.Add(IslandCodeLogName, $"The code from {AnneTemplate.Name}'s dream was {LaserBarrier.UnlockCode3}.");

                var reaction = PlayableCharacterManager.Switch(NaomiTemplate.Identifier, g);

                if (reaction.Result == ReactionResult.Error)
                    return reaction;

                return new Reaction(ReactionResult.Inform, $"The simulation of the {Name} vanishes to immediate black and you find yourself back in the stasis pod. The contrast of being back in the confinement of the stasis pod is incredibly jarring.");
            });

            return new Room(Name, Description, Introduction, commands: [escape]);
        }

        #endregion
    }
}
