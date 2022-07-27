using System;
using System.Windows.Input;

namespace MVVM_05
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public DelegateCommand(Predicate<object> canExecute, Action<object> execute) =>
            (this._canExecute, this._execute) = (canExecute, execute);
        
        public DelegateCommand(Action<object> execute) : this(null, execute) { }
        
        public void RaiseCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        
        #region ICommand
        
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => this._execute?.Invoke(parameter);

        #endregion
    }
}