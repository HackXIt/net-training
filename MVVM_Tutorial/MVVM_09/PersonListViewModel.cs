using System.Collections.ObjectModel;

namespace MVVM_09
{
    public class PersonListViewModel : NotifiableBaseObject
    {
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
            AddPersonCommand = new DelegateCommand(
                (o => !Persons.Contains(NewPerson)),
                (o =>
            {
                Persons.Add(NewPerson);
                NewPerson = new Person();
            }));
        }
    }
}