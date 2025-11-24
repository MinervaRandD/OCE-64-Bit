using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDriverCombosSelectionGenerator
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

        public static double[,] WghtMtrx2 = new double[8, 8]
        {
           { 0, 30, 2, 3, 17, 16, 2, 13},
            { 12, 0, 9, 15, 6, 30, 22, 12},
            { 2, 14, 0, 25, 31, 8, 19, 16},
            { 27, 15, 29, 0, 10, 32, 29, 19},
            { 28, 18, 23, 25, 0, 22, 13, 27},
            { 1, 1, 18, 5, 23, 0, 1, 12},
            { 29, 8, 23, 21, 3, 29, 0, 25},
            { 16, 32, 32, 12, 16, 23, 9, 0}
        };
    }
}
