using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComboLib;
using MaterialsLayout;
using Utilities;

namespace FloorMaterialEstimator
{
    public class ComboPartitionSolution: IEnumerable<ComboPartitionUnitSolution>
    {
        public int[] Partition;

        public List<ComboPartitionUnitSolution> PartitionUnitSolutions = new List<ComboPartitionUnitSolution>();
       
        public ComboPartitionSolution(PartitionSolution partitionSolution, GraphicsComboElem[] graphicsComboElemList, double[,] wghtMtrx)
        {
            foreach (PartitionUnitSolution partitionUnitSolution in partitionSolution.OptimalSolution)
            {
                ComboPartitionUnitSolution comboPartitionUnitSolution = new ComboPartitionUnitSolution(partitionUnitSolution, graphicsComboElemList, wghtMtrx);

                PartitionUnitSolutions.Add(comboPartitionUnitSolution);
            }

            Partition = partitionSolution.Partition;
        }

        internal string partitionDefinitionText()
        {
            if (Partition == null)
            {
                return string.Empty;
            }

            if (Partition.Length <= 0)
            {
                return string.Empty;
            }

            List<string> labelList = new List<string>();

            for (int i = Partition.Length; i > 0; i--)
            {
                int count = Partition[i - 1];

                if (count == 0)
                {
                    continue;
                }

                string lbl = i.ToString();

                for (int j = 0; j < count; j++)
                {
                    labelList.Add(lbl);
                }
            }

            return string.Join(",", labelList);
        }

        public int Count => PartitionUnitSolutions.Count;

        public ComboPartitionUnitSolution this[int i]
        {
            get
            {
                return PartitionUnitSolutions[i];
            }
        }
        public IEnumerator<ComboPartitionUnitSolution> GetEnumerator()
        {
            return PartitionUnitSolutions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return PartitionUnitSolutions.GetEnumerator();
        }
    }
}
