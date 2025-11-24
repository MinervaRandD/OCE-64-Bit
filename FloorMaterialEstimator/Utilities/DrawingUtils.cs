

namespace Utilities
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DrawingUtils
    {
        public static Color ParseArgbColorStr(string argbColorStr)
        {
            if (!argbColorStr.ToLower().StartsWith("argb("))
            {
                return Color.Black;
            }

            argbColorStr = argbColorStr.Substring(5);

            if (!argbColorStr.EndsWith(")"))
            {
                return Color.Black;
            }

            argbColorStr = argbColorStr.Substring(0, argbColorStr.Length - 1);

            string[] term = argbColorStr.Split(new char[] { ',' });

            if (term.Length != 4)
            {
                return Color.Black;
            }

            try
            {
                for (int i = 0; i < 4; i++)
                {
                    term[i] = term[i].Trim();

                    if (!Utilities.IsAllDigits(term[i]))
                    {
                        return Color.Black;
                    }
                }
            }

            catch
            {
                return Color.Black;
            }

            int A = 0;
            int R = 0;
            int G = 0;
            int B = 0;

            if (!int.TryParse(term[0].Trim(), out A))
            {
                return Color.Black;
            }

            if (!int.TryParse(term[1].Trim(), out R))
            {
                return Color.Black;
            }

            if (!int.TryParse(term[2].Trim(), out G))
            {
                return Color.Black;
            }

            if (!int.TryParse(term[3].Trim(), out B))
            {
                return Color.Black;
            }

            if (A < 0 || A > 255 || R < 0 || R > 255 || G < 0 || G > 255 || B < 0 || B > 255)
            {
                return Color.Black;
            }

            return Color.FromArgb(A, R, G, B);
        }

        public static string FormatArgbColorStr(Color color)
        {
            return "Argb(" + color.A + ',' + color.R + ',' + color.G + ',' + color.B + ')';
        }

        public static Color modifyColorByIntensity(Color color, int intensity)
        {
            double intensityFactor = 0.85 * (1.0 - Math.Max(0.0, Math.Min(0.5, (double)intensity / 100.0)));

            double RDelta = (255.0 - (double)color.R) * intensityFactor;
            double GDelta = (255.0 - (double)color.G) * intensityFactor;
            double BDelta = (255.0 - (double)color.B) * intensityFactor;

            int R = Math.Min(255, Math.Max(0, color.R + (int)Math.Round(RDelta)));
            int G = Math.Min(255, Math.Max(0, color.G + (int)Math.Round(GDelta)));
            int B = Math.Min(255, Math.Max(0, color.B + (int)Math.Round(BDelta)));

            return Color.FromArgb(color.A, R, G, B);
        }
    }
}
