

namespace FloorMaterialEstimator
{
    using Graphics;
    using SettingsLib;
    using System;

    using Globals;

    using Utilities;
    using System.Diagnostics;
    using CanvasLib.Filters.Area_Filter;
    using System.Windows.Forms;
    using CanvasLib.Filters.Line_Filter;
    using MaterialsLayout;
    using System.Collections.Generic;
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.OversUndersForm;
    using SummaryReport;
    using FinishesLib;
    using System.IO;
    using System.Drawing;
    using TracerLib;
    using CanvasLib.Counters;
    using CanvasLib.Legend;

    public partial class FloorMaterialEstimatorBaseForm
    {
        public void BtnToolStripSave_Click(object sender, EventArgs e)
        {
            while (withinAutosave)
            {
                System.Threading.Thread.Sleep(10);
            }

            withinProjectAccess = true;

            lock (AutosaveLock)
            {
                doSaveProject(true);
            }

            withinProjectAccess = false;
        }

        public void BtnPanMode_Click(object sender, EventArgs e)
        {
            this.btnPanMode.Checked = true;
            this.btnDrawMode.Checked = false;
        }

        public void BtnDrawMode_Click(object sender, EventArgs e)
        {
            this.btnPanMode.Checked = false;
            this.btnDrawMode.Checked = true;
        }

        public void BtnAreaDesignState_Click(object sender, EventArgs e)
        {
            if (DesignState == DesignState.Area)
            {
                //Debug.Assert(btnAreaMode.Checked &&
                //    this.tbcPageAreaLine.SelectedIndex == FloorMaterialEstimatorBaseForm.tbcAreaModeIndex);

                //Utilities.SetTabSelectedIndex(this.tbcPageAreaLine, FloorMaterialEstimatorBaseForm.tbcAreaModeIndex, ref AllowTabSelection);

                return;
            }

            if (DesignState == DesignState.Seam && SystemState.SeamMode == SeamMode.Subdivision && SystemState.DrawingShape)
            {
                btnCancelSubdivision_Click(null, null);
            }

            // Hit the tab for area mode
            Utilities.SetTabSelectedIndex(this.tbcPageAreaLine, FloorMaterialEstimatorBaseForm.tbcAreaModeIndex, ref AllowTabSelection);
        }

        public void BtnLineDesignState_Click(object sender, EventArgs e)
        {
            if (DesignState == DesignState.Line)
            {
                Debug.Assert(btnLineDesignState.Checked &&
                    this.tbcPageAreaLine.SelectedIndex == FloorMaterialEstimatorBaseForm.tbcLineModeIndex);

                return;
            }

            if (DesignState == DesignState.Seam && SystemState.SeamMode == SeamMode.Subdivision && SystemState.DrawingShape)
            {
                btnCancelSubdivision_Click(null, null);
            }

            // Hit the tab for line mode
            Utilities.SetTabSelectedIndex(this.tbcPageAreaLine, FloorMaterialEstimatorBaseForm.tbcLineModeIndex, ref AllowTabSelection);
        }

        public void BtnSeamDesignState_Click(object sender, EventArgs e)
        {
            if (DesignState == DesignState.Seam)
            {
                Debug.Assert(btnSeamDesignState.Checked &&
                    this.tbcPageAreaLine.SelectedIndex == FloorMaterialEstimatorBaseForm.tbcSeamModeIndex);

                return;
            }

            // Hit the tabe for seam mode
            Utilities.SetTabSelectedIndex(this.tbcPageAreaLine, FloorMaterialEstimatorBaseForm.tbcSeamModeIndex, ref AllowTabSelection);
        }


        private void BtnCombos_Click(object sender, EventArgs e)
        {
            if (!cutsAvailable())
            {
                ManagedMessageBox.Show("There are no cuts available for combination with the current finish.");
                return;
            }

            CombosBaseForm combosBaseForm = new CombosBaseForm(this);
            combosBaseForm.Show();
        }


        private void btnCutOversNesting_Click(object sender, EventArgs e)
        {
            if (!cutsAvailable())
            {
                ManagedMessageBox.Show("There are no cuts available for nesting with the current finish.");
                return;
            }

            if (!undersAvailable())
            {
                ManagedMessageBox.Show("There are no unders available for nesting with the current finish.");
                return;
            }


            CutUndrsNestingBaseForm cutOversNestingBaseForm = new CutUndrsNestingBaseForm(this);

            cutOversNestingBaseForm.Show();
        }

