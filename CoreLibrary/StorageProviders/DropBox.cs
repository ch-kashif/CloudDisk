using CloudDisk.CoreLibrary.Interfaces;
using CloudDisk.CoreLibrary.utils;
using System;
using System.IO;

namespace CloudDisk.CoreLibrary.StorageProviders
{
    public class DropBox : BaseStorageProvider, ICloudService
    {
        public override string ApplicationName
        {
            get
            {
                return "DropBox";
            }
        }
        public override string ClientFolder
        {
            get
            {
                if (!this.IsInstalled)
                    return "";

                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dropbox\\host.db");

                if (File.Exists(dbPath))
                {
                    byte[] dbBase64Text = Convert.FromBase64String(System.IO.File.ReadAllText(dbPath));

                    string folderPath = System.Text.ASCIIEncoding.ASCII.GetString(dbBase64Text);

                    int colon_index = folderPath.IndexOf(":");

                    if (colon_index > -1)
                    {
                        folderPath = folderPath.Substring(colon_index - 1);
                    }
                    return folderPath;
                }
                else
                {
                    return "";
                }
            }
        }
        public override long AvaiableMemoryInBytes
        {
            get
            {
                string size = RegistryUtils.Dropbox.Read(RegistryUtils.AVAILABLE_BYTES);
                return string.IsNullOrEmpty(size) ? 0 : long.Parse(size);
            }
            set
            {
                RegistryUtils.Dropbox.Write(RegistryUtils.AVAILABLE_BYTES, value);
            }
        }
        public override int Sort
        {
            get
            {
                string sort = RegistryUtils.Dropbox.Read(RegistryUtils.SORT);
                return string.IsNullOrEmpty(sort) ? 0 : int.Parse(sort);
            }
            set
            {
                RegistryUtils.Dropbox.Write(RegistryUtils.SORT, value);
            }
        }
    }
}
