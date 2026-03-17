using System.Globalization;
using System.Windows.Data;

namespace SSHammerhead.WPF.Converters
{
    internal class IntToModifiedFlowDocumentThemeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var theme = SSHammerheadThemes.SSHHTheme;

            if (!int.TryParse(value?.ToString() ?? string.Empty, out var modifier))
                return theme;

            theme.Monospace.FontSize += modifier;
            theme.Paragraph.FontSize += modifier;
            theme.Heading1.FontSize += modifier;
            theme.Heading2.FontSize += modifier;
            theme.Heading3.FontSize += modifier;
            theme.Heading4.FontSize += modifier;

            return theme;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
