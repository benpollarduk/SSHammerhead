using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Utilities;
using SSHammerhead.Logic.Modes;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    internal class LockedMaintenanceControlPanel : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Locked Maintenance Control Panel";
        private const string Description = "A small control panel for the Spider Bot maintenance system. " +
            "It has a very basic black and green display but it is functional enough to allow the user to remotely " +
            "control the Spider Bot system. The system requires a password to unlock.";

        #endregion

        #region StaticProperties

        private static readonly Dictionary<string, float> Composition = new()
        {
            { "Steel", 5.68f },
            { "Aluminum", 4.82f },
            { "Glass", 21.1f },
            { "Copper", 4.3f },
            { "Zinc", 3.64f },
            { "Plastic", 76.4f },
            { "Silver", 0.2f },
            { "Gold", 0.03f },
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            InteractionCallback interation = (item) =>
            {
                if (Scanner.Name.EqualsIdentifier(item.Identifier))
                    return Scanner.PerformScan(new(Name, Composition));

                if (Hammer.Name.EqualsIdentifier(item.Identifier))
                    return new Interaction(InteractionResult.NoChange, item, $"Smacking the control panel won't unlock it, a label on the side of it proudly states that it is 'Utility Tested Tough!'.");
                    

                return new Interaction(InteractionResult.NoChange, item);
            };

            Item item = null;
            item = new(Name, Description, interaction: interation, commands:
            [
                new CustomCommand(new CommandHelp("Login", "Login to the maintenance control system", instructions: "Attempt to login to the maintenance control system"), true, true, (game, arguments) =>
                {
                    game.ChangeMode(new BotLoginMode());
                    return new Reaction(ReactionResult.Silent, "Switched mode.");
                })
            ]);

            return item;
        }

        #endregion
    }
}
