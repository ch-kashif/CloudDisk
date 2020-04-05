using CloudDisk.CoreLibrary.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CloudDisk.CoreLibrary.StorageProviders
{
    public abstract class BaseStorageProvider
    {
        public abstract string ApplicationName
        {
            get;
        }
        public abstract string ClientFolder
        {
            get;
        }
        public bool IsInstalled
        {
            get
            {
                return RegistryUtils.IsApplicationInstalled(this.ApplicationName);
            }
        }
        public bool ClientFolderExists
        {
            get
            {
                string pathToCheck = this.ClientFolder;

                if(!string.IsNullOrEmpty(pathToCheck))
                    if (Directory.Exists(pathToCheck)) 
                        return true;

                return false;
            }
        }

        public abstract long AvaiableMemoryInBytes
        {
            get;
            set;
        }

        public abstract int Sort
        {
            get;
            set;
        }
        public long UsedMemoryInBytes 
        {
            get
            {
                if (this.IsInstalled)
                {
                    return DirectoryUtils.GetDirectorySize(this.ClientFolder);
                }
                else
                {
                    return 0;
                }
            }
        }

        public long FreeMemoryInBytes
        {
            get
            {
                return Math.Max(0, this.AvaiableMemoryInBytes - this.UsedMemoryInBytes);
            }
        }
    }
}
