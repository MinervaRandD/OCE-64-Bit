

namespace TestDriverPartitionAllocator
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

    public partial class PartionaAllocatorForm : Form
    {
        public PartionaAllocatorForm()
        {
            InitializeComponent();

            this.lsbAllocations.Items.Clear();
        }

        private void btnGenerateAllocation_Click(object sender, EventArgs e)
        {
            this.lsbAllocations.Items.Clear();

            string partitionStr = this.txbPartitonDefinition.Text.Trim();

            string[] partElemArray = partitionStr.Split(',');

            List<int> partElemList = new List<int>();

            foreach (string partElem in partElemArray)
            {
                int elem = 0;

                if (!int.TryParse(partElem.Trim(), out elem))
                {
                    MessageBox.Show("Please provide a valid partition definition.");
                    return;
                }

                partElemList.Add(elem);
            }

            int[] partition = partElemList.ToArray();

            PartitionAllocator partitionAllocator = new PartitionAllocator(partition);

            List<ulong[]> allocations = partitionAllocator.GenerateAllocations();

            foreach (ulong[] allocation in allocations)
            {
                List<string> elemStrList = new List<string>();

                foreach (ulong elem in allocation)
                {
                    elemStrList.Add(string.Join(", ", BitUtils.BitsetToIntList(elem)));
                }

                this.lsbAllocations.Items.Add(string.Join(" | ", elemStrList));
            }
        }

        
    }
}
