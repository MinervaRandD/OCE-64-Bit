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

namespace Utilities
{

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Security.Cryptography;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement;
    using TextBox = System.Windows.Forms.TextBox;

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
            catch
            {
            }

            return null;
        }

        private static Regex dblRegex = new Regex("^(([+-]?(([0-9]*[.])?[0-9]+)|([0-9]+[.])))$");

        private static Regex posDblRegex = new Regex("^(([+]?(([0-9]*[.])?[0-9]+)|([0-9]+[.])))$");

        //private static Regex dblRegex = new Regex("^([0-9]*[.])?[0-9]+$");
        private static Regex singleDigitFrac = new Regex("^(([0-9]*[.])?[0-9])$");
        private static Regex intRegex = new Regex("^[0-9]+$");
        private static Regex fractional = new Regex("^[0-9]*[.][0-9]+$");

        public static bool IsValidDbl(string sDbl)
        {
            if (string.IsNullOrWhiteSpace(sDbl))
            {
                return false;
            }

            return dblRegex.IsMatch(sDbl);
        }

        public static bool IsValidPosDbl(string sDbl)
        {
            if (string.IsNullOrWhiteSpace(sDbl))
            {
                return false;
            }

            return posDblRegex.IsMatch(sDbl);
        }

        public static bool IsValidSingleDigitFrac(string sDbl)
        {
            if (string.IsNullOrWhiteSpace(sDbl))
            {
                return false;
            }

            return singleDigitFrac.IsMatch(sDbl);
        }

        public static bool IsValidPosInt(string sInt)
        {
            if (string.IsNullOrWhiteSpace(sInt))
            {
                return false;
            }

            return intRegex.IsMatch(sInt.Trim());
        }

        public static bool HasFractionalPart(string sDbl)
        {
            if (string.IsNullOrWhiteSpace(sDbl))
            {
                return false;
            }

            return fractional.IsMatch(sDbl.Trim());
        }

        public static bool IsValidFeetAndOrInches(string value, bool allowNegatives = false)
        {
            if (IsValidFeetAndInchesFeet(value, allowNegatives))
            {
                return true;
            }

            if (IsValidFeet(value, allowNegatives))
            {
                return true;
            }

            if (IsValidInches(value, allowNegatives))
            {
                return true;
            }

            return false;
        }

        public static bool IsValidFeetAndInchesFeet(string parseText, bool allowNegatives = false)
        {
            Match m = null;

            if (!allowNegatives)
            {
                m = regexFeetAndInches.Match(parseText);
            }

            else
            {
                m = regexFeetAndInchesWithNegative.Match(parseText);
            }

            if (!m.Success)
            {
                return false;
            }

            GroupCollection gc = m.Groups;

            if (!allowNegatives)
            {
                if (gc.Count != 3)
                {
                    return false;
                }
            }

            else
            {
                if (gc.Count != 4)
                {
                    return false;
                }
            }

            int iTemp = 0;

            if (!int.TryParse(gc[1].Value, out iTemp))
            {
                return false;
            }

            if (!allowNegatives)
            {
                if (!int.TryParse(gc[2].Value, out iTemp))
                {
                    return false;
                }
            }

            else
            {
                if (!int.TryParse(gc[3].Value, out iTemp))
                {
                    return false;
                }
            }

            if (iTemp > 11)
            {
                return false;
            }

            return true;
        }

        public static bool IsValidFeet(string parseText, bool allowNegatives = false)
        {
            Match m = null;

            if (!allowNegatives)
            {
                m = regexFeet.Match(parseText);
            }

            else
            {
                m = regexFeetWithNegatives.Match(parseText);
            }

            if (!m.Success)
            {
                return false;
            }

            GroupCollection gc = m.Groups;

            if (!allowNegatives)
            {
                if (gc.Count != 2)
                {
                    return false;
                }
            }

            else
            {
                if (gc.Count != 3)
                {
                    return false;
                }
            }

            int iTemp = 0;

            if (!int.TryParse(gc[1].Value, out iTemp))
            {
                return false;
            }

            return true;
        }

        public static bool IsValidInches(string parseText, bool allowNegatives = false)
        {
            Match m = null;

            if (!allowNegatives)
            {
                m = regexInches.Match(parseText);
            }

            else
            {
                m = regexInchesWithNegatives.Match(parseText);
            }

            if (!m.Success)
            {
                return false;
            }

            GroupCollection gc = m.Groups;

            if (!allowNegatives)
            {
                if (gc.Count != 2)
                {
                    return false;
                }
            }

            else
            {
                if (gc.Count != 3)
                {
                    return false;
                }
            }

            int iTemp = 0;

            if (!int.TryParse(gc[1].Value, out iTemp))
            {
                return false;
            }

            if (iTemp > 11)
            {
                return false;
            }

            return true;
        }

        public static void SetTextFormatForValidPositiveDouble(System.Windows.Forms.TextBox texbox)
        {
            if (IsValidPosDbl(texbox.Text.Trim()))
            {
                texbox.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                texbox.BackColor = Color.Pink;
            }
        }

        public static string FormatInchesTo2DecimalPlaces(double inchesOnly)
        {
            int inchTimes100 = (int)Math.Round(100.0 * inchesOnly);

            string inchFormat = string.Empty;

            if (inchTimes100 % 100 == 0)
            {
                inchFormat = (inchTimes100 / 100).ToString("0");
            }

            else if (inchTimes100 % 10 == 0)
            {
                inchFormat = ((double)inchTimes100 / 100.0).ToString("0.0");
            }

            else
            {
                inchFormat = ((double)inchTimes100 / 100.0).ToString("0.00");
            }

            return inchFormat;
        }

        public static bool CheckTextBoxValidPositiveDouble(TextBox textbox, string msg)
        {
            if (!IsValidPosDbl(textbox.Text.Trim()))
            {
                ManagedMessageBox.Show("The value specified for " + msg + " is not a valid double precision value.");
                return false;
            }

            return true;
        }

        public static bool CheckTextBoxValidPositiveDouble(TextBox textbox, out double? value)
        {
            value = null;

            if (!IsValidPosDbl(textbox.Text.Trim()))
            {
                return false;
            }

            double result = 0;

            if (!double.TryParse(textbox.Text.Trim(), out result))
            {
                return false;
            }

            value = result;

            return true;
        }



        public static void SetTextFormatForValidInt(TextBox textbox)
        {
            if (IsValidPosInt(textbox.Text.Trim()))
            {
                textbox.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                textbox.BackColor = Color.Pink;
            }
        }

        public static bool CheckTextBoxValidInt(TextBox textbox, string msg)
        {
            if (!IsValidPosInt(textbox.Text.Trim()))
            {
                ManagedMessageBox.Show("The value specified for " + msg + " is not a valid integer precision value.");
                return false;
            }

            return true;
        }

        public static bool CheckTextBoxValidInt(TextBox textbox, out int? inches, bool allowNegatives = false)
        {
            inches = null;

            int inchesOut = 0;

            if (!int.TryParse(textbox.Text.Trim(), out inchesOut))
            {
                return false;
            }

            if (allowNegatives)
            {
                inches = inchesOut;
                return true;
            }

            if (inchesOut < 0)
            {
                return false;
            }

            inches = inchesOut;

            return true;

        }

        public static bool CheckTextBoxValidMeasurement(TextBox textbox, out int? feet, out int? inches,
            bool allowNegatives = false)
        {
            return CheckTextBoxValidMeasurementFeetAndOrInches(textbox, out feet, out inches, allowNegatives);
        }

        public static bool CheckTextBoxValidMeasurementFeetAndOrInches(TextBox textbox, out int? feet, out int? inches,
            bool allowNegatives = false)
        {

            if (CheckTextBoxValidMeasurementFeetAndInches(textbox, out feet, out inches, allowNegatives))
            {
                return true;
            }

            if (CheckTextBoxValidMeasurementFeet(textbox, out feet, allowNegatives))
            {
                inches = 0;

                return true;
            }

            if (CheckTextBoxValidMeasurementInches(textbox, out inches, allowNegatives))
            {
                feet = 0;
                return true;
            }

            feet = null;
            inches = null;

            return false;
        }

        private static string matchFeetAndInches = "^(0|[1-9][0-9]*)' +(\\d|[1][0-1])\\\"$";
        private static Regex regexFeetAndInches = new Regex(matchFeetAndInches);

        private static string matchFeetAndInchesWithNegative = "^([-]?(0|[1-9][0-9]*))' +(\\d|[1][0-1])\\\"$";
        private static Regex regexFeetAndInchesWithNegative = new Regex(matchFeetAndInchesWithNegative);

        public static bool CheckTextBoxValidMeasurementFeetAndInches(TextBox textbox, out int? feet, out int? inches,
            bool allowNegatives = false)
        {
            string parseText = textbox.Text.Trim();

            feet = null;
            inches = null;

            Match m = null;

            if (!allowNegatives)
            {
                m = regexFeetAndInches.Match(parseText);
            }

            else
            {
                m = regexFeetAndInchesWithNegative.Match(parseText);
            }

            if (!m.Success)
            {
                return false;
            }

            GroupCollection gc = m.Groups;

            if (!allowNegatives)
            {
                if (gc.Count != 3)
                {
                    return false;
                }
            }

            else
            {
                if (gc.Count != 4)
                {
                    return false;
                }
            }

            int iTemp = 0;

            if (!int.TryParse(gc[1].Value, out iTemp))
            {
                return false;
            }

            feet = iTemp;

            if (!allowNegatives)
            {
                if (!int.TryParse(gc[2].Value, out iTemp))
                {
                    return false;
                }
            }

            else
            {
                if (!int.TryParse(gc[3].Value, out iTemp))
                {
                    return false;
                }
            }

            if (iTemp > 11)
            {
                return false;
            }

            inches = iTemp;

            if (parseText.StartsWith("-"))
            {
                inches = -inches.Value;
            }

            return true;
        }

        private static string matchFeet = "^(0|[1-9][0-9]*)'$";
        private static Regex regexFeet = new Regex(matchFeet);

        private static string matchFeetWithNegatives = "^([-]?(0|[1-9][0-9]*))'$";
        private static Regex regexFeetWithNegatives = new Regex(matchFeetWithNegatives);

        public static bool CheckTextBoxValidMeasurementFeet(TextBox textbox, out int? feet, bool allowNegatives = false)
        {
            string parseText = textbox.Text.Trim();

            feet = null;

            Match m = null;

            if (!allowNegatives)
            {
                m = regexFeet.Match(parseText);
            }

            else
            {
                m = regexFeetWithNegatives.Match(parseText);
            }

            if (!m.Success)
            {
                return false;
            }

            GroupCollection gc = m.Groups;

            if (!allowNegatives)
            {
                if (gc.Count != 2)
                {
                    return false;
                }
            }

            else
            {
                if (gc.Count != 3)
                {
                    return false;
                }
            }

            int iTemp = 0;

            if (!int.TryParse(gc[1].Value, out iTemp))
            {
                return false;
            }

            feet = iTemp;

            return true;
        }

        private static string matchInches = "^(0|[1-9][0-9]*)\"$";
        private static Regex regexInches = new Regex(matchInches);

        private static string matchInchesWithNegatives = "^([-]?(0|[1-9][0-9]*))\"$";
        private static Regex regexInchesWithNegatives = new Regex(matchInchesWithNegatives);

        public static bool CheckTextBoxValidMeasurementInches(TextBox textbox, out int? inches,
            bool allowNegatives = false)
        {
            string parseText = textbox.Text.Trim();

            inches = null;

            Match m = null;

            if (!allowNegatives)
            {
                m = regexInches.Match(parseText);
            }

            else
            {
                m = regexInchesWithNegatives.Match(parseText);
            }

            if (!m.Success)
            {
                return false;
            }

            GroupCollection gc = m.Groups;

            if (!allowNegatives)
            {
                if (gc.Count != 2)
                {
                    return false;
                }
            }

            else
            {
                if (gc.Count != 3)
                {
                    return false;
                }
            }

            int iTemp = 0;

            if (!int.TryParse(gc[1].Value, out iTemp))
            {
                return false;
            }

            if (iTemp > 11)
            {
                return false;
            }

            inches = iTemp;

            return true;
        }

        public static void SnapToGrid(double frstPntX, double frstPntY, ref double scndPntX, ref double scndPntY,
            double snapResolutionInDegrees)
        {
            double dx = Math.Abs(frstPntX - scndPntX);
            double dy = Math.Abs(frstPntY - scndPntY);

            if (dx <= 1.0e-8)
            {
                // avoid divide by zero.
                scndPntX = frstPntX;

                return;
            }

            double atanInDegrees = MathUtils.C_180_over_pi * Math.Atan(dy / dx);

            if (atanInDegrees <= snapResolutionInDegrees)
            {
                scndPntY = frstPntY;
            }

            else if (atanInDegrees >= 90.0 - snapResolutionInDegrees)
            {
                scndPntX = frstPntX;
            }

        }

        public static void Swap<T>(ref T v1, ref T v2)
        {
            T vTemp = v1;
            v1 = v2;
            v2 = vTemp;
        }


        public static bool IsAllDigits(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return true;
            }

            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }

        public static void SetButtonState(ToolStripButton button, bool isChecked)
        {
            if (button.Checked != isChecked)
            {
                button.Checked = isChecked;
            }
        }

        public static void SetTabSelectedIndex(TabControl tabControl, int index, ref bool AllowTabSelection)
        {
            AllowTabSelection = true;

            tabControl.SelectedTab = tabControl.TabPages[index];

            AllowTabSelection = false;

            //if (tabControl.SelectedIndex != index)
            //{
            //    tabControl.SelectedIndex = index;
            //}
        }

        public static bool ColorEquals(Color color1, Color color2, bool useOpacity = false)
        {
            if (useOpacity)
            {
                if (color1.A != color2.A)
                {
                    return false;
                }
            }

            return color1.R == color2.R && color1.G == color2.G && color1.B == color2.B;
        }

        public static Color InvertColor(Color color)
        {
            int A = color.A;
            int R = Math.Max(255 - color.R, 127) / 2;
            int G = Math.Max(255 - color.G, 127) / 2;
            int B = Math.Max(255 - color.B, 127) / 2;

            //int R = color.R / 4;
            //int G = color.G / 4;
            //int B = color.B / 4;

            //double originalColorTotal = color.R + color.G + color.B;

            //double inverseColorTotal = R + G + B;

            //double scale = originalColorTotal / inverseColorTotal;

            //R = (int)((double)R * scale);
            //G = (int)((double)G * scale);
            //B = (int)((double)B * scale);

            return Color.FromArgb(A, R, G, B);
        }

        public static int GetFeet(double inches)
        {
            int iInches = (int)Math.Round(inches, 3);

            return iInches / 12;
        }

        public static double GetInch(double inches)
        {
            int iInches = (int)Math.Round(inches, 3);

            return iInches - 12 * GetFeet(inches);
        }

        public static bool Not(bool x) => (!x);

        public static bool IsNotNull(object obj)
        {
            return !(obj is null);
        }

        public static string IndexToLowerCaseString(uint index)
        {
            string result = string.Empty;

            while (index > 0)
            {
                char nextChar = (char)('a' + ((index - 1) % 26));

                result = nextChar + result;

                if (index <= 26)
                {
                    return result;
                }

                index /= 26;
            }

            return result;
        }

        public static string IndexToUpperCaseString(uint index)
        {
            string result = string.Empty;

            while (index > 0)
            {
                char nextChar = (char)('A' + ((index - 1) % 26));

                result = nextChar + result;

                if (index <= 26)
                {
                    return result;
                }

                index /= 26;
            }

            return result;
        }

        public static void TruncateFileFromEnd(string filePath, int desiredLength)
        {
            return;

            //try
            //{
            //    if (!File.Exists(filePath))
            //    {
            //        return;
            //    }

            //    long fileSize = new FileInfo(filePath).Length;

            //    if (fileSize <= desiredLength)
            //    {
            //        return;
            //    }

            //    using (MemoryStream memoryStream = new MemoryStream(desiredLength))
            //    {
            //        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            //        {
            //            fileStream.Seek(-desiredLength, SeekOrigin.End);
            //            fileStream.CopyTo(memoryStream);
            //            fileStream.SetLength(desiredLength);
            //            fileStream.Position = 0;
            //            memoryStream.Position = 0; // Begin from the start of the memory stream
            //            memoryStream.CopyTo(fileStream);
            //            fileStream.Flush();
            //            fileStream.Close();
            //        }


            //    }
            //}

            //catch
            //{

            //}
        }

        public static double FeetToInches(double feetDouble)
        {
            int feet = (int)Math.Floor(feetDouble);
            int inches = (int)Math.Round((feetDouble - feet) * 12.0);

            return feet * 12 + inches;
        }

        static char[] separators = new char[] { '\\' };

        public static string FilePathSummary(string fileFullPath, int summaryLevel, int rghtTruncate)
        {
            if (string.IsNullOrEmpty(fileFullPath))
            {
                return string.Empty;
            }

            if (summaryLevel <= 0)
            {
                return string.Empty;
            }

            string[] pathElements = fileFullPath.Split(separators);


            if (pathElements.Length <= rghtTruncate)
            {
                return string.Empty;
            }

            List<string> pathElementsList = new List<string>();

            for (int i = 0; i < pathElements.Length - rghtTruncate; i++)
            {
                pathElementsList.Add(pathElements[i]);
            }

            if (pathElementsList.Count <= 0)
            {
                return string.Empty;
            }

            string filePathSummary = string.Empty;

            if (pathElementsList.Count <= summaryLevel)
            {
                filePathSummary = pathElementsList[0];

                for (int i = 1; i < pathElementsList.Count; i++)
                {
                    filePathSummary += '\\' + pathElementsList[i];
                }
            }

            else
            {
                filePathSummary = "..";

                for (int i = pathElementsList.Count - summaryLevel; i < pathElementsList.Count; i++)
                {
                    filePathSummary += '\\' + pathElementsList[i];
                }
            }

            return filePathSummary;
        }

        public static Size MeasureString(string text, Font font)
        {
            Size textSize = TextRenderer.MeasureText(text, font);

            return textSize;
        }

        public static Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                throw new ArgumentException("Byte array is null or empty.");
            }


            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
    }
}

