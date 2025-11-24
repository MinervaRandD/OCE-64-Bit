

using System.Drawing;
using Globals;

namespace CanvasLib.Filters.Area_Filter
{
    using System;
    using System.Windows.Forms;
    using FinishesLib;
    using CanvasLib.Filters;

    public partial class AreaFilterRow : UserControl
    {
        private AreaFinishBase areaFinishBase;

        private AreaFilterForm baseForm;

        private double netTotalInSqrFeet = 0;
        
        private double? grsTotalInSqrFeet = 0;
        
        public AreaFilterRow()
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
            this.Anchor = AnchorStyles.None;
            this.AutoSize = false;
        }

        public void Init(AreaFilterForm baseForm, AreaFinishBase areaFinishBase)
        {
            this.baseForm = baseForm;
            this.areaFinishBase = areaFinishBase;

            this.ckbFinishFilter.Checked = !this.areaFinishBase.Filtered;
            this.pnlColor.BackColor = this.areaFinishBase.Color;
            this.lblTag.Text = this.areaFinishBase.AreaName;

            updateStatsDisplay();

            areaFinishBase.FinishStatsUpdated += AreaFinishBase_FinishStatsUpdated;
            //areaFinishBase.WastePercentChanged += AreaFinishBase_WastePercentChanged;
            areaFinishBase.ColorChanged += AreaFinishBase_ColorChanged;
            areaFinishBase.AreaNameChanged += AreaFinishBase_AreaNameChanged;
        }

        private void AreaFinishBase_FinishStatsUpdated(AreaFinishBase finishBase, int count, double netAreaInSqrInches, double? grossAreaInSqrInches, double perimeterInInches, double? wasteRatio)
        {
            updateStatsDisplay();
            baseForm.UpdateTotals();
        }

        private void AreaFinishBase_ColorChanged(AreaFinishBase finishBase, System.Drawing.Color color)
        {
            this.pnlColor.BackColor = this.areaFinishBase.Color;
        }

        private void AreaFinishBase_AreaNameChanged(AreaFinishBase finishBase, string areaName)
        {
            this.lblTag.Text = areaFinishBase.AreaName;
        }

        bool firstClick = true;

        private void ckbFinishFilter_CheckedChanged(object sender, System.EventArgs e)
        {
            this.areaFinishBase.Filtered = !this.ckbFinishFilter.Checked;

            //if (firstClick)
            //{
            //    // Some weird bug requires this.
            //    this.areaFinishBase.Filtered = this.ckbFinishFilter.Checked;
            //    this.areaFinishBase.Filtered = !this.ckbFinishFilter.Checked;

            //    firstClick = false; 
            //}
        }

        private void updateStatsDisplay()
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                this.lblGross.Text = string.Empty;
                this.lblNet.Text = string.Empty;
                this.lblWastePct.Text = string.Empty;

                return;
            }

            netTotalInSqrFeet = areaFinishBase.NetAreaInSqrInches / 144.0;
            grsTotalInSqrFeet = areaFinishBase.GrossAreaInSqrInches / 144.0;
            
            double? wghtPct = null;

            if (netTotalInSqrFeet > 0.0 && grsTotalInSqrFeet.HasValue)
            {
                wghtPct = (grsTotalInSqrFeet.Value / netTotalInSqrFeet - 1.0) * 100.0;
            }

            if (grsTotalInSqrFeet.HasValue)
            {
                this.lblGross.Text = grsTotalInSqrFeet.Value.ToString("#,##0.0") + " s.f.";
            }
            
            else
            {
                this.lblGross.Text = "N/A";
            }

            this.lblNet.Text = netTotalInSqrFeet.ToString("#,##0.0") + " s.f.";
            
            if (wghtPct.HasValue)
            {
                this.lblWastePct.Text = wghtPct.Value.ToString("0.00");
            }
            
            else
            {
                this.lblWastePct.Text = "N/A";
            }

            if (baseForm.BtnGTZero.BackColor == Color.Orange)
            {
                SetFiltered(FilteredState.ShowGT0);
            }
        }

        internal void SetFiltered(FilteredState filteredState)
        {
            if (filteredState == FilteredState.ShowNone)
            {
                if (this.ckbFinishFilter.Checked)
                {
                    this.ckbFinishFilter.Checked = false;
                }

                return;
            }

            if (filteredState == FilteredState.ShowAll)
            {
                if (!this.ckbFinishFilter.Checked)
                {
                    this.ckbFinishFilter.Checked = true;
                }

                return;
            }

            if (filteredState == FilteredState.ShowGT0)
            {
                if (netTotalInSqrFeet > 0)
                {
                    if (!this.ckbFinishFilter.Checked)
                    {
                        this.ckbFinishFilter.Checked = true;
                    }
                }

                else
                {
                    if (this.ckbFinishFilter.Checked)
                    {
                        this.ckbFinishFilter.Checked = false;
                    }
                }
            }
        }
    }

}
