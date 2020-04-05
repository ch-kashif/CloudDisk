using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CloudDisk.CoreLibrary.utils;
using CloudDisk.CoreLibrary.Interfaces;
using CloudDisk.CoreLibrary.StorageProviders;
using System.Threading;
using CloudDisk.TrayIconApplication.Controls;

namespace CloudDisk.TrayIconApplication
{
    public partial class TrayForm : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        private ICloudService dropbox = new DropBox();
        private ICloudService googledrive = new GoogleDrive();
        private ICloudService copydrive = new CopyDrive();
        private ICloudService onedrive = new OneDrive();
        private ICloudService amazon = new AmazonDrive();

        private System.Windows.Forms.Timer tmrRootWatcher;
        private System.Windows.Forms.Timer tmrUpdateInfo;

        public TrayForm()
        {
            InitializeComponent();
            try
            {
                string trayIconLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\icons\40x40.ico");
                trayMenu = new ContextMenu();
                MenuItem defaultItem = new MenuItem("Control Panel", OnPreferences);
                defaultItem.DefaultItem = true;
                trayMenu.MenuItems.Add(defaultItem);
                trayMenu.MenuItems.Add("Exit", OnExit);

                trayIcon = new NotifyIcon();
                trayIcon.Text = "Cloud Disk";
                trayIcon.Icon = new Icon(trayIconLocation, 40, 40);

                trayIcon.ContextMenu = trayMenu;
                trayIcon.Visible = true;
                trayIcon.DoubleClick += trayIcon_DoubleClick;

                // -- Initialize Services

                dropbox.AvaiableMemoryInBytes = DirectoryUtils.SizeInBytes(0, 2, 512, 0, 0);
                dropbox.Sort = 1;

                googledrive.AvaiableMemoryInBytes = DirectoryUtils.SizeInBytes(0, 15, 0, 0, 0);
                googledrive.Sort = 2;

                copydrive.AvaiableMemoryInBytes = DirectoryUtils.SizeInBytes(0, 15, 0, 0, 0);
                copydrive.Sort = 3;

                onedrive.AvaiableMemoryInBytes = DirectoryUtils.SizeInBytes(0, 7, 0, 0, 0);
                onedrive.Sort = 4;

                amazon.AvaiableMemoryInBytes = DirectoryUtils.SizeInBytes(0, 5, 0, 0, 0);
                amazon.Sort = 5;

                // -- Root timer
                this.tmrRootWatcher = new System.Windows.Forms.Timer();
                this.tmrRootWatcher.Interval = 5000;
                this.tmrRootWatcher.Tick += tmrRootWatcher_Tick;

                // -- UpdateInfo Timer
                this.tmrUpdateInfo = new System.Windows.Forms.Timer();
                this.tmrUpdateInfo.Interval = 1000;
                this.tmrUpdateInfo.Tick += tmrUpdateInfo_Tick;
                this.tmrUpdateInfo.Start();
            }
            catch (Exception ex)
            {
                LogUtils.LogE(ex);
            }
        }

        void tmrUpdateInfo_Tick(object sender, EventArgs e)
        {
            this.DisplayInfo();
            Application.DoEvents();
        }
        private ICloudService PreferredRootService
        {
            get
            {
                if (string.Compare(ApplicationSettings.PreferredService, dropbox.ApplicationName, true) == 0)
                {
                    return dropbox;
                }
                else if (string.Compare(ApplicationSettings.PreferredService, copydrive.ApplicationName, true) == 0)
                {
                    return copydrive;
                }
                else if (string.Compare(ApplicationSettings.PreferredService, onedrive.ApplicationName, true) == 0)
                {
                    return onedrive;
                }
                else if (string.Compare(ApplicationSettings.PreferredService, amazon.ApplicationName, true) == 0)
                {
                    return amazon;
                }
                else if (string.Compare(ApplicationSettings.PreferredService, googledrive.ApplicationName, true) == 0)
                {
                    return googledrive;
                }
                else
                {
                    return null;
                }
            }
        }

