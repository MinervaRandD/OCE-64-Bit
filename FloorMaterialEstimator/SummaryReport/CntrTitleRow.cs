

namespace SummaryReport
{
    using System;
    using System.Windows.Forms;
    using FinishesLib;
    using CanvasLib;
    using System.Drawing;
    using CanvasLib.Counters;

    public partial class CntrTitleRow : UserControl, IDisposable, IReportRow
    {
        Counter counter;


        int ckbFinishFilterSizeX => RowColumnWidths.CkbFinishFilterSizeX;
        int pnlColorSizeX => RowColumnWidths.PnlColorSizeX;
        int lblTagSizeX => RowColumnWidths.CounterTagSizeX;
        int lblCountSizeX => RowColumnWidths.CounterCountSizeX;
        int lblDescriptionSizeX => RowColumnWidths.CounterDescriptionSizeX;

        int lblSizeSizeX = RowColumnWidths.CounterSizeSizeX;
        
        int lblTotalSizeX = RowColumnWidths.CounterTotalSizeX;

        //public bool Selected => this.ckbFinishFilter.Checked;

        //public void SetSelectionStatus(bool selectionStatus) => this.ckbFinishFilter.Checked = selectionStatus;

        public string Tag => this.counter.Tag.ToString();

        public int Count => counter.Count;

        public ReportRowType ReportRowType => ReportRowType.Counter;

        public int LocationOnReport { get; set; }

        public UserControl ControlBase => this;

        public int Index { get; set; }

        public event ReportRowChangedHandler ReportRowChanged;

        bool IReportRow.HasMeasurement { get { return counter.Count > 0; } }

        public string Guid => throw new NotImplementedException();

        public bool Selected => throw new NotImplementedException();

        public CntrTitleRow()
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
            this.Anchor = AnchorStyles.None;
            this.AutoSize = false;

            initializeControls();

            this.Disposed += CounterRow_Disposed;
        }


        private void initializeControls()
        {
            // Set up element sizes

            int cntlSizeY = 21;

            int locX = 1;
            int locY = 1;

            // Check box

            //this.ckbFinishFilter.Size = new Size(ckbFinishFilterSizeX, cntlSizeY);
            //this.ckbFinishFilter.Location = new Point(locX, locY);

            locX += ckbFinishFilterSizeX;

            // Color panel

            //this.pnlColor.Size = new Size(pnlColorSizeX, cntlSizeY);
            //this.pnlColor.Location = new Point(locX, locY);

            locX += pnlColorSizeX;

            // Tag

            this.lblTag.Size = new Size(lblTagSizeX, cntlSizeY);
            this.lblTag.Location = new Point(locX, locY);

            locX += lblTagSizeX;

            // Count

            this.lblCount.Size = new Size(lblCountSizeX, cntlSizeY);
            this.lblCount.Location = new Point(locX, locY);

            locX += lblCountSizeX;

            // Description

            this.lblDescription.Size = new Size(lblDescriptionSizeX, cntlSizeY);
            this.lblDescription.Location = new Point(locX, locY);

            locX += lblDescriptionSizeX;

            // Size

            this.lblSize.Size = new Size(lblSizeSizeX, cntlSizeY);
            this.lblSize.Location = new Point(locX, locY);

            locX += lblSizeSizeX;

            // Total

            this.lblTotal.Size = new Size(lblTotalSizeX, cntlSizeY);
            this.lblTotal.Location = new Point(locX, locY);

            locX += lblTotalSizeX;


            this.Size = new Size(locX, cntlSizeY);
        }

#if false
        public void Init( Counter counter)
        {
            this.counter = counter;

            this.pnlColor.BackColor = this.counter.Color;
            this.lblTag.Text = this.counter.Tag.ToString();
            this.lblCount.Text = this.counter.Count.ToString();
            this.lblDescription.Text = this.counter.Description;
            this.lblSize.Text = this.counter.Size.ToString("#,##0.0");
            this.lblTotal.Text = (this.counter.Count * this.counter.Size).ToString("#,##0.0");

            this.counter.CounterColorChanged += Counter_CounterColorChanged;
            this.counter.CounterSizeChanged += Counter_CounterSizeChanged;
        }
#endif

        public void Delete()
        {
            this.Dispose();
        }

#if false
        private void Dispose(object sender, EventArgs e)
        {

            this.counter.CounterColorChanged -= Counter_CounterColorChanged;
            this.counter.CounterSizeChanged -= Counter_CounterSizeChanged;
        }
#endif
        private void CounterRow_Disposed(object sender, EventArgs e)
        {
            
        }

        //private void Counter_CounterSizeChanged(Counter counter, CounterSize counterSize)
        //{
        //    this.lblSize.Text = this.counter.Size.ToString("#,##0.0");
        //    this.lblTotal.Text = (this.counter.Count * this.counter.Size).ToString("#,##0.0");
        //}

        //private void Counter_CounterColorChanged(Counter counter, Color color)
        //{
        //    this.pnlColor.BackColor = color;
        //}

