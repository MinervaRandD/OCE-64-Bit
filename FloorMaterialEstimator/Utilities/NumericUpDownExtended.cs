using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilities
{
    public partial class NumericUpDownExtended : NumericUpDown
    {
        private string format = "{0}";

        public NumericUpDownExtended()
        {
            InitializeComponent();
        }

        public void Init(string format)
        {
            this.format = format;

            this.Text = string.Format(format, 0);
        }

        protected override void UpdateEditText()
        {
            this.Text = string.Format(format, this.Value);
          
        }
    }
}
