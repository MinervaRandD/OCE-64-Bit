

using Globals;

namespace CanvasLib.Filters.Line_Filter
{

    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using FinishesLib;

    public partial class ScaleNotSetWarningRow : UserControl
    {
        public ScaleNotSetWarningRow()
        {
            InitializeComponent();
        }

        internal void Update(LineFinishBaseList lineFinishBaseList)
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                this.lblScaleNotSet.Text = string.Empty;

                return;
            }

            double lengthInFeet = lineFinishBaseList.Where(l => !l.Filtered).Sum(a => a.LengthInInches) / 12.0;

            this.lblScaleNotSet.Text = lengthInFeet.ToString("#,##0.0") + " l.f.";
        }
    }
}
