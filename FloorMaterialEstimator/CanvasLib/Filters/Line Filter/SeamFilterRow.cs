
namespace CanvasLib.Filters.Line_Filter
{
    using FinishesLib;
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Globals;
    public partial class SeamFilterRow : UserControl
    {
        LineFilterForm baseForm;

        SeamFinishBase seamFinishBase;

        public SeamFilterRow()
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
            this.Anchor = AnchorStyles.None;
            this.AutoSize = false;
        }

        public void Init(LineFilterForm baseForm, SeamFinishBase seamFinishBase)
        {
            this.baseForm = baseForm;

            this.seamFinishBase = seamFinishBase;

            this.ckbFinishFilter.Checked = !this.seamFinishBase.Filtered;
            this.lblTag.Text = this.seamFinishBase.SeamName;
            this.pnlSeamType.Paint += PnlLineType_Paint;

            this.seamFinishBase.LengthChanged += SeamFinishBase_LengthChanged;
            this.seamFinishBase.FilteredChanged += SeamFinishBase_FilteredChanged;
            this.ckbFinishFilter.CheckedChanged += CkbFinishFilter_CheckedChanged;

            SystemState.ScaleHasBeenSetChanged += SystemState_ScaleHasBeenSetChanged;

            UpdateStatsDisplay();
        }

        private void SystemState_ScaleHasBeenSetChanged(bool scaleHasBeenSet)
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                this.lblTotal.Text = string.Empty;
            }

            else
            {
                this.lblTotal.Text = (this.seamFinishBase.LengthInInches / 12.0).ToString("#,##0.0") + " l.f.";
            }

            if (baseForm.BtnLinesGT0.BackColor == Color.Orange)
            {
                Filter(FilteredState.ShowGT0);
            }

        }

        private void CkbFinishFilter_CheckedChanged(object sender, EventArgs e)
        {
            this.seamFinishBase.Filtered = !ckbFinishFilter.Checked;
        }

        private void SeamFinishBase_FilteredChanged(SeamFinishBase SeamFinishBase, bool filtered)
        {
            baseForm.UpdateSeamTotals();
        }

        private void SeamFinishBase_LengthChanged(SeamFinishBase SeamFinishBase, double lengthInInches)
        {
            UpdateStatsDisplay();
        }

        public void UpdateStatsDisplay()
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                this.lblTotal.Text = string.Empty;
            }

            else
            {
                this.lblTotal.Text = (this.seamFinishBase.LengthInInches / 12.0).ToString("#,##0.0") + " l.f.";
            }

            if (baseForm.BtnLinesGT0.BackColor == Color.Orange)
            {
                Filter(FilteredState.ShowGT0);
            }

            baseForm.UpdateSeamTotals();

            
        }

        private void PnlLineType_Paint(object sender, PaintEventArgs e)
        {
            Draw(e);
        }

        private void Draw(PaintEventArgs e)
        {
            // Set the SmoothingMode property to smooth the line.
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen = new Pen(seamFinishBase.SeamColor);

            // Set the width
            pen.Width = (float)seamFinishBase.SeamWidthInPts;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[this.seamFinishBase.VisioDashType];

            e.Graphics.DrawLine(pen, 12.0F, 8.0F, 182.0F, 8.0F);

            // Dispose of the custom pen.
            pen.Dispose();
        }


        internal void Filter(FilteredState filteredState)
        {
            if (filteredState == FilteredState.ShowAll)
            {
                if (!this.ckbFinishFilter.Checked)
                {
                    this.ckbFinishFilter.Checked = true;
                }

                return;
            }

            if (filteredState == FilteredState.ShowNone)
            {
                if (this.ckbFinishFilter.Checked)
                {
                    this.ckbFinishFilter.Checked = false;
                }

                return;
            }

            if (filteredState == FilteredState.ShowGT0)
            {
                if (this.seamFinishBase.LengthInInches > 0)
                {
                    if (!this.ckbFinishFilter.Checked)
                    {
                        this.ckbFinishFilter.Checked = true;
                    }
                }

                else if (this.ckbFinishFilter.Checked)
                {
                    this.ckbFinishFilter.Checked = false;
                }

                return;
            }

        }

        public void Delete()
        {
            this.seamFinishBase.LengthChanged -= SeamFinishBase_LengthChanged;
            this.seamFinishBase.FilteredChanged -= SeamFinishBase_FilteredChanged;
            this.ckbFinishFilter.CheckedChanged -= CkbFinishFilter_CheckedChanged;

            SystemState.ScaleHasBeenSetChanged -= SystemState_ScaleHasBeenSetChanged;

        }
    }
}
