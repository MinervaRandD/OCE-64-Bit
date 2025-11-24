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


using Microsoft.VisualBasic.Devices;

namespace FloorMaterialEstimator.CanvasManager
{
    using AxMicrosoft.Office.Interop.VisOcx;
    using CanvasLib.Borders;
    using CanvasLib.Legend;
    using CanvasLib.Markers_and_Guides;
    using CanvasLib.Scale_Line;
    using FinishesLib;
    using FloorMaterialEstimator;
    using Geometry;
    using Globals;
    using Graphics;
    using MaterialsLayout;
    using SettingsLib;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Windows.Forms;
    using Utilities;
    using Visio = Microsoft.Office.Interop.Visio;

    public partial class CanvasManager
    {


        double mouseDownX = 0;
        double mouseDownY = 0;

        private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            if (KeyButtonState == 1)
            {
                mouseDownX = x;
                mouseDownY = y;

                CancelDefault = false;
                return;
            }

            doMouseDown(Button, KeyButtonState, x, y, ref CancelDefault);

            if (DesignState == DesignState.Seam && Button == 1)
            {
                CancelDefault = false;
            }

            if (KeyButtonState == 1)
            {
                CancelDefault = false;

            }

            CancelDefault = false;
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

                return;

                //CancelDefault = true;

                //Window.DeselectAll();

                //return;
            }

            if (SystemState.DrawingMode == DrawingMode.TextBoxCreate && button == 2)
            {
                Page.SelectedTextBox = Page.DrawRectangle(null, x, y, x + 2, y + 1); 

                string guid = GuidMaintenance.CreateGuid(Page.SelectedTextBox);

                Page.SelectedTextBox.SetShapeData("[TextBox]", "Rectangle", guid);

                Page.AddToPageShapeDict(Page.SelectedTextBox);

                
                SystemState.DrawingMode = DrawingMode.TextBoxEdit;

                CancelDefault = false;

                return;
            }

            if (SystemState.DrawingMode == DrawingMode.ArrowCreate && button == 2)
            {
                Page.SelectedCanvasArrow = Page.DrawPolyline(null, new double[] { x, y, x + 1.25, y, x + 0.82, y + 0.225 }, 8);

                Page.SelectedCanvasArrow.ParentObject = Page.SelectedCanvasArrow;

                VisioInterop.SetBaseLineColor(Page.SelectedCanvasArrow, SelectedAreaFinishManager.Color);

                VisioInterop.SetLineWidth(Page.SelectedCanvasArrow, 2);

                string guid = GuidMaintenance.CreateGuid(Page.SelectedCanvasArrow);

                Page.SelectedCanvasArrow.SetShapeData("[CanvasArrow]", "Polyline", guid);

                SystemState.DrawingMode = DrawingMode.ArrowEdit;

                CancelDefault = false;

                return;
            }

            if ((Page.SelectedTextBox = Page.MouseOverTextBox(x, y)) != null)
            {
                if (SystemState.DrawingMode == DrawingMode.TextBoxEdit)
                {
                    CancelDefault = false;

                    return;
                }

                else
                {
                    VisioInterop.SelectShape(Window, Page.SelectedTextBox);


                    SystemState.DrawingMode = DrawingMode.TextBoxEdit;

                    CancelDefault = false;

                    return;
                }
            }

            else if (SystemState.DrawingMode == DrawingMode.TextBoxEdit)
            {

                Window.DeselectAll();

                SystemState.DrawingMode = DrawingMode.Default;

               // Page.SelectedTextBox = null;

                CancelDefault = true;

                return;
            }

            if (Page.MouseOverCanvasArrow(x, y))
            {
                if (SystemState.DrawingMode == DrawingMode.ArrowEdit)
                {
                    CancelDefault = false;

                    return;
                }

                else
                {
                    VisioInterop.SelectShape(Window, Page.SelectedTextBox);

                    SystemState.DrawingMode = DrawingMode.ArrowEdit;

                    CancelDefault = false;

                    return;
                }
            }

            if (SystemState.DrawingMode == DrawingMode.TextBoxEdit)
            {
                SystemState.DrawingMode = DrawingMode.Default;

                Window.DeselectAll();

                CancelDefault = false;

                return;
            }
            //    else
            //    {
            //        Window.DeselectAll();

