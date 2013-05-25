namespace Launcher
{
    partial class frmCheckUpdates
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckUpdates));
            this.barDownloadProgress = new System.Windows.Forms.ProgressBar();
            this.tmrUpdateDownloadQueue = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // barDownloadProgress
            // 
            this.barDownloadProgress.Location = new System.Drawing.Point(4, 4);
            this.barDownloadProgress.Name = "barDownloadProgress";
            this.barDownloadProgress.Size = new System.Drawing.Size(400, 32);
            this.barDownloadProgress.TabIndex = 0;
            // 
            // tmrUpdateDownloadQueue
            // 
            this.tmrUpdateDownloadQueue.Interval = 300;
            this.tmrUpdateDownloadQueue.Tick += new System.EventHandler(this.tmrUpdateDownloadQueue_Tick);
            // 
            // frmCheckUpdates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 40);
            this.Controls.Add(this.barDownloadProgress);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCheckUpdates";
            this.Text = "NWOHack :: Check Updates";
            this.Load += new System.EventHandler(this.frmCheckUpdates_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar barDownloadProgress;
        private System.Windows.Forms.Timer tmrUpdateDownloadQueue;
    }
}