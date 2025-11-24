//--------------------------------------------------------------------------------//
// <copyright file="KeyPressEventManager.cs" company="Bruun Estimating, LLC">    // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

using System.Security.Cryptography;

namespace FloorMaterialEstimator.CanvasManager
{
    using SettingsLib;
    using Globals ;
    using Utilities;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Linq;
    using Geometry;
    using System;
    using CanvasShapes;
    public partial class CanvasManager
    {
        string kys = "";

        bool keyUsedAsShiftKey = false;

        bool isKeyDown = false;

        public bool ShowUnseamedAreas = false;

        public bool ShowZeroAreas = false;

        public bool ShowAreasInLineMode = false;

        public bool ShowSeamsInLineMode = false;

        public void ProcessKeyDown(int keyVal, KeyModifiers modifiers = KeyModifiers.None)
        {
            if (isKeyDown) // MDD Reset
            {
                return;
            }

            isKeyDown = true;

            doProcessKeyDown(keyVal, modifiers);
        }

        public bool ShowSeamsInLineModeInitialized = false;

        public void doProcessKeyDown(int keyVal, KeyModifiers modifiers = KeyModifiers.None)
        {
            if (SystemState.DrawingMode == DrawingMode.TextBoxEdit || SystemState.DrawingMode == DrawingMode.ArrowEdit)
            {
                return;
            }

            if (keyVal == 65 && DesignState == DesignState.Line)
            {
                if (!ShowAreasInLineMode)
                {
                    //CurrentPage.AreaModeGlobalLayer.SetLayerVisibility(true);
                    BaseForm.CanvasManager.AreaFinishManagerList.SetDesignStateFormat(DesignState.Area, SeamMode.Selection, true);

                    ShowAreasInLineMode= true;
                }

                else
                {
                    //CurrentPage.AreaModeGlobalLayer.SetLayerVisibility(false);
                    BaseForm.CanvasManager.AreaFinishManagerList.SetDesignStateFormat(DesignState.Area, SeamMode.Selection, false);

                    ShowAreasInLineMode = false;

                }

                return;
            }

            if (keyVal == 83 && DesignState == DesignState.Line)
            {
                
                if (!ShowSeamsInLineMode)
                {
                    if (!ShowSeamsInLineModeInitialized)
                    {
                        foreach (var x in BaseForm.CanvasManager.AreaFinishManagerList)
                        {
                            foreach (var y in x.CanvasLayoutAreas)
                            {
                                foreach (var z in y.CanvasSeamList)
                                {
                                    z.SetSeamLineWidth(DesignState.Area, false);
                                }
                            }
                        }

                        ShowSeamsInLineModeInitialized = true;
                    }


                    CurrentPage.SeamModeGlobalLayer.SetLayerVisibility(true);

                    foreach (var x in BaseForm.CanvasManager.AreaFinishManagerList)
                    {
                        var x1 = x.AreaFinishLayers.NormalSeamsLayer;
                        x1.SetLayerVisibility(true);
                        var x2 = x.AreaFinishLayers.NormalSeamsUnhideableLayer;

                        x2.SetLayerVisibility(true);
                    }

                    ShowSeamsInLineMode = true;
                }

                else
                {
                    CurrentPage.SeamModeGlobalLayer.SetLayerVisibility(false);

                    foreach (var x in BaseForm.CanvasManager.AreaFinishManagerList)
                    {
                        var x1 = x.AreaFinishLayers.NormalSeamsLayer;
                        x1.SetLayerVisibility(false);
                        var x2 = x.AreaFinishLayers.NormalSeamsUnhideableLayer;

                        x2.SetLayerVisibility(false);
                    }
                    
                    ShowSeamsInLineMode = false;

                }

                return;
            }
            if (keyVal == 189 && DesignState == DesignState.Seam)
            {
                if (SystemState.CkbShowAllSeamsInSeamMode.Checked)
                {
                    SystemState.CkbShowAllSeamsInSeamMode.Checked = false;
                }

                else
                {
                    SystemState.CkbShowAllSeamsInSeamMode.Checked = true;
                }

                return;
            }

            if (keyVal == 27) // Escape
            {
                SystemGlobals.ActivateTapeMeasure();

                return;
            }

            if (keyVal == 17)
            {
                kys = "Ctrl ";

            }
            else if (keyVal == 16)
            {
                kys = "Shift ";
            }


            if (keyVal >= 37 && keyVal <= 40)
            {
                ProcessArrowKey(keyVal - 37);
            }

            int shortcutNumber = ShortcutSettings.MiscStateKeyToActionMapValue(keyVal);

            // General case

            switch (shortcutNumber)
            {
                case 0: PanAndZoomController.ProcessGeneralZoomIn(); return;
                case 1: PanAndZoomController.ProcessGeneralZoomOut(); return;
                case 2: ProcessToggleShowMeasuringStickMode(); return;
                case 3: processShowExtendedCrosshairs(); return;
                case 4: if (DesignState == DesignState.Seam)
                    {
                        CurrentPage.ProcessShowUnseamedAreas(true); ShowUnseamedAreas = true;  return;
                    }
                    break;
                case 5:
                    if (DesignState == DesignState.Area)
                    {
                        CurrentPage.ProcessShowZeroAreas(true); ShowZeroAreas = true; return;
                    }
                    break;
                default: break;
            }

            if (modifiers == KeyModifiers.Alt)
            {
                ProcessAltKeyDown(keyVal);
                BaseForm.lblKeystrokes.Text = "Alt " + ((Keys)keyVal).ToString();
                return;
            }

            if (keyVal == 76) // L
            {
                ProcessTempSwitchToLineMode();

                return;
            }

        }

