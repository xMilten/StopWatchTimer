using System;
using System.Windows.Input;

namespace SMWTimer {
    public class InputKey {
        public bool active;
        public Key Key { get; set; }
        public Action Action { get; }

        public InputKey(Key key, Action action) {
            Key = key;
            Action = action;
        }
    }
}