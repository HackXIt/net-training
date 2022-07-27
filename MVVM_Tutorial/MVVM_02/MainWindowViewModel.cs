namespace MVVM_02
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Firstname = "Dev";
            Lastname = "Nick";
        }
        
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Fullname => $"{Firstname} {Lastname}";
    }
}