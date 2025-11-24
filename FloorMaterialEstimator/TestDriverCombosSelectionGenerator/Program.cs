

namespace TestDriverCombosSelectionGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Utilities;

    class Program
    {
        static void Main(string[] args)
        {
            CombosSelectionGenerator csg = new CombosSelectionGenerator(TestCases.WghtMtrx2);

            List<PartitionSolution> solutionList = csg.GenerateSelections();
            
        }
    }
}
