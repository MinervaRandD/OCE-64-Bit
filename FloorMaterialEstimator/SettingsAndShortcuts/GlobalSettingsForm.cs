//-------------------------------------------------------------------------------//
// <copyright file="GlobalSettingsForm.cs"                                       //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
//-------------------------------------------------------------------------------//

namespace SettingsLib
{
    using System;
    using System.Configuration;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;
    using Utilities;
    using TracerLib;

    public partial class GlobalSettingsForm : Form, ICursorManagementForm
    {
        ToolStripButton btnShowToolTipEditor = null;

        Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

        public GlobalSettingsForm()
        {
            InitializeComponent();
        }

        public void Init()
        {
            ckbKeystrokes.Checked =Convert.ToBoolean(config.AppSettings.Settings["keystrokes"].Value);

            AddToCursorManagementList();

            this.FormClosed += FinishesEditForm_FormClosed;

            if (GlobalSettings.InitProjectDesignState == 1)
            {
                this.rbnAreaModeInitialDesignState.Checked = true;
            }

            else
            {
                this.rbnLineModeInitialDesignState.Checked = true;
            }

            this.ckbStartupFullScreenMode.Checked = GlobalSettings.StartupFullScreenMode;

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
            this.txbSnapDistance.Text = GlobalSettings.SnapDistance.ToString("0.00");

            this.ckbShowMarker.Checked = GlobalSettings.ShowMarker;
            this.ckbShowGuides.Checked = GlobalSettings.ShowGuides;
            this.ckbKeepInitialGuideOnCanvas.Checked = GlobalSettings.KeepInitialGuideOnCanvas;
            this.ckbKeepAllGuidesOnCanvas.Checked = GlobalSettings.KeepAllGuidesOnCanvas;

            this.ckbShowGrid.Checked = GlobalSettings.ShowGrid;
            this.ckbShowRulers.Checked = GlobalSettings.ShowRulers;
            this.ckbShowPanAndZoom.Checked = GlobalSettings.ShowPanAndZoom;

            this.txbMarkerWidth.Text = GlobalSettings.MarkerWidth.ToString();

            this.txbSnapToAxisResolutionInDegrees.TextChanged += TxbSnapToAxisResolutionInDegrees_TextChanged;
            this.txbSnapDistance.TextChanged += TxbSnapDistance_TextChanged;

            this.ckbShowAreaEditFormAsModal.Checked = GlobalSettings.ShowAreaEditFormAsModal;
            this.ckbShowLineEditFormAsModal.Checked = GlobalSettings.ShowLineEditFormAsModal;
            this.ckbShowSeamEditFormAsModal.Checked = GlobalSettings.ShowSeamEditFormAsModal;

            this.ckbShowAreaFilterFormAsModal.Checked = GlobalSettings.ShowAreaFilterFormAsModal;
            this.ckbShowLineFilterFormAsModal.Checked = GlobalSettings.ShowLineFilterFormAsModal;

            #region Minimums for overs and unders

            this.txbMinOverWidthInInches.Text = GlobalSettings.MinOverageWidthInInches.ToString();
            this.txbMinOverLengthInInches.Text = GlobalSettings.MinOverageLengthInInches.ToString();
            this.txbMinUnderWidthInInches.Text = GlobalSettings.MinUnderageWidthInInches.ToString();
            this.txbMinUnderLengthInInches.Text = GlobalSettings.MinUnderageLengthInInches.ToString();

            this.txbMinOverWidthForCombosInInches.Text = GlobalSettings.MinOverComboWidthInInches.ToString();
            this.txbMinOverLengthForCombosInInches.Text = GlobalSettings.MinOverComboLengthInInches.ToString();
            this.txbMinUnderWidthForCombosInInches.Text = GlobalSettings.MinUnderComboWidthInInches.ToString();
            this.txbMinUnderLengthForCombosInInches.Text = GlobalSettings.MinUnderComboLengthInInches.ToString();

            #endregion

            this.txbAutosaveIntervalInSeconds.Text = GlobalSettings.AutosaveIntervalInSeconds.ToString();
            this.ckbAutosaveEnabled.Checked = GlobalSettings.AutosaveEnabled;
            this.ckbValidateOnProjectSave.Checked = GlobalSettings.ValidateOnProjectSave;

            this.ckbAutoUpdateSeamsAndCuts.Checked = GlobalSettings.AutoReseamAndCutOnRollWidthOrScaleChange;

            this.ckbLockScrollWhenDrawingSmallerThanCanvas.Checked = GlobalSettings.LockScrollWhenDrawingSmallerThanCanvas;

            this.ckbLockAreaWhen100PctSeamed.Checked = GlobalSettings.LockAreaWhen100PctSeamed;

            this.txbDefaultDrawingScaleInInches.Text = GlobalSettings.DefaultDrawingScaleInInches.ToString("#,##0.0");

            this.txbDefaultWidth.Text = GlobalSettings.DefaultNewDrawingWidthInInches.ToString("0.0");
            this.txbDefaultHeight.Text = GlobalSettings.DefaultNewDrawingHeightInInches.ToString("0.0");

            this.txbDefaultWidth.TextChanged += TxbDefaultWidth_TextChanged;
            this.txbDefaultHeight.TextChanged += TxbDefaultHeight_TextChanged;

            this.txbGraphicsPrecision.Text = GlobalSettings.GraphicsPrecision.ToString();
            this.txbGraphicsPrecision.TextChanged += TxbGraphicsPrecision_TextChanged;

            this.ckbShowSetScaleReminder.Checked = GlobalSettings.ShowSetScaleReminder;

            this.ckbAllowEditingOfShortcutKeys.Checked = GlobalSettings.AllowEditingOfShortcutKeys;

            this.ckbAllowEditingOfToolTips.Checked = GlobalSettings.AllowEditingOfToolTips;

            this.nudMouseWheelZoomInterval.Value = GlobalSettings.MouseWheelZoomInterval;

            this.nudZoomInOutButtonPercent.Value = GlobalSettings.ZoomInOutButtonPercent;

            this.nudArrowMoveIncrement.Value = GlobalSettings.ArrowMoveIncrement;

            this.rbnRightHanded.Checked = GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded;
            this.rbnLeftHanded.Checked = GlobalSettings.ShortcutOrientation == ShortCutOrientation.LeftHanded;

            this.ckbValidateRolloutAndCutWidths.Checked = GlobalSettings.ValidateRolloutAndCutWidth;
            this.ckbUpdateDebugFormDynamically.Checked = GlobalSettings.UpdateDebugFormDynamically;

            this.nmAreaNumbersFontsize.Value = (decimal)GlobalSettings.AreaIndexFontInPts;
            this.nmCutNumbersFontsize.Value = (decimal)GlobalSettings.CutIndexFontInPts;
            this.nmCutIndexCircleRadius.Value = (decimal)(Math.Min(100, GlobalSettings.CutIndexCircleRadius * 100.0));
            this.ckbShowCutIndexCircles.Checked = GlobalSettings.ShowCutIndexCircles;
            this.nmOverageNumbersFontsize.Value = (decimal)GlobalSettings.OverageIndexFontInPts;
            this.nmUnderageNumbersFontsize.Value = (decimal)GlobalSettings.UnderageIndexFontInPts;

            this.ckbTraceLevelInfo.Checked = (GlobalSettings.TraceLevel & TraceLevel.Info) != 0;
            this.ckbTraceLevelError.Checked = (GlobalSettings.TraceLevel & TraceLevel.Error) != 0;
            this.ckbTraceLevelException.Checked = (GlobalSettings.TraceLevel & TraceLevel.Exception) != 0;
            this.ckbTraceLevelMethodCall.Checked = (GlobalSettings.TraceLevel & TraceLevel.MethodCall) != 0;

            this.txbGridSpacingInInches.Text = GlobalSettings.GridSpacingInInches.ToString("0.00");
            this.txbGridOffsetInInches.Text = GlobalSettings.GridOffsetInInches.ToString("0.00");

            this.NudDefaultRollWidthFeet.Value = GlobalSettings.RollWidthDefaultValueFeet;
            this.NudDefaultRollWidthInches.Value = GlobalSettings.RollWidthDefaultValueInches;
            this.NudDefaultOverlapInches.Value = GlobalSettings.RollOverlapDefaultValueInches;
            this.NudExtraPerCutInches.Value = GlobalSettings.RollExtraPerCutDefaultValueInches;

            this.CkbShowAreaOutlinesInLineMode.Checked = GlobalSettings.ShowAreaOutlineInLineMode;

            setupCounterSettings();
        }

