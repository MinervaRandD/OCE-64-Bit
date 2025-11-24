using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class PartitionGenerator
    {
        private int maxmElem;

        public PartitionGenerator(int maxmElem)
        {
            this.maxmElem = maxmElem;
        }

        public List<int[]> GeneratePartitions()
        {
            return GeneratePartitions(maxmElem, maxmElem);
        }

        private List<int[]> GeneratePartitions(int level, int remainingElements)
        {
            if (remainingElements == 0 || level == 0 || level == 1)
            {
                
                int[] partition = new int[maxmElem];

                for (int i = 0; i < maxmElem; i++)
                {
                    partition[i] = 0;
                }

                partition[0] = remainingElements;

                return new List<int[]>() { partition };
            }

            if (remainingElements == 1)
            {
                int[] partition = new int[maxmElem];

                for (int i = 0; i < maxmElem; i++)
                {
                    partition[i] = 0;
                }

                partition[0] = 1;

                return new List<int[]>() { partition };
            }

            List<int[]> returnList = new List<int[]>();

            for (int levelCount = 0; levelCount * level <= remainingElements; levelCount++)
            {
                List<int[]> subList = GeneratePartitions(level - 1, remainingElements - levelCount * level);

                foreach (int[] partition in subList)
                {
                    partition[level - 1] = levelCount;
                }

                returnList.AddRange(subList);
            }

            return returnList;
        }
    }
}
