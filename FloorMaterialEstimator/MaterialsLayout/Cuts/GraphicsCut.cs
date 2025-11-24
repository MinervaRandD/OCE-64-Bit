#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: ParentGraphicsCut.cs. Project: MaterialsLayout. Created: 11/14/2024         */
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

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

using SettingsLib;

namespace MaterialsLayout
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Graphics;
    using Geometry;
    using Utilities;
    using global::MaterialsLayout.MaterialsLayout;
    using VoronoiLib;
    using FinishesLib;

    #region Key Elements

    public class GraphicsCut: Cut, IGraphicsShape
    {

        private GraphicsRollout _parentGraphicsRollout = null;

        public GraphicsRollout ParentGraphicsRollout
        {
            get
            {
                if (_parentGraphicsRollout == null)
                {
                    throw new InvalidOperationException();
                }

                return _parentGraphicsRollout;
            }

            set
            {
                if (_parentGraphicsRollout == value)
                {
                    return;
                }

                _parentGraphicsRollout = value;
            }
        }

        FinishesLibElements _finishesLibElements = null;

        public FinishesLibElements FinishesLibElements
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

                // Propagate Changes
                foreach (GraphicsOverage graphicsOverage in this.OverageList)
                {
                    graphicsOverage.FinishesLibElements = _finishesLibElements;
                }
            }
        }

        #endregion

        #region Constructors and Cloners

        public GraphicsCut(GraphicsRollout parentArea, GraphicsWindow window, GraphicsPage page, string guid, FinishesLibElements finishLibElements)
        {
            Window = window;

            Page = page;

            Guid = guid;

            this.FinishesLibElements = finishLibElements;

            this.ParentGraphicsRollout = parentArea;

            setupCommon();
        }

        public GraphicsCut(GraphicsRollout parentArea, GraphicsWindow window, GraphicsPage page, FinishesLibElements finishesLibElements)
        {
            this.Window = window;
            this.Page = page;

            this.FinishesLibElements = finishesLibElements;

            this.ParentGraphicsRollout = parentArea;

            setupCommon();
        }

        public GraphicsCut Clone()
        {
            GraphicsCut clonedCut = new GraphicsCut(this.ParentGraphicsRollout, Window, Page, this.FinishesLibElements);

            CutPolygonList = new List<DirectedPolygon>();

            CutPolygonList.AddRange(this.CutPolygonList.Select(p => p.Clone()));

            CutAngle = this.CutAngle;

            CutOffset = this.CutOffset;

            Tag = this.Tag;

            if (graphicsOverageList != null)
            {
                clonedCut.graphicsOverageList = new List<GraphicsOverage>();

                graphicsOverageList.ForEach(o => clonedCut.graphicsOverageList.Add(new GraphicsOverage(this, Window, Page, this.FinishesLibElements, (Overage)o)));
            }

            return clonedCut;
        }

        private void setupCommon()
        {
            GlobalSettings.CutIndexFontInPtsChanged += GlobalSettings_CutIndexFontInPtsChanged;
            GlobalSettings.CutIndexCircleRadiusChanged += GlobalSettings_CutIndexCircleRadiusChanged;
            GlobalSettings.ShowCutIndexCirclesChanged += GlobalSettings_ShowCutIndexCirclesChanged; ;
        }

        #endregion

        #region Deleters and Destructors

        public void Delete()
        {
            GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 90);

            if (Utilities.IsNotNull(graphicsOverageList))
            {
                graphicsOverageList.ForEach(o => o.Delete());
            }

            GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 91);

            if (Utilities.IsNotNull(GraphicsRemnantCutList))
            {
                GraphicsRemnantCutList.ForEach(r => r.Delete());
            }

            GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 92);

            Page.RemoveFromPageShapeDict(this.Shape);

            if (Utilities.IsNotNull(Shape))
            {
                GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 94);

                Page.RemoveFromPageShapeDict(Shape);

                GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 95);

                Shape.Delete();

                GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 96);

                Shape = null;

                GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 97);
            }

            GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 93);

            if (Utilities.IsNotNull(GraphicsCutIndex))
            {
                GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 100);

                Page.RemoveFromPageShapeDict(GraphicsCutIndex.Shape);

                GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 101);

                GraphicsCutIndex.Delete();

                GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 102);
            }

            deleteCommon();

        }

        private void deleteCommon()
        {
            GlobalSettings.CutIndexFontInPtsChanged -= GlobalSettings_CutIndexFontInPtsChanged;
            GlobalSettings.CutIndexCircleRadiusChanged -= GlobalSettings_CutIndexCircleRadiusChanged;
            GlobalSettings.ShowCutIndexCirclesChanged -= GlobalSettings_ShowCutIndexCirclesChanged;
        }

        #endregion

        public string Guid { get; set; }

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

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

        private uint _cutIndex = 0;


        public uint CutIndex
        {
            get
            {
                return _cutIndex;
            }

            set
            {
                if (_cutIndex == value)
                {
                    return;
                }

                if (GraphicsCutIndex != null)
                {
                    GraphicsCutIndex.CutIndex = value;
                }

                _cutIndex = value;
            }
        }

        public GraphicsCutIndex GraphicsCutIndex
        {
            get;
            set;
        }

      
        public List<GraphicsRemnantCut> GraphicsRemnantCutList = new List<GraphicsRemnantCut>();

        private List<GraphicsOverage> graphicsOverageList = null;

        public List<GraphicsOverage> GraphicsOverageList
        {
            get
            {
                if (graphicsOverageList is null)
                {
                    graphicsOverageList = new List<GraphicsOverage>();

                    OverageList.ForEach(o => graphicsOverageList.Add(new GraphicsOverage(this, Window, Page, FinishesLibElements, o)));
                }

                return graphicsOverageList;
            }
        }

        public void Add(GraphicsOverage graphicsOverage)
        {
            if (graphicsOverageList is null)
            {
                graphicsOverageList = new List<GraphicsOverage>();
            }

            graphicsOverageList.Add(graphicsOverage);

            OverageList.Add(graphicsOverage);
        }
        
        public List<GraphicsDirectedPolygon> GraphicsCutPolygonList
        {
            get
            {
                List<GraphicsDirectedPolygon> rtrnList = new List<GraphicsDirectedPolygon>();

                foreach (DirectedPolygon cutPolygon in CutPolygonList)
                {
                    rtrnList.Add(new GraphicsDirectedPolygon(Window, Page, cutPolygon));
                }

                return rtrnList;
            }

            set
            {
                CutPolygonList = new List<DirectedPolygon>();

                foreach (GraphicsDirectedPolygon poly in value)
                {
                    CutPolygonList.Add((DirectedPolygon)poly);
                }
            }
        }

        private void GlobalSettings_ShowCutIndexCirclesChanged(bool showCutIndexCircles)
        {
            if (GraphicsCutIndex is null)
            {
                return;
            }

            if (GraphicsCutIndex.Shape is null)
            {
                return;
            }

            GraphicsCutIndex.SetCircleVisibility();
        }

        private void GlobalSettings_CutIndexCircleRadiusChanged(double cutIndexCircleRadius)
        {
            if (GraphicsCutIndex is null)
            {
                return;
            }

            if (GraphicsCutIndex.Shape is null)
            {
                return;
            }

            VisioInterop.SetShapeSize(GraphicsCutIndex.Shape, 2.0 * cutIndexCircleRadius, 2.0 * cutIndexCircleRadius);
        }

        private void GlobalSettings_CutIndexFontInPtsChanged(double cutIndexFontInPts)
        {
            if (GraphicsCutIndex is null)
            {
                return;
            }

            if (GraphicsCutIndex.Shape is null)
            {
                return;
            }

            VisioInterop.SetTextFontSize(GraphicsCutIndex.Shape, (int) Math.Round(GlobalSettings.CutIndexFontInPts));
        }


        public GraphicShape Shape
        {
            get;
            set;
        }

        public ShapeType ShapeType
        {
            get
            {
                return ShapeType.Polygon;
            }
        }

        public void Rotate(double theta)
        {
            double[,] rotationMatrix = new double[2, 2];
            
            rotationMatrix[0, 0] = Math.Cos(theta);
            rotationMatrix[0, 1] = -Math.Sin(theta);
            rotationMatrix[1, 0] = Math.Sin(theta);
            rotationMatrix[1, 1] = Math.Cos(theta);

            Rotate(rotationMatrix, theta);
        }

        public GraphicShape DrawBoundingRectangle(GraphicsWindow window, GraphicsPage page, Color cutPenColor, Color cutFillColor, double lineWidthInPts)
        {
            //if (Cut is null)
            //{
            //    return null;
            //}

            Geometry.Rectangle boundingRectangle = CutRectangle;

            DirectedPolygon boundingPolygon = (DirectedPolygon)boundingRectangle;

            GraphicsDirectedPolygon graphicsBoundingPolygon = new GraphicsDirectedPolygon(window, page, boundingPolygon);

            GraphicShape boundingRectangleShape = graphicsBoundingPolygon.Draw(cutPenColor, cutFillColor);

            boundingRectangleShape.SetLineWidth(lineWidthInPts);

            return boundingRectangleShape;
        }

        public GraphicShape DrawBoundingRectangleAndIndex(GraphicsLayerBase cutIndexLayer, Color cutPenColor, Color cutFillColor, double lineWidthInPts)
        {
            Geometry.Rectangle boundingRectangle = CutRectangle;

            DirectedPolygon boundingPolygon = (DirectedPolygon)boundingRectangle;

            GraphicsDirectedPolygon graphicsBoundingPolygon = new GraphicsDirectedPolygon(this.Window, this.Page, boundingPolygon);

            GraphicShape boundingRectangleShape = graphicsBoundingPolygon.Draw(cutPenColor, cutFillColor);

            boundingRectangleShape.SetLineWidth(lineWidthInPts);

            if (GraphicsCutIndex is null)
            {
                GraphicsCutIndex = new GraphicsCutIndex(this.Window, this.Page, cutIndexLayer);

                // The voronoi runner does not seem to work correctly in this environment. Should be fixed at some point
                // in the future. Using Centroid in the mean time.

                DirectedPolygon dp = (DirectedPolygon)this.ParentGraphicsRollout.ParentGraphicsLayoutArea.ExternalArea;

                GraphicsCutIndex.Location = dp.Intersect(boundingPolygon)[0].Centroid(); ;
            }

            GraphicShape shape = GraphicsCutIndex.Draw();

            Page.AddToPageShapeDict(shape);

            if (Utilities.IsNotNull(cutIndexLayer))
            {
                 cutIndexLayer.AddShape(GraphicsCutIndex.Shape, 1);
            }

            GraphicsCutIndex.Shape.Lock();

            return boundingRectangleShape;
        }

        internal Dictionary<string, GraphicShape> GenerateShapeDict()
        {
            Dictionary<string, GraphicShape> rtrnDict = new Dictionary<string, GraphicShape>();

            GraphicShape shape = this.Shape;

            if (shape == null)
            {
                return rtrnDict;
            }

            if (shape.VisioShape == null)
            {
                return rtrnDict;
            }

            foreach(GraphicsDirectedPolygon graphicsDirectedPolygon in this.GraphicsCutPolygonList)
            {
                if (graphicsDirectedPolygon.Shape == null)
                {
                    continue;
                }

                if (graphicsDirectedPolygon.Shape.VisioShape == null)
                {
                    continue;
                }

                foreach (KeyValuePair<string, GraphicShape> kvp in graphicsDirectedPolygon.GenerateShapeDict())
                {
                    if (!rtrnDict.ContainsKey(kvp.Key))
                    {
                        rtrnDict[kvp.Key] = kvp.Value;
                    }
                }
            }

            foreach (GraphicsRemnantCut graphicsRemnantCut in GraphicsRemnantCutList)
            {
                GraphicShape graphicsRemnantCutShape = graphicsRemnantCut.Shape;

                if (graphicsRemnantCutShape == null)
                {
                    continue;
                }

                if (graphicsRemnantCutShape.VisioShape == null)
                {
                    continue;
                }

                if (!rtrnDict.ContainsKey(graphicsRemnantCutShape.Guid))
                {
                    rtrnDict[graphicsRemnantCutShape.Guid] = graphicsRemnantCutShape;
                }
            }
             
            foreach (GraphicsOverage graphicsOverage in this.graphicsOverageList)
            {
                if (graphicsOverage.Shape == null)
                {
                    continue;
                }

                if (graphicsOverage.Shape.VisioShape == null)
                {
                    continue;
                }

                foreach (KeyValuePair<string, GraphicShape> kvp in graphicsOverage.GenerateShapeDict())
                {
                    if (!rtrnDict.ContainsKey(kvp.Key))
                    {
                        rtrnDict[kvp.Key] = kvp.Value;
                    }
                }
            }

            if (this.GraphicsCutIndex != null)
            {
                GraphicShape graphicsCutIndexShape = this.GraphicsCutIndex.Shape;

                if (graphicsCutIndexShape != null)
                {
                    if (graphicsCutIndexShape.VisioShape != null)
                    {
                        if (!rtrnDict.ContainsKey(graphicsCutIndexShape.Guid))
                        {
                            rtrnDict[graphicsCutIndexShape.Guid] = graphicsCutIndexShape;
                        }
                    }
                }
            }

            if (!rtrnDict.ContainsKey(Shape.Guid))
            {
                rtrnDict[shape.Guid] = shape;
            }

            return rtrnDict;
        }

        public static explicit operator GraphicShape(GraphicsCut graphicsCut)
        {
            return graphicsCut.Shape;
        }
        //public void SetVisibility(bool visible)
        //{
        //    if (Shape is null)
        //    {
        //        return;
        //    }

        //    VisioInterop.SetShapeLineVisibility(Shape, visible);

        //    if (Utilities.IsNotNull(GraphicsCutIndex))
        //    {
        //        GraphicsCutIndex.SetVisibility(visible);
        //    }
        //}
    }
}