        int count = 0;

        private void processShowExtendedCrosshairs()
        {
            count++;

            if (!CurrentPage.ExtendedCrosshairsLayer.IsVisible())
            {
                CurrentPage.ExtendedCrosshairsLayer.SetLayerVisibility(true);
            }

            ExtendedCrosshairs.SetCenter(MouseX, MouseY);
        }

        private void ProcessAltKeyDown(int keyVal)
        {
            
            if (keyVal == 65) // A
            {
                BaseForm.BtnAreaDesignState_Click(null, null);

                return;
            }

            else if (keyVal == 76) // L
            {
                BaseForm.BtnLineDesignState_Click(null, null);

                return;
            }

            else if (keyVal == 83) // S
            {
                BaseForm.BtnSeamDesignState_Click(null, null);

                return;
            }

            else if (keyVal == 84) // T
            {
                BaseForm.ToggleDesignStateMode();


                return;
            }
        }


        public void ProcessKeyUp(int keyVal, KeyModifiers modifiers = KeyModifiers.None)
        {
            isKeyDown = false;

            doProcessKeyUp(keyVal, modifiers);
        }

        private void doProcessKeyUp(int keyVal, KeyModifiers modifiers)
        {
            if (CurrentPage.ExtendedCrosshairsLayer.IsVisible())
            {
                CurrentPage.ExtendedCrosshairsLayer.SetLayerVisibility(false);
            }

            if (ShowUnseamedAreas)
            {
                CurrentPage.ProcessShowUnseamedAreas(false);
                ShowUnseamedAreas = false;
            }

            if (ShowZeroAreas)
            {
                CurrentPage.ProcessShowZeroAreas(false);
                ShowZeroAreas = false;
            }
        

            //if (keyVal == 65 && DesignState == DesignState.Line)
            //{
            //    CurrentPage.AreaModeGlobalLayer.SetLayerVisibility(false);
            //    BaseForm.CanvasManager.AreaFinishManagerList.SetDesignStateFormat(DesignState.Area, SeamMode.Selection, false);

            //    return;
            //}

            if (keyVal == 16)
            {
                return;
            }

            if (keyUsedAsShiftKey)
            {
                keyUsedAsShiftKey = false;
                return;
            }

            if (keyVal !=16 && keyVal !=17)
            {
                kys += ((Keys)keyVal).ToString();

                if (kys == "Back")
                {
                    BaseForm.lblKeystrokes.Text = "Backspace";
                }

                else if (kys == "Subtract")
                {
                    BaseForm.lblKeystrokes.Text = "Minus (-)";
                }

                else if (kys == "Add")
                {
                    BaseForm.lblKeystrokes.Text = "Plus (+)";
                }

                else
                {
                    BaseForm.lblKeystrokes.Text = kys;
                }
                
                kys = "";
            }

            if (keyVal == 76) // L
            {
                ProcessTempSwitchFromLineMode();

                return;
            }

            //if (keyVal >= 112 && keyVal <= 123) // Function keys remapped to avoid conflict with other key values
            //{
            //    ProcessFunctionKey(keyVal - 111);

            //    return;
            //}

            if (ProcessNumericKey(keyVal))
            {
                return;
            }

            ProcessShortcutKey(keyVal);

            //ProcessKeyStroke(keyVal);
        }

