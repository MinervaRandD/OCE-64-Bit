using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator.Dialog_Boxes
{
    public partial class ChooseCompleteLSolution : Form
    {
        public bool SelectMinimumArea { get; set; } = false;

        public bool SelectMaximumArea { get; set; } = false;

        public ChooseCompleteLSolution()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedDialog;    
        }

        private void btnSelectMinimumArea_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            SelectMinimumArea = true;
            SelectMaximumArea = false;

            this.Close();
        }

        private void btnSelectMaximumArea_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            SelectMinimumArea = false;
            SelectMaximumArea = true;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            SelectMinimumArea = false;
            SelectMaximumArea = false;

            this.Close();
        }

    }
}
