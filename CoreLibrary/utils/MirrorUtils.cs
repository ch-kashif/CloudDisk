using CloudDisk.CoreLibrary.Interfaces;
using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CloudDisk.CoreLibrary.utils
{
    public class MirrorUtils
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DefineDosDevice(int flags, string devname, string path);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int QueryDosDevice(string devname, StringBuilder buffer, int bufSize);

        public static void MapDrive(char letter, string path)
        {
            if (!DefineDosDevice(0, devName(letter), path))
                throw new Win32Exception();
        }
        public static void SetDriveIcon(char letter)
        {
            string drive_letter = new string(letter, 1);
            string drive_icon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\icons\256x256.ico");
            drive_icon = drive_icon.Replace(@"\", @"\\");
            RegistryUtils.DrivesIcons.CreateSubKeyAndWrite(drive_letter + "\\DefaultIcon", "", drive_icon);
            RegistryUtils.DrivesIcons.CreateSubKeyAndWrite(drive_letter + "\\DefaultLabel", "", "Cloud Plus Drive");
        }
        public static void MapDriveInDos(char letter, string path)
        {
            string command = string.Format("subst {0} {1}", devName(letter), path);

            Console.WriteLine(ResourceUtils.ExecuteDOSCommand(command));
        }
        public static void UnmapDrive(char letter)
        {
            if (!DefineDosDevice(2, devName(letter), null))
                throw new Win32Exception();
        }
        public static string GetDriveMapping(char letter)
        {
            StringBuilder sb = new StringBuilder(259);
            if (QueryDosDevice(devName(letter), sb, sb.Capacity) == 0)
            {
                int err = Marshal.GetLastWin32Error();
                if (err == 2) return "";
                throw new Win32Exception();
            }
            return sb.ToString().Substring(4);
        }


        private static string devName(char letter)
        {
            return new string(char.ToUpper(letter), 1) + ":";
        }

        public static string GetNextAvailableDriveLetter()
        {
            try
            {

                DriveInfo[] allDrives = DriveInfo.GetDrives();
                string selected_letter = "";

                for (int i = 90, k = allDrives.Length - 1; i >= 65 && k >= 0; i--, k--)
                {
                    string letter = ((char)i).ToString();
                    if (!allDrives[k].Name.StartsWith(letter))
                    {
                        selected_letter = letter;
                        break;
                    }
                }

                return selected_letter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DisplayDriveInfo()
        {
            foreach (DriveInfo dinfo in DriveInfo.GetDrives())
            {
                Console.WriteLine("{0} => {1}", dinfo.Name, dinfo.DriveType);
            }
        }
        public static string MapFolder(ICloudService service)
        {
            try
            {
                if (service.IsInstalled)
                {
                    string mappedfolder = Path.Combine(ApplicationSettings.MappedFolder, service.ApplicationName);
                    string command = string.Format("\"{0}\"", ResourceUtils.Junction);
                    string parameters = string.Format("\"{0}\" \"{1}\"", mappedfolder, service.ClientFolder);
                    return ResourceUtils.ExecuteCommand(command, parameters);
                }
                else
                {
                    return "NOT MAPPED... Service is not installed on this machine.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string UnmapFolder(ICloudService service)
        {
            try
            {
                if (service.IsInstalled)
                {
                    string mappedfolder = Path.Combine(ApplicationSettings.MappedFolder, service.ApplicationName);
                    string command = string.Format("\"{0}\" -d \"{1}\" ", ResourceUtils.Junction, mappedfolder);
                    return ResourceUtils.ExecuteCommand(command, "");
                }
                else
                {
                    return "Already unmapped";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string UnmapFolder(string folderName)
        {
            try
            {
                string mappedfolder = Path.Combine(ApplicationSettings.MappedFolder, folderName);
                string command = string.Format("\"{0}\" -d \"{1}\" ", ResourceUtils.Junction, mappedfolder);
                return ResourceUtils.ExecuteCommand(command, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Make sure you use try/catch block because your App may has no permissions on the target path!



        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="SourceFile">A file you want to make shortcut to</param>
        /// <param name="ShortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        public static void CreateShortcut(string SourceFile, string ShortcutFile)
        {
            CreateShortcut(SourceFile, ShortcutFile, null, null, null, null);
        }

        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="SourceFile">A file you want to make shortcut to</param>
        /// <param name="ShortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        /// <param name="Description">Shortcut description</param>
        /// <param name="Arguments">Command line arguments</param>
        /// <param name="HotKey">Shortcut hot key as a string, for example "Ctrl+F"</param>
        /// <param name="WorkingDirectory">"Start in" shorcut parameter</param>
        public static void CreateShortcut(string SourceFile, string ShortcutFile, string Description,
           string Arguments, string HotKey, string WorkingDirectory)
        {
            // Check necessary parameters first:
            if (String.IsNullOrEmpty(SourceFile))
                throw new ArgumentNullException("SourceFile");
            if (String.IsNullOrEmpty(ShortcutFile))
                throw new ArgumentNullException("ShortcutFile");

            // Create WshShellClass instance:
            WshShell wshShell = new WshShell();

            // Create shortcut object:
            IWshRuntimeLibrary.IWshShortcut shorcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(ShortcutFile);

            // Assign shortcut properties:
            shorcut.TargetPath = SourceFile;
            shorcut.Description = Description;
            shorcut.IconLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\icons\256x256.ico");

            if (!String.IsNullOrEmpty(Arguments))
                shorcut.Arguments = Arguments;
            if (!String.IsNullOrEmpty(HotKey))
                shorcut.Hotkey = HotKey;
            if (!String.IsNullOrEmpty(WorkingDirectory))
                shorcut.WorkingDirectory = WorkingDirectory;

            // Save the shortcut:
            shorcut.Save();
        }
    }
}
