using NetAF.Rendering.FrameBuilders.Color;
using NetAF.Rendering.FrameBuilders;
using NetAF.SSHammerhead.Assets.Players.FrameBuilders.Bot;

namespace NetAF.SSHammerhead.Assets.Players.FrameBuilders
{
    /// <summary>
    /// Provides a container from frame builder collections.
    /// </summary>
    public static class FrameBuilderCollections
    {
        /// <summary>
        /// Get the default frame builder collection for Naomi.
        /// </summary>
        public static FrameBuilderCollection Naomi
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

        /// <summary>
        /// Get the default frame builder collection for the maintenance bot.
        /// </summary>
        public static FrameBuilderCollection Bot
        {
            get
            {
                var gridLayoutBuilder = new GridStringBuilder();
                var botLayoutBuilder = new GridStringBuilder('[', ']', '+');

                return new FrameBuilderCollection(
                    new ColorTitleFrameBuilder(gridLayoutBuilder),
                    new BotColorSceneFrameBuilder(botLayoutBuilder, new  BotColorRoomMapBuilder()),
                    new BotColorRegionMapFrameBuilder(botLayoutBuilder, new BotColorRegionMapBuilder()),
                    new ColorHelpFrameBuilder(gridLayoutBuilder),
                    new ColorCompletionFrameBuilder(gridLayoutBuilder),
                    new ColorGameOverFrameBuilder(gridLayoutBuilder),
                    new ColorAboutFrameBuilder(gridLayoutBuilder),
                    new ColorTransitionFrameBuilder(gridLayoutBuilder),
                    new ColorConversationFrameBuilder(gridLayoutBuilder));
            }
        }
    }
}
