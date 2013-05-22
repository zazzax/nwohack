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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckUpdates));
            this.barDownloadProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // barDownloadProgress
            // 
            this.barDownloadProgress.Location = new System.Drawing.Point(4, 4);
            this.barDownloadProgress.Name = "barDownloadProgress";
            this.barDownloadProgress.Size = new System.Drawing.Size(300, 32);
            this.barDownloadProgress.TabIndex = 0;
            // 
            // frmCheckUpdates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 40);
            this.Controls.Add(this.barDownloadProgress);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCheckUpdates";
            this.ShowInTaskbar = false;
            this.Text = "NWOHack :: Check Updates";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar barDownloadProgress;
    }
}