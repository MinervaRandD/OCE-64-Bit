

namespace SummaryReport
{
    using System;
    using System.Windows.Forms;
    using FinishesLib;
    using Utilities;
    using System.Drawing;

    public partial class RollRow : UserControl, IDisposable, IReportRow
    {
        public AreaFinishBase AreaFinishBase { get; private set; }

        int ckbFinishFilterSizeX => RowColumnWidths.CkbFinishFilterSizeX;
        int pnlColorSizeX => RowColumnWidths.PnlColorSizeX;
        int valueSizeX => RowColumnWidths.ValueSizeX;
        int unitsSizeX => RowColumnWidths.UnitsSizeX;
        int tagSizeX => RowColumnWidths.TagSizeX;

        int typeSizeX = RowColumnWidths.TypeSizeX;

        int valueSmallSizeX = RowColumnWidths.ValueSmallSizeX;

        int totlSizeX = RowColumnWidths.TotlSizeX;

        public bool Selected => this.ckbFinishFilter.Checked;

        public void SetSelectionStatus(bool selectionStatus) => this.ckbFinishFilter.Checked = selectionStatus;

        public string FinishName => this.AreaFinishBase.AreaName;

        // public double TotalInSqrFeet => AreaFinishBase.GrossAreaInSqrInches / 144.0 + AreaFinishBase.WRepeatsInSqrFeet + AreaFinishBase.LRepeatsInSqrFeet;

        public double? TotalGrossInSqrFeet
        {
            get
            {
                if (AreaFinishBase.GrossAreaInSqrInches.HasValue)
                {
                    return AreaFinishBase.GrossAreaInSqrInches.Value / 144.0;
                }

                return null;
            }
        }

        public double TotalNetInSqrFeet => AreaFinishBase.NetAreaInSqrInches / 144.0;

        public string Guid => AreaFinishBase.Guid;

        public UserControl ControlBase => this;

        public int LocationOnReport { get; set; }

        public ReportRowType ReportRowType => ReportRowType.RollRow;

        public int Index { get; set; }

        private bool scaleHasBeenSet;

        public event ReportRowChangedHandler ReportRowChanged;

        bool IReportRow.HasMeasurement { get { return AreaFinishBase.NetAreaInSqrInches > 0; } }

        public RollRow()
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
            this.Anchor = AnchorStyles.None;
            this.AutoSize = false;

            initializeControls();

            this.Disposed += RollRow_Disposed;
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

            //// Units 1

            //this.lblUnits1.Size = new Size(unitsSizeX, cntlSizeY);
            //this.lblUnits1.Location = new Point(locX, locY);

            //locX += unitsSizeX;

            // Tag

            this.lblTag.Size = new Size(tagSizeX, cntlSizeY);
            this.lblTag.Location = new Point(locX, locY);

            locX += tagSizeX;

            // Type

            this.lblType.Size = new Size(typeSizeX, cntlSizeY);
            this.lblType.Location = new Point(locX, locY);

            locX += typeSizeX;

            // Net

            this.lblNet.Size = new Size(valueSizeX, cntlSizeY);
            this.lblNet.Location = new Point(locX, locY);

            locX += valueSizeX;

            // Gross

            this.lblGross.Size = new Size(valueSizeX, cntlSizeY);
            this.lblGross.Location = new Point(locX, locY);

            locX += valueSizeX;

            //// Units 2

            //this.lblUnits2.Size = new Size(unitsSizeX, cntlSizeY);
            //this.lblUnits2.Location = new Point(locX, locY);

            //locX += unitsSizeX;

            // Waste Pct

            this.lblWastePct.Size = new Size(valueSizeX, cntlSizeY);
            this.lblWastePct.Location = new Point(locX, locY);

            locX += valueSizeX;



            //// Units 3

            //this.lblUnits3.Size = new Size(unitsSizeX, cntlSizeY);
            //this.lblUnits3.Location = new Point(locX, locY);

            //locX += unitsSizeX;

            // W-Repeats

