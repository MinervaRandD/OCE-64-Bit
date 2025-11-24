using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OversUndersLib
{
    public class PrioritiesX
    {
        public int priorityCounter = 0;
        public int[] priorityIndexPtr = new int[100];
        public Double[] priorityWid = new double[100];
        public Double[] priorityRem = new double[100];
        public double wid = 0;

        public void CreatePriorities()
        {
            double inches = Globals.inch * 3;
            double n = 0;
            List<int> underRemListIndex = new List<int>(0);
            List<double> underRemList = new List<double>(0);
            List<double> underRemWidList = new List<double>(0);
            double hiRem = 0;
            int hiRemIndex = 0;
            double hiRemWid = 0;
            int removeIndex = 0;
            //wid = SettingsX.rollWidth;
            wid = Globals.rollWidth;
            int test1 = 0;
            int test2 = 0;
            int test3 = 0;
            do
            {
                n = n + inches;
                underRemWidList.Add(n);
                underRemList.Add(RemWid(n));
                underRemListIndex.Add(underRemList.Count - 1);
            }
            while (n < wid / 2);
            redo:
            test1 = underRemListIndex.Count;
            test2 = underRemWidList.Count;
            test3 = underRemList.Count;
            hiRem = 0;
            int j = underRemWidList.Count - 1;
            for (int i = j; i >= 0; i--)
            {
                if (underRemList[i] > hiRem)
                {
                    hiRem = underRemList[i];
                    hiRemIndex = underRemListIndex[i]; 
                    hiRemWid = underRemWidList[i];
                    removeIndex = i;
                }
            }
            j = 0;
            if (hiRem > 0)
            {
                priorityCounter++;
                priorityIndexPtr[priorityCounter] = hiRemIndex;
                priorityWid[priorityCounter] = hiRemWid;
                priorityRem[priorityCounter] = hiRem;
                underRemListIndex.RemoveAt(removeIndex);
                underRemWidList.RemoveAt(removeIndex);////////
                underRemList.RemoveAt(removeIndex);
                goto redo;
            }
            j = underRemWidList.Count - 1;
            for (int i = j; i >= 0; i--)
            {
                priorityCounter++;
                priorityIndexPtr[priorityCounter] = underRemListIndex[i];
                priorityWid[priorityCounter] = underRemWidList[i];
                priorityRem[priorityCounter] = hiRem;
            }
            for (int i = 1; i <= priorityCounter; i++)
            {
                priorityIndexPtr[i] = priorityIndexPtr[i] + 1;
            }
        }

        public double RemWid(double d)
        {
            int j = 0;
            double t = 0;
            j = Convert.ToInt32(Math.Truncate(wid / d));
            double remValue = 0;
            if (d > 0)
            {
                for (int i = 0; i < j; i++)
                {
                    t = t + d;
                }
                remValue = wid - t;
            }
            else
            {
                remValue = 0;
            }
            return remValue;
        }
    }
}