        private void tmrRootWatcher_Tick(object sender, EventArgs e)
        {
            try
            {
                this.tmrRootWatcher.Stop();
                bool askforService = ApplicationSettings.AskForPreferredService;
                string defaultService = ApplicationSettings.PreferredService;
                DirectoryInfo diMappedDirInfo = new DirectoryInfo(ApplicationSettings.MappedFolder);
                List<FileInfo> TopLevelFiles = new List<FileInfo>(diMappedDirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly));

                TopLevelFiles = (from f in TopLevelFiles
                                 where string.Compare(f.Name, "desktop.ini", true) != 0
                                 select f).ToList();

                if (TopLevelFiles.Count > 0)
                {
                    if (askforService||string.IsNullOrEmpty(defaultService))
                    {
                        RootPreferences getPreferences = new RootPreferences();
                        getPreferences.Services.Add(amazon);
                        getPreferences.Services.Add(dropbox);
                        getPreferences.Services.Add(googledrive);
                        getPreferences.Services.Add(onedrive);
                        getPreferences.Services.Add(copydrive);

                        if (getPreferences.ShowDialog() == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    
                    foreach(FileInfo fsingleFile in TopLevelFiles)
                    {
                        try
                        {
                            string destName = Path.Combine(this.PreferredRootService.ClientFolder, Path.GetFileName(fsingleFile.FullName));
                            if (this.PreferredRootService.ClientFolderExists)
                            {
                                if (File.Exists(destName))
                                {
                                    File.Delete(destName);
                                }
                                File.Move(fsingleFile.FullName, destName);
                            }
                        }
                        catch
                        {
                        }
                        Thread.Sleep(10);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                LogUtils.LogE(ex);
            }
            finally
            {
                this.tmrRootWatcher.Start();
            }
        }

        void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.OpenPreferences();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Visible = false;
            
            ShowInTaskbar = false;
            //this.trayIcon.ShowBalloonTip(1000, "This is my application", "Hello baloon app", ToolTipIcon.Info);
            base.OnLoad(e);
            this.Startup();
        }
        private void TrayForm_Load(object sender, EventArgs e)
        {
            
        }
        
        private void DisplayInfo()
        {
            try
            {
                List<ICloudService> services = new List<ICloudService>();
                services.Add(googledrive);
                services.Add(amazon);
                services.Add(onedrive);
                services.Add(dropbox);
                services.Add(copydrive);

                services = (from s in services
                            orderby s.Sort
                            select s).ToList();

                long total_free_space = 0;
                int add_index = 0;
                

                foreach (ICloudService srv in services)
                {
                    ucDriveStatus dsAlready = (from ctrl in this.gbDrivesStatus.Controls.OfType<ucDriveStatus>()
                                               where string.Compare(ctrl.Service.ApplicationName, srv.ApplicationName, true) == 0
                                               select ctrl).FirstOrDefault();
                    if (srv.IsInstalled)
                    {
                        total_free_space += srv.FreeMemoryInBytes;

                        if (dsAlready == null)
                        {
                            ucDriveStatus ds = new ucDriveStatus();
                            ds.Service = srv;
                            ds.Location = new Point(20, 20 + add_index * (ds.Height + 10));
                            this.gbDrivesStatus.Controls.Add(ds);
                        }
                        else
                        {
                            dsAlready.Service = srv;
                        }

                        add_index++;
                    }
                    else
                    {
                        if (dsAlready != null)
                        {
                            this.gbDrivesStatus.Controls.Remove(dsAlready);
                        }
                    }
                }
                this.lblSize.Text = DirectoryUtils.PretifyBytes(total_free_space);
            }
            catch (Exception ex)
            {
                LogUtils.LogE(ex);
            }
        }


        private void OpenPreferences()
        {
            this.Visible = true;
            this.Activate();
        }
        private void OnPreferences(object sender, EventArgs e)
        {
            this.OpenPreferences();
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TrayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }

        private void Startup()
        {
            try
            {
                LogUtils.Log("*********** APP STARTED **********");
                LogUtils.Log("Step # 1 : Unmapping previous drive.");

                string UserLinks = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Links\\Cloud Plus Drive.lnk");

                MirrorUtils.UnmapDrive(ApplicationSettings.MappedDriveLetter);

                if (!Directory.Exists(ApplicationSettings.MappedFolder))
                    Directory.CreateDirectory(ApplicationSettings.MappedFolder);

                ResourceUtils.SetFolderIcon(ApplicationSettings.MappedFolder, "Cloude Plus Drive");

                LogUtils.Log("Removing favoriates....");
                if (File.Exists(UserLinks))
                    File.Delete(UserLinks);

                LogUtils.Log("Adding folder into favoriates");

                try
                {
                    MirrorUtils.CreateShortcut(ApplicationSettings.MappedFolder, UserLinks, "", "", "", Path.GetDirectoryName(UserLinks));
                }
                catch
                {

                }


                LogUtils.Log(string.Format("Mapping drive {0} at {1}", ApplicationSettings.MappedDriveLetter, ApplicationSettings.MappedFolder));
                MirrorUtils.MapDrive(ApplicationSettings.MappedDriveLetter, ApplicationSettings.MappedFolder);
                MirrorUtils.SetDriveIcon(ApplicationSettings.MappedDriveLetter);

                LogUtils.Log("drive mapped");
                MirrorUtils.DisplayDriveInfo();
                ResourceUtils.RecreateAllExecutableResources();

                LogUtils.Log("Unmapping drives...");
                LogUtils.Log(string.Format("Unmapping Dropbox: {0}", MirrorUtils.UnmapFolder(dropbox.ApplicationName)));
                LogUtils.Log(string.Format("Unmapping Google Drive: {0}", MirrorUtils.UnmapFolder(googledrive.ApplicationName)));
                LogUtils.Log(string.Format("Unmapping Copy Drive: {0}", MirrorUtils.UnmapFolder(copydrive.ApplicationName)));
                LogUtils.Log(string.Format("Unmapping One Drive: {0}", MirrorUtils.UnmapFolder(onedrive.ApplicationName)));
                LogUtils.Log(string.Format("Unmapping Amazon: {0}", MirrorUtils.UnmapFolder(amazon.ApplicationName)));

                LogUtils.Log(string.Format("Dropbox installed status: {0}", dropbox.IsInstalled));
                LogUtils.Log(string.Format("Dropbox client folder: {0}", dropbox.ClientFolder));
                LogUtils.Log(string.Format("Dropbox folder status: {0}", dropbox.ClientFolderExists ? "Exists" : "Not Exists"));
                LogUtils.Log(string.Format("Dropbox Drive Available Space: {0}", DirectoryUtils.PretifyBytes(dropbox.AvaiableMemoryInBytes)));
                LogUtils.Log(string.Format("Dropbox Drive Used Space: {0}", DirectoryUtils.PretifyBytes(dropbox.UsedMemoryInBytes)));
                LogUtils.Log(string.Format("Dropbox Drive mapping : {0}", MirrorUtils.MapFolder(dropbox)));
                LogUtils.Log("---------------------------------------------------------------");

                LogUtils.Log(string.Format("Amazon Drive installed status: {0}", amazon.IsInstalled));
                LogUtils.Log(string.Format("Amazon client folder: {0}", amazon.ClientFolder));
                LogUtils.Log(string.Format("Amazon folder status: {0}", amazon.ClientFolderExists ? "Exists" : "Not Exists"));
                LogUtils.Log(string.Format("Amazon Drive Available Space: {0}", DirectoryUtils.PretifyBytes(amazon.AvaiableMemoryInBytes)));
                LogUtils.Log(string.Format("Amazon Drive Used Space: {0}", DirectoryUtils.PretifyBytes(amazon.UsedMemoryInBytes)));
                LogUtils.Log(string.Format("Amazon Drive mapping : {0}", MirrorUtils.MapFolder(amazon)));
                LogUtils.Log("---------------------------------------------------------------");

                LogUtils.Log(string.Format("One Drive installed status: {0}", onedrive.IsInstalled));
                LogUtils.Log(string.Format("One Drive Client Folder: {0}", onedrive.ClientFolder));
                LogUtils.Log(string.Format("One Drive folder status: {0}", onedrive.ClientFolderExists ? "Exists" : "Not Exists"));
                LogUtils.Log(string.Format("One Drive Available Size: {0}", DirectoryUtils.PretifyBytes(onedrive.AvaiableMemoryInBytes)));
                LogUtils.Log(string.Format("One Drive Used Size: {0}", DirectoryUtils.PretifyBytes(onedrive.UsedMemoryInBytes)));
                LogUtils.Log(string.Format("One Drive mapping : {0}", MirrorUtils.MapFolder(onedrive)));
                LogUtils.Log("---------------------------------------------------------------");

                LogUtils.Log(string.Format("Copy Drive installed status: {0}", copydrive.IsInstalled));
                LogUtils.Log(string.Format("Copy Drive Client Folder: {0}", copydrive.ClientFolder));
                LogUtils.Log(string.Format("Copy Drive folder status: {0}", copydrive.ClientFolderExists ? "Exists" : "Not Exists"));
                LogUtils.Log(string.Format("Copy Drive Available Size: {0}", DirectoryUtils.PretifyBytes(copydrive.AvaiableMemoryInBytes)));
                LogUtils.Log(string.Format("Copy Drive Used Size: {0}", DirectoryUtils.PretifyBytes(copydrive.UsedMemoryInBytes)));
                LogUtils.Log(string.Format("Copy Drive mapping : {0}", MirrorUtils.MapFolder(copydrive)));
                LogUtils.Log("---------------------------------------------------------------");

                LogUtils.Log(string.Format("Google Drive installed status: {0}", googledrive.IsInstalled));
                LogUtils.Log(string.Format("Google Drive Client Folder: {0}", googledrive.ClientFolder));
                LogUtils.Log(string.Format("Google Drive folder status: {0}", googledrive.ClientFolderExists ? "Exists" : "Not Exists"));
                LogUtils.Log(string.Format("Google Drive Available Size: {0}", DirectoryUtils.PretifyBytes(googledrive.AvaiableMemoryInBytes)));
                LogUtils.Log(string.Format("Google Drive Used Size: {0}", DirectoryUtils.PretifyBytes(googledrive.UsedMemoryInBytes)));
                LogUtils.Log(string.Format("Google Drive mapping : {0}", MirrorUtils.MapFolder(googledrive)));

                this.tmrRootWatcher.Start();
            }
            catch (Exception ex)
            {
                LogUtils.LogE(ex);
            }
        }

        private void lnklblRootPreferences_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                RootPreferences getPreferences = new RootPreferences();
                getPreferences.Services.Add(amazon);
                getPreferences.Services.Add(dropbox);
                getPreferences.Services.Add(googledrive);
                getPreferences.Services.Add(onedrive);
                getPreferences.Services.Add(copydrive);
                getPreferences.ShowDialog();
            }
            catch (Exception ex)
            {
                LogUtils.LogE(ex);
            }
        }
    }
}
