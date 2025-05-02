using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class StasisPodC(ExaminationCallback examination) : StasisPod(Name, Description)
    {
        #region Constants

        internal const string Name = "Stasis Pod (C)";

        #endregion

        #region StaticProperties

        private static readonly string Description = $"The third of the Stasis pods.{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"{DefaultDescription}{StringUtilities.Newline}{StringUtilities.Newline}" +
            $"The door to this pod is unlocked and slightly ajar.";

        #endregion

        #region Overrides of StasisPod

        public override Item Instantiate()
        {
            return new Item(Name, Description, examination: examination, interaction: (item) =>
            {
                if (Hammer.Name.EqualsIdentifier(item.Identifier))
                    return new Interaction(InteractionResult.NoChange, item, $"The pod is reinforced, it will take more than a swing from a {Hammer.Name} to break it.");

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
