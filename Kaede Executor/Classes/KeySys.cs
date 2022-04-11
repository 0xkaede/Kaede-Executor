using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kaede_Executor.Classes
{
    internal class KeySys
    {
        static readonly string Domain = "http://kaedeport.tk:443/";
        static readonly string Key = Domain + $"GetKey?HWID={MachineGuid()}";

        public static string MachineGuid()
        {
            if (RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Microsoft\Cryptography") == null)
                throw new KeyNotFoundException(string.Format("Registry key not found: {0}", @"SOFTWARE\Microsoft\Cryptography"));
            if (RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Microsoft\Cryptography").GetValue("MachineGuid") == null)
                throw new IndexOutOfRangeException(string.Format("Index not found: {0}", "MachineGuid"));
            string myhwid = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Microsoft\Cryptography").GetValue("MachineGuid").ToString();
            return myhwid;
        }

        public static bool CanAccess(string key)
        {
            var StatusAPI = new WebClient().DownloadString(Key);
            DataBase StatusResponse = JsonConvert.DeserializeObject<DataBase>(StatusAPI);

            if (key == StatusResponse.Key)
            {
                if (MachineGuid() == StatusResponse.HWID.ToString())
                {
                    var path = Util.Key;
                    File.WriteAllText(path, key);

                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static void GenKey()
            => Process.Start(Key);

        public static string GetKeyFromini()
        {
            var path = Util.Key;

            if (!File.Exists(path))
                File.WriteAllText(path, "Enter your Key...");

            var txt = File.ReadAllText(path);
            return txt;
        }
    }
}
