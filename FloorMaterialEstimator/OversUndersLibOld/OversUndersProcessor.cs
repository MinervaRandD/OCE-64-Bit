

namespace OversUndersLib
{
    using OversUnders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class OversUndersProcessor
    {

        public static double rollWidth;

        public static List<double> inputOWs;
        public static List<double> inputOLs;
        public static List<double> inputUWs;
        public static List<double> inputULs;


        public OversUndersProcessor() { }

        public static void GetOUsOutput(
            List<MaterialArea> oversList,
            List<MaterialArea> undrsList,
            double rollWidthInches,
            out double totalFillLength,
            out double wasteFactor)
        {
            List<double> overWidths = new List<double>();
            List<double> overLngths = new List<double>();
            List<double> undrWidths = new List<double>();
            List<double> undrLngths = new List<double>();

            foreach (MaterialArea over in oversList)
            {
                overWidths.Add(over.WidthInInches);
                overLngths.Add(over.LngthInInches);
            }

            foreach (MaterialArea undr in undrsList)
            {
                undrWidths.Add(undr.WidthInInches);
                overLngths.Add(undr.LngthInInches);
            }

            OversUndersProcessor.GetOUsOutput(overWidths, overLngths, undrWidths, undrLngths, rollWidthInches, out totalFillLength, out wasteFactor);
        }

        public static void GetOUsOutput(
            List<double> overWidths,
            List<double> overLengths,
            List<double> underWidths,
            List<double> underLengths,
            double rollWidthInches,
            out double totalFillLength,
            out double wasteFactor)
        {
            inputOWs = new List<double>(overWidths);
            inputOLs = new List<double>(overLengths);
            inputUWs = new List<double>(underWidths);
            inputULs = new List<double>(underLengths);

            FormatWid3Inches();
            rollWidth = rollWidthInches / 12;
            PrioritiesX.CreatePriorities();
            ProcessOvers();

            //Output:
            totalFillLength = FuncsX.TotalCutLength();
            wasteFactor = (FuncsX.TotalCutLength() * (SettingsX.rollWidth)) / MainNonXAML.netUnders * 100 - 100;

            // Optional output:
            double CutsCount = MainNonXAML.cutCtr;
            string s = "";
            for (int i = 1; i <= MainNonXAML.cutCtr; i++)
            {
                s = s + Convert.ToString(MainNonXAML.cutLength[i]) + ",    ";
            }
            System.Diagnostics.Debugger.Break();
        }

        public static void FormatWid3Inches()
        {
            double d = 0;
            double r = 0;
            for (int i = 0; i < inputOWs.Count; i++)
            {
                d = inputOWs[i];
                r = Math.Truncate(inputOWs[i]);
                double f = d - r;
                if (Math.Abs(f) > .01)
                {
                    if (f > 0 && f <= .25)
                        inputOWs[i] = r + .25;
                    else if (f > 0 && f <= .5)
                        inputOWs[i] = r + .50;
                    else if (f > 0 && f <= .75)
                        inputOWs[i] = r + .75;
                    else
                        inputOWs[i] = r + 1.0;
                }
                else
                {
                    inputOWs[i] = r;
                }
            }

            for (int i = 0; i < inputUWs.Count; i++)
            {
                d = inputUWs[i];
                r = Math.Truncate(inputUWs[i]);
                double f = d - r;
                if (Math.Abs(f) > .01)
                {
                    if (f > 0 && f <= .25)
                        inputUWs[i] = r + .25;
                    else if (f > 0 && f <= .5)
                        inputUWs[i] = r + .50;
                    else if (f > 0 && f <= .75)
                        inputUWs[i] = r + .75;
                    else
                        inputUWs[i] = r + 1.0;
                }
                else
                {
                    inputUWs[i] = r;
                }
            }
        }

        public static void ProcessOvers()
        {
            MainNonXAML.CreateOriginalOUs(); // Checked
            if (MainNonXAML.overCtr > 0)
            {
                int i = 1;
                do
                {
                    i++;
                    if (i > 1000)
                    {
                        MessageBox.Show("Endless loop encountered.", "Driver.cs");
                    }
                    if (MainNonXAML.oversFinished == false)
                    {
                        MainNonXAML.GetComboItems();
                        if (MainNonXAML.oversFinished == true)
                        {
                            ProcessUnders();
                            return;
                        }
                        MainNonXAML.GetCombos();
                        MainNonXAML.PrioritizeItems();
                        MainNonXAML.GetBestCombo();
                        MainNonXAML.MoveOver();
                        if (MainNonXAML.oversFinished == true)
                        {
                            ProcessUnders();
                            return;
                        }
                    }
                    else
                    {
                        break;
                    }

                } while (true);
            }
        }

        public static void ProcessUnders()
        {
            MainNonXAML.GetNetUnders();
            int ctr = 0;
            int nextAction = 1;
            do
            {
                ctr++;
                if (ctr > 1000)
                {
                    MessageBox.Show("Endless loop encountered.", "Driver.cs");
                    return;
                }
                else
                    switch (nextAction)
                    {
                        case 1:
                            MainNonXAML.PrioritizeWidths();
                            if (MainNonXAML.undersDone == true)
                            {
                                return;
                            }
                            nextAction = 2;
                            break;
                        case 2:
                            MainNonXAML.FixedItems();
                            nextAction = 3;
                            break;
                        case 3:
                            MainNonXAML.OtherItems();
                            nextAction = 4;
                            break;
                        case 4:
                            MainNonXAML.PossibleItems();
                            nextAction = 5;
                            break;
                        case 5:
                            MainNonXAML.GetCombosU();
                            switch (MainNonXAML.nextAction)
                            {
                                case "Other Items":
                                    nextAction = 3;
                                    break;
                                case "Next Fixed Items":
                                    nextAction = 6;
                                    break;
                                case "Next Fill":
                                    nextAction = 7;
                                    break;
                                default:
                                    nextAction = 6;
                                    break;
                            }
                            break;
                        case 6:
                            MainNonXAML.NextFixedItems();
                            nextAction = 4;
                            break;
                        case 7:
                            MainNonXAML.NextFill();
                            if (MainNonXAML.underCtr == 0)
                            {
                                return;
                            }
                            nextAction = 1;
                            break;
                    }

            }
            while (true);
        }

    }
}
