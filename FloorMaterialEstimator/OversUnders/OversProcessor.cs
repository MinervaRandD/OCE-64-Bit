using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OversUnders
{
    public class OversProcessor
    {
    }

    private void consolidate()
    {
        Main.CreateOriginalOUs();
        string s = "";
        {
            ListBoxB1.Items.Clear();
            int j = Main.originalOverCtr;
            for (int i = 1; i <= j; i++)
            {
                s = Funcs.FormatOU(i, Main.originalOW[i], Main.originalOL[i]);
                ListBoxB1.Items.Add(s);
            }
            ListBoxB2.Items.Clear();
            j = Main.originalUnderCtr;
            for (int i = 1; i <= j; i++)
            {
                s = Funcs.FormatOU(i, Main.originalUW[i], Main.originalUL[i]);
                ListBoxB2.Items.Add(s);
            }
            ResetListColors();
            HighlightList(7, "grey");
            HighlightList(8, "grey");
            BtnConsolidate.IsEnabled = false;
            BtnSave.IsEnabled = true;
            BtnExactOUs.IsEnabled = true;
            //testCtr++;
            //txtBlockCtr.Text=Convert.ToString(testCtr);
        }
    }
}
