using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Utilities;
using NetAF.Variables;
using SSHammerhead.Assets.Players.Anne;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.Naomi;
using SSHammerhead.Assets.Regions.Stasis.Awaji;
using SSHammerhead.Assets.Regions.Stasis.Awaji.Rooms;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class StasisPodC(ExaminationCallback examination) : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = $"Stasis Pod ({AnneTemplate.Name})";
        internal const string FlipBreakerCommandName = "Flip Breaker";
        internal const string EnterStasisCommandName = "Enter Stasis (C)";
        private const string EnteredStasisCVairableName = "EnteredStasisC";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The third of the Stasis pods, belonging to {AnneTemplate.Name}.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod is unlocked and slightly ajar.";

        #endregion

        #region Overrides of StasisPod

        public override Item Instantiate()
        {
            CustomCommand enterStasisCommand = null;
            CustomCommand enableStasisCommand = null;

            enterStasisCommand = new CustomCommand(new CommandHelp(EnterStasisCommandName, $"Enter stasis in {Name}."), false, false, (g, _) =>
            {
                if (!g.VariableManager.ContainsVariable(EnteredStasisCVairableName))
                {
                    g.VariableManager.Add(new Variable(EnteredStasisCVairableName, true.ToString()));

                    var sanity = g.Player.Attributes.GetValue(NaomiTemplate.SanityAttributeName);

                    // stasis reduces sanity first time
                    g.Player.Attributes.Set(NaomiTemplate.SanityAttributeName, sanity + 1);
                }

                // unlock the lab
                g.Overworld.CurrentRegion.UnlockDoorPair(Direction.West);

                enterStasisCommand.IsPlayerVisible = false;
                var reaction = PlayableCharacterManager.Switch(AnneTemplate.Identifier, g);

                if (reaction.Result == ReactionResult.Error)
                    return reaction;

                // jump to annes stasis
                g.Overworld.FindRegion(Awaji.Name, out var region);
                reaction = g.Overworld.Move(region);

                if (reaction.Result == ReactionResult.Error)
                    return reaction;

                // because the character switch makes the introduction be shown need to manually show it
                return new Reaction(ReactionResult.Inform, Island.Introduction);
            });

            enableStasisCommand = new CustomCommand(new CommandHelp(FlipBreakerCommandName, $"Flip the power breaker on {Name}."), false, false, (g, _) =>
            {
                g.NoteManager.Expire(StasisPodManual.StasisPodManualLogName);
                enterStasisCommand.IsPlayerVisible = true;
                enableStasisCommand.IsPlayerVisible = false;
                return new Reaction(ReactionResult.Inform, $"Flipping the breaker on {Name} to the 'On' position powers up the stasis pod. Its internal lights flicker on and the internal controls light up.");
            });

            CustomCommand[] commands = 
            [
                enterStasisCommand,
                enableStasisCommand
            ];

            return new Item(Name, Description, examination: examination, commands: commands, interaction: DefaultInteraction);
        }

        #endregion
    }
}
