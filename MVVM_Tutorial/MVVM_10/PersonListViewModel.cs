using System;
using System.Collections.ObjectModel;

namespace MVVM_10
{
    public class PersonListViewModel : NotifiableBaseObject
    {
        // Neue Event-Property MissingData
        public event EventHandler MissingData;
        
        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();

        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set
            {
                if(_persons == value) return;
                _persons = value;
                RaisePropertyChanged();
            }
        }

        private Person _newPerson = new Person();
        public Person NewPerson
        {
            get => _newPerson;
            set
            {
                if (_newPerson == value) return;
                _newPerson = value;
                RaisePropertyChanged();
                AddPersonCommand.RaiseCanExecuteChanged(); // Damit der Button ggf. deaktiviert wird.
            }
        }

        public DelegateCommand AddPersonCommand
        {
            get;
            set;
        }

        public PersonListViewModel()
        {
            // In der Verarbeitung des Buttons werden die Person-Daten überprüft
            AddPersonCommand = new DelegateCommand(
                (o => !Persons.Contains(NewPerson)),
                (o =>
            {
                // Bei unzureichender Daten-Eingabe wird das MissingData Event geworfen
                if(string.IsNullOrWhiteSpace(NewPerson.Firstname) || string.IsNullOrWhiteSpace(NewPerson.Lastname))
                    MissingData?.Invoke(this, EventArgs.Empty);
                else
                {
                    Persons.Add(NewPerson);
                    NewPerson = new Person();
                }
            }));
        }
    }
}