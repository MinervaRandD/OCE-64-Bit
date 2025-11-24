

namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PartitionAllocator
    {
        private int[] partition;

        private int maxmElem;

        private int totlElem;

        private int[] partitionCount;

        public PartitionAllocator(int[] partition)
        {
            this.partition = partition;

            maxmElem = partition.Length;
        }

        public List<ulong[]> GenerateAllocations()
        {
            partitionCount = new int[maxmElem];

            for (int i = 0; i < maxmElem; i++)
            {
                partitionCount[i] = (i + 1) * partition[i];
            }

            totlElem = partitionCount.Sum();

            ulong remainingElements = 0;

            for (int i = 0; i < totlElem; i++)
            {
                remainingElements |= BitUtils.encodedBits[i];
            }

            List<ulong[]> allocationList = generateAllocations(0, remainingElements);

            return allocationList;
        }

        private List<ulong[]> generateAllocations(int partIndx, ulong subset)
        {
            List<ulong[]> returnList = new List<ulong[]>();

            if (partIndx >= maxmElem)
            {
                return returnList;
            }

            int subsetSize = partitionCount[partIndx];

            ComboGenerator comboGenerator = new ComboGenerator(subset, totlElem);

            List<ulong> combosPerLevel = comboGenerator.GenerateCombos(partitionCount[partIndx]);

            if (partIndx == maxmElem - 1)
            {
                if (combosPerLevel.Count == 0)
                {
                    ulong[] elem = new ulong[maxmElem];

                    returnList.Add(elem);
                }

                else
                {
                    foreach (ulong combo in combosPerLevel)
                    {
                        ulong[] elem = new ulong[maxmElem];

                        elem[partIndx] = combo;

                        returnList.Add(elem);
                    }
                }

                return returnList;
            }

            else
            {
                if (combosPerLevel.Count == 0)
                {
                    List<ulong[]> subList = generateAllocations(partIndx + 1, subset);

                    returnList.AddRange(subList);
                }

                else
                {
                    foreach (ulong combo in combosPerLevel)
                    {
                        List<ulong[]> subList = generateAllocations(partIndx + 1, subset & ~combo);

                        foreach (ulong[] subElem in subList)
                        {
                            subElem[partIndx] = combo;

                            returnList.Add(subElem);
                        }
                    }
                }

                return returnList;
            }
        }
        
    }
}
