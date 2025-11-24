//-------------------------------------------------------------------------------//
// <copyright file="MouseEventManager.cs" company="Bruun Estimating, LLC">       // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//


namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using CanvasLib.Scale_Line;
    using CanvasLib.Legend;
    using Globals;
    using FloorMaterialEstimator;
    using CanvasLib.Markers_and_Guides;
    using Geometry;
    using Graphics;

    using SettingsLib;
    using Utilities;

    using Visio = Microsoft.Office.Interop.Visio;
    using System.Collections.Generic;
    using CanvasLib.Borders;
    using FinishesLib;
    using MaterialsLayout;
    using System.Windows.Forms;

    public partial class CanvasManager
    {


        double mouseDownX = 0;
        double mouseDownY = 0;

        private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            doMouseDown(Button, KeyButtonState, x, y, ref CancelDefault);

            if (DesignState == DesignState.Seam && Button == 1)
            {
                CancelDefault = false;
            }
        }

        Coordinate areaDragOutStartCoord = Coordinate.NullCoordinate;
        Coordinate areaDragOutEndCoord = Coordinate.NullCoordinate;

        bool draggingOutArea => !Coordinate.IsNullCoordinate(areaDragOutStartCoord);

        private void doMouseDown(int button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            mouseDownX = x;
            mouseDownY = y;

            // Return if click is outside the canvas

            if (x < 0.0 || y < 0.0 || x > CurrentPage.PageWidth || y > CurrentPage.PageHeight)
            {
                CancelDefault = true;

                Window.DeselectAll();

                return;
            }

            // Display button click

            if (button == 2)
            {
                BaseForm.MRgt.Image = BaseForm.MRghtButtonDownImage;
                BaseForm.MLft.Image = BaseForm.MButtonUpImage;
            }

            else
            {
                BaseForm.MLft.Image = BaseForm.MLeftButtonDownImage;
                BaseForm.MRgt.Image = BaseForm.MButtonUpImage;
            }

            if (BaseForm.btnPanMode.Checked == true)
            {
                processPanModeMouseDown(button, KeyButtonState, x, y, ref CancelDefault);

                CancelDefault = true;

                return;
            }

            if (KeyboardUtils.AltKeyPressed && KeyboardUtils.CntlKeyPressed && button == 2)
            {

                Point baseCursorPosition = StaticGlobals.GetCursorPosition();

                Point loclCursorPosition = this.BaseForm.PointToClient(baseCursorPosition);

                ExtendedCrosshairsContextMenu.Show(BaseForm, loclCursorPosition);

                return;
            }

            if (KeyboardUtils.CntlKeyPressed && (DesignState == DesignState.Area || DesignState == DesignState.Seam) && button == 2)
            {
                placeOrEraseMarker(x, y);

                Window.DeselectAll();

                CancelDefault = true ;

                return;
            }

            string data1 = VisioInterop.GetSelectedShapeData1(Window);

            if (data1 == "PastedLayoutArea")
            {
                CancelDefault = false;

                return;
            }

            if (data1 == "[MeasuringStick]")
            {
                CancelDefault = false;

                return;
            }

            if (KeyboardUtils.CntlKeyPressed && button == 1)
            {
                areaDragOutStartCoord = new Coordinate(x, y);

                CancelDefault = false;

                return;
            }

            else
            {
                areaDragOutStartCoord = Coordinate.NullCoordinate;
            }

            if (MeasuringStick.IsVisible)
            {
                if (MeasuringStick.SelectedFromMouseDown(button, x, y))
                {
                    CancelDefault = true; // MDD Reset

                    return;
                }
            }

            if (DesignState == DesignState.Seam && SeamingTool.IsVisible)
            {
                if (SeamingTool.SelectedFromMouseDown(button, x, y))
                {
                    if (button == 2)
                    {
                        this.SeamingTool.IsSelected = true;

                        Point baseCursorPosition = StaticGlobals.GetCursorPosition();

                        Point loclCursorPosition = this.BaseForm.PointToClient(baseCursorPosition);

                        SeamingToolContextMenu.Show(BaseForm, loclCursorPosition);
                    }

                    CancelDefault = false;

                    return;

                }

                if (Page.MouseOverSeamingTool(x, y))
                {
                    CancelDefault = false;

                    return;
                }
            }

            if (SystemState.DrawingMode == DrawingMode.Default)
            {
                Debug.Assert(this.BuildingPolyline == null);

                CancelDefault = true;

                return;
            }

            if (!SystemState.DrawingShape)
            {
                if (SystemState.DrawingMode == DrawingMode.Line1X || SystemState.DrawingMode == DrawingMode.Line2X || SystemState.DrawingMode == DrawingMode.LineDuplicate)
                {
                    CancelDefault = true;

                    return;
                }

                if (SystemState.DrawingMode == DrawingMode.Rectangle)
                {
                    this.InitializeRectangleDraw(x, y);

                    CancelDefault = true;

                    return;
                }

                if (SystemState.DrawingMode == DrawingMode.Polyline)
                {
                    CancelDefault = true;

                    return;
                }
            }

            CancelDefault = true;
        }

        private void VsoWindow_MouseUp(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            doVsoWindow_MouseUp(button, keyButtonState, x, y, ref cancelDefault);

        }

        private void doVsoWindow_MouseUp(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            if (BaseForm.btnPanMode.Checked == true)
            {
                processPanModeMouseUp(button, keyButtonState, x, y, ref cancelDefault);

                return;
            }

            if (button == 2)
            {
                BaseForm.MRgt.Image = BaseForm.MButtonUpImage;
            }
            else
            {
                BaseForm.MLft.Image = BaseForm.MButtonUpImage;
            }


            if (System.Windows.Input.Mouse.LeftButton == System.Windows.Input.MouseButtonState.Pressed ||
                System.Windows.Input.Mouse.RightButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                return;
            }


            if (KeyboardUtils.CntlKeyPressed && (DesignState == DesignState.Area || DesignState == DesignState.Seam) && button == 2)
            {
                Window.DeselectAll();

                return;
            }

            if (KeyboardUtils.CntlKeyPressed && button == 1)
            {
                if (draggingOutArea)
                {
                    areaDragOutEndCoord = new Coordinate(x, y);

                    if (Coordinate.H2Distance(areaDragOutStartCoord, areaDragOutEndCoord) > 0.5)
                    {
                        PanAndZoomController.ProcessZoomToArea(areaDragOutStartCoord, areaDragOutEndCoord);

                        cancelDefault = true;

                        return;
                    }
                }
            }


            //if (KeyboardUtils.CntlKeyPressed && (DesignState == DesignState.Area || DesignState == DesignState.Seam) && button == 2)
            //{
            //    placeOrEraseMarker(x, y);

            //    return;
            //}

            if (MeasuringStick.IsVisible)
            {
                if (MeasuringStick.Shape.VisioShape.HitTest(x, y, 0) != 0/*VisHitTestResults.visHitOutside*/)
                {
                    if (button != 2/*(int)VisKeyButtonFlags.visMouseRight*/)
                    {
                        cancelDefault = false;
                        return;
                    }
                }
            }

            MouseX = x;
            MouseY = y;

            cancelDefault = true;

            if (x < 0.0 || y < 0.0 || x > CurrentPage.PageWidth || y > CurrentPage.PageHeight)
            {
                cancelDefault = true;

                return;
            }

            //Debug.WriteLineIf((Program.Debug & DebugCond.VisioMouseEvents) != 0, "Mouse up event at (" + x.ToString("0.0000") + "," + y.ToString("0.0000") + "), button = " + button + ", KeyButtonState = " + keyButtonState);

            if (MathUtils.H0Dist(this.mouseDownX, this.mouseDownY, x, y) <= 0.1)
            {
                VsoWindow_MouseClick(button, keyButtonState, x, y, ref cancelDefault);

                cancelDefault = false;

                //Window.DeselectAll();

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.Default)
            {
                return;
            }

            if (!SystemState.DrawingShape)
            {
                return;
            }

            if (SystemState.DrawingMode == DrawingMode.Line1X || SystemState.DrawingMode == DrawingMode.Line2X || SystemState.DrawingMode == DrawingMode.LineDuplicate)
            {
                //CompleteLineDraw(x, y);

                cancelDefault = true;

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.Rectangle)
            {
                CompleteRectangleDraw(x, y);

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.Polyline)
            {
                return;
            }

            if (SystemState.DrawingMode == DrawingMode.ScaleLine)
            {
                //CompleteScaleLineDraw(x, y);

                cancelDefault = true;

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.TapeMeasure)
            {
                //CompleteTapeMeasureDraw(x, y);

                cancelDefault = true;

                return;
            }
        }

        public double MouseX = 0;
        public double MouseY = 0;

        public void VsoWindow_MouseClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            MouseX = x;
            MouseY = y;

            doVsoWindow_MouseClick(button, keyButtonState, x, y, ref cancelDefault);

            //cancelDefault = false; // MDD Reset
        }

        private void doVsoWindow_MouseClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            if (x < 0.0 || y < 0.0 || x > CurrentPage.PageWidth || y > CurrentPage.PageHeight)
            {
                cancelDefault = true;

                return;
            }
            
            if (KeyboardUtils.ShiftKeyPressed && button == 1 && SystemState.DrawingMode == DrawingMode.Default)
            {
                if (VertexEditor.VertexEditStart(CurrentPage, Window, Page, x, y))
                {
                    SystemState.DrawingMode = DrawingMode.VertexEditing;

                    cancelDefault = true;

                    return;
                }

            }

            // MDD Reset


            //if (KeyboardUtils.CntlKeyPressed && (DesignState == DesignState.Area || DesignState == DesignState.Seam) && button == 2)
            //{
            //    placeOrEraseMarker(x, y);

            //    return;
            //}

            if (SystemState.DrawingMode == DrawingMode.VertexEditing)
            {
                if (!KeyboardUtils.ShiftKeyPressed || button != 1)
                {
                    VertexEditor.TerminateVertexEditing();

                    SystemState.DrawingMode = DrawingMode.Default;
                }

                else
                {
                    VertexEditor.CompleteVertexEditing(x, y);

                    SystemState.DrawingMode = DrawingMode.Default;

                    return;
                }
            }

            if (!BaseForm.btnTapeMeasure.Checked && !SystemState.BtnSetCustomScale.Checked && DesignState == DesignState.Seam)
            {
                if (processCutOverAndUnderIndexSelect(x, y))
                {
                    cancelDefault = true;

                    return;
                }
            }

            if (processFieldGuideClick(button, keyButtonState, x, y))
            {
                cancelDefault = true;

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.TapeMeasure)
            {
                this.TapeMeasureController.TapeMeasureDrawingModeClick(button, x, y);

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.BorderGeneration)
            {
                this.BorderManager.BorderDrawingModeClick(button, x, y);

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.ScaleLine)
            {
                this.ScaleRuleController.ScaleLineDrawingModeClick(button, x, y);

                return;
            }

            //if (CurrentPage.AreaModeLegend.LegendShowLocation != LegendLocation.None && CurrentPage.AreaModeLegend.LegendShowLocation != LegendLocation.Notset)
            //{
            //    if (CurrentPage.GetLegend(x, y) != null)
            //    {
            //        cancelDefault = false;
            //        return;
            //    }
            //}

            BaseForm.CurrentProjectChanged = true;

            if (SystemState.DesignState == DesignState.Line)
            {
                processLineDesignStateClick(button, keyButtonState, x, y, ref cancelDefault);
            }

            else if (SystemState.DesignState == DesignState.Area)
            {
                ProcessAreaDesignStateClick(button, keyButtonState, x, y, ref cancelDefault);
            }

            else if (SystemState.DesignState == DesignState.Seam)
            {
                cancelDefault = true;

                ProcessSeamDesignStateClick(button, keyButtonState, x, y, ref cancelDefault);
            }

            else
            {
                //throw new NotImplementedException();
            }

            //cancelDefault = true; // MDD
            if (button == 1)
            {
                //double cursorGridPosnX = CurrentPage.DrawingScaleInInches * (x / this.GridScale);
                //double cursorGridPosnY = CurrentPage.DrawingScaleInInches * (y / this.GridScale);

                if (DesignState == DesignState.Area)
                {
                    if (LegendController.AreaModeLegend.LocateToClick)
                    {
                        double size = LegendController.AreaModeLegend.CurrentSize;

                        LegendController.AreaModeLegend.Setlocation(x, y, size);

                        LegendController.SetShowLegendText("Hide Legend");

                        LegendController.AreaModeLegend.ShowLegend(true);
                    }
                }

                else if (DesignState == DesignState.Line)
                {
                    if (LegendController.LineModeLegend.LocateToClick)
                    {
                        double size = LegendController.AreaModeLegend.CurrentSize;

                        LegendController.LineModeLegend.Setlocation(x, y, size);

                        LegendController.SetShowLegendText("Hide Legend");

                        LegendController.LineModeLegend.ShowLegend(true);
                    }
                }
            }
        }

        private void placeOrEraseMarker(double x, double y)
        {
            GraphicsSelection graphicsSelection = VisioInterop.SpatialSearch(
                Window
                , Page
                , x
                , y
                , (short)
                    (Visio.VisSpatialRelationCodes.visSpatialContain
                    | Visio.VisSpatialRelationCodes.visSpatialContainedIn
                    | Visio.VisSpatialRelationCodes.visSpatialOverlap
                    | Visio.VisSpatialRelationCodes.visSpatialTouching)
                , 0.125
                , 1);

            List<Shape> filteredShapeList = graphicsSelection.FilterByData1("PlaceMarker");

            if (filteredShapeList.Count <= 0)
            {
                PlaceMarker placeMarker = new PlaceMarker(Window, Page, x, y, 0.2);

                placeMarker.Draw();

                CurrentPage.AreaModeGlobalLayer.AddShapeToLayer(placeMarker.Shape, 1);
                CurrentPage.SeamModeGlobalLayer.AddShapeToLayer(placeMarker.Shape, 1);

                Page.AddToPageShapeDict(placeMarker);
            }

            else
            {
                foreach (Shape filteredShape in filteredShapeList)
                {
                    Page.RemoveFromPageShapeDict(filteredShape);

                    filteredShape.Delete();
                }
            }
        }

        private bool processFieldGuideClick(int button, int keyButtonState, double x, double y)
        {

            if (button != 2)
            {
                return false;
            }

            if (KeyboardUtils.TabKeyPressed || KeyboardUtils.AddKeyPressed)
            {
                if (SystemState.BtnShowFieldGuides.Checked)
                {
                    keyUsedAsShiftKey = true;

                    FieldGuideController.SuspendAlignMode = !FieldGuideController.SuspendAlignMode;
                    FieldGuideController.ProcessFieldGuideClick(x, y);
                }

                else if (BaseForm.btnHideFieldGuides.Checked)
                {
                    MessageBox.Show("Field guides are hidden. Switch to 'Show Field Guides' to add guides.");
                 
                }

                return true;
            }

            if (KeyboardUtils.SubKeyPressed)
            {
                if (SystemState.BtnShowFieldGuides.Checked)
                {
                    keyUsedAsShiftKey = true;
                    FieldGuideController.ProcessFieldGuideClick(-100000, y);
                }

                else if (BaseForm.btnHideFieldGuides.Checked)
                {
                    MessageBox.Show("Field guides are hidden. Switch to 'Show Field Guides' to add guides.");

                }

                return true;
            }

            if (KeyboardUtils.FwdSlashKeyPressed)
            {
                if (SystemState.BtnShowFieldGuides.Checked)
                {
                    keyUsedAsShiftKey = true;
                    FieldGuideController.ProcessFieldGuideClick(x, -100000);
                }

                else if (BaseForm.btnHideFieldGuides.Checked)
                {
                    MessageBox.Show("Field guides are hidden. Switch to 'Show Field Guides' to add guides.");

                }

                return true;
            }

            return false;
        }

        //bool onCanvas = false;

        bool ignoreMouseMove = false;

        private int areaMouseOverHitCounter = 0;

        private void VsoWindow_MouseMove(int button, int keyButtonState, double x, double y, ref bool CancelDefault)
        {
            VsoApplication.ScreenUpdating = 0;

            System.Windows.Forms.Cursor.Current = Cursors.Cross;

            //Debug.WriteLine("MouseMove {0} {1} x/y {2}/{3}", button, keyButtonState, x, y);
            doMouseMove(button, keyButtonState, x, y, ref CancelDefault);

            if (BaseForm.btnPanMode.Checked == true)
            {
                CancelDefault = true;
            }

            else
            {
                CancelDefault = false;
            }
            
        }

        private void doMouseMove(int button, int keyButtonState, double x, double y, ref bool CancelDefault)
        {
            MouseX = x;
            MouseY = y;

            double cursorGridPosnX = CurrentPage.DrawingScaleInInches * (x / this.GridScale);
            double cursorGridPosnY = CurrentPage.DrawingScaleInInches * (y / this.GridScale);

            BaseForm.ttsCursorPosition.Text = "(" + cursorGridPosnX.ToString("0.00").PadLeft(5) + ", " + cursorGridPosnY.ToString("0.00").PadLeft(5) + ")";

            if (BaseForm.btnPanMode.Checked == true)
            {
                processPanModeMouseMove(button, keyButtonState, x, y, ref CancelDefault);

                return;
            }

            if (KeyboardUtils.BKeyPressed)
            {
                if (!CurrentPage.ExtendedCrosshairsLayer.IsVisible())
                {
                    CurrentPage.ExtendedCrosshairsLayer.SetLayerVisibility(true);
                }

                ExtendedCrosshairs.SetCenter(x, y);
            }

            else
            {
                if (CurrentPage.ExtendedCrosshairsLayer.IsVisible())
                {
                    CurrentPage.ExtendedCrosshairsLayer.SetLayerVisibility(false);
                }
            }

            SetDrawoutLength(x, y);

            if (Utilities.IsNotNull(SelectedCutIndex))
            {
                CancelDefault = false;
                return;
            }

            if (MeasuringStick.IsVisible) 
            {
                string selectedShapeGuid = VisioInterop.GetSelectedShapeGuid(Window);

                if (!string.IsNullOrEmpty(selectedShapeGuid))
                {
                    if (selectedShapeGuid == MeasuringStick.Shape.Guid)
                    {
                        CancelDefault = false;
                        return;
                    }
                }
            }

            if (SeamingTool.IsVisible)
            {
                string selectedShapeGuid = VisioInterop.GetSelectedShapeGuid(Window);

                if (!string.IsNullOrEmpty(selectedShapeGuid))
                {
                    if (selectedShapeGuid == SeamingTool.Shape.Guid)
                    {
                        CancelDefault = false;
                        return;
                    }
                }
            }

            CancelDefault = true;

            if (ignoreMouseMove)
            {
                return;
            }
 
            if (x < 0.0 || y < 0.0 || x > CurrentPage.PageWidth || y > CurrentPage.PageHeight)
            {
                return;
            }


            if (SystemState.DesignState == DesignState.Area)
            {
                if (SystemState.FixedWidthMode)
                {
                    Coordinate lastMarkerCoordinate = BorderManager.LastMarkerCoordinate();

                    if (Coordinate.IsNullCoordinate(lastMarkerCoordinate))
                    {
                        return;
                    }

                    snapToAxis(lastMarkerCoordinate.X, lastMarkerCoordinate.Y, ref x, ref y);

                    snapToFieldGuide(x, y);

                    return;
                }

                if (Utilities.IsNotNull(buildingPolyline))
                {
                    Coordinate buildingLineLastCoord = buildingPolyline.CoordList.Last();
                    
                    snapToAxis(buildingLineLastCoord.X, buildingLineLastCoord.Y, ref x, ref y);

                    snapToFieldGuide(x, y);

                    return;
                }
               

            }

            // MDD Reset

            if (SystemState.DesignState == DesignState.Seam
                && BaseForm.btnSeamDesignStateSelectionMode.BackColor == Color.Orange
                && BaseForm.btnAutoSelect.Checked
                && !BaseForm.btnTapeMeasure.Checked
                && !SystemState.BtnSetCustomScale.Checked
                && !_measuringStick.IsVisible)
            {
                CanvasLayoutArea initialLayoutArea = null;

                areaMouseOverHitCounter++;

                string areaName = null;

                if (areaMouseOverHitCounter >= 3) // MDD Reset
                {
                    areaMouseOverHitCounter = 0;

                    initialLayoutArea = CurrentPage.GetSelectedLayoutArea(x, y, MaterialsType.Rolls, LayoutAreaType.Normal | LayoutAreaType.OversGenerator);

                    if (Utilities.IsNotNull(initialLayoutArea))
                    {
                        areaName = initialLayoutArea.AreaFinishManager.AreaName;

                        SetSeamSelectedArea(initialLayoutArea);
                    }

                    else
                    {
                        ResetSeamSelectedArea();
                    }
                }

                if (SelectedLayoutArea is null)
                {
                    ResetSeamSelectedArea();
                    return;
                }

                if (KeyboardUtils.DKeyPressed)
                {
                   int xx= 1;
                }

                List<CanvasDirectedLine> initialLineList = CurrentPage.GetSelectedLineShapeList(x, y);

                List<CanvasDirectedLine> perimeterLineList = initialLineList.FindAll(l => l.IsValidLineForSeamSelection());
               
                CanvasDirectedLine perimeterLine = getPerimeterLine(perimeterLineList, SelectedLayoutArea);

                if (Utilities.IsNotNull(perimeterLine))
                {
                    SetSelectedLine(perimeterLine);

                    return;
                }
                else
                {
                    ResetSelectedLine();
                }
            }

            if (SystemState.DrawingMode == DrawingMode.Line1X || SystemState.DrawingMode == DrawingMode.Line2X || SystemState.DrawingMode == DrawingMode.LineDuplicate)
            {
                // Snap to grid is called by the processLineDrawModeMouseMove method

                processLineDrawModeMouseMove(x, y, ref CancelDefault);

                if (Utilities.IsNotNull(LineModeBuildingLine))
                {
                    double x1 = LineModeBuildingLine.Coord1.X;
                    double y1 = LineModeBuildingLine.Coord1.Y;

                    double l = MathUtils.H2Distance(x1, y1, x, y);

                    this.BaseForm.SetLineLengthStatusStripDisplay(CurrentPage.DrawingScaleInInches * l);

                    //if (Utilities.IsNotNull(CurrentPage.CurrentGuide))
                    //{
                    //    snapToAxis(CurrentPage.CurrentGuide.X, CurrentPage.CurrentGuide.Y, ref x, ref y);
                    //}
                }

                //snapToFieldGuide(x, y);
                return;
            }

            if (SystemState.DrawingMode == DrawingMode.Default || !SystemState.DrawingShape)
            {
                snapToFieldGuide(x, y);

                return;
            }


            if (SystemState.DrawingMode == DrawingMode.Polyline)
            {
                if (this.BuildingPolyline is null)
                {
                    snapToFieldGuide(x, y);

                    return;
                }

                if (this.BuildingPolyline.CoordList.Count <= 0)
                {
                    snapToFieldGuide(x, y);

                    return;
                }

                double x1 = BuildingPolyline.GetLastCoordinate().X;
                double y1 = BuildingPolyline.GetLastCoordinate().Y;

                snapToAxis(x1, y1, ref x, ref y);
                
                double length = MathUtils.H2Distance(x1, y1, x, y);

                this.BaseForm.SetLineLengthStatusStripDisplay(CurrentPage.DrawingScaleInInches * length);

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.ScaleLine)
            {
                
                if (!Coordinate.IsNullCoordinate(this.ScaleRuleController.ScaleFrstCoord))
                {
                    double x1 = this.ScaleRuleController.ScaleFrstCoord.X;
                    double y1 = this.ScaleRuleController.ScaleFrstCoord.Y;

                    snapToAxis(x1, y1, ref x, ref y);
                }
               

                return;
            }

            else if (SystemState.DrawingMode == DrawingMode.BorderGeneration)
            {
                double length = BorderManager.LastBoundaryGuideLength();

                this.BaseForm.SetLineLengthStatusStripDisplay(CurrentPage.DrawingScaleInInches * length);

                if (IsSnapToGridActive())
                {
                    Coordinate lastMarkerCoord = BorderManager.LastMarkerCoordinate();

                    if (!Coordinate.IsNullCoordinate(lastMarkerCoord))
                    {

                        double x1 = lastMarkerCoord.X;
                        double y1 = lastMarkerCoord.Y;

                        snapToAxis(x1, y1, ref x, ref y);
                    }
                }
            }

            else if (SystemState.DrawingMode == DrawingMode.TapeMeasure)
            {
                if (!Coordinate.IsNullCoordinate(this.TapeMeasureController.MeasureFrstCoord) &&
                    Coordinate.IsNullCoordinate(this.TapeMeasureController.MeasureScndCoord))
                {
                    double x1 = this.TapeMeasureController.MeasureFrstCoord.X;
                    double y1 = this.TapeMeasureController.MeasureFrstCoord.Y;

                    if (IsSnapToGridActive())
                    {
                        snapToAxis(x1, y1, ref x, ref y);
                    }

                    double length = MathUtils.H2Distance(x, y, x1, y1);

                    this.BaseForm.SetLineLengthStatusStripDisplay(CurrentPage.DrawingScaleInInches * length);
 
                }

                if (GlobalSettings.SnapToAxis && KeyboardUtils.CntlKeyPressed)
                {
                    if (!Coordinate.IsNullCoordinate(this.TapeMeasureController.MeasureFrstCoord))
                    {
                        double x1 = this.TapeMeasureController.MeasureFrstCoord.X;
                        double y1 = this.TapeMeasureController.MeasureFrstCoord.Y;

                        snapToAxis(x1, y1, ref x, ref y);
                      
                        if (Coordinate.IsNullCoordinate(this.TapeMeasureController.MeasureScndCoord))
                        {
                            double length = MathUtils.H2Distance(x, y, x1, y1);

                            this.BaseForm.SetLineLengthStatusStripDisplay(CurrentPage.DrawingScaleInInches * length);
                        }

                    }
                }
            }
        }

        private bool IsSnapToGridActive()
        {
            bool isSnapToGridActive = 
                GlobalSettings.SnapToAxis
               // && !FieldGuideController.SuspendAlignMode
                && ((KeyboardUtils.SpaceKeyPressed && !BaseForm.btnSnapToGrid.Checked) || (!KeyboardUtils.SpaceKeyPressed && BaseForm.btnSnapToGrid.Checked));
            //Debug.WriteLine($"IsSnapToGridActive:  {isSnapToGridActive}");
            return isSnapToGridActive;
        }

        private void SetSelectedLine(CanvasDirectedLine perimeterLine)
        {
            if (SelectedLine is null)
            {
                perimeterLine.Shape.SetLineColor(GlobalSettings.AreaEditSettingColor2);
                perimeterLine.Shape.SetLineWidth(5);

                VisioInterop.BringToFront(perimeterLine.Shape);

                SelectedLine = perimeterLine;
            }

            else if (perimeterLine != SelectedLine)
            {
                perimeterLine.Shape.SetLineColor(GlobalSettings.AreaEditSettingColor2);
                perimeterLine.Shape.SetLineWidth(5);

                VisioInterop.BringToFront(perimeterLine.Shape);

                SelectedLine.SetLineGraphics(DesignState.Seam, false, AreaShapeBuildStatus.Completed);

                SelectedLine = perimeterLine;
            }
        }

        public void ResetSelectedLine()
        {
            if (Utilities.IsNotNull(SelectedLine))
            {
                SelectedLine.SetLineGraphics(DesignState.Seam, false, AreaShapeBuildStatus.Completed);

                SelectedLine = null;
            }
        }

        private void SetSeamSelectedArea(CanvasLayoutArea initialLayoutArea)
        {
            if (SelectedLayoutArea is null)
            {
                SelectedLayoutArea = initialLayoutArea;

                SelectedLayoutArea.SetFillColor(GlobalSettings.AreaEditSettingColor1);
            }

            else
            {
                SelectedLayoutArea.SetFillColor(SelectedLayoutArea.AreaFinishManager.Color);

                SelectedLayoutArea = initialLayoutArea;

                SelectedLayoutArea.SetFillColor(GlobalSettings.AreaEditSettingColor1);
            }
        }

        public void ResetSeamSelectedArea()
        {
            if (Utilities.IsNotNull(SelectedLayoutArea))
            {

                SelectedLayoutArea.SetFillColor(SelectedLayoutArea.AreaFinishManager.Color);

                SelectedLayoutArea = null;
            }
        }

        public void ResetSeamSelectedObjects()
        {
            ResetSelectedLine();

            ResetSeamSelectedArea();
        }

        private CanvasDirectedLine getPerimeterLine(List<CanvasDirectedLine> perimeterLineList, CanvasLayoutArea selectedLayoutArea)
        {
            if (perimeterLineList is null)
            {
                return null;
            }

            if (perimeterLineList.Count <= 0)
            {
                return null;
            }

            if (selectedLayoutArea is null)
            {
                return perimeterLineList[0];
            }

            foreach (CanvasDirectedLine canvasDirectedLine in perimeterLineList)
            {
                if (canvasDirectedLine.ParentLayoutArea == selectedLayoutArea)
                {
                    return canvasDirectedLine;
                }
            }

            return perimeterLineList[0];
        }

        private void snapToAxis(double x1, double y1, ref double x, ref double y)
        {
            doSnapToAxis(x1, y1, ref x, ref y);

            return;
        }

        Point prev_P = new Point(0, 0);

        private void doSnapToAxis(double x1, double y1, ref double x, ref double y)
        {
            if (Utilities.IsNotNull(ScaleRuleController))
            {
                if (ScaleRuleController.ScaleLineState == ScaleLineState.ScndPointSelected)
                {
                    return;
                }

            }

            //if (Utilities.IsNotNull(BorderManager) && BaseForm.ckbFixedWidth.Checked)
            //{
            //    if (BorderManager.BorderGenerationState == BorderGenerationState.OngoingBorderBuild)
            //    {
            //        return;
            //    }
            //}
            
            if (IsSnapToGridActive())
            {
                string returnInfo = null;

                Point cursorPosition = StaticGlobals.GetCursorPosition();

                Point p = SnapToGridController.SnapToGrid(x1, y1, ref x, ref y, cursorPosition, SystemState.BtnShowFieldGuides.Checked, ref returnInfo);

                if (p != prev_P)
                {
                    //Debug.WriteLine("SnapToGrid((" + x1.ToString("##0.00") + ", " + y1.ToString("##0.00") + "), (" + x.ToString("##0.00") + ", " + y.ToString("##0.00") + "), " + cursorPosition.ToString() + ") = " + p.ToString() );

                    //Debug.WriteLine(returnInfo);

                    prev_P = p;
                }

                StaticGlobals.SetCursorPosition(p);
            }
        }

        private void snapToFieldGuide(double x, double y)
        {
            if (Utilities.IsNotNull(ScaleRuleController))
            {
                if (ScaleRuleController.ScaleLineState == ScaleLineState.ScndPointSelected)
                {
                    return;
                }

            }

            if (IsSnapToGridActive())
            {
                Point p;

                if (FieldGuideController.SnapToFieldGuide(x, y, 0, StaticGlobals.GetCursorPosition(), out p))
                {
                    StaticGlobals.SetCursorPosition(p);
                }
            }
        }

    }
}
