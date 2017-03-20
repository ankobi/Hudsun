using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.IO;

namespace Hudsun
{
    public class Supervisor
    {
        public event EventHandler OnLampDisconnected;
        public event EventHandler OnLampConnected;
        public event EventHandler OnProjectUnavailable;
        public event EventHandler OnProjectAvailable;

        private readonly UsbConnector connector;
        private readonly Thread updaterThread;
        private readonly Thread lightThread;
        private bool started;
        private string projectUrl;
        private string projectName;
        private string projectState;
        private readonly Semaphore refreshSemaphore;
        private readonly Semaphore updaterSemaphore;
        private bool connectionLost = false;
        private string oldState = "";

        public string ProjectUrl
        {
            get
            {
                return projectUrl;
            }
            set
            {
                ConfigurationManager.Current.ProjectUrl = value;
                projectUrl = value;
                projectState = GetProjectState();
                try
                {
                    connector.Abort();
                    refreshSemaphore.Release();
                }
                catch
                {
                }
            }
        }

        public string ProjectName
        {
            get
            {
                return projectName;
            }
            set
            {
                ConfigurationManager.Current.ProjectName = value;
                projectName = value;
            }
        }

        public Supervisor()
        {
            connector = new UsbConnector();

            connector.OnConnected += ConnectorOnConnected;
            connector.OnDisconnected += ConnectorOnDisconnected;

            refreshSemaphore = new Semaphore(0, 1);
            updaterSemaphore = new Semaphore(0, 1);

            if (!string.IsNullOrEmpty(ConfigurationManager.Current.ProjectUrl))
            {
                projectUrl = ConfigurationManager.Current.ProjectUrl;
                projectName = ConfigurationManager.Current.ProjectName;

                if (ConfigurationManager.Current.CheerLight)
                {
                    projectState = GetCheerlightState();
                }
                else
                {
                    projectState = GetProjectState();
                }

                if (projectState == null)
                {
                    connectionLost = true;

                    if (OnProjectUnavailable != null)
                    {
                        OnProjectUnavailable.Invoke(this, null);
                    }
                }
            }

            updaterThread = new Thread(UpdaterThreadExecute);
            updaterThread.Start();
            lightThread = new Thread(LightThreadExecute);
            lightThread.Start();
        }

        public void Refresh()
        {
            try
            {
                if (ConfigurationManager.Current.CheerLight)
                {
                    projectState = GetCheerlightState();
                }
                else
                {
                    projectState = GetProjectState();
                }
                connector.Abort();
                refreshSemaphore.Release();
            }
            catch
            {
            }
        }

        private void ConnectorOnDisconnected(object sender, EventArgs e)
        {
            oldState = "";

            if (OnLampDisconnected != null)
            {
                OnLampDisconnected(sender, e);
            }
        }

        private void ConnectorOnConnected(object sender, EventArgs e)
        {
            oldState = "";

            if (OnLampConnected != null)
            {
                OnLampConnected(sender, e);
            }
        }

        public void Shutdown()
        {
            started = false;
            projectState += projectState;
            try
            {
                refreshSemaphore.Release();
            }
            catch
            {
            }

            try
            {
                updaterSemaphore.Release();
            }
            catch
            {
            }

            connector.Abort();
            connector.SetRGB(0, 0, 0);
        }

