using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMWTimer {
    public static class KeyPressedManager {
        public static void KeyPressedHandler(InputKey inputKey) {
            if (Keyboard.IsKeyDown(inputKey.Key) && !inputKey.active) {
                inputKey.active = true;
                inputKey.Action();
            }
            else if (!Keyboard.IsKeyDown(inputKey.Key) && inputKey.active) {
                inputKey.active = false;
            }
        }
    }
}