using System.Windows;

namespace MVVM_10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            // ShowError aufrufen wenn das Event für fehlende Daten gesendet wird.
            ((PersonListViewModel)DataContext).MissingData += (sender, eventArgs) => ShowError();
        }

        public void ShowError()
        {
            // Dies ist eine Aktion, die ausschließlich die View betrifft
            MessageBox.Show("Please enter value at firstname and lastname");
        }
    }
}