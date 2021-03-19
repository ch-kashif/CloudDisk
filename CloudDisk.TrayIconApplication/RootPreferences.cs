using CloudDisk.CoreLibrary.Interfaces;
using CloudDisk.CoreLibrary.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CloudDisk.TrayIconApplication
{
    public partial class RootPreferences : Form
    {
        public List<ICloudService> Services
        {
            get;
            set;
        }

        public string SelectedService
        {
            get;
            set;
        }

        public RootPreferences()
        {
            InitializeComponent();

            this.Services = new List<ICloudService>();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.SelectedService = "";
            try
            {
                RadioButton rbSelected = (from ctrl in this.gbServices.Controls.OfType<RadioButton>()
                                          where ctrl.Checked
                                          select ctrl).FirstOrDefault();

                if (rbSelected == null)
                {
                    MessageBox.Show("Please select any service to continue");
                    return;
                }

                ApplicationSettings.AskForPreferredService = !this.cbAskForAgain.Checked;
                ApplicationSettings.PreferredService = rbSelected.Text;
                this.Close();
            }
            catch
            {
            }
        }

        private void RootPreferences_Load(object sender, EventArgs e)
        {
            try
            {
                int add_index = 0;
                this.Services = (from srv in this.Services
                                 orderby srv.Sort
                                 select srv).ToList();

                foreach (ICloudService service in this.Services)
                {
                    if (service.IsInstalled)
                    {
                        RadioButton rbServiceNew = new RadioButton();
                        rbServiceNew.Text = service.ApplicationName;
                        rbServiceNew.Location = new Point(20, 34 + 30 * add_index);
                        this.gbServices.Controls.Add(rbServiceNew);
                        add_index++;
                    }

                }
            }
            catch
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
