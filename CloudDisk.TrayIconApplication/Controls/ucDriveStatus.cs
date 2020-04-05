using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudDisk.CoreLibrary.Interfaces;
using CloudDisk.CoreLibrary.utils;

namespace CloudDisk.TrayIconApplication.Controls
{
    public partial class ucDriveStatus : UserControl
    {
        protected ICloudService _Service;
        public ucDriveStatus()
        {
            InitializeComponent();
        }
        
        public ICloudService Service
        {
            get
            {
                return this._Service;
            }
            set
            {
                this._Service = value;
                if (this._Service != null)
                {
                    this.lblName.Text = this._Service.ApplicationName;
                    float inused = (float)this._Service.UsedMemoryInBytes / (float)this._Service.AvaiableMemoryInBytes * 100;
                    this.pbStatus.Maximum = 100;
                    this.pbStatus.Value = (int)inused;
                    this.lnklblOpenFolder.Enabled = this._Service.IsInstalled;
                    this.lblFreeSpace.Text = "Free Space: " + DirectoryUtils.PretifyBytes(this._Service.FreeMemoryInBytes);
                    this.lblUsedSpace.Text = "Used Space: " + DirectoryUtils.PretifyBytes(this._Service.UsedMemoryInBytes);
                    this.lblTotalSpace.Text = "Total Space: " + DirectoryUtils.PretifyBytes(this._Service.AvaiableMemoryInBytes);
                }
            }
        }

        private void lnklblOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this._Service.IsInstalled)
                {
                    System.Diagnostics.Process.Start(this._Service.ClientFolder);
                }
            }
            catch
            {
            }
        }
    }
}
