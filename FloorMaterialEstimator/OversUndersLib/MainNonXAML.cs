using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OversUndersLib
{
    public class MainNonXAML
    {
        public int originalOverCtr = 0;
        public double[] originalOW = new double[100];
        public double[] originalOL = new double[100];
        public int originalUnderCtr = 0;
        public double[] originalUW = new double[100];
        public double[] originalUL = new double[100];

        public int currentOverCtr = 0;
        public double[] currentOW = new double[100];
        public double[] currentOL = new double[100];

        public int currentUnderCtr = 0;
        public double[] currentUW = new double[100];
        public double[] currentUL = new double[100];
        public int bestOverIndex = 0;

        public int overCtr = 0;
        public double[] oW = new double[100];
        public double[] oL = new double[100];
        public int underCtr = 0;
        public double[] uW = new double[100];
        public double[] uL = new double[100];

        public int[] uWidthComboItemCtr = new int[2000];
        public double[,] uWidthComboItem = new double[2000, 12];
        public int totalUCombos = 0;
        public int currentOverIndex = 0;
        public double[] uItem = new double[300];
        public int uItemCtr = 0;
        public bool oversFinished = false;
        public double smallestOverGap = 0;
        public double smallestComboGap = 0;
        //public double wid = SettingsX.rollWidth;
        public double wid = 0; // Driver.rollWidth;
        public int staticStartingCutCtr = 0;

        public int cutCtr = 0;

        public int band = 0;
        public int[] cs = new int[30];
        public int[] csMax = new int[30];
        public bool fillCompleted = false;
        public double[] tempUFill = new double[100];
        public int tempUFillCtr = 0;

        public int[] uWFinalCtr = new int[2000];
        public double[] cutLength = new double[100];
        public double[,] uWComboFinalWid = new double[2000, 100]; // Changed. MDD
        public int curPriorityCtr = 0;
        public double[] curPriorityWid = new double[2000];
        public double[] fixedWidItem = new double[100];
        public int fixedWidthComboCtr = 0;
        public int[] fixedComboItemCtr = new int[100];
        public double[,] fixedComboItem = new double[2000, 100]; // Changed. MDD
        public int fixedComboIndex = 0;
        public int bestComboItemCtr = 0;
        public double[] bestComboItem = new double[100]; // Changed. MDD
        public double netUnders;
        public Boolean undersDone = false;
        public string tempStr;
        //public double rollWidth;
        public List<string> tempListA = new List<string>(0);
        public List<string> tempListB = new List<string>(0);
        public List<string> tempListC = new List<string>(0);
        public List<string> tempListD = new List<string>(0);
        public List<string> testOWList = new List<string>(0);
        public List<string> tempListWide1 = new List<string>(0);
        public List<string> tempListWide2 = new List<string>(0);
        public List<string> tempListWide3 = new List<string>(0);
        public string nextAction = "";
        public string tempText = "";
        public string previousString = "nothing";
        public List<double> inputOWs = new List<double>(0);
        public List<double> inputOLs = new List<double>(0);
        public List<double> inputUWs = new List<double>(0);
        public List<double> inputULs = new List<double>(0);

        public List<double> PermanentListDbl = new List<double>(0);
        public bool AutoOversDone { get; set; }

        PrioritiesX prioritiesX;
        FuncsX funcsX;

        public MainNonXAML(
            PrioritiesX prioritiesX
            ,FuncsX funcsX)
        {
            this.prioritiesX = prioritiesX;
            this.funcsX = funcsX;
        }

        public void CreateOriginalOUs()
        {
            InitializeOUsX.LoadLists();
            //Driver.inputOWs[1] = Driver.inputOWs[1];
            InitializeOUsX.ConsolidateOULists();
            originalOverCtr = InitializeOUsX.olList.Count;
            for (int i = 0; i < originalOverCtr; i++)
            {
                originalOW[i + 1] = InitializeOUsX.owList[i];
                originalOL[i + 1] = InitializeOUsX.olList[i];
            }

            originalUnderCtr = InitializeOUsX.ulList.Count;
            for (int i = 0; i < originalUnderCtr; i++)
            {
                originalUW[i + 1] = InitializeOUsX.uwList[i];
                originalUL[i + 1] = InitializeOUsX.ulList[i];
            }

            InitializeOUsX.ExactOsToUs();
            {
                currentOverCtr = InitializeOUsX.olList.Count;
                overCtr = originalOverCtr;
                for (int i = 0; i < currentOverCtr; i++)
                {
                    currentOW[i + 1] = InitializeOUsX.owList[i];
                    currentOL[i + 1] = InitializeOUsX.olList[i];
                    oW[i + 1] = InitializeOUsX.owList[i];
                    oL[i + 1] = InitializeOUsX.olList[i];
                }

                currentUnderCtr = InitializeOUsX.ulList.Count;
                underCtr = currentUnderCtr;
                for (int i = 0; i < currentUnderCtr; i++)
                {
                    currentUW[i + 1] = InitializeOUsX.uwList[i];
                    currentUL[i + 1] = InitializeOUsX.ulList[i];
                    uW[i + 1] = InitializeOUsX.uwList[i];
                    uL[i + 1] = InitializeOUsX.ulList[i];
                }
            }
            RemoveZeroLengths();
        }


        //public void CreateOriginalOUs()
        //{
        //    InitializeOUsX.LoadLists();
        //    InitializeOUsX.ConsolidateOULists(); //originalOverCtr created here
        //    originalOverCtr = InitializeOUsX.olList.Count;
        //    for (int i = 0; i < originalOverCtr; i++)
        //    {
        //        originalOW[i + 1] = InitializeOUsX.owList[i];
        //        originalOL[i + 1] = InitializeOUsX.olList[i];
        //    }

        //    originalUnderCtr = InitializeOUsX.ulList.Count;
        //    for (int i = 0; i < originalUnderCtr; i++)
        //    {
        //        originalUW[i + 1] = InitializeOUsX.uwList[i];
        //        originalUL[i + 1] = InitializeOUsX.ulList[i];
        //    }

        //    InitializeOUsX.ExactOsToUs();
        //    {
        //        currentOverCtr = InitializeOUsX.olList.Count;
        //        overCtr = originalOverCtr;
        //        for (int i = 0; i < currentOverCtr; i++)
        //        {
        //            currentOW[i + 1] = InitializeOUsX.owList[i];
        //            currentOL[i + 1] = InitializeOUsX.olList[i];
        //            oW[i + 1] = InitializeOUsX.owList[i];
        //            oL[i + 1] = InitializeOUsX.olList[i];
        //        }

        //        currentUnderCtr = InitializeOUsX.ulList.Count;
        //        underCtr = currentUnderCtr;
        //        for (int i = 0; i < currentUnderCtr; i++)
        //        {
        //            currentUW[i + 1] = InitializeOUsX.uwList[i];
        //            currentUL[i + 1] = InitializeOUsX.ulList[i];
        //            uW[i + 1] = InitializeOUsX.uwList[i];
        //            uL[i + 1] = InitializeOUsX.ulList[i];
        //        }
        //    }
        //    RemoveZeroLengths();
        //}

        public void GetComboItems()
        {
            Array.Clear(uWidthComboItemCtr, 0, uWidthComboItemCtr.Length);
            Array.Clear(uWidthComboItem, 0, uWidthComboItem.GetLength(0) * uWidthComboItem.GetLength(1));
            totalUCombos = 0;
            if (overCtr == 0)
            // begin added March 2020
            {
                oversFinished = true;
                goto ExitRoutine;
            }
            // end added March 2020
            currentOverIndex = 1;
            // start XAML only
            testOWList.Clear();
            for (int i = 1; i <= overCtr; i++)
            {
                testOWList.Add(funcsX.FormatOU(-1, oW[i], oL[i]));
            }
            // end XAML only
            do
            {
                ProcessOverCombos(currentOverIndex);
                if (uItemCtr == 0)
                {
                    if (currentOverIndex == overCtr)
                    {
                        PrepareUnderFills();
                        goto ExitRoutine;
                    }
                    else
                    {
                        currentOverIndex++;
                    }
                }
            }
            while (uItemCtr == 0);
            testOWList.Clear(); // Testing only
            for (int i = 1; i <= overCtr; i++)
            {
                testOWList.Add(funcsX.FormatOU(-1, oW[i], oL[i]));
            }
            ExitRoutine:;
        }

        public void ProcessOverCombos(int overIndex)
        {
            //if (AutoOversDone == true)
            Array.Clear(uItem, 0, uItem.Length);
            tempListA.Clear();
            uItemCtr = 0;
            for (int i = 1; i <= overCtr; i++)
            {
                if (i == overIndex)
                {
                    if (oL[i] > 0)
                    {
                        for (int j = 1; j <= underCtr; j++)
                        {
                            if (uL[j] > 0 && uW[j] <= oW[i])
                            {
                                int m = Convert.ToInt32(Math.Truncate(oW[i] / uW[j]));
                                for (int k = 1; k <= m; k++)
                                {
                                    uItemCtr++;
                                    uItem[uItemCtr] = uW[j];
                                }
                            }
                        }
                        for (int k = 1; k <= uItemCtr; k++)
                        {
                            tempListA.Add(Convert.ToString(k) + ") " + funcsX.DblToStr(uItem[k]));
                        }
                    }
                }
            }
        }

        public void PrepareUnderFills()
        {
            RemoveZeroLengths();
            oversFinished = true;
        }

        public void RemoveZeroLengths()
        {
            List<double> tempListW = new List<double>(0);
            List<double> tempListL = new List<double>(0);
            int ctr = 0;
            double minL = Globals.minOULength;
            for (int i = 1; i <= overCtr; i++)
            {
                tempListW.Add(oW[i]);
                tempListL.Add(oL[i]);
            }
            Array.Clear(oW, 0, oW.Length);
            Array.Clear(oL, 0, oL.Length);
            int j = tempListW.Count;
            for (int i = 0; i < j; i++)
            {
                if (tempListL[i] >= minL)
                {
                    ctr = ctr + 1;
                    oW[ctr] = tempListW[i];
                    oL[ctr] = tempListL[i];
                }
            }
            overCtr = ctr;

            ctr = 0;
            tempListW.Clear();
            tempListL.Clear();
            for (int i = 1; i <= underCtr; i++)
            {
                tempListW.Add(uW[i]);
                tempListL.Add(uL[i]);
            }
            Array.Clear(uW, 0, uW.Length);
            Array.Clear(uL, 0, uL.Length);
            j = tempListW.Count;
            for (int i = 0; i < j; i++)
            {
                if (tempListL[i] >= minL)
                {
                    ctr = ctr + 1;
                    uW[ctr] = tempListW[i];
                    uL[ctr] = tempListL[i];
                }
            }
            underCtr = ctr;
        }

        public void GetCombos() // zxa
        {
            double overWidth = oW[currentOverIndex];
            double n = 0;
            string s = "";
            int k = 0;
            previousString = "nothing";
            tempListA.Clear();
            tempListB.Clear();
            smallestOverGap = 99999;
            totalUCombos = 0;
            Combo(uItemCtr);
            for (int i = 1; i <= totalUCombos; i++)
            {
                s = ""; n = 0;
                k = uWidthComboItemCtr[i];
                for (int j = 1; j <= k; j++)
                {
                    s = s + funcsX.DblToStr(uWidthComboItem[i, j]) + " - "; ; // XAML only
                    n = n + uWidthComboItem[i, j];
                }
                if (overWidth - n == smallestOverGap) // XAML only ?
                {
                    tempListB.Add(s);
                }
            }
        }

        public void Combo(int max)
        {
           
            int lo = 0;
            int hi = 0;
            int ctr = 0;
            staticStartingCutCtr = cutCtr;
            for (int i = 1; i <= max; i++)
            {
                band = 1;
                cs[1] = i;
                WriteCombo();
                //GoSub CheckIfDone
                if (staticStartingCutCtr != cutCtr)
                {
                    staticStartingCutCtr = cutCtr;
                    fillCompleted = true;
                    goto ExitSub;
                }
                //End of CheckIfDone
            }

            //int redoCount  = 0;

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
                cs[band]--;
                Redo:;
               
                //if (redoCount > 50)
                //{
                //    goto NextBand; // Kludge for quick fix on this algo. MDD 2024-08-05
                //}
                cs[band]++;
                if (cs[band] <= csMax[band])
                {
                    WriteCombo();
                    //GoSub CheckIfDone
                    if (staticStartingCutCtr != cutCtr)
                    {
                        staticStartingCutCtr = cutCtr;
                        fillCompleted = true;
                        goto ExitSub;
                    }


                    //redoCount++; // Kludge for quick fix on this algo. MDD 2024-08-05

                    //End of CheckIfDone
                    goto Redo;
                }
                Redo1:;

                if (cs[lo] > csMax[lo])
                {
                    lo--;
                    if (lo < 1)
                    {
                        goto NextBand;
                    }
                    goto Redo1;
                }
                cs[lo] = cs[lo] + 1;

                //GoSub RestLo
                ctr = 0;
                for (int k = lo + 1; k <= band; k++)
                {
                    ctr++;
                    cs[k] = cs[lo] + ctr;
                }
                lo = band - 1;
                //End of RestLo

                if (cs[band] <= csMax[band])
                {
                    WriteCombo();
                    //GoSub CheckIfDone
                    if (staticStartingCutCtr != cutCtr)
                    {
                        staticStartingCutCtr = cutCtr;
                        fillCompleted = true;
                        goto ExitSub;
                    }
                    //End of CheckIfDone
                }

                //redoCount++; // Kludge for quick fix on this algo. MDD 2024-08-05

                goto Redo;
                NextBand:;
            }
            ExitSub:;
        }

        public void WriteCombo()
        {
            double overWidth = oW[currentOverIndex];
            double n = 0;
            double gap = 0;
            String s = "";
            string s1 = "";
            if (oversFinished == true)
            {
                WriteFillCombo();// used for fills portion omly
                return;
            }
            //if (band > SettingsX.rollWidth / 2)
            if (band > Globals.rollWidth / 2)
            {
                goto ExitSub;
            }
            for (int i = 1; i <= band; i++)
            {
                s1 = s1 + funcsX.DblToStr(uItem[cs[i]]) + ", ";
                n = n + uItem[cs[i]];
            }
            if (n > overWidth)
            {
                return;
            }
            gap = overWidth - n;
            if (gap <= smallestOverGap)
            {
                if (gap < smallestOverGap)
                {
                    smallestOverGap = gap;
                }
                else
                {
                    //////goto ExitSub; //'Not sure why this is needed 
                }
            }
            s = s1 + "= " + funcsX.DblToStr(gap);
            for (int i = 0; i < tempListA.Count; i++)
            {
                if (s == tempListA[i])
                {
                    goto ExitSub;
                }
            }
            tempListA.Add(s1 + "= " + funcsX.DblToStr(gap));
            totalUCombos++;
            previousString = s1;
            uWidthComboItemCtr[totalUCombos] = band;
            for (int i = 1; i <= band; i++)
            {
                uWidthComboItem[totalUCombos, i] = uItem[cs[i]];
            }
            ExitSub:;
        }

        public void WriteFillCombo()
        {
            string s = "";
            string s1 = "";
            double n = 0;
            for (int i = 1; i <= band; i++)
            {
                n = n + tempUFill[cs[i]];
            }
            if (n > wid)
            {
                return;
            }
            n = 0;
            for (int i = 1; i <= band; i++)
            {
                n = n + tempUFill[cs[i]];
            }
            if (wid - n < smallestComboGap)
            {
                smallestComboGap = wid - n;
                bestComboItemCtr = band;
                s = "";
                for (int i = 1; i <= bestComboItemCtr; i++)
                {
                    bestComboItem[i] = tempUFill[cs[i]];
                    s = s + funcsX.DblToStr(bestComboItem[i]) + " - ";
                }
                s1 = funcsX.DblToStr(smallestComboGap);
                tempListWide3.Add(Convert.ToString(fixedComboIndex) + ") " + s + "= " + s1);
                tempListWide2.Clear();
                tempListWide2.Add(s + "= " + s1);
                tempText = "Best Combo Gap = " + s1;
              
                if (smallestComboGap == 0)
                {
                    BuildFill();
                }

            }

        }

        public void PrioritizeItems()
        {
            //Call BestGetOverCombos in VB6
            string s = "";
            tempListC.Clear(); // XAML only
            Array.Clear(uWFinalCtr, 0, uWFinalCtr.Length);
            Array.Clear(uWComboFinalWid, 0, uWComboFinalWid.Length);
            Array.Clear(curPriorityWid, 0, curPriorityWid.Length);
            curPriorityCtr = 0;
            int j = prioritiesX.priorityCounter;
            for (int i = 1; i <= j; i++)
            {
                for (int k = 1; k <= totalUCombos; k++)
                {
                    int n = uWidthComboItemCtr[k];
                    for (int m = 1; m <= n; m++)
                    {
                        if (uWidthComboItem[k, m] == prioritiesX.priorityWid[i])
                        {
                            curPriorityCtr++;
                            curPriorityWid[curPriorityCtr] = prioritiesX.priorityWid[i];
                            s = funcsX.DblToStr(prioritiesX.priorityWid[i]); // XAML only
                            tempListC.Add(s); // XAML only
                            goto NextWidth;
                        }
                    }
                }
                NextWidth:;
            }
        }

        public void GetBestCombo()
        {
            double largestn = 0;
            double n = 0;
            double u = 0;
            double cpw = 0;
            double overWidth = oW[currentOverIndex];
            string s = "";
            tempListA.Clear();
            for (int i = 1; i <= totalUCombos; i++)
            {
                n = 0;
                int k = uWidthComboItemCtr[i];
                for (int j = 1; j <= k; j++)
                {
                    u = uWidthComboItem[i, j];
                    n = n + uWidthComboItem[i, j];
                }
                if (overWidth - n != smallestOverGap)
                {
                    goto NextItem;
                }
                n = 0;
                s = "";
                for (int j = 1; j <= uWidthComboItemCtr[i]; j++)
                {
                    double d = uWidthComboItem[i, j];
                    s = s + funcsX.DblToStr(d) + ", "; //needed fortempListA
                    for (int m = 1; m <= curPriorityCtr; m++)
                    {
                        cpw = curPriorityWid[m];
                        if (uWidthComboItem[i, j] == curPriorityWid[m])
                        {
                            n = n + 1 / Math.Pow(10, m);
                        }
                    }
                    if (n > largestn)
                    {
                        largestn = n;
                        bestOverIndex = i;
                        tempStr = "Best: " + s + " = " + Convert.ToString(n); 
                    }
                }
                NextItem:;
            }
            tempListA.Add(tempStr); //XAML only
        }

        public void MoveOver()
        {
            string s1 = ""; //XAML only
            string s2 = ""; //XAML only
            tempListA.Clear(); //XAML only
            tempListB.Clear(); //XAML only
            int[] widCtr = new int[100];
            double shortLength = 0;
            for (int i = 1; i <= underCtr; i++)
            {
                int k = uWidthComboItemCtr[bestOverIndex];
                for (int j = 1; j <= k; j++)
                {
                    if (uW[i] == uWidthComboItem[bestOverIndex, j])
                    {
                        widCtr[i]++;
                    }
                }
            }
            shortLength = oL[currentOverIndex];
            for (int i = 1; i <= underCtr; i++)
            {
                if (widCtr[i] > 0)
                {
                    if (uL[i] / widCtr[i] < shortLength)
                    {
                        shortLength = uL[i] / widCtr[i];
                    }
                }
            }
            oL[currentOverIndex] = oL[currentOverIndex] - shortLength;
            for (int i = 1; i <= currentOverCtr; i++)
            {
                if (oW[currentOverIndex] == currentOW[i])
                {
                    currentOL[i] = oL[currentOverIndex];
                }
            }

            for (int i = 1; i <= underCtr; i++)
            {
                for (int j = 0; j <= currentUnderCtr; j++)
                {
                    if (uW[i] == currentUW[j] && widCtr[i] > 0)
                    {
                        s2 = funcsX.DblToStr(uW[i]) + " - " + Convert.ToString(widCtr[i]);
                        tempListA.Add(s2); //XAML only
                        if (widCtr[i] > 0)
                        {
                            uL[i] = uL[i] - shortLength * widCtr[i];
                            s1 = funcsX.DblToStr(uL[i]); //XAML only
                            if (uL[i] < 0.1) //XAML only
                            {
                                s1 = "0.00"; //XAML only
                            }
                            tempListB.Add(funcsX.DblToStr(uW[i]) + " x " + s1);  //XAML only
                        }
                    }
                }
            }


            for (int i = 1; i <= underCtr; i++)
            {
                for (int j = 0; j <= currentUnderCtr; j++)
                {
                    if (uW[i] == currentUW[j])
                    {
                        currentUL[j] = uL[i];
                    }
                }
            }
        }

        public void LoadOverUnderLists()
        {
            RemoveZeroLengths();
            tempListC.Clear();
            tempListD.Clear();
            for (int i = 1; i <= currentOverCtr; i++)
            {
                tempListC.Add(funcsX.FormatOU(i, currentOW[i], currentOL[i]));
            }
            for (int i = 1; i <= currentUnderCtr; i++)
            {
                tempListD.Add(funcsX.FormatOU(i, currentUW[i], currentUL[i]));
            }
        }

        public void PrioritizeWidths()
        {
            tempListA.Clear(); // XAML only
            string s = ""; // XAML only
            string s1 = ""; // XAML only
            double n = 0;
            int ctr = 0;
            int i = 0;
            // fixedWidItem doesn't have clear statement in VB6
            do
            {
                i++;
                for (int j = 1; j <= underCtr; j++)
                {
                    if (uW[j] == prioritiesX.priorityWid[i])
                    {
                        ctr++;
                        fixedWidItem[ctr] = uW[j];
                    }

                }
            }
            while (i < prioritiesX.priorityCounter);
            fixedWidthComboCtr = ctr;
            for (int j = 1; j <= fixedWidthComboCtr; j++)
            {
                s = funcsX.DblToStr(fixedWidItem[j]); // XAML only
                s1 = funcsX.DblToStr(funcsX.FRem(fixedWidItem[j])); // XAML only
                tempListA.Add(Convert.ToString(j) + ") " + s + " - " + s1); // XAML only
                n = n + funcsX.FRem(fixedWidItem[j]);
            }
            if (n > 0)
            {
                goto ExitSub;
            }

            cutCtr++;
            n = 0;
            for (int j = 1; j <= underCtr; j++)
            {
                n = n + uW[j] * uL[j];
            }
            cutLength[cutCtr] = n / 12;
            for (int j = 1; j <= underCtr; j++)
            {
                uL[j] = 0;
            }
            for (int j = 1; j <= underCtr; j++)
            {
                for (int k = 0; k < currentUnderCtr; k++)
                {
                    if (uW[j] == currentUW[k])
                    {
                        s1 = funcsX.DblToStr(uL[j]); // XAML only
                        if (uL[j] < .1) // XAML only
                        {
                            s1 = "0"; // XAML only
                        }
                    }
                }
            }
            undersDone = true;

            ExitSub:;

        }

        public void FixedItems()
        {
            fixedComboIndex = 1;
            GetFixedWidCombos();
            if (fillCompleted == true)
            {
                fillCompleted = false;
            }
            else
            {
                smallestComboGap = 99999;
            }
        }

        public void GetFixedWidCombos()
        {
            tempListWide1.Clear();
            double n = 0;
            string s = ""; // XAML only
            string s1 = ""; // XAML only
            for (int i = 1; i <= fixedWidthComboCtr; i++)
            {

                fixedComboItemCtr[i] = 1;
                fixedComboItem[i, 1] = fixedWidItem[1];
                s = funcsX.DblToStr(fixedWidItem[1]); // XAML only
                n = fixedWidItem[1];
                do
                {
                    n = n + fixedWidItem[i];
                    if (n == wid)
                    {
                        fixedComboItemCtr[i]++;
                        fixedComboItem[i, fixedComboItemCtr[i]] = fixedWidItem[i];
                        bestComboItemCtr = fixedComboItemCtr[i];
                        s1 = "";
                        for (int j = 1; j <= bestComboItemCtr; j++)
                        {
                            bestComboItem[j] = fixedComboItem[i, j];
                            s1 = s1 + funcsX.DblToStr(bestComboItem[j]) + " - "; // XAML only
                        }
                        tempListWide1.Add(s1 + " = " + Convert.ToString(wid) + "zxc"); // XAML only
                        goto ExitDo;
                    }
                    if (n < wid)
                    {
                        s = s + " - " + funcsX.DblToStr(fixedWidItem[i]); // XAML only
                        fixedComboItemCtr[i]++;
                        fixedComboItem[i, fixedComboItemCtr[i]] = fixedWidItem[i];
                    }
                    else
                    {
                        tempListWide1.Add(Convert.ToString(i) + ") " + s); // XAML only
                        goto ExitDo;
                    }
                }
                while (n < 1000);
                if (fillCompleted == true)
                {
                    goto ExitFor;
                }
                ExitDo:;
            }
            ExitFor:;
            if (fillCompleted == false)
            {
                //see VB6 code
            }
        }

        public void BuildFill()
        {
            tempListB.Clear();
            tempListC.Clear();
            string s1 = "";
 
            int[] widCtr = new int[30];
            double shortLength = 0;
            for (int i = 1; i <= underCtr; i++)
            {
                for (int j = 1; j <= bestComboItemCtr; j++)
                {
                    if (bestComboItem[j] == uW[i])
                    {
                        widCtr[i]++;
                    }
                }

            }
            shortLength = 99999;
            for (int i = 1; i <= underCtr; i++)
            {
                if (widCtr[i] > 0)
                {
                    if (uL[i] / widCtr[i] < shortLength)
                    {
                        shortLength = uL[i] / widCtr[i];
                    }
                }
            }
            cutCtr++;
            uL[2] = uL[2];
            for (int j = 1; j <= underCtr; j++)
            {
                for (int k = 1; k <= currentUnderCtr; k++)
                {
                    if (uW[j] == currentUW[k] && widCtr[j] > 0)
                    {
                        tempListC.Add(funcsX.DblToStr(uW[j]) + " - " + Convert.ToString(widCtr[j]));
                        if (widCtr[j] > 0)
                        {
                            double testul = uL[j];
                            uL[j] = uL[j] - shortLength * widCtr[j];
                            testul = uL[j];
                        }
                        s1 = Convert.ToString(uL[j]);
                        if (uL[j] < .1)
                        {
                            s1 = "0";
                        }
                        tempListB.Add(funcsX.DblToStr(uW[j]) + " - " + s1);
                    }
                }
            }
            cutLength[cutCtr] = shortLength;
        }

        public void OtherItems()
        {
            BuildComboListUWs();
        }

        public void BuildComboListUWs()
        {
            Array.Clear(uItem, 0, uItem.Length);
            tempListA.Clear();
            uItemCtr = 0;
            for (int i = 1; i <= underCtr; i++)
            {
                int k = Convert.ToInt32(Math.Truncate(wid / uW[i]));
                for (int j = 1; j <= k; j++)
                {
                    uItemCtr++;
                    tempListA.Add(Convert.ToString(uW[i]));
                    uItem[uItemCtr] = uW[i];
                }
            }
        }

        public void PossibleItems()
        {
            BuidFillCombos();
            FilterComboList();
        }

        public void BuidFillCombos()
        {
            int k = 0;
            int m = 0;
            double n = 0;
            PermanentListDbl.Clear();
            for (int i = 1; i <= uItemCtr; i++)
            {
                PermanentListDbl.Add(uItem[i]);
            }
            k = fixedComboItemCtr[fixedComboIndex];
            for (int i = 1; i <= k; i++)
            {
                m = PermanentListDbl.Count - 1;
                for (int j = 0; j < m; j++)
                {
                    if (PermanentListDbl[j] == fixedComboItem[fixedComboIndex, i])
                    {
                        PermanentListDbl.RemoveAt(j); //"Doesn't remove last item in list but still seems to work ok"
                        goto ExitFor;
                    }
                }
                ExitFor:;
            }
            k = fixedComboItemCtr[fixedComboIndex];
            for (int i = 1; i <= k; i++)
            {
                n = n + fixedComboItem[fixedComboIndex, i];
            }
        }

        public void FilterComboList()
        {
            tempListA.Clear();
            double RemWid = 0;
            double n = 0;
            int ctr = 0;
            int j = fixedComboItemCtr[fixedComboIndex];
            for (int i = 1; i <= j; i++)
            {
                n = n + fixedComboItem[fixedComboIndex, i];
            }
            RemWid = wid - n;
            for (int i = 1; i <= uItemCtr; i++)
            {
                if (uItem[i] == uItem[i - 1])
                {
                    n = n + uItem[i];
                    if (n <= RemWid)
                    {
                        ctr++;
                        tempUFill[ctr] = uItem[i];
                        tempListA.Add(Convert.ToString(ctr) + " - " + funcsX.DblToStr(tempUFill[ctr])); // XAML only
                    }
                }
                else
                {
                    if (uItem[i] <= RemWid)
                    {
                        ctr++;
                        tempUFill[ctr] = uItem[i];
                        tempListA.Add(Convert.ToString(ctr) + " - " + funcsX.DblToStr(tempUFill[ctr])); // XAML only
                        n = uItem[i];
                    }
                    else
                    {
                        n = 0;
                    }
                }
            }
            j = fixedComboItemCtr[fixedComboIndex];
            for (int i = 1; i <= j; i++)
            {
                ctr = ctr + 1;
                tempUFill[ctr] = fixedComboItem[fixedComboIndex, i];
                tempListA.Add(Convert.ToString(ctr) + " - " + funcsX.DblToStr(tempUFill[ctr])); // XAML only
            }
            tempUFillCtr = ctr;
        }

        public void GetCombosU()
        {
            // ProcessFixedCombos VB6
            Combo(tempUFillCtr);
            if (smallestComboGap == 0)
            {
                fillCompleted = true;
                tempText = "Smallest gap = 0"; // XAML only
            }
            else
            {
                if (smallestComboGap < wid)
                {
                    nextAction = "Other Items"; // XAML only
                }
            }
            // End of ProcessFixedCombos VB6

            if (fillCompleted == true)
            {
                fillCompleted = false;
                nextAction = "Next Fill"; // XAML only
                return;
            }

            if (smallestComboGap > 0)
            {
                if (fixedComboIndex < fixedWidthComboCtr)
                {
                    fixedComboIndex++;
                }
                else
                {
                    BuildFill();
                    //ListCutsCtr++;
                    nextAction = "Next Fill"; // XAML only
                    return;
                }
            }
            nextAction = "Next Fixed Items";
        }

        public void NextFixedItems()
        {
            BuildComboListUWs(); //May not be necessary per VB6 code - test to verify;
            BuidFillCombos();

        }

        public void NextFill()
        {
            RemoveZeroLengths();
            tempListA.Clear(); //XAML only
            tempListWide3.Clear(); //XAML only
            for (int i = 1; i <= underCtr; i++) //Why is i=1 instead of i=0? qwerty //XAML only
            {
                tempListA.Add(funcsX.FormatOU(i, uW[i], uL[i])); //XAML only
            }
        }
        public void GetNetUnders()
        {
            netUnders = 0;
            for (int i = 1; i <= underCtr; i++)
            {
                netUnders = netUnders + uW[i] * uL[i];//Why is i=1 instead of i=0? qwerty
            }
        }

    }
}

