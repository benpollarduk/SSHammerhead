using NetAF.Logic;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Audio;
using System.Windows;
using System.Windows.Controls;

namespace SSHammerhead.WPF.Controls
{
    /// <summary>
    /// Interaction logic for ApplicationSettingsControl.xaml
    /// </summary>
    public partial class ApplicationSettingsControl : UserControl
    {
        #region Constructors

        public ApplicationSettingsControl()
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

        private void ResetControlSettingsCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            App.Settings.ResetDefaultControlSettings();
        }

        private void ResetPersistenceSettingsCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            App.Settings.ResetDefaultPersistenceSettings();
        }

        private void TestSoundEffectVolumeCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            AudioPlayer.PlaySoundEffect(Audio.SoundEffect.KeyPressCharacterRandom, App.Settings.SoundEffectVolume);
        }

        private void RestartGameCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            GameExecutor.Restart();
        }

        private void ClearHistoryCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            GameExecutor.ExecutingGame?.HistoryManager?.Clear();
        }

        private void ClearNotesCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            GameExecutor.ExecutingGame?.NoteManager?.Clear();
        }

        private void CleanNotesCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            GameExecutor.ExecutingGame?.NoteManager?.Clean();
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
