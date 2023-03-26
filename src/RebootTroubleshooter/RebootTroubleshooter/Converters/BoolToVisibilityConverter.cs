using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using Microsoft.VisualBasic;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RebootTroubleshooter.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = System.Convert.ToBoolean(value);
            bool isInverted = false;

            if (parameter is string parameterString && parameterString.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
            {
                isInverted = true;
            }

            if (isInverted)
            {
                isVisible = !isVisible;
            }

            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
