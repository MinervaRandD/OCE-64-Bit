namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ComboGenerator
    {
        private ulong elementSet;

        private int nmbrElems;

        private int subsetSize;

        public ComboGenerator(ulong elementSet, int nmbrElems)
        {
            this.elementSet = elementSet;

            this.nmbrElems = nmbrElems;
        }

        public List<ulong> GenerateCombos(int subsetSize)
        {
            if (subsetSize <= 0 || subsetSize > nmbrElems)
            {
                return new List<ulong>();
            }

            this.subsetSize = subsetSize;

            List<ulong> returnList = generateCombos(0, 0);

            return returnList;
        }

        private List<ulong> generateCombos(int elemIndx, int count)
        {
            List<ulong> returnList = new List<ulong>();

            if (count == subsetSize - 1)
            {
                for (int i = elemIndx; i < nmbrElems; i++)
                {
                    if (BitUtils.Contains(elementSet, i))
                    {
                        returnList.Add(BitUtils.encodedBits[i]);
                    }
                }

                return returnList;
            }

            for (int i = elemIndx; i < nmbrElems; i++)
            {
                if (BitUtils.Contains(elementSet, i))
                {
                    List<ulong> subList = generateCombos(i + 1, count + 1);

                    foreach (ulong elem in subList)
                    {
                        returnList.Add(elem | BitUtils.encodedBits[i]);
                    }
                }
            }

            return returnList;
        }
    }
}
