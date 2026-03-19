using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SSHammerhead.WPF
{
    public class UserSettings : INotifyPropertyChanged
    {
        private static readonly string SettingsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "SSHammerhead", "usersettings.json");

        private int _volume = 80;
        public int Volume
        {
            get => _volume;
            set { _volume = value; OnPropertyChanged(); }
        }

        private bool _enableMusic = true;
        public bool EnableMusic
        {
            get => _enableMusic;
            set { _enableMusic = value; OnPropertyChanged(); }
        }

        // --- Load / Save Logic ---

        public static UserSettings Load()
        {
            if (File.Exists(SettingsFilePath))
            {
                try
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
                }
                catch
                {
                    // Fallback to defaults if file is corrupted
                    return new UserSettings();
                }
            }
            return new UserSettings();
        }

        public void Save()
        {
            var directory = Path.GetDirectoryName(SettingsFilePath);
            if (!Directory.Exists(directory) && directory != null)
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, json);
        }

        // --- INotifyPropertyChanged implementation ---

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}