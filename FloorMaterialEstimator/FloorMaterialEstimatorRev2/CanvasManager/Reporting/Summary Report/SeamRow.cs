

namespace CanvasManager.Reports.SummaryReport
{
    using System;
    using System.Windows.Forms;
    using FinishesLib;

    using System.Drawing;

    public partial class SeamRow : UserControl, IDisposable, IReportRow
    {
        public SeamFinishBase SeamFinishBase { get; set; }

        int ckbFinishFilterSizeX => RowColumnWidths.CkbFinishFilterSizeX;
        int pnlColorSizeX => RowColumnWidths.PnlColorSizeX;
        int valueSizeX => RowColumnWidths.ValueSizeX;
        int unitsSizeX => RowColumnWidths.UnitsSizeX;
        int tagSizeX => RowColumnWidths.TagSizeX;

        public bool Selected => this.ckbFinishFilter.Checked;

        public int LocationOnReport { get; set; }

        public string Guid => SeamFinishBase.Guid;

        public ReportRowType ReportRowType => ReportRowType.SeamRow;

        public UserControl ControlBase => this;

        public void SetSelectionStatus(bool selectionStatus) { }

        public int Index { get; set; }

        int typeSizeX = RowColumnWidths.TypeSizeX;

        int valueSmallSizeX = RowColumnWidths.ValueSmallSizeX;

        int totlSizeX = RowColumnWidths.TotlSizeX;

        private bool scaleHasBeenSet = false;

        public event ReportRowChangedHandler ReportRowChanged;

        bool IReportRow.HasMeasurement { get { return SeamFinishBase.LengthInInches > 0; } }

        public double LengthInInches {  get
            {
                return SeamFinishBase.LengthInInches;
            } 
        }

        public SeamRow()
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
            this.Anchor = AnchorStyles.None;
            this.AutoSize = false;

            initializeControls();

            this.pnlColor.Paint += PnlColor_Paint;
            this.Disposed += SeamRow_Disposed;
        }

        private void initializeControls()
        {
            // Set up element sizes

            int cntlSizeY = 21;

            int locX = 1;
            int locY = 1;

            // Check box

            this.ckbFinishFilter.Size = new Size(ckbFinishFilterSizeX, cntlSizeY);
            this.ckbFinishFilter.Location = new Point(locX, locY);

            locX += ckbFinishFilterSizeX;

            // Color panel

            this.pnlColor.Size = new Size(pnlColorSizeX, cntlSizeY);
            this.pnlColor.Location = new Point(locX, locY);

            locX += pnlColorSizeX;

            // Total

            this.lblTotal.Size = new Size(valueSizeX, cntlSizeY);
            this.lblTotal.Location = new Point(locX, locY);

            locX += valueSizeX;

            //// Units

            //this.lblUnits.Size = new Size(unitsSizeX, cntlSizeY);
            //this.lblUnits.Location = new Point(locX, locY);

            //locX += unitsSizeX;

            // Tag

            this.lblTag.Size = new Size(tagSizeX, cntlSizeY);
            this.lblTag.Location = new Point(locX, locY);

            locX += tagSizeX;

            // Tag

            this.lblType.Size = new Size(typeSizeX, cntlSizeY);
            this.lblType.Location = new Point(locX, locY);

            locX += typeSizeX;

            this.Size = new Size(totlSizeX, cntlSizeY);
        }

        public void Init(SeamFinishBase seamFinishBase, bool scaleHasBeenSet)
        {
            this.scaleHasBeenSet = scaleHasBeenSet;

            this.SeamFinishBase = seamFinishBase;

            //this.pnlColor.BackColor = this.lineFinishBase.Color;
            this.lblTag.Text = this.SeamFinishBase.SeamName;

            UpdateStatsDisplay(scaleHasBeenSet);

            this.SeamFinishBase.LengthChanged += SeamFinishBase_LengthChanged;
            this.SeamFinishBase.SeamGraphicsChanged += SeamFinishBase_SeamGraphicsChanged;
            this.SeamFinishBase.SeamNameChanged += SeamFinishBase_SeamNameChanged;
        }

