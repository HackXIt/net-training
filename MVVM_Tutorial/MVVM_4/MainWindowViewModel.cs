using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MVVM_4
{
    public class MainWindowViewModel : INotifyPropertyChanged
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
        
        // Zusatz - War nicht im Tutorial enthalten
        // https://stackoverflow.com/questions/1196991/get-property-value-from-string-using-reflection
        // Generic Property for accessing property values using reflection
        // Details über den Typ dieser Property findet man hier: https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/indexers/
        private object this[string propertyName] => this.GetType().GetProperty(propertyName)?.GetValue(this, null);

        // EventHandler vom Interface INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // CallerMemberName ist ein CompilerService und übergibt automatisch den Namen des Members, welcher die Methode aufruft
        // Somit benötigt man keine Methoden-Überladung 
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName)) return;
            // Zusatz - war nicht im Tutorial enthalten
            // Check out this: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/verbatim
            // In regards to the special character '@' => It interprets the string as verbatim, making that ReSharper warning go away
            // Trace shows console output when running in Debugger
            // Reflection is used to get the value of the calling attribute
            Trace.WriteLine($"Changed {propertyName} to \"{this[propertyName]}\"");
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}