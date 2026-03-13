using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SSHammerhead.WPF
{
    /// <summary>
    /// Provides user settings.
    /// </summary>
    public class UserSettings : INotifyPropertyChanged
    {
        #region StaticFields

        private static readonly string SettingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NetAF", TroubleAboardTheSSHammerhead.Title, "usersettings.json");

        #endregion

        #region Fields

        private bool useCrtEffect = true;
        private bool useCrtScanlines = true;
        private double crtBrightness = 0.65;
        private double crtIntensity = 0.2;
        private double crtCurvatureIntensity = 0.3;
        private bool useSoundEffects = true;
        private float soundEffectVolume = 0.5f;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set if the Crt effect is used.
        /// </summary>
        public bool UseCrtEffect
        {
            get { return useCrtEffect; }
            set 
            { 
                useCrtEffect = value; 
                OnPropertyChanged(); 
            }
        }

        /// <summary>
        /// Get or set if the scanlines are used with the Crt effect.
        /// </summary>
        public bool UseCrtScanlines
        {
            get { return useCrtScanlines; }
            set
            {
                useCrtScanlines = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Get or set the brightness are used with the Crt effect as a normalised value between 0 and 1.
        /// </summary>
        public double CrtBrightness
        {
            get { return crtBrightness; }
            set
            {
                crtBrightness = Clamp(value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Get or set the intensity are used with the Crt effect as a normalised value between 0 and 1.
        /// </summary>
        public double CrtIntensity
        {
            get { return crtIntensity; }
            set
            {
                crtIntensity = Clamp(value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Get or set the curvature intensity are used with the Crt effect as a normalised value between 0 and 1.
        /// </summary>
        public double CrtCurvatureIntensity
        {
            get { return crtCurvatureIntensity; }
            set
            {
                crtCurvatureIntensity = Clamp(value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Get or set if the sound effects are used.
        /// </summary>
        public bool UseSoundEffects
        {
            get { return useSoundEffects; }
            set
            {
                useSoundEffects = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Get or set the sound effect volume as a normalised value between 0 and 1.
        /// </summary>
        public float SoundEffectVolume
        {
            get { return soundEffectVolume; }
            set
            {
                soundEffectVolume = Clamp(value);
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reset the visual settings to their default values.
        /// </summary>
        public void ResetDefaultVisualSettings()
        {
            var defaults = new UserSettings();

            UseCrtEffect = defaults.UseCrtEffect;
            UseCrtScanlines = defaults.UseCrtScanlines;
            CrtBrightness = defaults.CrtBrightness;
            CrtIntensity = defaults.CrtIntensity;
            CrtCurvatureIntensity = defaults.CrtCurvatureIntensity;
        }

        /// <summary>
        /// Reset the audio settings to their default values.
        /// </summary>
        public void ResetDefaultAudioSettings()
        {
            var defaults = new UserSettings();

            UseSoundEffects = defaults.UseSoundEffects;
            SoundEffectVolume = defaults.SoundEffectVolume;
        }

        /// <summary>
        /// Save the settings to file.
        /// </summary>
        public void Save()
        {
            var directory = Path.GetDirectoryName(SettingsFilePath);

            if (!Directory.Exists(directory) && directory != null)
                Directory.CreateDirectory(directory);

            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, json);
        }

        #endregion

        #region StaticMethods

        private double Clamp(double value)
        {
            return Math.Min(1, Math.Max(0, value));
        }

        private float Clamp(float value)
        {
            return Math.Min(1, Math.Max(0, value));
        }

        /// <summary>
        /// Load user settings from file.
        /// </summary>
        /// <returns>The loaded settings.</returns>
        public static UserSettings Load()
        {
            if (!File.Exists(SettingsFilePath))
                return new UserSettings();

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

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
