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
    public partial class UCToolTipRadioButtonItem : UserControl
    {
        RadioButton radioButton;

        public UCToolTipRadioButtonItem()
        {
            InitializeComponent();
        }

        internal void Init(RadioButton radioButton, ToolTip toolTip)
        {
            this.radioButton = radioButton;

            this.lblRadioButtonText.Text = radioButton.Text;

            this.txbToolTip.Text = toolTip.GetToolTip(radioButton);
        }

        internal void UpdateToolTip(ToolTip toolTip)
        {
            toolTip.SetToolTip(radioButton, this.txbToolTip.Text.Trim());
        }
    }
}
