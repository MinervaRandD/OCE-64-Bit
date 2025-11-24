

namespace SummaryReport
{
    using System;
    using System.Windows.Forms;
    using FinishesLib;

    using System.Drawing;

    public partial class LineRow : UserControl, IDisposable, IReportRow
    {
        public LineFinishBase LineFinishBase { get; private set; }

        int ckbFinishFilterSizeX => RowColumnWidths.CkbFinishFilterSizeX;
        int pnlColorSizeX => RowColumnWidths.PnlColorSizeX;
        int valueSizeX => RowColumnWidths.ValueSizeX;
        int unitsSizeX => RowColumnWidths.UnitsSizeX;
        int tagSizeX => RowColumnWidths.TagSizeX;

        public bool Selected => this.ckbFinishFilter.Checked;

        public int LocationOnReport { get; set; }

        public string Guid => LineFinishBase.Guid;

        public ReportRowType ReportRowType => ReportRowType.LineRow;

        public UserControl ControlBase => this;

        public void SetSelectionStatus(bool selectionStatus) { }

        public int Index { get; set; }

        int typeSizeX = RowColumnWidths.TypeSizeX;

        int valueSmallSizeX = RowColumnWidths.ValueSmallSizeX;

        int totlSizeX = RowColumnWidths.TotlSizeX;

        private bool scaleHasBeenSet = false;

        public event ReportRowChangedHandler ReportRowChanged;

        bool IReportRow.HasMeasurement { get { return LineFinishBase.LengthInInches > 0; } }

        public double LengthInInches
        {
            get
            {
                return LineFinishBase.LengthInInches;
            }
        }

        public double LgthInFeet
        {
            get
            {
                return LengthInInches / 12.0;
            }
        }

        public double? SqrAreaInFeet
        {
            get
            {
                if (!LineFinishBase.IsWallLine)
                {
                    return null;
                }

                return LgthInFeet * LineFinishBase.WallHeightInFeet.Value;
            }
        }





        public LineRow()
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
            this.Anchor = AnchorStyles.None;
            this.AutoSize = false;

            initializeControls();

            this.pnlColor.Paint += PnlColor_Paint;
            this.Disposed += LineRow_Disposed;
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

            this.lblTotalLF.Size = new Size(valueSizeX, cntlSizeY);
            this.lblTotalLF.Location = new Point(locX, locY);

            locX += valueSizeX;


            // Total

            this.lblTotalSF.Size = new Size(valueSizeX, cntlSizeY);
            this.lblTotalSF.Location = new Point(locX, locY);

            locX += valueSizeX;



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

        public void Init(LineFinishBase lineFinishBase, bool scaleHasBeenSet)
        {
            this.scaleHasBeenSet = scaleHasBeenSet;

            this.LineFinishBase = lineFinishBase;

            //this.pnlColor.BackColor = this.lineFinishBase.Color;
            this.lblTag.Text = this.LineFinishBase.LineName;

            UpdateStatsDisplay(scaleHasBeenSet);

            this.LineFinishBase.LengthChanged += LineFinishBase_LengthChanged;

            this.LineFinishBase.LineColorChanged += LineFinishBase_LineColorChanged;
            this.LineFinishBase.LineTypeChanged += LineFinishBase_LineTypeChanged;
            this.LineFinishBase.LineWidthChanged += LineFinishBase_LineWidthChanged;
            this.LineFinishBase.LineNameChanged += LineFinishBase_LineNameChanged;
            this.LineFinishBase.WallHeightChanged += LineFinishBase_WallHeightChanged;
        }

        private void NotifyRowChanged()
        {
            if (ReportRowChanged != null)
                ReportRowChanged(this);
        }

        private void LineFinishBase_LineWidthChanged(LineFinishBase LineFinishBase, double lineWidthInPts)
        {
            this.pnlColor.Invalidate();
        }

        private void LineFinishBase_LineTypeChanged(LineFinishBase lineFinishBase, int lineType)
        {
            this.pnlColor.Invalidate();
        }

        private void LineFinishBase_LineColorChanged(LineFinishBase LineFinishBase, Color lineColor)
        {
            this.pnlColor.Invalidate();
        }

        public void Delete()
        {
            this.Dispose();
        }

