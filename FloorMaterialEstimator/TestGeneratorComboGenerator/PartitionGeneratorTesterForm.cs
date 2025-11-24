

namespace TestGeneratorComboGenerator
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

    using Utilities;

    public partial class PartitionGeneratorTesterForm : Form
    {
        public PartitionGeneratorTesterForm()
        {
            InitializeComponent();

            this.lsbPartitions.Items.Clear();
        }

        private void btnGeneratePartitions_Click(object sender, EventArgs e)
        {
            string totalElemsStr = this.txbElementCount.Text.Trim();

            if (!Utilities.IsValidPosInt(totalElemsStr))
            {
                MessageBox.Show("Please provide a valid value for total elements.");
                return;
            }

            int totalElems = int.Parse(totalElemsStr);

            PartitionGenerator partitionGenerator = new PartitionGenerator(totalElems);

            List<int[]> partitionList = partitionGenerator.GeneratePartitions();

            this.lsbPartitions.Items.Clear();

            foreach (int[] partition in partitionList)
            {
                string partitionStr = string.Empty;

                for (int i = 0; i < totalElems; i++)
                {
                    partitionStr += (i + 1).ToString() + " (" + partition[i].ToString() + "), ";
                }

                this.lsbPartitions.Items.Add(partitionStr.Substring(0, partitionStr.Length - 2));
            }
        }
    }
}
