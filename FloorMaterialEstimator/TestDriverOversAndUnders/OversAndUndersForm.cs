

namespace TestDriverOversAndUnders
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

    using OversUnders;

    public partial class OversAndUndersForm : Form
    {
        public OversAndUndersForm()
        {
            InitializeComponent();

            this.dgvOversPrev.Columns.Add("OversWidth", "Width");
            this.dgvOversPrev.Columns.Add("OversLngth", "Length");

            this.dgvOversPrev.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvOversPrev.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvOversPrev.Columns[0].Width = 100;
            this.dgvOversPrev.Columns[1].Width = this.dgvOversPrev.Width - this.dgvOversPrev.Columns[0].Width - 2;

            this.dgvOversPrev.RowHeadersVisible = false;

            this.dgvUndrsPrev.Columns.Add("OversWidth", "Width");
            this.dgvUndrsPrev.Columns.Add("OversLngth", "Length");

            this.dgvUndrsPrev.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvUndrsPrev.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvUndrsPrev.Columns[0].Width = 100;
            this.dgvUndrsPrev.Columns[1].Width = this.dgvUndrsPrev.Width - this.dgvUndrsPrev.Columns[0].Width - 2;

            this.dgvUndrsPrev.RowHeadersVisible = false;
            
            this.dgvOversCurr.Columns.Add("OversWidth", "Width");
            this.dgvOversCurr.Columns.Add("OversLngth", "Length");

            this.dgvOversCurr.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvOversCurr.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvOversCurr.Columns[0].Width = 100;
            this.dgvOversCurr.Columns[1].Width = this.dgvOversCurr.Width - this.dgvOversCurr.Columns[0].Width - 2;

            this.dgvOversCurr.RowHeadersVisible = false;

            this.dgvUndrsCurr.Columns.Add("OversWidth", "Width");
            this.dgvUndrsCurr.Columns.Add("OversLngth", "Length");

            this.dgvUndrsCurr.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvUndrsCurr.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvUndrsCurr.Columns[0].Width = 100;
            this.dgvUndrsCurr.Columns[1].Width = this.dgvUndrsCurr.Width - this.dgvUndrsCurr.Columns[0].Width - 2;

            this.dgvUndrsCurr.RowHeadersVisible = false;


            this.dgvPrioritiesbyWidth.Columns.Add("Width", "Width");
            this.dgvPrioritiesbyWidth.Columns.Add("Priority", "Priority");

            this.dgvPrioritiesbyWidth.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvPrioritiesbyWidth.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvPrioritiesbyWidth.Columns[0].Width = 100;
            this.dgvPrioritiesbyWidth.Columns[1].Width = this.dgvPrioritiesbyWidth.Width - this.dgvPrioritiesbyWidth.Columns[0].Width - 2;

            this.dgvPrioritiesbyWidth.RowHeadersVisible = false;

            this.dgvBestCombos.Columns.Add("Combo", "Combo");
            this.dgvBestCombos.Columns.Add("Priority", "Priority");

            this.dgvBestCombos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dgvBestCombos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvBestCombos.Columns[0].Width = this.dgvBestCombos.Width - 120 - 2;
            this.dgvBestCombos.Columns[1].Width = 120 - 2;

            this.dgvBestCombos.RowHeadersVisible = false;

            this.lbxUnders.Items.Clear();
            this.lbxFillsToUndersUnders.Items.Clear();
        }

        private OversUnders oversUnders;
     
        private void btnTest1_Click(object sender, EventArgs e)
        {
            oversUnders = new OversUnders(TestCases.Overs1, TestCases.Undrs1);

            testSetup();
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            oversUnders = new OversUnders(TestCases.Overs2, TestCases.Undrs2);

            testSetup();
        }

        private void btnTest3_Click(object sender, EventArgs e)
        {
            oversUnders = new OversUnders(TestCases.Overs3, TestCases.Undrs3);

            testSetup();
        }

        private void testSetup()
        {
            oversUnders.InitializeGeneration(12 * 12);

            this.dgvOversCurr.Rows.Clear();

            foreach (MaterialArea m in oversUnders.CurrOversbyWidthDict.Values)
            {
                string width = ((double)m.WidthInInches / 12.0).ToString("#,##0.00");
                string lngth = ((double)m.LngthInInches / 12.0).ToString("#,##0.00");

                object[] row = new object[2] { width + " ", lngth + " " };

                this.dgvOversCurr.Rows.Add(row);
            }

            this.dgvOversCurr.CurrentCell = null;

            this.dgvUndrsCurr.Rows.Clear();

            foreach (MaterialArea m in oversUnders.CurrUndrsByWidthDict.Values)
            {
                string width = ((double)m.WidthInInches / 12.0).ToString("#,##0.00");
                string lngth = ((double)m.LngthInInches / 12.0).ToString("#,##0.00");

                object[] row = new object[2] { width + " ", lngth + " " };

                this.dgvUndrsCurr.Rows.Add(row);
            }

            this.dgvUndrsCurr.CurrentCell = null;

            this.dgvPrioritiesbyWidth.Rows.Clear();

            foreach (KeyValuePair<int, int> kvp in oversUnders.widthPrioDict)
            {
                object[] row = new object[2] { (kvp.Key / 12.0).ToString("#,##0.000"), kvp.Value.ToString() };

                this.dgvPrioritiesbyWidth.Rows.Add(row);
            }

            this.dgvPrioritiesbyWidth.CurrentCell = null;

            this.dgvBestCombos.Rows.Clear();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            setupPrevState();

            bool result = oversUnders.DoOversToUndersCycle();

            this.lblResult.Text = result.ToString();

            setupBestCombos();

            setupOversToUndrsCurrState();
        }

        private void btnStepFillsToUnders_Click(object sender, EventArgs e)
        {
            setupPrevState();

            int result = oversUnders.DoFillCutsToUndrsCycle(12 * 12);


            setupBestCombos();

            setupFillsToUndrsCurrState(result);
        }

        private void setupOversToUndrsCurrState()
        {

            MaterialArea materialArea = oversUnders.OversToUndersUsedOver;

            this.lbxUnders.Items.Clear();

            foreach (MaterialArea area in oversUnders.OversToUndersAppliedUnders)
            {
                this.lbxUnders.Items.Add(
                    "   " + ((double)area.WidthInInches / 12.0).ToString("#,##0.00") + " x " + ((double)area.LngthInInches / 12.0).ToString("#,##0.00")
                    );
            }

            if (materialArea != null)
            {
                this.lblUsedOver.Text = ((double)materialArea.WidthInInches / 12.0).ToString("#,##0.00") + " x " + ((double)materialArea.LngthInInches / 12.0).ToString("#,##0.00");
            }

            else
            {
                this.lblUsedOver.Text = string.Empty;
            }

            this.dgvOversCurr.Rows.Clear();

            foreach (MaterialArea m in oversUnders.CurrOversbyWidthDict.Values)
            {
                string width = ((double)m.WidthInInches / 12.0).ToString("#,##0.00");
                string lngth = ((double)m.LngthInInches / 12.0).ToString("#,##0.00");

                object[] row = new object[2] { width + " ", lngth + " " };

                this.dgvOversCurr.Rows.Add(row);
            }

            this.dgvOversCurr.CurrentCell = null;

            this.dgvUndrsCurr.Rows.Clear();

            foreach (MaterialArea m in oversUnders.CurrUndrsByWidthDict.Values)
            {
                string width = ((double)m.WidthInInches / 12.0).ToString("#,##0.00");
                string lngth = ((double)m.LngthInInches / 12.0).ToString("#,##0.00");

                object[] row = new object[2] { width + " ", lngth + " " };

                this.dgvUndrsCurr.Rows.Add(row);
            }

            this.dgvUndrsCurr.CurrentCell = null;

            this.dgvPrioritiesbyWidth.Rows.Clear();

            foreach (KeyValuePair<int, int> kvp in oversUnders.widthPrioDict)
            {
                object[] row = new object[2] { (kvp.Key / 12.0).ToString("#,##0.000"), kvp.Value.ToString() };

                this.dgvPrioritiesbyWidth.Rows.Add(row);
            }

            this.dgvPrioritiesbyWidth.CurrentCell = null;
        }

        private void setupFillsToUndrsCurrState(int result)
        {
            this.lblResultFillsToUnders.Text = oversUnders.BestFillToUndrWidth.ToString();
            
            this.lbxFillsToUndersUnders.Items.Clear();

           

            foreach (MaterialArea area in oversUnders.OversToUndersAppliedUnders)
            {
                this.lbxFillsToUndersUnders.Items.Add(
                    "   " + ((double)area.WidthInInches / 12.0).ToString("#,##0.00") + " x " + ((double)area.LngthInInches / 12.0).ToString("#,##0.00")
                    );
            }

            //this.lblUsedOver.Text = ((double)materialArea.WidthInInches / 12.0).ToString("#,##0.00") + " x " + ((double)materialArea.LngthInInches / 12.0).ToString("#,##0.00");

            this.lblUsedLength.Text = (((double)result) / 12.0).ToString("0.00");

            this.dgvOversCurr.Rows.Clear();

            foreach (MaterialArea m in oversUnders.CurrOversbyWidthDict.Values)
            {
                string width = ((double)m.WidthInInches / 12.0).ToString("#,##0.00");
                string lngth = ((double)m.LngthInInches / 12.0).ToString("#,##0.00");

                object[] row = new object[2] { width + " ", lngth + " " };

                this.dgvOversCurr.Rows.Add(row);
            }

            this.dgvOversCurr.CurrentCell = null;

            this.dgvUndrsCurr.Rows.Clear();

            foreach (MaterialArea m in oversUnders.CurrUndrsByWidthDict.Values)
            {
                string width = ((double)m.WidthInInches / 12.0).ToString("#,##0.00");
                string lngth = ((double)m.LngthInInches / 12.0).ToString("#,##0.00");

                object[] row = new object[2] { width + " ", lngth + " " };

                this.dgvUndrsCurr.Rows.Add(row);
            }

            this.dgvUndrsCurr.CurrentCell = null;

            this.dgvPrioritiesbyWidth.Rows.Clear();

            foreach (KeyValuePair<int, int> kvp in oversUnders.widthPrioDict)
            {
                object[] row = new object[2] { (kvp.Key / 12.0).ToString("#,##0.000"), kvp.Value.ToString() };

                this.dgvPrioritiesbyWidth.Rows.Add(row);
            }

            this.dgvPrioritiesbyWidth.CurrentCell = null;
        }

        private void setupPrevState()
        {
            this.dgvUndrsPrev.Rows.Clear();

            foreach (MaterialArea m in oversUnders.CurrUndrsByWidthDict.Values)
            {
                string width = ((double)m.WidthInInches / 12.0).ToString("#,##0.00");
                string lngth = ((double)m.LngthInInches / 12.0).ToString("#,##0.00");

                object[] row = new object[2] { width + " ", lngth + " " };

                this.dgvUndrsPrev.Rows.Add(row);
            }

            this.dgvUndrsPrev.CurrentCell = null;

            this.dgvOversPrev.Rows.Clear();

            foreach (MaterialArea m in oversUnders.CurrOversbyWidthDict.Values)
            {
                string width = ((double)m.WidthInInches / 12.0).ToString("#,##0.00");
                string lngth = ((double)m.LngthInInches / 12.0).ToString("#,##0.00");

                object[] row = new object[2] { width + " ", lngth + " " };

                this.dgvOversPrev.Rows.Add(row);
            }

            this.dgvOversPrev.CurrentCell = null;

        }

        private void setupBestCombos()
        {
            this.dgvBestCombos.Rows.Clear();

            foreach (List<int> combo in oversUnders.BestComboItems)
            {
                string comboStr = string.Join(", ", combo.Select(c=>((double) c / 12.0).ToString("0.00")));
                string priority = oversUnders.GetPriority(combo).ToString();

                this.dgvBestCombos.Rows.Add(new object[2] { comboStr, priority });
            }

            this.dgvBestCombos.CurrentCell = null;
        }

    }
}
