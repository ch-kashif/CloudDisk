using CloudDisk.CoreLibrary.Interfaces;
using CloudDisk.CoreLibrary.utils;

namespace CloudDisk.CoreLibrary.StorageProviders
{
    public class AmazonDrive : BaseStorageProvider, ICloudService
    {
        public override string ApplicationName
        {
            get { return "Amazon Cloud Drive"; }
        }
        public override string ClientFolder
        {
            get
            {
                if (!this.IsInstalled)
                    return "";

                const string AMAZON_CLOUD_PATH = @"Amazon\AmazonCloudDrive\SyncRoot";
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
                string size = RegistryUtils.AmazonDrive.Read(RegistryUtils.AVAILABLE_BYTES);
                return string.IsNullOrEmpty(size) ? 0 : long.Parse(size);
            }
            set
            {
                RegistryUtils.AmazonDrive.Write(RegistryUtils.AVAILABLE_BYTES, value);
            }
        }

        public override int Sort
        {
            get
            {
                string sort = RegistryUtils.AmazonDrive.Read(RegistryUtils.SORT);
                return string.IsNullOrEmpty(sort) ? 0 : int.Parse(sort);
            }
            set
            {
                RegistryUtils.AmazonDrive.Write(RegistryUtils.SORT, value);
            }
        }

    }
}