        public AreaFilterForm AreaFilterForm = null;

        private void BtnFilterAreas_Click(object sender, EventArgs e)
        {
            if (GlobalSettings.ShowAreaFilterFormAsModal)
            {
                this.BtnFilterAreas.Checked = true;

                AreaFilterForm = new AreaFilterForm();

                AreaFilterForm.Init(this.BtnFilterAreas, this.AreaFinishBaseList);

                AreaFilterForm.Load += AreaFilterForm_Load;

                AreaFilterForm.FormClosing += AreaFilterForm_FormClosing;

                AreaFilterForm.ShowDialog();

                //btnFilterAreas.Checked = this.AreaFilters.ItemsFiltered();

                AreaFilterForm = null;
            }

            else
            {
                if (AreaFilterForm != null)
                {
                    AreaFilterForm.BringToFront();

                    AreaFilterForm.WindowState = FormWindowState.Normal;

                    AreaFilterForm.BringToFront();
                }

                else
                {
                    this.BtnFilterAreas.Checked = true;

                    AreaFilterForm = new AreaFilterForm();

                    AreaFilterForm.Init(this.BtnFilterAreas, this.AreaFinishBaseList);

                    AreaFilterForm.FormClosed += AreaFilterForm_FormClosed;

                    AreaFilterForm.Show(this);

                    AreaFilterForm.BringToFront();
                }

            }
        }

        private void AreaFilterForm_Load(object sender, EventArgs e)
        {
            //AreaFilterForm areaFilterForm = (AreaFilterForm)sender;

            //areaFilterForm.rdbSeamShow.CheckedChanged += RdbSeamShow_CheckedChanged;
            //areaFilterForm.rdbSeamIncludeCutNumbers.CheckedChanged += RdbSeamIncludeCutNumbers_CheckedChanged;
            //areaFilterForm.rdbSeamHide.CheckedChanged += RdbSeamHide_CheckedChanged;
        }


        private void AreaFilterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AreaFilterForm areaFilterForm = (AreaFilterForm)sender;

            areaFilterForm.FormClosing -= AreaFilterForm_FormClosing;
            areaFilterForm.Load -= AreaFilterForm_Load;

            //areaFilterForm.rdbSeamShow.CheckedChanged -= RdbSeamShow_CheckedChanged;
            //areaFilterForm.rdbSeamIncludeCutNumbers.CheckedChanged -= RdbSeamIncludeCutNumbers_CheckedChanged;
            //areaFilterForm.rdbSeamHide.CheckedChanged -= RdbSeamHide_CheckedChanged;

            // Shouldn't be needed, but defensive

            //if (Utilities.IsNotNull(AreaFilters))
            //{
            //    if (areaFilterForm.rdbSeamShow.Checked)
            //    {
            //        AreaFilters.SeamFilter = areaFilterForm.s
            //    }

            //}
        }

        //private void RdbSeamShow_CheckedChanged(object sender, EventArgs e)
        //{
        //    AreaFilterForm areaFilterForm = (AreaFilterForm)sender;

        //    if (!areaFilterForm.rdbSeamShow.Checked)
        //    {
        //        return;
        //    }

        //    if (Utilities.IsNotNull(AreaFilters))
        //    {
        //        AreaFilters.SeamFilter = CanvasLib.Filters.SeamFilter.Show;
        //    }
        //    if (DesignState == DesignState.Area)
        //    {
        //        areaPalette.SetupAllSeamLayers(
        //            true, false, false, false, false, false);
        //    }
        //}

        //private void RdbSeamIncludeCutNumbers_CheckedChanged(object sender, EventArgs e)
        //{
        //    AreaFilterForm areaFilterForm = (AreaFilterForm)sender;

        //    if (!areaFilterForm.rdbSeamIncludeCutNumbers.Checked)
        //    {
        //        return;
        //    }

        //    AreaModeSeamShow = false;
        //    AreaModeSeamIncludeCutNumbers = true;
        //    AreaModeSeamHide = false;

        //    if (DesignState == DesignState.Area)
        //    {
        //        areaPalette.SetupAllSeamLayers(
        //            true, true, false, false, false, false);
        //    }
        //}


        //private void RdbSeamHide_CheckedChanged(object sender, EventArgs e)
        //{
        //    AreaFilterForm areaFilterForm = (AreaFilterForm)sender;

