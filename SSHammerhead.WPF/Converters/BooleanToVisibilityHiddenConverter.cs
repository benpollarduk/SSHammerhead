using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SSHammerhead.WPF.Converters
{
    internal class BooleanToVisibilityHiddenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!bool.TryParse(value?.ToString(), out var b))
                return Visibility.Visible;

            return b ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