        private void BaseForm_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            
        }

        private void ProcessArrowKey(int keyVal)
        {
            if (DesignState == DesignState.Area)
            {
                ProcessAreaStateArrowKey(keyVal);

                return;
            }

            if (DesignState == DesignState.Line)
            {
                ProcessLineStateArrowKey(keyVal);

                return;
            }

            if (DesignState == DesignState.Seam)
            {
                ProcessSeamStateArrowKey(keyVal);

                return;
            }
        }


        private void ProcessDeleteCommand()
        {

            if (DesignState == DesignState.Area)
            {
                CurrentPage.AreaDesignStateSelectedAreas().ForEach(a => ProcessEditAreaModeActionDeleteShape(a));

                BaseForm.OversUndersFormUpdate(false);

                RaiseAreaSelectionChangedEvent();

                return;
            }

            if (DesignState == DesignState.Line)
            {
                bool areaLinesFound = false;

                foreach (CanvasDirectedLine canvasDirectedLine in CurrentPage.LineDesignStateSelectedLines())
                {
                    if (!canvasDirectedLine.IsDeleteable)
                    {
                        areaLinesFound = true;
                        continue;
                    }

                    ProcessEditLineModeActionDeleteLine(canvasDirectedLine);

                }
                
                if (areaLinesFound)
                {
                    ManagedMessageBox.Show("One or more lines could not be deleted because they belong to an area.");
                }
            }


        }


        private void ProcessFunctionKey(int functionKey)
        {
            switch (functionKey)
            {
                case 1: // F1

                    BaseForm.BtnAreaDesignState_Click(null, null);

                    return;

                case 2: // F2

                    BaseForm.BtnLineDesignState_Click(null, null);

                    return;

                case 3: // F3

                    BaseForm.BtnSeamDesignState_Click(null, null);

                    return;

                case 4: // F4

                    BaseForm.ToggleDesignStateMode();

                    return;

                case 5: // F5

                    BaseForm.BtnShowFieldGuides_Click(null, null);

                    return;

                case 6: // F6

                    BaseForm.BtnHideFieldGuides_Click(null, null);

                    return;

                case 7: // F7

                    BaseForm.BtnDeleteFieldGuides_Click(null, null);

                    return;

                default: break;
            }
         
        }

        private void ProcessTempSwitchToLineMode()
        {
            // We are only concerned here with the case where the L key is depressed when in area mode so as to bring up
            // the line pallet as a temporary measure.

            if (DesignState == DesignState.Line)
            {
                return;
            }

            if (BaseForm.tbcPageAreaLine.SelectedIndex == FloorMaterialEstimatorBaseForm.tbcLineModeIndex)
            {
                // Already showing line pallet. Return;

                return;
            }

            BaseForm.AllowTabSelection = true;

            BaseForm.LinePaletteTemporaryPopUp = true;

            BaseForm.tbcPageAreaLine.SelectedIndex = FloorMaterialEstimatorBaseForm.tbcLineModeIndex;

            BaseForm.AllowTabSelection = false;

        }

        private void ProcessTempSwitchFromLineMode()
        {
            // We are only concerned here with the case where the L key is depressed when in area mode or seam mode so as to bring up
            // the line pallet as a temporary measure.

            if (DesignState == DesignState.Line)
            {
                return;
            }

            if (BaseForm.tbcPageAreaLine.SelectedIndex != FloorMaterialEstimatorBaseForm.tbcLineModeIndex)
            {
                // Not showing line pallet. Return;

                return;
            }

            BaseForm.AllowTabSelection = true;

            BaseForm.tbcPageAreaLine.SelectedIndex = FloorMaterialEstimatorBaseForm.tbcAreaModeIndex;

            BaseForm.LinePaletteTemporaryPopUp = false;

            BaseForm.AllowTabSelection = false;

            return;
        }

