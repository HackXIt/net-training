namespace MVVM_10
{
    public class Person : NotifiableBaseObject
    {
        private string _firstname;

        public string Firstname
        {
            get => _firstname;
            set
            {
                if (value == _firstname) return;
                _firstname = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Fullname));
            }
        }

        private string _lastname;

        public string Lastname
        {
            get => _lastname;
            set
            {
                if (value == _lastname) return;
                _lastname = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Fullname));
            }
        }

        public string Fullname => $"{Firstname} {Lastname}";

        private string _department;
        public string Department
        {
            get => _department;
            set
            {
                if (value == _department) return;
                _department = value;
                RaisePropertyChanged();
            }
        }
    }
}