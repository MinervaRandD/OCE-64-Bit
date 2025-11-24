
namespace TestDriverOptimizationSetGenerator
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
            OptimizationSetGenerator osg = new OptimizationSetGenerator(8, TestCases.WghtMtrx2);

            var x = osg.GenerateSolutionSet();
        }
    }
}
