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
    public partial class SelectOpenWorkbookForm : Form
    {
        public string SelectedWorkbookName = string.Empty;

        public string SelectedWorksheetName = string.Empty;

        public SelectOpenWorkbookForm()
        {
            InitializeComponent();

            this.grbSelectWorksheet.Enabled = false;
        }

        public void Init(List<string> availableWorkbooks)
        {
            this.lsbAvailableWorkbooks.Items.Clear();

            foreach (string workbookName in availableWorkbooks)
            {
                this.lsbAvailableWorkbooks.Items.Add(workbookName);
            }

            lsbAvailableWorkbooks.DoubleClick += LsbAvailableWorkbooks_DoubleClick;

            lsbAvailableWorksheets.DoubleClick += LsbAvailableWorksheets_DoubleClick;

            this.DialogResult = DialogResult.Cancel;

        }

        private void LsbAvailableWorkbooks_DoubleClick(object sender, EventArgs e)
        {
            SelectedWorkbookName = lsbAvailableWorkbooks.SelectedItem.ToString();

            if (string.IsNullOrEmpty(SelectedWorkbookName))
            {
                return;
            }

            populateWorksheets();

            this.grbSelectWorksheet.Enabled = true;
        }

        private void LsbAvailableWorksheets_DoubleClick(object sender, EventArgs e)
        {
            SelectedWorksheetName = lsbAvailableWorksheets.SelectedItem.ToString();

            if (string.IsNullOrEmpty(SelectedWorkbookName))
            {
                return;
            }

            SelectedWorksheetName = lsbAvailableWorksheets.SelectedItem.ToString();

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnSelectWorkbook_Click(object sender, EventArgs e)
        {
            if (lsbAvailableWorkbooks.SelectedIndex < 0)
            {
                MessageBox.Show("No workbook has been selected.");

                return;
            }

            SelectedWorkbookName = lsbAvailableWorkbooks.SelectedItem.ToString();

            populateWorksheets();

            this.grbSelectWorksheet.Enabled = true;
        }

        private void btnSelectWorksheet_Click(object sender, EventArgs e)
        {
            SelectedWorksheetName = this.txbSheetName.Text;

            if (!string.IsNullOrEmpty(SelectedWorksheetName.Trim()))
            {
                if (!Utilities.ExcelInteropUtils.ValidWorksheetName(SelectedWorksheetName))
                {
                    MessageBox.Show("'" + SelectedWorksheetName + "' is not a valid excel worksheet name.");

                    return;
                }
            }

            else
            {
                if (lsbAvailableWorksheets.SelectedIndex < 0)
                {
                    MessageBox.Show("No worksheet has been selected.");

                    return;
                }

                SelectedWorksheetName = lsbAvailableWorksheets.SelectedItem.ToString();
            }

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void populateWorksheets()
        {
            List<string> worksheets = Utilities.ExcelInteropUtils.GetWorksheetNames(SelectedWorkbookName);

            foreach (string worksheetName in worksheets)
            {
                this.lsbAvailableWorksheets.Items.Add(worksheetName);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
