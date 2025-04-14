using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace SMWTimer {
    public static class TimerPropertiesManager {
        private static string path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\config.xml";
        private static XmlWriter xmlWriter;

        public static void InitConfig() {
            if (!File.Exists(path)) {
                CreateConfig();
            }

            LoadConfig();
        }

        private static void LoadConfig() {
            XmlReader xmlReader = XmlReader.Create(path);
            foreach (FrameworkElement fe in MainWindow.textElements) {
                GetProperties(fe, xmlReader);
            }
        }

        private static void GetProperties(FrameworkElement fe, XmlReader xmlReader) {
            xmlReader.ReadToFollowing(fe.Name.Substring(2));
            int depth = xmlReader.Depth;
            while (xmlReader.Read()) {
                if (xmlReader.Depth == depth + 1 && xmlReader.NodeType == XmlNodeType.Element) {
                    if (fe.GetType() == typeof(TextBlock)) {
                        TextBlock tb = (TextBlock)fe;
                        ConvertXmlValues(xmlReader, tb);
                    }
                    else if (fe.GetType() == typeof(TextBox)) {
                        TextBox tb = (TextBox)fe;
                        ConvertXmlValues(xmlReader, tb);
                    }
                }
                else if (xmlReader.Depth == depth && xmlReader.NodeType == XmlNodeType.EndElement)
                    break;
            }
        }

        private static void ConvertXmlValues(XmlReader xmlReader, TextBlock tb) {
            switch (xmlReader.Name) {
                case "Text":
                    tb.Text = xmlReader.ReadElementContentAsString();
                    break;
                case "FontFamily":
                    FontFamilyConverter ffc = new FontFamilyConverter();
                    tb.FontFamily = (FontFamily)ffc.ConvertFromString(xmlReader.ReadElementContentAsString());
                    break;
                case "FontSize":
                    tb.FontSize = double.Parse(xmlReader.ReadElementContentAsString());
                    break;
                case "FontStyle":
                    FontStyleConverter fsc = new FontStyleConverter();
                    tb.FontStyle = (FontStyle)fsc.ConvertFromString(xmlReader.ReadElementContentAsString());
                    break;
                case "FontWeight":
                    FontWeightConverter fwc = new FontWeightConverter();
                    tb.FontWeight = (FontWeight)fwc.ConvertFromString(xmlReader.ReadElementContentAsString());
                    break;
                case "Background":
                    BrushConverter bbc = new BrushConverter();
                    tb.Background = (Brush)bbc.ConvertFromString(xmlReader.ReadElementContentAsString());
                    break;
                case "Foreground":
                    BrushConverter bfc = new BrushConverter();
                    tb.Foreground = (Brush)bfc.ConvertFromString(xmlReader.ReadElementContentAsString());
                    break;
            }
        }

        private static void ConvertXmlValues(XmlReader xmlReader, TextBox tb) {
            switch (xmlReader.Name) {
                case "Text":
                    tb.Text = xmlReader.ReadElementContentAsString();
                    break;
                case "FontFamily":
                    FontFamilyConverter ffc = new FontFamilyConverter();
                    tb.FontFamily = (FontFamily)ffc.ConvertFromString(xmlReader.ReadElementContentAsString());
                    break;
                case "FontSize":
                    tb.FontSize = double.Parse(xmlReader.ReadElementContentAsString());
                    break;
                case "FontStyle":
                    FontStyleConverter fsc = new FontStyleConverter();
                    tb.FontStyle = (FontStyle)fsc.ConvertFromString(xmlReader.ReadElementContentAsString());
                    break;
                case "FontWeight":
                    FontWeightConverter fwc = new FontWeightConverter();
                    tb.FontWeight = (FontWeight)fwc.ConvertFromString(xmlReader.ReadElementContentAsString());
                    break;
                case "Background":
                    BrushConverter bbc = new BrushConverter();
                    tb.Background = (Brush)bbc.ConvertFromString(xmlReader.ReadElementContentAsString());
                    break;
                case "Foreground":
                    BrushConverter bfc = new BrushConverter();
                    tb.Foreground = (Brush)bfc.ConvertFromString(xmlReader.ReadElementContentAsString());
                    break;
            }
        }

        public static void CreateConfig() {
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, ConformanceLevel = ConformanceLevel.Auto };
            xmlWriter = XmlWriter.Create(path, settings);
            xmlWriter.WriteStartElement("TimerProperties");

            foreach (FrameworkElement fe in MainWindow.textElements) {
                SetProperties(fe);
            }
            xmlWriter.Close();
        }

        private static void SetProperties(FrameworkElement fe) {
            TextAttributes ta = new TextAttributes();

            if (fe.GetType() == typeof(TextBlock)) {
                TextBlock tb = (TextBlock)fe;
                xmlWriter.WriteStartElement(tb.Name.Substring(2));

                foreach (PropertyInfo prop in ta.GetType().GetProperties()) {
                    xmlWriter.WriteStartElement(prop.Name);
                    xmlWriter.WriteString(tb.GetType().GetProperty(prop.Name).GetValue(tb).ToString());
                    xmlWriter.WriteEndElement();
                }
            }
            else if (fe.GetType() == typeof(TextBox)) {
                TextBox tb = (TextBox)fe;
                xmlWriter.WriteStartElement(tb.Name.Substring(2));

                foreach (PropertyInfo prop in ta.GetType().GetProperties()) {
                    xmlWriter.WriteStartElement(prop.Name);
                    xmlWriter.WriteString(tb.GetType().GetProperty(prop.Name).GetValue(tb).ToString());
                    xmlWriter.WriteEndElement();
                }
            }
            xmlWriter.WriteEndElement();
        }
    }
}