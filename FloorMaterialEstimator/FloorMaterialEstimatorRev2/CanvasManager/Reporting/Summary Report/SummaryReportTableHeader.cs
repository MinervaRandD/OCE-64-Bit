using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasLib.Filters.Area_Filter
{
    public partial class AreaFilterTitleRow : UserControl
    {
        public AreaFilterTitleRow()
        {
            InitializeComponent();

            this.Dock = DockStyle.None;
            this.Anchor = AnchorStyles.None;
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;
        }
    }
}
