using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Utilities;
using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Ship.Items
{
    internal class LaserBarrier : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Laser Barrier";
        private const string Description = "An impenetrable laser barrier. Dangerous red lasers emit from all points around the perimeter of the barrier. It has a small touch panel that allows the user to enter 6 two-digit hexadecimal numbers.";
        private const string UnlockCode = "7F-DA-13-FA-B4-82";

        #endregion

        #region StaticProperties

        internal static Dictionary<string, float> Composition => new()
        {
            { "Steel", 10.11f },
            { "Aluminum", 64.21f },
            { "Copper", 1.13f },
            { "Glass", 4.21f },
            { "Zinc", 2.74f },
            { "Plastics", 6.44f }
        };

        #endregion

        #region Implementation of IAssetTemplate<Item>

        public Item Instantiate()
        {
            Item item = null;

            CustomCommand[] commands =
            [
                new CustomCommand(new CommandHelp("Enter Code", "Enter an unlock code."), true, false, (g, a) =>
                {
                    if (a.Length == 0)
                        return new Reaction(ReactionResult.Error, "No code was entered.");

                    string formattedCode;

                    if (a.Length == 1)
                    {
                        formattedCode = a[0];
                    }
                    else
                    {
                        formattedCode = string.Empty;

                        foreach (var arg in a)
                            formattedCode += arg + ' ';

                        formattedCode = formattedCode.TrimEnd(' ');
                    }

                    formattedCode = formattedCode.Replace(' ', '-');

                    if (formattedCode.InsensitiveEquals(UnlockCode))
                    {
                        item.IsPlayerVisible = false;
                        return new Reaction(ReactionResult.Inform, "The emitters around the barrier deactivate, the barrier is now disabled.");
                    }

                    return new Reaction(ReactionResult.Error, "An incorrect code was entered.");
                })
            ];

            item = new Item(Name, Description, false, commands: commands);

            return item;
        }

        #endregion
    }
}
