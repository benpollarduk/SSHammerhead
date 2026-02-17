using NetAF.Targets.WPF.Themes;
using System.Windows.Media;

namespace SSHammerhead.WPF
{
    public static class SSHammerheadThemes
    {
        public static FlowDocumentTheme SSHHTheme
        {
            get
            {
                FlowDocumentTheme theme = DefaultTheme.FlowDocument;
                /*
                var foreground = new SolidColorBrush(Color.FromRgb(180, 180, 255));
                var fontFamily = new FontFamily("DIN OT");

                theme.Heading1.Foreground = foreground;
                theme.Heading2.Foreground = foreground;
                theme.Heading3.Foreground = foreground;
                theme.Heading4.Foreground = foreground;
                theme.Paragraph.Foreground = foreground;

                theme.Heading1.FontFamily = fontFamily;
                theme.Heading2.FontFamily = fontFamily;
                theme.Heading3.FontFamily = fontFamily;
                theme.Heading4.FontFamily = fontFamily;
                theme.Paragraph.FontFamily = fontFamily;*/
                return theme;
            }
        }
    }
}