        private void NotifyRowChanged()
        {
            if (ReportRowChanged != null)
                ReportRowChanged(this);
        }

        private void SeamFinishBase_LengthChanged(SeamFinishBase SeamFinishBase, double lengthInInches)
        {
            if (lengthInInches >= 0.0)
            { 
                UpdateStatsDisplay(scaleHasBeenSet);
            }

            NotifyRowChanged();
        }

        private void SeamFinishBase_SeamGraphicsChanged(SeamFinishBase SeamFinishBase)
        {
            this.pnlColor.Invalidate();
        }

        private void SeamFinishBase_SeamNameChanged(SeamFinishBase seamFinishBase, string seamName)
        {
            this.lblTag.Text = seamName;
        }

        public void Delete()
        {
            this.Dispose();
        }

        private void SeamRow_Disposed(object sender, EventArgs e)
        {
            this.SeamFinishBase.LengthChanged -= SeamFinishBase_LengthChanged;
            this.SeamFinishBase.SeamGraphicsChanged -= SeamFinishBase_SeamGraphicsChanged;
            this.SeamFinishBase.SeamNameChanged -= SeamFinishBase_SeamNameChanged;
        }

        private void AreaFinishBase_FinishStatsUpdated(AreaFinishBase finishBase, double netAreaInSqrInches, double grossAreaInSqrInches, double perimeterInInches)
        {
            UpdateStatsDisplay(scaleHasBeenSet);
        }


        private void PnlColor_Paint(object sender, PaintEventArgs e)
        {

            if (SeamFinishBase is null)
            {
                return;
            }

            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen = new Pen(SeamFinishBase.SeamColor);

            // Set the width
            pen.Width = (float)SeamFinishBase.SeamWidthInPts;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            int visioDashType = SeamFinishBase.VisioDashType;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[visioDashType];

            e.Graphics.DrawLine(pen, 2.0F, 10.0F, 28.0F, 10.0F);


            // Dispose of the custom pen.
            pen.Dispose();

        }

        public void UpdateStatsDisplay(bool scaleHasBeenSet)
        {
            if (!scaleHasBeenSet)
            {
                this.lblTotal.Text = string.Empty;
                return;
            }

            this.lblTotal.Text = (LengthInInches / 12.0).ToString("#,##0") + " l.f.";

            NotifyRowChanged();
        }

        private void ckbFinishFilter_CheckedChanged(object sender, EventArgs e)
        {
            NotifyRowChanged();
        }

        internal string ToString(string separator, bool separateUnits = true, string convert = "", bool scaleHasBeenSet = true)
        {
            if (!scaleHasBeenSet)
            {
                
                if (convert == "yards")
                {
                    if (separateUnits)
                    {
                        return " " + separator + "s.y." + separator + SeamFinishBase.SeamName + separator + "Seam";
                    }

                    else
                    {
                        return " s.y." + separator + SeamFinishBase.SeamName + separator + "Seam";
                    }
                }

                else
                {
                    if (separateUnits)
                    {
                        return " " + separator + "s.f." + separator + SeamFinishBase.SeamName + separator + "Seam";
                    }

                    else
                    {
                        return " s.f." + separator + SeamFinishBase.SeamName + separator + "Seam";
                    }
                }
            }

            string total = (SeamFinishBase.LengthInInches / 12.0).ToString("#,##0");

            if (convert == "feet" || string.IsNullOrEmpty(convert))
            {
                total = (SeamFinishBase.LengthInInches / 12.0).ToString("#,##0");

                if (separateUnits)
                {
                    total += separator + "l.f.";
                }

                else
                {
                    total += " l.f.";
                }
            }

            else if (convert == "yards")
            {
                total = (SeamFinishBase.LengthInInches / 36.0).ToString("#,##0");

                if (separateUnits)
                {
                    total += separator + "l.y.";
                }

                else
                {
                    total += " l.y.";
                }
            }

            string rtrnString = total + separator + SeamFinishBase.SeamName + separator + "Seam";

            return rtrnString;
        }
    }
}