            this.txbWRepeats.Size = new Size(valueSmallSizeX, cntlSizeY);
            this.txbWRepeats.Location = new Point(locX, locY);

            locX += valueSmallSizeX;

            // Units 4

            this.lblUnits4.Size = new Size(unitsSizeX, cntlSizeY);
            this.lblUnits4.Location = new Point(locX, locY);

            locX += unitsSizeX;

            // L-Repeats

            this.txbLRepeats.Size = new Size(valueSmallSizeX, cntlSizeY);
            this.txbLRepeats.Location = new Point(locX, locY);

            locX += valueSmallSizeX;

            // Units 5

            this.lblUnits5.Size = new Size(unitsSizeX, cntlSizeY);
            this.lblUnits5.Location = new Point(locX, locY);

            locX += unitsSizeX;

            // GrossRepeats

            this.lblTotalRepeats.Size = new Size(valueSizeX, cntlSizeY);
            this.lblTotalRepeats.Location = new Point(locX, locY);

            locX += valueSizeX;

            this.Size = new Size(totlSizeX, cntlSizeY);
        }



        public void Init(AreaFinishBase areaFinishBase, bool scaleHasBeenSet)
        {
            this.scaleHasBeenSet = scaleHasBeenSet;

            this.AreaFinishBase = areaFinishBase;

            this.pnlColor.BackColor = this.AreaFinishBase.Color;

            this.pnlColor.Paint += PnlColor_Paint;
            this.lblTag.Text = this.AreaFinishBase.AreaName;

            if (!scaleHasBeenSet)
            {
                this.txbWRepeats.Text = string.Empty;
                this.txbLRepeats.Text = string.Empty;

                this.lblUnits4.Text = string.Empty;
                this.lblUnits5.Text = string.Empty;
            }

            else
            {
                this.txbWRepeats.Text = areaFinishBase.WRepeatsInSqrFeet.ToString();
                this.txbLRepeats.Text = areaFinishBase.LRepeatsInSqrFeet.ToString();

                if (areaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareFeet)
                {
                    this.txbWRepeats.Text = areaFinishBase.WRepeatsInSqrFeet.ToString("#,##0");
                    this.txbLRepeats.Text = areaFinishBase.LRepeatsInSqrFeet.ToString("#,##0");

                    this.lblUnits4.Text = "s.f.";
                    this.lblUnits5.Text = "s.f.";
                }

                else
                {
                    this.txbWRepeats.Text = (areaFinishBase.WRepeatsInSqrFeet / 9.0).ToString("#,##0");
                    this.txbLRepeats.Text = (areaFinishBase.LRepeatsInSqrFeet / 9.0).ToString("#,##0");

                    this.lblUnits4.Text = "s.y.";
                    this.lblUnits5.Text = "s.y.";
                }
            }

            this.txbWRepeats.TextChanged += TxbWRepeats_TextChanged;
            this.txbLRepeats.TextChanged += TxbLRepeats_TextChanged;

            UpdateStatsDisplay(scaleHasBeenSet);

            areaFinishBase.FinishStatsUpdated += AreaFinishBase_FinishStatsUpdated;
            areaFinishBase.ColorChanged += AreaFinishBase_ColorChanged;
            areaFinishBase.FinishSeamChanged += AreaFinishBase_FinishSeamChanged;
            areaFinishBase.AreaNameChanged += AreaFinishBase_AreaNameChanged;
            areaFinishBase.AreaDisplayUnitsChanged += AreaFinishBase_AreaDisplayUnitsChanged;
        }

        private void AreaFinishBase_FinishSeamChanged(AreaFinishBase finishBase, SeamFinishBase seamFinishBase)
        {
            this.pnlColor.Invalidate();
        }

        private void AreaFinishBase_FinishStatsUpdated(AreaFinishBase finishBase, int count, double netAreaInSqrInches, double? grossAreaInSqrInches, double perimeterInInches, double? wasteRatio)
        {
            UpdateStatsDisplay(scaleHasBeenSet);
        }

