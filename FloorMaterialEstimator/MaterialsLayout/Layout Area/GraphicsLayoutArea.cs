#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: ParentGraphicsLayoutArea.cs. Project: MaterialsLayout. Created: 11/10/2024         */
/*                                                                                                     */
/* Copyright (c) 2025, Minerva Research and Development, LLC. All rights reserved.                     */
/*                                                                                                     */
/* Not to be copied or distributed in any way without prior authorization. If provided with permission,*/
/* this software is provided without warranty of any kind, express or implied,                         */
/* including but not limited to the warranties of merchantability, fitness for a particular            */
/* purpose, and non-infringement. In no event shall the authors or copyright holders be liable         */
/* for any claim, damages, or other liability, whether in an action of contract, tort, or              */
/* otherwise, arising from, out of, or in connection with the software or the use or other             */
/* dealings in the software.                                                                           */
/*                                                                                                     */
/* Author: Marc Diamond, Minerva Research and Development, LLC                                         */
/*                                                                                                     */
/*******************************************************************************************************/
#endregion


using Globals;
using Microsoft.Office.Interop.Visio;

namespace MaterialsLayout
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    using Geometry;
    using Graphics;
    using Utilities;
    using SettingsLib;
    using TracerLib;
    using FinishesLib;

    public class GraphicsLayoutArea  : LayoutArea, IGraphicsShape
    {

        #region Key Elements


        private FinishesLibElements _finishesLibElements = null;

        public FinishesLibElements FinishLibElements
        {
            get
            {
                if (_finishesLibElements == null)
                {
                    throw new InvalidOperationException();
                }

                return _finishesLibElements;
            }

            set
            {
                if (value == _finishesLibElements)
                {
                    return;
                }

                _finishesLibElements = value;

                foreach (GraphicsRollout graphicsRollout in this.GraphicsRolloutList)
                {
                    graphicsRollout.FinishesLibElements = _finishesLibElements;
                }
            }
        }

        #endregion

        private GraphicShape _shape = null;

        public GraphicShape Shape
        {
            get
            { 
                return _shape;
            }

            set
            {
                if (value == _shape)
                {
                    return;
                }

                if (_shape != null)
                {
                    _shape.Delete();
                }

                _shape = value;
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

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public string Guid
        {
            get;
            set;
        }

        public GraphicsPage Page
        {
            get; set;
        }

        public GraphicsWindow Window
        {
            get; set;
        }

        private new GraphicsDirectedPolygon _externalArea = null;

        public new GraphicsDirectedPolygon ExternalArea
        {
            get
            {
                return _externalArea;
            }

            set
            {
                _externalArea = value;
                base.ExternalArea = (DirectedPolygon)value;
            }
        }

        public bool IsZeroAreaLayoutArea
        {
            get
            {
                return LayoutAreaType == LayoutAreaType.ColorOnly;
            }

            set
            {
                if (value)
                {
                    LayoutAreaType = LayoutAreaType.ColorOnly;
                }
            }
        }
      
        public bool IsSubdivided()
        {
            return OffspringAreas.Count > 0;
        }

        public bool IsSeamed()
        {
            return this.SeamList.Count > 0;
        }

        private List<GraphicsDirectedPolygon> internalAreas
        {
            get;
            set;
        }

        public new List<GraphicsDirectedPolygon> InternalAreas
        {
            get
            {
                return internalAreas;
            }

            set
            {
                internalAreas = value;
                base.InternalAreas = internalAreas.Select(i => (DirectedPolygon)i).ToList();
            }
        }

        public double OversGeneratorOversWidthInInches { get; set; }

        //public LayoutAreaType LayoutAreaType { get; set; }  = LayoutAreaType.Normal;

        public List<GraphicsOverage> EmbeddedOversList = new List<GraphicsOverage>();

        public void InternalAreasAdd(GraphicsDirectedPolygon graphicsDirectedPolygon)
        {
            if (InternalAreas == null)
            {
                InternalAreas = new List<GraphicsDirectedPolygon>();
            }

            InternalAreas.Add(graphicsDirectedPolygon);
            
            base.InternalAreasAdd((DirectedPolygon)graphicsDirectedPolygon);
        }

        /// <summary>
        /// Get the point on either the external or internal boundaries closest to the coordinate
        /// </summary>
        /// <param name="targetCoord">The target coordinate to match</param>
        /// <returns>Returns the coordinate of the closest line.</returns>
       
        public Coordinate GetNearestPointToBoundary(Coordinate targetCoord)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { targetCoord });

            if (Coordinate.IsNullCoordinate(targetCoord))
            {
                Tracer.TraceGen.TraceError("ParentGraphicsLayoutArea:GetNearestPointToBoundary is called with a null targetCoord.", 1, true);

                return Coordinate.NullCoordinate;
            }

            if (ExternalArea is null)
            {
                Tracer.TraceGen.TraceError("ParentGraphicsLayoutArea:GetNearestPointToBoundary is called with a null ExternalArea.", 1, true);

                return Coordinate.NullCoordinate;
            }

            Coordinate minCoord = this.ExternalArea.GetNearestPointToBoundary(targetCoord);

            double minDistance = Coordinate.H2Distance(minCoord, targetCoord);

            foreach (GraphicsDirectedPolygon internalArea in this.InternalAreas)
            {
                Coordinate nxtCoord = internalArea.GetNearestPointToBoundary(targetCoord);

                double nxtDistance = Coordinate.H2Distance(nxtCoord, targetCoord);

                if (nxtDistance < minDistance)
                {
                    minCoord = nxtCoord;
                    minDistance = nxtDistance;
                }
            }

            return minCoord;
        }

        public bool IsZeroLayoutArea
        {
            get
            {
                return LayoutAreaType == LayoutAreaType.ColorOnly;
            }

            set
            {
                if (value)
                {
                    LayoutAreaType = LayoutAreaType.ColorOnly;
                }
            }
        }

        private string _parentAreaGuid = null;

        public string ParentAreaGuid
        {
            get
            {
                if (this.Guid == _parentAreaGuid)
                {
                    ;
                }


                return _parentAreaGuid;
            }

            set
            {
                if (value != null)
                {
                    if (this.Guid == value)
                    {
                        ;
                    }
                }

                if (_parentAreaGuid == value)
                {
                    return;
                }

                _parentAreaGuid = value;
            }
        }

        private GraphicsLayoutArea _parentArea = null;

        public GraphicsLayoutArea ParentArea
        {
            get
            {
                return _parentArea;
            }

            set
            {
                if (_parentArea == value)
                {
                    return;
                }

                _parentArea = value;
            }
        }

        public List<GraphicsLayoutArea> OffspringAreas = new List<GraphicsLayoutArea>();

        public int SeamAreaIndex
        {
            get;
            set;
        }
        
    

        public List<string> OffspringAreaGuidList { get; set; } = new List<string>();

        public double AreaInSqrInches()
        {
            if (LayoutAreaType == LayoutAreaType.OversGenerator || LayoutAreaType == LayoutAreaType.ColorOnly)
            {
                return 0;
            }

            double area = ExternalArea.AreaInSqrInches();

            area -= InternalAreas.Sum(s => s.AreaInSqrInches());

            return area;
        }

        private ShapeType _shapeType = ShapeType.Unknown;

        public ShapeType ShapeType
        {
            get
            {
                if (Shape != null)
                {
                    return Shape.ShapeType;
                }

                else
                {
                    return _shapeType;
                }
            }

            set
            {
                _shapeType = value;

                if (Shape != null)
                {
                    Shape.ShapeType = value;
                }
            }

        }

        public CoordinatedList<GraphicsSeam> GraphicsSeamList { get; set; }  = new CoordinatedList<GraphicsSeam>();

        public CoordinatedList<GraphicsSeam> GraphicsDisplaySeamList { get; set; } = new CoordinatedList<GraphicsSeam>();

        public CoordinatedList<GraphicsRollout> GraphicsRolloutList = new CoordinatedList<GraphicsRollout>();

        public List<GraphicsCut> GraphicsCutList
        {
            get
            {
                List<GraphicsCut> rtrnList = new List<GraphicsCut>();

                GraphicsRolloutList.ForEach(r => rtrnList.AddRange(r.GraphicsCutList));

                return rtrnList;
            }
        }

        public List<GraphicsOverage> GraphicsOverageList
        {
            get
            {
                List<GraphicsOverage> rtrnList = new List<GraphicsOverage>();

                GraphicsRolloutList.ForEach(r => rtrnList.AddRange(r.GraphicsOverageList));

                return rtrnList;
            }
        }

        public List<GraphicsUndrage> GraphicsUndrageList
        {
            get
            {
                List<GraphicsUndrage> rtrnList = new List<GraphicsUndrage>();

                GraphicsRolloutList.ForEach(r => rtrnList.AddRange(r.GraphicsUndrageList));

                return rtrnList;
            }
        }

        
        #region Constructors

        public GraphicsLayoutArea(
            GraphicsWindow window
            ,GraphicsPage page
            ,FinishesLibElements finishLibElements
            ,GraphicsDirectedPolygon externalArea
            ,List<GraphicsDirectedPolygon> internalAreas)
        {

            this.Window = window;

            this.Page = page;

            this.ExternalArea = externalArea;
            this.InternalAreas = internalAreas;

            this.Guid = externalArea.Guid;

            this.FinishLibElements = finishLibElements;

            setupSeamListCoordination();
            setupRolloutListCoordination();
        }


        public GraphicsLayoutArea(
            GraphicsWindow window
            , GraphicsPage page
            , FinishesLibElements finishLibElements)
        {
            this.Window = window;

            this.Page = page;

            this.ExternalArea = null;
            this.InternalAreas = new List<GraphicsDirectedPolygon>();

            this.Guid = GuidMaintenance.CreateGuid(this);

            this.FinishLibElements = finishLibElements;

            setupSeamListCoordination();
            setupRolloutListCoordination();
        }
       
        internal GraphicsLayoutArea Clone(
            GraphicsWindow window
            , GraphicsPage page
            , FinishesLibElements finishLibElements)
        {
            GraphicsDirectedPolygon clonedPolygon = null;

            if (this.ExternalArea != null)
            {
                clonedPolygon = new GraphicsDirectedPolygon(window, page, this.ExternalArea);
            }

            List<GraphicsDirectedPolygon> clonedInternalAreas = new List<GraphicsDirectedPolygon>();

            if (this.internalAreas != null)
            {
                foreach (var polygon in this.internalAreas)
                {
                    internalAreas.Add(polygon.Clone());
                }
            }
            GraphicsLayoutArea clonedLayoutArea = new GraphicsLayoutArea(window, page, finishLibElements, clonedPolygon, clonedInternalAreas)
            {
                AreaFinishLayers = this.AreaFinishLayers
            };

            return clonedLayoutArea;
        }


        #endregion

        private static int layerCount = 0;



        public GraphicsLayoutArea ShapeToGraphicsLayoutArea(GraphicShape shape)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });

            try
            {
                if (shape is null)
                {
                    return null;
                }

                List<DirectedPolygon> directedPolygonList = VisioInterop.GetPathList(shape);

                if (directedPolygonList is null)
                {
                    return null;
                }

                if (directedPolygonList.Count <= 0)
                {
                    return null;
                }

                List<DirectedPolygon> externalPolygonAreaList = new List<DirectedPolygon>();
                List<DirectedPolygon> internalPolygonAreaList = new List<DirectedPolygon>();

                // This assumes that interior areas cannot contain other interior areas.

                for (int i1 = 0; i1 < directedPolygonList.Count; i1++)
                {
                    DirectedPolygon directedPolygon1 = directedPolygonList[i1];

                    for (int i2 = 0; i2 < directedPolygonList.Count; i2++)
                    {

                        if (i1 == i2)
                        {
                            continue;
                        }

                        DirectedPolygon directedPolygon2 = directedPolygonList[i2];

                        if (directedPolygon1.Contains(directedPolygon2))
                        {
                            internalPolygonAreaList.Add(directedPolygon2);
                        }
                    }
                }


                externalPolygonAreaList.AddRange(directedPolygonList);

                foreach (DirectedPolygon directedPolygon in internalPolygonAreaList)
                {
                    externalPolygonAreaList.Remove(directedPolygon);
                }

                if (externalPolygonAreaList.Count > 1)
                {
                    Tracer.TraceGen.TraceError("In ParentGraphicsLayoutArea:ShapeToGraphicsLayoutArea: Currently unable to handle cases with more than one external area.", 1, true);

                    return null;
                }

                if (externalPolygonAreaList.Count == 0)
                {
                    Tracer.TraceGen.TraceError("In ParentGraphicsLayoutArea:ShapeToGraphicsLayoutArea: No external areas found.", 1, true);

                    return null;
                }


                GraphicsDirectedPolygon externalAreaPolygon = new GraphicsDirectedPolygon(Window, Page, externalPolygonAreaList[0]);

                List<GraphicsDirectedPolygon> graphicsDirectedPolygonList = new List<GraphicsDirectedPolygon>();

                graphicsDirectedPolygonList.AddRange(internalPolygonAreaList.Select(p => new GraphicsDirectedPolygon(Window, Page, p)));

                GraphicsLayoutArea GraphicsLayoutArea = new GraphicsLayoutArea(Window, Page, this.FinishLibElements, externalAreaPolygon, graphicsDirectedPolygonList);

                return GraphicsLayoutArea;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("ParentGraphicsLayoutArea:ParentGraphicsLayoutArea throws an exception", ex, 1, true);

                return null;
            }
        }

        private void setupSeamListCoordination()
        {
            base.SeamList.ItemAdded += SeamList_ItemAdded;
            base.SeamList.ItemRemoved += SeamList_ItemRemoved;
            base.SeamList.ListCleared += SeamList_ListCleared;
        }


        private void SeamList_ItemAdded(Seam seam)
        {
            GraphicsSeamList.Add(new GraphicsSeam(Window, Page, (Seam) seam));
        }

        private void SeamList_ItemRemoved(Seam seam)
        {
            GraphicsSeam graphicsSeamToRemove = null;

            foreach (GraphicsSeam graphicsSeam in GraphicsSeamList)
            {
                if (graphicsSeam.Seam == seam)
                {
                    graphicsSeamToRemove = graphicsSeam;
                    break;
                }
            }

            if (Utilities.IsNotNull(graphicsSeamToRemove))
            {
                GraphicsSeamList.Remove(graphicsSeamToRemove);
            }
        }

        private void SeamList_ListCleared()
        {
            GraphicsSeamList.Clear();
        }

        private void setupRolloutListCoordination()
        {
            base.RolloutList.ItemAdded += RolloutList_ItemAdded;
            base.RolloutList.ItemRemoved += RolloutList_ItemRemoved;
            base.RolloutList.ListCleared += RolloutList_ListCleared;
        }

        private void RolloutList_ItemAdded(Rollout rollout)
        {
            //GraphicsRollout graphicsRollout = new GraphicsRollout(this, Window, Page, rollout)
            //{
            //    FinishesLibElements = ((GraphicsLayoutArea)this).FinishLibElements
            //};

            //graphicsRollout.GraphicsRollout = graphicsRollout;

            GraphicsRolloutList.Add((GraphicsRollout)rollout);

        }

        private void RolloutList_ItemRemoved(Rollout rollout)
        {
            GraphicsRollout graphicsRolloutToRemove = null;

            foreach (GraphicsRollout graphicsRollout in GraphicsRolloutList)
            {
                if (graphicsRollout == rollout)
                {
                    graphicsRolloutToRemove = graphicsRollout;
                    break;
                }
            }

            if (Utilities.IsNotNull(graphicsRolloutToRemove))
            {
                GraphicsRolloutList.Remove(graphicsRolloutToRemove);
            }
        }

        private void RolloutList_ListCleared()
        {
            GraphicsRolloutList.Clear();
        }


        public void DeleteRollouts(AreaFinishLayers areaFinishLayers)
        {
            if (GraphicsRolloutList is null)
            {
                return;
            }

            foreach (GraphicsRollout rollout in GraphicsRolloutList)
            {
                foreach (GraphicsCut cut in rollout.GraphicsCutList)
                {
                    areaFinishLayers.CutsLayer.RemoveShapeFromLayer(cut.Shape, 1);

                    if (Utilities.IsNotNull(cut.GraphicsCutIndex))
                    {
                        //Page.RemoveFromPageShapeDict(cut.GraphicsCutIndex);

                        areaFinishLayers.CutsIndexLayer.RemoveShapeFromLayer(cut.GraphicsCutIndex.Shape, 1);

                        //cut.GraphicsCutIndex.Delete();
                    }

                    foreach (GraphicsOverage overage in cut.GraphicsOverageList)
                    {
                        areaFinishLayers.OversLayer.RemoveShapeFromLayer(overage.Shape, 1);
                    }
                }

                foreach (GraphicsUndrage undrage in GraphicsUndrageList)
                {
                    undrage.Delete();
                }

                rollout.Delete();
            }

           // GraphicsRolloutList.ForEach(r => r.Delete());

            GraphicsSeamList.ClearBase();
            base.RolloutList.ClearBase();

            this.GraphicsRolloutList.ClearBase();

            SystemState.XtraLgthFeet = null;
            SystemState.XtraLgthInch = null;
        }

        public bool ValidateConsistentPerimeters()
        {
            if (!ExternalArea.ValidateConsistentPerimeter()) return false;

            foreach (var p in InternalAreas)
            {
                if (!p.ValidateConsistentPerimeter()) return false;
            }

            return true;
        }

        public List<GraphicShape> Divide(GraphicsWindow window, GraphicsPage page, List<Coordinate> dividingLineCoordinates)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { dividingLineCoordinates });
