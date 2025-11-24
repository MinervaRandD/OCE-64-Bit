using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasLib.Legend
{
    public partial class LegendNotesForm : Form
    {
        public string Notes { get; private set; }

        public LegendNotesForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Notes = this.txbNotes.Text.Trim();

            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
