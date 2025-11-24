//-------------------------------------------------------------------------------//
// <copyright file="CanvasDirectedPolyline.cs" company="Bruun Estimating, LLC">  // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace CanvasManagerLib
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
//    using FloorMateralEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;
    using Geometry;
    using Graphics;
    using Utilities;
    using CanvasLib.Markers_and_Guides;
    using SettingsLib;
    using System.Diagnostics;
    using Globals;
    using TracerLib;

    public class CanvasDirectedPolyline : List<CanvasDirectedLine>
    {
        public List<Coordinate> CoordList = new List<Coordinate>();

        private FloorMaterialEstimatorBaseForm baseForm => canvasManager.BaseForm;

        private CanvasManager canvasManager;

        private CanvasPage currentPage => canvasManager.CurrentPage;

        private Color currentLineColor => baseForm.selectedLineFinish.LineColor;

        public AreaModeStartMarker StartMarker;

        private bool IsBaseZeroLine
        {
            get
            {
                if (canvasManager == null)
                {
                    return false;
                }

                return canvasManager.BaseForm.btnAreaDesignStateZeroLine.BackColor == Color.Orange;
            }
        }

        public CanvasDirectedPolyline(CanvasManager canvasManager)
        {
            this.canvasManager = canvasManager;

            this.Page = canvasManager.CurrentPage;

            this.Window = canvasManager.Window;

            this.ShapeType = ShapeType.Polyline;

            this.Guid = GuidMaintenance.CreateGuid(this);
        }

        //public CanvasDirectedPolyline(CanvasManager canvasManager, Microsoft.Office.Interop.Visio.Shape visioShape) : this(canvasManager)
        //{
        //    this.Shape = new Shape(visioShape, ShapeType.Polyline);

        //    this.Guid = GuidMaintenance.CreateGuid(this);
        //}

        //public CanvasDirectedLine BuildingLine;

        //public Shape Shape { get; set; }

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public ShapeType ShapeType { get; }

        private static int nameIdCounter = 0;

        private static string nameId = string.Empty;
        public string NameID
        {
            get
            {
                if (!string.IsNullOrEmpty(nameId))
                {
                    return nameId;
                }

                nameIdCounter++;

                nameId = "CanvasDirectedPolyline" + nameIdCounter.ToString("00000");

                return nameId;
            }
        }

        public string Guid { get; set; }

        public AreaShapeBuildStatus BuildStatus { get; set; } = AreaShapeBuildStatus.Unknown;

        internal void Delete()
        {
            foreach (CanvasDirectedLine line in this)
            {
                if (!Page.PageShapeDictContains(line))
                {
                    Tracer.TraceGen.TraceError("Attempt to remove line from Page Shape Dict which is not in the dictionary in CanvasDirectedPolyline:Delete.", 1, true);
                }

                else
                {
                    Page.RemoveFromPageShapeDict(line);
                }

                if (Utilities.IsNotNull(line.LineFinishManager))
                {
                    line.LineFinishManager.RemoveLineFull(line);
                }

                else
                {
                    //-------------------------------------------------------------//
                    // This is defensive. It should never be called.               //
                    //-------------------------------------------------------------//

                    Tracer.TraceGen.TraceError("Removing line which is not in the ucLine in CanvasDirectedPolyline:Delete.", 1, true);

                    VisioInterop.DeleteShape(line.Shape);
                }
            }

            if (!(StartMarker is null))
            {
                StartMarker.Delete();
                StartMarker = null;
            }

            this.CoordList.Clear();

        }

        public CanvasDirectedLine LastLine
        {
            get
            {
                if (Count <= 0)
                {
                    return null;
                }

                return base[Count - 1];
            }
        }

        public CanvasDirectedLine FrstLine
        {
            get
            {
                if (Count <= 0)
                {
                    return null;
                }

                return base[0];
            }
        }

        public List<CanvasDirectedLine> Perimeter
        {
            get
            {
                return this;
            }
        }

        public Shape PolygonInternalArea
        {
            get;
            set;
        }
        
          
        //public UCAreaFinishPaletteElement UCAreaFinish
        //{
        //    get;
        //    set;
        //}

        internal bool ValidateNoIntersection(double x, double y)
        {
            if (this.Count <= 0)
            {
                return true;
            }

            Coordinate coord1 = GetLastCoordinate();
            Coordinate coord2 = new Coordinate(x, y);

            DirectedLine directedLine = new DirectedLine(coord1, coord2);

            for (int i = 0; i < this.Count-1; i++)
            {
                if (directedLine.Intersects(this[i]))
                {
                    return false;
                }
            }

            directedLine = directedLine.ExtendStart(-0.01);

            if (directedLine.Intersects(this[Count - 1]))
            {
                return false;
            }

            return true;
        }

        //List<CanvasDirectedLine> IAreaShape.Perimeter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double PerimeterLength()
        {
            return Perimeter.Sum(l => l.Length) * Page.DrawingScaleInInches;
        }

        internal void BringToFront()
        {
            PolygonInternalArea.BringToFront();

            this.ForEach(l => l.Shape.BringToFront());
        }

        internal void AutofillFromGuides(double x, double y, int finishIndex)
        {
            double? xMin = null;
            double? xMax = null;
            double? yMin = null;
            double? yMax = null;

            canvasManager.FieldGuideController.GetBoundingGuides(x, y, out xMin, out xMax, out yMin, out yMax);

            if (xMin == null)
            {
                ManagedMessageBox.Show("No left guideline found.");

                resetAutoFillFromGuides();
               
                return;
            }

            if (xMax == null)
            {
                ManagedMessageBox.Show("No right guideline found.");

                resetAutoFillFromGuides();

                return;
            }

            if (yMax == null)
            {
                ManagedMessageBox.Show("No upper guideline found.");

                resetAutoFillFromGuides();

                return;
            }

            if (yMin == null)
            {
                ManagedMessageBox.Show("No lower guideline found.");

                resetAutoFillFromGuides();

                return;
            }

            AddPoint(xMin.Value, yMax.Value);
            AddPoint(xMax.Value, yMax.Value);
            AddPoint(xMax.Value, yMin.Value);
            AddPoint(xMin.Value, yMin.Value);
            AddPoint(xMin.Value, yMax.Value);

            canvasManager.CompletePolylineDraw(finishIndex);

        }

        internal bool AutoCompleteFromGuides(double x, double y, int finishIndex)
        {
            double? xMin = null;
            double? xMax = null;
            double? yMin = null;
            double? yMax = null;

            canvasManager.FieldGuideController.GetBoundingGuides(x, y, out xMin, out xMax, out yMin, out yMax);

            if (xMin == null)
            {
                //ManagedMessageBox.Show("No left guideline found.");

                resetAutoFillFromGuides();

                return false;
            }

            if (xMax == null)
            {
                //ManagedMessageBox.Show("No right guideline found.");

                resetAutoFillFromGuides();

                return false;
            }

            if (yMax == null)
            {
                //ManagedMessageBox.Show("No upper guideline found.");

                resetAutoFillFromGuides();

                return false;
            }

            if (yMin == null)
            {
                ManagedMessageBox.Show("No lower guideline found.");

                resetAutoFillFromGuides();

                return false;
            }

            AddPoint(xMin.Value, yMax.Value);
            AddPoint(xMax.Value, yMax.Value);
            AddPoint(xMax.Value, yMin.Value);
            AddPoint(xMin.Value, yMin.Value);
            AddPoint(xMin.Value, yMax.Value);

            canvasManager.CompletePolylineDraw(finishIndex);

            return true;

        }
        private void resetAutoFillFromGuides()
        {
            SystemState.DrawingShape = false;

            canvasManager.BuildingPolyline = null;

            SystemState.DrawingMode = DrawingMode.Default;
        }

        public double AreaInSqrInches()
        {
            return VisioInterop.GetShapeArea(PolygonInternalArea) * Math.Pow(Page.DrawingScaleInInches, 2.0);
        }

        //public UCFinish ucFinish { get; set; }

        internal CanvasDirectedLine RemoveLastLine()
        {
            if (Count <= 0)
            {
                return null;
            }

            CanvasDirectedLine returnLine = base[Count - 1];

            base.RemoveAt(Count - 1);

            CoordList.RemoveAt(CoordList.Count - 1);

            if (CoordList.Count > 0)
            {
                SetupMarkerAndGuides();
            }

            else
            {
                canvasManager.RemoveMarkerAndGuides();

                if (!(StartMarker is null))
                {
                    StartMarker.Delete();
                    StartMarker = null;
                }
            }

            return returnLine;
        }

        internal Coordinate GetFrstCoordinate()
        {
            if (CoordList.Count <= 0)
            {
                return Coordinate.NullCoordinate;
            }

            return CoordList.First();
        }

        internal Coordinate GetLastCoordinate()
        {
            if (CoordList.Count <= 0)
            {
                return Coordinate.NullCoordinate;
            }

            return CoordList.Last();
        }

        public void AddPoint(double x, double y, bool noGuidesOrMarker = false)
        {
            Coordinate currCoord = new Coordinate(x, y);

            if (CoordList.Count > 0)
            {
                Coordinate lastCoord = CoordList.Last();

                if (currCoord == lastCoord)
                {
                    // do not allow two points to lie on top of one another.

                    return;
                }
            }


            AddPoint(currCoord, noGuidesOrMarker);
        }

        public void AddPoint(Coordinate currCoord, bool noGuidesOrMarker = false)
        { 
            CoordList.Add(currCoord);

            if (CoordList.Count == 1)
            {
                SetupStartMarker();
            }

            if (CoordList.Count > 1)
            {
                Coordinate prevCoord = CoordList[CoordList.Count - 2];

                AddLine(prevCoord, currCoord);
            }

            if (noGuidesOrMarker)
            {
                return;
            }

            SetupMarkerAndGuides();
        }

        public void AddPoints(List<Coordinate> coordList, bool noGuidesOrMarker = false)
        {
            foreach (Coordinate currCoord in coordList)
            {
                CoordList.Add(currCoord);

                if (CoordList.Count > 1)
                {
                    Coordinate prevCoord = CoordList[CoordList.Count - 2];

                    AddLine(prevCoord, currCoord);
                }
            }

            if (noGuidesOrMarker)
            {
                return;
            }

            SetupMarkerAndGuides();
        }

        public void InsertPoint(Coordinate currCoord, int index)
        {
            if (index != 0)
            {
                // Not yet coded to insert point anywhere.
                throw new NotImplementedException();
            }

            Coordinate nextCoord = CoordList[0];

            CoordList.Insert(0, currCoord);

            InsertLine(currCoord, nextCoord, index);

            SetupStartMarker();
        }

        private void AddLine(Coordinate coord1, Coordinate coord2)
        {
            InsertLine(coord1, coord2, this.Count);
        }

        private void InsertLine(Coordinate coord1, Coordinate coord2, int index)
        {
#if DEBUG
            // Check to be sure insertion keeps perimeter consistent

            if (Count > 0)
            {
                if (index == 0)
                {
                    Debug.Assert(Coordinate.H2Distance(coord2, this[0].Coord1) < 0.01);
                }

                else if (index == Count)
                {
                    Debug.Assert(Coordinate.H2Distance(coord1, this[Count-1].Coord2) < 0.01);
                }

                else if (Count >= 2)
                {
                    Debug.Assert(Coordinate.H2Distance(coord1, this[index - 1].Coord2) < 0.01);
                    Debug.Assert(Coordinate.H2Distance(coord2, this[index].Coord1) < 0.01);
                }
            }
#endif
            CanvasDirectedLine canvasDirectedLine = new CanvasDirectedLine(
                canvasManager
                , Window
                , Page
                , baseForm.selectedLineFinishManager
                , new GraphicsDirectedLine(this.Window, this.Page, new DirectedLine(coord1, coord2), LineRole.ExternalPerimeter)
                , canvasManager.DesignState)
            {
                LineStartCursorPosition = canvasManager.BaseForm.GetCursorPosition(),
                IsZeroLine = IsBaseZeroLine
            };

            Insert(index, canvasDirectedLine);

            canvasDirectedLine.Draw(
                canvasManager.SelectedLineType.LineColor,
                canvasManager.SelectedLineType.LineWidthInPts);

            //canvasManager.ShapeDict.Add(canvasDirectedLine.NameID, canvasDirectedLine);

            //canvasManager.SelectedLineType.AddLine(canvasDirectedLine);

            // Set line style for current line, but note, we are not adding it to the line finish element

            canvasDirectedLine.ucLine = canvasManager.SelectedLineType;

            baseForm.selectedLineFinishManager.AddLine(canvasDirectedLine);

            canvasDirectedLine.SetBaseLineStyle(AreaShapeBuildStatus.Building);

            Window?.DeselectAll();
        }

        public void AddExtendingLineFromGuides(double x, double y)
        {
            double? guideX = null;
            double? guideY = null;

            if (!canvasManager.FieldGuideController.GetClosestGuides(x, y, out guideX, out guideY, GlobalSettings.SnapDistance, 0))
            {
                ManagedMessageBox.Show("No guideline found.");
                return;
            }

            if (guideX.HasValue && guideY.HasValue)
            {
                if (Math.Abs(guideX.Value - x) <= Math.Abs(guideY.Value - y))
                {
                    AddExtendingLineFromGuidesVertical(guideX.Value, y);
                }

                else
                {
                    AddExtendingLineFromGuidesHorizontal(x, guideY.Value);
                }
            }

            else if (guideX.HasValue)
            {
                AddExtendingLineFromGuidesVertical(guideX.Value, y);
            }

            else
            {
                AddExtendingLineFromGuidesHorizontal(x, guideY.Value);
            }

            Window?.DeselectAll();
        }

        private void AddExtendingLineFromGuidesVertical(double x, double y)
        {
            Coordinate coord1;
            Coordinate coord2;

            if (!getVerticalLineBounds(x, y, out coord1, out coord2))
            {
                return;
            }

            if (Coordinate.H2Distance(coord1, coord2) <= 0.1)
            {
                // Line is too small to consider

                return;
            }

            AddExtendingLine(coord1, coord2);
        }

        private void AddExtendingLineFromGuidesHorizontal(double x, double y)
        {
            Coordinate coord1;
            Coordinate coord2;

            if (!getHorizontalLineBounds(x, y, out coord1, out coord2))
            {
                return;
            }

            if (Coordinate.H2Distance(coord1, coord2) <= 0.1)
            {
                // Line is too small to consider

                return;
            }

            AddExtendingLine(coord1, coord2);
        }

        private void AddExtendingLine(Coordinate coord1, Coordinate coord2)
        {
            // The line segment must extend either the first line or last line in the building polyline

            Coordinate frstCoord = GetFrstCoordinate();
            Coordinate lastCoord = GetLastCoordinate();

            if (Coordinate.H2Distance(coord1, lastCoord) < 0.01)
            {
                if (Coordinate.H2Distance(coord2, frstCoord) < 0.01)
                {
                    AddCompletingLine(coord1, coord2);

                    return;
                }

                // The first coordinate of the extending line is the same as the last coordinate of the building polyline.

                AddExtendingLineAtEnd(coord1, coord2);

                return;
            }

            else if (Coordinate.H2Distance(coord2, lastCoord) < 0.01)
            {
                if (Coordinate.H2Distance(coord1, frstCoord) < 0.01)
                {
                    AddCompletingLine(coord2, coord1);

                    return;
                }

                // The second coordinate of the extending line is the same as the last coordinate of the building polyline.

                AddExtendingLineAtEnd(coord2, coord1);

                return;
            }

            else if (Coordinate.H2Distance(coord1, frstCoord) < 0.01)
            {
                // The first coordinate of the extending line is the same as the last coordinate of the building polyline.

                AddExtendingLineAtFront(coord2, coord1);

                return;
            }

            else if (Coordinate.H2Distance(coord2, frstCoord) < 0.01)
            {
                // The second coordinate of the extending line is the same as the last coordinate of the building polyline.

                AddExtendingLineAtFront(coord1, coord2);

                return;
            }
        }

        private void AddExtendingLineAtFront(Coordinate coord1, Coordinate coord2)
        {
            InsertPoint(coord1, 0);

            //CanvasDirectedLine canvasDirectedLine = new CanvasDirectedLine(GraphicsPage, new GraphicsDirectedLine(this.GraphicsPage, new DirectedLine(coord1, coord2)), canvasManager.DesignState)
            //{
            //    IsZeroLine = IsBaseZeroLine
            //};

            //this.Insert(0, canvasDirectedLine);

            //AddExtendingLine(canvasDirectedLine);
        }

        private void AddExtendingLineAtEnd(Coordinate coord1, Coordinate coord2)
        {
            //AddPoint(coord1);
            AddPoint(coord2);
            //CanvasDirectedLine canvasDirectedLine = new CanvasDirectedLine(GraphicsPage, new GraphicsDirectedLine(this.GraphicsPage, new DirectedLine(coord1, coord2)), canvasManager.DesignState)
            //{
            //    IsZeroLine = IsBaseZeroLine
            //};

            //this.Add(canvasDirectedLine);

            //AddExtendingLine(canvasDirectedLine);
        }

        private void AddCompletingLine(Coordinate coord1, Coordinate coord2)
        {
            AddPoint(coord2);

            canvasManager.CompletePolylineDraw();
        }

        public void AddInitialLineFromGuides(double x, double y)
        {
            double? guideX = null;
            double? guideY = null;

            if (!canvasManager.FieldGuideController.GetClosestGuides(x, y, out guideX, out guideY, GlobalSettings.SnapDistance, 0))
            {
                ManagedMessageBox.Show("No guideline found.");
                return;
            }

            if (guideX.HasValue && guideY.HasValue)
            {
                if (Math.Abs(guideX.Value - x) <= Math.Abs(guideY.Value - y))
                {
                    AddInitialLineFromGuidesVertical(guideX.Value, y);
                }

                else
                {
                    AddInitialLineFromGuidesHorizontal(x, guideY.Value);
                }
            }

            else if (guideX.HasValue)
            {
                AddInitialLineFromGuidesVertical(guideX.Value, y);
            }

            else
            {
                AddInitialLineFromGuidesHorizontal(x, guideY.Value);
            }

        }

        private bool AddInitialLineFromGuidesVertical(double x, double y)
        {
            Coordinate coord1;
            Coordinate coord2;

            if (!getVerticalLineBounds(x, y, out coord1, out coord2))
            {
                return false;
            }

            AddInitialLine(coord1, coord2);

            return true;
        }

        private bool AddInitialLineFromGuidesHorizontal(double x, double y)
        {
            Coordinate coord1;
            Coordinate coord2;

            if (!getHorizontalLineBounds(x, y, out coord1, out coord2))
            {
                return false;
            }

            AddInitialLine(coord1, coord2);

            return true;
        }

        private void AddInitialLine(Coordinate coord1, Coordinate coord2)
        {
            AddPoint(coord1);
            AddPoint(coord2);
        }

        private bool getHorizontalLineBounds(double x, double y, out Coordinate coord1, out Coordinate coord2)
        {
            coord1 = Coordinate.NullCoordinate;
            coord2 = Coordinate.NullCoordinate;

            double? xMin = canvasManager.FieldGuideController.GetLeftGuideLineX(x);

            if (xMin == null)
            {
                ManagedMessageBox.Show("No left guide line found.");

                return false;
            }

            double? xMax = canvasManager.FieldGuideController.GetRghtGuideLineX(x);

            if (xMax == null)
            {
                ManagedMessageBox.Show("No right guide line found.");
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

            double? yMin = canvasManager.FieldGuideController.GetLowrGuideLineY(y);

            if (yMin == null)
            {
                ManagedMessageBox.Show("No lower guide line found.");

                return false;
            }

            double? yMax = canvasManager.FieldGuideController.GetUpprGuideLineY(y);

            if (yMax == null)
            {
                ManagedMessageBox.Show("No upper guide line found.");

                return false;
            }

            coord1 = new Coordinate(x, yMin.Value);
            coord2 = new Coordinate(x, yMax.Value);

            return true;
        }

        internal void ValidateConsistentPerimeter()
        {
            Debug.Assert(Count > 2);
            
            for (int i = 0; i < Count; i++)
            {
                CanvasDirectedLine l1 = base[i];
                CanvasDirectedLine l2 = base[(i + 1) % Count];

                Debug.Assert(CoordList[i] == l1.Coord1);
                Debug.Assert(CoordList[i+1] == l2.Coord1);
                Debug.Assert(Coordinate.H2Distance(l1.Coord2, l2.Coord1) < 0.001);
            }
        }
        
        //internal void SetCompletedLineWidth()
        //{
        //    base.ForEach(l => l.Shape.SetLineWidth(CanvasManager.CompletedShapeLineWidthInPts));
        //}

        //internal void CreateInternalAreaShape()
        //{
        //    double[] coordinateArray = GetCoordinateArray();

        //    PolygonInternalArea = canvasManager.CurrentPage.DrawPolyline(coordinateArray, 0);
      
        //    Shape = PolygonInternalArea;

        //    PolygonInternalArea.ShowShapeOutline(false);

        //    Shape.LockSelected(0);

        //    PolygonInternalArea.VisioShape.BringToFront();
        //}

        private double[] GetCoordinateArray()
        {
            Coordinate coord;

            double[] returnCoords = new double[2 * (Count + 1)];

            for (int i = 0; i < Count; i++)
            {
                coord = base[i].Coord1;

                int j = 2 * i;

                returnCoords[j] = coord.X;
                returnCoords[j + 1] = coord.Y;
            }

            coord = base[0].Coord1;

            returnCoords[2 * Count] = coord.X;
            returnCoords[2 * Count + 1] = coord.Y;

            return returnCoords;
        }

        public List<Coordinate> GetCoordinateList()
        {
            List<Coordinate> rtrnList = new List<Coordinate>();

            for (int i = 0; i < Count; i++)
            {
                rtrnList.Add(base[i].Coord1);
            }

            rtrnList.Add(base[Count - 1].Coord2);

            return rtrnList;
        }

        //internal CanvasDirectedPolyline Clone()
        //{
        //    CanvasDirectedPolyline clonedPolyline = new CanvasDirectedPolyline(this.canvasManager);

        //    foreach (CanvasDirectedLine sourceLine in this)
        //    {
        //        CanvasDirectedLine clonedLine = sourceLine.Clone();

        //        clonedLine.Draw(sourceLine.ucLine.LineColor, sourceLine.ucLine.LineWidthInPts, sourceLine.ucLine.VisioDashType);

        //        sourceLine.ucLine.AddLineFull(clonedLine);

        //        clonedPolyline.Add(clonedLine);
        //    }

        //    return clonedPolyline;
        //}

        public void SetupStartMarker()
        {
            if (!(StartMarker is null))
            {
                StartMarker.Delete();
            }

            Coordinate coord = GetFrstCoordinate();

            double x = coord.X;
            double y = coord.Y;

            StartMarker = new AreaModeStartMarker(x, y, 0.075, currentLineColor);

#if DEBUG
            baseForm.UpdateDebugForm();
#endif

            StartMarker.Draw(currentPage);

#if DEBUG
            baseForm.UpdateDebugForm();
#endif
        }

        public void SetupMarkerAndGuides()
        {
            Coordinate coord = GetLastCoordinate();

            canvasManager.SetupMarkerAndGuides(coord);
        }

        public void SetupMarkerAndGuides(Coordinate coord)
        {
            canvasManager.SetupMarkerAndGuides(coord);
        }

        public double Orientation()
        {
            if (CoordList.Count <= 1)
            {
                return 0;
            }

            List<Tuple<double, double>> coordinateList = CoordList.Select(c => new Tuple<double, double>(c.X, c.Y)).ToList();

            coordinateList.Add(new Tuple<double, double>(CoordList[0].X, CoordList[0].Y));

            return MathUtils.CurveOrientation(coordinateList);
        }
    }
}
