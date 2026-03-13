namespace SSHammerhead.WPF
{
    public partial class App : Application
    {
        public static UserSettings Settings { get; private set; } = new UserSettings();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Load settings when app starts
            Settings = UserSettings.Load();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Save settings when app closes
            Settings.Save();
            
            base.OnExit(e);
        }
    }

    public class UserSettings : INotifyPropertyChanged
    {
        private bool _enableMusic;
        private double _volume;

        public bool EnableMusic
        {
            get { return _enableMusic; }
            set
            {
                _enableMusic = value;
                OnPropertyChanged(nameof(EnableMusic));
            }
        }

        public double Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }

        public static UserSettings Load()
        {
            // Load settings from storage
            return new UserSettings();
        }

        public void Save()
        {
            // Save settings to storage
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}