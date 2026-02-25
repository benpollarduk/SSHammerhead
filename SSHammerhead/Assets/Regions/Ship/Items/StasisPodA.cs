using NetAF.Assets;
using NetAF.Commands;
using NetAF.Utilities;
using NetAF.Variables;
using SSHammerhead.Assets.Players.Management;
using SSHammerhead.Assets.Players.Naomi;
using SSHammerhead.Assets.Players.Scott;
using SSHammerhead.Assets.Regions.Stasis.Awaji.Rooms;
using SSHammerhead.Assets.Regions.Stasis.Space;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class StasisPodA() : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = $"Stasis Pod ({ScottTemplate.Name})";
        internal const string EnterStasisCommandName = "Enter Stasis (A)";
        private const string EnteredStasisAVairableName = "EnteredStasisC";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The first of the Stasis pods, belonging to {ScottTemplate.Name}.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod is closed but unlocked.";

        #endregion

        #region Overrides of StasisPod

        public override Item Instantiate()
        {
            var enterStasisCommand = new CustomCommand(new CommandHelp(EnterStasisCommandName, $"Enter stasis in {Name}."), false, false, (g, _) =>
            {
                if (!g.VariableManager.ContainsVariable(EnteredStasisAVairableName))
                {
                    g.VariableManager.Add(new Variable(EnteredStasisAVairableName, true.ToString()));

                    var sanity = g.Player.Attributes.GetValue(NaomiTemplate.SanityAttributeName);

                    // stasis reduces sanity first time
                    g.Player.Attributes.Set(NaomiTemplate.SanityAttributeName, sanity + 1);
                }

                var reaction = PlayableCharacterManager.Switch(ScottTemplate.Identifier, g);

                if (reaction.Result == ReactionResult.Error)
                    return reaction;

                // jump to scotts stasis
                g.Overworld.FindRegion(Space.Name, out var region);
                reaction = g.Overworld.Move(region);

                if (reaction.Result == ReactionResult.Error)
                    return reaction;

                // because the character switch makes the introduction be shown need to manually show it
                return new Reaction(ReactionResult.Inform, Island.Introduction);
            });

            CustomCommand[] commands =
            [
                enterStasisCommand
            ];

            return new Item(Name, Description, commands: commands, interaction: DefaultInteraction);
        }

        #endregion
    }
}
