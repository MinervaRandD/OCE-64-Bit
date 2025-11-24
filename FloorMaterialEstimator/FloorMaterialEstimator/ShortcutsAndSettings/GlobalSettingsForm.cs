using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator.ShortcutsAndSettings
{
    public partial class GlobalSettingsForm : Form
    {
        public GlobalSettingsForm()
        {
            InitializeComponent();

            if (GlobalSettings.OperatingModeSetting == OperatingMode.Normal)
            {
                this.rbnNormalOperatingMode.Checked = true;
            }

            else
            {
                this.rbnDevelopmentOperatingMode.Checked = true;
            }

            if (GlobalSettings.LineDrawoutModeSetting == LineDrawoutMode.ShowLineDrawout)
            {
                this.rbnShowLineDrawout.Checked = true;
            }

            else
            {
                this.rbnHideLineDrawout.Checked = true;
            }

            this.ckbSnapToAxis.Checked = GlobalSettings.SnapToAxisSetting;

            this.txbSnapToAxisResolutionInDegrees.Text = GlobalSettings.SnapResolutionInDegrees.ToString();

            this.txbGridLineOffset.Text = GlobalSettings.GridlineOffsetSetting.ToString();
            this.txbYGridLineCount.Text = GlobalSettings.YGridlineCountSetting.ToString();

            this.txbSnapToAxisResolutionInDegrees.TextChanged += TxbSnapToAxisResolutionInDegrees_TextChanged;
            this.txbGridLineOffset.TextChanged += TxbGridLineOffset_TextChanged;
            this.txbYGridLineCount.TextChanged += TxbYGridLineCount_TextChanged;
        }

        private void TxbSnapToAxisResolutionInDegrees_TextChanged(object sender, EventArgs e)
        {
            string sResolution = this.txbSnapToAxisResolutionInDegrees.Text.Trim();

            if (!Utilities.Utilities.IsValidDbl(sResolution))
            {
                this.txbSnapToAxisResolutionInDegrees.BackColor = Color.Pink;
            }

            else
            {
                this.txbSnapToAxisResolutionInDegrees.BackColor = Color.White;
            }
        }

        private void TxbGridLineOffset_TextChanged(object sender, EventArgs e)
        {
            string sGridLineOffset = this.txbGridLineOffset.Text.Trim();

            if (!Utilities.Utilities.IsValidDbl(sGridLineOffset))
            {
                this.txbGridLineOffset.BackColor = Color.Pink;
            }

            else
            {
                this.txbGridLineOffset.BackColor = Color.White;
            }
        }

        private void TxbYGridLineCount_TextChanged(object sender, EventArgs e)
        {
            string sGridLineCount = this.txbYGridLineCount.Text.Trim();

            if (!Utilities.Utilities.IsValidInt(sGridLineCount))
            {
                this.txbYGridLineCount.BackColor = Color.Pink;
            }

            else
            {
                this.txbYGridLineCount.BackColor = Color.White;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sResolution = this.txbSnapToAxisResolutionInDegrees.Text.Trim();

            if (!Utilities.Utilities.IsValidDbl(sResolution))
            {
                MessageBox.Show("The snap resolution is invalid");
                return;
            }

            string sGridLineOffset = this.txbGridLineOffset.Text.Trim();

            if (!Utilities.Utilities.IsValidDbl(sGridLineOffset))
            {
                MessageBox.Show("The grid line offset is invalid");
                return;
            }

            string sGridLineCount = this.txbYGridLineCount.Text.Trim();

            if (!Utilities.Utilities.IsValidInt(sGridLineCount))
            {
                MessageBox.Show("The grid line count is invalid");
                return;
            }

            this.DialogResult = DialogResult.OK;
 
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