        private void LightThreadExecute()
        {
            while (started)
            {
                if (connector.CheckConnectionStatus())
                {
                    if (projectState != null)
                    {
                        if (oldState != projectState)
                        {
                            oldState = projectState;
                            connector.Abort();

                            switch (projectState)
                            {
                                case "blue":
                                    connector.SetRGB(ConfigurationManager.Current.SuccessColor);
                                    break;
                                case "yellow":
                                    connector.SetRGB(ConfigurationManager.Current.UnstableColor);
                                    break;
                                case "red":
                                    connector.SetRGB(ConfigurationManager.Current.FailureColor);
                                    break;
                                case "grey":
                                    connector.SetRGB(ConfigurationManager.Current.AbortedColor);
                                    break;
                                case "aborted":
                                    connector.SetRGB(ConfigurationManager.Current.AbortedColor);
                                    break;
                                case "blue_anime":
                                    while (projectState == "blue_anime")
                                    {
                                        connector.PulseRgb(ConfigurationManager.Current.SuccessColor);
                                    }
                                    break;
                                case "yellow_anime":
                                    while (projectState == "yellow_anime")
                                    {
                                        connector.PulseRgb(ConfigurationManager.Current.UnstableColor);
                                    }
                                    break;
                                case "red_anime":
                                    while (projectState == "red_anime")
                                    {
                                        connector.PulseRgb(ConfigurationManager.Current.FailureColor);
                                    }
                                    break;
                                case "grey_anime":
                                    while (projectState == "grey_anime")
                                    {
                                        connector.PulseRgb(ConfigurationManager.Current.AbortedColor);
                                    }
                                    break;
                                case "aborted_anime":
                                    while (projectState == "aborted_anime")
                                    {
                                        connector.PulseRgb(ConfigurationManager.Current.AbortedColor);
                                    }
                                    break;
                                case "CL_red":
                                    connector.SetRGB(ConfigurationManager.Current.ClRed);
                                    break;
                                case "CL_green":
                                    connector.SetRGB(ConfigurationManager.Current.ClGreen);
                                    break;
                                case "CL_blue":
                                    connector.SetRGB(ConfigurationManager.Current.ClBlue);
                                    break;
                                case "CL_cyan":
                                    connector.SetRGB(ConfigurationManager.Current.ClCyan);
                                    break;
                                case "CL_white":
                                    connector.SetRGB(ConfigurationManager.Current.ClWhite);
                                    break;
                                case "CL_oldlace":
                                    connector.SetRGB(ConfigurationManager.Current.ClWarmWhite);
                                    break;
                                case "CL_warmwhite":
                                    connector.SetRGB(ConfigurationManager.Current.ClWarmWhite);
                                    break;
                                case "CL_purple":
                                    connector.SetRGB(ConfigurationManager.Current.ClPurple);
                                    break;
                                case "CL_magenta":
                                    connector.SetRGB(ConfigurationManager.Current.ClMagenta);
                                    break;
                                case "CL_yellow":
                                    connector.SetRGB(ConfigurationManager.Current.ClYellow);
                                    break;
                                case "CL_orange":
                                    connector.SetRGB(ConfigurationManager.Current.ClOrange);
                                    break;
                                case "CL_pink":
                                    connector.SetRGB(ConfigurationManager.Current.ClPink);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        oldState = "";
                    }
                }

                refreshSemaphore.WaitOne(2000);
            }
        }

        private void UpdaterThreadExecute()
        {
            started = true;

            while (started)
            {
                if (ConfigurationManager.Current.CheerLight)
                {
                    projectState = GetCheerlightState();
                }
                else
                {
                    if (ProjectUrl != null)
                    {
                        projectState = GetProjectState();
                    }
                }

                updaterSemaphore.WaitOne(5000);
            }
        }

        private string GetProjectState()
        {
            string state = "";
            var document = new XmlDocument();

            try
            {
                document.Load(ProjectUrl + "/api/xml");

                foreach (XmlNode a in document.FirstChild.ChildNodes)
                {
                    if (a.Name.ToUpper() == "COLOR")
                    {
                        state = a.InnerText;
                    }
                    if (a.Name.ToUpper() == "DISPLAYNAME")
                    {
                        ProjectName = a.InnerText;
                    }
                }

                if (connectionLost)
                {
                    if (OnProjectAvailable != null)
                    {
                        OnProjectAvailable(this, null);
                    }
                    connectionLost = false;
                }
            }
            catch
            {
                if (projectState != null)
                {
                    connector.SetRGB(0, 0, 0);
                    if (OnProjectUnavailable != null)
                    {
                        OnProjectUnavailable.Invoke(this, null);
                    }
                    connectionLost = true;
                }
                projectState = null;
                state = null;
            }

            return state;
        }

        private string GetCheerlightState()
        {
            string state = "";
            var document = new XmlDocument();

            try
            {
                document.Load(@"http://api.thingspeak.com/channels/1417/field/1/last.xml");

                XmlNodeList list = document.GetElementsByTagName("field1");

                foreach (XmlNode node in list)
                {
                    state = "CL_" + node.InnerText;
                }
               
                if (connectionLost)
                {
                    if (OnProjectAvailable != null)
                    {
                        OnProjectAvailable(this, null);
                    }
                    connectionLost = false;
                }
            }
            catch
            {
                if (projectState != null)
                {
                    connector.SetRGB(0, 0, 0);
                    if (OnProjectUnavailable != null)
                    {
                        OnProjectUnavailable.Invoke(this, null);
                    }
                    connectionLost = true;
                }
                projectState = null;
                state = null;
            }

            return state;
        }

    }
}
