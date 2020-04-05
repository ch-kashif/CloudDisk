namespace CloudDisk.TrayIconApplication.Controls
{
    partial class ucDriveStatus
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.lnklblOpenFolder = new System.Windows.Forms.LinkLabel();
            this.lblUsedSpace = new System.Windows.Forms.Label();
            this.lblTotalSpace = new System.Windows.Forms.Label();
            this.lblFreeSpace = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            // 
            // pbStatus
            // 
            this.pbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbStatus.Location = new System.Drawing.Point(3, 25);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(641, 34);
            this.pbStatus.TabIndex = 1;
            // 
            // lnklblOpenFolder
            // 
            this.lnklblOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnklblOpenFolder.AutoSize = true;
            this.lnklblOpenFolder.Location = new System.Drawing.Point(579, 9);
            this.lnklblOpenFolder.Name = "lnklblOpenFolder";
            this.lnklblOpenFolder.Size = new System.Drawing.Size(65, 13);
            this.lnklblOpenFolder.TabIndex = 2;
            this.lnklblOpenFolder.TabStop = true;
            this.lnklblOpenFolder.Text = "Open Folder";
            this.lnklblOpenFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblOpenFolder_LinkClicked);
            // 
            // lblUsedSpace
            // 
            this.lblUsedSpace.AutoSize = true;
            this.lblUsedSpace.ForeColor = System.Drawing.Color.Blue;
            this.lblUsedSpace.Location = new System.Drawing.Point(150, 9);
            this.lblUsedSpace.Name = "lblUsedSpace";
            this.lblUsedSpace.Size = new System.Drawing.Size(69, 13);
            this.lblUsedSpace.TabIndex = 3;
            this.lblUsedSpace.Text = "Used Space:";
            // 
            // lblTotalSpace
            // 
            this.lblTotalSpace.AutoSize = true;
            this.lblTotalSpace.Location = new System.Drawing.Point(390, 9);
            this.lblTotalSpace.Name = "lblTotalSpace";
            this.lblTotalSpace.Size = new System.Drawing.Size(68, 13);
            this.lblTotalSpace.TabIndex = 3;
            this.lblTotalSpace.Text = "Total Space:";
            // 
            // lblFreeSpace
            // 
            this.lblFreeSpace.AutoSize = true;
            this.lblFreeSpace.ForeColor = System.Drawing.Color.Green;
            this.lblFreeSpace.Location = new System.Drawing.Point(277, 9);
            this.lblFreeSpace.Name = "lblFreeSpace";
            this.lblFreeSpace.Size = new System.Drawing.Size(65, 13);
            this.lblFreeSpace.TabIndex = 3;
            this.lblFreeSpace.Text = "Free Space:";
            // 
            // ucDriveStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblFreeSpace);
            this.Controls.Add(this.lblTotalSpace);
            this.Controls.Add(this.lblUsedSpace);
            this.Controls.Add(this.lnklblOpenFolder);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.lblName);
            this.Name = "ucDriveStatus";
            this.Size = new System.Drawing.Size(650, 70);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ProgressBar pbStatus;
        private System.Windows.Forms.LinkLabel lnklblOpenFolder;
        private System.Windows.Forms.Label lblUsedSpace;
        private System.Windows.Forms.Label lblTotalSpace;
        private System.Windows.Forms.Label lblFreeSpace;
    }
}