        private void AreaFinishBase_AreaDisplayUnitsChanged(AreaFinishBase finishBase, AreaDisplayUnits areaDisplayUnits)
        {

            if (!scaleHasBeenSet)
            {
                this.txbWRepeats.Text = string.Empty;
                this.txbLRepeats.Text = string.Empty;

                this.lblUnits4.Text = string.Empty;
                this.lblUnits5.Text = string.Empty;
            }

            else
            {
                if (AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareFeet)
                {
                    this.txbWRepeats.Text = AreaFinishBase.WRepeatsInSqrFeet.ToString("#,##0");
                    this.txbLRepeats.Text = AreaFinishBase.LRepeatsInSqrFeet.ToString("#,##0");

                    this.lblUnits4.Text = "s.f.";
                    this.lblUnits5.Text = "s.f.";
                }

                else
                {
                    this.txbWRepeats.Text = (AreaFinishBase.WRepeatsInSqrFeet / 9.0).ToString("#,##0");
                    this.txbLRepeats.Text = (AreaFinishBase.LRepeatsInSqrFeet / 9.0).ToString("#,##0");

                    this.lblUnits4.Text = "s.y.";
                    this.lblUnits5.Text = "s.y.";
                }
            }

            UpdateStatsDisplay(scaleHasBeenSet);
        }

        private void AreaFinishBase_ColorChanged(AreaFinishBase finishBase, System.Drawing.Color color)
        {
            this.pnlColor.BackColor = this.AreaFinishBase.Color;
        }

        private void AreaFinishBase_AreaNameChanged(AreaFinishBase finishBase, string areaName)
        {
            this.lblTag.Text = AreaFinishBase.AreaName;

            NotifyRowChanged();
        }

        private void NotifyRowChanged()
        {
            if (ReportRowChanged != null)
                ReportRowChanged(this);
        }

        private void PnlColor_Paint(object sender, PaintEventArgs e)
        {
            SeamFinishBase seamFinishBase = this.AreaFinishBase.SeamFinishBase;

            if (seamFinishBase is null)
            {
                return;
            }

            double height = this.pnlColor.Height;
            double yIncmnt = height / 3.0;

            double yOffset = yIncmnt / 2.0 - 0.333F;

            // Set the SmoothingMode property to smooth the line.
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen;

            pen = new Pen(seamFinishBase.SeamColor);

            // Set the width
            pen.Width = (float)1.5F; // this.FinishSeamLine.LineWeight;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[seamFinishBase.VisioDashType];

            double sizeX = this.pnlColor.Width;

            for (int i = 0; i < 3; i++)
            {
                e.Graphics.DrawLine(pen, 0.1F, (float)yOffset, (float)sizeX - 0.2F, (float)yOffset);

                yOffset += yIncmnt;
            }

            // Dispose of the custom pen.
            pen.Dispose();
        }

        public void Delete()
        {
            this.Dispose();
        }

        private void RollRow_Disposed(object sender, EventArgs e)
        {
            AreaFinishBase.FinishStatsUpdated -= AreaFinishBase_FinishStatsUpdated;
            AreaFinishBase.ColorChanged -= AreaFinishBase_ColorChanged;
            AreaFinishBase.AreaNameChanged -= AreaFinishBase_AreaNameChanged;
            AreaFinishBase.AreaDisplayUnitsChanged -= AreaFinishBase_AreaDisplayUnitsChanged;
            AreaFinishBase.FinishSeamChanged -= AreaFinishBase_FinishSeamChanged;

            base.Dispose();
        }

        private void TxbWRepeats_TextChanged(object sender, EventArgs e)
        {

            Utilities.SetTextFormatForValidPositiveDouble(this.txbWRepeats);

            if (this.txbWRepeats.BackColor != SystemColors.ControlLightLight)
            {
                return;
            }

            double wRepeats = double.Parse(this.txbWRepeats.Text.Trim());

            if (AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareYards)
            {
                wRepeats *= 9.0;
            }

            AreaFinishBase.WRepeatsInSqrFeet = wRepeats;

            double? grsTotalInSqrFeet = null;

            if (AreaFinishBase.GrossAreaInSqrInches.HasValue)
            {
                grsTotalInSqrFeet = AreaFinishBase.GrossAreaInSqrInches.Value / 144.0 ;
            }

            updateTotalStat(grsTotalInSqrFeet, scaleHasBeenSet);
        }

