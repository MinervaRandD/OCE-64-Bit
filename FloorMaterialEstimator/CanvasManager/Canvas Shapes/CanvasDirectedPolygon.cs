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

namespace CanvasManager.CanvasShapes
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
    using global::CanvasManager.FinishManager;
    using CanvasManagerLib.FinishManager;

    public class CanvasDirectedPolygon : GraphicsDirectedPolygon, IAreaShape
    {
        // public string Guid { get; set; }

        public CanvasPage CurrentPage
        {
            get;
            set;
        }

        public new GraphicShape Shape
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
        public GraphicsLayerBase GraphicsLayer
        {
            get
            {
                return Shape.SingleGraphicsLayer;
            }

            set
            {
                if (Shape is null)
                {
                    throw new NotImplementedException();
                }

                Shape.AddToLayerSet(value);

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

                        LineFinishManager lineFinishManager = FinishManagerGlobals.LineFinishManagerList[graphicsDirectedLine.Guid];

                        perimeter.Add(new CanvasDirectedLine(Window, Page, lineFinishManager, graphicsDirectedLine, SystemState.DesignState));
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
      
        public CanvasLayoutArea ParentLayoutArea { get; set; }

        #region Constructors

        public CanvasDirectedPolygon(GraphicsWindow window, GraphicsPage page): base(window, page)
        {
            this.Guid = GuidMaintenance.CreateGuid(this);
        }

        //public CanvasDirectedPolygon(
        //    CanvasManager canvasManager,
        //    List<CanvasDirectedLine> directedLineList) : base(canvasManager.Window, CanvasPage.CurrentPage)
        //{
        //    this.Guid = GuidMaintenance.CreateGuid(this);

        //    this.canvasManager = canvasManager;

        //    this.Perimeter = directedLineList;

        //    base.PerimeterLineAdded += CanvasDirectedPolygon_PerimeterLineAdded;
        //}

        public void AddAssociatedLines(GraphicsWindow window, GraphicsPage page, string lineName)
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

                CurrentPage.AddToDirectedLineDict(lineClonedCanvasDirectedLine);

                CurrentPage.LineHistoryList.Add(lineClonedCanvasDirectedLine);

                canvasDirectedLine.AssociatedDirectedLine = lineClonedCanvasDirectedLine;

                lineClonedCanvasDirectedLine.AssociatedDirectedLine = canvasDirectedLine;
            }
        }

        public CanvasDirectedPolygon(
            GraphicsWindow window
            , GraphicsPage page
            , string guid
            , List<CanvasDirectedLine> directedLineList): base(window, page)
        {
            this.Window = window;

            this.Page = page;

            this.Guid = guid;

            this.Perimeter = directedLineList;

            base.PerimeterLineAdded += CanvasDirectedPolygon_PerimeterLineAdded;
        }

        public CanvasDirectedPolygon(
            GraphicsWindow window
            , GraphicsPage page
            , CanvasDirectedPolyline canvasDirectedPolyline)
            : base(window, page)
        {
            this.Guid = GuidMaintenance.CreateGuid(this);

            this.canvasDirectedPolyline = canvasDirectedPolyline;

            List<CanvasDirectedLine> perimeterList = new List<CanvasDirectedLine>();
            
            canvasDirectedPolyline.ForEach(l => { perimeterList.Add(l); l.ParentPolygon = this;  CurrentPage.AddToDirectedLineDict(l); });

            this.Perimeter = perimeterList;

            this.PolygonInternalArea = canvasDirectedPolyline.PolygonInternalArea;

            base.PerimeterLineAdded += CanvasDirectedPolygon_PerimeterLineAdded;
        }

        public CanvasDirectedPolygon(
            GraphicsWindow window
            , GraphicsPage page
            , GraphicsDirectedPolygon graphicsDirectedPolygon
            , UCLineFinishPaletteElement ucLine
            , DesignState designState)
            : this(window, page)
        {
            this.Window = window;

            this.Page = page;

            List<CanvasDirectedLine> perimeterList = new List<CanvasDirectedLine>();

            foreach (GraphicsDirectedLine line in graphicsDirectedPolygon)
            {
                LineFinishManager lineFinishManager = FinishManagerGlobals.LineFinishManagerList[ucLine.Guid];

                CanvasDirectedLine l = new CanvasDirectedLine(window, page, lineFinishManager, line, designState,  false);

                l.ucLine = ucLine;

                l.ParentPolygon = this;

                perimeterList.Add(l);

                this.Add(l);


                if (!string.IsNullOrEmpty(l.Guid))
                {
                    CurrentPage.AddToDirectedLineDict(l);
                }
            }

            this.Perimeter = perimeterList;

            this.Guid = GuidMaintenance.CreateGuid(this);

            base.PerimeterLineAdded += CanvasDirectedPolygon_PerimeterLineAdded;
            Debug.Assert(AllLinesAssignedToUCLineElement());
        }

        public CanvasDirectedPolygon(
            GraphicsWindow window
            , GraphicsPage page
            , LineFinishManager lineFinishManager
            , DirectedPolygon polygon
            , DesignState designState)
            : base(window, page)
        {           
            foreach (DirectedLine directedLine in polygon)
            {
                GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(window, page, directedLine, LineRole.ExternalPerimeter);

                CanvasDirectedLine canvasDirectedLine = new CanvasDirectedLine(window, page, lineFinishManager, graphicsDirectedLine, SystemState.DesignState);

                PerimeterAdd(canvasDirectedLine);
            }


            base.PerimeterLineAdded += CanvasDirectedPolygon_PerimeterLineAdded;
        }

        #endregion

        private void CanvasDirectedPolygon_PerimeterLineAdded(GraphicsDirectedLine graphicsDirectedLine)
        {
            if (perimeter is null)
            {
                perimeter = new List<CanvasDirectedLine>();
            }

            LineFinishManager lineFinishManager = FinishManagerGlobals.LineFinishManagerList[graphicsDirectedLine.Guid];

            perimeter.Add(new CanvasDirectedLine(Window, Page, lineFinishManager, graphicsDirectedLine, SystemState.DesignState));
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

        public Coordinate GetMaxCoord()
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

        public void GetBounds(out double minX, out double maxX, out double minY, out double maxY)
        {
            minX = this.Min(l => Math.Min(l.Coord1.X, l.Coord2.X));
            maxX = this.Max(l => Math.Max(l.Coord1.X, l.Coord2.X));
            minY = this.Min(l => Math.Min(l.Coord1.Y, l.Coord2.Y));
            maxY = this.Max(l => Math.Max(l.Coord1.Y, l.Coord2.Y));
        }

        public bool IntersectsBoundaries(CanvasLayoutArea canvasLayoutArea)
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

        public void BringToFront()
        {
            PolygonInternalArea.BringToFront();

            Perimeter.ForEach(l => l.Shape.BringToFront());
        }

        public new double NetAreaInSqrInches()
        {
            return base.AreaInSqrInches(Page.DrawingScaleInInches);// * Math.Pow(GraphicsPage.DrawingScaleInInches, 2.0);
        }

        //public UCFinish ucFinish { get; set; }

        public CanvasDirectedLine RemoveLastLine()
        {
            if (Count <= 0)
            {
                return null;
            }

            CanvasDirectedLine returnLine = Perimeter[Count - 1];

            base.RemoveAt(Count - 1);

            return returnLine;
        }

        public void MakeDisappear()
        {
            VisioInterop.SetFillPattern(this.PolygonInternalArea, "0");

            Perimeter.ForEach(l => l.MakeDisappear());

            //VisioInterop.SendToBack(this.PolygonInternalArea);

            //VisioInterop.SetShapeVisibility(this.Shape, false);
        }

        public void MakeReappear()
        {
            VisioInterop.SetFillPattern(this.PolygonInternalArea, "1");

            Perimeter.ForEach(l => l.MakeReappear());
        }

        public Coordinate GetFirstCoordinate()
        {
            if (Count <= 0)
            {
                throw new NotImplementedException();
            }

            return Perimeter[0].GetLineStartpoint();
        }

        public Coordinate GetLastCoordinate()
        {
            if (Count <= 0)
            {
                throw new NotImplementedException();
            }

            return Perimeter[Count - 1].GetLineEndpoint();
        }

        public void SetDesignStateSelectedLineGraphics()
        {
            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                canvasDirectedLine.SetDesignStateSelectedLineGraphics();
            }
        }

        public void RemoveFromSystem()
        {
            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                canvasDirectedLine.RemoveFromSystem();
            }
        }

        public void Delete(bool deleteAssociatedLines = false)
        {            
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
                        CurrentPage.LineHistoryList.Remove(canvasDirectedLine.AssociatedDirectedLine);

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

            if (Page.PageShapeDictContains(this.PolygonInternalArea.Guid))
            {
                Page.RemoveFromPageShapeDict(this.PolygonInternalArea.Guid);
            }
        }

        public void Redraw(DesignState lineArea, AreaShapeBuildStatus areaBuildStatus)
        {
            CreateInternalAreaShape(this.Guid);

            foreach (CanvasDirectedLine line in this)
            {
                line.Redraw(lineArea, false, areaBuildStatus);
            }
        }

        public void Undraw()
        {
     
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

        public GraphicShape CreateInternalAreaShape()
        {
            double[] coordinateArray = GetCoordinateArray();

            this.Guid = GuidMaintenance.CreateGuid(this);

            Shape = CurrentPage.DrawPolygon(this, coordinateArray, this.Guid);

            Shape.SetShapeData1("Internal Area");

            Shape.SetShapeData3(this.Guid);

            PolygonInternalArea.BringToFront();

            return Shape;
        }

        public GraphicShape CreateInternalAreaShape(string guid)
        {
            double[] coordinateArray = GetCoordinateArray();

            Shape = CurrentPage.DrawPolygon(this, coordinateArray, guid);

            PolygonInternalArea.BringToFront();

            return Shape;
        }

        public GraphicShape CreateInternalAreaShape(GraphicsPage page)
        {
            double[] coordinateArray = GetCoordinateArray();

            //this.Guid = GuidMaintenance.CreateGuid(this);

            Shape = CurrentPage.DrawPolygon(this, coordinateArray, this.Guid);

            PolygonInternalArea = Shape;

            PolygonInternalArea.ShowShapeOutline(false);

            PolygonInternalArea.BringToFront();

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

        public GraphicShape Draw(GraphicsPage page, Color color, bool drawPolygonOnly = false)
        {
            double[] coordinates = GetCoordinateArray();

            Shape = page.DrawPolyline(this, coordinates, 1);

            VisioInterop.SetBaseFillColor(Shape, color);

            if (!drawPolygonOnly)
            {
                foreach (CanvasDirectedLine canvasDirectedLine in this)
                {
                    canvasDirectedLine.Draw();
                }
            }
            
            VisioInterop.SetShapeData(Shape, "[Layout Area]", "Compound Shape[" + UCAreaFinish.AreaName + "]", Shape.Guid);

            return Shape;
        }
      
        public new CanvasDirectedPolygon Clone()
        {
            CanvasDirectedPolygon clonedCanvasDirectedPolygon
                = new CanvasDirectedPolygon(
                      this.Window
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
                        window
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

        public void BringPerimeterToFront()
        {
            foreach (CanvasDirectedLine line in Perimeter)
            {
                if (Utilities.IsNotNull(line.Shape))
                {
                    line.Shape.BringToFront();
                }
            }
        }

        public void AddToLayer(GraphicsLayer graphicsLayer)
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

        public void RemoveFromLayer(GraphicsLayer graphicsLayer)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { graphicsLayer });

            try
            {
                graphicsLayer.RemoveShape(Shape);

                foreach (CanvasDirectedLine canvasDirectedLine in this.Perimeter)
                {
                    graphicsLayer.RemoveShape(canvasDirectedLine.Shape);
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("CanvasDirectedPolygon:RemoveFromLayer throws an exception.", ex, 1, true);
            }
        }

        public void Lock()
        {
            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                canvasDirectedLine.Shape.Lock();
            }
        }

        public void Unlock()
        {
            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                canvasDirectedLine.Shape.Unlock();
            }
        }

        public bool IsLocked()
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

        public Coordinate GetSelectedVertex(double x, double y, out CanvasDirectedLine line1, out CanvasDirectedLine line2)
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

        internal Dictionary<string, GraphicShape> GenerateGraphicShapeDict()
        {
            Dictionary<string, GraphicShape> rtrnDict = new Dictionary<string, GraphicShape>();

            foreach (CanvasDirectedLine canvasDirectedLine in this.perimeter)
            {
                if (canvasDirectedLine.Shape == null)
                {
                    continue;
                }

                if (canvasDirectedLine.Shape.VisioShape == null)
                {
                    continue;
                }

                rtrnDict.Add(canvasDirectedLine.Guid, canvasDirectedLine.Shape);
            }

            return rtrnDict;
        }

        #endregion

    }
}
