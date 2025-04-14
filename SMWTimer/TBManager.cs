using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMWTimer {
    public class TBManager {
        public static List<TextAttributes> tbModelsDic = new List<TextAttributes>();

        public static void CreateTBModels() {
            foreach (FrameworkElement fe in MainWindow.textElements) {
                TextAttributes textAttribute = new TextAttributes(fe);
                tbModelsDic.Add(textAttribute);
            }
        }
    }
}