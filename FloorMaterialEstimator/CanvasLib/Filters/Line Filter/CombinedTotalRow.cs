

namespace CanvasLib.Filters.Line_Filter
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using FinishesLib;

    public partial class CombinedTotalRow : UserControl
    {
        public CombinedTotalRow()
        {
            InitializeComponent();
        }

        internal void Update(LineFinishBaseList lineFinishBaseList, SeamFinishBaseList seamFinishBaseList)
        {
            double lengthInFeet = 0;

            if (lineFinishBaseList != null)
            {
                lengthInFeet += lineFinishBaseList.Where(l => !l.Filtered).Sum(a => a.LengthInInches) / 12.0;
            }

            if (seamFinishBaseList != null)
            {
                lengthInFeet += seamFinishBaseList.Where(s => !s.Filtered).Sum(a => a.LengthInInches) / 12.0;
            }

            this.lblTotal.Text = lengthInFeet.ToString("#,##0.0") + " l.f.";
        }
    }
}
