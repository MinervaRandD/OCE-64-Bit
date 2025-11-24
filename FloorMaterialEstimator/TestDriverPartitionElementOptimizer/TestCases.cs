using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDriverPartitionElementOptimizer
{
    public static class TestCases
    {
        public static double[,] WghtMtrx1 = new double[6, 6]
        {
            { 0, 30, 2, 3, 17, 16},
            { 12, 0, 9, 15, 6, 30},
            { 2, 14, 0, 25, 31, 8},
            { 27, 15, 29, 0, 10, 32},
            { 28, 18, 23, 25, 0, 22},
            { 1, 1, 18, 5, 23, 0}
        };

        public static double[,] WghtMtrx2 = new double[4, 4]
       {
            { 0, 30, 2, 3},
            { 12, 0, 9, 15},
            { 2, 14, 0, 25},
            { 27, 15, 29, 0}
           
       };
    }
}
