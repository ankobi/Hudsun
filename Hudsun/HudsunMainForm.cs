using System;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace Hudsun
{
    public partial class HudsunMainForm : Form
    {
        private bool closeForm = false;
        private readonly Supervisor supervisor;
        private bool shutdown = false;

        public HudsunMainForm()
        {
            InitializeComponent();

            ConfigurationManager.Load();

            supervisor = new Supervisor();

            supervisor.OnLampConnected += SupervisorOnLampConnected;
            supervisor.OnLampDisconnected += SupervisorOnLampDisconnected;
            supervisor.OnProjectAvailable += SupervisorOnProjectAvailable;
            supervisor.OnProjectUnavailable += SupervisorOnProjectUnavailable;

            if (ConfigurationManager.Current.CheerLight)
            {
                SetProjectName("Cheerlight NOw");
                cheerLightOnToolStripMenuItem.Text = "Switch cheer light off";
                closeForm = true;
            }
            else
            {
                cheerLightOnToolStripMenuItem.Text = "Switch cheer light on";

                if (supervisor.ProjectName != null)
                {
                    SetProjectName(supervisor.ProjectName);
                    closeForm = true;
                }
                else
                {
                    notifyIcon.Text = "Hudsun";
                }
            }

            Visible = false;
        }

        void SupervisorOnProjectUnavailable(object sender, EventArgs e)
        {
            notifyIcon.ShowBalloonTip(8000, "Project unavailable", "The project state cannot be retrieved.", ToolTipIcon.Warning);
        }

        void SupervisorOnProjectAvailable(object sender, EventArgs e)
        {
            if (supervisor.ProjectName != null)
            {
                SetProjectName(supervisor.ProjectName);
            }

            notifyIcon.ShowBalloonTip(5000, "Project available again", "The project status was retrieved. Connection re-established.", ToolTipIcon.Info);
        }

        private void SupervisorOnLampDisconnected(object sender, EventArgs e)
        {
            notifyIcon.ShowBalloonTip(8000, "Lamp disconnected", "The USB lamp was diconnected.", ToolTipIcon.Warning);
        }

        private void SupervisorOnLampConnected(object sender, EventArgs e)
        {
            notifyIcon.ShowBalloonTip(5000, "Lamp connected", "The USB lamp was reconnected.", ToolTipIcon.Info);
        }

        private void GetProjectsButtonClick(object sender, EventArgs e)
        {
            projectList.Items.Clear();
            var document = new XmlDocument();
            document.Load(hudsonUrl.Text + "/api/xml");

            foreach (XmlNode a in document.FirstChild.ChildNodes)
            {
                if (a.Name.ToUpper() == "JOB")
                {
                    var project = new HudsonProject();

                    foreach (XmlNode b in a.ChildNodes)
                    {
                        switch (b.Name.ToUpper())
                        {
                            case "NAME":
                                project.Name = b.InnerText;
                                break;
                            case "URL":
                                project.Url = b.InnerText;
                                break;
                            case "COLOR":
                                project.Color = b.InnerText;
                                break;
                        }
                    }

                    if (project.Name != null && project.Url != null && project.Color != null)
                    {
                        projectList.Items.Add(project);
                    }
                }
            }
        }

        private void ProjectListSelectedValueChanged(object sender, EventArgs e)
        {
            var project = projectList.SelectedItem as HudsonProject;

            if (project != null)
            {
                supervisor.ProjectUrl = project.Url;
                SetProjectName(project.Name);
            }
        }

        private void HudsonMainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!shutdown && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Visible = false;
            }
            else
            {
                if (e.CloseReason == CloseReason.WindowsShutDown)
                {
                    supervisor.Shutdown();
                    ConfigurationManager.Save();
                    Application.Exit();
                }
            }
        }

        private void VisibilityButtonClick(object sender, EventArgs e)
        {
            Visible = !Visible;
        }

        private void QuitButtonClick(object sender, EventArgs e)
        {
            shutdown = true;
            supervisor.Shutdown();
            ConfigurationManager.Save();
            Application.Exit();
        }

        private void SetProjectName(string name)
        {
            this.Text = "Hudsun - " + name;
            if (Text.Length < 64)
            {
                notifyIcon.Text = Text;
            }
            else
            {
                notifyIcon.Text = Text.Substring(0, 59) + " ...";
            }
        }

        private void HudsunMainFormShown(object sender, EventArgs e)
        {
            if (closeForm)
            {
                closeForm = false;
                Visible = false;
            }
        }

        private void ProjectButtonClick(object sender, EventArgs e)
        {
            if (supervisor.ProjectUrl != null)
            {
                Process.Start(supervisor.ProjectUrl);
            }
        }

        private void CheerLightOnToolStripMenuItemClick(object sender, EventArgs e)
        {
            ConfigurationManager.Current.CheerLight = !ConfigurationManager.Current.CheerLight;
            ConfigurationManager.Save();

            if (ConfigurationManager.Current.CheerLight)
            {
                SetProjectName("Cheerlight NOw");
                cheerLightOnToolStripMenuItem.Text = "Switch cheer light off";
            }
            else
            {
                SetProjectName(supervisor.ProjectName);
                cheerLightOnToolStripMenuItem.Text = "Switch cheer light on";
            }
            supervisor.Refresh();
        }
    }
}
