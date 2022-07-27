using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MVVM_09
{
    public class NotifiableBaseObject : INotifyPropertyChanged
    {
        // Zusatz - War nicht im Tutorial enthalten
        // https://stackoverflow.com/questions/1196991/get-property-value-from-string-using-reflection
        // Generic Property for accessing property values using reflection
        // Details über den Typ dieser Property findet man hier: https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/indexers/
        private object this[string propertyName] => this.GetType().GetProperty(propertyName)?.GetValue(this, null);

        // EventHandler vom Interface INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // CallerMemberName ist ein CompilerService und übergibt automatisch den Namen des Members, welcher die Methode aufruft
        // Somit benötigt man keine Methoden-Überladung 
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
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