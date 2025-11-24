
using Globals;

namespace CanvasLib.Filters.Line_Filter
{
    using FinishesLib;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using CanvasLib.Filters;

    public partial class LineFilterRow : UserControl
    {
        LineFilterForm baseForm;

        LineFinishBase lineFinishBase;

        public LineFilterRow()
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
            this.Anchor = AnchorStyles.None;
            this.AutoSize = false;
        }

        public void Init(LineFilterForm baseForm, LineFinishBase lineFinishBase)
        {
            this.baseForm = baseForm;

            this.lineFinishBase = lineFinishBase;

            this.ckbFinishFilter.Checked = !this.lineFinishBase.Filtered;
            this.lblTag.Text = this.lineFinishBase.LineName;
            this.pnlLineType.Paint += PnlLineType_Paint;

            this.ckbFinishFilter.CheckedChanged += CkbFinishFilter_CheckedChanged;

            this.lineFinishBase.LineNameChanged += LineFinishBase_LineNameChanged;
            this.lineFinishBase.LineColorChanged += LineFinishBase_LineColorChanged;
            this.lineFinishBase.LineTypeChanged += LineFinishBase_LineTypeChanged;
            this.lineFinishBase.LengthChanged += LineFinishBase_LengthChanged;
            this.lineFinishBase.LineWidthChanged += LineFinishBase_LineWidthChanged;
            this.lineFinishBase.FilteredChanged += LineFinishBase_FilteredChanged;

            SystemState.ScaleHasBeenSetChanged += SystemState_ScaleHasBeenSetChanged;

            updateStatsDisplay();

        }

        private void SystemState_ScaleHasBeenSetChanged(bool scaleHasBeenSet)
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                this.lblTotal.Text = string.Empty;
            }

            else
            {
                this.lblTotal.Text = (this.lineFinishBase.LengthInInches / 12.0).ToString("#,##0.0") + " l.f.";
            }

            if (baseForm.BtnLinesGT0.BackColor == Color.Orange)
            {
                Filter(FilteredState.ShowGT0);
            }

        }

        public void Delete()
        {
            this.lineFinishBase.LineNameChanged -= LineFinishBase_LineNameChanged;
            this.lineFinishBase.LineColorChanged -= LineFinishBase_LineColorChanged;
            this.lineFinishBase.LineTypeChanged -= LineFinishBase_LineTypeChanged;
            this.lineFinishBase.LengthChanged -= LineFinishBase_LengthChanged;
            this.lineFinishBase.LineWidthChanged -= LineFinishBase_LineWidthChanged;
            this.lineFinishBase.FilteredChanged -= LineFinishBase_FilteredChanged;

            SystemState.ScaleHasBeenSetChanged -= SystemState_ScaleHasBeenSetChanged;
        }

        private void CkbFinishFilter_CheckedChanged(object sender, System.EventArgs e)
        {
            lineFinishBase.Filtered = !this.ckbFinishFilter.Checked;
        }

        private void LineFinishBase_FilteredChanged(LineFinishBase LineFinishBase, bool filtered)
        {
            this.pnlLineType.Invalidate();
        }

        private void LineFinishBase_LineWidthChanged(LineFinishBase LineFinishBase, double lineWidthInPts)
        {
            this.pnlLineType.Invalidate();
        }

        private void LineFinishBase_LengthChanged(LineFinishBase LineFinishBase, double lengthInInches)
        {
            updateStatsDisplay();
        }

        private void LineFinishBase_LineNameChanged(LineFinishBase lineFinishBase, string lineName)
        {
            this.lblTag.Text = lineFinishBase.LineName;
        }

        private void LineFinishBase_LineColorChanged(LineFinishBase LineFinishBase, Color lineColor)
        {
            this.pnlLineType.Invalidate();
        }

        private void LineFinishBase_LineTypeChanged(LineFinishBase lineFinishBase, int lineType)
        {
            this.pnlLineType.Invalidate();
        }

        private void updateStatsDisplay()
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                this.lblTotal.Text = string.Empty;
            }

            else
            {
                this.lblTotal.Text = (this.lineFinishBase.LengthInInches / 12.0).ToString("#,##0.0") + " l.f.";
            }
            
            if (baseForm.BtnLinesGT0.BackColor == Color.Orange)
            {
                Filter(FilteredState.ShowGT0);
            }

            baseForm.UpdateLineTotals();


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
            Pen pen = new Pen(lineFinishBase.LineColor);

            // Set the width
            pen.Width = (float)lineFinishBase.LineWidthInPts;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[this.lineFinishBase.VisioLineType];

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
                if (this.lineFinishBase.LengthInInches > 0)
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
    }
}
