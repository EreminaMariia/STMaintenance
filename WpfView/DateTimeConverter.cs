using System.Globalization;
using System;
using System.Windows.Data;
using System.ComponentModel.DataAnnotations;

namespace WpfView;

public class DateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime)
        {
            var test = (DateTime)value;
            return test == DateTime.MinValue ? string.Empty : test.ToString("dd.MM.yyyy HH:mm:ss");
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (string.IsNullOrEmpty(value.ToString()))
        {
            return null;
        }

        if (DateTime.TryParseExact(value.ToString(), "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            return result;
        }
        else
        {
            return new ValidationResult("Неверная дата");
        }
    }
}
