using System.Windows;

namespace SSHammerhead.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region StaticProperties

        /// <summary>
        /// Get the user settings.
        /// </summary>
        public static UserSettings Settings { get; private set; } = new UserSettings();

        #endregion

        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Settings = UserSettings.Load();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Settings.Save();

            base.OnExit(e);
        }

        #endregion
    }
}
