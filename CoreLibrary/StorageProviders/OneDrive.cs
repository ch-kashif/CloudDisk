using CloudDisk.CoreLibrary.Interfaces;
using CloudDisk.CoreLibrary.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDisk.CoreLibrary.StorageProviders
{
    public class OneDrive:BaseStorageProvider, ICloudService
    {
        public override string ApplicationName
        {
            get { return "Microsoft OneDrive"; }
        }

        public override string ClientFolder
        {
            get 
            {
                if (!this.IsInstalled)
                    return "";

                const string AMAZON_CLOUD_PATH = @"Microsoft\SkyDrive\UserFolder";
                if (RegistryUtils.CurrentUser.Read(AMAZON_CLOUD_PATH) == null)
                {
                    return string.Empty;
                }
                else
                {
                    return RegistryUtils.CurrentUser.Read(AMAZON_CLOUD_PATH);
                }                    
            }
        }
        public override long AvaiableMemoryInBytes
        {
            get
            {
                string size = RegistryUtils.OneDrive.Read(RegistryUtils.AVAILABLE_BYTES);
                return string.IsNullOrEmpty(size) ? 0 : long.Parse(size);
            }
            set
            {
                RegistryUtils.OneDrive.Write(RegistryUtils.AVAILABLE_BYTES, value);
            }
        }
        public override int Sort
        {
            get
            {
                string sort = RegistryUtils.OneDrive.Read(RegistryUtils.SORT);
                return string.IsNullOrEmpty(sort) ? 0 : int.Parse(sort);
            }
            set
            {
                RegistryUtils.OneDrive.Write(RegistryUtils.SORT, value);
            }
        }
    }
}
