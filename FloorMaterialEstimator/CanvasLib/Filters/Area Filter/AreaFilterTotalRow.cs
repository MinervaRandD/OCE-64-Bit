using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinishesLib;
using Globals;

namespace CanvasLib.Filters.Area_Filter
{
    public partial class AreaFilterTotalRow : UserControl
    {
        public AreaFilterTotalRow()
        {
            InitializeComponent();

            this.Dock = DockStyle.None;
            this.Anchor = AnchorStyles.None;
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;
        }

        internal void Update(AreaFinishBaseList areaFinishBaseList)
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                this.lblGross.Text = string.Empty;
                this.lblNet.Text = string.Empty;
                this.lblWastePct.Text = string.Empty;

                return;
            }

            double netTotalInSqrFeet = 0;
            double wasteTotal = 0;

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                if (areaFinishBase.Filtered)
                {
                    continue;
                }

                netTotalInSqrFeet += areaFinishBase.NetAreaInSqrInches / 144.0;
                
                if (areaFinishBase.Waste.HasValue)
                {
                    wasteTotal += areaFinishBase.Waste.Value / 144.0;
                }
                
            }

            double grossTotalInSqrFeet = netTotalInSqrFeet + wasteTotal;

            double wghtPct = 0.0;

            if (netTotalInSqrFeet > 0.0)
            {
                wghtPct = 100.0 * wasteTotal / netTotalInSqrFeet;
            }

            this.lblGross.Text = grossTotalInSqrFeet.ToString("#,##0.0") + " s.f.";
            this.lblNet.Text = netTotalInSqrFeet.ToString("#,##0.0") + " s.f.";
            this.lblWastePct.Text = wghtPct.ToString("0.00");
        }
    }
}
