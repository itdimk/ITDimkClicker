using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace ITDimkClicker.App.Converters
{
    public class FileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return Path.GetFileName(value.ToString());
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }
    }
}