

namespace SummaryReport
{
    using System;
    using System.Windows.Forms;
    using FinishesLib;

    using System.Drawing;

    public partial class TileRow : UserControl, IDisposable, IReportRow
    {
        public AreaFinishBase AreaFinishBase { get; private set; }


        int ckbFinishFilterSizeX => RowColumnWidths.CkbFinishFilterSizeX;
        int pnlColorSizeX => RowColumnWidths.PnlColorSizeX;
        int valueSizeX => RowColumnWidths.ValueSizeX;
        int unitsSizeX => RowColumnWidths.UnitsSizeX;
        int tagSizeX => RowColumnWidths.TagSizeX;
        int typeSizeX = RowColumnWidths.TypeSizeX;
        
        int totlSizeX = RowColumnWidths.TotlSizeX;

        public bool Selected => this.ckbFinishFilter.Checked;

        public void SetSelectionStatus(bool selectionStatus) => this.ckbFinishFilter.Checked = selectionStatus;

        public string FinishName => this.AreaFinishBase.AreaName;

        public double? TotalInSqrFeet => AreaFinishBase.TileAreaGrossAreaInSqrInches / 144.0 + AreaFinishBase.WRepeatsInSqrFeet + AreaFinishBase.LRepeatsInSqrFeet;

        public double TotalNetInSqrFeet => AreaFinishBase.NetAreaInSqrInches / 144.0;

        public string Guid => AreaFinishBase.Guid;

        public ReportRowType ReportRowType => ReportRowType.NonRollRow;

        public int LocationOnReport { get; set; }

        public UserControl ControlBase => this;

        public int Index { get; set; }

        private bool scaleHasBeenSet = false;

        public event ReportRowChangedHandler ReportRowChanged;

        bool IReportRow.HasMeasurement { get { return AreaFinishBase.NetAreaInSqrInches > 0; } }

        public TileRow()
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
            this.Anchor = AnchorStyles.None;
            this.AutoSize = false;

            initializeControls();

            this.Disposed += NonRollRow_Disposed;
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

            // Type

            this.lblType.Size = new Size(typeSizeX, cntlSizeY);
            this.lblType.Location = new Point(locX, locY);

            locX += typeSizeX;

            // Net

            this.lblNet.Size = new Size(valueSizeX, cntlSizeY);
            this.lblNet.Location = new Point(locX, locY);

            locX += valueSizeX;

            // Gross (dummy)

            this.lblGross.Size = new Size(valueSizeX, cntlSizeY);
            this.lblGross.Location = new Point(locX, locY);

            locX += valueSizeX;

            //// Units1

            //this.lblUnits1.Size = new Size(unitsSizeX, cntlSizeY);
            //this.lblUnits1.Location = new Point(locX, locY);

            //locX += unitsSizeX;

            // Waste Pct

            this.lblWastePct.Size = new Size(valueSizeX, cntlSizeY);
            this.lblWastePct.Location = new Point(locX, locY);

            locX += valueSizeX;

            this.Size = new Size(totlSizeX, cntlSizeY);
        }

        public void Init( AreaFinishBase areaFinishBase, bool scaleHasBeenSet)
        {
            this.scaleHasBeenSet = scaleHasBeenSet;

            this.AreaFinishBase = areaFinishBase;

            this.pnlColor.BackColor = this.AreaFinishBase.Color;
            this.lblTag.Text = this.AreaFinishBase.AreaName;

            UpdateStatsDisplay(scaleHasBeenSet);

            areaFinishBase.FinishStatsUpdated += AreaFinishBase_FinishStatsUpdated;
            
            areaFinishBase.ColorChanged += AreaFinishBase_ColorChanged;
            areaFinishBase.AreaNameChanged += AreaFinishBase_AreaNameChanged;
        }

        public void Delete()
        {
            this.Dispose();
        }

        private void NonRollRow_Disposed(object sender, EventArgs e)
        {
            AreaFinishBase.FinishStatsUpdated -= AreaFinishBase_FinishStatsUpdated;
        }

        private void AreaFinishBase_FinishStatsUpdated(AreaFinishBase finishBase, int count, double netAreaInSqrInches, double? grossAreaInSqrInches, double perimeterInInches, double? wasteRatio)
        {
            UpdateStatsDisplay(scaleHasBeenSet);
        }

        private void AreaFinishBase_ColorChanged(AreaFinishBase finishBase, System.Drawing.Color color)
        {
            this.pnlColor.BackColor = this.AreaFinishBase.Color;
        }

