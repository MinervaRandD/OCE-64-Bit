using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SummaryReport.Exporters
{
    using Utilities;

    public partial class SelectSheetNameForm : Form
    {
        public string WorksheetName = string.Empty;

        public SelectSheetNameForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            WorksheetName = string.Empty;

            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            WorksheetName = this.txbWorksheetName.Text.Trim();

            if (!Utilities.ValidWorksheetName(WorksheetName))
            {
                MessageBox.Show("'" + WorksheetName + "' is not a valid excel worksheet name.");

                return;
            }

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        internal void Init(string sheetName)
        {
            WorksheetName = sheetName;

            this.txbWorksheetName.Text = sheetName;
        }
    }
}
