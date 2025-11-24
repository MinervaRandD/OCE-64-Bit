using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialsLayout;
using MaterialsLayout.MaterialsLayout;
using Utilities;

namespace CanvasLib.Area_and_Seam_Views
{
    public partial class UCRemnantViewElement : UserControl
    {
        public UCRemnantViewElement()
        {
            InitializeComponent();

            this.Load += UCRemnantViewElement_Load;
        }

        private void UCRemnantViewElement_Load(object sender, EventArgs e)
        {
            this.Size = new Size(210, 302);

            this.dgvRemnant.Size = new Size(236, 180);
            this.dgvRemnant.Location = new Point(1, 1);

            this.lblWasteFactorTitle.Location = new Point(32, dgvRemnant.Height + 16);
            this.lblWasteFactor.Location = new Point(48 + this.lblWasteFactorTitle.Width, this.lblWasteFactorTitle.Location.Y);
        }

        public void Init()
        {
           

            dgvRemnant.Columns.Add("#", "#");
            dgvRemnant.Columns.Add("Width", "Width");
            dgvRemnant.Columns.Add("Height", "Height");

            dgvRemnant.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            dgvRemnant.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            dgvRemnant.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);

            dgvRemnant.Columns[0].Width = 32;
            dgvRemnant.Columns[1].Width = 84;
            dgvRemnant.Columns[2].Width = 83;

            dgvRemnant.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRemnant.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRemnant.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvRemnant.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            dgvRemnant.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvRemnant.SelectionChanged += DgvRemnant_SelectionChanged;

        }

        private void DgvRemnant_SelectionChanged(object sender, EventArgs e)
        {
            this.dgvRemnant.ClearSelection();
        }

        public void Setup(List<GraphicsCut> graphicsCutList, double remnantAreaInInches, double totalAreaInInches)
        {
            double wasteFactor = 1.0 - remnantAreaInInches / totalAreaInInches;

            dgvRemnant.Rows.Clear();

            int indx = 1;

            foreach (GraphicsCut graphicsCut in graphicsCutList)
            {
                foreach (GraphicsRemnantCut graphicsRemantCut in graphicsCut.GraphicsRemnantCutList)
                {
                    dgvRemnant.Rows.Add(
                        new object[]
                            {   indx.ToString()
                                ,Utilities.FormatUtils.FormatInchesToFeetAndInches(graphicsRemantCut.WidthInInches)
                                ,Utilities.FormatUtils.FormatInchesToFeetAndInches(graphicsRemantCut.HeightInInches) });

                    indx++;
                }
            }

            dgvRemnant.Refresh();

            dgvRemnant.CurrentCell = null;

            this.lblWasteFactor.Text = (100.0 * wasteFactor).ToString("0.0") + '%';
        }
    }
}
