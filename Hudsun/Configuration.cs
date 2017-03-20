using System.Xml;
using System.Xml.Serialization;

namespace Hudsun
{
    public struct RgbValue
    {
        public int R
        {
            get;
            set;
        }

        public int G
        {
            get;
            set;
        }

        public int B
        {
            get;
            set;
        }
    }

    [XmlInclude(typeof(RgbValue))]
    public class Configuration
    {
        public RgbValue SuccessColor
        {
            get;
            set;
        }

        public RgbValue UnstableColor
        {
            get;
            set;
        }

        public RgbValue FailureColor
        {
            get;
            set;
        }

        public RgbValue AbortedColor
        {
            get;
            set;
        }

        public RgbValue ClRed
        {
            get;
            set;
        }

        public RgbValue ClGreen
        {
            get;
            set;
        }

        public RgbValue ClBlue
        {
            get;
            set;
        }

        public RgbValue ClCyan
        {
            get;
            set;
        }

        public RgbValue ClWhite
        {
            get;
            set;
        }

        public RgbValue ClWarmWhite
        {
            get;
            set;
        }

        public RgbValue ClPurple
        {
            get;
            set;
        }

        public RgbValue ClMagenta
        {
            get;
            set;
        }

        public RgbValue ClYellow
        {
            get;
            set;
        }

        public RgbValue ClOrange
        {
            get;
            set;
        }

        public RgbValue ClPink
        {
            get;
            set;
        }

        public string ProjectName
        {
            get;
            set;
        }

        public string ProjectUrl
        {
            get;
            set;
        }

        public bool CheerLight
        {
            get;
            set;
        }
    }
}
