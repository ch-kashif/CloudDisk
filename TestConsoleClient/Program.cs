using CloudDisk.CoreLibrary.Interfaces;
using CloudDisk.CoreLibrary.StorageProviders;
using CloudDisk.CoreLibrary.utils;
using CloudDisk.Logger;
using System;
using System.IO;

namespace TestConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application start");
            try
            {
                LogUtils.Info("*********** APP STARTED **********");
                LogUtils.Info("Step # 1 : Unmapping previous drive.");

                string UserLinks = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Links\\Cloud Drive.lnk");

                MirrorUtils.UnmapDrive(ApplicationSettings.MappedDriveLetter);

                if (!Directory.Exists(ApplicationSettings.MappedFolder))
                    Directory.CreateDirectory(ApplicationSettings.MappedFolder);

                ResourceUtils.SetFolderIcon(ApplicationSettings.MappedFolder, "Cloude Plus Drive");

                LogUtils.Info("Removing favoriates....");
                if (File.Exists(UserLinks))
                    File.Delete(UserLinks);

                LogUtils.Info("Adding folder into favoriates");

                try
                {
                    MirrorUtils.CreateShortcut(ApplicationSettings.MappedFolder, UserLinks, "", "", "", Path.GetDirectoryName(UserLinks));
                }
                catch
                {

                }

                // this is comment by masroor
                LogUtils.Info(string.Format("Mapping drive {0} at {1}", ApplicationSettings.MappedDriveLetter, ApplicationSettings.MappedFolder));
                MirrorUtils.MapDrive(ApplicationSettings.MappedDriveLetter, ApplicationSettings.MappedFolder);
                MirrorUtils.SetDriveIcon(ApplicationSettings.MappedDriveLetter);

                LogUtils.Info("drive mapped");
                MirrorUtils.DisplayDriveInfo();
                ResourceUtils.RecreateAllExecutableResources();

                ICloudService dropbox = new DropBox();
                ICloudService googledrive = new GoogleDrive();
                ICloudService copydrive = new CopyDrive();
                ICloudService onedrive = new OneDrive();
                ICloudService amazon = new AmazonDrive();

                dropbox.AvaiableMemoryInBytes = DirectoryUtils.SizeInBytes(0, 2, 512, 0, 0);
                googledrive.AvaiableMemoryInBytes = DirectoryUtils.SizeInBytes(0, 15, 0, 0, 0);
                copydrive.AvaiableMemoryInBytes = DirectoryUtils.SizeInBytes(0, 15, 0, 0, 0);
                onedrive.AvaiableMemoryInBytes = DirectoryUtils.SizeInBytes(0, 7, 0, 0, 0);
                amazon.AvaiableMemoryInBytes = DirectoryUtils.SizeInBytes(0, 5, 0, 0, 0);

                LogUtils.Info("Unmapping drives...");
                LogUtils.Info(string.Format("Unmapping Dropbox: {0}", MirrorUtils.UnmapFolder(dropbox.ApplicationName)));
                LogUtils.Info(string.Format("Unmapping Google Drive: {0}", MirrorUtils.UnmapFolder(googledrive.ApplicationName)));
                LogUtils.Info(string.Format("Unmapping Copy Drive: {0}", MirrorUtils.UnmapFolder(copydrive.ApplicationName)));
                LogUtils.Info(string.Format("Unmapping One Drive: {0}", MirrorUtils.UnmapFolder(onedrive.ApplicationName)));
                LogUtils.Info(string.Format("Unmapping Amazon: {0}", MirrorUtils.UnmapFolder(amazon.ApplicationName)));

                LogUtils.Info(string.Format("Dropbox installed status: {0}", dropbox.IsInstalled));
                LogUtils.Info(string.Format("Dropbox client folder: {0}", dropbox.ClientFolder));
                LogUtils.Info(string.Format("Dropbox folder status: {0}", dropbox.ClientFolderExists ? "Exists" : "Not Exists"));
                LogUtils.Info(string.Format("Dropbox Drive Available Space: {0}", DirectoryUtils.PretifyBytes(dropbox.AvaiableMemoryInBytes)));
                LogUtils.Info(string.Format("Dropbox Drive Used Space: {0}", DirectoryUtils.PretifyBytes(dropbox.UsedMemoryInBytes)));
                LogUtils.Info(string.Format("Dropbox Drive mapping : {0}", MirrorUtils.MapFolder(dropbox)));
                LogUtils.Info("---------------------------------------------------------------");

                LogUtils.Info(string.Format("Amazon Drive installed status: {0}", amazon.IsInstalled));
                LogUtils.Info(string.Format("Amazon client folder: {0}", amazon.ClientFolder));
                LogUtils.Info(string.Format("Amazon folder status: {0}", amazon.ClientFolderExists ? "Exists" : "Not Exists"));
                LogUtils.Info(string.Format("Amazon Drive Available Space: {0}", DirectoryUtils.PretifyBytes(amazon.AvaiableMemoryInBytes)));
                LogUtils.Info(string.Format("Amazon Drive Used Space: {0}", DirectoryUtils.PretifyBytes(amazon.UsedMemoryInBytes)));
                LogUtils.Info(string.Format("Amazon Drive mapping : {0}", MirrorUtils.MapFolder(amazon)));
                LogUtils.Info("---------------------------------------------------------------");

                LogUtils.Info(string.Format("One Drive installed status: {0}", onedrive.IsInstalled));
                LogUtils.Info(string.Format("One Drive Client Folder: {0}", onedrive.ClientFolder));
                LogUtils.Info(string.Format("One Drive folder status: {0}", onedrive.ClientFolderExists ? "Exists" : "Not Exists"));
                LogUtils.Info(string.Format("One Drive Available Size: {0}", DirectoryUtils.PretifyBytes(onedrive.AvaiableMemoryInBytes)));
                LogUtils.Info(string.Format("One Drive Used Size: {0}", DirectoryUtils.PretifyBytes(onedrive.UsedMemoryInBytes)));
                LogUtils.Info(string.Format("One Drive mapping : {0}", MirrorUtils.MapFolder(onedrive)));
                LogUtils.Info("---------------------------------------------------------------");

                LogUtils.Info(string.Format("Copy Drive installed status: {0}", copydrive.IsInstalled));
                LogUtils.Info(string.Format("Copy Drive Client Folder: {0}", copydrive.ClientFolder));
                LogUtils.Info(string.Format("Copy Drive folder status: {0}", copydrive.ClientFolderExists ? "Exists" : "Not Exists"));
                LogUtils.Info(string.Format("Copy Drive Available Size: {0}", DirectoryUtils.PretifyBytes(copydrive.AvaiableMemoryInBytes)));
                LogUtils.Info(string.Format("Copy Drive Used Size: {0}", DirectoryUtils.PretifyBytes(copydrive.UsedMemoryInBytes)));
                LogUtils.Info(string.Format("Copy Drive mapping : {0}", MirrorUtils.MapFolder(copydrive)));
                LogUtils.Info("---------------------------------------------------------------");

                LogUtils.Info(string.Format("Google Drive installed status: {0}", googledrive.IsInstalled));
                LogUtils.Info(string.Format("Google Drive Client Folder: {0}", googledrive.ClientFolder));
                LogUtils.Info(string.Format("Google Drive folder status: {0}", googledrive.ClientFolderExists ? "Exists" : "Not Exists"));
                LogUtils.Info(string.Format("Google Drive Available Size: {0}", DirectoryUtils.PretifyBytes(googledrive.AvaiableMemoryInBytes)));
                LogUtils.Info(string.Format("Google Drive Used Size: {0}", DirectoryUtils.PretifyBytes(googledrive.UsedMemoryInBytes)));
                LogUtils.Info(string.Format("Google Drive mapping : {0}", MirrorUtils.MapFolder(googledrive)));
            }
            catch (Exception ex)
            {
                LogUtils.Error(ex.Message, ex);
            }
            Console.WriteLine("Press any key to end");
            Console.Read();
        }
    }
}
