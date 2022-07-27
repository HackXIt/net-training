namespace MVVM_08
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            ClearCommand = new DelegateCommand(
                (o) => !string.IsNullOrEmpty(Firstname) && !string.IsNullOrEmpty(Lastname), 
                (o => { this.Firstname = ""; this.Lastname = ""; }));
            Firstname = "Nick";
            Lastname = "Developer";
        }
        
        public DelegateCommand ClearCommand { get; set; }

        // Da durch das PropertyChanged ein spezieller Setter benötigt wird, kann man nicht direkt Auto-Properties verwenden
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                if (_firstname == value) return; // Don't change if value stayed the same
                _firstname = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(Fullname));
                // Da wir folgenden Code mehrfach verwenden müssten, wird dieser in eine Methode ausgelagert
                // this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Firstname)));
                // this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Fullname)));
                this.ClearCommand.RaiseCanExecuteChanged();
            }
        }

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string _lastname;
        
        public string Lastname { get => _lastname;
            set
            {
                if (_lastname == value) return;
                _lastname = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(Fullname));
            }
        }

        public string Fullname => $"{Firstname} {Lastname}";
    }
}