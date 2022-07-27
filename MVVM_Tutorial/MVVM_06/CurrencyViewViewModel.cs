using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVVM_06
{
    public class CurrencyViewViewModel : BaseViewModel
    {
        // Additionally to the tutorial, I added an ENUM for currency selection
        public IEnumerable<Currency> Currencies => Enum.GetValues(typeof(Currency)).Cast<Currency>();
        
        private Currency _selectedCurrency = Currency.None;
        public Currency SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                if(value == _selectedCurrency) return;
                _selectedCurrency = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(SelectedCurrencyString));
            }
        }

        public string SelectedCurrencyString
        {
            get
            {
                switch (SelectedCurrency)
                {
                    case Currency.None:
                        return string.Empty;
                    case Currency.Euro:
                        return "€";
                    case Currency.Dollar:
                        return "$";
                    case Currency.Yen:
                        return "¥";
                    case Currency.Pound:
                        return "£";
                    default:
                        return string.Empty;
                }
            }
        }

        private decimal _value;

        public decimal Value
        {
            get => _value;
            set
            {
                if (value == _value) return;
                _value = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(HasNonZeroValue));
            }
        }

        public bool HasNonZeroValue => Value != 0.0m;
    }
}