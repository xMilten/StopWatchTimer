using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace SMWTimer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public static List<FrameworkElement> textElements;
        public ConfigWindow configWindow;
        public static HackTimer hackTimer;
        public static HackTimer levelTimer;
        public DispatcherTimer dispatcherKeyDown;
        private DispatcherTimer dispatcherGlow;
        private DispatcherTimer dispatcherAutoSave;
        private bool fillingOpacity;

        private InputKey _startStop;
        private InputKey _newLevel;
        private InputKey _resetLevel;
        private InputKey _fullReset;

        public InputKey StartStop { get => _startStop; }
        public InputKey NewLevel { get => _newLevel; }
        public InputKey ResetLevel { get => _resetLevel; }
        public InputKey FullReset { get => _fullReset; }

        public MainWindow() {
            InitializeComponent();
            InitTextElementList();
            HackHotkeys.InitHotkeys();
            TimerPropertiesManager.InitConfig();
            hackTimer = new HackTimer(tbHackTime);
            levelTimer = new HackTimer(tbLevelTime);
            InitKeys();
            StartDispatcherKeyDown();
            InitDispatcherGlowOnPause();
            StartDispatcherAutoSave();
        }

        private void InitKeys() {
            _startStop = new InputKey(HackHotkeys.StartStopTimer, StartStopTimer);
            _newLevel = new InputKey(HackHotkeys.NewLevelTimer, NewLevelTimer);
            _resetLevel = new InputKey(HackHotkeys.ResetLevelTimer, ResetLevelTimer);
            _fullReset = new InputKey(HackHotkeys.FullReset, ResetTimer);
        }

        private void StartDispatcherKeyDown() {
            dispatcherKeyDown = new DispatcherTimer();
            dispatcherKeyDown.Tick += On_KeyDown;
            dispatcherKeyDown.Interval = TimeSpan.FromMilliseconds(1);
            dispatcherKeyDown.Start();
        }

        private void On_KeyDown(object sender, EventArgs e) {
            KeyPressedManager.KeyPressedHandler(StartStop);
            KeyPressedManager.KeyPressedHandler(NewLevel);
            KeyPressedManager.KeyPressedHandler(ResetLevel);
            KeyPressedManager.KeyPressedHandler(FullReset);
        }

        private void StartStopTimer() {
            if (hackTimer.Running == false) {
                hackTimer.StartTimer();
                levelTimer.StartTimer();
                dispatcherAutoSave.Start();
                dispatcherGlow.Stop();
                if (tbHackTime.Foreground.Opacity != 1) {
                    tbHackTime.Foreground.Opacity = 1;
                }
            }
            else {
                hackTimer.StopTimer();
                levelTimer.StopTimer();
                dispatcherAutoSave.Stop();
                dispatcherGlow.Start();
                dispatcherAutoSave.Stop();
                TimerPropertiesManager.CreateConfig();

            }
        }

        private void NewLevelTimer() {
            if (levelTimer != null) {
                levelTimer.StopTimer();
                int current = int.Parse(tbExitCurrent.Text);
                int max = int.Parse(tbExitMax.Text);

                if (current < max) {
                    levelTimer = new HackTimer(tbLevelTime);
                    levelTimer.ResetTimer();

                    if (hackTimer.Running) {
                        levelTimer.StartTimer();
                    }

                    current++;
                    tbExitCurrent.Text = current.ToString();
                }
                else {
                    hackTimer.StopTimer();
                }
            }
        }

        private void ResetLevelTimer() {
            if (levelTimer != null) {
                levelTimer.StopTimer();
                levelTimer = new HackTimer(tbLevelTime);
                levelTimer.ResetTimer();

                if (hackTimer.Running)
                    levelTimer.StartTimer();
            }
        }

        private void ResetTimer() {
            if (hackTimer.Running) {
                StartStopTimer();
            }

            if (MessageBox.Show("Do you really want to set everything to 0?", "Full Reset", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
                levelTimer = new HackTimer(tbLevelTime);
                hackTimer = new HackTimer(tbHackTime);
                levelTimer.ResetTimer();
                hackTimer.ResetTimer();
                tbExitCurrent.Text = 0.ToString();
            }
        }

        private void InitDispatcherGlowOnPause() {
            dispatcherGlow = new DispatcherTimer();
            dispatcherGlow.Tick += On_Pause;
            dispatcherGlow.Interval = TimeSpan.FromMilliseconds(10);
            dispatcherGlow.Start();
        }

        private void On_Pause(object sender, EventArgs e) {
            if (fillingOpacity) {
                tbHackTime.Foreground.Opacity += 0.02;
                if (tbHackTime.Foreground.Opacity >= 1) {
                    fillingOpacity = false;
                }
            }
            else {
                tbHackTime.Foreground.Opacity -= 0.02;
                if (tbHackTime.Foreground.Opacity <= 0) {
                    fillingOpacity = true;
                }
            }
        }

        private void StartDispatcherAutoSave() {
            dispatcherAutoSave = new DispatcherTimer();
            dispatcherAutoSave.Tick += On_AutoSave;
            dispatcherAutoSave.Interval = TimeSpan.FromSeconds(5);
        }

        private void On_AutoSave(object sender, EventArgs e) {
            TimerPropertiesManager.CreateConfig();
        }

        private void Grid_MouseDown_ResetFocus(object sender, MouseButtonEventArgs e) {
            mainGrid.Focus();
        }

        private void Grid_MouseRight_OpenConfig(object sender, MouseButtonEventArgs e) {
            if (configWindow == null) {
                configWindow = new ConfigWindow();
                configWindow.Init(this);
                configWindow.Show();
                dispatcherKeyDown.Stop();

                if (hackTimer.Running) {
                    StartStopTimer();
                }
            }
        }

        private void TextBoxValue_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) {
            TextBox textBox = (TextBox)sender;
            textBox.BorderThickness = new Thickness(1);
            dispatcherKeyDown.Stop();
        }

        private void TextBoxValue_LoseFocus(object sender, RoutedEventArgs e) {
            TextBox textBox = (TextBox)sender;
            textBox.BorderThickness = new Thickness(0);
            dispatcherKeyDown.Start();
        }

        private void InitTextElementList() {
            textElements = new List<FrameworkElement> {
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

        private void Window_Closed(object sender, EventArgs e) {
            Application.Current.Shutdown();
        }
    }
}