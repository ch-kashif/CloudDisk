namespace CloudDisk.TrayIconApplication
{
    partial class TrayForm
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

            if (disposing)
            {
                this.trayIcon.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrayForm));
            this.lblSize = new System.Windows.Forms.Label();
            this.gbDrivesStatus = new System.Windows.Forms.GroupBox();
            this.lnklblRootPreferences = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblSize
            // 
            this.lblSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(420, 9);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(209, 76);
            this.lblSize.TabIndex = 0;
            this.lblSize.Text = "label1";
            // 
            // gbDrivesStatus
            // 
            this.gbDrivesStatus.Location = new System.Drawing.Point(12, 97);
            this.gbDrivesStatus.Name = "gbDrivesStatus";
            this.gbDrivesStatus.Size = new System.Drawing.Size(747, 415);
            this.gbDrivesStatus.TabIndex = 1;
            this.gbDrivesStatus.TabStop = false;
            this.gbDrivesStatus.Text = "Drives Status";
            // 
            // lnklblRootPreferences
            // 
            this.lnklblRootPreferences.AutoSize = true;
            this.lnklblRootPreferences.Location = new System.Drawing.Point(13, 78);
            this.lnklblRootPreferences.Name = "lnklblRootPreferences";
            this.lnklblRootPreferences.Size = new System.Drawing.Size(120, 13);
            this.lnklblRootPreferences.TabIndex = 2;
            this.lnklblRootPreferences.TabStop = true;
            this.lnklblRootPreferences.Text = "Cloud Root Preferences";
            this.lnklblRootPreferences.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblRootPreferences_LinkClicked);
            // 
            // TrayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 524);
            this.Controls.Add(this.lnklblRootPreferences);
            this.Controls.Add(this.gbDrivesStatus);
            this.Controls.Add(this.lblSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrayForm";
            this.Text = "Cloud Disk Control Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrayForm_FormClosing);
            this.Load += new System.EventHandler(this.TrayForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.GroupBox gbDrivesStatus;
        private System.Windows.Forms.LinkLabel lnklblRootPreferences;
    }
}

