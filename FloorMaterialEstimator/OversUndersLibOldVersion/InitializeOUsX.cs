using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OversUndersLibOldVersion
{
    class InitializeOUsX
    {
        public static List<double> owList = new List<double>(0);
        public static List<double> olList = new List<double>(0);
        public static List<double> uwList = new List<double>(0);
        public static List<double> ulList = new List<double>(0);


        public static void LoadLists() // Now input from Driver
        {
            owList = new List<double>(Driver.inputOWs);
            olList = new List<double>(Driver.inputOLs);
            uwList = new List<double>(Driver.inputUWs);
            olList = new List<double>(Driver.inputOLs);
           
        }

        public static void ConsolidateOULists()
        {
            owList.Sort();
            owList = owList.Distinct().ToList();
             List<double> tempLength = new List<double>();
            for (int i = 0; i < owList.Count; i++)
            {
                tempLength.Add(0);
            }
            for (int i = 0; i < owList.Count; i++)
                for (int j = 0; j < Driver.inputOWs.Count; j++)
                {
                    if (Driver.inputOWs[j] == owList[i])
                    {
                         tempLength[i] = tempLength[i] + Driver.inputOLs[j];
                    }
                }
            olList = tempLength;
            uwList.Sort();
            uwList = uwList.Distinct().ToList();
            tempLength = new List<double>();
            for (int i = 0; i < uwList.Count; i++)
            {
                tempLength.Add(0);
            }
            for (int i = 0; i < uwList.Count; i++)
                for (int j = 0; j < Driver.inputUWs.Count ; j++)
                {
                    if (Driver.inputUWs[j] == uwList[i])
                    {
                        tempLength[i] = tempLength[i] + Driver.inputULs[j];
                    }
                }
            ulList = tempLength;
        }

        //public static void ConsolidateOULists()
        //{
            //    owList.Sort();
            //    owList = owList.Distinct().ToList();
            //    List<double> tempLength = new List<double>();
            //    for (int i = 0; i < owList.Count; i++)
            //    {
            //        tempLength.Add(0);
            //    }
            //    for (int i = 0; i < owList.Count; i++)
            //        for (int j = 0; j < LoadStartingOUsX.oCount; j++)
            //        {
            //            if (LoadStartingOUsX.owRandom[j] == owList[i])
            //            {
            //                tempLength[i] = tempLength[i] + LoadStartingOUsX.olRandom[j];
            //            }
            //        }
            //    olList = tempLength;
            //    uwList.Sort();
            //    uwList = uwList.Distinct().ToList();
            //    tempLength = new List<double>();
            //    for (int i = 0; i < uwList.Count; i++)
            //    {
            //        tempLength.Add(0);
            //    }
            //    for (int i = 0; i < uwList.Count; i++)
            //        for (int j = 0; j < LoadStartingOUsX.uCount; j++)
            //        {
            //            if (LoadStartingOUsX.uwRandom[j] == uwList[i])
            //            {
            //                tempLength[i] = tempLength[i] + LoadStartingOUsX.ulRandom[j];
            //            }
            //        }
            //    ulList = tempLength;
        //}


        public static void ExactOsToUs()
        {
            for (int i = 0; i < uwList.Count; i++)
            {
                for (int j = 0; j < owList.Count; j++)
                {
                    if (uwList[i] == owList[j])
                    {
                        if (ulList[i] >= olList[j])
                        {
                            ulList[i] = ulList[i] - olList[j];
                            olList[j] = 0;
                            if (ulList[i] < SettingsX.minOULength)
                            {
                                ulList[i] = 0;
                            }
                        }
                        else
                        {
                            olList[j] = olList[j] - ulList[i];
                            ulList[i] = 0;
                            if (olList[j] < SettingsX.minOULength)
                            {
                                olList[j] = 0;
                            }
                        }
                    }
                }
            }
        }


    }
}
