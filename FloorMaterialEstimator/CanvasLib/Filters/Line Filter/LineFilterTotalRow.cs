

using Globals;

namespace CanvasLib.Filters.Line_Filter
{

    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using FinishesLib;

    public partial class LineFilterTotalRow : UserControl
    {
        public LineFilterTotalRow()
        {
            InitializeComponent();
        }

        internal void Update(LineFinishBaseList lineFinishBaseList)
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                this.lblTotal.Text = string.Empty;

                return;
            }

            double lengthInFeet = lineFinishBaseList.Where(l => !l.Filtered).Sum(a => a.LengthInInches) / 12.0;

            this.lblTotal.Text = lengthInFeet.ToString("#,##0.0") + " l.f.";
        }
    }
}
