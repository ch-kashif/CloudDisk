using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CloudDisk.CoreLibrary.utils
{
    public class ResourceUtils
    {
        private static string _Junction;
        public static string Junction
        {
            get
            {
                return _Junction;
            }
        }
        public static bool IsAdministrator
        {
            get
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();

                if (identity != null)
                {
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }

                return false;
            }
        }
        public static string ExecuteCommand(string command, string parameters)
        {
            try
            {
                ProcessStartInfo cmdsi = new ProcessStartInfo(command);
                cmdsi.Arguments = parameters;
                cmdsi.UseShellExecute = false;
                cmdsi.RedirectStandardOutput = true;
                cmdsi.CreateNoWindow = true;
                if (Environment.OSVersion.Version.Major > 5)
                {
                    if(!IsAdministrator)
                        cmdsi.Verb = "runas";
                }
                Process cmd = Process.Start(cmdsi);
                cmd.WaitForExit(5000);
                return string.Format("PROCESS OUTPUT: {0}", cmd.StandardOutput.ReadToEnd());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string ExecuteDOSCommand(string command)
        {
            try
            {
                if (!command.StartsWith("/c "))
                    command = "/c " + command;

                return ExecuteCommand("cmd.exe", command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetFolderIcon(string path, string folderToolTip)
        {
            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\icons\256x256.ico");
            if (File.Exists(path + @"\desktop.ini")) File.Delete(path + @"\desktop.ini");

            StreamWriter sw = File.CreateText(path + @"\desktop.ini");
            sw.WriteLine("[.ShellClassInfo]");
            sw.WriteLine("InfoTip=" + folderToolTip);
            sw.WriteLine("IconFile=" + iconPath);
            sw.WriteLine("IconIndex=0");
            sw.Close();
            sw.Dispose();

            File.SetAttributes(path + @"\desktop.ini", File.GetAttributes(path + @"\desktop.ini") | FileAttributes.Hidden);

            File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.System);
        }

        public static void RecreateAllExecutableResources()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string[] arrResources = currentAssembly.GetManifestResourceNames();

            foreach (string resourceName in arrResources)
            {
                if (resourceName.EndsWith(".exe"))
                {
                    string saveAsName = Path.Combine(Path.GetTempPath(), resourceName);

                    switch (resourceName.ToLower())
                    {
                        case "clouddisk.corelibrary.resources.junction.exe":
                            _Junction = saveAsName;
                            break;
                    }

                    FileInfo fileInfoOutputFile = new FileInfo(saveAsName);

                    if (fileInfoOutputFile.Exists)
                    {
                        fileInfoOutputFile.Delete();
                    }
                    FileStream streamToOutputFile = fileInfoOutputFile.OpenWrite();
                    Stream streamToResourceFile = currentAssembly.GetManifestResourceStream(resourceName);

                    const int size = 4096;

                    byte[] bytes = new byte[4096];
                    int numBytes;

                    while ((numBytes = streamToResourceFile.Read(bytes, 0, size)) > 0)
                    {
                        streamToOutputFile.Write(bytes, 0, numBytes);
                    }

                    streamToOutputFile.Close();
                    streamToResourceFile.Close();
                }
            }
        }
    }
}
