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
    public class ComboPartitionUnitSolution: IEnumerable<ComboPath>
    {
        public List<ComboPath> ComboPaths = new List<ComboPath>();
        
        public ComboPartitionUnitSolution(PartitionUnitSolution partitionUnitSolution, GraphicsComboElem[] graphicsComboElemList, double[,] wghtMtrx)
        {
            foreach (int[] path in partitionUnitSolution.OptimalPaths)
            {
                ComboPath comboPath = new ComboPath(path, graphicsComboElemList, wghtMtrx);

                ComboPaths.Add(comboPath);
            }
        }

        public int Count => ComboPaths.Count;

        public IEnumerator<ComboPath> GetEnumerator()
        {
            return ComboPaths.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ComboPaths.GetEnumerator();
        }
    }
}
