using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Extensions;
using NetAF.Rendering.FrameBuilders.Color;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using SSHammerhead.Assets.Regions.Core.Items;

namespace SSHammerhead.Assets.Players.Naomi
{
    public class NaomiTemplate : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        public static Identifier Identifier => new(Name);
        public const string ErrorPrefix = "Uh-oh";
        private const string Name = "Naomi";
        private const string Description = "You, Naomi Martin, are a 32 year old shuttle mechanic.";

        public static FrameBuilderCollection FrameBuilderCollection
        {
            get
            {
                var gridLayoutBuilder = new GridStringBuilder();

                return new FrameBuilderCollection(
                    new ColorTitleFrameBuilder(gridLayoutBuilder),
                    new ColorSceneFrameBuilder(gridLayoutBuilder, new ColorRoomMapBuilder()),
                    new ColorRegionMapFrameBuilder(gridLayoutBuilder, new ColorRegionMapBuilder()),
                    new ColorHelpFrameBuilder(gridLayoutBuilder),
                    new ColorCompletionFrameBuilder(gridLayoutBuilder),
                    new ColorGameOverFrameBuilder(gridLayoutBuilder),
                    new ColorAboutFrameBuilder(gridLayoutBuilder),
                    new ColorTransitionFrameBuilder(gridLayoutBuilder),
                    new ColorConversationFrameBuilder(gridLayoutBuilder));
            }
        }

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        public PlayableCharacter Instantiate()
        {
            var player = new PlayableCharacter(Name, Description, new Hammer().Instantiate(), new Mirror().Instantiate())
            {
                Interaction = (i) =>
                {
                    if (i == null)
                        return new InteractionResult(InteractionEffect.NoEffect, null);

                    if (Hammer.Name.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.FatalEffect, i, "You swing wildly at your own head. The first few blows connect and knock you down. You are dead.");

                    if (Mirror.Name.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, i, "Peering in to the mirror you can see yourself looking back through your helmets visor.");

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            return player;
        }

        #endregion
    }
}
