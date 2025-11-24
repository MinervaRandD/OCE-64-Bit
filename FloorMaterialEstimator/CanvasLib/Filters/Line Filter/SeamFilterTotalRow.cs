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

namespace CanvasLib.Filters.Line_Filter
{
    public partial class SeamFilterTotalRow : UserControl
    {
        public SeamFilterTotalRow()
        {
            InitializeComponent();
        }


        internal void Update(SeamFinishBaseList seamFinishBaseList)
        {
            double lengthInFeet = seamFinishBaseList.Where(s => !s.Filtered).Sum(s => s.LengthInInches) / 12.0;

            this.lblTotal.Text = lengthInFeet.ToString("#,##0.0") + " l.f.";
        }
    }
}
