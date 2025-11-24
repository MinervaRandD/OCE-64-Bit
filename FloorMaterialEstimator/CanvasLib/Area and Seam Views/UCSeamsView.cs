using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasLib.Area_and_Seam_Views
{
    public partial class UCSeamsView : UserControl
    {
        public UCSeamsView()
        {
            InitializeComponent();
        }

        public void Init()
        {

            this.dgvSeams.Columns.Add("Seam Index", "Seam Number");
            this.dgvSeams.Columns.Add("Length", "Length");

            this.dgvSeams.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            this.dgvSeams.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);

            this.dgvSeams.Columns[0].Width = 32;
            this.dgvSeams.Columns[1].Width = this.dgvSeams.Width - 54;

            this.dgvSeams.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvSeams.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvSeams.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            this.dgvSeams.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvSeams.AllowUserToAddRows = false;

        }

        public void Setup(string seamName, SortedList<int, double> seamModeSeamData)
        {
            dgvSeams.Rows.Clear();

            this.lblSeam.Text = seamName;

            double totalLnth = 0.0;

            foreach (KeyValuePair<int, double> kvp in seamModeSeamData)
            {
                dgvSeams.Rows.Add(new object[] { kvp.Key.ToString() + " ", (kvp.Value / 12.0).ToString("#,##0.0") + "  " });

                totalLnth += kvp.Value / 12.0;
            }

            this.lblSeamsTotalLength.Text = totalLnth.ToString("#,##0 ");

            dgvSeams.AllowUserToAddRows = false;

            dgvSeams.CurrentCell = null;

            dgvSeams.Refresh();
        }
    }
}
