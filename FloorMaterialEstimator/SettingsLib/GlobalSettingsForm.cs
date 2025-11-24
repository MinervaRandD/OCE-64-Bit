//--------------------------------------------------------------------------------//
// <copyright file="GlobalSettingsForm.cs" company="Bruun Estimating, LLC">      // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace SettingsLib
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Graphics;

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

            if (GlobalSettings.LineDrawoutModeSetting == LineDrawoutMode.ShowNormalDrawout)
            {
                this.rbnShowLineDrawout.Checked = true;
            }

            else
            {
                this.rbnHideLineDrawout.Checked = true;
            }

            this.ckbSnapToAxis.Checked = GlobalSettings.SnapToAxis;

            this.txbSnapToAxisResolutionInDegrees.Text = GlobalSettings.SnapResolutionInDegrees.ToString();

            this.ckbShowMarker.Checked = GlobalSettings.ShowMarker;
            this.ckbShowGuides.Checked = GlobalSettings.ShowGuides;
            this.ckbKeepInitialGuideOnCanvas.Checked = GlobalSettings.KeepInitialGuideOnCanvas;
            this.ckbKeepAllGuidesOnCanvas.Checked = GlobalSettings.KeepAllGuidesOnCanvas;

            this.ckbShowGrid.Checked = GlobalSettings.ShowGrid;
            this.ckbShowRulers.Checked = GlobalSettings.ShowRulers;

            this.txbMarkerWidth.Text = GlobalSettings.MarkerWidth.ToString();

            this.txbSnapToAxisResolutionInDegrees.TextChanged += TxbSnapToAxisResolutionInDegrees_TextChanged;
           

            this.ckbShowAreaEditFormAsModal.Checked = GlobalSettings.ShowAreaEditFormAsModal;
            this.ckbShowLineEditFormAsModal.Checked = GlobalSettings.ShowLineEditFormAsModal;
            this.ckbShowSeamEditFormAsModal.Checked = GlobalSettings.ShowSeamEditFormAsModal;

            this.txbMinOverWidthInInches.Text = GlobalSettings.MinOverageWidthInInches.ToString() ;
            this.txbMinOverLengthInInches.Text = GlobalSettings.MinOverageLengthInInches.ToString();
            this.txbMinUnderWidthInInches.Text = GlobalSettings.MinUnderageWidthInInches.ToString();
            this.txbMinUnderLengthInInches.Text = GlobalSettings.MinUnderageLengthInInches.ToString();
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
            
        }

        private void TxbYGridLineCount_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sResolution = this.txbSnapToAxisResolutionInDegrees.Text.Trim();

            if (!Utilities.Utilities.IsValidDbl(sResolution))
            {
                MessageBox.Show("The snap resolution is invalid");
                return;
            }

            string sMinOverWidthInInches = this.txbMinOverWidthInInches.Text.Trim();

            if (!Utilities.Utilities.IsValidInt(sMinOverWidthInInches))
            {
                MessageBox.Show("The minimum overage width is invalid");
                return;
            }

            string sMinOverLengthInInches = this.txbMinOverLengthInInches.Text.Trim();

            if (!Utilities.Utilities.IsValidInt(sMinOverLengthInInches))
            {
                MessageBox.Show("The minimum overage length is invalid");
                return;
            }

            string sMinUnderWidthInInches = this.txbMinUnderWidthInInches.Text.Trim();

            if (!Utilities.Utilities.IsValidInt(sMinUnderWidthInInches))
            {
                MessageBox.Show("The minimum underage width is invalid");
                return;
            }

            string sMinUnderLengthInInches = this.txbMinUnderLengthInInches.Text.Trim();

            if (!Utilities.Utilities.IsValidInt(sMinUnderLengthInInches))
            {
                MessageBox.Show("The minimum underage length is invalid");
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
