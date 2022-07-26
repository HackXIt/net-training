using System.Collections.ObjectModel;

namespace MVVM_8
{
    public class PersonListViewModel : BaseViewModel
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
            }
        }

        public DelegateCommand AddPersonCommand
        {
            get;
            set;
        }

        public PersonListViewModel()
        {
            AddPersonCommand = new DelegateCommand((o =>
            {
                Persons.Add(NewPerson);
                NewPerson = new Person();
            }));
        }
    }
}