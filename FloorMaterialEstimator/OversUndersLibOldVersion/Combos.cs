using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OversUndersLibOldVersion
{
    public class Combos
    {
        public static int[] cs = new int[30];
        public static int[] csMax = new int[30];
        public static List<string> comboStr = new List<string>(0);
        public static int band = 0;

        public static void CreateCombos(int max)
        {
            comboStr.Clear();
            int lo = 0;
            int hi = 0;
            int ctr = 0;
            string s = "";
            band = 1;
            for (int i = 1; i <= max; i++)
            {
                cs[1] = i;
                comboStr.Add(Convert.ToString(cs[1]) + " -");
            }
            for (int i = 2; i <= max; i++)
            {
                band = i;
                lo = band - 1;
                hi = band;
                for (int j = 1; j <= band; j++)
                {
                    cs[j] = j;
                    csMax[j] = j + max - band;
                }
                cs[band] = cs[band] - 1;
            redo:
                cs[band] = cs[band] + 1;
                if (cs[band] <= csMax[band])
                {

                    //WriteCombo
                    s = "";
                    for (int k = 1; k <= band; k++)
                    {
                        s=s+ Convert.ToString(cs[k] + " -");
                    }
                    comboStr.Add(s);
                    //WriteCombo

                    goto redo;
                }

            redo1:
                if (cs[lo] > csMax[lo])
                {
                    lo = lo - 1;
                    if (lo < 1)
                    {
                        goto nextband;
                    }
                    goto redo1;
                }
                cs[lo] = cs[lo] + 1;

                //RestLo
                ctr = 0;
                for (int k=lo + 1 ; k <= band; k++)
                {
                    ctr = ctr + 1;
                    cs[k] = cs[lo] + ctr;
                }
                lo = band - 1;
                //RestLo

                if (cs[band] <= csMax[band])

                {
                    //WriteCombo
                    s = "";
                    for (int k = 1; k <= band; k++)
                    {
                        s = s + Convert.ToString(cs[k] + " -");
                    }
                    comboStr.Add(s);
                    //WriteCombo
                }


                goto redo;
                nextband:
                {

                }
            }

        }

    }
}
 