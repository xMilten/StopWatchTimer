using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SMWTimer {
    public class TextAttributes {
        public string Text { get; set; }
        public FontFamily FontFamily { get; set; }
        public double FontSize { get; set; }
        public FontStyle FontStyle { get; set; }
        public FontWeight FontWeight { get; set; }
        public Brush Background { get; set; }
        public Brush Foreground { get; set; }

        public TextAttributes() {

        }

        public TextAttributes(FrameworkElement fe) {
            if (fe.GetType() == typeof(TextBlock)) {
                TextBlock tb = (TextBlock)fe;

                Text = tb.Text;
                FontFamily = tb.FontFamily;
                FontSize = tb.FontSize;
                FontStyle = tb.FontStyle;
                FontWeight = tb.FontWeight;
                Background = tb.Background;
                Foreground = tb.Foreground;
            }
            else if (fe.GetType() == typeof(TextBox)) {
                TextBox tb = (TextBox)fe;

                Text = tb.Text;
                FontFamily = tb.FontFamily;
                FontSize = tb.FontSize;
                FontStyle = tb.FontStyle;
                FontWeight = tb.FontWeight;
                Background = tb.Background;
                Foreground = tb.Foreground;
            }
        }
    }
}