

namespace TestDriverCombosDisplay
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using FloorMaterialEstimator;

    public partial class CombosDisplayForm : Form
    {
        public CombosDisplayForm()
        {
            InitializeComponent();

            //UCPartitionSolution partitionSolution
            //    = new UCPartitionSolution(TestCases.comboPartitionSolution1);

            UCPartitionSolution partitionSolution
                = new UCPartitionSolution(TestCases.comboPartitionSolution2, 1.0);

            this.Controls.Add(partitionSolution);

            partitionSolution.Location = new Point(32, 4);
        }
    }
}
