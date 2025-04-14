using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SMWTimer {
    /// <summary>
    /// Interaktionslogik für ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window {
        public static List<TextBox> textBoxes;
        public HotkeyWindow hotkeyWindow;
        private MainWindow _mainWindow;

        public ConfigWindow() {
            InitializeComponent();
            InitTextBoxList();
            TBManager.CreateTBModels();
        }

        public void Init(MainWindow mainWindow) {
            _mainWindow = mainWindow;

            for (int i = 0; i < MainWindow.textElements.Count; i++) {
                if (MainWindow.textElements[i].GetType() == typeof(TextBlock)) {
                    TextBlock tb = (TextBlock)MainWindow.textElements[i];
                    textBoxes[i].Text = tb.Text;
                }
                else if (MainWindow.textElements[i].GetType() == typeof(TextBox)) {
                    TextBox tb = (TextBox)MainWindow.textElements[i];
                    textBoxes[i].Text = tb.Text;
                }
            }

            /*
            for (int i = 0; i < TBManager.tbModelsDic.Count; i++) {
                textBoxes[i].Text = TBManager.tbModelsDic[i].Text;
            }
            */
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e) {
            _mainWindow.tbHackTitle.Text = tbHackTitle.Text;
            _mainWindow.tbHack.Text = tbHack.Text;
            _mainWindow.tbAuthorTitle.Text = tbAuthorTitle.Text;
            _mainWindow.tbAuthor.Text = tbAuthor.Text;
            _mainWindow.tbExitTitle.Text = tbExitTitle.Text;
            _mainWindow.tbExitCurrent.Text = tbExitCurrent.Text;
            _mainWindow.tbExitSeparator.Text = tbExitSeparator.Text;
            _mainWindow.tbExitMax.Text = tbExitMax.Text;
            _mainWindow.tbLevelTimeTitle.Text = tbLevelTimeTitle.Text;
            MainWindow.levelTimer.ResetTimer();
            MainWindow.levelTimer.stopwatch.StartOffset = TimeSpan.Parse(tbLevelTime.Text);
            _mainWindow.tbLevelTime.Text = tbLevelTime.Text;
            _mainWindow.tbHackTimeTitle.Text = tbHackTimeTitle.Text;
            MainWindow.hackTimer.ResetTimer();
            MainWindow.hackTimer.stopwatch.StartOffset = TimeSpan.Parse(tbHackTime.Text);
            _mainWindow.tbHackTime.Text = tbHackTime.Text;
            TimerPropertiesManager.CreateConfig();

            Close();
        }

        public void UpdateHotkeys() {
            _mainWindow.StartStop.Key = HackHotkeys.StartStopTimer;
            _mainWindow.NewLevel.Key = HackHotkeys.NewLevelTimer;
            _mainWindow.ResetLevel.Key = HackHotkeys.ResetLevelTimer;
            _mainWindow.FullReset.Key = HackHotkeys.FullReset;
        }

        private void ButtonHotkeys_Click(object sender, RoutedEventArgs e) {
            if (hotkeyWindow == null) {
                hotkeyWindow = new HotkeyWindow();
                hotkeyWindow.Init(this);
                hotkeyWindow.Show();
            }
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
            TextAttributeWindow textAttributeWindow = new TextAttributeWindow();
            textAttributeWindow.Show();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) {
            mainGrid.Focus();
        }

        private void Window_Closed(object sender, EventArgs e) {
            if (hotkeyWindow != null) {
                hotkeyWindow.Close();
                hotkeyWindow = null;
            }

            _mainWindow.dispatcherKeyDown.Start();
            _mainWindow.configWindow = null;
        }

        private void InitTextBoxList() {
            textBoxes = new List<TextBox> {
                tbHackTitle,
                tbHack,
                tbAuthorTitle,
                tbAuthor,
                tbExitTitle,
                tbExitCurrent,
                tbExitSeparator,
                tbExitMax,
                tbLevelTimeTitle,
                tbLevelTime,
                tbHackTimeTitle,
                tbHackTime
            };
        }
    }
}