        private void TxbLRepeats_TextChanged(object sender, EventArgs e)
        {

            Utilities.SetTextFormatForValidPositiveDouble(this.txbLRepeats);

            if (this.txbLRepeats.BackColor != SystemColors.ControlLightLight)
            {
                return;
            }

            double lRepeats = double.Parse(this.txbLRepeats.Text.Trim());

            if (AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareYards)
            {
                lRepeats *= 9.0;
            }

            AreaFinishBase.LRepeatsInSqrFeet = lRepeats;

            double? grsTotalInSqrFeet = AreaFinishBase.GrossAreaInSqrInches / 144.0;

            updateTotalStat(grsTotalInSqrFeet, scaleHasBeenSet);
        }

        public void UpdateStatsDisplay(bool scaleHasBeenSet)
        {
            double? wastePct = null;

            if (!scaleHasBeenSet)
            {
                this.lblGross.Text = string.Empty;
                this.lblNet.Text = string.Empty;
                this.lblTotal.Text = string.Empty;
                this.lblWastePct.Text = string.Empty;

                return;
            }

            double netTotalInSqrFeet = AreaFinishBase.NetAreaInSqrInches / 144.0;
            double? grsTotalInSqrFeet = AreaFinishBase.GrossAreaInSqrInches / 144.0;


            if (netTotalInSqrFeet > 0.0 && grsTotalInSqrFeet.HasValue)
            {
                wastePct = ((grsTotalInSqrFeet / netTotalInSqrFeet) - 1.0) * 100.0;
            }

            if (this.AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareFeet)
            {
                if (grsTotalInSqrFeet.HasValue)
                {
                    this.lblTotal.Text = (grsTotalInSqrFeet.Value + AreaFinishBase.WRepeatsInSqrFeet + AreaFinishBase.LRepeatsInSqrFeet).ToString("#,##0") + " s.f.";
                }

                else
                {
                    this.lblTotal.Text = "N/A";
                }

                if (grsTotalInSqrFeet.HasValue)
                {
                    this.lblGross.Text = grsTotalInSqrFeet.Value.ToString("#,##0") + " s.f.";
                }

                else
                {
                    this.lblGross.Text = "N/A";
                }
                
                this.lblNet.Text = netTotalInSqrFeet.ToString("#,##0") + " s.f.";
            }

            else
            {
                if (grsTotalInSqrFeet.HasValue)
                {
                    this.lblTotal.Text = (grsTotalInSqrFeet.Value / 9.0 + AreaFinishBase.WRepeatsInSqrFeet + AreaFinishBase.LRepeatsInSqrFeet).ToString("#,##0") + " s.y.";
                }

                else
                {
                    this.lblTotal.Text = "N/A";
                }

                if (grsTotalInSqrFeet.HasValue)
                {
                    this.lblGross.Text = (grsTotalInSqrFeet.Value / 9.0).ToString("#,##0") + " s.y.";
                }
                
                else
                {
                    this.lblGross.Text = "N/A";
                }
                
                this.lblNet.Text = (netTotalInSqrFeet / 9.0).ToString("#,##0") + " s.y.";
            }

            updateTotalStat(grsTotalInSqrFeet, scaleHasBeenSet);

            if (wastePct < 0)
            {
                this.lblWastePct.ForeColor = Color.Red;
            }

            else
            {
                this.lblWastePct.ForeColor = Color.Black;
            }

            if (wastePct.HasValue)
            {
                this.lblWastePct.Text = wastePct.Value.ToString("0.00");
            }
            
            else
            {
                this.lblWastePct.Text = "N/A";
            }

            NotifyRowChanged();
        }

