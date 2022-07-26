using System;
using System.Globalization;
using System.Windows.Data;

namespace MVVM_6.ValueConverters
{
    public class CurrencyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal val = 0;
            if (value != null)
            {
                val = (decimal)value;
            }

            return val.ToString("0.00") + (string)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (string)value ?? string.Empty;
            val = val.Replace((string)parameter ?? string.Empty, "").Trim();
            var res = decimal.Parse(val);
            return res;
        }
    }
}