using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingsLib
{
    public partial class UCToolTipCheckBoxItem : UserControl
    {
        CheckBox checkBox;

        public UCToolTipCheckBoxItem()
        {
            InitializeComponent();
        }

        internal void Init(CheckBox checkBox, ToolTip toolTip)
        {
            this.checkBox = checkBox;

            this.lblCheckBoxText.Text = checkBox.Text;

            this.txbToolTip.Text = toolTip.GetToolTip(checkBox);
        }

        internal void UpdateToolTip(ToolTip toolTip)
        {
            toolTip.SetToolTip(checkBox, this.txbToolTip.Text.Trim());
        }
    }
}