        private void updateTotalStat(double? grsTotalInSqrFeet, bool scaleHasBeenSet)
        {
            if (!scaleHasBeenSet)
            {
                this.lblTotal.Text = string.Empty;
                return;
            }

            double totalRepeats = AreaFinishBase.LRepeatsInSqrFeet + AreaFinishBase.WRepeatsInSqrFeet;

            if (this.AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareFeet)
            {
                if (grsTotalInSqrFeet.HasValue)
                {
                    this.lblTotal.Text = (grsTotalInSqrFeet.Value + totalRepeats).ToString("#,##0") + " s.f.";
                }

                else
                {
                    this.lblTotal.Text = "N/A";
                }

                this.lblTotalRepeats.Text = totalRepeats.ToString("#,##0") + " s.f.";
            }

            else
            {
                if (grsTotalInSqrFeet.HasValue)
                {
                    this.lblTotal.Text = ((grsTotalInSqrFeet.Value + totalRepeats) / 9.0).ToString("#,##0") + " s.y.";
                }

                else
                {
                    this.lblTotal.Text = "N/A";
                }

                this.lblTotalRepeats.Text = (totalRepeats / 9.0).ToString("#,##0") + " s.y.";
            }

            NotifyRowChanged();
        }

        private void ckbFinishFilter_CheckedChanged(object sender, EventArgs e)
        {
            NotifyRowChanged();
        }

        internal string ToString(string separator, bool separateUnits = true, string convert = "", bool scaleHasBeenSet = true)
        {
            string rtrnStr = string.Empty;

            if (convert == "feet")
            {
                rtrnStr = ToStringFeet(separator, separateUnits, scaleHasBeenSet);

                return rtrnStr;
            }

            if (convert == "yards")
            {
                rtrnStr = ToStringYards(separator, separateUnits, scaleHasBeenSet);

                return rtrnStr;
            }

            if (this.AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareFeet)
            {
                rtrnStr = ToStringFeet(separator, separateUnits, scaleHasBeenSet);

                return rtrnStr;
            }

            if (this.AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareYards)
            {
                rtrnStr = ToStringYards(separator, separateUnits, scaleHasBeenSet);

                return rtrnStr;
            }

            return string.Empty;
        }

        internal string ToStringFeet(string separator, bool separateUnits, bool scaleHasBeenSet)
        {
            if (!scaleHasBeenSet)
            {
                if (separateUnits)
                {
                    return string.Join(separator, " ", "s.f.", AreaFinishBase.AreaName, "Roll", " ", "s.f.", " ", "%", " ", "s.f.", " ", "s.f.", " ", "s.f.");
                }

                else
                {
                    return string.Join(separator, " s.f.", AreaFinishBase.AreaName, "Roll", " s.f.", " %", " s.f.", " s.f.", " s.f.");
                }

            }

            double netTotalInSqrFeet = AreaFinishBase.NetAreaInSqrInches / 144.0;
            double? grsTotalInSqrFeet = AreaFinishBase.GrossAreaInSqrInches / 144.0;

            string total;
            string netTotal;
            string grossTotal;
            string wRepeats;
            string lRepeats;
            string waste;
 
            if (grsTotalInSqrFeet.HasValue)
            {
                total = (grsTotalInSqrFeet.Value + AreaFinishBase.WRepeatsInSqrFeet + AreaFinishBase.LRepeatsInSqrFeet).ToString("#,##0");
            }

            else
            {
                total = "N/A";
            }

            netTotal = netTotalInSqrFeet.ToString("#,##0");

            if (grsTotalInSqrFeet.HasValue)
            {
                grossTotal = grsTotalInSqrFeet.Value.ToString("#,##0");
            }
            
            else
            {
                grossTotal = "N/A";
            }

            wRepeats = AreaFinishBase.WRepeatsInSqrFeet.ToString("#,##0");
            lRepeats = AreaFinishBase.LRepeatsInSqrFeet.ToString("#,##0");

            if (netTotalInSqrFeet > 0.0 && grsTotalInSqrFeet.HasValue)
            {
                waste = ((grsTotalInSqrFeet.Value / netTotalInSqrFeet - 1.0) * 100.0).ToString("0.00");
            }

            else
            {
                waste = "N/A";
            }

            if (separateUnits)
            {
                total += separator;
                netTotal += separator;
                waste += separator;
                grossTotal += separator;
                wRepeats += separator;
                lRepeats += separator;
            }

            else
            {
                total += ' ';
                netTotal += ' ';
                waste += ' ';
                grossTotal += ' ';
                wRepeats += ' ';
                lRepeats += ' ';
            }

            total += "s.f.";
            netTotal += "s.f.";
            waste += "%";
            grossTotal += "s.f.";
            wRepeats += "s.f.";
            lRepeats += "s.f.";

            string rtrnString = total + separator + AreaFinishBase.AreaName + separator + "Roll" + separator + netTotal + separator + waste + separator + grossTotal + separator + wRepeats + separator + lRepeats;

            return rtrnString;
        }