#endif

#region Validations

            if (!VisioValidations.ValidateWindowParm(window, "GrapicsLayoutEngine:Divide"))
            {
                return null;
            }

            if (!VisioValidations.ValidatePageParm(page, "VisioGeometryEngine:Divide"))
            {
                return null;
            }

            if (dividingLineCoordinates is null)
            {
                Tracer.TraceGen.TraceError("dividingLineCoordinates is null in call to VisioGeometryEngine:Divide", 1, true);

                return null;
            }

            if (dividingLineCoordinates.Count <= 0)
            {
                Tracer.TraceGen.TraceError("dividingLineCoordinates is empty in call to VisioGeometryEngine:Divide", 1, true);

                return null;
            }

#endregion

            List<GraphicShape> rtrnList = new List<GraphicShape>();

            try
            {
                int coordinateCount = dividingLineCoordinates.Count;

                if (coordinateCount < 2)
                {
                    return rtrnList;
                }

                double[] dividingLinePoints = new double[coordinateCount * 2];

                for (int i = 0; i < coordinateCount; i++)
                {
                    dividingLinePoints[2 * i] = dividingLineCoordinates[i].X;
                    dividingLinePoints[2 * i + 1] = dividingLineCoordinates[i].Y;
                }

                List<GraphicShape> subdividedElementListIn = VisioGeometryEngine.Subdivide(window, page, this.Shape, dividingLinePoints);


                List<GraphicShape> subdividedElementListOut = new List<GraphicShape>();

                foreach (GraphicShape shape in subdividedElementListIn)
                {
                    double[] boundaryPoints = VisioInterop.GetShapeBoundary(shape);

                    if (boundaryPoints is null)
                    {
                        continue;
                    }

                    double[] directedPolygonPoints = new double[boundaryPoints.Length - 2];

                    Array.Copy(boundaryPoints, directedPolygonPoints, boundaryPoints.Length - 2);

                    DirectedPolygon directedPolygon = new DirectedPolygon(directedPolygonPoints);

                    bool excludeShape = false;

                    // Internal areas of the original shape are returned by the fragment command as separate shapes. The following
                    // handles this.

                    foreach (DirectedPolygon internalArea in this.InternalAreas)
                    {
                        if (internalArea.ApproxContains(directedPolygon, 0.1))
                        {
                            if (string.IsNullOrEmpty(shape.VisioShape.Data3))
                            {
                                shape.Delete();
                            }

                            excludeShape = true;

                            break;
                        }
                    }

                    if (excludeShape)
                    {
                        continue;
                    }

                    if (!this.ExternalArea.ApproxContains(directedPolygon, 0.1))
                    {
                        if (string.IsNullOrEmpty(shape.VisioShape.Data3))
                        {
                            shape.Delete();
                        }

                        continue;
                    }
                    //}

                    shape.Guid = GuidMaintenance.CreateGuid(shape);

                    rtrnList.Add(shape);
                }

                return rtrnList;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in ParentGraphicsLayoutArea:Divide", ex, 1, true);

                return rtrnList;
            }
        }


        public void Delete()
        {
            VisioInterop.DeleteShape(this.Shape);
          
            if (ExternalArea != null)
            {
                ExternalArea.Delete();
            }

            if (InternalAreas != null)
            {
                InternalAreas.ForEach(ip => ip.Delete());
            }

            if (GraphicsCutList != null)
            {
                GraphicsCutList.ForEach(gc => gc.Delete());
            }

            if (GraphicsSeamList != null)
            {
                GraphicsSeamList.ForEach(gs => ((GraphicsSeam)gs).Delete());
            }
        }

        public List<string> SummaryRecord()
        {
            List<string> rtrnList = new List<string>();

            rtrnList.Add(this.Guid);

            return rtrnList;
        }

        #region Casts

        public static explicit operator GraphicShape(GraphicsLayoutArea graphicsLayoutArea)
        {
            return graphicsLayoutArea.Shape;
        }

        public static explicit operator GraphicsLayoutArea(GraphicShape graphicShape)
        {
            if (graphicShape == null)
            {
                return null;
            }

            if (!(graphicShape.ShapeType == ShapeType.LayoutArea ||
                  graphicShape.ShapeType == ShapeType.LayoutAreaShape ||
                  graphicShape.ShapeType == ShapeType.Polyline))
            {
                return null;
            }

            return (GraphicsLayoutArea) graphicShape.ParentObject;

        }

        #endregion

    }
}
