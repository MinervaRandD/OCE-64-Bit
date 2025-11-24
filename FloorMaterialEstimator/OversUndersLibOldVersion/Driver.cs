using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace OversUndersLibOldVersion
{
    class Driver
    {
        public static double rollWidth;
        public static List<double> inputOWs = new List<double>(0);
        public static List<double> inputOLs = new List<double>(0);
        public static List<double> inputUWs = new List<double>(0);
        public static List<double> inputULs = new List<double>(0);

        public static void LoadSampleInput()
        {
            double out1 = 0;
            double out2 = 0;

            var ow = new List<double>() { 2, 2.75, 3.25, 4.5, 5.75 };
            var ol = new List<double>() { 35, 58, 6, 57, 42 };
            //var ow = new List<double>();// { 2, 2.75, 3.25, 4.5, 5.75 };
            //var ol = new List<double>();// { 35, 58, 6, 57, 42 };
            var uw = new List<double>() { 1.25, 1.5, 2.75, 3.5, 4.25, 5, 5.25, 5.5, 5.75 };
            var ul = new List<double>() { 63, 36, 23, 76, 11, 8, 66, 131, 28 };
            double netUnders = 0;

            GetOUsOutput(ow, ol, uw, ul, 144.0, out out1, out out2, out netUnders);
        }

        public static void GetOUsOutput(List<double> overWidths, List<double> overLengths, List<double> underWidths, List<double> underLengths,
                    double rollWidthInches, out double totalFillLength, out double wasteFactor, out double netUnders)
        {
            inputOWs = overWidths;
            inputOLs = overLengths;
            inputUWs = underWidths;
            inputULs = underLengths;

            FormatWid3Inches();
            rollWidth = rollWidthInches / 12;
            MainNonXAML.wid = rollWidth;
            PrioritiesX.CreatePriorities();
            ProcessOvers();

            //Output:
            totalFillLength = FuncsX.TotalCutLength();
            wasteFactor = (FuncsX.TotalCutLength() * (rollWidth)) / MainNonXAML.netUnders * 100 - 100;
            netUnders = MainNonXAML.netUnders;

            // Optional output:
            double CutsCount = MainNonXAML.cutCtr;
            string s = "";
            for (int i = 1; i <= MainNonXAML.cutCtr; i++)
            {
                s = s + Convert.ToString(MainNonXAML.cutLength[i]) + ",    ";
            }
            //System.Diagnostics.Debugger.Break();
        }

        // Order of events in xaml:
        // BtnConsolidate, BtnExactOUs, BtnGetComboItems, BtnGetCombos, BtnPrioritizeItems,
        // BtnGetBestCombo, BtnMoveOver, BtnNextOver

        // BtnConsolidate:       Main.CreateOriginalOUs();
        // BtnExactOUs:          MainWindow.xaml.csMoveExactOUs()  For display only - no changes in overs/unders apparently

        // BtnGetComboItems      Main.GetComboItems(); 
        // if (Main.oversFinished == true)
        ////    {
        ////        PrepareUnderFills();
        ////    }
        // BtnGetCombos:         Main.GetCombos();
        // BtnPrioritizeItems    Main.PrioritizeItems();
        // BtnGetBestCombo       Main.GetBestCombo();  
        // BtnMoveOver
        //// if (Main.oversFinished == true)
        ////   {
        ////       PrepareUnderFills();
        ////   }
        // BtnNextOver           Main.GetComboItems();

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
                        break; // Should never occur
                    }

                } while (true);
            }
            else
            {
                MainNonXAML.oversFinished = true;
                ProcessUnders();
                return;
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

        //  Default order of commands in XAML:

        //  List<Button> lstBtnProcessUs = new List<Button>  Default order of commands in XAML
        //      {
        //      BtnStartFills, BtnPrioritizeWidths, BtnFixedItems, BtnOtherItems, BtnPossibleItems,
        //      BtnGetCombosU, BtnNextFixedItems, BtnNextFill
        //      };


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

    }
}
