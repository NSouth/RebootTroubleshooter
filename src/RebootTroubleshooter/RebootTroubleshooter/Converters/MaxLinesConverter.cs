using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace RebootTroubleshooter.Converters
{
    public class MaxLinesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null!;

            if (!int.TryParse(parameter as string, out int maxLines))
                throw new ArgumentException("MaxLines parameter must be an integer.");

            string? text = value?.ToString()?.Trim();

            if (string.IsNullOrEmpty(text))
                return null!;

            string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            if (lines.Length <= maxLines)
                return text;

            return string.Join(Environment.NewLine, lines.Take(maxLines)) + Environment.NewLine + "...";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
