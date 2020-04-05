using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace CloudDisk.CoreLibrary.HelperObjects
{
    public class ReadOnlyRegistryStructure
    {
        protected RegistryKey _BaseRegistryKey;
        protected string _SubKey;

        public ReadOnlyRegistryStructure(RegistryKey baseRegistry, string subKey)
        {
            this._BaseRegistryKey = baseRegistry;
            this._SubKey = subKey;
        }

        public string Read(string KeyName)
        {
            RegistryKey rk = _BaseRegistryKey;
            RegistryKey sk1 = rk.OpenSubKey(_SubKey);

            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    string[] keyTokens = KeyName.Split(new char[] { '\\' });

                    for (int i = 0; i < keyTokens.Length - 1; i++)
                    {
                        sk1 = sk1.OpenSubKey(keyTokens[i]);
                        if (sk1 == null) return null;
                    }

                    return (string)sk1.GetValue(keyTokens[keyTokens.Length-1]);
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool FindInSubKeys(string subKeyValueName, string keyValue)
        {
            try
            {
                RegistryKey rk = _BaseRegistryKey;
                RegistryKey key = rk.OpenSubKey(_SubKey);

                if (key == null)
                {
                    return false;
                }
                else
                {
                    foreach (string keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        string displayName = subkey.GetValue(subKeyValueName) as string;
                        if (string.Compare(keyValue,displayName,true) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
            }
            return false;
        }
    }
}
