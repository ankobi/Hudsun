using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Hudsun
{
    public static class ConfigurationManager
    {
        private static Configuration current;

        public static Configuration Current
        {
            get
            {
                if (current == null)
                {
                    current = new Configuration();
                    SetDefaultConfiguration();
                }
                return current;
            }
            set
            {
                current = value;
            }
        }

        public static void Load()
        {
            if (File.Exists("hudsun.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(Current.GetType());
                XmlReader reader = XmlReader.Create("hudsun.xml");
                Current = serializer.Deserialize(reader) as Configuration;
                reader.Close();

                if (Current != null && Current.ClBlue.R == 0 && Current.ClBlue.G == 0 && Current.ClBlue.B == 0)
                {
                    // Most likely blue should have some blue in it
                    SetCheerlightDefaultConfiguration();
                }
            }
            else
            {
                Current = new Configuration();
                SetDefaultConfiguration();

                if (File.Exists("hudsun.cfg"))
                {
                    TextReader reader = new StreamReader("hudsun.cfg");
                    Current.ProjectUrl = reader.ReadLine();
                    reader.Close();
                }
            }
        }

        public static void Save()
        {
            XmlSerializer serializer = new XmlSerializer(Current.GetType());
            
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            XmlWriter writer = XmlWriter.Create("hudsun.xml", settings);

            serializer.Serialize(writer, Current);
            writer.Close();
        }

        private static void SetDefaultConfiguration()
        {

            current.AbortedColor = new RgbValue
                                   {
                                           R = 64,
                                           G = 64,
                                           B = 64
                                   };


            current.FailureColor = new RgbValue
                                   {
                                           R = 64,
                                           G = 0,
                                           B = 0
                                   };

            current.SuccessColor = new RgbValue
                                   {
                                           R = 0,
                                           G = 0,
                                           B = 64
                                   };

            current.UnstableColor = new RgbValue
                                    {
                                            R = 64,
                                            G = 40,
                                            B = 0
                                    };

            SetCheerlightDefaultConfiguration();

            current.ProjectName = "";
            current.ProjectUrl = "";
        }

        private static void SetCheerlightDefaultConfiguration()
        {
            current.ClRed = new RgbValue
            {
                R = 255,
                G = 0,
                B = 0
            };

            current.ClGreen = new RgbValue
            {
                R = 0,
                G = 128,
                B = 0
            };

            current.ClBlue = new RgbValue
            {
                R = 0,
                G = 0,
                B = 255
            };

            current.ClCyan = new RgbValue
            {
                R = 0,
                G = 255,
                B = 255
            };

            current.ClWhite = new RgbValue
            {
                R = 255,
                G = 255,
                B = 255
            };

            current.ClWarmWhite = new RgbValue
            {
                R = 255,
                G = 245,
                B = 230
            };

            current.ClPurple = new RgbValue
            {
                R = 128,
                G = 0,
                B = 128
            };

            current.ClMagenta = new RgbValue
            {
                R = 255,
                G = 0,
                B = 255
            };

            current.ClYellow = new RgbValue
            {
                R = 255,
                G = 255,
                B = 0
            };

            current.ClOrange = new RgbValue
            {
                R = 255,
                G = 20,
                B = 0
            };

            current.ClPink = new RgbValue
            {
                R = 255,
                G = 40,
                B = 255
            };
        }
    }
}
