using NetAF.Logic;
using System.IO;
using System.Windows.Controls;

namespace SSHammerhead.WPF.Controls
{
    /// <summary>
    /// Interaction logic for PersistenceControl.xaml
    /// </summary>
    public partial class PersistenceControl : UserControl
    {
        public PersistenceControl(Game game)
        {
            InitializeComponent();

            FileManager.SelectedDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NetAF");
            FileManager.Setup(game);
        }
    }
}
