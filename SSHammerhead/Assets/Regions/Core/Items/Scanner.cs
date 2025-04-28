using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;
using System.Linq;
using System.Text;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class Scanner : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Scanner";
        private const string Description = "A small hand held scanner designed for analysing the composition of objects.";

        #endregion

        #region StaticMethods

        internal static Interaction PerformScan(Composition composition)
        {
            StringBuilder description = new();

            description.AppendLine($"Scanned {composition.Name}, composition is as follows:");

            foreach (var element in composition.Elements.OrderByDescending(x => x.Value))
                description.AppendLine($"-{element.Key}: {element.Value}%");

            var remaining = 100 - composition.Elements.Values.Sum();

            if (remaining > 0)
                description.AppendLine($"-Unknown: {remaining}%");

            return new Interaction(InteractionResult.NoChange, null, description.ToString());
        }

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            return new Item(Name, Description, true, interaction: (item) =>
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
