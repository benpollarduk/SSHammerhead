using NetAF.Logging.Notes;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SSHammerhead.WPF.Controls
{
    /// <summary>
    /// Interaction logic for NoteManagerViewer.xaml
    /// </summary>
    public partial class NoteManagerViewer : UserControl
    {
        #region Properties

        /// <summary>
        /// Get or set the recent entries. This is a depencency property.
        /// </summary>
        public ObservableCollection<NoteEntry> RecentEntries
        {
            get { return (ObservableCollection<NoteEntry>)GetValue(RecentEntriesProperty); }
            set { SetValue(RecentEntriesProperty, value); }
        }

        #endregion

        #region DependencyProperties

        /// <summary>
        /// Identifies the NoteManagerViewer.RecentEntries property.
        /// </summary>
        public static readonly DependencyProperty RecentEntriesProperty = DependencyProperty.Register(nameof(RecentEntries), typeof(ObservableCollection<NoteEntry>), typeof(NoteManagerViewer), new PropertyMetadata(new ObservableCollection<NoteEntry>()));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the NoteManagerViewer class.
        /// </summary>
        public NoteManagerViewer()
        {
            InitializeComponent();
        }

        #endregion
    }
}
