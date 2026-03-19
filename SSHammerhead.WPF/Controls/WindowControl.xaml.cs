using System.Windows;
using System.Windows.Controls;

namespace SSHammerhead.WPF.Controls
{
    /// <summary>
    /// Interaction logic for WindowControl.xaml
    /// </summary>
    public partial class WindowControl : UserControl
    {
        #region Properties

        /// <summary>
        /// Get or set the title. This is a depencency property.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Get or set the hosted content. This is a depencency property.
        /// </summary>
        public UIElement HostedContent
        {
            get { return (UIElement)GetValue(HostedContentProperty); }
            set { SetValue(HostedContentProperty, value); }
        }

        /// <summary>
        /// Get or set the corner radius. This is a depencency property.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// Occurs when this is closed.
        /// </summary>
        public event EventHandler? Closed = null;

        #endregion

        #region DependencyProperties

        /// <summary>
        /// Identifies the WindowControl.Title property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(WindowControl));

        /// <summary>
        /// Identifies the WindowControl.HostedContent property.
        /// </summary>
        public static readonly DependencyProperty HostedContentProperty = DependencyProperty.Register(nameof(HostedContent), typeof(UIElement), typeof(WindowControl));

        /// <summary>
        /// Identifies the WindowControl.CornerRadius property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(WindowControl));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the WindowControl class.
        /// </summary>
        public WindowControl()
        {
            InitializeComponent();
        }

        #endregion

        #region CommandCallbacks

        private void CloseCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region EventHandlers

        private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount != 1)
                return;

            Window.GetWindow(this)?.DragMove();
        }

        #endregion
    }
}