        private void TxbGraphicsPrecision_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidInt(this.txbGraphicsPrecision);
        }

        private void TxbDefaultWidth_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidPositiveDouble(this.txbDefaultWidth);
        }

        private void TxbDefaultHeight_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidPositiveDouble(this.txbDefaultHeight);
        }

        #region Counter Settings

        private void setupCounterSettings()
        {
            this.txbSmallCircleRadius.Text = GlobalSettings.CounterSmallCircleRadius.ToString("0.00");
            this.txbMediumCircleRadius.Text = GlobalSettings.CounterMediumCircleRadius.ToString("0.00");
            this.txbLargeCircleRadius.Text = GlobalSettings.CounterLargeCircleRadius.ToString("0.00");

            this.txbSmallFontSizeInPts.Text = GlobalSettings.CounterSmallFontInPts.ToString("0.0");
            this.txbMediumFontSizeInPts.Text = GlobalSettings.CounterMediumFontInPts.ToString("0.0");
            this.txbLargeFontSizeInPts.Text = GlobalSettings.CounterLargeFontInPts.ToString("0.0");

            this.txbSmallFontSizeInPts.BackColor = SystemColors.ControlLightLight;
            this.txbMediumFontSizeInPts.BackColor = SystemColors.ControlLightLight;
            this.txbLargeFontSizeInPts.BackColor = SystemColors.ControlLightLight;

            this.txbSmallCircleRadius.BackColor = SystemColors.ControlLightLight;
            this.txbSmallCircleRadius.BackColor = SystemColors.ControlLightLight;
            this.txbSmallCircleRadius.BackColor = SystemColors.ControlLightLight;

            this.txbSmallCircleRadius.TextChanged += CounterTextBoxTextChanged;
            this.txbMediumCircleRadius.TextChanged += CounterTextBoxTextChanged;
            this.txbLargeCircleRadius.TextChanged += CounterTextBoxTextChanged;

            this.txbSmallFontSizeInPts.TextChanged += CounterTextBoxTextChanged;
            this.txbMediumFontSizeInPts.TextChanged += CounterTextBoxTextChanged;
            this.txbLargeFontSizeInPts.TextChanged += CounterTextBoxTextChanged;
        }

        private void CounterTextBoxTextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidPositiveDouble((TextBox)sender);
        }

        #endregion

        private void TxbSnapToAxisResolutionInDegrees_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidPositiveDouble(this.txbSnapToAxisResolutionInDegrees);
        }

        private void TxbSnapDistance_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidPositiveDouble(this.txbSnapDistance);
        }

        private void TxbGridLineOffset_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TxbYGridLineCount_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txbGridSpacingInInches_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidPositiveDouble(txbGridSpacingInInches);
        }

        private void txbGridOffsetInInches_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidPositiveDouble(txbGridOffsetInInches);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sResolution = this.txbSnapToAxisResolutionInDegrees.Text.Trim();

            if (!Utilities.IsValidPosDbl(sResolution))
            {
                ManagedMessageBox.Show("The snap resolution is invalid");
                return;
            }

            sResolution = this.txbSnapDistance.Text.Trim();

            if (!Utilities.IsValidPosDbl(sResolution))
            {
                ManagedMessageBox.Show("The snap distance is invalid");
                return;
            }

            #region Overs and Unders Minimums

            string sMinOverWidthInInches = this.txbMinOverWidthInInches.Text.Trim();

            if (!Utilities.IsValidPosInt(sMinOverWidthInInches))
            {
                ManagedMessageBox.Show("The minimum overage width for system is invalid");
                return;
            }

            string sMinOverLengthInInches = this.txbMinOverLengthInInches.Text.Trim();

            if (!Utilities.IsValidPosInt(sMinOverLengthInInches))
            {
                ManagedMessageBox.Show("The minimum overage length for system is invalid");
                return;
            }

            string sMinUnderWidthInInches = this.txbMinUnderWidthInInches.Text.Trim();

            if (!Utilities.IsValidPosInt(sMinUnderWidthInInches))
            {
                ManagedMessageBox.Show("The minimum underage width for system is invalid");
                return;
            }

            string sMinUnderLengthInInches = this.txbMinUnderLengthInInches.Text.Trim();

            if (!Utilities.IsValidPosInt(sMinUnderLengthInInches))
            {
                ManagedMessageBox.Show("The minimum underage length for system is invalid");
                return;
            }

            string sMinComboOverWidthInInches = this.txbMinOverWidthForCombosInInches.Text.Trim();

            if (!Utilities.IsValidPosInt(sMinComboOverWidthInInches))
            {
                ManagedMessageBox.Show("The minimum overage width for combos is invalid");
                return;
            }

            string sMinComboOverLengthInInches = this.txbMinOverLengthForCombosInInches.Text.Trim();

            if (!Utilities.IsValidPosInt(sMinComboOverLengthInInches))
            {
                ManagedMessageBox.Show("The minimum overage length for combos is invalid");
                return;
            }

            string sMinComboUnderWidthInInches = this.txbMinUnderWidthForCombosInInches.Text.Trim();

            if (!Utilities.IsValidPosInt(sMinComboUnderWidthInInches))
            {
                ManagedMessageBox.Show("The minimum underage width for combos is invalid");
                return;
            }

            string sMinComboUnderLengthInInches = this.txbMinUnderLengthForCombosInInches.Text.Trim();

            if (!Utilities.IsValidPosInt(sMinComboUnderLengthInInches))
            {
                ManagedMessageBox.Show("The minimum underage length for combos is invalid");
                return;
            }

            #endregion

            if (!Utilities.CheckTextBoxValidPositiveDouble(this.txbSmallCircleRadius, "the small counter circle radius"))
            {
                return;
            }

            if (!Utilities.CheckTextBoxValidPositiveDouble(this.txbMediumCircleRadius, "the medium counter circle radius"))
            {
                return;
            }

            if (!Utilities.CheckTextBoxValidPositiveDouble(this.txbLargeCircleRadius, "the large counter circle radius"))
            {
                return;
            }

            if (!Utilities.CheckTextBoxValidPositiveDouble(this.txbSmallFontSizeInPts, "the small counter font"))
            {
                return;
            }

            if (!Utilities.CheckTextBoxValidPositiveDouble(this.txbMediumFontSizeInPts, "the medium counter font"))
            {
                return;
            }

            if (!Utilities.CheckTextBoxValidPositiveDouble(this.txbLargeFontSizeInPts, "the large counter font"))
            {
                return;
            }

            if (!Utilities.CheckTextBoxValidPositiveDouble(this.txbDefaultDrawingScaleInInches, "the default drawing scale in inches"))
            {
                return;
            }

            if (!Utilities.CheckTextBoxValidPositiveDouble(this.txbDefaultWidth, "the default drawing width in inches"))
            {
                return;
            }

            if (!Utilities.CheckTextBoxValidPositiveDouble(this.txbDefaultHeight, "the default drawing height in inches"))
            {
                return;
            }

            if (!Utilities.CheckTextBoxValidPositiveDouble(this.txbGridSpacingInInches, "the default grid spacing in inches"))
            {
                return;
            }


            if (!Utilities.CheckTextBoxValidPositiveDouble(this.txbGridOffsetInInches, "the default grid offset in inches"))
            {
                return;
            }


            if (!Utilities.CheckTextBoxValidInt(this.txbGraphicsPrecision, "the default graphics precision height"))
            {
                return;
            }

            #region Trace Level
            TraceLevel traceLevel = TraceLevel.None;

            if (this.ckbTraceLevelInfo.Checked)
            {
                traceLevel |= TraceLevel.Info;
            }

            if (this.ckbTraceLevelError.Checked)
            {
                traceLevel |= TraceLevel.Error;
            }

            if (this.ckbTraceLevelException.Checked)
            {
                traceLevel |= TraceLevel.Exception;
            }

            if (this.ckbTraceLevelMethodCall.Checked)
            {
                traceLevel |= TraceLevel.MethodCall;
            }

            GlobalSettings.TraceLevel = traceLevel;

            Tracer.TraceGen.TraceLevel = traceLevel;

            #endregion

            GlobalSettings.ShowSetScaleReminder = this.ckbShowSetScaleReminder.Checked;

            this.DialogResult = DialogResult.OK;
 
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        //private void btnShortcuts_Click(object sender, EventArgs e)
        //{
        //    ShortcutSettingsForm shortcutSettingsForm = new ShortcutSettingsForm();

        //    DialogResult dialogResult = shortcutSettingsForm.ShowDialog();

        //    if (dialogResult != DialogResult.OK)
        //    {
        //        return;
        //    }
        //}

        private void txbDefaultDrawingScaleInInches_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidPositiveDouble(this.txbDefaultDrawingScaleInInches);
        }

        #region Cursor Management

        protected override void WndProc(ref Message m)
        {
            CursorManager.WndProc(this);

            base.WndProc(ref m);
        }

        private void FinishesEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveFromCursorManagementList();
        }

        public bool CursorWithinBounds()
        {
            return base.Bounds.Contains(Cursor.Position);
        }

        public void AddToCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Add(this);
        }

        public void RemoveFromCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Remove(this);
        }
        public bool IsTopMost { get; set; } = false;

        #endregion

        private void ckbKeystrokes_CheckedChanged(object sender, EventArgs e)
        {
            
            config.AppSettings.Settings["keystrokes"].Value = ckbKeystrokes.Checked.ToString();
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void txbMinOverWidthForCombosInInches_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidInt(txbMinOverWidthForCombosInInches);
        }

        private void txbMinOverLengthForCombosInInches_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidInt(txbMinOverLengthForCombosInInches);
        }

        private void txbMinUnderWidthForCombosInInches_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidInt(txbMinUnderWidthForCombosInInches);
        }

        private void txbMinUnderLengthForCombosInInches_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidInt(txbMinUnderLengthForCombosInInches);
        }

        private void txbMinOverWidthInInches_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidInt(txbMinOverWidthInInches);
        }

        private void txbMinOverLengthInInches_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidInt(txbMinOverLengthInInches);
        }

        private void txbMinUnderWidthInInches_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidInt(txbMinUnderWidthInInches);
        }

        private void txbMinUnderLengthInInches_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidInt(txbMinUnderLengthInInches);
        }

        private void CkbShowAreaOutlinesInLineMode_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSettings.ShowAreaOutlineInLineMode = this.CkbShowAreaOutlinesInLineMode.Checked;
        }
    }
}
