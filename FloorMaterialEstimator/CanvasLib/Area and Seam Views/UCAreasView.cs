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
    public partial class UCAreasView : UserControl
    {
        public UCAreasView()
        {
            InitializeComponent();

            this.Size = new Size(212, 250);
        }

        public void Init()
        {
            this.Size = new Size(212, 302);

            dgvAreas.Columns.Add("#", "#");
            dgvAreas.Columns.Add("Area", "Area");

            dgvAreas.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            dgvAreas.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);

            dgvAreas.Columns[0].Width = 32;
            dgvAreas.Columns[1].Width = dgvAreas.Width - 36 - 20;

            dgvAreas.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAreas.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvAreas.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            dgvAreas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public double? Setup(string finishName, SortedList<int, double> seamedAreaList, double totalFinishArea, double totalSeamedArea)
        {
            dgvAreas.Rows.Clear();

            this.lblFinish.Text = finishName;

            foreach (KeyValuePair<int, double> kvp in seamedAreaList)
            {
                if (kvp.Key == -1)
                {
                    // This is a fixed width area summary

                    dgvAreas.Rows.Add(new object[] { "F ", (kvp.Value / 144.0).ToString("#,##0.0") + "  " });
                }

                else
                {
                    dgvAreas.Rows.Add(new object[] { kvp.Key.ToString() + " ", (kvp.Value / 144.0).ToString("#,##0.0") + "  " });
                }

            }

            totalSeamedArea /= 144.0;

            this.lblFinishTotalArea.Text = totalFinishArea.ToString("#,##0 ");

            double? pctAreaSelected = null;

            if (totalFinishArea <= 0)
            {
                this.lblPctAreaSelected.Text = "N/A";
            }

            else
            {
                pctAreaSelected = totalSeamedArea / totalFinishArea;

                this.lblPctAreaSelected.Text = (100.0 * pctAreaSelected.Value).ToString("#,##0.0") + "% ";
            }

            dgvAreas.Refresh();

            dgvAreas.CurrentCell = null;

            return pctAreaSelected;
        }
    }
}
