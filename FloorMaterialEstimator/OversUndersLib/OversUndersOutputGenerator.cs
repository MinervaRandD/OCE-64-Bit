using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace OversUndersLib
{
    public class OversUndersOutputGenerator
    {


        //public static void LoadSampleInput()
        //{
        //    double out1 = 0;
        //    double out2 = 0;

        //    var ow = new List<double>() { 2, 2.75, 3.25, 4.5, 5.75 };
        //    var ol = new List<double>() { 35, 58, 6, 57, 42 };
        //    //var ow = new List<double>();// { 2, 2.75, 3.25, 4.5, 5.75 };
        //    //var ol = new List<double>();// { 35, 58, 6, 57, 42 };
        //    var uw = new List<double>() { 1.25, 1.5, 2.75, 3.5, 4.25, 5, 5.25, 5.5, 5.75 };
        //    var ul = new List<double>() { 63, 36, 23, 76, 11, 8, 66, 131, 28 };
        //    double netUnders = 0;

        //    GetOUsOutput(ow, ol, uw, ul, 144.0, out out1, out out2, out netUnders);
        //}

        public MainNonXAML mainNonXAML = null;
        public PrioritiesX prioritiesX = null;
        public FuncsX funcsX = null;

        public OversUndersOutputGenerator(
            List<double> overWidths
            ,List<double> overLengths
            ,List<double> underWidths
            ,List<double> underLengths,
             double rollWidthInches)
        {
            Globals.inputOWs = new List<double>(overWidths);
            Globals.inputOLs = new List<double>(overLengths);
            Globals.inputUWs = new List<double>(underWidths);
            Globals.inputULs = new List<double>(underLengths);

            Globals.rollWidthInInches = rollWidthInches;
        }

        public void GetOUsOutput(out double totalFillLength, out double wasteFactor, out double netUnders)
        {
            

            FormatWid3Inches();

            prioritiesX = new PrioritiesX();
            funcsX = new FuncsX();

            mainNonXAML = new MainNonXAML(prioritiesX, funcsX);

            mainNonXAML.wid = Globals.rollWidth;

            prioritiesX.CreatePriorities();

            ProcessOvers();

            //Output:
            totalFillLength = funcsX.TotalCutLength(mainNonXAML);
            wasteFactor = (funcsX.TotalCutLength(mainNonXAML) * (Globals.rollWidth)) / mainNonXAML.netUnders * 100 - 100;
            netUnders = mainNonXAML.netUnders;

            // Optional output:
            double CutsCount = mainNonXAML.cutCtr;
            string s = "";
            for (int i = 1; i <= mainNonXAML.cutCtr; i++)
            {
                s = s + Convert.ToString(mainNonXAML.cutLength[i]) + ",    ";
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

        public void ProcessOvers()
        {
            mainNonXAML.CreateOriginalOUs(); // Checked

            if (mainNonXAML.overCtr > 0)
            {
                int i = 1;
                do
                {
                    i++;
                    if (i > 1000)
                    {
                        MessageBox.Show("Endless loop encountered.", "Driver.cs");
                    }
                    if (mainNonXAML.oversFinished == false)
                    {
                        mainNonXAML.GetComboItems();
                        if (mainNonXAML.oversFinished == true)
                        {
                            ProcessUnders();
                            return;
                        }
                        mainNonXAML.GetCombos();
                        mainNonXAML.PrioritizeItems();
                        mainNonXAML.GetBestCombo();
                        mainNonXAML.MoveOver();
                        if (mainNonXAML.oversFinished == true)
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
                mainNonXAML.oversFinished = true;

                ProcessUnders();

                return;
            }
        }

        public void ProcessUnders()
        {
            mainNonXAML.GetNetUnders();
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
                            mainNonXAML.PrioritizeWidths();
                            if (mainNonXAML.undersDone == true)
                            {
                                return;
                            }
                            nextAction = 2;
                            break;
                        case 2:
                            mainNonXAML.FixedItems();
                            nextAction = 3;
                            break;
                        case 3:
                            mainNonXAML.OtherItems();
                            nextAction = 4;
                            break;
                        case 4:
                            mainNonXAML.PossibleItems();
                            nextAction = 5;
                            break;
                        case 5:
                            mainNonXAML.GetCombosU();
                            switch (mainNonXAML.nextAction)
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
                            mainNonXAML.NextFixedItems();
                            nextAction = 4;
                            break;
                        case 7:
                            mainNonXAML.NextFill();
                            if (mainNonXAML.underCtr == 0)
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


        public void FormatWid3Inches()
        {
            double d = 0;
            double r = 0;
            for (int i = 0; i < Globals.inputOWs.Count; i++)
            {
                d = Globals.inputOWs[i];
                r = Math.Truncate(Globals.inputOWs[i]);
                double f = d - r;
                if (Math.Abs(f) > .01)
                {
                    if (f > 0 && f <= .25)
                        Globals.inputOWs[i] = r + .25;
                    else if (f > 0 && f <= .5)
                        Globals.inputOWs[i] = r + .50;
                    else if (f > 0 && f <= .75)
                        Globals.inputOWs[i] = r + .75;
                    else
                        Globals.inputOWs[i] = r + 1.0;
                }
                else
                {
                    Globals.inputOWs[i] = r;
                }
            }

            for (int i = 0; i < Globals.inputUWs.Count; i++)
            {
                d = Globals.inputUWs[i];
                r = Math.Truncate(Globals.inputUWs[i]);
                double f = d - r;
                if (Math.Abs(f) > .01)
                {
                    if (f > 0 && f <= .25)
                        Globals.inputUWs[i] = r + .25;
                    else if (f > 0 && f <= .5)
                        Globals.inputUWs[i] = r + .50;
                    else if (f > 0 && f <= .75)
                        Globals.inputUWs[i] = r + .75;
                    else
                        Globals.inputUWs[i] = r + 1.0;
                }
                else
                {
                    Globals.inputUWs[i] = r;
                }
            }
        }

    }
}
