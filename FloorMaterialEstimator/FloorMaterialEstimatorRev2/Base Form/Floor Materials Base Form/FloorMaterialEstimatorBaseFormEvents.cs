//-------------------------------------------------------------------------------//
// <copyright file="FloorMaterialEstimatorBaseFormEvents.cs"                     //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//   Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019 and 2020    //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator
{
    using CanvasLib.Borders;
    using CanvasLib.Filters.Area_Filter;
    using CanvasLib.Filters.Line_Filter;
    using CanvasLib.Scale_Line;
    using CanvasLib.SeamingTool;
    using CanvasLib.Tape_Measure;
    using FinishesLib;
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Dialog_Boxes;
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.Supporting_Forms;
    using global::CanvasManager;
    using Globals;
    using Globals;
    using Graphics;
    using MaterialsLayout;
    using PaletteLib;
    using SettingsLib;
    using ShapeNestLib;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using Utilities;
    using Utilities;
    using Utilities.User_Controls;
    using Visio = Microsoft.Office.Interop.Visio;

    public partial class FloorMaterialEstimatorBaseForm
    {
        internal UCLineFinishPaletteElement selectedLineFinish => linePalette.SelectedLine;
       
        internal UCAreaFinishPaletteElement selectedAreaFinish => areaPalette.SelectedFinish;
        
        internal AreaFinishManager selectedAreaFinishManager => AreaFinishManagerList.SelectedAreaFinishManager;

        internal LineFinishManager selectedLineFinishManager => LineFinishManagerList.SelectedLineFinishManager;

        private void FloorMaterialEstimatorBaseForm_Activated(object sender, EventArgs e)
        {
           
            //Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            //bool keystrokes = Convert.ToBoolean(config.AppSettings.Settings["keystrokes"].Value);
            //MLft.Visible = keystrokes; MRgt.Visible = keystrokes; lblKeystrokes.Visible = keystrokes;

            SystemState.BaseFormHasFocus = true;
        }


        private void FloorMaterialEstimatorBaseForm_Deactivate(object sender, EventArgs e)
        {
            SystemState.BaseFormHasFocus = false;
        }

        private void FloorMaterialEstimatorBaseForm_Load(object sender, EventArgs e)
        {      
            if (GlobalSettings.StartupFullScreenMode)
            {

                this.WindowState = FormWindowState.Maximized;
                axDrawingControl.Window.ShowPageTabs = false;


            }

            
            //-------------------------------------------------------
            var pnlColorPalette = new UCDrawColor()
            {
                BackColor = Color.White,
            };
            var ColorPalette = new ToolStripControlHost(pnlColorPalette)
            {
                Padding = Padding.Empty,
                Margin = Padding.Empty
            };
            ((ToolStripDropDownMenu)btnDrawColors.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)btnDrawColors.DropDown).ShowImageMargin = false;
            btnDrawColors.DropDown.Items.Add(ColorPalette);
            //---------------------------------------------------------------------------------------
        }

        private void ToolStripCounters_Click(object sender, EventArgs e)
        {

        }

        internal void SetFilteredLineToolsStatus(bool bChecked)
        {
            this.BtnFilterLines.Checked = bChecked;
        }

        
        #region Drawing mode button events

        private void btnDrawRectangle_Click(object sender, EventArgs e)
        {
            ManagedMessageBox.Show("Rectangle drawing not currently activated.");

            return;

            //drawingMode = DrawingMode.Rectangle;
        }

        private void btnDrawPolyline_Click(object sender, EventArgs e)
        {
            //DrawingMode = DrawingMode.Polyline;
        }

        private void btnSetScale_Click(object sender, EventArgs e)
        {
            if (this.btnSetCustomScale.Checked)
            {
                CanvasManager.ScaleRuleController.CancelScaleLine();

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.TapeMeasure)
            {
                MessageBoxAdv.Show(
                    "Close the tape measure before launching the scale tool."
                    , "Close Tape Measure"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Error);

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.ScaleLine)
            {
                SystemState.DrawingMode = DrawingMode.Default;

                return;
            }

            CanvasManager.PrevDrawingMode = SystemState.DrawingMode;

            CanvasManager.PrevDrawingShape = SystemState.DrawingShape;

            SystemState.DrawingMode = DrawingMode.ScaleLine;

            CanvasManager.processCutOverAndUnderIndexDeselect();

            CanvasManager.ScaleRuleController.InitiateSetScale(this, CurrentPage.SeamedAreasExist());

            //DrawingMode = DrawingMode.Default;
        }

        private DrawingMode onEntryToTapeMeasureDrawingMode = (DrawingMode)0;


        private bool onEntryToTapeaMeasureDrawingShape = false;

        public void BtnTapeMeasure_Click(object sender, EventArgs e)
        {
            if (this.btnTapeMeasure.Checked)
            {
                CanvasManager.TapeMeasureController.CancelTapeMeasure();

                SystemState.DrawingMode = onEntryToTapeMeasureDrawingMode;

                SystemState.DrawingShape = onEntryToTapeaMeasureDrawingShape  ;

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.ScaleLine)
            {
                MessageBoxAdv.Show(
                    "Close the scale setting tool before launching the tape measure."
                    , "Close Scale Tool"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Error);
                return;
            }

            if (SystemState.DrawingMode == DrawingMode.TapeMeasure)
            {
                SystemState.DrawingMode = DrawingMode.Default;

                return;
            }

            CanvasManager.TapeMeasureController.InitiateGetScale();

            CanvasManager.processCutOverAndUnderIndexDeselect();

            onEntryToTapeMeasureDrawingMode = SystemState.DrawingMode;

            onEntryToTapeaMeasureDrawingShape = SystemState.DrawingShape;

            SystemState.DrawingMode = DrawingMode.TapeMeasure;

            CanvasManager.Window.DeselectAll();
        }

        public void ResetTapeMeasure()
        {
            if (this.btnTapeMeasure.Checked)
            {
                CanvasManager.TapeMeasureController.CancelTapeMeasure();
            }

            if (SystemState.DrawingMode == DrawingMode.TapeMeasure)
            {
                SystemState.DrawingMode = DrawingMode.Default;
            }
        }
        #endregion


        private void btnAreaEditSettings_Click(object sender, EventArgs e)
        {
            Color[] colorArray = new Color[2]
            {
                GlobalSettings.AreaEditSettingColor1,
                GlobalSettings.AreaEditSettingColor2
            };

            double[] transparencyArray = new double[2]
            {
                GlobalSettings.AreaEditSettingColor1Transparency,
                GlobalSettings.AreaEditSettingColor2Transparency
            };

            AreaEditSettings areaEditSettings = new AreaEditSettings(this, colorArray, transparencyArray, GlobalSettings.AreaEditSettingsDefaultIndex);

            DialogResult dialogResult = areaEditSettings.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            GlobalSettings.AreaEditSettingColor1 = areaEditSettings.AreaEditSettingColorArray[0];
            GlobalSettings.AreaEditSettingColor2 = areaEditSettings.AreaEditSettingColorArray[1];

            GlobalSettings.AreaEditSettingColor1Transparency = areaEditSettings.AreaEditSettingTransparencyArray[0];
            GlobalSettings.AreaEditSettingColor2Transparency = areaEditSettings.AreaEditSettingTransparencyArray[1];

            CanvasManager.CurrentPage.UpdateSelectedAreaColors(areaEditSettings.DefaultColorIndex);

            if (areaEditSettings.SetDefault)
            {
                GlobalSettings.AreaEditSettingsDefaultIndex = areaEditSettings.DefaultColorIndex;

                double transparency = ((double)areaEditSettings.trbTransparency.Value) / 100.0;

                transparency = Math.Min(1.0, Math.Max(0.0, transparency));

                if (GlobalSettings.AreaEditSettingsDefaultIndex == 0)
                {
                    GlobalSettings.AreaEditSettingColor1Transparency = transparency;
                }

                else if (GlobalSettings.AreaEditSettingsDefaultIndex == 1)
                {
                    GlobalSettings.AreaEditSettingColor2Transparency = transparency;
                }
            }
        }

        private void btnLineEditSettings_Click(object sender, EventArgs e)
        {
            Color[] colorArray = new Color[2] { GlobalSettings.LineEditSettingColor1, GlobalSettings.LineEditSettingColor2 };
            int[] intensityArray = new int[2] { GlobalSettings.LineEditSettingColor1Intensity, GlobalSettings.LineEditSettingColor2Intensity };

            LineEditSettings lineLineSettings = new LineEditSettings(this, colorArray, intensityArray, 0);

            DialogResult dialogResult = lineLineSettings.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            GlobalSettings.LineEditSettingColor1Intensity = lineLineSettings.LineEditSettingIntensityArray[0];
            GlobalSettings.LineEditSettingColor2Intensity = lineLineSettings.LineEditSettingIntensityArray[1];

            CanvasManager.CurrentPage.UpdateSelectedLineColors(lineLineSettings.DefaultColorIndex);

            if (lineLineSettings.SetDefault)
            {
                GlobalSettings.LineEditSettingsDefaultIndex = lineLineSettings.DefaultColorIndex;
            }
        }


        private void btnLayoutLineHide_Click(object sender, EventArgs e)
        {
            if (BtnDoorTakeoutShow.Text == "Show")
            {
                GraphicsLayerBase layer = CanvasManager.Page.TakeoutLayer;

                layer.SetLayerVisibility(true);

                BtnDoorTakeoutShow.Text = "Hide";
            }

            else
            {
                GraphicsLayerBase layer = CanvasManager.Page.TakeoutLayer;

                layer.SetLayerVisibility(false);

                BtnDoorTakeoutShow.Text = "Show";
            }
        }


        private void btnZoom50_Click(object sender, EventArgs e)
        {
            CanvasManager.Zoom(50.0 / 40.0);
        }

        private void btnZoom100_Click(object sender, EventArgs e)
        {
            CanvasManager.Zoom(100.0 / 40.0);
        }

        private void btnZoom150_Click(object sender, EventArgs e)
        {
            CanvasManager.Zoom(150.0 / 40.0);
        }

        private void btnZoom200_Click(object sender, EventArgs e)
        {
            CanvasManager.Zoom(200.0 / 40.0);
        }

        private void btnZoom400_Click(object sender, EventArgs e)
        {
            CanvasManager.Zoom(400.0 / 40.0);
        }

        public void BtnLayoutAreaTakeout_Click(object sender, EventArgs e)
        {
            if (SystemState.TakeoutAreaMode)
            {
                if (SystemState.DrawingShape)
                {
                    ManagedMessageBox.Show("Complete the current drawing before exiting take-out mode.");
                    return;
                }

                ResetAreaTakeOut();
            }

            else
            {
                if (SystemState.DrawingShape)
                {
                    ManagedMessageBox.Show("Complete the current drawing before going to take-out mode.");
                    return;
                }

                SetAreaTakeOut();
            }
        }

        public void ResetAreaTakeOutControls ()
        {
            ResetAreaTakeOut();
            ResetAreaTakeOutAndFill();
            ResetEmbeddedLayoutAreas();
        }

        public void ResetAreaTakeOut()
        {
            this.btnLayoutAreaTakeout.BackColor = SystemColors.ControlLightLight;
        }

        public void SetAreaTakeOut()
        {
            ResetEmbeddedLayoutAreas();
            ResetAreaTakeOutAndFill();

            this.btnLayoutAreaTakeout.BackColor = Color.Orange;
        }


        public void BtnLayoutAreaTakeOutAndFill_Click(object sender, EventArgs e)
        {
            if (SystemState.TakeoutAreaAndFillMode)
            {
                if (SystemState.DrawingShape)
                {
                    ManagedMessageBox.Show("Complete the current drawing before exiting take-out and fill mode.");
                    return;
                }

                ResetAreaTakeOutAndFill();
            }

            else
            {
                if (SystemState.DrawingShape)
                {
                    ManagedMessageBox.Show("Complete the current drawing before going to take-out and fill mode.");
                    return;
                }

                SetAreaTakeOutAndFill();
            }
        }


        public void ResetAreaTakeOutAndFill()
        {
            this.btnLayoutAreaTakeOutAndFill.BackColor = SystemColors.ControlLightLight;
        }

        public void SetAreaTakeOutAndFill()
        {
            ResetAreaTakeOut();
            ResetEmbeddedLayoutAreas();

            this.btnLayoutAreaTakeOutAndFill.BackColor = Color.Orange;
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            (new AboutForm()).ShowDialog();
        }

        private bool cutsAvailable()
        {
            AreaFinishManager selectedAreaFinishManager = AreaFinishManagerList.SelectedAreaFinishManager;

            foreach (CanvasLayoutArea layoutArea in selectedAreaFinishManager.CanvasLayoutAreas)
            {
                if (layoutArea.GraphicsCutList is null)
                {
                    continue;
                }

                if (layoutArea.GraphicsCutList.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private bool oversAvailable()
        {
            AreaFinishManager selectedAreaFinishManager = AreaFinishManagerList.SelectedAreaFinishManager;

            foreach (CanvasLayoutArea layoutArea in selectedAreaFinishManager.CanvasLayoutAreas)
            {
                if (layoutArea.RolloutList is null)
                {
                    continue;
                }

                foreach (Rollout rollout in layoutArea.RolloutList)
                {
                    if (rollout.OverageList is null)
                    {
                        continue;
                    }

                    if (rollout.OverageList.Count > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool undersAvailable()
        {
            AreaFinishManager selectedAreaFinishManager = AreaFinishManagerList.SelectedAreaFinishManager;

            foreach (CanvasLayoutArea layoutArea in selectedAreaFinishManager.CanvasLayoutAreas)
            {
                if (layoutArea.UndrageList is null)
                {
                    continue;
                }
               
                if (layoutArea.UndrageList.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }


        private void nudFixedWidthFeet_ValueChanged(object sender, EventArgs e)
        {
            AreaFinishBase areaFinishBase = areaPalette.SelectedFinish.AreaFinishBase;

            int fixedWidthInches = (int) nudFixedWidthFeet.Value * 12 + (int) nudFixedWidthInches.Value;

            if (fixedWidthInches > areaFinishBase.RollWidthInInches)
            {
                MessageBoxAdv.Show(
                    "Cannot set fixed width greater than roll width."
                    , "Fixed Width Greater Than Roll Width"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Error);

                int feet = areaFinishBase.FixedWidthInches / 12;

                this.nudFixedWidthFeet.Value = feet;

                return;
            }

            double widthInScaleInches = FixedWidthScaleInInches() ;

            areaFinishBase.FixedWidthInches = fixedWidthInches;

            double widthInLocalInches = widthInScaleInches / currentPage.DrawingScaleInInches;

            CanvasManager.BorderManager.ResetWidth(widthInLocalInches);
        }

        private void nudFixedWidthInches_ValueChanged(object sender, EventArgs e)
        {
            AreaFinishBase areaFinishBase = areaPalette.SelectedFinish.AreaFinishBase;

            int fixedWidthInches = (int)nudFixedWidthFeet.Value * 12 + (int)nudFixedWidthInches.Value;

            if (fixedWidthInches > areaFinishBase.RollWidthInInches)
            {
                MessageBoxAdv.Show(
                   "Cannot set fixed width greater than roll width."
                   , "Fixed Width Greater Than Roll Width"
                   , MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Error);


                int inch = areaFinishBase.FixedWidthInches % 12;

                this.nudFixedWidthInches.Value = inch;

                return;
            }

            double widthInLocalInches = FixedWidthScaleInLocalInches();

            areaFinishBase.FixedWidthInches = fixedWidthInches;

            CanvasManager.BorderManager.ResetWidth(widthInLocalInches);
        }

        public double FixedWidthScaleInInches()
        {
            return (12.0 * (double)nudFixedWidthFeet.Value) + (double)nudFixedWidthInches.Value;
        }

        public double FixedWidthScaleInLocalInches()
        {
            return FixedWidthScaleInInches() / currentPage.DrawingScaleInInches;
        }

        private KeyboardAndMouseActionsForm fixedShortcutPanel = null;

        private void btnKeyboardAndMouseActions_Click(object sender, EventArgs e)
        {
            if (fixedShortcutPanel != null)
            {
                fixedShortcutPanel.WindowState = FormWindowState.Normal;
            }

            else
            {
                fixedShortcutPanel = new KeyboardAndMouseActionsForm();

                fixedShortcutPanel.FormClosed += FixedShortcutPanel_FormClosed;

                fixedShortcutPanel.Show(this);
            }

            fixedShortcutPanel.BringToFront();

            this.btnKeyboardAndMouseActions.Checked = true;

            this.fixedShortcutPanel = null;
        }

        private void FixedShortcutPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.btnKeyboardAndMouseActions.Checked = false;
        }

        //private bool withinOpenProject = false;

        /// <summary>
        /// Responds to a new project button click. Loads a new project.
        /// The definition is a bit strange: Loading a new project means loading
        /// a png image to start a new project.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewProject_Click(object sender, EventArgs e)
        {
            while (withinAutosave)
            {
                System.Threading.Thread.Sleep(10);
            }

            withinProjectAccess = true;

            lock (AutosaveLock)
            {
                doNewProject();
            }

            withinProjectAccess = false;
        }

        private bool doNewProject()
        {
            this.tsmFile.HideDropDown();

            DialogResult dr;

            if (SystemState.ScaleHasBeenSet && currentPage.UnseamedAreasExist())
            {
                dr = MessageBoxAdv.Show(
                    "Some areas have not been seamed, do you want to continue to open a new project?"
                    , "Open New Project"
                    , MessageBoxAdv.Buttons.YesNo
                    , MessageBoxAdv.Icon.Question);

                if (dr != DialogResult.Yes)
                {
                    return false;
                }
            }

            if (CanvasManager.DrawingInProgress())
            {
                NewProjectWarning newProjectWarningForm = new NewProjectWarning();

                newProjectWarningForm.StartPosition = FormStartPosition.CenterParent;

                dr = newProjectWarningForm.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    return false;
                }
            }

            bool cancel = false;

            cancel = CheckCloseEditForms();

            if (cancel)
            {
                return false;
            }
           
            cancel = SaveExistingProject(false);

            if (cancel)
            {
                return false;
            }

            string drawingPathName = CurrentDrawingPath;

            string drawingFullFileName = string.Empty;

            if (string.IsNullOrEmpty(drawingPathName))
            {
                drawingFullFileName = RegistryUtils.GetRegistryStringValue("DrawingFileName", string.Empty);

                if (string.IsNullOrEmpty(drawingFullFileName))
                {
                    drawingPathName = Path.Combine(Program.OCEOperatingDataFolder, "Drawings");
                }

                else
                {
                    drawingPathName = Path.GetDirectoryName(drawingFullFileName);
                }
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = drawingPathName;

            openFileDialog.Filter = "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|BMP files (*.bmp)|*.bmp|DWG files (*.dwg)|*.dwg";
            openFileDialog.FilterIndex = 1;
            openFileDialog.DefaultExt = "png";
            openFileDialog.FileName = "";

            dr = openFileDialog.ShowDialog();

            if (dr == DialogResult.Cancel)
            {
                return false;
            }

            drawingFullFileName = openFileDialog.FileName;

            if (string.IsNullOrEmpty(drawingFullFileName))
            {
                return false;
            }

            CleanupOldProject();

            cancel = CanvasManager.LoadDrawing(drawingFullFileName);

            if (cancel)
            {
                return false;
            }



            if (SystemState.BtnTapeMeasureChecked)
            {
                CanvasManager.TapeMeasureController.CancelTapeMeasure();
            }


            RegistryUtils.SetRegistryValue("DrawingFileName", drawingFullFileName);

            CurrentDrawingName = Path.GetFileNameWithoutExtension(drawingFullFileName);
            CurrentDrawingPath = Path.GetDirectoryName(drawingFullFileName);

            CurrentProjectName = CurrentDrawingName;
            CurrentProjectPath = string.Empty;
            CurrentProjectFullPath = string.Empty;

            setProjNameLabel(this.lblProjectName.Text = CurrentProjectName);

            setProjectPath(Utilities.FilePathSummary(drawingFullFileName, 4, 1));
            setOriginalImage(drawingFullFileName.ToString());

            SystemGlobals.OriginalImagePath = drawingFullFileName;

            LoadDefaultFinishes();

            ClearAllFilters();

            PrepareNewProject();

  //          this.ckbShowAreaModeSeams.Checked = true;

            VisioInterop.SetGridSpacing(currentPage, 0.25, 0.25);
            VisioInterop.SetGridOrigin(currentPage, 0, 0);

            CanvasManager.PanAndZoomController.ZoomToFit();
            //CanvasManager.PanAndZoomController.CenterDrawing();

            SystemState.InitializeSeamStateForExistingProject = false;

            return true;
        }

        private void setProjectPath(string projectPath)
        {
            if (!string.IsNullOrEmpty(projectPath))
            {
                projectPath = "Project Path: " + projectPath;
            }
            

            Size projectPathSize = Utilities.MeasureString(projectPath, this.tlsProjectPath.Font);

            int fillerWidth = this.Width - projectPathSize.Width - this.pnlAreaCommandPane.Width - 64
                - tlsSystemInfoFiller1.Width
                - tlsCursorTitle.Width
                - tlsCursorPosition.Width
                - tlsSystemInfoFiller2.Width
                - tlsDrawoutLengthTitle.Width
                - tlsDrawoutLength.Width
                - 250;

            tlsSystemInfoFiller3.Width = fillerWidth;

            this.tlsProjectPath.Text = projectPath;
        }

        private void setOriginalImage(string originalImagePath)
        {
            if (string.IsNullOrEmpty(originalImagePath))
            {
                this.tlsImageName.Text = string.Empty;
                return;
            }

            string originalImageFileName = Path.GetFileName(originalImagePath);

            this.tlsImageName.Text = "Original Image: " + originalImageFileName;
        }

        private void btnNewBlankProject_Click(object sender, EventArgs e)
        {
            while (withinAutosave)
            {
                System.Threading.Thread.Sleep(10);
            }

            withinProjectAccess = true;

            lock (AutosaveLock)
            {
                doNewBlankProject();
            }

            withinProjectAccess = false;
        }

        private void doNewBlankProject()
        {
            this.tsmFile.HideDropDown();

            DialogResult dr;

            if (SystemState.ScaleHasBeenSet && currentPage.UnseamedAreasExist())
            {
                dr = MessageBoxAdv.Show(
                    "Some areas have not been seamed, do you want to continue to open a new blank project?"
                    , "Open New Project"
                    , MessageBoxAdv.Buttons.YesNo
                    , MessageBoxAdv.Icon.Question);

                if (dr != DialogResult.Yes)
                {
                    return;
                }
            }


            if (CanvasManager.DrawingInProgress())
            {
                NewBlankProjectWarning newBlankProjectWarningForm = new NewBlankProjectWarning();

                newBlankProjectWarningForm.StartPosition = FormStartPosition.CenterParent;

                dr = newBlankProjectWarningForm.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    return;
                }
            }

            bool cancel = false;

            cancel = CheckCloseEditForms();

            if (cancel)
            {
                return;
            }

            cancel = SaveExistingProject(false);

            if (cancel)
            {
                return;
            }

            if (SystemState.BtnTapeMeasureChecked)
            {
                CanvasManager.TapeMeasureController.CancelTapeMeasure();
            }

            CleanupOldProject();

            LoadDefaultFinishes();

            ClearAllFilters();

            PrepareNewProject();

            //this.ckbShowAreaModeSeams.Checked = true;

            //CanvasManager.PanAndZoomController.SetPageSize(currentPage.PageWidth, currentPage.PageHeight);

            VisioInterop.SetGridSpacing(currentPage, 0.25, 0.25);
            VisioInterop.SetGridOrigin(currentPage, 0, 0);

            CanvasManager.PanAndZoomController.SetPageSize(20, 12);

            CanvasManager.PanAndZoomController.ZoomToFit();

            SystemState.InitializeSeamStateForExistingProject = false;

        }

        private void CleanupOldProject()
        {
         
            ContractCanvas();

            ResetAreaTakeOutControls();

            CanvasManager.ScaleRuleController.CancelScaleLine();

            CanvasManager.TapeMeasureController.CancelTapeMeasure();

            //-------------------------------------------------------------------------------------------------//
            // The following closes the filter and editor forms if they are open. This is necessary
            // because those forms subscribe to events that depend on the definition of the area line and
            // seam finishes, which now may have changed. Closing these forms cause the forms to unsubscribe
            // to these events. Otherwise, memory leaks may occur
            //-------------------------------------------------------------------------------------------------//

            //DebugSupportRoutines.FinishBaseListEvents(AreaFinishBaseList);


            if (AreaFilterForm != null)
            {
                AreaFilterForm.Close();
            }

            this.BtnFilterAreas.Checked = false;


            if (LineFilterForm != null)
            {
                LineFilterForm.Close();
            }

            this.BtnFilterLines.Checked = false;

            //DebugSupportRoutines.FinishBaseListEvents(AreaFinishBaseList);


            //-------------------------------------------------------------------------------------------------//


            CanvasManager.SetMeasuringStickState(false);
            CanvasManager.UpdateSetScaleTooltip(false);

            DestroyMeasuringStick();

            if (Utilities.IsNotNull(CanvasManager.BuildingPolyline))
            {
                CanvasManager.BuildingPolyline.Delete();
            }

            CanvasManager.BuildingPolyline = null;
            CanvasManager.LineModeBuildingLine = null;
            SystemState.DrawingMode = DrawingMode.Default;
            SystemState.DrawingShape = false;

            CanvasEraser canvasEraser = new CanvasEraser(this);


            canvasEraser.ClearCurrentCanvas();

            CanvasManager.CurrentPage.PageHeight = GlobalSettings.DefaultNewDrawingHeightInInches;
            CanvasManager.CurrentPage.PageWidth = GlobalSettings.DefaultNewDrawingWidthInInches;

            this.CurrentPage.SetPageSizeToCurrentSize();


            CloseEditForms();

            if (Utilities.IsNotNull(OversUndersForm))
            {
                OversUndersForm.Close();
                OversUndersForm = null;
            }

            if (Utilities.IsNotNull(SummaryReportForm))
            {
                SummaryReportForm.Close();
                SummaryReportForm = null;
            }

            if (Utilities.IsNotNull(OversUndersForm))
            {
                OversUndersForm.Close();
                OversUndersForm = null;
            }


            if (CanvasManager.CounterController.CountersActivated)
            {
                CanvasManager.CounterController.ToggleCountersActivation();
            }
            
            CanvasManager.CounterController.ResetCounters();

            CanvasManager.LabelManager.DeActivateLabels();
            CanvasManager.LabelManager.ResetLabelList();

            areaPalette.ClearTallyDisplays();
            linePalette.ClearTallyDisplays();
            seamPalette.ClearTallyDisplays();

            this.btnAreaDesignStateZeroLine.BackColor = SystemColors.ControlLightLight;

            currentPage.ScaleHasBeenSet = false;
             

            CanvasManager.FieldGuideController.Init(GlobalSettings.FieldGuideColor, GlobalSettings.FieldGuideOpacity, GlobalSettings.FieldGuideWidthInPts, GlobalSettings.FieldGuideStyle);


            currentPage.SetDefaultScale(GlobalSettings.DefaultDrawingScaleInInches);

            setProjNameLabel("");

            setProjectPath("");
            setOriginalImage("");

            SystemGlobals.ShowAreaLegendInAreaMode = false;
            SystemGlobals.ShowAreaLegendInLineMode = false;
            SystemGlobals.ShowLineLegendInAreaMode = false;
            SystemGlobals.ShowLineLegendInLineMode = false;

            areaPalette.Clear();
            linePalette.Clear();
            seamPalette.Clear();

            SystemState.DesignState = DesignState.Unknown;

            Undrage.UndrageIndexSet.Clear();
            Overage.OverageIndexSet.Clear();
        }

        private void LoadDefaultFinishes()
        {
            tbcPageAreaLine.Hide();

            //AreaFilters.SeamFilter = CanvasLib.Filters.SeamFilter.Show;

            loadFinishPalettesFromDefaults();

            tbcPageAreaLine.Show();
        }

        private void ClearAllFilters()
        {
            areaPalette.ClearAllFilters();
            linePalette.ClearAllFilters();
            seamPalette.ClearAllFilters();
        }

        private bool LoadSetupFromOtherSource()
        {
            DialogResult dr;

            if (SystemState.DrawingShape || SystemState.DrawingMode != DrawingMode.Default)
            {
                MessageBoxAdv.Show(
                    "Cannot load palette definitions while drawing shapes."
                    , "Cannot Load Palette Definitions"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Error);

                return false;
            }

            if (AreaFinishManagerList.CanvasAreasDefinined() || LineFinishManagerList.LinesAreaDefined())
            {
                MessageBoxAdv.Show(
                   "Cannot load palette definitions once canvas layout area or lines have been defined."
                   , "Cannot Load Palette Definitions"
                   , MessageBoxAdv.Buttons.OK
                   , MessageBoxAdv.Icon.Error);

                return false;
            }

            string setupFullFileName = RegistryUtils.GetRegistryStringValue("SetupFullFilePath", string.Empty);

            OpenFileDialog openFileDialog = new OpenFileDialog();

            try
            {
                openFileDialog.InitialDirectory = setupFullFileName;
            }

            catch { }

            openFileDialog.Filter = "Project setup files (*.sproj)|*.sproj|Project files (*.eproj)|*.eproj";
            openFileDialog.FilterIndex = 1;
            openFileDialog.DefaultExt = "sproj";

            if (!string.IsNullOrEmpty(setupFullFileName))
            {
                try
                {
                    openFileDialog.FileName = Path.GetFileName(setupFullFileName);
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(setupFullFileName);
                }

                catch { }
            }

            dr = openFileDialog.ShowDialog();

            if (dr == DialogResult.Cancel)
            {
                return false;
            }

            setupFullFileName = openFileDialog.FileName;

            RegistryUtils.SetRegistryValue("SetupFullFilePath", setupFullFileName);

            if (string.IsNullOrEmpty(setupFullFileName))
            {
                return false;
            }

            setProjNameLabel(Path.GetFileNameWithoutExtension(setupFullFileName));

            string extension = Path.GetExtension(setupFullFileName);

            ProjectSetupImporter projectSetupImporter = new ProjectSetupImporter(this);

            string result = projectSetupImporter.ImportProjectSetup(setupFullFileName);

            return true;
        }

        private void PrepareNewProject()
        {

            // The first time the seaming tool is displayed for a project it is centered in the current view.
            // This is to guarantee that it will be visible, since the user may zoom or pan before first showing.

            //CanvasManager.SeamingTool.FirstTimeDisplay = true;

            CanvasManager.HideSeamingTool();

            CurrentProjectChanged = false;

            ContractCanvas();

            //ResetAreaTakeOut();

            //CanvasManager.PanAndZoomController.CenterDrawing();

            if (!CurrentPage.ScaleHasBeenSet)
            {
                areaPalette.ClearTallyDisplays();
                linePalette.ClearTallyDisplays();
                seamPalette.ClearTallyDisplays();
            }

            this.btnColorOnly.BackColor = SystemColors.ControlLightLight;
            this.btnFixedWidth.BackColor = SystemColors.ControlLightLight;
            this.btnNormalLayoutArea_Click(null, null);

            if (!CurrentPage.ScaleHasBeenSet && GlobalSettings.ShowSetScaleReminder)
            {
                SetScaleWarningForm setScaleWarningForm = new SetScaleWarningForm();

                setScaleWarningForm.ShowDialog();
            }

            int fixedWidthInches = areaPalette.SelectedFinish.AreaFinishBase.FixedWidthInches;

            this.nudFixedWidthFeet.Value = fixedWidthInches / 12;
            this.nudFixedWidthInches.Value = fixedWidthInches % 12;

            this.AreaFinishBaseList.SelectElem(0);

            this.lblProjectName.Focus();

            SystemState.DrawingMode = DrawingMode.Default;

            if (GlobalSettings.InitProjectDesignState == 2)
            {
                Utilities.SetTabSelectedIndex(this.tbcPageAreaLine, FloorMaterialEstimatorBaseForm.tbcLineModeIndex, ref AllowTabSelection);
                DesignState = DesignState.Line;
            }
            else
            {
                Utilities.SetTabSelectedIndex(this.tbcPageAreaLine, FloorMaterialEstimatorBaseForm.tbcAreaModeIndex, ref AllowTabSelection);
                SystemState.DesignState = DesignState.Area;
            }

            if (CanvasManager.CounterController != null)
            {
                CanvasManager.CounterController.SelectedCounterIndex = -1;
            }

            CanvasManager.SeamingTool.CenterInView();

            CanvasManager.ShowSeamingToolOnSwitchToSeamMode = false;

            this.btnCopyAndPasteShapes.BackColor = SystemColors.ControlLightLight;

            SetupAreaSeamsState();

            CanvasManager.LabelManager = new LabelManager(AreaFinishManagerList, this, CanvasManager.Window, CanvasManager.Page, btnShowLabelEditor);

            CanvasManager.LockIcon.HideLockIcon();

            //CanvasManager.CanvasLayoutArea lockIconLayoutArea = null;

            CanvasManager.ShowUnseamedAreas = false;

            CanvasManager.ShowZeroAreas = false;

            CanvasManager.ShowAreasInLineMode = false;

            CanvasManager.ShowSeamsInLineMode = false;
        }


        public bool EmbedLayoutAreas => this.btnEmbeddLayoutAreas.BackColor == Color.Orange;

        public void BtnEmbeddLayoutAreas_Click(object sender, EventArgs e)
        {
            if (this.btnEmbeddLayoutAreas.BackColor == Color.Orange)
            {
                ResetEmbeddedLayoutAreas();
            }

            else
            {
                if (SystemState.DrawingShape)
                {
                    ManagedMessageBox.Show("Complete the current drawing before exiting take-out and fill mode.");
                    return;
                }

                SetEmbeddedLayoutAreas();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            bool completeCurrentOperation = false;

            bool warningIssued = false;

            if (CanvasManager.DrawingInProgress())
            {
                ExitProgramWarning exitWarningForm = new ExitProgramWarning();

                exitWarningForm.StartPosition = FormStartPosition.CenterParent;

                DialogResult dr = exitWarningForm.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    return;
                }

                warningIssued = true;

                completeCurrentOperation = true;
            }

            if (!CurrentProjectChanged)
            {
                DialogResult dr1 = MessageBoxAdv.Show(
                    "Are you sure you want to exit the application?"
                    , "Confirm Application Exit"
                    , MessageBoxAdv.Buttons.YesNo
                    , MessageBoxAdv.Icon.Question);

                if (dr1 == DialogResult.No)
                {
                    return;
                }
            }

            bool cancel = SaveExistingProject(false);

            if (cancel)
            {
                return;
            }

            try
            {
             
                VisioInterop.Exit();
                Process.GetCurrentProcess().Kill();
                System.Environment.Exit(0);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Console.WriteLine($"COM Exception: {ex.Message}");
                Console.WriteLine($"HRESULT: {ex.HResult}");
            }

        }

        public void ResetEmbeddedLayoutAreas()
        {
            this.btnEmbeddLayoutAreas.BackColor = SystemColors.ControlLightLight;
        }

        public void SetEmbeddedLayoutAreas()
        {
            this.btnEmbeddLayoutAreas.BackColor = Color.Orange;

            ResetAreaTakeOut();
            ResetAreaTakeOutAndFill();
        }


        private void FloorMaterialEstimatorBaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool completeCurrentOperation = false;

            bool warningIssued = false;

            if (CanvasManager.DrawingInProgress())
            {
                ExitProgramWarning exitWarningForm = new ExitProgramWarning();

                exitWarningForm.StartPosition = FormStartPosition.CenterParent;
              
                DialogResult dr = exitWarningForm.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }

                warningIssued= true;

                completeCurrentOperation = true;
            }

            if (!CurrentProjectChanged)
            {
                DialogResult dr1 = MessageBoxAdv.Show(
                    "Are you sure you want to exit the application?"
                    , "Confirm Application Exit"
                    , MessageBoxAdv.Buttons.YesNo
                    , MessageBoxAdv.Icon.Question);

                if (dr1 == DialogResult.No)
                {
                    e.Cancel= true;
                }

                return;
            }

            bool cancel = SaveExistingProject(false);

            if (cancel)
            {
                e.Cancel = true;

                return;
            }
            try
            {
            
                VisioInterop.Exit();
                Process.GetCurrentProcess().Kill();
                System.Environment.Exit(0);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Console.WriteLine($"COM Exception: {ex.Message}");
                Console.WriteLine($"HRESULT: {ex.HResult}");
            }

        }

        private void btnAlignTool_Click(object sender, EventArgs e)
        {
            CanvasManager.SeamingToolAlign_Click(null, null);
        }

        private void btnSeamSingleLineToTool_Click(object sender, EventArgs e)
        {
            CanvasManager.SeamingToolSeamSingleLineToTool_Click(null, null);
        }


        private void btnSeamArea_Click(object sender, EventArgs e)
        {
            CanvasManager.SeamingToolAreaSeam_Click(null, null);
        }
      
        private void btnSubdivideRegion_Click(object sender, EventArgs e)
        {
            CanvasManager.SeamingToolSubdivideRegion_Click(null, null);
        }

        //------------------------------------------------------------------------------------//
        // Note: in the following, the radio buttons that control the display of the various  //
        // seam types are coordinated between the area mode display and the seam mode display //
        //                                                                                    //
        // The radio button state for radio buttons in seam mode are the ones that are        //
        // actually used for displaying or not displaying seams                               //
        //------------------------------------------------------------------------------------//


        private bool inSeamDisplayCoordination = false;

        private void SetupSeamDisplays(
            RadioButton coordinatingRadioButton   // This is the radio button that has changed
            , RadioButton coordinatedRadioButton) // This is the radio button that has to be coordinated with.
        {
            if (!coordinatingRadioButton.Checked)
            {
                return;
            }

            if (inSeamDisplayCoordination)
            {
                return;
            }

            inSeamDisplayCoordination = true;

            coordinatedRadioButton.Checked = true; // Force coordination with coordianted mode display

            CanvasManager.SetupAllSeamStateSeamLayersForSelectedArea();

            inSeamDisplayCoordination = false;
        }

        #region auto seams coordination and display

        private void rbnSeamModeAutoSeamsShowAll_CheckedChanged(object sender, EventArgs e)
        {
            SetupSeamDisplays(RbnSeamModeAutoSeamsShowAll, RbnAreaModeAutoSeamsShowAll);
        }

        private void rbnAreaModeAutoSeamsShowAll_CheckedChanged(object sender, EventArgs e)
        {
            SetupSeamDisplays(RbnAreaModeAutoSeamsShowAll, RbnSeamModeAutoSeamsShowAll);
        }

        private void rbnSeamModeAutoSeamsHideAll_CheckedChanged(object sender, EventArgs e)
        {
            SetupSeamDisplays(RbnSeamModeAutoSeamsHideAll, RbnAreaModeAutoSeamsHideAll);
        }

        private void rbnAreaModeAutoSeamsHideAll_CheckedChanged(object sender, EventArgs e)
        {
            SetupSeamDisplays(RbnAreaModeAutoSeamsHideAll, RbnSeamModeAutoSeamsHideAll);
        }

        private void rbnSeamModeAutoSeamsShowUnHideable_CheckedChanged(object sender, EventArgs e)
        {
            SetupSeamDisplays(RbnSeamModeAutoSeamsShowUnHideable, RbnAreaModeAutoSeamsShowUnHideable); // MDD Check Here
        }

        private void rbnAreaModeAutoSeamsShowUnHideable_CheckedChanged(object sender, EventArgs e)
        {
            SetupSeamDisplays(RbnAreaModeAutoSeamsShowUnHideable, RbnSeamModeAutoSeamsShowUnHideable);
        }

        #endregion


        #region manual seams coordination and display

        private void rbnSeamModeManualSeamsShowAll_CheckedChanged(object sender, EventArgs e)
        {
            SetupSeamDisplays(RbnSeamModeManualSeamsShowAll, RbnAreaModeManualSeamsShowAll);
        }

        private void rbnAreaModeManualSeamsShowAll_CheckedChanged(object sender, EventArgs e)
        {
            SetupSeamDisplays(RbnAreaModeManualSeamsShowAll, RbnSeamModeManualSeamsShowAll);
        }

        private void rbnSeamModeManualSeamsHideAll_CheckedChanged(object sender, EventArgs e)
        {
            SetupSeamDisplays(RbnSeamModeManualSeamsHideAll, RbnAreaModeManualSeamsHideAll);
        }

        private void rbnAreaModeManualSeamsHideAll_CheckedChanged(object sender, EventArgs e)
        {
            SetupSeamDisplays(RbnAreaModeManualSeamsHideAll, RbnSeamModeManualSeamsHideAll);
        }

        #endregion

        //private void rbnManualSeamsShowUnhideable_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.rbnManualSeamsShowUnhideable.Checked)
        //    {
        //        CanvasManager.SetupAllSeamStateSeamLayersForSelectedArea();
        //    }
        //}


        private void ckbShowCuts_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.SetupAllSeamStateSeamLayersForSelectedArea();

            //areaPalette.SetupCutsLayersForSelectedFinish(this.ckbShowSeamModeCuts.Checked);
        }


        private void ckbShowSeamModeCutIndices_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.SetupAllSeamStateSeamLayersForSelectedArea();

            //areaPalette.SetupCutIndicesLayersForSelectedFinish(this.ckbShowSeamModeCutIndices.Checked);

            // The following should not be needed. But without it, when the show cut index is checked,
            // the indices appear with the circles regardsless of other considerations. Some day will 
            // figure it out.

            //foreach (AreaFinishManager areaFinishManager in this.AreaFinishElementList)
            //{
            //    areaFinishManager.SetCutIndexCircleVisibility();
            //}
        }

        private void ckbShowOvers_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.SetupAllSeamStateSeamLayersForSelectedArea();

           // areaPalette.SetupOversLayersForSelectedFinish(this.ckbShowSeamModeOvers.Checked);
        }

        private void ckbShowUnders_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.SetupAllSeamStateSeamLayersForSelectedArea();


            //foreach (AreaFinishManager areaFinishManager in this.AreaFinishElementList)
            //{
            //    areaFinishManager.SetCutIndexCircleVisibility();
            //}
        }

        private void ckbEmbeddedOvers_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.SetupAllSeamStateSeamLayersForSelectedArea();
        }

        private void ckbShowEmbeddedCuts_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.SetupAllSeamStateSeamLayersForSelectedArea();
        }

        private void btnSeamElementsShowAll_Click(object sender, EventArgs e)
        {
            if (!ckbShowSeamModeCuts.Checked)
            {
                ckbShowSeamModeCuts.Checked = true;
            }

            if (!ckbShowSeamModeOvers.Checked)
            {
                ckbShowSeamModeOvers.Checked = true;
            }

            if (!ckbShowSeamModeUndrs.Checked)
            {
                ckbShowSeamModeUndrs.Checked = true;
            }

            if (!ckbShowSeamModeCutIndices.Checked)
            {
                ckbShowSeamModeCutIndices.Checked = true;
            }
        }

        private void btnSeamElementsSelectNone_Click(object sender, EventArgs e)
        {
            if (ckbShowSeamModeCuts.Checked)
            {
                ckbShowSeamModeCuts.Checked = false;
            }

            if (ckbShowSeamModeOvers.Checked)
            {
                ckbShowSeamModeOvers.Checked = false;
            }

            if (ckbShowSeamModeUndrs.Checked)
            {
                ckbShowSeamModeUndrs.Checked = false;
            }

            if (ckbShowSeamModeCutIndices.Checked)
            {
                ckbShowSeamModeCutIndices.Checked = false;
            }
        }


        //private void ckbShowEmbeddedOvers_CheckedChanged(object sender, EventArgs e)
        //{
        //    areaPalette.SetupEmbdOverLayerForSelectedFinish(this.ckbShowSeamModeOvers.Checked);
        //}

        ////private void ckbNormalLayoutArea_Click(object sender, EventArgs e)
        ////{
        ////    if (ckbNormalLayoutArea.Checked)
        ////    {
        ////        return;
        ////    }

        ////    if (ckbNormalLayoutArea.Checked)
        ////    {
        ////        ckbFixedWidth.Checked = false;
        ////        ckbColorOnly.Checked = false;
        ////        ckbOversGenerator.Checked = false;
        ////    }
        ////}

        private void SetLayoutAreaSelectedColor(RadioButton radioButton, PictureBox pbxBackground = null)
        {
            radioButton.BackColor = radioButton.Checked ? Color.Orange : SystemColors.ControlLightLight;

            if (pbxBackground != null)
            {
                pbxBackground.BackColor = radioButton.Checked ? Color.Orange : SystemColors.ControlLightLight;
            }
        }

        private void btnNormalLayoutArea_Click(object sender, EventArgs e)
        {
            if (SystemState.DrawingShape && SystemState.CurrentLayoutType != LayoutAreaType.Normal)
            {
                MessageBoxAdv.Show("Cannot change layout type while drawing is in progress", "Drawing In Progress", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Exclamation);

                return;
            }
            
            SystemState.CurrentLayoutType = LayoutAreaType.Normal;

            setupLayoutAreaDisplayElements();
        }

        private void btnColorOnly_Click(object sender, EventArgs e)
        {
            if (SystemState.DrawingShape && SystemState.CurrentLayoutType != LayoutAreaType.ColorOnly)
            {
                MessageBoxAdv.Show("Cannot change layout type while drawing is in progress", "Drawing In Progress", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Exclamation);

                return;

            }

            SystemState.CurrentLayoutType = LayoutAreaType.ColorOnly;

            setupLayoutAreaDisplayElements();
        }

        private void btnFixedWidth_Click(object sender, EventArgs e)
        {
            if (SystemState.DrawingShape && SystemState.CurrentLayoutType != LayoutAreaType.FixedWidth)
            {
                MessageBoxAdv.Show("Cannot change layout type while drawing is in progress", "Drawing In Progress", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Exclamation);

                return;
            }

            if (!CurrentPage.ScaleHasBeenSet)
            {
                MessageBoxAdv.Show("Fixed width can not be used until a drawing scale has been set", "Scale Not Set", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Exclamation);

                return;
            }

            double widthInScaleInches = FixedWidthScaleInInches();

            SystemState.CurrentLayoutType = LayoutAreaType.FixedWidth;

            double widthInLocalInches = widthInScaleInches / currentPage.DrawingScaleInInches;

            CanvasManager.BorderManager.Init(widthInLocalInches, this.selectedAreaFinishManager.AreaFinishBase, lblFixedWidthJump);

            SystemState.DrawingMode = DrawingMode.Default;


            setupLayoutAreaDisplayElements();
        }

        private void btnOversGenerator_Click(object sender, EventArgs e)
        {
            if (SystemState.DrawingShape && SystemState.CurrentLayoutType != LayoutAreaType.OversGenerator)
            {
                MessageBoxAdv.Show("Cannot change layout type while drawing is in progress", "Drawing In Progress", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Exclamation);

                return;
            }



            SystemState.CurrentLayoutType = LayoutAreaType.OversGenerator;


            setupLayoutAreaDisplayElements();
        }

        private void setupLayoutAreaDisplayElements()
        {
            this.btnNormalLayoutArea.BackColor = SystemState.CurrentLayoutType == LayoutAreaType.Normal ? Color.Orange : SystemColors.ControlLightLight;
            this.btnColorOnly.BackColor = SystemState.CurrentLayoutType == LayoutAreaType.ColorOnly ? Color.Orange : SystemColors.ControlLightLight;
            this.btnFixedWidth.BackColor = SystemState.CurrentLayoutType == LayoutAreaType.FixedWidth ? Color.Orange : SystemColors.ControlLightLight;
            this.btnOversGenerator.BackColor = SystemState.CurrentLayoutType == LayoutAreaType.OversGenerator ? Color.Orange : SystemColors.ControlLightLight;

            this.shlNormalLayoutArea.HighLight(SystemState.CurrentLayoutType == LayoutAreaType.Normal);
            this.shlColorOnly.HighLight(SystemState.CurrentLayoutType == LayoutAreaType.ColorOnly);
            this.shlFixedWidth.HighLight(SystemState.CurrentLayoutType == LayoutAreaType.FixedWidth);
            this.shlOversGenerator.HighLight(SystemState.CurrentLayoutType == LayoutAreaType.OversGenerator);

            foreach (Tuple<Control, LayoutAreaType> areaTypeControlSpec in LayoutAreaControlList)
            {
                Control layoutAreaControl = areaTypeControlSpec.Item1;
                LayoutAreaType layoutAreaType = areaTypeControlSpec.Item2;

                if (layoutAreaType == SystemState.CurrentLayoutType)
                {
                    layoutAreaControl.ForeColor = Color.Orange;
                }

                else
                {
                    layoutAreaControl.ForeColor = Color.Black;
                }
            }
        }


        //private void resetLayoutAreaTypeRadioButtons()
        //{


        //    ignoreRbnFixedWidth_CheckedChanged = true;
        //    ignoreRbnOversGenerator_CheckedChanged = true;

        //    if (SystemState.CurrentLayoutType == LayoutAreaType.Normal)
        //    {
        //        btnNormalLayoutArea.BackColor = Color.Orange;
        //        btnColorOnly.BackColor = SystemColors.ControlLightLight;
        //    }

        //    else if (SystemState.CurrentLayoutType == LayoutAreaType.ColorOnly)
        //    {
        //        btnNormalLayoutArea.BackColor = SystemColors.ControlLightLight;
        //        btnColorOnly.BackColor = Color.Orange;
        //    }

        //    else if (SystemState.CurrentLayoutType == LayoutAreaType.FixedWidth)
        //    {
        //        rbnFixedWidth.Checked = true;
        //    }

        //    else if (SystemState.CurrentLayoutType == LayoutAreaType.OversGenerator)
        //    {
        //        rbnOversGenerator.Checked = true;
        //    }

        //    ignoreRbnFixedWidth_CheckedChanged = false;
        //    ignoreRbnOversGenerator_CheckedChanged = false;
        //}

        private void ckbShowAreaModeSeams_CheckedChanged(object sender, EventArgs e)
        {
            SetupAreaSeamsState();

            // The following should not be needed. But without it, when the show cut index is checked,
            // the indices appear with the circles regardsless of other considerations. Some day will 
            // figure it out.

            //foreach (AreaFinishManager areaFinishManager in this.AreaFinishManagerList)
            //{
            //    areaFinishManager.SetCutIndexCircleVisibility();
            //}
        }

        private void ckbShowAreaModeCutIndices_CheckedChanged(object sender, EventArgs e)
        {
            SetupAreaSeamsState();

            // The following should not be needed. But without it, when the show cut index is checked,
            // the indices appear with the circles regardsless of other considerations. Some day will 
            // figure it out.

            //foreach (AreaFinishManager areaFinishManager in this.AreaFinishManagerList)
            //{
            //    areaFinishManager.SetCutIndexCircleVisibility();
            //}
        }


        private void btnTextBox_Click(object sender, EventArgs e)
        {
            if (SystemState.DrawingMode != DrawingMode.Default)
            {
                MessageBoxAdv.Show(
                    "Please complete the current drawing operation."
                    , "Complete Current Drawing Operation"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Exclamation);

                return;
            }

            SystemState.DrawingMode = DrawingMode.TextBoxCreate;
        }


        private void arrowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SystemState.DrawingMode != DrawingMode.Default)
            {
                MessageBoxAdv.Show(
                    "Please complete the current drawing operation."
                    , "Complete Current Drawing Operation"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Exclamation);
                return;
            }

            SystemState.DrawingMode = DrawingMode.ArrowCreate;
        }


        private void ckbShowAllSeams_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.AreaFinishManagerList.SetupAllSeamLayers();
        }


        //private void rbnShowAllLocks_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!rbnShowAllLocks.Checked)
        //    {
        //        return;
        //    }

        //    selectedAreaFinishManager.ShowLocks();
        //}

        //private void rbnHideAllLocks_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!rbnHideAllLocks.Checked)
        //    {
        //        return;
        //    }

        //    selectedAreaFinishManager.HideLocks();
        //}

        //private void btnDeleteAllLocks_Click(object sender, EventArgs e)
        //{
        //    selectedAreaFinishManager.DeleteLocks();
        //}


        private void btnAreaModeNestShapes_Click(object sender, EventArgs e)
        {
            doNestShapes();
        }

        private void btnSeamModeNestShapes_Click(object sender, EventArgs e)
        {
            doNestShapes();
        }

        private void doNestShapes()
        {
            List<ShapeNestShape> shapeNestShapes = new List<ShapeNestShape>();


            double drawingScaleInInches = this.CurrentPage.DrawingScaleInInches;

            int indxNmbr = 1;

            foreach (var layoutArea in selectedAreaFinishManager.CanvasLayoutAreas)
            {
                var perimeter = layoutArea.Perimeter;

                List<PointF> perimeterPoints = new List<PointF>();

                foreach (var cdl in perimeter)
                {
                    perimeterPoints.Add(new PointF((float) (cdl.Coord1.X * drawingScaleInInches), (float) (cdl.Coord1.Y * drawingScaleInInches)));
                }

                if (perimeterPoints[0] != perimeterPoints[perimeterPoints.Count - 1])
                {
                    perimeterPoints.Add(perimeterPoints[0]);

                }
                int? selectionNmbr = null;
                
                if (layoutArea.SeamDesignStateSelectionModeSelected)
                {
                    var canvasSeamTag = layoutArea.SeamIndexTag;

                    if (canvasSeamTag != null)
                    {
                        selectionNmbr = canvasSeamTag.SeamAreaIndex;
                    }
                }

                Color lineColor = layoutArea.MaxLineType().LineColor;

                Color fillColor = layoutArea.AreaFinishBase.Color;

                ShapeNestShape sns = new ShapeNestShape(perimeterPoints, lineColor, fillColor, selectionNmbr, indxNmbr);

                shapeNestShapes.Add(sns);

                indxNmbr++;
            }

            if (shapeNestShapes.Count <= 0)
            {
                MessageBoxAdv.Show(
                    "There are no shapes available to nest for the currently selected finish."
                    , "No Shape Available"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Error);
                return;
            }

            double rollWidthInInches = selectedAreaFinishManager.RollWidthInInches;

            ShapeNestLib.ShapeNestForm snf = new ShapeNestForm(rollWidthInInches, shapeNestShapes);

           
            snf.ShowDialog(); 
        }

        CanvasManager.RotationBaseForm rotationBaseForm = null;

        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (rotationBaseForm == null)
            {
                rotationBaseForm = new CanvasManager.RotationBaseForm(CanvasManager);

                rotationBaseForm.FormClosed += RotationBaseForm_FormClosed;
                rotationBaseForm.Show();
            }
        }

        private void RotationBaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            rotationBaseForm = null;
        }


        private void ckbShowSeamModeAreaNmbrs_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.SetupAllSeamStateSeamLayersForSelectedArea();

        }
    }


}
