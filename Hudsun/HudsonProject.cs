namespace Hudsun
{
    public class HudsonProject
    {
        public string Name
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public string Color
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
