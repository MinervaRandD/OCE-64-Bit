//-------------------------------------------------------------------------------//
// <copyright file=PolylineManger.cs"                                            //
//            company="Bruun Estimating, LLC">                                   // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

/// <summary>
/// This file contains routines for managing the build of a polyline. Eventually
/// this should be made into its own class rather than an exension of the canvas
/// manager
/// </summary>
namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using FloorMaterialEstimator;

    using Graphics;
    using Geometry;
    using Utilities;
    using Globals;
    using FloorMaterialEstimator.Finish_Controls;
    using System.Collections.Generic;
    using FinishesLib;
    using PaletteLib;
    using System.Windows.Forms;
    using FloorMaterialEstimator.Dialog_Boxes;
    using TracerLib;
    using MaterialsLayout;

    public partial class CanvasManager
    {
        CanvasDirectedPolyline buildingPolyline = null;

        /// <summary>
        /// The polyline that is currently being built. It could be built in any over various design states.
        /// </summary>
        public CanvasDirectedPolyline BuildingPolyline
        {
            get
            {
                return this.buildingPolyline;
            }

            set
            {
                this.buildingPolyline = value;
            }
        }

        /// <summary>
        /// Initializes the building polyline and sets the original point
        /// </summary>
        /// <param name="x">The x value of the original coordinate</param>
        /// <param name="y">The y value of the original coordinate</param>
        /// <param name="fromGuides">Whether or not the polyline is being initialized from field guides.</param>
        private void InitializePolylineDraw(double x, double y, bool fromGuides = false)
        {
            if (BuildingPolyline != null)
            {
                // Defensive. Should never initialize polyline when building polyline exists.

                throw new NotImplementedException();
            }

            SystemState.DrawingShape = true;

            SystemState.DrawingMode = DrawingMode.Polyline;

            BuildingPolyline = new CanvasDirectedPolyline(this);
          
            BuildingPolyline.BuildStatus = AreaShapeBuildStatus.Building;

            if (fromGuides)
            {
                BuildingPolyline.AddInitialLineFromGuides(x, y);
            }

            else
            {
                BuildingPolyline.AddPoint(x, y);
            }
        }

        /// <summary>
        /// Continue the build of the current polyline by adding an additional point.
        /// </summary>
        /// <param name="x">The x coordinate of the additional point</param>
        /// <param name="y">The y coordinate of the additional point</param>
        /// <param name="fromGuides">Whether or not the polyline is being initialized from field guides.</param>
        public void ContinuePolylineDraw(double x, double y, bool fromGuides = false)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { x, y, fromGuides });
