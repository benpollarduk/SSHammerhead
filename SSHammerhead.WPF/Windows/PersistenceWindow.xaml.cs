using NetAF.Logic;
using System.IO;
using System.Windows;

namespace SSHammerhead.WPF
{
    /// <summary>
    /// Interaction logic for PersistenceWindow.xaml
    /// </summary>
    public partial class PersistenceWindow : Window
    {
        public PersistenceWindow(Game game)
        {
            InitializeComponent();

            FileManager.SelectedDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NetAF");
            FileManager.Setup(game);
        }
    }
}