        private bool ProcessNumericKey(int keyVal)
        {
            if (keyVal >= 49 && keyVal <= 57)
            {
                // Numeric key pressed.

                if (SystemState.DesignState == DesignState.Area)
                {
                    ProcessAreaModeFinishNumericShortCut(keyVal - 48);
                }

                else if (SystemState.DesignState == DesignState.Line)
                {
                    ProcessLineModeFinishNumericShortCut(keyVal - 48);
                }

                return true;
            }

            //Keypad numeric key pressed.

            //if (keyVal >= 97 && keyVal <= 105)
            //{
            //    if (SystemState.DesignState == DesignState.Area)
            //    {
            //        ProcessAreaModeFinishNumericShortCut(keyVal - 96);
            //    }

            //    else if (SystemState.DesignState == DesignState.Line)
            //    {
            //        ProcessLineModeFinishNumericShortCut(keyVal - 96);
            //    }

            //    return true;
            //}

            return false;
        }

        private void ProcessShortcutKey(int keyVal)
        {
            // Hardcoded shortcuts...

            if (keyVal == 65)
            {
                if (KeyboardUtils.CntlKeyPressed) // Cntl-A
                {
                    BaseForm.BtnAreaDesignState_Click(null, null); // Switch to Area Mode

                    return;
                }
            }

            if (keyVal == 76)
            {
                if (KeyboardUtils.CntlKeyPressed) // Cntl-L
                {
                    BaseForm.BtnLineDesignState_Click(null, null); // Switch to Line Mode

                    return;
                }
            }

            if (keyVal == 83)
            {
                if (KeyboardUtils.CntlKeyPressed) // Cntl-S
                {
                    BaseForm.BtnSeamDesignState_Click(null, null); // Switch to Seam Mode

                    return;
                }
            }

            if (keyVal == 68)
            {
                if (KeyboardUtils.CntlKeyPressed) // Cntl-D
                {
                    BaseForm.BtnDebug_Click(null, null); // Bring up the debug form
                    return;
                }
            }

            if (DesignState == DesignState.Seam)
            {
                if (keyVal == 83)
                {
                    BaseForm.BtnSeamDesignStateSubdivisionMode_Click(null, null);
                    return;
                }

                if (keyVal == 65)
                {
                    BaseForm.BtnSeamDesignStateSelectionMode_Click(null, null);
                    return;
                }

                if (keyVal == 13) // Enter key
                {
                    // If subdividing an area, an enter key is equivalent to clicking on the 'complete' button

                    if (SystemState.SeamMode == SeamMode.Subdivision)
                    {
                        BaseForm.btnCompleteSubdivision_Click(null, null);
                    }

                    return;
                }

                if (KeyboardUtils.CntlKeyPressed)
                {
                    if (keyVal == 84)
                    {
                        SeamingToolShowHide_Click(null, null); return;
                    }

                    if (SeamingTool.IsVisible)
                    {
                        switch (keyVal)
                        {
                            case 65: SeamingToolAlign_Click(null, null); return;                 // Ctrl-A
                            case 69: SeamingToolExpand_Click(null, null); return;                // Ctrl-E
                            case 77: SeamingToolSeamSingleLineToTool_Click(null, null); return;  // Ctrl-M
                            case 83: SeamingToolAreaSeam_Click(null, null); return;              // Ctrl-S
                            case 68: SeamingToolSubdivideRegion_Click(null, null); return;       // Ctrl-D
                            case 82: SeamingToolRotate90Degrees_Click(null, null); return;       // Ctrl-R
                            case 72: SeamingToolAlignHorizontal_Click(null, null); return;       // Ctrl-H
                            case 86: SeamingToolAlignVertical_Click(null, null); return;         // Ctrl-V
                            default: break;
                        }
                    }
                }
            }

            int shortcutNumber = -1;

            shortcutNumber = ShortcutSettings.MiscStateKeyToActionMapValue(keyVal);

            // General case

            if (shortcutNumber >= 0)
            {
                switch (shortcutNumber)
                {
                    case 0: PanAndZoomController.ProcessGeneralZoomIn(); return;
                    case 1: PanAndZoomController.ProcessGeneralZoomOut(); return;
                    case 2: ProcessToggleShowMeasuringStickMode(); return;
                    default: break;
                }
            }

            shortcutNumber = ShortcutSettings.MenuStateKeyToActionMapValue(keyVal);

            if (shortcutNumber >= 0)
            {
                switch (shortcutNumber)
                {
                    case 0: ProcessMenuModeSaveProject(); return;
                    case 1: ProcessMenuModeZoomIn(); return;
                    case 2: ProcessMenuModeZoomOut(); return;
                    case 3: ProcessMenuModeFitToCanvas(); return;
                    case 4: ProcessMenuModePanMode(); return;
                    case 5: ProcessMenuModeDrawMode(); return;
                    case 6: ProcessMenuModeAreaMode(); return;
                    case 7: ProcessMenuModeLineMode(); return;
                    case 8: ProcessMenuModeSeamMode(); return;
                    case 9: ProcessMenuModeShowFieldGuides(); return;
                    case 10: ProcessMenuModeHideFieldGuides(); return;
                    case 11: ProcessMenuModeDeleteFieldGuides(); return;
                    case 12: ProcessMenuModeToggleHighlightSeamAreas(); return;
                    case 13: ProcessMenuModeAlignToGrid(); return;
                    case 14: /*shift key - no action*/ return;
                    case 15: ProcessMenuModeLineMeasuringTool(); return;
                    case 16: ProcessMenuModeOversUnders(); return;
                    case 17: ProcessMenuModeFullScreenMode(); return;

                    default: break;
                }

                return;
            }


            if (DesignState == DesignState.Area)
            {
                shortcutNumber = ShortcutSettings.AreaStateKeyToActionMapValue(keyVal);

                switch (shortcutNumber)
                {
                    case 0: ProcessAreaModeCompleteShape(); return;
                    case 1: ProcessAreaModeCompleteShapeByIntersection(); return;
                    case 2: ProcessAreaModeCompleteShapeByMaxArea(); return;
                    case 3: ProcessAreaModeCompleteShapeByMinArea(); return;
                    case 4: ProcessAreaModeCompleteRotatedShape(); return;
                    case 5: ProcessAreaModeDeleteBuildingLine(); return;
                    case 6: ProcessAreaModeCancelShapeInProgress(); return;
                    case 7: ProcessDeleteCommand(); return;
                    case 8: ProcessAreaModeToggleZeroLineMode(); return;
                    case 9: ProcessAreaModeTakeout();  return;
                    case 10: ProcessAreaModeTakeoutAndFill(); return;
                    case 11: ProcessAreaModeEmbedShape(); return;
                    case 12: ProcessAreaModeCopyAndShape(); return;
                    case 13: ProcessAreaModeToggleFixedWidth(); return;
                    case 14: ProcessAreaModeFixedWidthJump(); return;
                    case 15: return;
                    case 16: ProcessAreaModeSetAreasToSelectedMaterial(); return;
                   
                    default: break;
                }

                return;
            }

            else if (DesignState == DesignState.Line)
            {
                shortcutNumber = ShortcutSettings.LineStateKeyToActionMapValue(keyVal);

                switch (shortcutNumber)
                {
                    case 0: ProcessLineModeJump(); return;
                    case 1: ProcessLineModeSetSingleLine(); return;
                    case 2: ProcessLineModeSetDoubleLineMode(); return;
                    case 3: ProcessLineModeDuplicateLine(); return;
                    case 4: ProcessLineModeToggleDoorTakeout(); return;
                    case 5: ProcessLineModeDeleteBuildingLine(); return;
                    case 6: ProcessDeleteCommand(); return;
                    case 7: return;
                    case 8: return;
                    case 9: ProcessLineModeSetSelectedLineToCurrentMaterial(); return;


                    default: break;
                }
            }

            else if (DesignState == DesignState.Seam)
            {
                if (keyVal == 27)
                {
                    if (Utilities.IsNotNull(SelectedCutIndex) || Utilities.IsNotNull(SelectedOverageIndex) || Utilities.IsNotNull(SelectedUndrageIndex))
                    {
                        if (Utilities.IsNotNull(SelectedCutIndex))
                        {
                            SelectedCutIndex.Deselect();

                            SelectedCutIndex = null;
                        }

                        if (Utilities.IsNotNull(SelectedOverageIndex))
                        {
                            SelectedOverageIndex.Deselect();

                            SelectedOverageIndex = null;
                        }

                        if (Utilities.IsNotNull(SelectedUndrageIndex))
                        {
                            SelectedUndrageIndex.Deselect();

                            SelectedUndrageIndex = null;
                        }

                        return;
                    }

                    return;
                }

                shortcutNumber = ShortcutSettings.SeamStateKeyToActionMapValue(keyVal);

                switch (shortcutNumber)
                {
                    case 0: ProcessSeamModeDeleteBuildingLine(); return;
                    case 1: ProcessSeamModeCompleteShape(); return;
                    case 2: ProcessSeamModeCancelShapeInProgress(); return;
                    default: break;
                }
            }

        }

