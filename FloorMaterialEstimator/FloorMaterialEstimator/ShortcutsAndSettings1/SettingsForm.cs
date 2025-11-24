

namespace FloorMaterialEstimator.ShortcutsAndSettings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void rbnNormalOperatingMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnHideLineDrawout.Checked)
            {
                GlobalSettings.OperatingModeSetting = OperatingMode.Normal;
            }
        }

        private void rbnDevelopmentOperatingMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnHideLineDrawout.Checked)
            {
                GlobalSettings.OperatingModeSetting = OperatingMode.Development;
            }
        }

        private void rbnShowLineDrawout_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnHideLineDrawout.Checked)
            {
                GlobalSettings.LineDrawoutModeSetting = LineDrawoutMode.ShowLineDrawout;
            }
        }

        private void rbnHideLineDrawout_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnHideLineDrawout.Checked)
            {
                GlobalSettings.LineDrawoutModeSetting = LineDrawoutMode.ShowLineDrawout;
            }
        }
    }
}
