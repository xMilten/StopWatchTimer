using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SMWTimer {
    public class HackTimer {

        public bool Running { get => _running; }
        public HackStopWatch stopwatch;

        private bool _running;
        private TextBlock _textBlock;

        private DispatcherTimer dispatcherTimer;
        private TimeSpan timeSpan;

        public HackTimer(TextBlock textBlock) {
            _textBlock = textBlock;

            InitDispatcher();
        }

        private void InitDispatcher() {
            stopwatch = new HackStopWatch(TimeSpan.Parse(_textBlock.Text));
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Timer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(200);
        }

        public void StartTimer() {
            _running = true;
            stopwatch.Start();
            dispatcherTimer.Start();
        }

        public void StopTimer() {
            _running = false;
            stopwatch.Stop();
            dispatcherTimer.Stop();
        }

        public void ResetTimer() {
            stopwatch.Reset();
            stopwatch.StartOffset = TimeSpan.FromMilliseconds(0);
            timeSpan = stopwatch.Elapsed;
            //timeSpan = TimeSpan.FromMilliseconds(0);
            _textBlock.Text = timeSpan.ToString("hh\\:mm\\:ss");
        }

        private void Timer_Tick(object sender, EventArgs e) {
            timeSpan = stopwatch.Elapsed;
            _textBlock.Text = timeSpan.ToString("hh\\:mm\\:ss");
        }
    }
}
