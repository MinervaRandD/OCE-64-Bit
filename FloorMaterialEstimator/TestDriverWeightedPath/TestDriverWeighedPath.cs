using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace TestDriverWeightedPath
{
    public partial class TestDriverWeighedPath : Form
    {
        public TestDriverWeighedPath()
        {
            InitializeComponent();
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            WeightedPath weightedPath = new WeightedPath(TestCases.TestCase1);

            List<int> elemList = new List<int>();

            for (int i = 0; i < TestCases.TestCase1.GetLength(0); i++)
            {
                elemList.Add(i);
            }

            List<int> maxPath = new List<int>();

            //weightedPath.MaximizePrimalGenAndTest();

            weightedPath.MaximizePrimalAStar();

            double result = weightedPath.maxmWght;

            int[] maxmPath = weightedPath.maxmPath;
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            WeightedPath weightedPath = new WeightedPath(TestCases.TestCase2);

            List<int> elemList = new List<int>();

            for (int i = 0; i < TestCases.TestCase1.GetLength(0); i++)
            {
                elemList.Add(i);
            }

            List<int> maxPath = new List<int>();

            weightedPath.MaximizePrimalGenAndTest();

            double result = weightedPath.maxmWght;

            int[] maxmPath = weightedPath.maxmPath;
        }

        private void btnTest3_Click(object sender, EventArgs e)
        {
            WeightedPath weightedPath = new WeightedPath(TestCases.TestCase3);

            List<int> elemList = new List<int>();

            for (int i = 0; i < TestCases.TestCase1.GetLength(0); i++)
            {
                elemList.Add(i);
            }

            List<int> maxPath = new List<int>();

            weightedPath.MaximizePrimalGenAndTest();

            double result = weightedPath.maxmWght;

            int[] maxmPath = weightedPath.maxmPath;
        }

        private void btnTest4_Click(object sender, EventArgs e)
        {
            WeightedPath weightedPath = new WeightedPath(TestCases.TestCase4);

            List<int> elemList = new List<int>();

            for (int i = 0; i < TestCases.TestCase1.GetLength(0); i++)
            {
                elemList.Add(i);
            }

            List<int> maxPath = new List<int>();

            //weightedPath.MaximizePrimalGenAndTest();

            weightedPath.MaximizePrimalAStar();

            double result = weightedPath.maxmWght;

            int[] maxmPath = weightedPath.maxmPath;
        }

        private void btnTest5_Click(object sender, EventArgs e)
        {
            WeightedPath weightedPath = new WeightedPath(TestCases.TestCase5);

            List<int> elemList = new List<int>();

            for (int i = 0; i < TestCases.TestCase1.GetLength(0); i++)
            {
                elemList.Add(i);
            }

            List<int> maxPath = new List<int>();

            //weightedPath.MaximizePrimalGenAndTest();

            weightedPath.MaximizePrimalAStar();

            double result = weightedPath.maxmWght;

            int[] maxmPath = weightedPath.maxmPath;
        }
    }
}