            //        SystemState.DrawingMode = DrawingMode.Default;
            //    }
            //}

            // Display button click

            if (button == 2)
            {
                BaseForm.MRgt.Image = SystemGlobals.MRghtButtonDownImage;
                BaseForm.MLft.Image = SystemGlobals.MButtonUpImage;
            }

            else
            {
                BaseForm.MLft.Image = SystemGlobals.MLeftButtonDownImage;
                BaseForm.MRgt.Image = SystemGlobals.MButtonUpImage;
            }

            if (BaseForm.btnPanMode.Checked == true)
            {
                processPanModeMouseDown(button, KeyButtonState, x, y, ref CancelDefault);

                CancelDefault = true;

                return;
            }

            if (KeyboardUtils.AltKeyPressed && KeyboardUtils.CntlKeyPressed && button == 2)
            {

                Point baseCursorPosition = SystemGlobals.GetCursorPosition();

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

                        Point baseCursorPosition = SystemGlobals.GetCursorPosition();

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

        private GraphicShape mouseOverDisplaySeam(double x, double y)
        {
            List<CanvasSeam> seamList = new List<CanvasSeam>();

            Visio.Selection selection = CurrentPage.VisioPage.SpatialSearch[
                x, y,
                (short)(Visio.VisSpatialRelationCodes.visSpatialContain | Visio.VisSpatialRelationCodes.visSpatialOverlap |
                Visio.VisSpatialRelationCodes.visSpatialTouching),
                .1, 0];

            if (selection is null)
            {
                return null;
            }

            if (selection.Count <= 0)
            {
                return null;
            }

            foreach (Visio.Shape visioShape in selection)
            {

                if (visioShape.Data1 != "[DisplaySeam]")
                {
                    continue;
                }

                if (!CurrentPage.PageShapeDict.ContainsKey(visioShape.Data3))
                {
                    continue;
                }

                return (GraphicShape) CurrentPage.PageShapeDict[visioShape.Data3];
              
            }

            return null;
        }

        private void VsoWindow_MouseUp(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
#if true
            if (button == 1 && MathUtils.H0Dist(this.mouseDownX, this.mouseDownY, x, y) > 0.1 && !KeyboardUtils.CntlKeyPressed)
            {
                
                var selectedShapes = VisioInterop.SelectShapes(Page, mouseDownX, mouseDownY, x, y);

                cancelDefault = false;

                Window.VisioWindow.DeselectAll();

                //if (selectedShapes.Count() == 0)
                //{
                   
                //    return;
                //}

                if (SystemState.DesignState == DesignState.Line)
                {
                    processLineDesignStateLayoutModeNormalSelectDrag(selectedShapes);
                    
                }

                if (SystemState.DesignState == DesignState.Area)
                {
                    processAreaDesignStateLayoutModeNormalSelectDrag(selectedShapes);
                }

                if (SystemState.DesignState == DesignState.Seam)
                {
                    foreach (GraphicShape graphicShape in selectedShapes)
                    {
                        
                    }
                }
                return;
            }
#endif
            doVsoWindow_MouseUp(button, keyButtonState, x, y, ref cancelDefault);

            cancelDefault = true;
        }

        private void doVsoWindow_MouseUp(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            if (SystemState.DrawingMode == DrawingMode.TextBoxCreate || SystemState.DrawingMode == DrawingMode.TextBoxEdit ||
                SystemState.DrawingMode == DrawingMode.ArrowCreate || SystemState.DrawingMode == DrawingMode.ArrowEdit)
            {
                //if (Page.SelectedTextBox != null)
                //{
                //    VisioInterop.SelectShape(Window, Page.SelectedTextBox);
                //}
                cancelDefault = false;

                return;
            }

            if (BaseForm.btnPanMode.Checked == true)
            {
                processPanModeMouseUp(button, keyButtonState, x, y, ref cancelDefault);

                return;
            }

            if (button == 2)
            {
                BaseForm.MRgt.Image = SystemGlobals.MButtonUpImage;
            }
            else
            {
                BaseForm.MLft.Image = SystemGlobals.MButtonUpImage;
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

            if (MathUtils.H0Dist(this.mouseDownX, this.mouseDownY, x, y) <= 0.1)
            {
                VsoWindow_MouseClick(button, keyButtonState, x, y, ref cancelDefault);


                cancelDefault = true;

                //Window.DeselectAll();

                return;
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

            cancelDefault = true;
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private void doVsoWindow_MouseClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            try
            {


                if (SystemState.DrawingMode == DrawingMode.TextBoxCreate ||
                    SystemState.DrawingMode == DrawingMode.TextBoxEdit ||
                    SystemState.DrawingMode == DrawingMode.ArrowCreate ||
                    SystemState.DrawingMode == DrawingMode.ArrowEdit)
                {
                    cancelDefault = false;

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

                if (!BaseForm.btnTapeMeasure.Checked && !SystemState.BtnSetCustomScale.Checked &&
                    DesignState == DesignState.Seam)
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

                SystemState.CurrentProjectChanged = true;

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

               
                if (button == 1 && KeyboardUtils.ShiftKeyPressed)
                {
                    if (LegendController.AreaModeLegendIsActive())
                    {
                        if (SystemState.DesignState == DesignState.Area && SystemGlobals.ShowAreaLegendInAreaMode ||
                            SystemState.DesignState == DesignState.Line && SystemGlobals.ShowAreaLegendInLineMode)
                        {
                            if (LegendController.AreaModeLegendNavigationForm.LocateToClick)
                            {

                                LegendController.AreaModeLegend.LocateToClick(x, y);

                               // LegendController.SetShowLegendText("Hide Legend");

                                LegendController.AreaModeLegend.ShowLegend(true);

                            }

                            LegendController.AreaModeLegendNavigationForm.Activate();
                        }

                    }

                    else if (LegendController.LineModeLegendIsActive())
                    {
                        if (SystemState.DesignState == DesignState.Area && SystemGlobals.ShowLineLegendInAreaMode ||
                            SystemState.DesignState == DesignState.Line && SystemGlobals.ShowLineLegendInLineMode)
                        {
                            if (LegendController.LineModeLegendNavigationForm.LocateToClick)
                            {
                                double size = LegendController.LineModeLegend.CurrentSize;

                                LegendController.LineModeLegend.LocateToClick(x, y);

                              
                                LegendController.LineModeLegend.ShowLegend(true);


                            }


                            LegendController.LineModeLegendNavigationForm.Activate();
                        }
                    }
                }

                else
                {
                    LegendController.AreaModeLegendNavigationForm.IsActive = false;
                    LegendController.LineModeLegendNavigationForm.IsActive = false;
                }
            }

            catch (Exception ex)
            {
                ;
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

            List<GraphicShape> filteredShapeList = graphicsSelection.FilterByData1("PlaceMarker");

            if (filteredShapeList.Count <= 0)
            {
                PlaceMarker placeMarker = new PlaceMarker(Window, Page, x, y, 0.2);

                placeMarker.Draw();

                CurrentPage.AreaModeGlobalLayer.AddShape(placeMarker.Shape, 1);
                CurrentPage.SeamModeGlobalLayer.AddShape(placeMarker.Shape, 1);

                Page.AddToPageShapeDict(placeMarker);
            }

            else
            {
                foreach (GraphicShape filteredShape in filteredShapeList)
                {
                    Page.RemoveFromPageShapeDict(filteredShape);

                    filteredShape.Delete();
                }
            }
        }

        //private ToolTip lockedToolTip = new ToolTip();

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
                    ManagedMessageBox.Show("Field guides are hidden. Switch to 'Show Field Guides' to add guides.");
                 
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
                    ManagedMessageBox.Show("Field guides are hidden. Switch to 'Show Field Guides' to add guides.");

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
                    ManagedMessageBox.Show("Field guides are hidden. Switch to 'Show Field Guides' to add guides.");

                }

                return true;
            }

            return false;
        }

        //bool onCanvas = false;

        bool ignoreMouseMove = false;

        private int areaMouseOverHitCounter = 0;

        double lastX = 0;
        double lastY = 0;

        private void VsoWindow_MouseMove(int button, int keyButtonState, double x, double y, ref bool CancelDefault)
        {
            //Mdd//
            //CancelDefault = false;
            //return;

         //   VsoApplication.ScreenUpdating = 1;

            // VsoApplication.DoCmd((short)Visio.VisUICmds.visCmdDRPointerTool);

            if (snapToGridIgnore(x, y))
            {
                // This is to exit quickly to avoid 'bounces' in mouse. It occurs because many events can occur while mouse moves.

                lastX = x;
                lastY = y;

                return;
            }

            lastX = x;
            lastY = y;

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

            BaseForm.tlsCursorPosition.Text = "(" + cursorGridPosnX.ToString("0.00").PadLeft(5) + ", " + cursorGridPosnY.ToString("0.00").PadLeft(5) + ")";

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

                    //snapToFieldGuide(x, y);

                    return;
                }

                if (Utilities.IsNotNull(buildingPolyline))
                {
                    Coordinate buildingLineLastCoord = buildingPolyline.CoordList.Last();
                    
                    snapToAxis(buildingLineLastCoord.X, buildingLineLastCoord.Y, ref x, ref y);

                    //snapToFieldGuide(x, y);

                    return;
                }
               

            }

            // MDD Reset
           CanvasLayoutArea initialLayoutArea;

            if (SystemState.DesignState == DesignState.Seam
                && BaseForm.btnSeamDesignStateSelectionMode.BackColor == Color.Orange
                && BaseForm.btnAutoSelect.Checked
                && !BaseForm.btnTapeMeasure.Checked
                && !SystemState.BtnSetCustomScale.Checked
                && !_measuringStick.IsVisible)
            {
                initialLayoutArea = CurrentPage.GetSelectedLayoutArea(x, y, MaterialsType.Rolls, LayoutAreaType.Normal | LayoutAreaType.Normal);
                if (initialLayoutArea == null)
                {
                    ResetSelectedLine();
                }

                else
                {
                    
                }

                areaMouseOverHitCounter++;

                if (areaMouseOverHitCounter >= 1) // MDD Reset
                {
                    areaMouseOverHitCounter = 0;

                    initialLayoutArea = CurrentPage.GetSelectedLayoutArea(x, y, MaterialsType.Rolls, LayoutAreaType.Normal | LayoutAreaType.OversGenerator);


                    if (Utilities.IsNotNull(initialLayoutArea))
                    {
                        if (initialLayoutArea.AreaFinishBase.SeamAreaLocked)
                        {
                            LockIcon.ShowLockIcon(initialLayoutArea.Shape);
                            
                            return;
                        }

                        LockIcon.HideLockIcon();

                        SetSeamSelectedArea(initialLayoutArea);
                    }

                    else
                    {
                        ResetSeamSelectedArea();
                    }
                }
                
                if (SelectedLayoutArea is null)
                {
                    LockIcon.HideLockIcon();
                    ResetSeamSelectedArea();
                    return;
                }


                List<CanvasDirectedLine> initialLineList = CurrentPage.GetSelectedLineShapeList(x, y);

                List<CanvasDirectedLine> perimeterLineList = initialLineList.FindAll(l => l.IsValidLineForSeamSelection());
               
                CanvasDirectedLine perimeterLine = getPerimeterLine(perimeterLineList, SelectedLayoutArea);

                if (Utilities.IsNotNull(perimeterLine))
                {
                    if (perimeterLine.ParentLayoutArea.AreaFinishBase.SeamAreaLocked)
                    {
                        return;
                    }

                    if (KeyboardUtils.AltKeyPressed)
                    {
                        return;
                    }

                    SetSelectedLine(perimeterLine);

                    return;
                }
                //else
                //{
                //    ResetSelectedLine();
                //}
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

            if (SystemState.DrawingMode == DrawingMode.TapeMeasure)
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

           
        }

        //public GraphicShape lockIcon = null;

        //public bool lockIconVisible = false;

        //public CanvasLayoutArea lockIconLayoutArea = null;

        //private void ShowLockIcon(CanvasLayoutArea canvasLayoutArea)
        //{
           
        //    if (lockIcon == null)
        //    {
        //        Visio.Shape visioLockIconShape = VisioInterop.CreateLockIcon(Page.VisioPage);

        //        lockIcon = new GraphicShape(this, Window, Page, visioLockIconShape, ShapeType.Image);

        //        lockIcon.SetShapeData("[LockIcon]", "Image", lockIcon.Guid);

        //        CurrentPage.AddToPageShapeDict(lockIcon);
        //    }

        //    if (canvasLayoutArea == lockIconLayoutArea && lockIconVisible)
        //    {
        //        return;
        //    }

        //    Coordinate areaCenter = canvasLayoutArea.Centroid;

        //    //ShapeSize shapeSize = VisioInterop.GetShapeDimensions(canvasLayoutArea.Shape);

        //    double shapeArea = VisioInterop.GetShapeArea(canvasLayoutArea.Shape);

        //    VisioInterop.BeginFastUpdate(VsoApplication);

        //    VisioInterop.SetShapeCenter(lockIcon, areaCenter);

        //    double w = Math.Sqrt(shapeArea) * 0.4;
        //    double h = w;

        //    double d = Math.Min(w, h) * 0.25;

        //    VisioInterop.SetShapeSize(lockIcon, h, w);

        //    lockIconVisible = true;

        //    lockIconLayoutArea = canvasLayoutArea;

        //    VisioInterop.EndFastUpdate(VsoApplication);
        //    //Page.LockIconLayer.SetLayerVisibility(true);
        //}

        //private void HideLockIcon()
        //{
        //    if (lockIconVisible == false)
        //    {
        //        return;
        //    }


        //    VisioInterop.BeginFastUpdate(VsoApplication);
        //    VisioInterop.SetShapeSize(lockIcon, 0, 0);
        //    VisioInterop.EndFastUpdate(VsoApplication);

        //  lockIconVisible = false;

        //  lockIconLayoutArea = null;
        //}

        private bool snapToGridIgnore(double x, double y)
        {
            if (!GlobalSettings.SnapToAxis)
            {
                return false;
            }

            if (Math.Max(Math.Abs(lastX - x), Math.Abs(lastY - y)) > 0.05)
            {
                return true;
            }

            return false;
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
                perimeterLine.Shape.SetLineOpacity(0.333);

                VisioInterop.BringToFront(perimeterLine.Shape);

                SelectedLine = perimeterLine;
            }

            else if (perimeterLine != SelectedLine)
            {
                perimeterLine.Shape.SetLineColor(GlobalSettings.AreaEditSettingColor2);
                perimeterLine.Shape.SetLineWidth(5);
                perimeterLine.Shape.SetLineOpacity(0.333);
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
            if (SelectedLayoutArea != null)
            {
                if (SelectedLayoutArea == initialLayoutArea)
                {
                    return; // Assumes color has not changed
                }

                else
                {
                    Console.WriteLine("Setting selected layout area color and fill pattern");
                    SelectedLayoutArea.SetFillColorAndPattern(SelectedLayoutArea.AreaFinishBase.Color, SelectedLayoutArea.AreaFinishBase.Pattern);

                }
            }

            Console.WriteLine("Setting selected layout area color and fill pattern");
            SelectedLayoutArea = initialLayoutArea;
            SelectedLayoutArea.SetFillColor(GlobalSettings.AreaEditSettingColor1);
        }

        public void ResetSeamSelectedArea()
        {
            if (Utilities.IsNotNull(SelectedLayoutArea))
            {

                SelectedLayoutArea.SetFillColorAndPattern(SelectedLayoutArea.AreaFinishBase.Color, SelectedLayoutArea.AreaFinishBase.Pattern);

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

            if (IsSnapToGridActive())
            {
                string returnInfo = null;

                Point cursorPosition = SystemGlobals.GetCursorPosition();

                Point p = SnapToGridController.SnapToGrid(x1, y1, ref x, ref y, cursorPosition, SystemState.BtnShowFieldGuides.Checked, ref returnInfo);

                SystemGlobals.SetCursorPosition(p);

                prev_P = p;
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

                if (FieldGuideController.SnapToFieldGuide(x, y, 0, SystemGlobals.GetCursorPosition(), out p))
                {
                    SystemGlobals.SetCursorPosition(p);
                }
            }
        }

    }
}
