using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDisk.CoreLibrary.utils
{
    public class ApplicationSettings
    {
        private const string REGKEY_APPLICATION_MAPPED_DRIVE_LETTER = "MappedDrive";
        private const string REGKEY_APPLICATION_MAPPED_FOLDER_PATH = "MappedFolder";
        private const string REGKEY_APPLICATION_LOGS_FOLDER_PATH = "Logs_Folder_Path";
        private const string REGKEY_APPLICATION_PREFERENCED_SERVICE = "Preferred_Service";
        private const string REGKEY_APPLICATION_ASK_FOR_PREFERENCED_SERVICE = "Ask_For_Preferred_Service";

        public static char MappedDriveLetter
        {
            get
            {
                string letter = "";
                
                if (RegistryUtils.ApplicationRoot.Read(REGKEY_APPLICATION_MAPPED_DRIVE_LETTER) == null)
                {
                    letter = MirrorUtils.GetNextAvailableDriveLetter();
                    RegistryUtils.ApplicationRoot.Write(REGKEY_APPLICATION_MAPPED_DRIVE_LETTER, letter);
                }

                letter = RegistryUtils.ApplicationRoot.Read(REGKEY_APPLICATION_MAPPED_DRIVE_LETTER);
                return letter[0];
            }
        }

        public static string MappedFolder
        {
            get
            {
                string path = "";

                if (RegistryUtils.ApplicationRoot.Read(REGKEY_APPLICATION_MAPPED_FOLDER_PATH) == null)
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CloudDriveMappedFolder");
                    RegistryUtils.ApplicationRoot.Write(REGKEY_APPLICATION_MAPPED_FOLDER_PATH, path);
                }

                path = RegistryUtils.ApplicationRoot.Read(REGKEY_APPLICATION_MAPPED_FOLDER_PATH);
                return path;
            }
        }

        public static string PreferredService
        {
            get
            {
                string service = "";

                if (RegistryUtils.ApplicationRoot.Read(REGKEY_APPLICATION_PREFERENCED_SERVICE) == null)
                {
                    service = "";
                    RegistryUtils.ApplicationRoot.Write(REGKEY_APPLICATION_PREFERENCED_SERVICE, service);
                }

                service = RegistryUtils.ApplicationRoot.Read(REGKEY_APPLICATION_PREFERENCED_SERVICE);
                return service;
            }
            set
            {
                RegistryUtils.ApplicationRoot.Write(REGKEY_APPLICATION_PREFERENCED_SERVICE, value);
            }
        }

        public static bool AskForPreferredService
        {
            get
            {
                bool flag = true;

                if (RegistryUtils.ApplicationRoot.Read(REGKEY_APPLICATION_ASK_FOR_PREFERENCED_SERVICE) == null)
                {
                    flag = true;
                    RegistryUtils.ApplicationRoot.Write(REGKEY_APPLICATION_ASK_FOR_PREFERENCED_SERVICE, flag);
                }

                flag = bool.Parse(RegistryUtils.ApplicationRoot.Read(REGKEY_APPLICATION_ASK_FOR_PREFERENCED_SERVICE));
                return flag;
            }
            set
            {
                RegistryUtils.ApplicationRoot.Write(REGKEY_APPLICATION_ASK_FOR_PREFERENCED_SERVICE, value);
            }
        }
        public static string LogsFolderPath
        {
            get
            {
                string path = "";

                if (RegistryUtils.ApplicationRoot.Read(REGKEY_APPLICATION_LOGS_FOLDER_PATH) == null)
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                    RegistryUtils.ApplicationRoot.Write(REGKEY_APPLICATION_LOGS_FOLDER_PATH, path);
                }

                path = RegistryUtils.ApplicationRoot.Read(REGKEY_APPLICATION_LOGS_FOLDER_PATH);
                return path;
            }
        }
    }
}
