using System;
using System.Globalization;
using System.Windows.Data;

namespace MVVM_6.ValueConverters
{
    public class MultiCurrencyValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (decimal)values[0];
            var currencySymbol = (string)values[1];

            return val.ToString("0.00") + currencySymbol;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var val = (string)value;
            var currencySymbol = val.Substring(-1, 1);
            var currencyValue = decimal.Parse(val.Substring(0, val.Length - 1));
            Currency currency = Currency.None;
            switch (currencySymbol)
            {
                case "€":
                    // Here the actual value conversion TO EUROS would happen (1:1)
                    currency = Currency.Euro;
                    break;
                case "$":
                    // Here the actual value conversion TO DOLLARS would happen (1:?)
                    currency = Currency.Dollar;
                    break;
                case "¥":
                    // Here the actual value conversion TO YEN would happen (1:?)
                    currency = Currency.Yen;
                    break;
                case "£":
                    // Here the actual value conversion TO POUND would happen (1:?
                    currency = Currency.Pound;
                    break;
                default:
                    // In any other case, there was no currency symbol and we should parse the whole string as number
                    currencyValue = decimal.Parse(val);
                    break;
            }
            return new object[] {currencyValue, currency};
        }
    }
}