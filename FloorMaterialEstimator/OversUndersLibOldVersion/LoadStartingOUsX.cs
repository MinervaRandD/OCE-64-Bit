using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OversUndersLibOldVersion
{
    class LoadStartingOUsX // Now loaded from driver input
    {
        public static List<double> owRandom = new List<double>(0);
        public static List<double> olRandom = new List<double>(0);
        public static List<double> uwRandom = new List<double>(0);
        public static List<double> ulRandom = new List<double>(0);
        public static int oCount = 0;
        public static int uCount = 0;

        public static void LoadInputOUs() // no references found
        {
            owRandom = MainNonXAML.inputOWs;
            olRandom = MainNonXAML.inputOLs;
            uwRandom = MainNonXAML.inputUWs;
            ulRandom = MainNonXAML.inputULs;
            oCount = owRandom.Count;
            uCount = uwRandom.Count;
        }

        public static void LoadSavedOUs() // no references found
        {
            string[] linesOW = File.ReadAllLines(SettingsX.filePath[0]);
            string[] linesOL = File.ReadAllLines(SettingsX.filePath[1]);
            int i = 0;
            foreach (string line in linesOW)
            {
                try
                {
                    owRandom.Add(Convert.ToDouble(linesOW[i]));
                    olRandom.Add(Convert.ToDouble(linesOL[i]));
                    i++;
                }
                catch
                {
                    var result = MessageBox.Show("Invalid input format.", "Invalid Input");
                }

            }
            oCount = i;

            string[] linesUW = File.ReadAllLines(SettingsX.filePath[2]);
            string[] linesUL = File.ReadAllLines(SettingsX.filePath[3]);
            i = 0;
            foreach (string line in linesUW)
            {
                try
                {
                    uwRandom.Add(Convert.ToDouble(linesUW[i]));
                    ulRandom.Add(Convert.ToDouble(linesUL[i]));
                    i++;
                }
                catch
                {
                    var result = MessageBox.Show("Invalid input format.", "Invalid Input");
                }
            }
            uCount = i;
        }

        public static void ValidateInput()  // no references found
        {
            if (uCount == 0)
            {
                var result = MessageBox.Show("There are no unders to process.", "Invalid Input");
            }
        }

        public static void FormatWid3Inches()  // no references found - moved to Driver
        {
            double d = 0;
            double r = 0;
            for (int i = 0; i < uCount; i++)
            {
                r = Convert.ToInt32(uwRandom[i]);
                d = uwRandom[i] - Convert.ToInt32(uwRandom[i]);
                if (d > 0 && d <= .25)
                    uwRandom[i] = r + .25;
                else if (d > 0 && d <= .5)
                    uwRandom[i] = r + .50;
                else if (d > 0 && d < .75)
                    uwRandom[i] = r + .75;
            }
        }

    }
}
