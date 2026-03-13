using System.Globalization;
using System.Windows.Data;

namespace SSHammerhead.WPF.Converters
{
    public class DoubleRoundingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!double.TryParse(value?.ToString() ?? string.Empty, out var v))
                return value;

            if (!int.TryParse(parameter?.ToString() ?? string.Empty, out var p))
                p = 0;

            return Math.Round(v, p);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
