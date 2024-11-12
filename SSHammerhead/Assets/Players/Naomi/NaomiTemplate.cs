using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Extensions;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;
using NetAF.Commands.Persistence;
using NetAF.Commands;
using NetAF.Rendering.FrameBuilders.Console;

namespace SSHammerhead.Assets.Players.Naomi
{
    public class NaomiTemplate : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static Identifier Identifier => new(Name);
        public const string ErrorPrefix = "Uh-oh";
        private const string Name = "Naomi";
        private const string Description = "You, Naomi Martin, are a 32-year-old shuttle mechanic.";

        public static FrameBuilderCollection FrameBuilderCollection
        {
            get
            {
                var gridLayoutBuilder = new GridStringBuilder();

                return new FrameBuilderCollection(
                    new ConsoleTitleFrameBuilder(gridLayoutBuilder),
                    new ConsoleSceneFrameBuilder(gridLayoutBuilder, new ConsoleRoomMapBuilder(gridLayoutBuilder)),
                    new ConsoleRegionMapFrameBuilder(gridLayoutBuilder, new ConsoleRegionMapBuilder(gridLayoutBuilder)),
                    new ConsoleHelpFrameBuilder(gridLayoutBuilder),
                    new ConsoleCompletionFrameBuilder(gridLayoutBuilder),
                    new ConsoleGameOverFrameBuilder(gridLayoutBuilder),
                    new ConsoleAboutFrameBuilder(gridLayoutBuilder),
                    new ConsoleTransitionFrameBuilder(gridLayoutBuilder),
                    new ConsoleConversationFrameBuilder(gridLayoutBuilder));
            }
        }

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            CustomCommand[] commands =
            [
                new Save() { IsPlayerVisible = false },
                new LoadWithRestore() { IsPlayerVisible = false }
            ];

            return new(Name, Description, [new Hammer().Instantiate(), new Mirror().Instantiate()], commands: commands, interaction: (i) =>
            {
                if (i == null)
                    return new InteractionResult(InteractionEffect.NoEffect, null);

                if (Hammer.Name.EqualsIdentifier(i.Identifier))
                    return new InteractionResult(InteractionEffect.FatalEffect, i, "You swing wildly at your own head. The first few blows connect and knock you down. You are dead.");

                if (Mirror.Name.EqualsIdentifier(i.Identifier))
                    return new InteractionResult(InteractionEffect.NoEffect, i, "Peering in to the mirror you can see yourself looking back through your helmets visor.");

                return new InteractionResult(InteractionEffect.NoEffect, i);
            });
        }

        #endregion
    }
}
