using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Logic.Modes;
using NetAF.Narratives;
using NetAF.Utilities;
using SSHammerhead.Assets.Players.Anne;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.Naomi;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Assets.Regions.Ship.Rooms.L1;

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

        #region StaticMethods

        private static Narrative GetNarrative()
        {
            Section[] sections =
            [
                new Section(["Looking around you realise just how isolated you really are.", "The gentle but warm breeze on your face feels fantastic.", "You let out a deep breath, almost a sigh. This moment is feels so surreal after the confinements of the ship.", "Maybe Anne craved this freedom?"]),
                new Section(["The seagulls chirp and chatter as they circle in the sky above the islands.", "You wonder how it would feel to be a seagull. They must feel so free."]),
                new Section(["You imagine having wings.", "You imagine the warm breeze flowing through your feathers as you circle in the sky high above the islands.", "The feeling of flying is exhilarating."]),
                new Section(["You open your eyes and see another seagull flying so close that your wing almost brushes it's wing.", "Another couple of gulls are flying just above you", "The world feels so big right now."]),
                new Section(["Looking down there are other gulls below you.", "However, their positions don't appear to be random like they looked from the island...", "Viewed from above they form the distinct shape of two characters:"]),
                new Section([LaserBarrier.UnlockCode3]),
                new Section(["Yes, there is a definite pattern:"]),
                new Section([LaserBarrier.UnlockCode3]),
                new Section(["You feel a sucking feeling, everything rushes away from you all at once.", "You cling on, trying to remain but it is no good, you are powerless."]),
                new Section([$"The simulation of the {Name} vanishes to immediate black and you find yourself back in the stasis pod.", "The contrast of being back in the confinement of the stasis pod is incredibly jarring."]),
                new Section(["The stasis pod crackles and sparks, then emits a puff of smoke.", "The breaker flips and the machine dies.", $"The door to the {Laboratory.Name} clicks as it unlocks, possibly from the surge created by the stasis pod as it died."])
            ];
           
            return new Narrative(Name, sections);
        }

        #endregion

        #region Implementation of IAssetTemplare<Room>

        public Room Instantiate()
        {
            var survey = new CustomCommand(new CommandHelp("Survey", $"Survey the area."), true, true, (g, _) =>
            {
                g.VariableManager.Add(LaserBarrier.UnlockCode1Variable, LaserBarrier.UnlockCode1);
                g.NoteManager.Add(IslandCodeLogName, $"The code from {AnneTemplate.Name}'s dream was {LaserBarrier.UnlockCode3}.");

                var reaction = PlayableCharacterManager.Switch(NaomiTemplate.Identifier, g);

                if (reaction.Result == ReactionResult.Error)
                    return reaction;

                g.ChangeMode(new NarrativeMode(GetNarrative()));

                return new Reaction(ReactionResult.GameModeChanged, string.Empty);
            });

            return new Room(Name, Description, Introduction, commands: [survey]);
        }

        #endregion
    }
}
