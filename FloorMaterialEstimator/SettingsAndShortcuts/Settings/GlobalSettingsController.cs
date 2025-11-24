

using System.Runtime.CompilerServices;

namespace SettingsLib
{
    using System.Windows.Forms;
    using Graphics;
    using TracerLib;
    using FinishesLib;
    using Globals;
   
    public class GlobalSettingsController
    {
        GraphicsWindow window;

        GraphicsPage page;

        public GlobalSettingsController(GraphicsWindow window, GraphicsPage page)
        {
            this.window = window;

            this.page = page;

        }

        private GlobalSettingsForm globalSettingsForm = null;

        public void SetGlobalSettings()
        {
            globalSettingsForm = new GlobalSettingsForm();

            globalSettingsForm.Init();

            

            DialogResult dialogResult = globalSettingsForm.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            if (globalSettingsForm.rbnAreaModeInitialDesignState.Checked)
            {
                GlobalSettings.InitProjectDesignState = 1;
            }

            else if (globalSettingsForm.rbnLineModeInitialDesignState.Checked)
            {
                GlobalSettings.InitProjectDesignState = 2;
            }


            if (globalSettingsForm.rbnShowLineDrawout.Checked)
            {
                GlobalSettings.LineDrawoutModeSetting = LineDrawoutMode.ShowNormalDrawout;
            }

            else if (globalSettingsForm.rbnHideLineDrawout.Checked)
            {
                GlobalSettings.LineDrawoutModeSetting = LineDrawoutMode.NoLineDrawout;
            }

            GlobalSettings.SnapToAxis = globalSettingsForm.ckbSnapToAxis.Checked;

            double dTemp = 0;
            int iTemp = 0;

            if (double.TryParse(globalSettingsForm.txbSnapToAxisResolutionInDegrees.Text.Trim(), out dTemp))
            {
                GlobalSettings.SnapResolutionInDegrees = dTemp;
            }

            if (double.TryParse(globalSettingsForm.txbSnapDistance.Text.Trim(), out dTemp))
            {
                GlobalSettings.SnapDistance = dTemp;
            }

            GlobalSettings.ShowMarker = globalSettingsForm.ckbShowMarker.Checked;
           
            GlobalSettings.ShowGuides = globalSettingsForm.ckbShowGuides.Checked;

            GlobalSettings.ShowGrid = globalSettingsForm.ckbShowGrid.Checked;

            GlobalSettings.ShowRulers = globalSettingsForm.ckbShowRulers.Checked;

            GlobalSettings.ShowPanAndZoom = globalSettingsForm.ckbShowPanAndZoom.Checked;
            
            GlobalSettings.ShowAreaEditFormAsModal = globalSettingsForm.ckbShowAreaEditFormAsModal.Checked;

            GlobalSettings.ShowLineEditFormAsModal = globalSettingsForm.ckbShowLineEditFormAsModal.Checked;

            GlobalSettings.ShowSeamEditFormAsModal = globalSettingsForm.ckbShowSeamEditFormAsModal.Checked;

            GlobalSettings.ShowAreaFilterFormAsModal = globalSettingsForm.ckbShowAreaFilterFormAsModal.Checked;

            GlobalSettings.ShowLineFilterFormAsModal = globalSettingsForm.ckbShowLineFilterFormAsModal.Checked;

            GlobalSettings.StartupFullScreenMode = globalSettingsForm.ckbStartupFullScreenMode.Checked;

            GlobalSettings.LockAreaWhen100PctSeamed = globalSettingsForm.ckbLockAreaWhen100PctSeamed.Checked;

            #region Minimums for overs and unders

            iTemp = 0;

            if (int.TryParse(globalSettingsForm.txbMinOverWidthInInches.Text.Trim(), out iTemp))
            {
                GlobalSettings.MinOverageWidthInInches = iTemp;
            }

            iTemp = 0;

            if (int.TryParse(globalSettingsForm.txbMinOverLengthInInches.Text.Trim(), out iTemp))
            {
                GlobalSettings.MinOverageLengthInInches = iTemp;
            }

            iTemp = 0;

            if (int.TryParse(globalSettingsForm.txbMinUnderWidthInInches.Text.Trim(), out iTemp))
            {
                GlobalSettings.MinUnderageWidthInInches = iTemp;
            }

            iTemp = 0;

            if (int.TryParse(globalSettingsForm.txbMinUnderLengthInInches.Text.Trim(), out iTemp))
            {
                GlobalSettings.MinUnderageLengthInInches = iTemp;
            }

            iTemp = 0;

            if (int.TryParse(globalSettingsForm.txbMinOverWidthForCombosInInches.Text.Trim(), out iTemp))
            {
                GlobalSettings.MinOverComboWidthInInches = iTemp;
            }

            iTemp = 0;

            if (int.TryParse(globalSettingsForm.txbMinOverLengthForCombosInInches.Text.Trim(), out iTemp))
            {
                GlobalSettings.MinOverComboLengthInInches = iTemp;
            }

            iTemp = 0;

            if (int.TryParse(globalSettingsForm.txbMinUnderWidthForCombosInInches.Text.Trim(), out iTemp))
            {
                GlobalSettings.MinUnderComboWidthInInches = iTemp;
            }

            iTemp = 0;

            if (int.TryParse(globalSettingsForm.txbMinUnderLengthForCombosInInches.Text.Trim(), out iTemp))
            {
                GlobalSettings.MinUnderComboLengthInInches = iTemp;
            }

            #endregion

            #region Roll Width Default Values

            GlobalSettings.RollWidthDefaultValueFeet = (int) globalSettingsForm.NudDefaultRollWidthFeet.Value ;
            GlobalSettings.RollWidthDefaultValueInches = (int)globalSettingsForm.NudDefaultRollWidthInches.Value ;
            GlobalSettings.RollOverlapDefaultValueInches = (int)globalSettingsForm.NudDefaultOverlapInches.Value ;
            GlobalSettings.RollExtraPerCutDefaultValueInches = (int)globalSettingsForm.NudExtraPerCutInches.Value ;

            #endregion

            iTemp = 0;

            if (string.IsNullOrWhiteSpace(globalSettingsForm.txbAutosaveIntervalInSeconds.Text))
            {
                MessageBox.Show("An missing or invalid autosave interval was specified. The autosave function will be disabled.");

                GlobalSettings.AutosaveEnabled = false;
            }


            else if (int.TryParse(globalSettingsForm.txbAutosaveIntervalInSeconds.Text.Trim(), out iTemp))
            {
                if (iTemp < 10)
                {
                    MessageBox.Show("An autosave interval less than 10 seconds was specified. The autosave interval will be set to 60.");

                    GlobalSettings.AutosaveIntervalInSeconds = 10;
                }

                else
                {
                    GlobalSettings.AutosaveIntervalInSeconds = iTemp;
                }
            }

            else
            {
                MessageBox.Show("A missing or invalid autosave interval was specified. The autosave function will be disabled.");

                GlobalSettings.AutosaveEnabled = false;
            }

            GlobalSettings.AutoReseamAndCutOnRollWidthOrScaleChange = globalSettingsForm.ckbAutoUpdateSeamsAndCuts.Checked;

            GlobalSettings.LockScrollWhenDrawingSmallerThanCanvas = globalSettingsForm.ckbLockScrollWhenDrawingSmallerThanCanvas.Checked;

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbSmallCircleRadius.Text.Trim(), out dTemp))
            {
                GlobalSettings.CounterSmallCircleRadius = dTemp;
            }

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbMediumCircleRadius.Text.Trim(), out dTemp))
            {
                GlobalSettings.CounterMediumCircleRadius = dTemp;
            }

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbLargeCircleRadius.Text.Trim(), out dTemp))
            {
                GlobalSettings.CounterLargeCircleRadius = dTemp;
            }

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbSmallFontSizeInPts.Text.Trim(), out dTemp))
            {
                GlobalSettings.CounterSmallFontInPts = dTemp;
            }

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbMediumFontSizeInPts.Text.Trim(), out dTemp))
            {
                GlobalSettings.CounterMediumFontInPts = dTemp;
            }

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbLargeFontSizeInPts.Text.Trim(), out dTemp))
            {
                GlobalSettings.CounterLargeFontInPts = dTemp;
            }

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbDefaultDrawingScaleInInches.Text.Trim(), out dTemp))
            {
                GlobalSettings.DefaultDrawingScaleInInches = dTemp;
            }

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbDefaultWidth.Text.Trim(), out dTemp))
            {
                GlobalSettings.DefaultNewDrawingWidthInInches = dTemp;
            }

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbDefaultHeight.Text.Trim(), out dTemp))
            {
                GlobalSettings.DefaultNewDrawingHeightInInches = dTemp;
            }

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbDefaultHeight.Text.Trim(), out dTemp))
            {
                GlobalSettings.DefaultNewDrawingHeightInInches = dTemp;
            }

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbGridSpacingInInches.Text.Trim(), out dTemp))
            {
                GlobalSettings.GridSpacingInInches = dTemp;
            }


            VisioInterop.SetPageGrid1(page, GlobalSettings.GridSpacingInInches, GlobalSettings.GridOffsetInInches);

            dTemp = 0;

            if (double.TryParse(globalSettingsForm.txbGridOffsetInInches.Text.Trim(), out dTemp))
            {
                GlobalSettings.GridOffsetInInches = dTemp;
            }

            iTemp = 0;

            if (int.TryParse(globalSettingsForm.txbGraphicsPrecision.Text.Trim(), out iTemp))
            {
                GlobalSettings.GraphicsPrecision = iTemp;
            }

            GlobalSettings.AllowEditingOfShortcutKeys = globalSettingsForm.ckbAllowEditingOfShortcutKeys.Checked;

            GlobalSettings.AllowEditingOfToolTips = globalSettingsForm.ckbAllowEditingOfToolTips.Checked;

            GlobalSettings.AutosaveEnabled = globalSettingsForm.ckbAutosaveEnabled.Checked;

            GlobalSettings.ValidateOnProjectSave = globalSettingsForm.ckbValidateOnProjectSave.Checked;

            GlobalSettings.MouseWheelZoomInterval = (int) globalSettingsForm.nudMouseWheelZoomInterval.Value;

            GlobalSettings.ZoomInOutButtonPercent = (int) globalSettingsForm.nudZoomInOutButtonPercent.Value;

            GlobalSettings.ArrowMoveIncrement = (int)globalSettingsForm.nudArrowMoveIncrement.Value;

            GlobalSettings.ShortcutOrientation = globalSettingsForm.rbnLeftHanded.Checked ? ShortCutOrientation.LeftHanded : ShortCutOrientation.RightHanded;

            GlobalSettings.ValidateRolloutAndCutWidth = globalSettingsForm.ckbValidateRolloutAndCutWidths.Checked;

            GlobalSettings.UpdateDebugFormDynamically = globalSettingsForm.ckbUpdateDebugFormDynamically.Checked;

            GlobalSettings.AreaIndexFontInPts = (double)globalSettingsForm.nmAreaNumbersFontsize.Value;

            GlobalSettings.CutIndexFontInPts = (double)globalSettingsForm.nmCutNumbersFontsize.Value;

            GlobalSettings.CutIndexCircleRadius = ((double)globalSettingsForm.nmCutIndexCircleRadius.Value) / 100.0;

            GlobalSettings.ShowCutIndexCircles = globalSettingsForm.ckbShowCutIndexCircles.Checked;

            GlobalSettings.OverageIndexFontInPts = (double)globalSettingsForm.nmOverageNumbersFontsize.Value;

            GlobalSettings.UnderageIndexFontInPts = (double)globalSettingsForm.nmUnderageNumbersFontsize.Value;

            GlobalSettings.ShowAreaOutlineInLineMode = (bool)globalSettingsForm.CkbShowAreaOutlinesInLineMode.Checked;
        }


    }
}
