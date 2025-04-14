using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SMWTimer {
    /// <summary>
    /// Interaktionslogik für HotkeyWindow.xaml
    /// </summary>
    public partial class HotkeyWindow : Window {
        private ConfigWindow _configWindow;
        private TextBlock focusedTextBlock;

        public HotkeyWindow() {
            InitializeComponent();
        }

        public void Init(ConfigWindow configWindow) {
            _configWindow = configWindow;
            tbStartStop.Text = HackHotkeys.StartStopTimer.ToString();
            tbNewLevel.Text = HackHotkeys.NewLevelTimer.ToString();
            tbResetLevel.Text = HackHotkeys.ResetLevelTimer.ToString();
            tbFullReset.Text = HackHotkeys.FullReset.ToString();
        }

        private void Window_Closed(object sender, EventArgs e) {
            _configWindow.hotkeyWindow = null;
        }

        private void ChangeHotkey_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            TextBlock tb = (TextBlock)sender;
            if (focusedTextBlock != tb) {
                if (focusedTextBlock != null) {
                    focusedTextBlock.Background = Brushes.White;
                }

                tb.Background = Brushes.LightGreen;
                focusedTextBlock = tb;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (focusedTextBlock != null) {
                focusedTextBlock.Text = e.Key.ToString();
                focusedTextBlock.Background = Brushes.White;
                focusedTextBlock = null;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e) {
            KeyConverter keyConverter = new KeyConverter();
            HackHotkeys.StartStopTimer = (Key)keyConverter.ConvertFromString(tbStartStop.Text);
            HackHotkeys.NewLevelTimer = (Key)keyConverter.ConvertFromString(tbNewLevel.Text);
            HackHotkeys.ResetLevelTimer = (Key)keyConverter.ConvertFromString(tbResetLevel.Text);
            HackHotkeys.FullReset = (Key)keyConverter.ConvertFromString(tbFullReset.Text);
            HackHotkeys.CreateHotkeys();

            _configWindow.UpdateHotkeys();

            Close();
        }
    }
}