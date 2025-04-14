using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace SMWTimer {
    public class HackHotkeys {
        private static string path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\hotkeys.xml";

        public static Key StartStopTimer { get; set; } = Key.NumPad1;
        public static Key NewLevelTimer { get; set; } = Key.NumPad1;
        public static Key ResetLevelTimer { get; set; } = Key.NumPad7;
        public static Key FullReset { get; set; } = Key.NumPad9;

        public static void InitHotkeys() {
            if (!File.Exists(path)) {
                CreateHotkeys();
            }

            LoadHotkeys();
        }

        private static void LoadHotkeys() {
            KeyConverter keyConverter = new KeyConverter();
            XmlReader xmlReader = XmlReader.Create(path);

            while (xmlReader.Read()) {
                if (xmlReader.NodeType == XmlNodeType.Element) {
                    switch (xmlReader.Name) {
                        case "StartStopTimer":
                            StartStopTimer = (Key)keyConverter.ConvertFromString(xmlReader.ReadElementContentAsString());
                            break;
                        case "NewLevel":
                            NewLevelTimer = (Key)keyConverter.ConvertFromString(xmlReader.ReadElementContentAsString());
                            break;
                        case "ResetLevelTimer":
                            ResetLevelTimer = (Key)keyConverter.ConvertFromString(xmlReader.ReadElementContentAsString());
                            break;
                        case "FullReset":
                            FullReset = (Key)keyConverter.ConvertFromString(xmlReader.ReadElementContentAsString());
                            break;
                    }
                }
            }
        }

        public static void CreateHotkeys() {
            HackHotkeys hackHotkeys = new HackHotkeys();
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, ConformanceLevel = ConformanceLevel.Auto };
            XmlWriter xmlWriter = XmlWriter.Create(path, settings);
            xmlWriter.WriteStartElement("Hotkeys");

            xmlWriter.WriteStartElement("StartStopTimer");
            xmlWriter.WriteString(StartStopTimer.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("NewLevel");
            xmlWriter.WriteString(NewLevelTimer.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("ResetLevelTimer");
            xmlWriter.WriteString(ResetLevelTimer.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("FullReset");
            xmlWriter.WriteString(FullReset.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.Close();
        }
    }
}