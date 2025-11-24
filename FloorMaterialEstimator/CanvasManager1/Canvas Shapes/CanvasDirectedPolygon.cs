//-------------------------------------------------------------------------------//
// <copyright file="CanvasDirectedPolygon.cs"                                    //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    
    using Globals;

    using Graphics;
    using Geometry;
    using System.Diagnostics;
    using Utilities;
    using FinishesLib;
    using PaletteLib;
    using TracerLib;

    public class CanvasDirectedPolygon : GraphicsDirectedPolygon, IAreaShape
    {
        // public string Guid { get; set; }

        public new Shape Shape
        {
            get
            {
                return PolygonInternalArea;
            }

            set
            {
                PolygonInternalArea = value;
                base.Shape = value;

                // The following is a kludge to catch situations where the shape role has not been set

                //if (value.Data1 is null)
                //{
                //    //Tracer.TraceGen.TraceError("CanvasDirectedPolygon:Shape(set) value has null Datat1", 1, true);

                //    VisioInterop.SetShapeData1(PolygonInternalArea, "LayoutArea");
                //    VisioInterop.SetShapeData1(base.Shape, "LayoutArea");
                //}
            }
        }

        private List<CanvasDirectedLine> perimeter;

        public new List<CanvasDirectedLine> Perimeter
        {
            get
            {
                if (perimeter is null)
                {
                    perimeter = new List<CanvasDirectedLine>();

                    foreach (GraphicsDirectedLine graphicsDirectedLine in base.Perimeter)
                    {
                        
                        perimeter.Add(new CanvasDirectedLine(canvasManager, null, graphicsDirectedLine, canvasManager.DesignState));
                    }
                }

                return perimeter;
            }

            set
            {
                perimeter = value;

                base.Clear();

                perimeter.ForEach(l => base.PerimeterAdd(l));
            }
        }

        public void PerimeterAdd(CanvasDirectedLine line)
        {
            if (perimeter == null)
            {
                perimeter = new List<CanvasDirectedLine>();
            }

            perimeter.Add(line);

            base.PerimeterAdd(line);
        }
        
        private CanvasManager canvasManager;

        public CanvasLayoutArea ParentLayoutArea { get; set; }

        #region Constructors

        public CanvasDirectedPolygon(CanvasManager canvasManager, GraphicsWindow window, GraphicsPage page): base(window, page)
        {
            this.canvasManager = canvasManager;

            this.Guid = GuidMaintenance.CreateGuid(this);

        }

        //public CanvasDirectedPolygon(
        //    CanvasManager canvasManager,
        //    List<CanvasDirectedLine> directedLineList) : base(canvasManager.Window, canvasManager.CurrentPage)
        //{
        //    this.Guid = GuidMaintenance.CreateGuid(this);

        //    this.canvasManager = canvasManager;

        //    this.Perimeter = directedLineList;

        //    base.PerimeterLineAdded += CanvasDirectedPolygon_PerimeterLineAdded;
        //}

        internal void AddAssociatedLines(GraphicsWindow window, GraphicsPage page, string lineName)
        {
    
            foreach (CanvasDirectedLine canvasDirectedLine in this.Perimeter)
            {
                CanvasDirectedLine lineClonedCanvasDirectedLine;

                UCLineFinishPaletteElement ucLine = canvasDirectedLine.ucLine;

                LineFinishManager lineFinishManager = canvasDirectedLine.LineFinishManager;

                lineClonedCanvasDirectedLine = canvasDirectedLine.Clone();

                lineClonedCanvasDirectedLine.ucLine = ucLine;

                lineClonedCanvasDirectedLine.LineRole = LineRole.SingleLine;

                // This forces the system to think that the line was created in line mode

                lineClonedCanvasDirectedLine.OriginatingDesignState = DesignState.Line;

                lineClonedCanvasDirectedLine.Draw();

                lineClonedCanvasDirectedLine.Shape.SetShapeData("Associated Line", "Line[" + lineName + "]", lineClonedCanvasDirectedLine.Guid);

                lineFinishManager.AddLine(lineClonedCanvasDirectedLine);

                lineFinishManager.AddLineToLayer(lineClonedCanvasDirectedLine.Guid, DesignState.Line, SeamMode.Unknown);

                canvasManager.CurrentPage.AddToDirectedLineDict(lineClonedCanvasDirectedLine);

                canvasManager.LineHistoryList.Add(lineClonedCanvasDirectedLine);

                canvasDirectedLine.AssociatedDirectedLine = lineClonedCanvasDirectedLine;

                lineClonedCanvasDirectedLine.AssociatedDirectedLine = canvasDirectedLine;
            }
        }

        public CanvasDirectedPolygon(
            CanvasManager canvasManager,
            string guid,
            List<CanvasDirectedLine> directedLineList): base(canvasManager.Window, canvasManager.CurrentPage)
        {
            this.Guid = guid;

            this.canvasManager = canvasManager;

            this.Perimeter = directedLineList;

            base.PerimeterLineAdded += CanvasDirectedPolygon_PerimeterLineAdded;
        }

        public CanvasDirectedPolygon(CanvasManager canvasManager, CanvasDirectedPolyline canvasDirectedPolyline): base(canvasDirectedPolyline.Window, canvasDirectedPolyline.Page)
        {
            Debug.Assert(canvasManager != null);
            Debug.Assert(canvasDirectedPolyline != null);

            this.Guid = GuidMaintenance.CreateGuid(this);

            this.canvasManager = canvasManager;

            this.canvasDirectedPolyline = canvasDirectedPolyline;

            List<CanvasDirectedLine> perimeterList = new List<CanvasDirectedLine>();
            
            canvasDirectedPolyline.ForEach(l => { perimeterList.Add(l); l.ParentPolygon = this;  canvasManager.CurrentPage.AddToDirectedLineDict(l); });

            this.Perimeter = perimeterList;

            this.PolygonInternalArea = canvasDirectedPolyline.PolygonInternalArea;

            base.PerimeterLineAdded += CanvasDirectedPolygon_PerimeterLineAdded;
            Debug.Assert(AllLinesAssignedToUCLineElement());
        }

        public CanvasDirectedPolygon(
            CanvasManager canvasManager,
            GraphicsWindow window,
            GraphicsPage page,
            GraphicsDirectedPolygon graphicsDirectedPolygon,
            UCLineFinishPaletteElement ucLine,
            DesignState designState) : this(canvasManager, window, page)
        {
            this.canvasManager = canvasManager;

            this.Page = page;

            List<CanvasDirectedLine> perimeterList = new List<CanvasDirectedLine>();

            foreach (GraphicsDirectedLine line in graphicsDirectedPolygon)
            {
                LineFinishManager lineFinishManager = this.canvasManager.LineFinishManagerList[ucLine.Guid];

                CanvasDirectedLine l = new CanvasDirectedLine(canvasManager, window, page, lineFinishManager, line, designState,  false);

                l.ucLine = ucLine;

                l.ParentPolygon = this;

                perimeterList.Add(l);

                this.Add(l);


                if (!string.IsNullOrEmpty(l.Guid))
                {
                    canvasManager.CurrentPage.AddToDirectedLineDict(l);
                }
            }

            this.Perimeter = perimeterList;

            this.Guid = GuidMaintenance.CreateGuid(this);

            base.PerimeterLineAdded += CanvasDirectedPolygon_PerimeterLineAdded;
            Debug.Assert(AllLinesAssignedToUCLineElement());
        }

        public CanvasDirectedPolygon(CanvasManager canvasManager, DirectedPolygon polygon, DesignState designState): base(canvasManager.Window, canvasManager.CurrentPage)
        {
            this.canvasManager = canvasManager;

            
            foreach (DirectedLine directedLine in polygon)
            {
                GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(canvasManager.Window, canvasManager.CurrentPage, directedLine, LineRole.ExternalPerimeter);

                CanvasDirectedLine canvasDirectedLine = new CanvasDirectedLine(canvasManager, null, graphicsDirectedLine, designState);

                PerimeterAdd(canvasDirectedLine);
            }


            base.PerimeterLineAdded += CanvasDirectedPolygon_PerimeterLineAdded;
        }

        //public CanvasDirectedPolygon(
        //    CanvasManager canvasManager
        //    , DirectedPolygon polygon
        //    , UCLineFinishPaletteElement lineFinishElement
        //    , DesignState designState) : base(canvasManager.Window, canvasManager.CurrentPage)
        //{
        //    this.canvasManager = canvasManager;

        //    foreach (DirectedLine directedLine in polygon)
        //    {
        //        GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(canvasManager.Window, canvasManager.CurrentPage, directedLine, LineRole.ExternalPerimeter);
        //        CanvasDirectedLine canvasDirectedLine = new CanvasDirectedLine(canvasManager, null, graphicsDirectedLine, designState);

        //        canvasDirectedLine.ucLine = lineFinishElement;

        //        canvasDirectedLine.Draw();

        //        lineFinishElement.AddLineFull(canvasDirectedLine);

        //        PerimeterAdd(canvasDirectedLine);
        //    }

        //    base.PerimeterLineAdded += CanvasDirectedPolygon_PerimeterLineAdded;
        //    Window?.DeselectAll();
        //}

        #endregion

        private void CanvasDirectedPolygon_PerimeterLineAdded(GraphicsDirectedLine graphicsDirectedLine)
        {
            if (perimeter is null)
            {
                perimeter = new List<CanvasDirectedLine>();
            }

            LineFinishManager lineFinishManager = canvasManager.LineFinishManagerList[graphicsDirectedLine.Guid];

            perimeter.Add(new CanvasDirectedLine(canvasManager, lineFinishManager, graphicsDirectedLine, canvasManager.DesignState));
        }


        public CanvasDirectedLine LastLine
        {
            get
            {
                if (Count <= 0)
                {
                    return null;
                }

                return Perimeter[Count - 1];
            }
        }

        public CanvasDirectedLine FirstLine
        {
            get
            {
                if (Count <= 0)
                {
                    return null;
                }

                return Perimeter[0];
            }
        }

        private AreaShapeBuildStatus buildStatus = AreaShapeBuildStatus.Unknown;
        private CanvasDirectedPolyline canvasDirectedPolyline;

        AreaShapeBuildStatus BuildStatus
        {
            get
            {
                return this.buildStatus;
            }

            set
            {
                this.buildStatus = value;
            }
        }

        internal Coordinate GetMaxCoord()
        {
            double minX = double.MaxValue;

            foreach (CanvasDirectedLine line in this)
            {
                if (line.Coord1.X < minX)
                {
                    minX = line.Coord1.X;
                }
            }

            double maxY = double.MinValue;

            foreach (CanvasDirectedLine line in this)
            {
                if (line.Coord1.X == minX)
                {
                    if (line.Coord1.Y > maxY)
                    {
                        maxY = line.Coord1.Y;
                    }
                }
            }

            return new Coordinate(minX, maxY);
        }

        public UCAreaFinishPaletteElement UCAreaFinish
        {
            get;
            set;
        }

        public AreaFinishBase AreaFinishBase => UCAreaFinish.AreaFinishBase;

        internal void GetBounds(out double minX, out double maxX, out double minY, out double maxY)
        {
            minX = this.Min(l => Math.Min(l.Coord1.X, l.Coord2.X));
            maxX = this.Max(l => Math.Max(l.Coord1.X, l.Coord2.X));
            minY = this.Min(l => Math.Min(l.Coord1.Y, l.Coord2.Y));
            maxY = this.Max(l => Math.Max(l.Coord1.Y, l.Coord2.Y));
        }

        internal bool IntersectsBoundaries(CanvasLayoutArea canvasLayoutArea)
        {
            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                if (canvasDirectedLine.Intersects(canvasLayoutArea.ExternalArea))
                {
                    return true;
                }

                foreach (CanvasDirectedPolygon interiorBoundary in canvasLayoutArea.InternalAreas)
                {
                    if (canvasDirectedLine.Intersects(interiorBoundary))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        AreaShapeBuildStatus IAreaShape.BuildStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public AreaFinishManager AreaFinishManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double PerimeterLength()
        {
            double totlLnth = 0.0;

            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                if (!canvasDirectedLine.IsZeroLine)
                {
                    totlLnth += canvasDirectedLine.Length;
                }
            }

            return totlLnth * Page.DrawingScaleInInches;
        }

        public double PerimeterLengthIncludingZeroLines()
        {
            double totlLnth = 0.0;

            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                totlLnth += canvasDirectedLine.Length;
            }

            return totlLnth * Page.DrawingScaleInInches;
        }

        public void SetFillColor(Color color)
        {
            this.Shape.SetFillColor(color);
        }

        public void SetFillColor(string visioFillColorFormula)
        {
            this.Shape.SetFillColor(visioFillColorFormula);
        }

        public void SetFillOpacity(double opacity)
        {
            this.Shape.SetFillOpacity( opacity);
        }

        public void SetFillTransparancy(string visioFillTransparencyFormula)
        {
            this.Shape.SetFillTransparency(visioFillTransparencyFormula);
        }

        public void SetLineGraphics(DesignState designState, bool selected, AreaShapeBuildStatus buildStatus)
        {
            Perimeter.ForEach(l => l.SetLineGraphics(designState, selected, AreaShapeBuildStatus.Completed));
        }

        public void SetLineGraphicsForBorderArea()
        {
            Perimeter.ForEach(l => l.SetLineGraphicsForBorderArea());
        }

        public void ShowLineGraphics()
        {
            Perimeter.ForEach(l => l.ShowLineGraphics());
        }

        public void HideLineGraphics()
        {
            Perimeter.ForEach(l => l.HideLineGraphics());
        }

        internal void BringToFront()
        {
            PolygonInternalArea.BringToFront();

            Perimeter.ForEach(l => l.Shape.BringToFront());
        }

        public new double NetAreaInSqrInches()
        {
            return base.AreaInSqrInches(Page.DrawingScaleInInches);// * Math.Pow(GraphicsPage.DrawingScaleInInches, 2.0);
        }

        //public UCFinish ucFinish { get; set; }

        internal CanvasDirectedLine RemoveLastLine()
        {
            if (Count <= 0)
            {
                return null;
            }

            CanvasDirectedLine returnLine = Perimeter[Count - 1];

            base.RemoveAt(Count - 1);

            return returnLine;
        }

        internal void MakeDisappear()
        {
            VisioInterop.SetFillPattern(this.PolygonInternalArea, "0");

            Perimeter.ForEach(l => l.MakeDisappear());

            //VisioInterop.SendToBack(this.PolygonInternalArea);

            //VisioInterop.SetShapeVisibility(this.Shape, false);
        }

        internal void MakeReappear()
        {
            VisioInterop.SetFillPattern(this.PolygonInternalArea, "1");

            Perimeter.ForEach(l => l.MakeReappear());
        }

        internal Coordinate GetFirstCoordinate()
        {
            if (Count <= 0)
            {
                throw new NotImplementedException();
            }

            return Perimeter[0].GetLineStartpoint();
        }

        internal Coordinate GetLastCoordinate()
        {
            if (Count <= 0)
            {
                throw new NotImplementedException();
            }

            return Perimeter[Count - 1].GetLineEndpoint();
        }

        internal void SetDesignStateSelectedLineGraphics()
        {
            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                canvasDirectedLine.SetDesignStateSelectedLineGraphics();
            }
        }

        internal void RemoveFromSystem()
        {
            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                canvasDirectedLine.RemoveFromSystem();
            }
        }

        internal void Delete(bool deleteAssociatedLines = false)
        {
            Debug.Assert(this.canvasManager != null);
            
            if (this.PolygonInternalArea == null)
            {
                return;
            }

            foreach (CanvasDirectedLine canvasDirectedLine in Perimeter)
            {
                if (deleteAssociatedLines)
                {
                    if (Utilities.IsNotNull(canvasDirectedLine.AssociatedDirectedLine))
                    {
                        canvasManager.LineHistoryList.Remove(canvasDirectedLine.AssociatedDirectedLine);

                        canvasDirectedLine.AssociatedDirectedLine.Delete();
                    }
                }

                // The following line is removed because the 'canvasDirectedLine.Delete' command removes the line from 
                // the shape dict anyway.
               
                //Page.RemoveFromPageShapeDict(canvasDirectedLine); // This line is not necessary.

                canvasDirectedLine.Delete();
            }

            Perimeter.Clear();

            VisioInterop.DeleteShape(PolygonInternalArea);

            if (canvasManager.Page.PageShapeDictContains(this.PolygonInternalArea.Guid))
            {
                canvasManager.Page.RemoveFromPageShapeDict(this.PolygonInternalArea.Guid);
            }
        }

        internal void Redraw(DesignState lineArea, AreaShapeBuildStatus areaBuildStatus)
        {
            CreateInternalAreaShape(this.Guid);

            foreach (CanvasDirectedLine line in this)
            {
                line.Redraw(lineArea, false, areaBuildStatus);
            }
        }

        internal void Undraw()
        {
            Debug.Assert(this.canvasManager != null);
           // Debug.Assert(this.Shape.NameID == this.PolygonInternalArea.NameID);

            if (PolygonInternalArea != null)
            {
                VisioInterop.DeleteShape(PolygonInternalArea);

                this.PolygonInternalArea = null;
            }

            foreach (CanvasDirectedLine line in this)
            {
                line.Delete();

                if (Utilities.IsNotNull(line.AssociatedDirectedLine))
                {
                    if (!KeyboardUtils.ShiftKeyPressed)
                    {
                        line.AssociatedDirectedLine.Delete();
                    }
                }
            }
        }

        internal Shape CreateInternalAreaShape()
        {
            double[] coordinateArray = GetCoordinateArray();

            this.Guid = GuidMaintenance.CreateGuid(this);

            Shape = canvasManager.CurrentPage.DrawPolygon(coordinateArray, this.Guid);

            Shape.VisioShape.Data1 = "Internal Area";

            Shape.VisioShape.Data3 = this.Guid;

            PolygonInternalArea.VisioShape.BringToFront();

            return Shape;
        }

        internal Shape CreateInternalAreaShape(string guid)
        {
            double[] coordinateArray = GetCoordinateArray();

            Shape = canvasManager.CurrentPage.DrawPolygon(coordinateArray, guid);

            PolygonInternalArea.VisioShape.BringToFront();

            return Shape;
        }

        internal Shape CreateInternalAreaShape(GraphicsPage page)
        {
            double[] coordinateArray = GetCoordinateArray();

            //this.Guid = GuidMaintenance.CreateGuid(this);

            Shape = canvasManager.CurrentPage.DrawPolygon(coordinateArray, this.Guid);

            PolygonInternalArea = Shape;

            PolygonInternalArea.ShowShapeOutline(false);

            PolygonInternalArea.VisioShape.BringToFront();

            return Shape;
        }

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

        internal Shape Draw(GraphicsPage page, Color color, bool drawPolygonOnly = false)
        {
            double[] coordinates = GetCoordinateArray();

            Shape = page.DrawPolyline(coordinates, 1);

            VisioInterop.SetBaseFillColor(Shape, color);

            if (!drawPolygonOnly)
            {
                foreach (CanvasDirectedLine canvasDirectedLine in this)
                {
                    canvasDirectedLine.Draw();
                }
            }
            
            VisioInterop.SetShapeData(Shape, "Layout Area", "Compound Shape[" + UCAreaFinish.AreaName + "]", Shape.Guid);

            return Shape;
        }
      
        public new CanvasDirectedPolygon Clone()
        {
            CanvasDirectedPolygon clonedCanvasDirectedPolygon
                = new CanvasDirectedPolygon(
                        this.canvasManager
                        , this.Window
                        , this.Page
                  );

            foreach (CanvasDirectedLine canvasDirectedLine in this.Perimeter)
            {
                CanvasDirectedLine clonedCanvasDirectedLine = canvasDirectedLine.Clone();

                clonedCanvasDirectedLine.ucLine = canvasDirectedLine.ucLine;

                clonedCanvasDirectedPolygon.PerimeterAdd(clonedCanvasDirectedLine);
            }

            foreach (CanvasDirectedLine canvasDirectedLine in clonedCanvasDirectedPolygon)
            {
                canvasDirectedLine.ParentPolygon = clonedCanvasDirectedPolygon;

                canvasDirectedLine.LineRole = LineRole.ExternalPerimeter;
            }

            return clonedCanvasDirectedPolygon;
        }

        public CanvasDirectedPolygon CloneBasic(GraphicsWindow window, GraphicsPage page)
        {
            CanvasDirectedPolygon clonedCanvasDirectedPolygon
                = new CanvasDirectedPolygon(
                        null // this.canvasManager
                        , window
                        , page
                  );

            foreach (CanvasDirectedLine canvasDirectedLine in this.Perimeter)
            {
                CanvasDirectedLine clonedCanvasDirectedLine = canvasDirectedLine.CloneBasic(window, page);

                clonedCanvasDirectedLine.ucLine = canvasDirectedLine.ucLine;

                clonedCanvasDirectedPolygon.PerimeterAdd(clonedCanvasDirectedLine);
            }

            foreach (CanvasDirectedLine canvasDirectedLine in clonedCanvasDirectedPolygon)
            {
                canvasDirectedLine.ParentPolygon = clonedCanvasDirectedPolygon;

                canvasDirectedLine.LineRole = LineRole.ExternalPerimeter;
            }

            return clonedCanvasDirectedPolygon;
        }

        #region Debug Support

        private bool AllLinesAssignedToUCLineElement()
        {
            foreach (CanvasDirectedLine line in this)
            {
                if (line.ucLine is null)
                {
                    return false;
                }
            }

            return true;
        }

        internal void BringPerimeterToFront()
        {
            foreach (CanvasDirectedLine line in Perimeter)
            {
                if (Utilities.IsNotNull(line.Shape))
                {
                    line.Shape.BringToFront();
                }
            }
        }

        internal void AddToLayer(GraphicsLayer graphicsLayer)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { graphicsLayer });

            try
            {
                graphicsLayer.AddShape(Shape, 1);

                foreach (CanvasDirectedLine canvasDirectedLine in this.Perimeter)
                {
                    graphicsLayer.AddShape(canvasDirectedLine.Shape, 1);
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("CanvasDirectedPolygon:AddToLayer throws an exception.", ex, 1, true);
            }
        }

        internal void RemoveFromLayer(GraphicsLayer graphicsLayer)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { graphicsLayer });

            try
            {
                graphicsLayer.RemoveShape(Shape, 1);

                foreach (CanvasDirectedLine canvasDirectedLine in this.Perimeter)
                {
                    graphicsLayer.RemoveShape(canvasDirectedLine.Shape, 1);
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("CanvasDirectedPolygon:RemoveFromLayer throws an exception.", ex, 1, true);
            }
        }

        internal void Lock()
        {
            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                VisioInterop.LockShape(canvasDirectedLine.Shape);
            }
        }

        internal void Unlock()
        {
            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                VisioInterop.UnlockShape(canvasDirectedLine.Shape);
            }
        }

        internal bool IsLocked()
        {
            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                if (VisioInterop.IsLocked(canvasDirectedLine.Shape))
                {
                    return true;
                }
            }

            return false;
        }

        internal Coordinate GetSelectedVertex(double x, double y, out CanvasDirectedLine line1, out CanvasDirectedLine line2)
        {
            Coordinate vertexCoord = Coordinate.NullCoordinate;

            for (int i = 0; i < this.Count; i++)
            {
                vertexCoord = Perimeter[i].Coord2;

                if (MathUtils.H2Distance(vertexCoord.X, vertexCoord.Y, x, y) <= 0.2)
                {
                    line1 = Perimeter[i];
                    line2 = Perimeter[(i + 1) % this.Count];

                    return vertexCoord;
                }
            }

            line1 = null;
            line2 = null;

            return Coordinate.NullCoordinate;
        }

        #endregion

    }
}
