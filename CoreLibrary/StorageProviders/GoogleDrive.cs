using CloudDisk.CoreLibrary.Interfaces;
using CloudDisk.CoreLibrary.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CloudDisk.CoreLibrary.HelperObjects;
using System.Data;

namespace CloudDisk.CoreLibrary.StorageProviders
{
    public class GoogleDrive: BaseStorageProvider , ICloudService
    {
        public override string ApplicationName
        {
            get 
            { 
                return "Google Drive"; 
            }
        }
        public override string ClientFolder
        {
            get 
            {
                if (!this.IsInstalled)
                    return "";

                string PathToConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                PathToConfigFile = Path.Combine(PathToConfigFile, "Google\\Drive\\sync_config.db");
                try
                {
                    Dictionary<string, string> dbValues = new Dictionary<string, string>();
                    //@"Data Source="+ dbFilePath + ";Version=3;New=False;Compress=True;"; 
                    dbValues["Data Source"] = PathToConfigFile;
                    dbValues["Version"] = "3";
                    dbValues["New"] = "False";
                    dbValues["Compress"] = "True";
                    SQLiteDatabase db = new SQLiteDatabase(dbValues);
                    string sql = "select * from data where entry_key='local_sync_root_path'";
                    DataTable dtConfig = db.GetDataTable(sql);
                    return dtConfig.Rows[0]["data_value"].ToString().Substring(4);
                }
                catch(Exception ex)
                {
                    LogUtils.LogE(ex, "Ignored Exception");
                    return "";
                }
            }
        }
        public override long AvaiableMemoryInBytes
        {
            get
            {
                string size = RegistryUtils.GoogleDrive.Read(RegistryUtils.AVAILABLE_BYTES);
                return string.IsNullOrEmpty(size) ? 0 :  long.Parse(size);
            }
            set
            {
                RegistryUtils.GoogleDrive.Write(RegistryUtils.AVAILABLE_BYTES, value);
            }
        }
        public override int Sort
        {
            get
            {
                string sort = RegistryUtils.GoogleDrive.Read(RegistryUtils.SORT);
                return string.IsNullOrEmpty(sort) ? 0 : int.Parse(sort);
            }
            set
            {
                RegistryUtils.GoogleDrive.Write(RegistryUtils.SORT, value);
            }
        }
    }
}