        //private void NotifyRowChanged()
        //{
        //    if (ReportRowChanged != null)
        //    {
        //        ReportRowChanged(this);
        //    }
        //}

        //public void UpdateStatsDisplay()
        //{
        //    this.pnlColor.BackColor = this.counter.Color;
        //    this.lblTag.Text = this.counter.Tag.ToString();
        //    this.lblCount.Text = this.counter.Count.ToString();
        //    this.lblDescription.Text = this.counter.Description;
        //    this.lblSize.Text = this.counter.Size.ToString("#,##0.0");
        //    this.lblTotal.Text = (this.counter.Count * this.counter.Size).ToString("#,##0.0");

        //    NotifyRowChanged();
        //}

        //private void ckbFinishFilter_CheckedChanged(object sender, EventArgs e)
        //{
        //    NotifyRowChanged();
        //}

        internal string ToString(string separator, bool separateUnits = true, string convert = "", bool scaleHasBeenSet = true)
        {
            string rtrnStr = string.Empty;

            rtrnStr += counter.Tag.ToString() + separator
                + counter.Count + separator;
            //if (convert == "feet")
            //{
            //    rtrnStr = ToStringFeet(separator, separateUnits, scaleHasBeenSet);

            //    return rtrnStr;
            //}

            //if (convert == "yards")
            //{
            //    rtrnStr = ToStringYards(separator, separateUnits, scaleHasBeenSet);

            //    return rtrnStr;
            //}

            //if (this.areaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareFeet)
            //{
            //    rtrnStr = ToStringFeet(separator, separateUnits, scaleHasBeenSet);

            //    return rtrnStr;
            //}

            //if (this.areaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareYards)
            //{
            //    rtrnStr = ToStringYards(separator, separateUnits, scaleHasBeenSet);

            //    return rtrnStr;
            //}

            return string.Empty;
        }

#if false
        private string ToStringFeet(string separator, bool separateUnits, bool scaleHasBeenSet)
        {
            if (!scaleHasBeenSet)
            {
                if (separateUnits)
                {
                    return " " + separator + "s.f." + separator + areaFinishBase.AreaName + separator + "Non Roll" + separator + " " + separator + "s.f." + separator + " " + separator + "%";
                }

                else
                {
                    return " s.f." + separator + areaFinishBase.AreaName + separator + "Non Roll" + separator + " s.f." + separator + " %";
                }
            }

            double netTotalInSqrFeet = areaFinishBase.NetAreaInSqrInches / 144.0;
            double grsTotalInSqrFeet = areaFinishBase.TileAreaGrossAreaInSqrInches / 144.0;

            string total;
            string netTotal;

            string waste;

            total = (grsTotalInSqrFeet + areaFinishBase.WRepeatsInSqrFeet + areaFinishBase.LRepeatsInSqrFeet).ToString("#,##0");
            netTotal = netTotalInSqrFeet.ToString("#,##0");

            if (netTotalInSqrFeet > 0.0)
            {
                waste = ((grsTotalInSqrFeet / netTotalInSqrFeet - 1.0) * 100.0).ToString("0.00") + "%";
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

            string rtrnString = total + separator + areaFinishBase.AreaName + separator + "Non Roll" + separator + netTotal + separator + waste;

            return rtrnString;
        }

        private string ToStringYards(string separator, bool separateUnits, bool scaleHasBeenSet)
        {
            if (!scaleHasBeenSet)
            {
                if (separateUnits)
                {
                    return " " + separator + "s.y." + separator + areaFinishBase.AreaName + separator + "Non Roll" + separator + " " + separator + "s.y." + separator + " " + separator + "%";
                }

                else
                {
                    return " s.y." + separator + areaFinishBase.AreaName + separator + "Non Roll" + separator + " s.y." + separator + " %";
                }
            }

            double netTotalInSqrFeet = areaFinishBase.NetAreaInSqrInches / 144.0;
            double grsTotalInSqrFeet = areaFinishBase.TileAreaGrossAreaInSqrInches / 144.0;

            string total;
            string netTotal;

            string waste;

            total = ((grsTotalInSqrFeet + areaFinishBase.WRepeatsInSqrFeet + areaFinishBase.LRepeatsInSqrFeet) / 9.0).ToString("#,##0");
            netTotal = (netTotalInSqrFeet / 9.0).ToString("#,##0");
               
            if (netTotalInSqrFeet > 0.0)
            {
                waste = ((grsTotalInSqrFeet / netTotalInSqrFeet - 1.0) * 100.0).ToString("0.00") + "%";
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


            string rtrnString = total + separator + areaFinishBase.AreaName + separator + "Non Roll" + separator + netTotal + separator + waste ;

            return rtrnString;

        }
#endif
        public void SetSelectionStatus(bool selectionStatus)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatsDisplay(bool scaleHasBeenSet)
        {
            throw new NotImplementedException();
        }
    }
}