        #region Area Mode Shortcut Processors

        // Area mode shortcut processors

        private void ProcessAreaModeCompleteShape()
        {
            BaseForm.BtnAreaDesignStateCompleteDrawing_Click(null, null);
        }

        private void ProcessAreaModeCompleteShapeByIntersection()
        {
            BaseForm.BtnAreaDesignStateCompleteDrawingByIntersection_Click(null, null);
        }

        private void ProcessAreaModeCompleteShapeByMaxArea()
        {
            BaseForm.AreaDesignStateCompleteDrawingByMaximumArea();
        }

        private void ProcessAreaModeCompleteShapeByMinArea()
        {
            BaseForm.AreaDesignStateCompleteDrawingByMinimumArea();
        }

        private void ProcessAreaModeCompleteRotatedShape()
        {
            if (this.buildingPolyline is null)
            {
                MessageBoxAdv.Show(
                    "No current area being drawn."
                    , "No Current Drawing"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Info);

                return;
            }

            if (this.buildingPolyline.Count <= 1)
            {
                MessageBoxAdv.Show(
                    "At least two lines must be drawn to complete this operation."
                    , "Two Lines Required"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Info);

                return;
            }

            DirectedPolyline b = new DirectedPolyline();
         
            var baseLine = this.buildingPolyline.FrstLine;

            var frstPoint = this.buildingPolyline.CoordList.First();
            var lastPoint = this.buildingPolyline.CoordList.Last();


            double deltaY = baseLine.Coord2.Y - baseLine.Coord1.Y;
            double deltaX = baseLine.Coord2.X - baseLine.Coord1.X;

            double theta = MathUtils.Atan(baseLine.Coord1.X, baseLine.Coord1.Y, baseLine.Coord2.X, baseLine.Coord2.Y);

            var translatedLastPoint = lastPoint - frstPoint;

            var rotatedTranslatedLastPoint = translatedLastPoint;

            rotatedTranslatedLastPoint.Rotate(-theta);

            double X = rotatedTranslatedLastPoint.X;
            double Y = rotatedTranslatedLastPoint.Y;

            Coordinate intersectionCoord = Coordinate.NullCoordinate;
            
            if (!genIntesectionPoint(X, Y, out intersectionCoord))
            {
                return;
            }

            if (intersectionCoord == rotatedTranslatedLastPoint)
            {
                this.buildingPolyline.AddPoint(frstPoint);

                completeAreaDesignStatePolylineDraw(this.buildingPolyline);

                return;
            }

            intersectionCoord.Rotate(theta);

            intersectionCoord.Translate(frstPoint);

            this.buildingPolyline.AddPoint(intersectionCoord);

            this.buildingPolyline.AddPoint(frstPoint);

            completeAreaDesignStatePolylineDraw(this.buildingPolyline);
        }

