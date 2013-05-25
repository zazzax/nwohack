namespace Launcher
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lstProcesses = new System.Windows.Forms.ListView();
            this.colProcessId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGameVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPlayerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInjected = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuOptions = new System.Windows.Forms.MenuStrip();
            this.menuOptions_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptions_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptions_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptions_Tools_Inject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptions_Tools_Eject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptions_Tools_Seperator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuOptions_Tools_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptions_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptions_Help_CheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptions_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstProcesses
            // 
            this.lstProcesses.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colProcessId,
            this.colGameVersion,
            this.colPlayerName,
            this.colInjected});
            this.lstProcesses.FullRowSelect = true;
            this.lstProcesses.GridLines = true;
            this.lstProcesses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstProcesses.Location = new System.Drawing.Point(12, 34);
            this.lstProcesses.MultiSelect = false;
            this.lstProcesses.Name = "lstProcesses";
            this.lstProcesses.Size = new System.Drawing.Size(496, 176);
            this.lstProcesses.TabIndex = 0;
            this.lstProcesses.UseCompatibleStateImageBehavior = false;
            this.lstProcesses.View = System.Windows.Forms.View.Details;
            this.lstProcesses.DoubleClick += new System.EventHandler(this.lstProcesses_DoubleClick);
            // 
            // colProcessId
            // 
            this.colProcessId.Text = "Process ID";
            this.colProcessId.Width = 84;
            // 
            // colGameVersion
            // 
            this.colGameVersion.Text = "Game Version";
            this.colGameVersion.Width = 124;
            // 
            // colPlayerName
            // 
            this.colPlayerName.Text = "Player";
            this.colPlayerName.Width = 184;
            // 
            // colInjected
            // 
            this.colInjected.Text = "Injected";
            this.colInjected.Width = 100;
            // 
            // menuOptions
            // 
            this.menuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOptions_File,
            this.menuOptions_Tools,
            this.menuOptions_Help});
            this.menuOptions.Location = new System.Drawing.Point(0, 0);
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size(520, 24);
            this.menuOptions.TabIndex = 1;
            this.menuOptions.Text = "menuStrip1";
            // 
            // menuOptions_File
            // 
            this.menuOptions_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOptions_File_Exit});
            this.menuOptions_File.Name = "menuOptions_File";
            this.menuOptions_File.Size = new System.Drawing.Size(37, 20);
            this.menuOptions_File.Text = "File";
            // 
            // menuOptions_File_Exit
            // 
            this.menuOptions_File_Exit.Name = "menuOptions_File_Exit";
            this.menuOptions_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuOptions_File_Exit.Size = new System.Drawing.Size(134, 22);
            this.menuOptions_File_Exit.Text = "Exit";
            this.menuOptions_File_Exit.Click += new System.EventHandler(this.menuOptions_File_Exit_Click);
            // 
            // menuOptions_Tools
            // 
            this.menuOptions_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOptions_Tools_Inject,
            this.menuOptions_Tools_Eject,
            this.menuOptions_Tools_Seperator1,
            this.menuOptions_Tools_Refresh});
            this.menuOptions_Tools.Name = "menuOptions_Tools";
            this.menuOptions_Tools.Size = new System.Drawing.Size(48, 20);
            this.menuOptions_Tools.Text = "Tools";
            // 
            // menuOptions_Tools_Inject
            // 
            this.menuOptions_Tools_Inject.Name = "menuOptions_Tools_Inject";
            this.menuOptions_Tools_Inject.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.menuOptions_Tools_Inject.Size = new System.Drawing.Size(186, 22);
            this.menuOptions_Tools_Inject.Text = "Inject";
            this.menuOptions_Tools_Inject.Click += new System.EventHandler(this.menuOptions_Tools_Inject_Click);
            // 
            // menuOptions_Tools_Eject
            // 
            this.menuOptions_Tools_Eject.Name = "menuOptions_Tools_Eject";
            this.menuOptions_Tools_Eject.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.menuOptions_Tools_Eject.Size = new System.Drawing.Size(186, 22);
            this.menuOptions_Tools_Eject.Text = "Eject";
            this.menuOptions_Tools_Eject.Click += new System.EventHandler(this.menuOptions_Tools_Eject_Click);
            // 
            // menuOptions_Tools_Seperator1
            // 
            this.menuOptions_Tools_Seperator1.Name = "menuOptions_Tools_Seperator1";
            this.menuOptions_Tools_Seperator1.Size = new System.Drawing.Size(183, 6);
            // 
            // menuOptions_Tools_Refresh
            // 
            this.menuOptions_Tools_Refresh.Name = "menuOptions_Tools_Refresh";
            this.menuOptions_Tools_Refresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuOptions_Tools_Refresh.Size = new System.Drawing.Size(186, 22);
            this.menuOptions_Tools_Refresh.Text = "Refresh processes";
            this.menuOptions_Tools_Refresh.Click += new System.EventHandler(this.menuOptions_Tools_Refresh_Click);
            // 
            // menuOptions_Help
            // 
            this.menuOptions_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOptions_Help_CheckUpdates,
            this.menuOptions_Help_About});
            this.menuOptions_Help.Name = "menuOptions_Help";
            this.menuOptions_Help.Size = new System.Drawing.Size(44, 20);
            this.menuOptions_Help.Text = "Help";
            // 
            // menuOptions_Help_CheckUpdates
            // 
            this.menuOptions_Help_CheckUpdates.Name = "menuOptions_Help_CheckUpdates";
            this.menuOptions_Help_CheckUpdates.Size = new System.Drawing.Size(179, 22);
            this.menuOptions_Help_CheckUpdates.Text = "Check for updates...";
            this.menuOptions_Help_CheckUpdates.Click += new System.EventHandler(this.menuOptions_Help_CheckUpdates_Click);
            // 
            // menuOptions_Help_About
            // 
            this.menuOptions_Help_About.Name = "menuOptions_Help_About";
            this.menuOptions_Help_About.Size = new System.Drawing.Size(179, 22);
            this.menuOptions_Help_About.Text = "About NWOHack";
            this.menuOptions_Help_About.Click += new System.EventHandler(this.menuOptions_Help_About_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 221);
            this.Controls.Add(this.lstProcesses);
            this.Controls.Add(this.menuOptions);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuOptions;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "NWOHack :: Injector";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuOptions.ResumeLayout(false);
            this.menuOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstProcesses;
        private System.Windows.Forms.ColumnHeader colProcessId;
        private System.Windows.Forms.ColumnHeader colGameVersion;
        private System.Windows.Forms.ColumnHeader colPlayerName;
        private System.Windows.Forms.ColumnHeader colInjected;
        private System.Windows.Forms.MenuStrip menuOptions;
        private System.Windows.Forms.ToolStripMenuItem menuOptions_File;
        private System.Windows.Forms.ToolStripMenuItem menuOptions_File_Exit;
        private System.Windows.Forms.ToolStripMenuItem menuOptions_Tools;
        private System.Windows.Forms.ToolStripMenuItem menuOptions_Tools_Inject;
        private System.Windows.Forms.ToolStripMenuItem menuOptions_Tools_Refresh;
        private System.Windows.Forms.ToolStripMenuItem menuOptions_Help;
        private System.Windows.Forms.ToolStripMenuItem menuOptions_Help_CheckUpdates;
        private System.Windows.Forms.ToolStripMenuItem menuOptions_Help_About;
        private System.Windows.Forms.ToolStripMenuItem menuOptions_Tools_Eject;
        private System.Windows.Forms.ToolStripSeparator menuOptions_Tools_Seperator1;

    }
}

