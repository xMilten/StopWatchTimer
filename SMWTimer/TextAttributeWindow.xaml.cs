using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SMWTimer {
    /// <summary>
    /// Interaktionslogik für TextAttributeWindow.xaml
    /// </summary>
    public partial class TextAttributeWindow : Window {
        public TextAttributeWindow() {
            InitializeComponent();
            cmbColors.ItemsSource = typeof(Colors).GetProperties();
        }

        private void btnColor_Click(object sender, RoutedEventArgs e) {

        }
    }
}