using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDisk.CoreLibrary.utils
{
    public class DirectoryUtils
    {
        public static long GetDirectorySize(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);

                return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            }
            return 0;
        }

        public static string PretifyBytes(long bytes)
        {
            string[] suffix = new string[] { "bytes", "KB", "MB", "GB", "TB" };
            double smart_size = bytes;
            int suffix_index = 0;
            while (bytes > 1024)
            {
                smart_size /= 1024;
                bytes /= 1024;
                suffix_index++;
            }
            return string.Format("{0} {1}", smart_size.ToString("####.##"), suffix[suffix_index]);
        }
        public static long SizeInBytes(int InTB, int InGB, int InMB, int InKB, int InBytes)
        {
            return ((((((long)InTB * 1024) + (long)InGB) * 1025) + (long)InMB) * 1024 + (long)InKB) * 1024 + (long)InBytes;
        }
    }
}
