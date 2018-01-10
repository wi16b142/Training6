using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Training6.Converter
{
    public class IntToBrush : IValueConverter
    {
        SolidColorBrush green = new SolidColorBrush(Colors.Green);
        SolidColorBrush yellow = new SolidColorBrush(Colors.Yellow);
        SolidColorBrush red = new SolidColorBrush(Colors.Red);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value < 3)
            {
                return green;
            } else if ((int)value == 3)
            {
                return yellow;
            } else return red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
