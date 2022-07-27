namespace MVVM_02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// In diese Code-behind Datei gehören NUR visuelle Programmierungen der View
    /// Alles was irgendwie in Logik abgebildet werden kann, gehört in das ViewModel
    /// Meist steht hier nichts.
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            // Den DataContext könnte man auch hier initialisieren
            // this.DataContext = new MainWindowViewModel();
            // Jedoch tun wir das nicht, da wir MVVM ernst nehmen wollen
        }
    }
}