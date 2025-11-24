using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OversUndersLib
{
    public static class FuncsX
    {
        public static List<double> OUList = new List<double>(0);

        static public string FormatOU(int index, double width, double length)
        {
            string s1 = DblToStr(width);
            string s2 = DblToStr(length);
            string s3 = "";
            if (index != -1)
            {
                if (index < 10)
                {
                    s3 = "  " + index.ToString() + ") ";
                }
                else
                {
                    s3 = index.ToString() + ") ";
                }
            }
            if (length < 10)
            {
                s2 = "  " + s2;
            }
            return s3 + s1 + "  x  " + s2;
        }
        public static string DblToStr(double width)
        {
            double n = Math.Round((width), 2);
            string s = n.ToString("N2");
            return s;
        }

        public static Double FRem(double d)
        {
            double t = 0;
            if (d > 0)
            {
                double j = Math.Truncate(SettingsX.rollWidth / d);
                for (int i = 1; i <= j; i++)
                {
                    t = t + d;
                }
                return SettingsX.rollWidth - t;
            }
            else
            {
                return 0;
            }
        }

        public static Double TotalCutLength()
        {
            int j = MainNonXAML.cutCtr;
            double n = 0;
            for (int i = 1; i <= j; i++)
            {
                n = n + MainNonXAML.cutLength[i];
            }
            return n;
        }
       
    }
   
}
