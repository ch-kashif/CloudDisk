namespace CloudDisk.TrayIconApplication
{
    partial class RootPreferences
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbServices = new System.Windows.Forms.GroupBox();
            this.cbAskForAgain = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(121, 251);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 33);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(217, 251);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 33);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbServices
            // 
            this.gbServices.Location = new System.Drawing.Point(12, 12);
            this.gbServices.Name = "gbServices";
            this.gbServices.Size = new System.Drawing.Size(295, 197);
            this.gbServices.TabIndex = 2;
            this.gbServices.TabStop = false;
            this.gbServices.Text = "Drive Preferences";
            // 
            // cbAskForAgain
            // 
            this.cbAskForAgain.AutoSize = true;
            this.cbAskForAgain.Checked = true;
            this.cbAskForAgain.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAskForAgain.Location = new System.Drawing.Point(13, 216);
            this.cbAskForAgain.Name = "cbAskForAgain";
            this.cbAskForAgain.Size = new System.Drawing.Size(186, 24);
            this.cbAskForAgain.TabIndex = 5;
            this.cbAskForAgain.Text = "Remember my answer";
            this.cbAskForAgain.UseVisualStyleBackColor = true;
            // 
            // RootPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 296);
            this.Controls.Add(this.cbAskForAgain);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbServices);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RootPreferences";
            this.Text = "RootPreferences";
            this.Load += new System.EventHandler(this.RootPreferences_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbServices;
        private System.Windows.Forms.CheckBox cbAskForAgain;
    }
}