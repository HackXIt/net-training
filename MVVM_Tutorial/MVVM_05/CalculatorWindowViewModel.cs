using System;
using System.Diagnostics;

namespace MVVM_05
{
    public class CalculatorWindowViewModel : BaseViewModel
    {
        private const double Tolerance = 0.0000000001;
        private const double Decimal = 10;

        private double _currentValue;

        public double CurrentValue
        {
            get => _currentValue;
            set
            {
                if (Math.Abs(_currentValue - value) < Tolerance) return;
                _currentValue = value;
                RaisePropertyChanged();
            }
        }

        private double _lastValue;
        private string _operatorToExecute;
        private bool _floatingPoint = false;
        private double _floatingPointMultiplier = 1;
        
        public DelegateCommand NumberCommand { get; set; }
        public DelegateCommand OperatorCommand { get; set; }

        public CalculatorWindowViewModel()
        {
            this.NumberCommand = new DelegateCommand((value) =>
            {
                var val = double.Parse((string)value);
                if (_floatingPoint)
                {
                    _floatingPointMultiplier *= Decimal;
                    Trace.WriteLine($@"FloatingPointMultiplier: {_floatingPointMultiplier}");
                    Trace.WriteLine($@"OldValue: {CurrentValue}");
                    CurrentValue += val / _floatingPointMultiplier;
                }
                else
                {
                    CurrentValue = CurrentValue * Decimal + val; // Dezimal-System: Bestehende Zahl * 10 + neue Zahl
                }
            });
            this.OperatorCommand = new DelegateCommand((o) =>
            {
                var op = (string)o;
                switch (op)
                {
                    case "=":
                        CurrentValue = Calculate();
                        Trace.WriteLine($@"Result: {CurrentValue}");
                        break;
                    case ".":
                        _floatingPoint = true;
                        break;
                    case "~":
                        CurrentValue *= -1;
                        break;
                    case "AC":
                        AllClear();
                        break;
                    default:
                        _operatorToExecute = op;
                        ResetInput();
                        break;
                }
            });
        }
        private double Calculate()
        {
            Trace.WriteLine($@"Calculating: {_currentValue} {_operatorToExecute} {_lastValue}");
            switch (_operatorToExecute)
            {
                case "+":
                    return CurrentValue + _lastValue;
                case "-":
                    return _lastValue - CurrentValue;
                case "*":
                    return CurrentValue * _lastValue;
                case "/":
                    if (_lastValue == 0) return 0.0;
                    return CurrentValue / _lastValue;
                default:
                    return 0.0;
            }
        }

        private void AllClear()
        {
            _operatorToExecute = null;
            CurrentValue = 0.0;
            _lastValue = 0.0;
            _floatingPoint = false;
            _floatingPointMultiplier = 1;
        }

        private void ResetInput()
        {
            _lastValue = CurrentValue;
            CurrentValue = 0.0;
            _floatingPoint = false;
            _floatingPointMultiplier = 1;
        }
    }
}