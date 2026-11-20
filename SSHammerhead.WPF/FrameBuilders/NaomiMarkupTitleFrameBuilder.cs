using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Markup;
using NetAF.Targets.Markup.Rendering;
using SSHammerhead.Resources;
using System.Diagnostics;

namespace SSHammerhead.WPF.FrameBuilders
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupTitleFrameBuilder(MarkupBuilder builder) : ITitleFrameBuilder
    {
        #region Implementation of ITitleFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string description, Size size)
        {
            builder.Clear();

            builder.Heading(title, HeadingLevel.H1);
            builder.Newline();
            builder.WriteLine(description);

            try
            {
                var imageBuilder = GridVisualBuilder.FromString(Images.Title);
                var imageAsMarkup = MarkupAdapter.ConvertGridVisualBuilderToMarkupString(imageBuilder);
                builder.Raw(imageAsMarkup);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception caught appending image: {e.Message}");
            }

            builder.Newline();

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
