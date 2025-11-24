using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

namespace Utilities
{
    public static class RegistryUtils
    {
        public static void SetRegistryValue(string key, string value)
        {
           
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\OCE\MARKETING", key, value);
        }

        public static object GetRegistryValue(string key)
        {
            Debug.Assert(!string.IsNullOrEmpty(key), "Invalid null key passed to GetRegistryValue");

            return Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\OCE\MARKETING", key, null);
        }

        public static void DeleteRegistryKey(string key)
        {

            try
            {
                Registry.CurrentUser.DeleteSubKey(@"HKEY_CURRENT_USER\SOFTWARE\OCE\MARKETING" + key, true);
            }

            catch (Exception ex)
            {

            }
        }

        public static string GetRegistryStringValue(string key, string defaultValue)
        {
            object regValue = GetRegistryValue(key);

            if (regValue is null)
            {
                return defaultValue;
            }

            return regValue.ToString();
        }

        public static T InitializeValFromReg<T>(string regKey, T defaultValu)
        {
            object regValu = GetRegistryValue(regKey);

            T rtrnValu;

            if (regValu is null)
            {
                return defaultValu;
            }

            else
            {
                try
                {
                    rtrnValu = (T)Convert.ChangeType(regValu, typeof(T));

                    return rtrnValu;
                }

                catch
                {
                    return defaultValu;
                }
            }
        }

       
    }
}