        //    if (!areaFilterForm.rdbSeamHide.Checked)
        //    {
        //        return;
        //    }

        //    AreaModeSeamShow = false;
        //    AreaModeSeamIncludeCutNumbers = false;
        //    AreaModeSeamHide = true;


        //    if (DesignState == DesignState.Area)
        //    {
        //        areaPalette.SetupAllSeamLayers(
        //            false, false, false, false, false, false);
        //    }
        //}

        private void AreaFilterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AreaFilterForm = null;
        }

        public LineFilterForm LineFilterForm = null;

        private void BtnFilterLines_Click(object sender, EventArgs e)
        {
            if (GlobalSettings.ShowLineFilterFormAsModal)
            {
                this.BtnFilterLines.Checked = true;

                LineFilterForm = new LineFilterForm();

                LineFilterForm.Init(this.BtnFilterLines, this.LineFinishBaseList, this.SeamFinishBaseList);

                LineFilterForm.ShowDialog();

                this.BtnFilterLines.Checked = false;

                LineFilterForm = null;
            }

            else
            {

                if (LineFilterForm != null)
                {
                    LineFilterForm.WindowState = FormWindowState.Normal;

                    LineFilterForm.BringToFront();

                }

                else
                {
                    LineFilterForm = new LineFilterForm();

                    LineFilterForm.Init(this.BtnFilterLines, this.LineFinishBaseList, this.SeamFinishBaseList);

                    LineFilterForm.FormClosed += LineFilterForm_FormClosed;

                    this.BtnFilterLines.Checked = true;

                    LineFilterForm.BringToFront();

                    LineFilterForm.Show(this);
                }

            }
        }


        private void LineFilterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LineFilterForm = null;
        }


        public SummaryReportForm SummaryReportForm { get; set; } = null;

        private void BtnSummaryReport_Click(object sender, EventArgs e)
        {
            //if (!CurrentPage.ScaleHasBeenSet)
            //{
            //    ManagedMessageBox.Show("The drawing scale must be set in order to display the report.");
            //    return;
            //}

            if (this.BtnSummaryReport.Checked && SummaryReportForm.WindowState != FormWindowState.Minimized)
            {
                this.BtnSummaryReport.Checked = false;

                if (Utilities.IsNotNull(SummaryReportForm))
                {
                    SummaryReportForm.Close();

                    SummaryReportForm = null;
                }

                return;
            }

            if (Utilities.IsNotNull(SummaryReportForm))
            {
                SummaryReportForm.WindowState = FormWindowState.Normal;

                SummaryReportForm.BringToFront();

            }

            else
            {
                SummaryReportForm = new SummaryReportForm();

                CounterList counterList = CanvasManager.CounterController.CounterList;

                SummaryReportForm.Init(this.BtnSummaryReport, AreaFinishBaseList, LineFinishBaseList, SeamFinishBaseList, counterList, CurrentPage.ScaleHasBeenSet);

                SummaryReportForm.FormClosed += SummaryReportForm_FormClosed;

                this.BtnSummaryReport.Checked = true;

                SummaryReportForm.BringToFront();

                SummaryReportForm.Show(this);
            }
        }

        private void SummaryReportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SummaryReportForm = null;

            this.BtnSummaryReport.Checked = false;
        }

        //public bool ShowAreas
        //{
        //    get;
        //    set;
        //} = true;

        private void BtnShowAreas_Click(object sender, EventArgs e)
        {
            SystemState.ShowAreas = !SystemState.ShowAreas;

            AreaFinishManagerList.SetDesignStateFormat(this.DesignState, this.SeamMode, SystemState.ShowAreas);

            if (SystemState.ShowAreas)
            {
                this.btnShowAreas.Image = HideAreaImage;

                this.btnShowAreas.ToolTipText = "Hide Areas";
            }

            else
            {
                this.btnShowAreas.Image = ShowAreaImage;

                this.btnShowAreas.ToolTipText = "Show Areas";
            }
        }

        //public bool ShowDrawings
        //{
        //    get;
        //    set;
        //} = true;

        private void BtnShowDrawing_Click(object sender, EventArgs e)
        {
            SystemState.ShowDrawings = !SystemState.ShowDrawings;

            CanvasManager.ShowDrawing(SystemState.ShowDrawings);

            if (SystemState.ShowDrawings)
            {
                this.btnShowDrawing.Image = HideDrawingImage;

                this.btnShowDrawing.ToolTipText = "Hide Drawing";
            }

            else
            {
                this.btnShowDrawing.Image = ShowDrawingImage;

                this.btnShowDrawing.ToolTipText = "Show Drawing";
            }

        }

        #region Field guide button events

        public void BtnShowFieldGuides_Click(object sender, EventArgs e)
        {
            CanvasManager.FieldGuideController.ShowFieldGuides();

            this.BtnShowFieldGuides.Checked = true;
            this.btnHideFieldGuides.Checked = false;
        }

        public void BtnHideFieldGuides_Click(object sender, EventArgs e)
        {
            CanvasManager.FieldGuideController.HideFieldGuides();

            this.BtnShowFieldGuides.Checked = false;
            this.btnHideFieldGuides.Checked = true;
        }

        public void BtnDeleteFieldGuides_Click(object sender, EventArgs e)
        {
            if (CanvasManager.FieldGuideController.FieldGuideList.Count <= 0)
            {
                return;
            }

            //DialogResult dr = ManagedMessageBox.Show("Are you sure you want to delete all field guides?", "Delete Field Guides", MessageBoxButtons.OKCancel);

            //if (dr != DialogResult.OK)
            //{
            //    return;
            //}

            CanvasManager.FieldGuideController.DeleteAllFieldGuides();
        }

        #endregion


        private void BtnAutoSelect_Click(object sender, EventArgs e)
        {
            if (this.btnAutoSelect.Checked)
            {
                ResetAutoSelectOption();
            }

            else
            {
                if (btnShowSeamingTool.BackColor == Color.Orange)
                {
                    DialogResult dr = ManagedMessageBox.Show("Activating the Highlight seam area function will disable the Seam Tool. Do you want to proceed?", "Disable Seam Tool", MessageBoxButtons.YesNo);

                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                    CanvasManager.HideSeamingTool();
                }
                this.btnAutoSelect.Checked = true;
            }
        }

        public void ResetAutoSelectOption()
        {
            this.btnAutoSelect.Checked = false;
            this.CanvasManager.ResetSeamSelectedObjects();
        }

        private void btnRedoSeamsAndCuts_Click(object sender, EventArgs e)
        {
            RedoSeamingInquiryForm redoSeamingInquiryForm = new RedoSeamingInquiryForm(this, AreaFinishBaseList);

            redoSeamingInquiryForm.ShowDialog();

            //btnRedoSeamsAndCuts.Checked = false;
            btnRedoSeamsAndCuts.Image = RedoSeamsOffImage;
        }

        public FloorMaterialEstimator.OversUndersForm.OversUndersForm OversUndersForm = null;

        public void OversUndersFormUpdate(bool warnOnMaterialType = true)
        {
            if (warnOnMaterialType)
            {
                if (selectedAreaFinish.MaterialsType != MaterialsType.Rolls)
                {
                    ManagedMessageBox.Show("Please be sure a roll-type material is selected before displaying or updating the overs and unders.");
                    return;
                }
            }

            EnsureOversUndersForm();

            OversUndersForm.UpdatePanelDisplay();
        }

        /// <summary>
        /// Responds to the overs/unders button click
        /// </summary>
        /// <param name="sender">The event sender. Ignored.</param>
        /// <param name="e">The event sender. Ignored.</param>
        public void BtnOversUnders_Click(object sender, EventArgs e)
        {
            if (selectedAreaFinish.MaterialsType != MaterialsType.Rolls)
            {
                ManagedMessageBox.Show("Please be sure a roll-type material is selected before displaying or updating the overs and unders.");
                return;

            }

            AreaFinishManager areaFinishManager = selectedAreaFinishManager;

            OversUndersFormUpdate();

            if (this.btnOversUnders.Checked)
            {
                if (!OversUndersForm.Visible)
                {
                    OversUndersForm.Show(this);
                }


                OversUndersForm.WindowState = FormWindowState.Normal;

                OversUndersForm.BringToFront();

                //OversUndersForm.TopMost = true;
            }
            else
            {

                this.btnOversUnders.Checked = true;

                if (Utilities.IsNotNull(OversUndersForm))
                {
                    if (!OversUndersForm.Visible)
                    {
                        OversUndersForm.Show(this);
                    }
                }
                else
                {
                    EnsureOversUndersForm();

                    //OversUndersFormUpdate(areaFinishManager, currentPage.DrawingScaleInInches, (int)Math.Round(selectedAreaFinish.RollWidthInInches));

                    OversUndersForm.Show(this);
                }
                OversUndersForm.WindowState = FormWindowState.Normal;

                OversUndersForm.BringToFront();

                // OversUndersForm.TopMost = true;

            }
        }

        /// <summary>
        /// Responds to the closing of the overs/unders form
        /// </summary>
        /// <param name="sender">The event sender. Ignored.</param>
        /// <param name="e">The event sender. Ignored.</param>
        private void UndersOversForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.btnOversUnders.Checked = false;

            this.CurrentProjectChanged = this.Notes != OversUndersForm.Notes;
            this.Notes = OversUndersForm.Notes;
            OversUndersForm = null;
        }

        public void BtnFullScreen_Click(object sender, EventArgs e)
        {
            if (this.btnFullScreen.Checked)
            {

                ContractCanvas();
            }

            else
            {
                ExpandCanvas();
            }
        }

        public void ExpandCanvas()
        {
            if (this.btnFullScreen.Checked)
            {
                return;
            }

            this.btnFullScreen.Checked = true;
            btnFullScreen.Image = ContractCanvasImage;


            setCommandPanelSizes();
            setTbcPageAreaSize();
            setAxAreaSize();

            //CanvasManager.SetZoom(CanvasManager.PanAndZoomController.CurrZoom * 1.1);
        }

        public void ContractCanvas()
        {
            if (!this.btnFullScreen.Checked)
            {
                return;
            }

            this.btnFullScreen.Checked = false;
            btnFullScreen.Image = ExpandCanvasImage;


            setCommandPanelSizes();
            setTbcPageAreaSize();
            setAxAreaSize();

            //CanvasManager.SetZoom(CanvasManager.PanAndZoomController.CurrZoom  / 1.1);
        }

        private void BtnCounters_Click(object sender, EventArgs e)
        {
            if (CanvasManager.CounterController.CountersActivated)
            {
                CanvasManager.CounterController.DeactivateCounters();
            }

            else
            {
                if (DesignState != DesignState.Area && DesignState != DesignState.Seam)
                {
                    ManagedMessageBox.Show("Counters can only be activated in area or seam mode.");
                    return;
                }

                CanvasManager.CounterController.ActivateCounters();
            }
        }

        private void btnShowLabelEditor_Click(object sender, EventArgs e)
        {
            if (DesignState != DesignState.Area && DesignState != DesignState.Seam)
            {
                ManagedMessageBox.Show("Labels can only be activated in area or seam mode.");
                return;
            }

            CanvasManager.LabelManager.ActivateLabels();
        }

        public ShortcutSettingsForm ShortcutSettingsForm = null;

        private void BtnShortcuts_Click(object sender, EventArgs e)
        {
            //switch (DesignState)
            //{
            //    case DesignState.Area:
            //        setDesignStateSelection(ShortcutMode.AreaMode);
            //        break;

            //    case DesignState.Line:
            //        setDesignStateSelection(ShortcutMode.LineMode);
            //        break;

            //    case DesignState.Seam:
            //        setDesignStateSelection(ShortcutMode.SeamMode);
            //        break;

            //    default:
            //        setDesignStateSelection(ShortcutMode.AreaMode);
            //        break;
            //}

        }

        private void btnSelectAreasShortcut_Click(object sender, EventArgs e)
        {
            setDesignStateSelection(ShortcutMode.AreaMode);
        }

        private void btnSelectLinesShortcut_Click(object sender, EventArgs e)
        {
            setDesignStateSelection(ShortcutMode.LineMode);
        }

        private void btnSelectSeamsShortcut_Click(object sender, EventArgs e)
        {
            setDesignStateSelection(ShortcutMode.SeamMode);
        }

        private void btnSelectMenusShortcut_Click(object sender, EventArgs e)
        {
            setDesignStateSelection(ShortcutMode.MenuMode);
        }

        private void btnSelectOtherShortcut_Click(object sender, EventArgs e)
        {
            setDesignStateSelection(ShortcutMode.MiscMode);
        }

        private void setDesignStateSelection(ShortcutMode shortcutMode)
        {
            bool newLaunch = false;

            if (ShortcutSettingsForm is null)
            {
                ShortcutSettingsForm = new ShortcutSettingsForm();

                ShortcutSettingsForm.FormClosed += ShortcutSettingsForm_FormClosed;

                newLaunch = true;
            }

            ShortcutSettingsForm.SetDesignStateSelection(shortcutMode);

            if (newLaunch)
            {
                ShortcutSettingsForm.Show(this);
                ShortcutSettingsForm.BringToFront();
            }
        }

        private void ShortcutSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShortcutSettingsForm = null;
        }

        private void BtnGeneralSettings_Click(object sender, EventArgs e)
        {

            double prevWidthInInches = GlobalSettings.DefaultNewDrawingWidthInInches;
            double prevHeightInInches = GlobalSettings.DefaultNewDrawingHeightInInches;

            bool prevShowCutIndexCircles = GlobalSettings.ShowCutIndexCircles;

            GlobalSettingsController globalSettingsController = new GlobalSettingsController(CanvasManager.Window, CanvasManager.Page);

            globalSettingsController.SetGlobalSettings();

            if (prevShowCutIndexCircles != GlobalSettings.ShowCutIndexCircles)
            {

                foreach (AreaFinishManager areaFinishManager in this.AreaFinishManagerList)
                {
                    areaFinishManager.SetCutIndexCircleVisibility();
                }
            }

            if (GlobalSettings.AllowEditingOfToolTips)
            {
                this.btnToolTipSettings.Visible = true;
            }

            else
            {
                this.btnToolTipSettings.Visible = false;
            }

            if (Utilities.IsNotNull(ShortcutSettingsForm))
            {
                ShortcutSettingsForm.SetUserAccess(GlobalSettings.AllowEditingOfShortcutKeys);
            }

            if (GlobalSettings.AutosaveEnabled)
            {
                if (Program.autosaveTimer is null)
                {
                    Program.autosaveTimer = new System.Timers.Timer();

                    if (GlobalSettings.AutosaveIntervalInSeconds >= 10)
                    {
                        Program.autosaveTimer = new System.Timers.Timer(1000 * GlobalSettings.AutosaveIntervalInSeconds);
                        Program.autosaveTimer.Enabled = true;

                        Program.autosaveTimer.Elapsed += doAutoSave;
                        // Synchronize the timer with the text box
                        Program.autosaveTimer.SynchronizingObject = this;
                        // Start the timer
                        Program.autosaveTimer.AutoReset = true;

                    }
                }

                int currAutosaveInterval = (int)Program.autosaveTimer.Interval;

                if (currAutosaveInterval != GlobalSettings.AutosaveIntervalInSeconds * 1000)
                {

                    Program.autosaveTimer.Stop();

                    Program.autosaveTimer.Interval = GlobalSettings.AutosaveIntervalInSeconds * 1000;

                    Program.autosaveTimer.Start();
                }
            }

            else
            {
                if (Utilities.IsNotNull(Program.autosaveTimer))
                {
                    Program.autosaveTimer.Stop();

                    Program.autosaveTimer = null;
                }
            }

            CanvasManager.ShowGrid = GlobalSettings.ShowGrid;
            CanvasManager.ShowRulers = GlobalSettings.ShowRulers;
            CanvasManager.ShowPanAndZoom = GlobalSettings.ShowPanAndZoom;
            CanvasManager.PanAndZoomController.LockOnUnderSize = GlobalSettings.LockScrollWhenDrawingSmallerThanCanvas;

            if (prevWidthInInches != GlobalSettings.DefaultNewDrawingWidthInInches ||
                prevHeightInInches != GlobalSettings.DefaultNewDrawingHeightInInches)
            {
                VisioInterop.SetPageSize(CanvasManager.CurrentPage, new ShapeSize(GlobalSettings.DefaultNewDrawingWidthInInches, GlobalSettings.DefaultNewDrawingHeightInInches));

                CanvasManager.PanAndZoomController.SetPageSize(GlobalSettings.DefaultNewDrawingWidthInInches, GlobalSettings.DefaultNewDrawingHeightInInches);
                CanvasManager.PanAndZoomController.SetZoom(1);
                CanvasManager.PanAndZoomController.CenterDrawing();
            }
        }

        private void btnToolTipSettings_Click(object sender, EventArgs e)
        {
#if true
            // Start of mod of tool tip editor.

            ToolTipEditManager.ShowToolTipEditForm();

#else
            if (this.btnToolTipSettings.Checked)
            {
                this.txbToolTipText.Visible = false;
                this.lblToolTipTextEditor.Visible = false;
                this.btnSaveToolTipEdits.Visible = false;

                this.btnToolTipSettings.Checked = false;
            }

            else
            {
                this.txbToolTipText.Visible = true;
                this.lblToolTipTextEditor.Visible = true;
                this.btnSaveToolTipEdits.Visible = true;

                this.btnToolTipSettings.Checked = true;
            }
#endif
        }


        //public void BtnLegendRemove_Click(object sender, EventArgs e)
        //{
        //    if (DesignState == DesignState.Area)
        //    {
        //        if (currentPage.AreaModeLegend_IsNull())
        //        {
        //            return;
        //        }

        //        currentPage.AreaModeLegend.ShowNone();

        //        this.btnLegendShow.Checked = currentPage.AreaModeLegend.Visible;
        //    }

        //    else if (DesignState == DesignState.Line)
        //    {
        //        currentPage.LineModeLegend.ShowNone();

        //        this.btnLegendShow.Checked = currentPage.LineModeLegend.Visible;
        //    }
        //}

        //public void BtnLegendShow_Click(object sender, EventArgs e)
        //{
        //    if (DesignState == DesignState.Area)
        //    {
        //        CanvasManager.LegendController.AreaModeLegend.Visible = !CanvasManager.LegendController.AreaModeLegend.Visible;

        //        btnLegendShow.Text = CanvasManager.LegendController.AreaModeLegend.Visible ? "Hide Legend" : "Show Legend";

        //        CanvasManager.LegendController.AreaModeLegend.ShowLegend(CanvasManager.LegendController.AreaModeLegend.Visible);
        //    }

        //    else if (DesignState == DesignState.Line)
        //    {
        //        CanvasManager.LegendController.LineModeLegend.Visible = !CanvasManager.LegendController.LineModeLegend.Visible;

        //        btnLegendShow.Text = CanvasManager.LegendController.LineModeLegend.Visible ? "Hide Legend" : "Show Legend";

        //        CanvasManager.LegendController.LineModeLegend.ShowLegend(CanvasManager.LegendController.LineModeLegend.Visible);

        //        CanvasManager.LegendController.LineModeLegend.ShowLegend(CanvasManager.LegendController.LineModeLegend.Visible);
        //    }
        //}

        public void BtnSnapToGrid_Click(object sender, EventArgs e)
        {
            if (btnSnapToGrid.Checked)
            {
                btnSnapToGrid.Checked = false;
            }

            else
            {
                btnSnapToGrid.Checked = true;
            }
        }


        private void btnRestore_Click(object sender, EventArgs e)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });

            while (withinAutosave)
            {
                System.Threading.Thread.Sleep(10);
            }

            withinProjectAccess = true;

            lock (AutosaveLock)
            {
                try
                {
                    doRestore();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Restore failed.");

                    Tracer.TraceGen.TraceException("Exception thrown in btnRestore_Click", ex, 1, true);
                }
            }

            withinProjectAccess = false;
        }

        private void doRestore()
        {
            string projectFullFileName = Path.Combine(Program.AutosaveFolder, "Autosave.eproj");

            if (!File.Exists(projectFullFileName))
            {
                MessageBox.Show("An autosave project does not exist.");
                return;
            }

            CleanupOldProject();

            ProjectImporter projectImporter = new ProjectImporter(this);
            string updatedProjectFileName = projectImporter.ImportProject(projectFullFileName);

            if (string.IsNullOrEmpty(CurrentProjectName))
            {
                CurrentProjectName = "<Unknown> (Restored)";
            }

            else if (!CurrentProjectName.ToLower().EndsWith(" (restored)"))
            {
                CurrentProjectName += " (Restored)";
            }

            this.lblProjectName.Text = CurrentProjectName;
        }

        //private void btnAddNotes_Click(object sender, EventArgs e)
        //{

        //    LegendNotesForm legendNotesForm = new LegendNotesForm();

        //    DialogResult dr = legendNotesForm.ShowDialog();

        //    if (dr == DialogResult.OK)
        //    {
        //        //currentPage.AreaModeLegendNotes = legendNotesForm.Notes;

        //        if (CanvasManager.LegendController.AreaModeLegend != null)
        //        {
        //            CanvasManager.LegendController.AreaModeLegend.NotesChanged(legendNotesForm.Notes);
        //        }
        //    }
        //}

        private void BtnLoadPaletteDefinitions_Click(object sender, EventArgs e)
        {
            LoadSetupFromOtherSource();
        }
    }

}
