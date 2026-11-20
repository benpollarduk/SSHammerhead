using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Html;
using NetAF.Targets.Html.Rendering;
using SSHammerhead.Resources;
using System.Diagnostics;

namespace SSHammerhead.Blazor.FrameBuilders
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class NaomiHtmlTitleFrameBuilder(HtmlBuilder builder) : ITitleFrameBuilder
    {
        #region Implementation of ITitleFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string description, Size size)
        {
            builder.Clear();

            builder.H1(title);
            builder.Br();
            builder.P(description);

            try
            {
                var imageBuilder = GridVisualBuilder.FromString(Images.Title);
                var imageAsHtml = HtmlAdapter.ConvertGridVisualBuilderToHtmlString(imageBuilder);
                builder.Raw(imageAsHtml);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception caught appending image: {e.Message}");
            }

            builder.Br();

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
