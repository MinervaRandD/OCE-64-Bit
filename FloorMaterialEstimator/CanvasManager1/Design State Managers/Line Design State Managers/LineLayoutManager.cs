namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Geometry;
    using Graphics;
    using Utilities;
    using SettingsLib;
    using TracerLib;

    using Globals;
    using CanvasLib.Markers_and_Guides;
    using CanvasLib.DoorTakeouts;
    using FloorMaterialEstimator.Finish_Controls;

    using Visio = Microsoft.Office.Interop.Visio;
    using System.Diagnostics;

    public partial class CanvasManager
    {
        private LineLayoutState lineLayoutState = LineLayoutState.Unknown;

       
        public LineLayoutState LineLayoutState
        { 
            get
            {
                return lineLayoutState;
            }

            set
            {
                if (lineLayoutState == value)
                {
                    return;
                }

                lineLayoutState = value;

                //------------------------------------------------------------------------//
                // BaseForm.btnLayoutLineJump.Enabled = true; below is redundant. However //
                // this is due to a recent change request from Martin so it is left in    //
                // in place as is in case Martin wants to change it back.                 //
                //------------------------------------------------------------------------//

                if (LineLayoutState == LineLayoutState.New1XLineSequenceStarted)
                {
                    BaseForm.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                }

                else if (lineLayoutState == LineLayoutState.Ongoing1XLineSequence)
                {
                    //-----------------------------------------------//
                    // This is defensive. It should not be necessary //
                    //-----------------------------------------------//
                    BaseForm.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                }

                //else
                //{                
                //    BaseForm.LblNewSequence.BackColor = Color.Orange;
                //}

                BaseForm.tlsDesignState.Text = "Line (" + lineLayoutState.ToString() + ")";
            }
        }

        private void processLineDesignStateLayoutModeClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            Window?.DeselectAll();

            cancelDefault = true;

            if (button == 1)
            {
                processLineDesignStateLayoutModeLeftClick(x, y);

                return;
            }

            if (button == 2)
            {
                processLineDesignStateLayoutModeRghtClick(x, y);

                return;
            }

            Window?.DeselectAll();
        }

        public void processLineDesignStateLayoutModeLeftClick(double x, double y)
        {
            CanvasDirectedLine canvasDirectedLine = getSelectedLine(x, y);

            if (canvasDirectedLine is null)
            {
                return;
            }

            if (KeyboardUtils.ShiftKeyPressed)
            {
                CurrentPage.UpdateLineDesignStateAreaSelectionStatus(canvasDirectedLine);

                return;
            }

            if (canvasDirectedLine.LineDesignStateEditModeSelected)
            {
                canvasDirectedLine.SetSelectionMode(false);

                return;
            }

            List<CanvasDirectedLine> selectedLineList = new List<CanvasDirectedLine>(CurrentPage.SelectedLines);

            foreach (CanvasDirectedLine selectedLine in selectedLineList)
            {
                if (selectedLine != canvasDirectedLine)
                {
                    selectedLine.SetSelectionMode(false);
                }
            }

            CurrentPage.UpdateLineDesignStateAreaSelectionStatus(canvasDirectedLine);
        }

        public void processLineDesignStateLayoutModeRghtClick(double x, double y)
        {


            if (KeyboardUtils.DKeyPressed)
            {
                processLineLayoutRghtDeleteClick(x, y); 

                return;
            }

            if (BaseForm.LayoutLineMode == LineDrawingMode.TakeoutArea)
            {
                processLineLayoutModeTakeoutAreaClick(x, y);
            }

            else if (BaseForm.CounterMode)
            {
                CounterController.ProcessCounterModeClick(x, y);
            }

            else if (BaseForm.LayoutLineMode == LineDrawingMode.Duplicate)
            {
                processLineLayoutModeDuplicateClick(x, y);
            }

            else if (BaseForm.btnLayoutLine2XMode.BackColor == Color.Orange)
            {
                processLineLayoutMode2XLineDrawClick(x, y);
            }

            else if (BaseForm.BtnLayoutLine1XMode.BackColor == Color.Orange)
            {
                processLineLayoutMode1XLineDrawClick(x, y);
            }

            //foreach (UCLineFinishPaletteElement lineFinishElement in linePalette)
            //{
            //    lineFinishElement.LineFinishManager.SetLineState(DesignState.Line, SeamMode.Selection, true);
            //}
            
            Window?.DeselectAll();
        }


        private void processLineLayoutRghtDeleteClick(double x, double y)
        {
            CanvasDirectedLine canvasDirectedLine = CurrentPage.GetSelectedLineShape(x, y);

            if (Utilities.IsNotNull(canvasDirectedLine is null))
            {
                ProcessEditLineModeActionDeleteLine(canvasDirectedLine);
            }
        }


        #region 1X line mode

        public Coordinate Line1XModeBaseCoord
        {
            get;
            set;
        } = Coordinate.NullCoordinate;

        private void processLineLayoutMode1XLineDrawClick(double x, double y)
        {
            if (KeyboardUtils.AltKeyPressed)
            {
                processLayout1XModeGuideDrawClick(x, y);
            }

            else if (KeyboardUtils.F1KeyPressed)
            {
                processLayout1XModeHGuideDrawClick(x, y);
            }

            else if (KeyboardUtils.F2KeyPressed)
            {
                processLayout1XModeVGuideDrawClick(x, y);
            }

            else
            {
                processLineLayoutMode1XNormalDrawClick(x, y);
            }
        }

        private void processLineLayoutMode1XNormalDrawClick(double x, double y)
        {
            if (Coordinate.IsNullCoordinate(Line1XModeBaseCoord))
            {
                // If here, then we are starting a new multi line.

                Line1XModeBaseCoord = new Coordinate(x, y);

                // The following determines whether we draw a single circle marker or a double circle marker

                //string guid = GuidMaintenance.GenerateGuid();

                CurrentPage.PlaceLineModeStartMarker(Line1XModeBaseCoord, 0.075, SelectedLineType.LineColor, StartMarkerType.Single);

                SetupMarkerAndGuides(Line1XModeBaseCoord);
               
                LineLayoutState = LineLayoutState.New1XLineSequenceStarted;

                Page.LastPointDrawn = new Coordinate(x, y);

                SystemState.DrawingShape = true;
            }

            else
            {

                Coordinate coord1 = Line1XModeBaseCoord;
                Coordinate coord2 = new Coordinate(x, y);

                if (Coordinate.H2Distance(coord1, coord2) <= clickResolution)
                {
                    // Ignore this click. The line must have positive length

                    return;
                }

                // The following is necessary to update the coordinates of base class
                // DirectedLine of building polyline. One could update these coordinates at
                // each mouse move event, but this would make the drawout too slow. It's a bit of 
                // a kludge.

                CanvasDirectedLine canvasDirectedLine = add1XLine(coord1, coord2);

                if (LineLayoutState == LineLayoutState.New1XLineSequenceStarted)
                {
                    canvasDirectedLine.FirstLineInSequence = true;
                }

                LineLayoutState = LineLayoutState.Ongoing1XLineSequence;

                Line1XModeBaseCoord = coord2;

                Page.LastPointDrawn = new Coordinate(x, y);

                SetupMarkerAndGuides(Line1XModeBaseCoord);

            }
        }

        private void processLayout1XModeGuideDrawClick(double x, double y)
        {
            double? guideX = null;
            double? guideY = null;

            if (!FieldGuideController.GetClosestGuides(x, y, out guideX, out guideY, GlobalSettings.SnapDistance, 0))
            {
                MessageBox.Show("No guideline found.");
                return;
            }

            if (guideX.HasValue && guideY.HasValue)
            {
                if (Math.Abs(guideX.Value - x) <= Math.Abs(guideY.Value - y))
                {
                    Add1XLineFromGuidesVertical(guideX.Value, y);
                }

                else
                {
                    Add1XLineFromGuidesHorizontal(x, guideY.Value);
                }
            }

            else if (guideX.HasValue)
            {
                Add1XLineFromGuidesVertical(guideX.Value, y);
            }

            else
            {
                Add1XLineFromGuidesHorizontal(x, guideY.Value);
            }

        }

        private void Add1XLineFromGuidesHorizontal(double x, double y)
        {
            Coordinate coord1;
            Coordinate coord2;

            if (!getHorizontalLineBounds(x, y, out coord1, out coord2))
            {
                return;
            }

            CanvasDirectedLine newHorizontalLine = add1XLine(coord1, coord2);

            newHorizontalLine.IsHorizontalGuideLine = true;
        }

        private void Add1XLineFromGuidesVertical(double x, double y)
        {
            Coordinate coord1;
            Coordinate coord2;

            if (!getVerticalLineBounds(x, y, out coord1, out coord2))
            {
                return;
            }

            CanvasDirectedLine newVerticalLine = add1XLine(coord1, coord2);

            newVerticalLine.IsVerticalGuideLine = true;
        }

        private void processLayout1XModeHGuideDrawClick(double x, double y)
        {
            Add1XLineFromGuidesHorizontal(x, y);

            lineLayoutState = LineLayoutState.Default;
        }

        private void processLayout1XModeVGuideDrawClick(double x, double y)
        {
            Add1XLineFromGuidesVertical(x, y);

            lineLayoutState = LineLayoutState.Default;
        }

        private CanvasDirectedLine add1XLine(Coordinate coord1, Coordinate coord2, DesignState designState = DesignState.Line)
        {
            // Yeah, the following is a redundant mess. Needs to be cleaned up some day.

            this.LineModeBuildingLine = new CanvasDirectedLine(
                this
                ,this.Window
                ,this.CurrentPage
                ,CanvasManagerGlobals.SelectedLineFinishManager
                ,new GraphicsDirectedLine(this.Window, this.CurrentPage, new DirectedLine(coord1, coord2), LineRole.SingleLine, false)
                ,designState
                ,false)
            {
                LineStartCursorPosition = StaticGlobals.GetCursorPosition()
            };

            this.LineModeBuildingLine.Draw(BaseForm.selectedLineFinish.LineColor, BaseForm.selectedLineFinish.LineWidthInPts, BaseForm.selectedLineFinish.VisioDashType);

            Page.AddToPageShapeDict(this.LineModeBuildingLine);

            CanvasManagerGlobals.SelectedLineFinishManager.AddLineFull(this.LineModeBuildingLine);


            CanvasManagerGlobals.SelectedLineFinishManager.LineDesignStateLayer.SetLayerVisibility(true);

            this.CurrentPage.AddToDirectedLineDict(this.LineModeBuildingLine);

            LineModeBuildingLine.SetShapeData("1X Line Mode Line", "line[" + this.SelectedLineType.LineName + "]");

            RemoveMarkerAndGuides();

            //double x = coord2.X;
            //double y = coord2.Y;

            if (GlobalSettings.ShowMarker)
            {
                CurrentPage.PlaceMarker(coord2, GlobalSettings.MarkerWidth);
            }

            if (GlobalSettings.ShowGuides)
            {
                CurrentPage.PlaceGuides(coord2);
            }

            LineHistoryList.Add(LineModeBuildingLine);

            return LineModeBuildingLine;
        }

        #endregion

        #region 2X line mode

        public Coordinate Line2XModeBaseCoord = Coordinate.NullCoordinate;

        private void processLineLayoutMode2XLineDrawClick(double x, double y)
        {
            if (KeyboardUtils.AltKeyPressed)
            {
                processLayout2XModeGuideDrawClick(x, y);
            }

            else if (KeyboardUtils.HKeyPressed)
            {
                processLayout2XModeHGuideDrawClick(x, y);
            }

            else if (KeyboardUtils.VKeyPressed)
            {
                processLayout2XModeVGuideDrawClick(x, y);
            }

            else
            {
                processLineLayoutMode2XNormalDrawClick(x, y);
            }
        }

        private void processLayout2XModeGuideDrawClick(double x, double y)
        {
            double? guideX = null;
            double? guideY = null;

            if (!FieldGuideController.GetClosestGuides(x, y, out guideX, out guideY, GlobalSettings.SnapDistance, 0))
            {
                MessageBox.Show("No guideline found.");
                return;
            }

            if (guideX.HasValue && guideY.HasValue)
            {
                if (Math.Abs(guideX.Value - x) <= Math.Abs(guideY.Value - y))
                {
                    Add2XLineFromGuidesVertical(guideX.Value, y);
                }

                else
                {
                    Add2XLineFromGuidesHorizontal(x, guideY.Value);
                }
            }

            else if (guideX.HasValue)
            {
                Add2XLineFromGuidesVertical(guideX.Value, y);
            }

            else
            {
                Add2XLineFromGuidesHorizontal(x, guideY.Value);
            }

        }

        private void processLayout2XModeHGuideDrawClick(double x, double y)
        {
            RemoveMarkerAndGuides();

            Add2XLineFromGuidesHorizontal(x, y);

            LineLayoutState = LineLayoutState.Default;
        }

        private void processLayout2XModeVGuideDrawClick(double x, double y)
        {
            RemoveMarkerAndGuides();

            Add2XLineFromGuidesVertical(x, y);

            LineLayoutState = LineLayoutState.Default;
        }

        // This list maintains all double lines created in area mode, which are intended to be temporary. When the area design mode is exited, this list
        // is used to remove all these lines in the 'Design State' change logic.

        public List<CanvasDirectedLine> DoubleLinesToRemoveWhenLeavingAreaMode = new List<CanvasDirectedLine>();

        public void Add2XLineFromGuidesHorizontal(double x, double y, DesignState designState = DesignState.Line)
        {
            Coordinate coord1;
            Coordinate coord2;

            if (!getHorizontalLineBounds(x, y, out coord1, out coord2))
            {
                return;
            }

            CanvasDirectedLine newHorizontalLine = add2XLine(coord1, coord2, designState);

            newHorizontalLine.IsHorizontalGuideLine = true;

            newHorizontalLine.Shape.SetShapeData("2X " + designState.ToString() + " Mode Line", "Line[" + SelectedLineType.LineName + "]");

            if (designState == DesignState.Area)
            {
                // These 2x lines are created only temporarily in area mode, and will be removed once area mode is left.

                DoubleLinesToRemoveWhenLeavingAreaMode.Add(newHorizontalLine);
            }
        }


        public void Add2XLineFromGuidesVertical(double x, double y, DesignState designState = DesignState.Line)
        {
            Coordinate coord1;
            Coordinate coord2;

            if (!getVerticalLineBounds(x, y, out coord1, out coord2))
            {
                return;
            }

            CanvasDirectedLine newVerticalLine = add2XLine(coord1, coord2, designState);

            newVerticalLine.IsVerticalGuideLine = true;

            newVerticalLine.Shape.SetShapeData("2X " + designState.ToString() + " Mode Line", "Line[" + SelectedLineType.LineName + "]");

            if (designState == DesignState.Area)
            {
                // These 2x lines are created only temporarily in area mode, and will be removed once area mode is left.

                DoubleLinesToRemoveWhenLeavingAreaMode.Add(newVerticalLine);
            }
        }

        #endregion

        private bool getHorizontalLineBounds(double x, double y, out Coordinate coord1, out Coordinate coord2)
        {
            coord1 = Coordinate.NullCoordinate;
            coord2 = Coordinate.NullCoordinate;

            double? xMin = FieldGuideController.GetLeftGuideLineX(x);

            if (xMin == null)
            {
                MessageBox.Show("No left guide line found.");

                return false;
            }

            if (xMin.Value == -10000)
            {
                MessageBox.Show("No left guide line found.");

                return false;
            }

            double? xMax = FieldGuideController.GetRghtGuideLineX(x);

            if (xMax == null)
            {
                MessageBox.Show("No right guide line found.");
                return false;
            }

            coord1 = new Coordinate(xMin.Value, y);
            coord2 = new Coordinate(xMax.Value, y);

            return true;
        }

        private bool getVerticalLineBounds(double x, double y, out Coordinate coord1, out Coordinate coord2)
        {
            coord1 = Coordinate.NullCoordinate;
            coord2 = Coordinate.NullCoordinate;

            double? yMin = FieldGuideController.GetLowrGuideLineY(y);

            if (yMin == null)
            {
                MessageBox.Show("No lower guide line found.");

                return false;
            }

            if (yMin.Value == -10000)
            {
                MessageBox.Show("No lower guide line found.");

                return false;
            }

            double? yMax = FieldGuideController.GetUpprGuideLineY(y);

            if (yMax == null)
            {
                MessageBox.Show("No upper guide line found.");

                return false;
            }

            coord1 = new Coordinate(x, yMin.Value);
            coord2 = new Coordinate(x, yMax.Value);

            return true;
        }

        private void processLineLayoutMode2XNormalDrawClick(double x, double y)
        {
            if (lineLayoutState == LineLayoutState.Default)
            {
                Line2XModeBaseCoord = new Coordinate(x, y);

                CurrentPage.PlaceLineModeStartMarker(Line2XModeBaseCoord, 0.075, SelectedLineType.LineColor, StartMarkerType.Double);

                SetupMarkerAndGuides(Line2XModeBaseCoord);

                LineLayoutState = LineLayoutState.New2XLineStarted;

                Page.LastPointDrawn = new Coordinate(x, y);

                SystemState.DrawingShape = true;

                return;
            }

            if (lineLayoutState == LineLayoutState.New2XLineStarted)
            {
                Coordinate coord1 = Line2XModeBaseCoord;
                Coordinate coord2 = new Coordinate(x, y);

                if (Coordinate.H2Distance(coord1, coord2) <= clickResolution)
                {
                    // Ignore this click. The line must have positive length

                    return;
                }

                add2XLine(coord1, coord2);

                RemoveMarkerAndGuides();

                LineLayoutState = LineLayoutState.Default;
            }

#if DEBUG
            //UpdateDebugForm();
#endif
        }

        private CanvasDirectedLine add2XLine(Coordinate coord1, Coordinate coord2, DesignState designState = DesignState.Line)
        {
            // Yeah, the following is a redundant mess. Needs to be cleaned up some day.

            DirectedLine directedLine = new DirectedLine(coord1, coord2);

            GraphicsDirectedLine graphicsDirectedLine
                = new GraphicsDirectedLine(Window, Page, directedLine, LineRole.SingleLine, true);
            
            this.LineModeBuildingLine = new CanvasDirectedLine(
                this
                , Window
                , Page
                , CanvasManagerGlobals.SelectedLineFinishManager
                , graphicsDirectedLine
                , designState
                , true)
            {
                LineStartCursorPosition = StaticGlobals.GetCursorPosition()
            };


            //this.LineModeBuildingLine.Draw(BaseForm.selectedLineFinish.LineColor, BaseForm.selectedLineFinish.LineWidthInPts, BaseForm.selectedLineFinish.VisioDashType);

            this.LineModeBuildingLine.Draw(BaseForm.selectedLineFinish.LineColor, BaseForm.selectedLineFinish.LineWidthInPts, BaseForm.selectedLineFinish.VisioDashType);

            LineFinishManager lineFinishManager = CanvasManagerGlobals.SelectedLineFinishManager;
#if true
            this.LineModeBuildingLine.OriginatingDesignState = designState;

            lineFinishManager.AddLineFull(this.LineModeBuildingLine, false);

            // If created in area mode, it should be removed when returned to area mode.

#endif
            if (designState == DesignState.Area)
            {
                this.LineModeBuildingLine.RemoveOnReturnToAreaMode = true;
            }


            //this.ShapeDict.Add(this.LineModeBuildingLine.NameID, LineModeBuildingLine);

            this.CurrentPage.AddToDirectedLineDict(LineModeBuildingLine);

            LineHistoryList.Add(LineModeBuildingLine);

            SystemState.DrawingShape = false;

            CurrentPage.RemoveLineModeStartMarker();
           
            Line2XModeBaseCoord = Coordinate.NullCoordinate;

            MostRecentlyCompletedDoubleLine = LineModeBuildingLine;

            //LineModeBuildingLine = null;

            LineModeBuildingLine.SetShapeData("2X Line Mode Line", "Line[" + SelectedLineType.LineName + "]");

            Page.AddToPageShapeDict(LineModeBuildingLine);

            Window?.DeselectAll();

            return LineModeBuildingLine;
        }

        private void processLineLayoutModeTakeoutAreaClick(double x, double y)
        {
            GraphicsLayerBase layer = Page.TakeoutLayer;

            //Visio.Selection selection = CurrentPage.VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            List<string> guidList = VisioInterop.SpatialSearchGuidList(
                Window, Page, x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0);

            LineFinishManager lineFinishManager1 = null;

            if (guidList.Count > 0)
            {
                //bool takeoutAreaElementFound = false;

                foreach (string guid in guidList)
                {
                    //UCLineFinishPaletteElement linePaletteElement = null;

                    DoorTakeout takeout = null;

                    foreach (LineFinishManager lineFinishManager in LineFinishManagerList)
                    {
                        if (lineFinishManager.GraphicsTakeoutAreaDictContains(guid))
                        {

                            takeout = lineFinishManager.GraphicsTakeoutAreaDict[guid];

                            lineFinishManager1 = lineFinishManager;

                            break;
                        }
                    }

                    if (takeout is null)
                    {
                        continue;
                    }
                    //if (!Page.GraphicsTakeoutAreaDictContains(guid))
                    //{
                    //    continue;
                    //}

                   // takeoutAreaElementFound = true;

                   // GraphicsTakeout takeout = Page.GraphicsTakeoutAreaDict[guid];

                    string lineFinishBaseGuid = takeout.LineFinishBaseGuid;

                    //UCLineFinishPaletteElement linePaletteElement = linePalette[lineFinishBaseGuid];

                    //lineFinishManager.RemoveFromGraphicsTakeoutAreaDict(takeout);

                    lineFinishManager1.RemoveFromGraphicsTakeoutAreaDict(guid);

                    Page.RemoveFromPageShapeDict(guid);

                    layer.RemoveShapeFromLayer(takeout, 1);

                    takeout.Delete();

                    lineFinishManager1.UpdateFinishStats();

                    return;
                }


                //if ()
                //{

                //    return;
                //}
            }

            double takeoutAmount = 0;

            if (BaseForm.rbnDoorTakeoutOther.Checked)
            {
                string takeoutAmountStr = BaseForm.txbDoorTakeoutOther.Text.Trim();

                if (!Utilities.IsValidPosDbl(takeoutAmountStr))
                {
                    MessageBox.Show("The amount specified for the takeout amount is invalid");
                }

                takeoutAmount = double.Parse(takeoutAmountStr);
            }

            else if (BaseForm.rbnDoorTakeout3Ft.Checked)
            {
                takeoutAmount = 3.0;
            }

            else if (BaseForm.rbnDoorTakeout6Ft.Checked)
            {
                takeoutAmount = 6.0;
            }

            DoorTakeout graphicsTakeoutArea = new DoorTakeout(Window, Page, new Coordinate(x, y), 0.175, takeoutAmount, SelectedLineType.LineFinishBase.Guid);

            graphicsTakeoutArea.Draw(SelectedLineType.LineFinishBase.LineColor, 10, SelectedLineType.LineFinishBase.VisioLineType);

            graphicsTakeoutArea.Shape.SetShapeData("[TakeoutArea]", "Circle");

            Window?.DeselectAll();

            //lineFinishManager = SelectedLineType.LineFinishManager;

            SelectedLineFinishManager.AddToGraphicsTakeoutAreaDict(graphicsTakeoutArea);

            //Page.AddToGraphicsTakeoutAreaDict(graphicsTakeoutArea);
            Page.AddToPageShapeDict(graphicsTakeoutArea);

            layer.AddShapeToLayer(graphicsTakeoutArea.Shape, 1);


            SelectedLineFinishManager.UpdateFinishStats();
        }

        private void processLineLayoutModeDuplicateClick(double x, double y)
        {
            if (mostRecentlyCompletedDoubleLine is null)
            {
                return;
            }

            CanvasDirectedLine duplicatedLine = new CanvasDirectedLine(this, CanvasManagerGlobals.SelectedLineFinishManager, mostRecentlyCompletedDoubleLine, DesignState);

            LineHistoryList.Add(duplicatedLine);

            Coordinate coord2 = new Coordinate(x, y);
            Coordinate coord1 = coord2 - (duplicatedLine.Coord2 - duplicatedLine.Coord1);

            duplicatedLine.Coord1 = coord1;
            duplicatedLine.Coord2 = coord2;

            duplicatedLine.Draw(SelectedLineType.LineColor);

            duplicatedLine.SetShapeData("2X Line Mode Line", "Line[" + SelectedLineType.LineName + "]");

            this.SelectedLineFinishManager.AddLineFull((CanvasDirectedLine)duplicatedLine);

            this.CurrentPage.AddToDirectedLineDict(duplicatedLine);

            SystemState.DrawingMode = DrawingMode.Default;
            SystemState.DrawingShape = false;

            return;
        }

        //private void InitializeLineDraw(double x, double y, LineDrawoutMode lineDrawoutMode = LineDrawoutMode.ShowNormalDrawout)
        //{
        //    Coordinate coord = new Coordinate(x, y);

        //    DirectedLine directedLine = new DirectedLine(coord, coord);

        //    GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(this.Window, CurrentPage, directedLine, LineRole.SingleLine);

        //    this.LineModeBuildingLine = new CanvasDirectedLine(
        //        this,
        //        this.Window,
        //        this.CurrentPage,
        //        graphicsDirectedLine,
        //        DesignState.Line,
        //        BaseForm.LayoutLineMode == LineDrawingMode.Mode2X)
        //    {
        //        LineDrawoutMode = lineDrawoutMode,
        //        LineStartCursorPosition = StaticGlobals.GetCursorPosition()
        //    };

        //    if (GlobalSettings.ShowMarker)
        //    {
        //        CurrentPage.PlaceMarker(x, y, GlobalSettings.MarkerWidth);
        //    }

        //    if (GlobalSettings.ShowGuides)
        //    {
        //        CurrentPage.PlaceGuides(x, y);
        //    }

        //    this.LineModeBuildingLine.Draw(BaseForm.selectedLineFinish.LineColor, BaseForm.selectedLineFinish.LineWidthInPts);

        //    this.ShapeDict.Add(this.LineModeBuildingLine.NameID, LineModeBuildingLine);

        //    this.DrawingShape = true;

        //    VisioInterop.DeselectAll(Window);

        //    if (BaseForm.btnLayoutLine1XMode.BackColor == Color.Orange)
        //    {
        //        BaseForm.DrawingMode = DrawingMode.Line1X;
        //    }

        //    else if (BaseForm.btnLayoutLine2XMode.BackColor == Color.Orange)
        //    {
        //        BaseForm.DrawingMode = DrawingMode.Line2X;
        //    }

        //    else if (BaseForm.btnLayoutLineDuplicate.BackColor == Color.Orange)
        //    {
        //        BaseForm.DrawingMode = DrawingMode.LineDuplicate;
        //    }
        //}

        private void ProcessLineStateArrowKey(int keyVal)
        {
            if (BaseForm.SeamMode != SeamMode.Selection)
            {
                return;
            }

            switch (keyVal)
            {
                case 0: moveLineStateSelectedItemLeft(); break;
                case 1: moveLineStateSelectedItemUp(); break;
                case 2: moveLineStateSelectedItemRght(); break;
                case 3: moveLineStateSelectedItemDown(); break;

                default: break;
            }
        }

        private void moveLineStateSelectedItemDown()
        {
            if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, 0, -1);
            }
        }

        private void moveLineStateSelectedItemLeft()
        {
            if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, -1, 0);
            }
        }

        private void moveLineStateSelectedItemUp()
        {
            if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, 0, 1);
            }
        }

        private void moveLineStateSelectedItemRght()
        {
            if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, 1, 0);
            }
        }
        public void ResetLineDrawState()
        {
            SystemState.DrawingShape = false;
            SystemState.DrawingMode = DrawingMode.Default;

            LineModeBuildingLine = null;

            RemoveMarkerAndGuides();

            CurrentPage.RemoveLineModeStartMarker();

            this.Line1XModeBaseCoord = Coordinate.NullCoordinate;
            this.Line2XModeBaseCoord = Coordinate.NullCoordinate;

            LineLayoutState = LineLayoutState.Default;
        }
    }
}
