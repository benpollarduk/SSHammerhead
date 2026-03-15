using NetAF.Logic;
using SSHammerhead.Assets.Regions.Ship.Items;
using System.Windows;

namespace SSHammerhead.WPF
{
    /// <summary>
    /// Interaction logic for ApplicationSettingsWindow.xaml
    /// </summary>
    public partial class ApplicationSettingsWindow : Window
    {
        #region Constructors

        public ApplicationSettingsWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region CommandCallbacks

        private void ResetVisualSettingsCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            App.Settings.ResetDefaultVisualSettings();
        }

        private void ResetAudioSettingsCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            App.Settings.ResetDefaultAudioSettings();
        }

        private void TestSoundEffectVolumeCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            AudioPlayer.PlaySoundEffect(Audio.SoundEffect.KeyPressCharacterRandom, App.Settings.SoundEffectVolume);
        }

        #endregion

        #region EventHandlers

        private void BackgroundMusicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AudioPlayer.AdjustBackgroundMusic((float)e.NewValue, Radio.DetermineProximity(GameExecutor.ExecutingGame));
        }

        #endregion
    }
}
