

namespace TestDriverComboGenerator
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

    public partial class ComboGeneratorTestForm : Form
    {
        public ComboGeneratorTestForm()
        {
            InitializeComponent();

            this.lsbCombinations.Items.Clear();
        }
    
        private void btnGenerateCombinations_Click(object sender, EventArgs e)
        {
            string elemMaskStr = this.txbElementCount.Text.Trim();

            ulong elementSet = 0;

            int totalElems = elemMaskStr.Length;

            for (int i = 0; i < totalElems; i++)
            {
                if (elemMaskStr[i] == '1')
                {
                    elementSet |= BitUtils.encodedBits[i];
                }

                else if (elemMaskStr[i] != '0')
                {
                    MessageBox.Show("Please provide a valid value for element mask.");
                    return;
                }
            }

            string subsetSizeStr = this.txbSubsetSize.Text.Trim();

            if (!Utilities.IsValidPosInt(subsetSizeStr))
            {
                MessageBox.Show("Please provide a valid value for subset size.");
                return;
            }

            int subsetSize = int.Parse(subsetSizeStr);

            ComboGenerator comboGenerator = new ComboGenerator(elementSet, totalElems);

            List<ulong> comboList = comboGenerator.GenerateCombos(subsetSize);

            this.lsbCombinations.Items.Clear();

            foreach (ulong combo in comboList)
            {
                string comboStr = string.Empty;

                for (int i = 0; i < totalElems; i++)
                {
                    if (BitUtils.Contains(combo, i))
                    {
                        comboStr += (i + 1).ToString() + ", ";
                    }
                }

                this.lsbCombinations.Items.Add(comboStr.Substring(0, comboStr.Length - 2));
            }
        }
    }
}