        internal string ToStringYards(string separator, bool separateUnits, bool scaleHasBeenSet)
        {
            if (!scaleHasBeenSet)
            {
                if (separateUnits)
                {
                    return string.Join(separator, " ", "s.y.", AreaFinishBase.AreaName, "Roll", " ", "s.y.", " ", "%", " ", "s.y.", " ", "s.y.", " ", "s.y.");
                }

                else
                {
                    return string.Join(separator, " s.y.", AreaFinishBase.AreaName, "Roll", " s.y.", " %", " s.y.", " s.y.", " s.y.");
                }
            }

            double netTotalInSqrFeet = AreaFinishBase.NetAreaInSqrInches / 144.0;
            double? grsTotalInSqrFeet = AreaFinishBase.GrossAreaInSqrInches / 144.0;

            string total;
            string netTotal;
            string grossTotal;
            string wRepeats;
            string lRepeats;
            string waste;

            if (grsTotalInSqrFeet.HasValue)
            {
                total = ((grsTotalInSqrFeet.Value + AreaFinishBase.WRepeatsInSqrFeet + AreaFinishBase.LRepeatsInSqrFeet) / 9.0).ToString("#,##0");
            }

            else
            {
                total = "N/A";
            }

            netTotal = (netTotalInSqrFeet / 9.0).ToString("#,##0");

            if (grsTotalInSqrFeet.HasValue)
            {
                grossTotal = (grsTotalInSqrFeet.Value / 9.0).ToString("#,##0");
            }

            else
            {
                grossTotal = "N/A";
            }

            wRepeats = (AreaFinishBase.WRepeatsInSqrFeet / 9.0).ToString("#,##0");
            lRepeats = (AreaFinishBase.LRepeatsInSqrFeet / 9.0).ToString("#,##0");

            if (netTotalInSqrFeet > 0.0 && grsTotalInSqrFeet.HasValue)
            {
                waste = ((grsTotalInSqrFeet.Value / netTotalInSqrFeet - 1.0) * 100.0).ToString("0.00") + "%";
            }

            else
            {
                waste = "N/A";
            }

            if (separateUnits)
            {
                total += separator;
                netTotal += separator;
                waste += separator;
                grossTotal += separator;
                wRepeats += separator;
                lRepeats += separator;
            }

            else
            {
                total += ' ';
                netTotal += ' ';
                waste += ' ';
                grossTotal += ' ';
                wRepeats += ' ';
                lRepeats += ' ';
            }

            total += "s.y.";
            netTotal += "s.y.";
            waste += "%";
            grossTotal += "s.y.";
            wRepeats += "s.y.";
            lRepeats += "s.y.";

            string rtrnString = total + separator + AreaFinishBase.AreaName + separator + "Roll" + separator + netTotal + separator + waste + separator + grossTotal + separator + wRepeats + separator + lRepeats;

            return rtrnString;
        }
    }
}
