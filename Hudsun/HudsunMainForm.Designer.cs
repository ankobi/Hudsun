namespace Hudsun
{
    partial class HudsunMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HudsunMainForm));
            this.getProjectsButton = new System.Windows.Forms.Button();
            this.projectList = new System.Windows.Forms.ListBox();
            this.hudsonUrl = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.visibilityButton = new System.Windows.Forms.ToolStripMenuItem();
            this.projectButton = new System.Windows.Forms.ToolStripMenuItem();
            this.cheerLightOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // getProjectsButton
            // 
            this.getProjectsButton.Location = new System.Drawing.Point(12, 38);
            this.getProjectsButton.Name = "getProjectsButton";
            this.getProjectsButton.Size = new System.Drawing.Size(75, 23);
            this.getProjectsButton.TabIndex = 0;
            this.getProjectsButton.Text = "Get projects";
            this.getProjectsButton.UseVisualStyleBackColor = true;
            this.getProjectsButton.Click += new System.EventHandler(this.GetProjectsButtonClick);
            // 
            // projectList
            // 
            this.projectList.FormattingEnabled = true;
            this.projectList.Location = new System.Drawing.Point(12, 76);
            this.projectList.Name = "projectList";
            this.projectList.Size = new System.Drawing.Size(180, 134);
            this.projectList.TabIndex = 1;
            this.projectList.SelectedValueChanged += new System.EventHandler(this.ProjectListSelectedValueChanged);
            // 
            // hudsonUrl
            // 
            this.hudsonUrl.Location = new System.Drawing.Point(12, 12);
            this.hudsonUrl.Name = "hudsonUrl";
            this.hudsonUrl.Size = new System.Drawing.Size(180, 20);
            this.hudsonUrl.TabIndex = 2;
            this.hudsonUrl.Text = "http://your-jenkins-server";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.VisibilityButtonClick);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visibilityButton,
            this.projectButton,
            this.cheerLightOnToolStripMenuItem,
            this.quitButton});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(150, 92);
            // 
            // visibilityButton
            // 
            this.visibilityButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.visibilityButton.Name = "visibilityButton";
            this.visibilityButton.Size = new System.Drawing.Size(149, 22);
            this.visibilityButton.Text = "Show/Hide";
            this.visibilityButton.Click += new System.EventHandler(this.VisibilityButtonClick);
            // 
            // projectButton
            // 
            this.projectButton.Name = "projectButton";
            this.projectButton.Size = new System.Drawing.Size(149, 22);
            this.projectButton.Text = "Open project";
            this.projectButton.Click += new System.EventHandler(this.ProjectButtonClick);
            // 
            // cheerLightOnToolStripMenuItem
            // 
            this.cheerLightOnToolStripMenuItem.Name = "cheerLightOnToolStripMenuItem";
            this.cheerLightOnToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.cheerLightOnToolStripMenuItem.Text = "Cheer light on";
            this.cheerLightOnToolStripMenuItem.Click += new System.EventHandler(this.CheerLightOnToolStripMenuItemClick);
            // 
            // quitButton
            // 
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(149, 22);
            this.quitButton.Text = "Quit";
            this.quitButton.Click += new System.EventHandler(this.QuitButtonClick);
            // 
            // HudsunMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 224);
            this.Controls.Add(this.hudsonUrl);
            this.Controls.Add(this.projectList);
            this.Controls.Add(this.getProjectsButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HudsunMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hudsun";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HudsonMainFormFormClosing);
            this.Shown += new System.EventHandler(this.HudsunMainFormShown);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getProjectsButton;
        private System.Windows.Forms.ListBox projectList;
        private System.Windows.Forms.TextBox hudsonUrl;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem visibilityButton;
        private System.Windows.Forms.ToolStripMenuItem quitButton;
        private System.Windows.Forms.ToolStripMenuItem projectButton;
        private System.Windows.Forms.ToolStripMenuItem cheerLightOnToolStripMenuItem;
    }
}

