namespace MVVM_07
{
    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname => $"{Firstname} {Lastname}";
        public string Department { get; set; }
    }
}