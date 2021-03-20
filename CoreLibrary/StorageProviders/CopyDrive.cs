using CloudDisk.CoreLibrary.HelperObjects;
using CloudDisk.CoreLibrary.Interfaces;
using CloudDisk.CoreLibrary.utils;
using CloudDisk.Logger;
using System;
using System.Data;
using System.IO;

namespace CloudDisk.CoreLibrary.StorageProviders
{
    public class CopyDrive : BaseStorageProvider, ICloudService
    {
        public override string ApplicationName
        {
            get { return "Copy"; }
        }
        public override string ClientFolder
        {
            get
            {
                if (!this.IsInstalled)
                    return "";

                string PathToConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                PathToConfigFile = Path.Combine(PathToConfigFile, @"Copy\config.db");
                try
                {
                    SQLiteDatabase db = new SQLiteDatabase(PathToConfigFile);
                    string sql = "SELECT * FROM config2 WHERE option LIKE 'csmRootPath'";
                    DataTable dtConfig = db.GetDataTable(sql);
                    return dtConfig.Rows[0]["value"].ToString();
                }
                catch (Exception ex)
                {
                    LogUtils.Error("Ignored Exception", ex);
                    return "";
                }
            }
        }
        public override long AvaiableMemoryInBytes
        {
            get
            {
                string size = RegistryUtils.CopyDrive.Read(RegistryUtils.AVAILABLE_BYTES);
                return string.IsNullOrEmpty(size) ? 0 : long.Parse(size);
            }
            set
            {
                RegistryUtils.CopyDrive.Write(RegistryUtils.AVAILABLE_BYTES, value);
            }
        }
        public override int Sort
        {
            get
            {
                string sort = RegistryUtils.CopyDrive.Read(RegistryUtils.SORT);
                return string.IsNullOrEmpty(sort) ? 0 : int.Parse(sort);
            }
            set
            {
                RegistryUtils.CopyDrive.Write(RegistryUtils.SORT, value);
            }
        }
    }
}
