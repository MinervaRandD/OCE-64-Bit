using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class CustomNumericUpDown: NumericUpDown
    {
        protected override void UpdateEditText()
        {
            try
            {
                this.Text = this.Value.ToString() + " % ";
            }

            catch
            {

            }
        }
    }
}
