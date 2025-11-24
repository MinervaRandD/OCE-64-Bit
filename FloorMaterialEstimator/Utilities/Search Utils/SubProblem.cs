using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class SubProblem
    {
        public double upperBound;
        public double lowerBound;

        public HashSet<int> ProblemSubset;
        
        private double[,] wghtMtrx;

        public SubProblem(IEnumerable<int> initializer, double[,] wghtMtrx)
        {
            ProblemSubset = new HashSet<int>(initializer);

            this.wghtMtrx = wghtMtrx; 
        }

        public SubProblem(int i1, int i2, double[,] wghtMtrx)
        {
            ProblemSubset = new HashSet<int>();

            ProblemSubset.Add(i1);
            ProblemSubset.Add(i2);

            this.wghtMtrx = wghtMtrx;
        }
    }
}
