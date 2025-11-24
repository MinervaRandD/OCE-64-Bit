using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;
using System.Windows.Forms;

namespace ProcessManager
{
    public static class RegistryUtils
    {


        public static void SetRegistryValue(string key, string value)
        {
            Debug.Assert(!string.IsNullOrEmpty(key), "Invalid null key passed to SetRegistryValue");
            //Debug.Assert(!string.IsNullOrEmpty(value), "Invalid null key passed to SetRegistryValue");

            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\OCE", key, value);
        }

        public static object GetRegistryValue(string key)
        {
            Debug.Assert(!string.IsNullOrEmpty(key), "Invalid null key passed to GetRegistryValue");

            return Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\OCE", key, null);
        }

        public static void DeleteRegistryKey(string key)
        {

            try
            {
                Registry.CurrentUser.DeleteSubKey(@"HKEY_CURRENT_USER\SOFTWARE\OCE\" + key, true);
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

        /// <summary>
        /// Sets the location (folder) where this user last loaded the pdf file. Marc Diamond 2019-01-19
        /// </summary>
        public static void SetBasePlanInitialDirectory(string pdfFilePath)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\OCE", "DrawingFilePath", pdfFilePath);
        }

        /// <summary>
        /// Gets the location (folder) where this user last loaded the pdf file. Marc Diamond 2019-01-19
        /// </summary>
        /// <returns>Returns the last loaded pdf file folder</returns>
        public static string GetBasePlanInitialDirectory()
        {
            string drawingFilePath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\OCE", "DrawingFilePath", null);

            if (drawingFilePath != null)
            {
                return drawingFilePath;
            }

            else
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
        }

        /// <summary>
        /// Sets the location (folder) where this user last loaded the pdf file. Marc Diamond 2019-01-19
        /// </summary>
        public static void SetBaseProjectInitialDirectory(string pdfFilePath)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\OCE", "ProjectFilePath", pdfFilePath);
        }

        /// <summary>
        /// Gets the location (folder) where this user last loaded the pdf file. Marc Diamond 2019-01-19
        /// </summary>
        /// <returns>Returns the last loaded pdf file folder</returns>
        public static string GetProjectPlanInitialDirectory()
        {
            string drawingFilePath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\OCE", "ProjectFilePath", null);

            if (drawingFilePath != null)
            {
                return drawingFilePath;
            }

            else
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
        }

        public static void SetupCheckBoxFromRegistry(string registryKey, CheckBox checkBox)
        {
            string registryValue = GetRegistryStringValue(registryKey, string.Empty);

            bool toCheck = false;

            bool.TryParse(registryValue, out toCheck);

            checkBox.Checked = toCheck;
        }

        public static void SetupRadioButtonFromRegistry(string registryKey, RadioButton radioButton)
        {
            string registryValue = GetRegistryStringValue(registryKey, string.Empty);

            bool toCheck = false;

            bool.TryParse(registryValue, out toCheck);

            radioButton.Checked = toCheck;
        }
    }
}
