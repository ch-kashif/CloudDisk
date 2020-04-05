using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using CloudDisk.CoreLibrary.HelperObjects;


namespace CloudDisk.CoreLibrary.utils
{
    public class RegistryUtils
    {
        public const string AVAILABLE_BYTES = "Available_Bytes";
        public const string SORT = "Sort";

        public static RegistryStructure LocalMachine = new RegistryStructure(Registry.LocalMachine, "SOFTWARE");
        public static RegistryStructure ApplicationRoot = new RegistryStructure(Registry.LocalMachine, @"SOFTWARE\CloudDrive");
        public static RegistryStructure CurrentUser = new RegistryStructure(Registry.CurrentUser, "SOFTWARE");
        public static RegistryStructure DrivesIcons = new RegistryStructure(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\DriveIcons");

        protected static ReadOnlyRegistryStructure CurrentUserPrograms = new ReadOnlyRegistryStructure(Registry.CurrentUser, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
        protected static ReadOnlyRegistryStructure LocalMachine32Programs = new ReadOnlyRegistryStructure(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
        protected static ReadOnlyRegistryStructure LocalMachine64Programs = new ReadOnlyRegistryStructure(Registry.LocalMachine, @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");

        #region Services Based Registry Structure
        public static RegistryStructure GoogleDrive = new RegistryStructure(Registry.LocalMachine, @"SOFTWARE\CloudDrive\GoogleDrive");
        public static RegistryStructure Dropbox = new RegistryStructure(Registry.LocalMachine, @"SOFTWARE\CloudDrive\Dropbox");
        public static RegistryStructure AmazonDrive = new RegistryStructure(Registry.LocalMachine, @"SOFTWARE\CloudDrive\AmazonDrive");
        public static RegistryStructure CopyDrive = new RegistryStructure(Registry.LocalMachine, @"SOFTWARE\CloudDrive\CopyDrive");
        public static RegistryStructure OneDrive = new RegistryStructure(Registry.LocalMachine, @"SOFTWARE\CloudDrive\SkyDrive");
        #endregion

        public static bool IsApplicationInstalled(string pApplicationName)
        {
            const string DISPLAY_NAME = "DisplayName";
            return CurrentUserPrograms.FindInSubKeys(DISPLAY_NAME, pApplicationName) ||
                    LocalMachine32Programs.FindInSubKeys(DISPLAY_NAME, pApplicationName) ||
                    LocalMachine64Programs.FindInSubKeys(DISPLAY_NAME, pApplicationName);
        }
    }
}