        private void LineRow_Disposed(object sender, EventArgs e)
        {
            this.LineFinishBase.LengthChanged -= LineFinishBase_LengthChanged;
            this.LineFinishBase.WallHeightChanged -= LineFinishBase_WallHeightChanged;
            this.LineFinishBase.LineNameChanged -= LineFinishBase_LineNameChanged;
            this.LineFinishBase.LineColorChanged -= LineFinishBase_LineColorChanged;
            this.LineFinishBase.LineTypeChanged -= LineFinishBase_LineTypeChanged;
            this.LineFinishBase.LineWidthChanged -= LineFinishBase_LineWidthChanged;
        }

        private void LineFinishBase_LengthChanged(LineFinishBase LineFinishBase, double lengthInInches)
        {
            UpdateStatsDisplay(scaleHasBeenSet);
        }

        private void LineFinishBase_WallHeightChanged(LineFinishBase LineFinishBase, double? wallHeightInFeet)
        {
            UpdateStatsDisplay(scaleHasBeenSet);
        }

        private void LineFinishBase_LineNameChanged(LineFinishBase lineFinishBase, string lineName)
        {
            this.lblTag.Text = this.LineFinishBase.LineName;
        }

        private void AreaFinishBase_FinishStatsUpdated(AreaFinishBase finishBase, double netAreaInSqrInches, double grossAreaInSqrInches, double perimeterInInches)
        {
            UpdateStatsDisplay(scaleHasBeenSet);
        }


        private void PnlColor_Paint(object sender, PaintEventArgs e)
        {
      
            if (LineFinishBase is null)
            {
                return;
            }

            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen = new Pen(LineFinishBase.LineColor);

            // Set the width
            pen.Width = (float)LineFinishBase.LineWidthInPts;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            int visioDashType = LineFinishBase.VisioLineType;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[visioDashType];

            e.Graphics.DrawLine(pen, 2.0F, 10.0F, 28.0F, 10.0F);


            // Dispose of the custom pen.
            pen.Dispose();

        }
#if false
        private void AreaFinishBase_ColorChanged(AreaFinishBase finishBase, System.Drawing.Color color)
        {
            this.pnlColor.BackColor = this.lineFinishBase.Color;
        }

        private void AreaFinishBase_AreaNameChanged(AreaFinishBase finishBase, string areaName)
        {
            this.lblTag.Text = lineFinishBase.AreaName;
        }

        bool firstClick = true;

        private void ckbFinishFilter_CheckedChanged(object sender, System.EventArgs e)
        {
            this.lineFinishBase.Filtered = !this.ckbFinishFilter.Checked;

            if (firstClick)
            {
                // Some weird bug requires this.
                this.lineFinishBase.Filtered = this.ckbFinishFilter.Checked;
                this.lineFinishBase.Filtered = !this.ckbFinishFilter.Checked;

                firstClick = false; 
            }
        }
#endif

        public void UpdateStatsDisplay(bool scaleHasBeenSet)
        {
            if (!scaleHasBeenSet)
            {
                this.lblTotalLF.Text = string.Empty;
                this.lblTotalSF.Text = string.Empty;

                return;
            }

            this.lblTotalLF.Text = (LengthInInches / 12.0).ToString("#,##0") + " l.f.";

            double? sqrFeet = LineFinishBase.sqrAreaInFeet;

            if (sqrFeet == null)
            {
                this.lblTotalSF.Text = string.Empty;

                this.lblType.Text = "Line";
            }

            else
            {
                this.lblTotalSF.Text = sqrFeet.Value.ToString("#,##0") + " s.f.";
                this.lblType.Text = "Wall Line";
            }

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
                        return " " + separator + "l.y." + separator + LineFinishBase.LineName + separator + "Line";
                    }

                    else
                    {
                        return " l.y." + separator + LineFinishBase.LineName + separator + "Line";
                    }
                    
                }

                else
                {
                    if (separateUnits)
                    {
                        return " " + separator + "l.f." + separator + LineFinishBase.LineName + separator + "Line";
                    }

                    else
                    {
                        return " l.f." + separator + LineFinishBase.LineName + separator + "Line";
                    }
                }
                
            }

            string total = (LineFinishBase.LengthInInches / 12.0).ToString("#,##0");

            if (convert == "feet" || string.IsNullOrEmpty(convert))
            {
                total = (LineFinishBase.LengthInInches / 12.0).ToString("#,##0");

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
                total = (LineFinishBase.LengthInInches / 36.0).ToString("#,##0");

                if (separateUnits)
                {
                    total += separator + "l.y.";
                }

                else
                {
                    total += " l.y.";
                }
            }

            string rtrnString = total + separator + LineFinishBase.LineName + separator + "Line";

            return rtrnString;
        }
    }
}