#endif

            try
            {
                if (BuildingPolyline == null)
                {
                    Tracer.TraceGen.TraceError("Error in PolylineManager:ContinuePolylineDraw: BuildingPolyine is null", 1, true);

                    return;
                }

                if (fromGuides)
                {
                    BuildingPolyline.AddExtendingLineFromGuides(x, y);
                }

                else
                {
                    
                    if (BuildingPolyline.Count >= 2)
                    {
                        Coordinate frstCoord = BuildingPolyline.GetFrstCoordinate();

                        if (MathUtils.H2Distance(frstCoord.X, frstCoord.Y, x, y) <= 1.0e-1)
                        {
                            // If the current point is within 1/10 inch of the first point, then assume the
                            // user is trying to close the polyline.
                            
                            BuildingPolyline.AddPoint(x, y);

                            CompletePolylineDraw(PalettesGlobal.AreaFinishPalette.SelectedItemIndex);

                            return;
                        }    
                    }

                    if (!BuildingPolyline.ValidateNoIntersection(x, y))
                    {
                        MessageBox.Show("Additional lines cannot intersect the current drawing");
                        {
                            return;
                        }
                    }

                    BuildingPolyline.AddPoint(x, y);
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("PolylineManager:ContinuePolylineDraw threw an exception", ex, 1, true);
            }

        }
  
        /// <summary>
        /// Completes the drawing of the polyline
        /// </summary>
        /// <param name="finishIndex"></param>
        public void CompletePolylineDraw(int finishIndex = 0)
        {
            ConditionBuildingPolyline();

            if (DesignState == DesignState.Seam)
            { 
                this.completeSeamDesignStatePolylineDraw(BuildingPolyline);

                return;
            }

            if (DesignState == DesignState.Area)
            {
                this.completeAreaDesignStatePolylineDraw(BuildingPolyline, finishIndex);

                return;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Normalizes the polyline to generate a consistent polygon shape.
        /// 
        /// At this point we expect a closed curve, but due to numerical precision, the values
        /// may not be exact for each line segment. This is corrected here.
        /// </summary>
        private void ConditionBuildingPolyline()
        {
            
            int count = buildingPolyline.Count;
            
            // The first coordinate must be equal to the last coordinate. The routine
            // may be entered with slight differences, but then the values are reset here.

            if (buildingPolyline.CoordList[0] != buildingPolyline.CoordList[count])
            {
                buildingPolyline.CoordList[count] = buildingPolyline.CoordList[0];
            }

            for (int i = 0; i < buildingPolyline.Count; i++)
            {
                CanvasDirectedLine canvasDirectedLine = buildingPolyline[i];

                Coordinate coord1 = buildingPolyline.CoordList[i];
                Coordinate coord2 = buildingPolyline.CoordList[i + 1];

                if (canvasDirectedLine.Coord1 != coord1)
                {
                    canvasDirectedLine.Coord1 = coord1;

                    VisioInterop.SetLineStartpoint(canvasDirectedLine.Shape, coord1);
                }

                if (canvasDirectedLine.Coord2 != coord2)
                {
                    canvasDirectedLine.Coord2 = coord2;

                    VisioInterop.SetLineEndpoint(canvasDirectedLine.Shape, coord2);
                }
            }
        }

        public void ProcessFixedWidthCompleteShape()
        {
            if (!BorderManager.IsLeftSelected && !BorderManager.IsRghtSelected)
            {
                MessageBox.Show("A border position must be selected to complete a shape.");
                
                return;
            }

            List<Tuple<AreaFinishBase, List<Coordinate>>> directedPolylineElementList = BorderManager.GetDirectedPolylineElementList();

            BorderManager.Reset();

            foreach (Tuple<AreaFinishBase, List<Coordinate>> directedPolylineElement in directedPolylineElementList)
            {
                AreaFinishBase areaFinishBase = directedPolylineElement.Item1;

                CanvasDirectedPolyline canvasDirectedPolyline = new CanvasDirectedPolyline(this);

                foreach (Coordinate coord in directedPolylineElement.Item2)
                {

                    canvasDirectedPolyline.AddPoint(coord, true);
                }

                int areaFinishBaseIndex = AreaFinishPalette.AreaFinishBaseList.GetIndex(areaFinishBase);

                UCAreaFinishPaletteElement ucAreaFinish = PalettesGlobal.AreaFinishPalette[areaFinishBaseIndex];

                AreaFinishManager areaFinishManager = CanvasManagerGlobals.AreaFinishManagerList[areaFinishBaseIndex];

                AddAreaPolylineToCanvas(canvasDirectedPolyline, areaFinishManager, ucAreaFinish, null, null, false);
            }

           BaseForm.OversUndersFormUpdate();

            SystemState.DrawingShape = false;

            return;
        }

        /// <summary>
        /// Complete the currently building shape. Currently only applies to polyline.
        /// </summary>
        public void ProcessPolylineCompleteShape(bool fromButtonClick = false)
        {
            if (SystemState.DrawingMode == DrawingMode.Default)
            {
                return;
            }

            if (!SystemState.DrawingShape)
            {
                return;
            }

            if (SystemState.DrawingMode != DrawingMode.Polyline)
            {
                return;
            }

            if (buildingPolyline == null)
            {
                return;
            }

            if (buildingPolyline.CoordList.Count < 3)
            {
                MessageBox.Show("At least 3 points must be selected to complete the current drawing.");

                return;
            }

            Coordinate coord = BuildingPolyline.GetLastCoordinate() - BuildingPolyline.GetFrstCoordinate();

            Coordinate coord1 = BuildingPolyline.GetFrstCoordinate();
            Coordinate coord2 = BuildingPolyline.GetLastCoordinate();

            if (MathUtils.H2Distance(coord1.X, coord1.Y, coord2.X, coord2.Y) >= 1e-1)
            {
                BuildingPolyline.AddPoint(BuildingPolyline.GetFrstCoordinate());
            }

            CompletePolylineDraw();

            buildingPolyline = null;
            SystemState.DrawingShape = false;
        }

        public enum LineCompletionOption
        {
            Ask,
            PreferMax,
            PreferMin
        }

        public void ProcessPolylineCompleteShapeByIntersectionV2(LineCompletionOption option = LineCompletionOption.Ask)
        {
            if (BuildingPolyline == null)
            {
                return;
            }

            double orientation = BuildingPolyline.Orientation();

            //orientation = 1;

            Coordinate frstCoord;
            Coordinate lastCoord;

            DirectedLine frstLine;
            DirectedLine lastLine;

            // Handle the case where the last line endpoint is either horizontal to or vertical to the first line start point.
            // In this case, the user should have clicked 'complete' rather than complete-L.

            if (orientation >= 0)
            {
                frstCoord = BuildingPolyline.GetFrstCoordinate();
                lastCoord = BuildingPolyline.GetLastCoordinate();

                frstLine = BuildingPolyline.FrstLine;
                lastLine = BuildingPolyline.LastLine;
            }

            else
            {
                frstCoord = BuildingPolyline.GetLastCoordinate();
                lastCoord = BuildingPolyline.GetFrstCoordinate();

                frstLine = BuildingPolyline.LastLine;
                lastLine = BuildingPolyline.FrstLine;
            }

            if (Math.Abs(frstCoord.X - lastCoord.X) <= 1e-2 || Math.Abs(frstCoord.Y- lastCoord.Y) <= 1e-2)
            {

                // Polyline is in effect already completed.

                ProcessPolylineCompleteShape();

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

            if (DesignState == DesignState.Seam)
            {
                this.completeSeamDesignStatePolylineDraw(BuildingPolyline);

                return;
            }

            if (BuildingPolyline.CoordList.Count <= 2)
            {
                MessageBox.Show("At least 3 points must be selected to complete the current drawing.");

                return;
            }

            Coordinate baseCoord = frstCoord;

            Coordinate coord1 = new Coordinate(frstCoord.X, lastCoord.Y);
            Coordinate coord2 = new Coordinate(lastCoord.X, frstCoord.Y);

            DirectedLine cmprLine1 = new DirectedLine(baseCoord, coord1);
            DirectedLine cmprLine2 = new DirectedLine(baseCoord, coord2);

            List<Coordinate> polyCoordList1 = new List<Coordinate>(BuildingPolyline.CoordList);
            polyCoordList1.Add(coord1);

            List<Coordinate> polyCoordList2 = new List<Coordinate>(BuildingPolyline.CoordList);
            polyCoordList2.Add(coord2);

            DirectedPolygon polySolution1 = new DirectedPolygon(polyCoordList1);
            DirectedPolygon polySolution2 = new DirectedPolygon(polyCoordList2);

            bool solution1IsValid = polySolution1.ValidateCompleteL();
            bool solution2IsValid = polySolution2.ValidateCompleteL();

            if (!solution1IsValid && !solution2IsValid)
            {
                MessageBox.Show("Cannot complete-L on the current building area.");
                return;
            }

            Coordinate coord = Coordinate.NullCoordinate;

            if (solution1IsValid && !solution2IsValid)
            {
                coord = coord1;
            }

            else if (solution2IsValid && !solution1IsValid)
            {
                coord = coord2;
            }

            else
            {
                bool selectMax = true;

                switch (option)
                {
                    case LineCompletionOption.Ask:
                        {
                            ChooseCompleteLSolution chooseSolutionForm = new ChooseCompleteLSolution();
                            DialogResult result = chooseSolutionForm.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                selectMax = chooseSolutionForm.SelectMaximumArea;
                            }
                            else
                            {
                                return;
                            }
                        }
                        break;

                    case LineCompletionOption.PreferMax:
                        selectMax = true;
                        break;

                    case LineCompletionOption.PreferMin:
                        selectMax = false;
                        break;
                }

                double solution1Area = polySolution1.AreaInSqrInches(1);
                double solution2Area = polySolution2.AreaInSqrInches(1);

                if (selectMax)
                {
                    coord = solution1Area >= solution2Area ? coord1 : coord2;
                }

                else
                {
                    coord = solution1Area <= solution2Area ? coord1 : coord2;
                }

            }

            if (Coordinate.IsNullCoordinate(coord))
            {
                MessageBox.Show("Cannot complete this figure.");
                return;
            }

            CanvasDirectedLine lastComleteLLine = null;

            BuildingPolyline.AddPoint(coord);

            // Set flag indicating that this line was added as a result of a complete L. This is
            // so the system will know to back out this line on 'delete last line';

            if (Utilities.IsNotNull(lastComleteLLine = BuildingPolyline.LastLine)) // defensive
            {
                lastComleteLLine.IsCompleteLLine = true;
            }

            BuildingPolyline.AddPoint(BuildingPolyline.GetFrstCoordinate());

            // Set flag indicating that this line was added as a result of a complete L. This is
            // so the system will know to back out this line on 'delete last line';

            if (Utilities.IsNotNull(lastComleteLLine = BuildingPolyline.LastLine)) // defensive
            {
                lastComleteLLine.IsCompleteLLine = true;
            }
            
            CompletePolylineDraw();

            buildingPolyline = null;
            SystemState.DrawingShape = false;
        }

        private DirectedPolygon generatePolySolution(Coordinate coord)
        {
            List<Coordinate> coordList = new List<Coordinate>(this.coordinateList);

            coordList.Add(coord);

            return new DirectedPolygon(coordList);
        }

        private bool validateCompleteLSolution(
            Coordinate coord
            , DirectedLine cmprLine
            , DirectedLine frstLine
            , DirectedLine lastLine)
        {
            if (frstLine.Contains(coord))
            {
                return false;
            }

            if (lastLine.Contains(coord))
            {
                return false;
            }

            return true;
        }

        public void RemoveBuildingPolyLineBuildingLine()
        {
            if (SystemState.DrawingMode != DrawingMode.Polyline || !SystemState.DrawingShape || buildingPolyline is null)
            {
                //if (this.AreaHistoryList.Count <= 0)
                //{
                //    return;
                //}

                if (this.LastLayoutArea is null)
                {
                    return;
                }

                //int histIndx = AreaHistoryList.Count - 1;

                CanvasLayoutArea layoutArea = this.LastLayoutArea;

                this.LastLayoutArea = null;


                if (layoutArea.InternalAreas.Count > 0)
                {
                    CanvasDirectedPolygon internalAreaPolygon = layoutArea.InternalAreas[layoutArea.InternalAreas.Count - 1];
                    
                    this.buildingPolyline = new CanvasDirectedPolyline(this);
                    
                    foreach (Coordinate coord in internalAreaPolygon.CoordinateList())
                    {
                        this.buildingPolyline.AddPoint(coord);
                    }

                    this.buildingPolyline.AddPoint(this.buildingPolyline.CoordList[0]);

                    SystemState.DrawingShape = true;

                    SystemState.DrawingMode = DrawingMode.Polyline;

                    CanvasDirectedLine frstLine = this.buildingPolyline.FrstLine;

                    Coordinate frstCoord = frstLine.Coord1;

                    Shape internalShape = CurrentPage.DrawPolygon(internalAreaPolygon.CoordinateArray());

                    layoutArea.AddBackInternalAreas(new List<Shape> { internalShape });
                   
                    BuildingPolyline.SetupStartMarker();

                    layoutArea.InternalAreas.Remove(internalAreaPolygon);
                }

                else
                {
                    //AreaHistoryList.RemoveAt(histIndx);

                    this.buildingPolyline = new CanvasDirectedPolyline(this);

                    CanvasDirectedLine clonedLine = null;

                    foreach (CanvasDirectedLine line in layoutArea.ExternalArea)
                    {
                        clonedLine = line.Clone();

                        clonedLine.ucLine = line.ucLine;

                        clonedLine.AssociatedDirectedLine = line.AssociatedDirectedLine;

                        clonedLine.Draw();

                        line.LineFinishManager.AddLineFull(clonedLine);

                        this.buildingPolyline.Add(clonedLine);

                        this.buildingPolyline.CoordList.Add(clonedLine.Coord1);
                    }

                    if (!(clonedLine is null))
                    {
                        this.buildingPolyline.CoordList.Add(clonedLine.Coord2);
                        this.buildingPolyline.SetupMarkerAndGuides(clonedLine.Coord2);
                    }

                    SystemState.DrawingShape = true;

                    layoutArea.AreaFinishManager.RemoveLayoutArea(layoutArea);

                    layoutArea.RemoveFromCanvas();

                    layoutArea.Delete();

                    SystemState.DrawingMode = DrawingMode.Polyline;

                    CanvasDirectedLine frstLine = this.buildingPolyline.FrstLine;

                    Coordinate frstCoord = frstLine.Coord1;

                    BuildingPolyline.SetupStartMarker();


                    CurrentPage.RemoveLayoutArea(layoutArea);
                }
            }

            if (buildingPolyline.CoordList.Count <= 0)
            {
                DeleteBuildingPolyLine();

                return;
            }

            if (buildingPolyline.CoordList.Count == 1)
            {
                DeleteBuildingPolyLine();
                return;
            }

            CanvasDirectedLine lastLine = buildingPolyline.LastLine;

            if (lastLine is null)
            {
                DeleteBuildingPolyLine();
                return;
            }

            if (lastLine.Length <= clickResolution)
            {
                DeleteBuildingPolylineLastLine();

                if (BuildingPolyline.Count <= 0)
                {
                    DeleteBuildingPolyLine();

                    return;
                }

                RemoveBuildingPolyLineBuildingLine();

                return;
            }

            DeleteBuildingPolylineLastLine();

            if (buildingPolyline.CoordList.Count <= 0)
            {
                DeleteBuildingPolyLine();

                return;
            }

            return;
        }

        public void DeleteBuildingPolyLine()
        {
            if (BuildingPolyline is null)
            {
                return;
            }

            foreach (CanvasDirectedLine directedLine in buildingPolyline)
            {
                if (CurrentPage.DirectedLineDictContains(directedLine))
                {
                    CurrentPage.RemoveFromDirectedLineDict(directedLine);
                }
            }

            BuildingPolyline.Delete();

            RemoveMarkerAndGuides();
            
            buildingPolyline = null;

            SystemState.DrawingShape = false;

            SystemState.DrawingMode = DrawingMode.Default;

            return;
        }

        private void DeleteBuildingPolylineLastLine()
        {
            CanvasDirectedLine lastLine = buildingPolyline.RemoveLastLine();

            if (lastLine is null)
            {
                return;
            }

            Coordinate lastCoord = buildingPolyline.GetLastCoordinate();

            CurrentPage.LastPointDrawn = lastCoord;

            SetDrawoutLength(MouseX, MouseY);

            //if (ShapeDict.ContainsKey(lastLine.NameID))
            //{
            //    ShapeDict.Remove(lastLine.NameID);
            //}

            if (CurrentPage.DirectedLineDictContains(lastLine))
            {
                CurrentPage.RemoveFromDirectedLineDict(lastLine);
            }

            LineFinishManager lineFinishManager = lastLine.LineFinishManager;

            lineFinishManager.RemoveLineFull(lastLine);

          //  StaticGlobals.SetCursorPosition(lastLine.LineStartCursorPosition);

            if (lastLine.LineRole == LineRole.ExternalPerimeter)
            {
                // This indicates that the line being deleted is from a canvas layout area. 
                // Now we delete the associated line in line mode.

                if (Utilities.IsNotNull(lastLine.AssociatedDirectedLine))
                {
                    CanvasDirectedLine lineModeLine = lastLine.AssociatedDirectedLine;

                    //if (ShapeDict.ContainsKey(lineModeLine.NameID))
                    //{
                    //    ShapeDict.Remove(lineModeLine.NameID);
                    //}

                    if (CurrentPage.DirectedLineDictContains(lineModeLine))
                    {
                        CurrentPage.RemoveFromDirectedLineDict(lineModeLine);
                    }
                   
                    lineModeLine.LineFinishManager.RemoveLineFull(lineModeLine);

                    lineModeLine.Delete();
                }

                // The following is a kludge to delete the previous line if 
                // it is a 'complete L' line. This is done by a recursive call to
                // DeleteBuildingPolylineLastLine. Probably not the best approach
                // but quick and dirty
                CanvasDirectedLine lastLine2 = buildingPolyline.LastLine;

                if (Utilities.IsNotNull(lastLine2))
                {
                    if (lastLine2.IsCompleteLLine)
                    {
                        DeleteBuildingPolylineLastLine();
                    }
                }
            }

            lastLine.Delete();
        }
    }
}
