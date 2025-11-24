using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OversUndersLib
{
    class InitializeOUsX
    {
        public static List<double> owList = new List<double>(0);
        public static List<double> olList = new List<double>(0);
        public static List<double> uwList = new List<double>(0);
        public static List<double> ulList = new List<double>(0);


        public static void LoadLists() // Now input from Driver
        {
            owList = new List<double>(Globals.inputOWs);
            olList = new List<double>(Globals.inputOLs);
            uwList = new List<double>(Globals.inputUWs);
            olList = new List<double>(Globals.inputOLs);
           
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
                for (int j = 0; j < Globals.inputOWs.Count; j++)
                {
                    if (Globals.inputOWs[j] == owList[i])
                    {
                         tempLength[i] = tempLength[i] + Globals.inputOLs[j];
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
                for (int j = 0; j < Globals.inputUWs.Count ; j++)
                {
                    if (Globals.inputUWs[j] == uwList[i])
                    {
                        tempLength[i] = tempLength[i] + Globals.inputULs[j];
                    }
                }
            ulList = tempLength;
        }



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
                            if (ulList[i] < Globals.minOULength)
                            {
                                ulList[i] = 0;
                            }
                        }
                        else
                        {
                            olList[j] = olList[j] - ulList[i];
                            ulList[i] = 0;
                            if (olList[j] < Globals.minOULength)
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
