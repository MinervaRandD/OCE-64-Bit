
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
    using PaletteLib;
    using FinishesLib;
    using CanvasLib.DoorTakeouts;
    using FloorMaterialEstimator.Finish_Controls;

    using Visio = Microsoft.Office.Interop.Visio;

    using CanvasLib.Markers_and_Guides;
    using Globals;
    public partial class CanvasManager
    {
        public void RemoveLineModeBuildingLine()
        {
            RemoveMarkerAndGuides();

            if (LineLayoutState == LineLayoutState.New1XLineSequenceStarted)
            {
                CurrentPage.RemoveLineModeStartMarker();

                Line1XModeBaseCoord = Coordinate.NullCoordinate;
                Line2XModeBaseCoord = Coordinate.NullCoordinate;

                LineLayoutState = LineLayoutState.Default;

                SystemState.DrawingShape = false;

                return;
            }

            if (LineLayoutState == LineLayoutState.New2XLineStarted)
            {
                CurrentPage.RemoveLineModeStartMarker();


                Line1XModeBaseCoord = Coordinate.NullCoordinate;
                Line2XModeBaseCoord = Coordinate.NullCoordinate;

                LineLayoutState = LineLayoutState.Default;

                SystemState.DrawingShape = false;

                return;
            }

            if (LineHistoryList.Count <= 0)
            {

                Line1XModeBaseCoord = Coordinate.NullCoordinate;
                Line2XModeBaseCoord = Coordinate.NullCoordinate;

                LineLayoutState = LineLayoutState.Default;

                SystemState.DrawingShape = false;

                SystemState.DrawingMode = DrawingMode.Default;

                return;
            }

            CanvasDirectedLine lastLine = LineHistoryList.Last();

            if (LineLayoutState == LineLayoutState.Default)
            {
                if (lastLine.IsVerticalGuideLine || lastLine.IsHorizontalGuideLine)
                {
                    Line1XModeBaseCoord = Coordinate.NullCoordinate;

                    LineLayoutState = LineLayoutState.Default;

                    removeLastLine();

                    return;
                }

                if (lastLine.LineCompoundType == LineCompoundType.Single)
                {
                    CanvasDirectedLine firstSingleLineInSequence = getFirstSingleLineInSequence();

                    if (firstSingleLineInSequence == lastLine)
                    {
                        CurrentPage.PlaceLineModeStartMarker(Line1XModeBaseCoord, 0.075, SelectedLineType.LineColor, StartMarkerType.Single);

                        LineLayoutState = LineLayoutState.New1XLineSequenceStarted;

                    }

                    else
                    {
                        CurrentPage.PlaceLineModeStartMarker(firstSingleLineInSequence.Coord1, 0.075, SelectedLineType.LineColor, StartMarkerType.Single);

                        LineLayoutState = LineLayoutState.Ongoing1XLineSequence;

                    }

                    removeLastLine();


                    Line1XModeBaseCoord = lastLine.Coord1;

                    SetupMarkerAndGuides(Line1XModeBaseCoord);

                    SystemState.DrawingShape = true;

                    return;
                }

                if (lastLine.LineCompoundType == LineCompoundType.Double)
                {
                    CurrentPage.PlaceLineModeStartMarker(lastLine.Coord1, 0.075, SelectedLineType.LineColor, StartMarkerType.Double);

                    Line2XModeBaseCoord = lastLine.Coord1;

                    LineLayoutState = LineLayoutState.New2XLineStarted;

                    removeLastLine();

                    SystemState.DrawingShape = true;

                    return;
                }

            }

            if (LineLayoutState == LineLayoutState.Ongoing1XLineSequence)
            {

                if (lastLine.FirstLineInSequence)
                {
                    Line1XModeBaseCoord = lastLine.Coord1;

                    LineLayoutState = LineLayoutState.New1XLineSequenceStarted;
                }

                else
                {
                    Line1XModeBaseCoord = LineHistoryList.Last().Coord1;

                    if (GlobalSettings.ShowGuides)
                    {
                        CurrentPage.PlaceGuides(Line1XModeBaseCoord);
                    }

                }

                removeLastLine();

                SetupMarkerAndGuides(Line1XModeBaseCoord);

                SystemState.DrawingShape = true;

                return;
            }
        }

        private CanvasDirectedLine getFirstSingleLineInSequence()
        {
            for (int i = LineHistoryList.Count - 1; i >= 0; i--)
            {
                CanvasDirectedLine currLine = LineHistoryList[i];

                if (currLine.LineCompoundType == LineCompoundType.Single)
                {
                    if (currLine.FirstLineInSequence)
                    {
                        return currLine;
                    }

                    if (i == 0)
                    {
                        // This should never happen, it means that the first line in sequence flag was not set up
                        // correctly. This is purely defensive

                        currLine.FirstLineInSequence = true;

                        return currLine;
                    }

                    if (LineHistoryList[i-1].LineCompoundType != LineCompoundType.Single)
                    {
                        // This should never happen, it means that the first line in sequence flag was not set up
                        // correctly. This is purely defensive

                        currLine.FirstLineInSequence = true;

                        return currLine;
                    }
                }
            }

            return null; // Should never happen.
        }

        private void removeLastLine()
        {
            if (LineHistoryList.Count <= 0)
            {
                ResetLineDrawState();
                
                return;
            }

            CanvasDirectedLine lastLine = LineHistoryList[LineHistoryList.Count - 1];

            if (CurrentPage.DirectedLineDictContains(lastLine))
            {
                CurrentPage.RemoveFromDirectedLineDict(lastLine);
            }

            lastLine.LineFinishManager.RemoveLineFull(lastLine);

            LineHistoryList.RemoveAt(LineHistoryList.Count - 1);

            lastLine.Delete();
        }

        //private void setupLineHistoryMarkers()
        //{

        //    RemoveMarkerAndGuides();

        //    if (LineHistoryList.Count <= 0)
        //    {
        //        SystemState.DrawingMode = DrawingMode.Default;

        //        return;
        //    }

        //    CanvasDirectedLine lastLine = LineHistoryList.Last();

        //    if (GlobalSettings.ShowGuides)
        //    {
        //        CurrentPage.PlaceGuides(lastLine.Coord2);
        //    }
            
        //    if (GlobalSettings.ShowMarker)
        //    {
        //        CurrentPage.PlaceMarker(lastLine.Coord2, GlobalSettings.MarkerWidth);
        //    }

        //    Window?.DeselectAll();

        //}

        private void processLineEditModeClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            cancelDefault = true;

            //if (BaseForm.ckbEditLineHighlightAndApply.Checked)
            //{
            //    processLineEditModeHighlightAndApply(x, y);
            //}

            //else
            //{
            if (button == 1)
            {
                processLineEditModeImmediate(x, y);
            }
            //}

        }

        private void processLineEditModeImmediate(double x, double y)
        {
            CanvasDirectedLine selectedCanvasDirectedLine = getSelectedLine(x, y);

            if (selectedCanvasDirectedLine is null)
            {
                return;
            }

            MovedLineShapes.Clear();
            DeletedLineShapes.Clear();
            ChangedLineTypeShapes.Clear();

            processLineEditModeAction(selectedCanvasDirectedLine);

            Window?.DeselectAll();

            //BaseForm.btnEditLineUndo.Enabled = true;
        }

        private void processLineEditModeAction(CanvasDirectedLine canvasDirectedLine)
        {
           
            switch (BaseForm.EditLineMode)
            {
                case EditLineMode.ChangeLinesToSelected:

                    ProcessEditLineModeActionChangeLineToFinish(canvasDirectedLine, BaseForm.selectedLineFinish);


                    SelectedLineFinishManager.LineDesignStateLayer.SetLayerVisibility(true);


                    return;

                case EditLineMode.DeleteLines:

                    ProcessEditLineModeActionDeleteLine(canvasDirectedLine);

                    return;

                case EditLineMode.SetLinesTo1X:

                    if (canvasDirectedLine.LineCompoundType == LineCompoundType.Single)
                    {
                        return;
                    }

                    ChangedLineTypeShapes.Add(new Tuple<int, CanvasDirectedLine>(2, canvasDirectedLine));

                    canvasDirectedLine.LineCompoundType = LineCompoundType.Single;

                    canvasDirectedLine.SetLineGraphics(DesignState.Line, false, AreaShapeBuildStatus.Completed);

                    return;

                case EditLineMode.SetLinesTo2X:

                    if (canvasDirectedLine.LineCompoundType == LineCompoundType.Double)
                    {
                        return;
                    }

                    ChangedLineTypeShapes.Add(new Tuple<int, CanvasDirectedLine>(1, canvasDirectedLine));

                    canvasDirectedLine.LineCompoundType = LineCompoundType.Double;

                    canvasDirectedLine.SetLineGraphics(DesignState.Line, false, AreaShapeBuildStatus.Completed);

                    return;
            }
        }

        public void ProcessEditLineModeActionChangeLineToFinish(CanvasDirectedLine canvasDirectedLine, UCLineFinishPaletteElement ucFinishElem)
        {

            MovedLineShapes.Add(new Tuple<LineFinishManager, CanvasDirectedLine>(canvasDirectedLine.LineFinishManager, canvasDirectedLine));

            BaseForm.MoveLineToSelectedLineType(canvasDirectedLine);

        }

        public void ProcessEditLineModeActionDeleteLine(CanvasDirectedLine canvasDirectedLine)
        {
            if (!canvasDirectedLine.IsDeleteable)
            {
                ManagedMessageBox.Show("The currently selected line is a boundary of finish area and cannot be independently deleted");
                return;
            }

            DeletedLineShapes.Add(canvasDirectedLine);


            bool processJump = !isLastLineOnBuildingPolyline(canvasDirectedLine);

            if (canvasDirectedLine.IsPerimeterRelatedLine)
            {
                canvasDirectedLine.SetZeroLine(true);
                canvasDirectedLine.SetSelectionMode(false);
            }
            else
            {
                BaseForm.DeleteLineShape(canvasDirectedLine);

                if (processJump)
                {
                    BaseForm.BtnLayoutLineJump_Click(null, null);
                }
            }

            this.LineHistoryList.Clear();

            BaseForm.BtnLayoutLine1XMode_Click(null, null); // Return to 1X mode after deleting line.
        }


        private bool isLastLineOnBuildingPolyline(CanvasDirectedLine canvasDirectedLine)
        {
          
            if (this.LineHistoryList.Count <= 1)
            {
                return false;
            }

            return (this.LineHistoryList.Last() == canvasDirectedLine);
        }

        //private void processLineEditModeHighlightAndApply(double x, double y)
        //{
        //    CanvasDirectedLine selectedCanvasDirectedLine = getSelectedLine(x, y);

        //    if (selectedCanvasDirectedLine is null)
        //    {
        //        return;
        //    }

        //    CurrentPage.UpdateLineDesignStateLineSelectionStatus(selectedCanvasDirectedLine);
        //}

        private CanvasDirectedLine getSelectedLine(double x, double y)
        {
            Visio.Selection selection = CurrentPage.VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialTouching, 0.1, 0];

            CanvasDirectedLine line = null;

            Coordinate coord = new Coordinate(x, y);

            CanvasDirectedLine minDistLine = null;

            double minDist = double.MaxValue;

            for (int i = 1; i <= selection.Count; i++)
            {
                Visio.Shape shape = selection[i];

                string guid = shape.Data3;

                if (!CurrentPage.DirectedLineDictContains(guid))
                {
                    // Background drawing getting selected also. These are not in the shape dict so they can be ignored.

                    continue;
                }

                line = (CanvasDirectedLine)CurrentPage.GetDirectedLine(guid);

                if (line.LineFinishManager.Filtered)
                {
                    continue;
                }

                if (line.LineRole != LineRole.SingleLine && line.LineRole != LineRole.AssociatedLine)
                {
                    continue;
                }

                if (!lineIsVisible(line))
                {
                    continue;
                }

                double dist = GeometryUtils.DistancePointToLineSegment(coord, line);

                if (dist < minDist)
                {
                    minDist = dist;

                    minDistLine = line;
                }
              
            }

            return minDistLine;
        }

        private bool lineIsVisible(CanvasDirectedLine line)
        {
            if (line == null)
            {
                return false;
            }

            GraphicShape graphicShape = line.Shape;

            if (graphicShape == null)
            {
                return false;
            }

            HashSet<GraphicsLayerBase> layerSet = graphicShape.LayerSet;

            if (layerSet is null)
            {
                return false;
            }

            if (layerSet.Count == 0)
            {
                return false;
            }

            return layerSet.Any(g => g.Visibility);
        }

        public void ProcessEditLineUndo()
        {
            CurrentPage.SelectedLineDict.Clear();

            foreach (CanvasDirectedLine line in DeletedLineShapes)
            {
                line.Draw(DesignState.Line, false, AreaShapeBuildStatus.Completed);

                Add(line);

                //if (BaseForm.ckbEditLineHighlightAndApply.Checked)
                //{
                //    CurrentPage.UpdateLineDesignStateLineSelectionStatus(line);
                //}
            }

            foreach (Tuple<LineFinishManager, CanvasDirectedLine> line in MovedLineShapes)
            {
                LineFinishManagerList.MoveLineToLineType(line.Item2, line.Item1);

                //if (BaseForm.ckbEditLineHighlightAndApply.Checked)
                //{
                //    CurrentPage.UpdateLineDesignStateLineSelectionStatus(line.Item2);
                //}
            }

            foreach (Tuple<int, CanvasDirectedLine> line in ChangedLineTypeShapes)
            {
                if (line.Item1 == 1)
                {
                    line.Item2.LineCompoundType = LineCompoundType.Single;
                }

                else if (line.Item1 == 2)
                {
                    line.Item2.LineCompoundType = LineCompoundType.Double;
                }

                line.Item2.SetLineGraphics(DesignState.Line, false, AreaShapeBuildStatus.Completed);

                //if (BaseForm.ckbEditLineHighlightAndApply.Checked)
                //{
                //    CurrentPage.UpdateLineDesignStateLineSelectionStatus(line.Item2);
                //}
            }

            DeletedLineShapes.Clear();
            MovedLineShapes.Clear();
            ChangedLineTypeShapes.Clear();

            Window?.DeselectAll();

            //BaseForm.btnEditLineUndo.Enabled = false;
        }

        //public void ProcessEditLineClear()
        //{
        //    foreach (CanvasDirectedLine line in CurrentPage.SelectedLineDict.Values)
        //    {
        //        line.SetLineGraphics(DesignState.Line, false, AreaShapeBuildStatus.Completed);
        //    }

        //    CurrentPage.SelectedLineDict.Clear();

        //    //BaseForm.btnEditLineUndo.Enabled = false;
        //}


        internal void DeleteSelectedLines(bool deleteSingleLines, bool deleteDoubleLines, bool deleteAreaModeLines, bool applyToSelectedLinesOnly)
        {
            LineFinishBase lineFinishBase = null;
            
            if (applyToSelectedLinesOnly)
            {
                lineFinishBase = this.SelectedLineType.LineFinishBase;
            }
          
            List<CanvasDirectedLine> linesToDelete1X = new List<CanvasDirectedLine>();
            List<CanvasDirectedLine> linesToDelete2X = new List<CanvasDirectedLine>();
            List<CanvasDirectedLine> linesToDeleteAssociated = new List<CanvasDirectedLine>();

            foreach (CanvasDirectedLine directedLine in CurrentPage.DirectedLines)
            {
                if (directedLine.Shape.Data1 == "Associated Line")
                {
                    if (!deleteAreaModeLines)
                    {
                        continue;
                    }

                    if (directedLine.IsZeroLine || !directedLine.IsDeleteable)
                    {
                        continue;
                    }

                    if (applyToSelectedLinesOnly)
                    {
                        if (lineFinishBase != directedLine.LineFinishManager.LineFinishBase)
                        {
                            continue;
                        }
                    }

                    linesToDeleteAssociated.Add(directedLine);

                    continue;
                }

                if (directedLine.ParentLayoutArea != null)
                {
                    continue;
                }

                if (directedLine.LineCompoundType == LineCompoundType.Single && deleteSingleLines)
                {

                    if (applyToSelectedLinesOnly)
                    {
                        if (lineFinishBase != directedLine.LineFinishManager.LineFinishBase)
                        {
                            continue;
                        }
                    }

                    linesToDelete1X.Add(directedLine);

                    continue;
                }

                if (directedLine.LineCompoundType == LineCompoundType.Double && deleteDoubleLines)
                {

                    if (applyToSelectedLinesOnly)
                    {
                        if (lineFinishBase != directedLine.LineFinishManager.LineFinishBase)
                        {
                            continue;
                        }
                    }

                    linesToDelete2X.Add(directedLine);

                    continue;
                }


            }

            if (deleteSingleLines)
            {
                foreach (CanvasDirectedLine directedLine1X in linesToDelete1X)
                {
                    CurrentPage.RemoveFromDirectedLineDict(directedLine1X);

                    if (directedLine1X.ucLine != null)
                    {
                        directedLine1X.LineFinishManager.RemoveLineFull(directedLine1X);
                    }

                    else
                    {
                        directedLine1X.Delete();
                    }
                }
            }

            if (deleteDoubleLines)
            {
                foreach (CanvasDirectedLine directedLine2X in linesToDelete2X)
                {
                    CurrentPage.RemoveFromDirectedLineDict(directedLine2X);

                    if (directedLine2X.ucLine != null)
                    {
                        directedLine2X.LineFinishManager.RemoveLineFull(directedLine2X);
                    }

                    else
                    {
                        directedLine2X.Delete();
                    }
                }
            }

            if (deleteAreaModeLines)
            {
                foreach (CanvasDirectedLine directedLineAssociated in linesToDeleteAssociated)
                {
                    directedLineAssociated.SetZeroLine(true);
                    directedLineAssociated.SetSelectionMode(false);
                }
            }

            // Remove door takeouts...

            List<DoorTakeout> doorTakeoutsToDelete = new List<DoorTakeout>();

            foreach (LineFinishManager lineFinishManager in LineFinishManagerList)
            {

                if (applyToSelectedLinesOnly)
                {
                    if (lineFinishBase != lineFinishManager.LineFinishBase)
                    {
                        continue;
                    }
                }

                doorTakeoutsToDelete.Clear();

                foreach (var doorTakeout in lineFinishManager.DoorTakeoutList)
                {

                    if (deleteSingleLines && deleteDoubleLines && deleteAreaModeLines)
                    {
                        doorTakeoutsToDelete.Add(doorTakeout);

                        continue;
                    }

                    if (doorTakeout.TakeoutAmount == 3.0 && deleteSingleLines)
                    {
                        doorTakeoutsToDelete.Add(doorTakeout);

                        continue;
                    }

                    if (doorTakeout.TakeoutAmount == 6.0 && deleteDoubleLines)
                    {
                        doorTakeoutsToDelete.Add(doorTakeout);

                        continue;
                    }

                }

                foreach (var doorTakeout in doorTakeoutsToDelete)
                {
                    string guid = doorTakeout.Guid;

                    lineFinishManager.RemoveFromGraphicsTakeoutAreaDict(guid);

                    Page.RemoveFromPageShapeDict(guid);

                    //layer.RemoveShapeFromLayer(doorTakeout, 1);

                    doorTakeout.Delete();

                }

                if (doorTakeoutsToDelete.Count > 0)
                {
                    lineFinishManager.UpdateFinishStats();
                }

            }


            foreach (LineFinishManager lineFinishManager in LineFinishManagerList)
            {
                lineFinishManager.SetLineState(DesignState.Line, SeamMode.Selection, false);
            }


        }
    }
}
