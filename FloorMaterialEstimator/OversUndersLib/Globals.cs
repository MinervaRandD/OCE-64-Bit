using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OversUndersLib
{
    public static class Globals
    {
        public static List<double> inputOWs;
        public static List<double> inputOLs;
        public static List<double> inputUWs;
        public static List<double> inputULs;

        public static double rollWidthInInches;

        public static double rollWidth => (rollWidthInInches / 12.0);

        public static double minOULength = 1;
        //public static double rollWidth = 12;
        public static double inch = 1.0 / 12.0;
        public static string folderFixed = @"C:\Users\Owner\Desktop\ProjectDataF";
        public static string[] filePath = new string[4];
        public static string[] fileName =
            {@"\DataFileOW.txt",
             @"\DataFileOL.txt",
             @"\DataFileUW.txt",
             @"\DataFileUL.txt"};
        public static void SetFolderPath()
        {
            for (int i = 0; i < 4; i++)
            {
                filePath[i] = folderFixed + fileName[i];
            }
        }
    }
}