        private void AreaFinishBase_AreaNameChanged(AreaFinishBase finishBase, string areaName)
        {
            this.lblTag.Text = AreaFinishBase.AreaName;

            //NotifyRowChanged();
        }
        private void NotifyRowChanged()
        {
            if (ReportRowChanged != null)
                ReportRowChanged(this);
        }

        public void UpdateStatsDisplay(bool scaleHasBeenSet)
        {
            if (!scaleHasBeenSet)
            {
                this.lblTotal.Text = string.Empty;
                this.lblNet.Text = string.Empty;
                this.lblWastePct.Text = string.Empty;

                return;
            }

            double netTotalInSqrFeet = AreaFinishBase.NetAreaInSqrInches / 144.0;
            double? grsTotalInSqrFeet = AreaFinishBase.TileAreaGrossAreaInSqrInches / 144.0;
            double? wastePct = AreaFinishBase.WastePct;

            if (AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareFeet)
            {
                if (grsTotalInSqrFeet.HasValue)
                {
                    this.lblTotal.Text = grsTotalInSqrFeet.Value.ToString("#,##0") + " s.f.";
                }

                else
                {
                    this.lblTotal.Text = "N/A";
                }
               
                this.lblNet.Text = netTotalInSqrFeet.ToString("#,##0") + " s.f.";
            }

            else
            {
                if (grsTotalInSqrFeet.HasValue)
                {
                    this.lblTotal.Text = (grsTotalInSqrFeet.Value / 9.0).ToString("#,##0") + " s.y.";
                }

                else
                {
                    this.lblTotal.Text = "N/A";
                }

                this.lblNet.Text = (netTotalInSqrFeet / 9.0).ToString("#,##0") + " s.y.";
            }

            if (wastePct.HasValue)
            {
                if (wastePct < 0)
                {
                    this.lblWastePct.ForeColor = Color.Red;
                }

                else
                {
                    this.lblWastePct.ForeColor = Color.Black;
                }

                this.lblWastePct.Text = (100.0 * wastePct).Value.ToString("0.00");
            }

            else
            {
                this.lblWastePct.Text = string.Empty;
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

        private string ToStringFeet(string separator, bool separateUnits, bool scaleHasBeenSet)
        {
            if (!scaleHasBeenSet)
            {
                if (separateUnits)
                {
                    return " " + separator + "s.f." + separator + AreaFinishBase.AreaName + separator + "Non Roll" + separator + " " + separator + "s.f." + separator + " " + separator + "%";
                }

                else
                {
                    return " s.f." + separator + AreaFinishBase.AreaName + separator + "Non Roll" + separator + " s.f." + separator + " %";
                }
            }

            double netTotalInSqrFeet = AreaFinishBase.NetAreaInSqrInches / 144.0;
            double? grsTotalInSqrFeet = AreaFinishBase.TileAreaGrossAreaInSqrInches / 144.0;

            string total;
            string netTotal;

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
            }

            else
            {
                total += ' ';
                netTotal += ' ';
                waste += ' ';
            }

            total += "s.f.";
            netTotal += "s.f.";
            waste += "%";

            string rtrnString = total + separator + AreaFinishBase.AreaName + separator + "Non Roll" + separator + netTotal + separator + waste;

            return rtrnString;
        }

        private string ToStringYards(string separator, bool separateUnits, bool scaleHasBeenSet)
        {
            if (!scaleHasBeenSet)
            {
                if (separateUnits)
                {
                    return " " + separator + "s.y." + separator + AreaFinishBase.AreaName + separator + "Non Roll" + separator + " " + separator + "s.y." + separator + " " + separator + "%";
                }

                else
                {
                    return " s.y." + separator + AreaFinishBase.AreaName + separator + "Non Roll" + separator + " s.y." + separator + " %";
                }
            }

            double netTotalInSqrFeet = AreaFinishBase.NetAreaInSqrInches / 144.0;
            double? grsTotalInSqrFeet = AreaFinishBase.TileAreaGrossAreaInSqrInches / 144.0;

            string total;
            string netTotal;

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
                 
            }

            else
            {
                total += ' ';
                netTotal += ' ';
            }

            total += "s.y.";
            netTotal += "s.y.";


            string rtrnString = total + separator + AreaFinishBase.AreaName + separator + "Non Roll" + separator + netTotal + separator + waste ;

            return rtrnString;

        }
    }
}
