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
    public partial class UCToolTipButtonItem : UserControl
    {
        Button button;

        public UCToolTipButtonItem()
        {
            InitializeComponent();
        }

        internal void Init(Button button, ToolTip toolTip)
        {
            this.button = button;

            this.btnToolTipButtonItem.Text = button.Text;

            this.txbToolTip.Text = toolTip.GetToolTip(button);

            this.btnToolTipButtonItem.Enabled = false;

            this.btnToolTipButtonItem.BackColor = SystemColors.ControlLightLight;

            this.btnToolTipButtonItem.FlatStyle = FlatStyle.Standard;
        }

        internal void UpdateToolTip(ToolTip toolTip)
        {
            toolTip.SetToolTip(button, this.txbToolTip.Text.Trim());
        }
    }
}