        private bool genIntesectionPoint(double X, double Y, out Coordinate intersectionCoord)
        {
            intersectionCoord = Coordinate.NullCoordinate;

            if (Math.Abs(X) < 1.0e-2)
            {
                if (Math.Abs(Y) < 1.0e-2)
                {
                    MessageBoxAdv.Show(
                        "Unable to complete shape using this feature since the last point is too close to the starting point."
                        , "Unable To Complete Shape"
                        , MessageBoxAdv.Buttons.OK
                        , MessageBoxAdv.Icon.Info);

                    return false;
                }

                intersectionCoord = new Coordinate(X, Y);

                return true;
            }

            if (X > 0)
            {
                if (Math.Abs(Y) < 1.0e-2)
                {
                    MessageBoxAdv.Show(
                        "Unable to complete shape using this feature since the last point is too intersections the building region."
                        , "Unable To Complete Shape"
                        , MessageBoxAdv.Buttons.OK
                        , MessageBoxAdv.Icon.Info);

                    return false;
                }

                intersectionCoord = new Coordinate(0, Y);

                return true;
            }

            intersectionCoord = new Coordinate(X, 0);

            return true;
        }

        private void ProcessAreaModeDeleteBuildingLine()
        {
            if (SystemState.FixedWidthMode)
            {
                BorderManager.DeleteLastMarker();
            }

            else
            {
                RemoveBuildingPolyLineBuildingLine();
            }
        }

        private void ProcessAreaModeCancelShapeInProgress()
        {
            if (SystemState.FixedWidthMode)
            {
                BorderManager.Reset();
            }

            else
            {
                DeleteBuildingPolyLine();
            }
        }

