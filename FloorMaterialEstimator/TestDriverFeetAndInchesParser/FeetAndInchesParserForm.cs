

namespace TestDriverFeetAndInchesParser
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

    public partial class FeetAndInchesParserForm : Form
    {
        public FeetAndInchesParserForm()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            int? feet = null;
            int? inch = null;

            if (Utilities.CheckTextBoxValidMeasurement(this.txbFeetAndInches, out feet, out inch, this.ckbAllowNegatives.Checked))
            {
                this.lblResult.Text = feet.Value.ToString() + "' " + inch.Value + '\'';
            }

            else
            {
                this.lblResult.Text = "Invalid";
            }
        }
    }
}
