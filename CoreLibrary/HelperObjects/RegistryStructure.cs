using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace CloudDisk.CoreLibrary.HelperObjects
{
    public class RegistryStructure:ReadOnlyRegistryStructure
    {

        public RegistryStructure(RegistryKey baseRegistry, string subKey):base(baseRegistry, subKey)
        {
            
        }
        public bool CreateSubKeyAndWrite(string SubKey,string KeyName, object Value)
        {
            try
            {
                RegistryKey rk = _BaseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(_SubKey);

                sk1 = sk1.CreateSubKey(SubKey);
                sk1.SetValue(KeyName.ToUpper(), Value);

                return true;
            }
            catch 
            {
                return false;
            }
        }
        public bool Write(string KeyName, object Value)
        {
            try
            {
                RegistryKey rk = _BaseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(_SubKey);
                sk1.SetValue(KeyName.ToUpper(), Value);

                return true;
            }
            catch
            {
                return false;
            }
        }
        
    }
}