        private void ProcessAreaModeToggleZeroLineMode()
        {
            BaseForm.BtnAreaDesignModeZeroLine_Click(null, null);
        }

        private void ProcessAreaModeTakeout()
        {
            BaseForm.BtnLayoutAreaTakeout_Click(null, null);
        }

        private void ProcessAreaModeTakeoutAndFill()
        {
            BaseForm.BtnLayoutAreaTakeOutAndFill_Click(null, null);
        }
        private void ProcessAreaModeEmbedShape()
        {
            BaseForm.BtnEmbeddLayoutAreas_Click(null, null);
        }
        private void ProcessAreaModeCopyAndShape()
        {
            BaseForm.BtnEditAreaCopyAndPasteShapes_Click(null, null);
        }
        private void ProcessAreaModeToggleFixedWidth()
        {
            //BaseForm.rbnFixedWidth.Checked = !BaseForm.rbnFixedWidth.Checked;
        }

        private void ProcessAreaModeFixedWidthJump()
        {
            if (BorderManager == null)
            {
                return;
            }

            if (SystemState.CurrentLayoutType != LayoutAreaType.FixedWidth)
            {
                return;
            }

            if (!BorderManager.IsBorderSelected)
            {
                return;
            }

            BaseForm.lblFixedWidthJump_Click(null, null);
        }

        #endregion

        #region Line Mode ShortcutProcessors

        private void ProcessLineModeJump()
        {
            BaseForm.BtnLayoutLineJump_Click(null, null);
        }

        private void ProcessLineModeSetSingleLine()
        {
            BaseForm.BtnLayoutLine1XMode_Click(null, null);
        }

        private void ProcessLineModeSetDoubleLineMode()
        {
            BaseForm.btnLayoutLine2XMode_Click(null, null);
        }

        private void ProcessLineModeDuplicateLine()
        {
            BaseForm.BtnLayoutLineDuplicate_Click(null, null);
        }

        private void ProcessLineModeToggleDoorTakeout()
        {
            BaseForm.BtnDoorTakeoutActivate_Click(null, null);
        }

        private void ProcessLineModeDeleteBuildingLine()
        {
            RemoveLineModeBuildingLine();
        }

        #endregion

        #region Menu Mode Shortcut Processors
        
        private void ProcessMenuModeSaveProject()
        {
            BaseForm.BtnToolStripSave_Click(null, null);
        }
        
        private void ProcessMenuModeZoomIn()
        {
            PanAndZoomController.ZoomInButton_Click(null, null);
        }
        
        private void ProcessMenuModeZoomOut()
        {
            PanAndZoomController.ZoomOutButton_Click(null, null);
        }
        
        private void ProcessMenuModeFitToCanvas()
        {
            PanAndZoomController.ZoomToFit();
        }
        
        private void ProcessMenuModePanMode()
        {
            BaseForm.BtnPanMode_Click(null, null);
        }

        private void ProcessMenuModeDrawMode()
        {
            BaseForm.BtnDrawMode_Click(null, null);
        }

        private void ProcessMenuModeAreaMode()
        {
            BaseForm.BtnAreaDesignState_Click(null, null);
        }

        private void ProcessMenuModeLineMode()
        {
            BaseForm.BtnLineDesignState_Click(null, null);
        }

        private void ProcessMenuModeSeamMode()
        {
            BaseForm.BtnSeamDesignState_Click(null, null);
        }

        private void ProcessMenuModeShowFieldGuides()
        {
            BaseForm.BtnShowFieldGuides_Click(null, null);
        }

        private void ProcessMenuModeHideFieldGuides()
        {
            BaseForm.BtnHideFieldGuides_Click(null, null);
        }

        private void ProcessMenuModeDeleteFieldGuides()
        {
            BaseForm.BtnDeleteFieldGuides_Click(null, null);
        }

        private void ProcessMenuModeToggleHighlightSeamAreas()
        {
        
        }

        private void ProcessMenuModeAlignToGrid() 
        {
            BaseForm.BtnSnapToGrid_Click(null, null);
        }

        private void ProcessMenuModeLineMeasuringTool()
        {
            SystemGlobals.ActivateTapeMeasure();
        }

        private void ProcessMenuModeOversUnders()
        {
            BaseForm.BtnOversUnders_Click(null, null);
        }
        
        private void ProcessMenuModeFullScreenMode()
        {
            BaseForm.BtnFullScreen_Click(null, null);
        }

        #endregion
    }
}
