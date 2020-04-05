using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDisk.CoreLibrary.Interfaces
{
    public interface ICloudService
    {
        string ApplicationName { get; }
        bool IsInstalled { get; }
        string ClientFolder { get; }
        bool ClientFolderExists { get; }
        int Sort { get; set; }
        long AvaiableMemoryInBytes { get; set; }
        long UsedMemoryInBytes { get; }
        long FreeMemoryInBytes { get; }
    }
}
