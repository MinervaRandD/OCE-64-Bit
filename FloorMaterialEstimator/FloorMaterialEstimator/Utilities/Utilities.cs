//-------------------------------------------------------------------------------//
// <copyright file="Utilities.cs" company="Bruun Estimating, LLC">               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Utilities
{
    using Microsoft.Win32;

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Security.Cryptography;

    using FloorMaterialEstimator.ShortcutsAndSettings;

    public static class Utilities
    {
        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static byte[] DecryptFile(string inputFile)
        {
            try
            {
                string password = @"34FEdsg4";

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                //FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                //while ((data = cs.ReadByte()) != -1)
                //    fsOut.WriteByte((byte)data);

                //fsOut.Close();
                byte[] outBytes = null;
                List<byte> lOut = new List<byte>();

                while ((data = cs.ReadByte()) != -1)
                {
                    lOut.Add((byte)data);
                }
                outBytes = lOut.ToArray();
                cs.Close();
                fsCrypt.Close();
                return outBytes;
            }
            catch { }
            return null;
        }

        private static Regex dblRegex = new Regex("^([0-9]*[.])?[0-9]+$");
        private static Regex intRegex = new Regex("^[0-9]+$");

        public static bool IsValidDbl(string sDbl)
        {
            if (string.IsNullOrWhiteSpace(sDbl))
            {
                return false;
            }

            return dblRegex.IsMatch(sDbl);
        }

        public static bool IsValidInt(string sInt)
        {
            if (string.IsNullOrWhiteSpace(sInt))
            {
                return false;
            }

            return intRegex.IsMatch(sInt);
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

        const double c180_over_pi = 57.2957795130823;

        public static void SnapToGrid(double x1, double y1, ref double x2, ref double y2)
        {
            double dx = Math.Abs(x1 - x2);
            double dy = Math.Abs(y1 - y2);

            if (dx <= 1.0e-8)
            {
                // avoid divide by zero.
                x2 = x1;
                return;
            }

            double atanInDegrees = c180_over_pi * Math.Atan(dy / dx);

            if (atanInDegrees <= GlobalSettings.SnapResolutionInDegrees)
            {
                y2 = y1;
            }

            else if (atanInDegrees >= 90.0 - GlobalSettings.SnapResolutionInDegrees)
            {
                x2 = x1;
            }

        }

        /// <summary>
        /// Sets the location (folder) where this user last loaded the pdf file. Marc Diamond 2019-01-19
        /// </summary>
        public static void SetBasePlanInitialDirectory(string pdfFilePath)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\OCE", "DrawingFilePath", pdfFilePath);
        }

        public static void SetRegistryValue(string key, string value)
        {
            Debug.Assert(!string.IsNullOrEmpty(key), "Invalid null key passed to SetRegistryValue");
            Debug.Assert(!string.IsNullOrEmpty(value), "Invalid null key passed to SetRegistryValue");

            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\OCE", key, value);
        }

        public static object GetRegistryValue(string key)
        {
            Debug.Assert(!string.IsNullOrEmpty(key), "Invalid null key passed to GetRegistryValue");

            return Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\OCE", key, null);
        }

        public static T InitializeValFromReg<T>(string regKey, T defaultValu)
        {
            object regValu = Utilities.GetRegistryValue(regKey);

            T rtrnValu;

            if (regValu == null)
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

        public static void Swap<T>(ref T v1, ref T v2)
        {
            T vTemp = v1;
            v1 = v2;
            v2 = vTemp;
        }
    }
}
