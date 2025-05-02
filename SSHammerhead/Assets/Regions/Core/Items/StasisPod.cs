using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal abstract class StasisPod(string name, string description) : IAssetTemplate<Item>
    {
        #region Constants

        protected const string DefaultDescription = "The stasis pod is a cylindrical, self-contained, life support system designed to accommodate a single person in a state of indefinite stasis.";

        #endregion

        #region StaticProperties

        protected static string DefaultHammerInteractionDescription => $"The pod is reinforced, it will take more than a swing from a {Hammer.Name} to break it.";

        internal static Dictionary<string, float> Composition => new()
        {
            { "Plastics", 47.21f },
            { "Aluminum", 34.39f },
            { "Steel", 3.27f },
            { "Perspex", 1.65f },
            { "Copper", 7.35f },
            { "Silver", 3.21f },
            { "Gold", 0.23f },
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public virtual Item Instantiate()
        {
            return new Item(name, description, interaction: (item) =>
            {
                if (Hammer.Name.EqualsIdentifier(item.Identifier))
                    return new Interaction(InteractionResult.NoChange, item, DefaultHammerInteractionDescription);

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}
