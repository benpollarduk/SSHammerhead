using NetAF.Targets.WPF.Themes;

namespace SSHammerhead.WPF
{
    public static class SSHammerheadThemes
    {
        public static FlowDocumentTheme SSHHTheme
        {
            get
            {
                FlowDocumentTheme theme = DefaultTheme.FlowDocument;

                var offset = 6;  //App.Settings.FontSizeModifier;

                theme.Monospace.FontSize += offset;
                theme.Paragraph.FontSize += offset;
                theme.Heading1.FontSize += offset;
                theme.Heading2.FontSize += offset;
                theme.Heading3.FontSize += offset;
                theme.Heading4.FontSize += offset;

                return theme;
            }
        }
    }
}
