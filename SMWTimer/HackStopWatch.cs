using System;
using System.Diagnostics;

namespace SMWTimer {
    public class HackStopWatch : Stopwatch {
        public TimeSpan StartOffset { get; set; }

        public HackStopWatch(TimeSpan startOffset) {
            StartOffset = startOffset;
        }

        public new TimeSpan Elapsed {
            get {
                return base.Elapsed + StartOffset;
            }
        }

        public new long ElapsedMilliseconds {
            get {
                return base.ElapsedMilliseconds + (long)StartOffset.TotalMilliseconds;
            }
        }

        public new long ElapsedTicks {
            get {
                return base.ElapsedTicks + StartOffset.Ticks;
            }
        }
    }
}