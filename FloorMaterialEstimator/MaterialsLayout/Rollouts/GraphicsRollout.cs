#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: ParentGraphicsRollout.cs. Project: MaterialsLayout. Created: 11/14/2024         */
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



namespace MaterialsLayout
{
    using System.Collections.Generic;
    using System.Drawing;

    using Utilities;
    using Geometry;
    using Graphics;
    using System.Xml.Serialization;

    using Rectangle = Geometry.Rectangle;
    using System.Diagnostics;
    using global::MaterialsLayout.MaterialsLayout;
    using System;
    using FinishesLib;

    public class GraphicsRollout: Rollout
    {
        #region Key Elements

        [XmlIgnore]

        private GraphicsLayoutArea _parentGraphicsLayoutArea = null;
        
        [XmlIgnore]
        public GraphicsLayoutArea ParentGraphicsLayoutArea
        {
            get
            {
                if (_parentGraphicsLayoutArea == null)
                {
                    throw new InvalidOperationException();
                }

                return _parentGraphicsLayoutArea;
            }

            set
            {
                if (_parentGraphicsLayoutArea == value)
                {
                    return;
                }

                _parentGraphicsLayoutArea = value;
            }
        }
        private FinishesLibElements _finishesLibElements = null;

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

                // Propagate Changes.

                foreach (GraphicsCut graphicsCut in this.GraphicsCutList)
                {
                    graphicsCut.FinishesLibElements = _finishesLibElements;
                }

                foreach (GraphicsUndrage graphicsUndrage in this.GraphicsUndrageList)
                {
                    graphicsUndrage.FinishesLibElements = _finishesLibElements;
                }
            }
        }

        #endregion

        #region Constructors and Cloners

        public GraphicsRollout(GraphicsLayoutArea parentArea, GraphicsWindow window, GraphicsPage page)
        {
            ParentGraphicsLayoutArea = parentArea;

            this.Window = window;
            this.Page = page;

            this.Guid = GuidMaintenance.CreateGuid(this);
        }


        public GraphicsRollout(GraphicsLayoutArea parentArea, GraphicsWindow window, GraphicsPage page, Rollout rollout)
        {
            Debug.Assert(Utilities.IsNotNull(parentArea));

            this.Window = window;
            this.Page = page;

            ParentGraphicsLayoutArea = parentArea;

            this.Guid = GuidMaintenance.CreateGuid(this);
        }

        internal GraphicsRollout Clone(GraphicsLayoutArea parentArea, GraphicsWindow window, GraphicsPage page)
        {
            GraphicsRollout clonedGraphicsRollout = new GraphicsRollout(parentArea, window, page)
            {
                FinishesLibElements = FinishesLibElements
                ,RolloutRectangle = RolloutRectangle
                ,SeamWidth = SeamWidth
                ,MaterialOverlap = MaterialOverlap
            };

            clonedGraphicsRollout.GraphicsRollout = clonedGraphicsRollout;

            clonedGraphicsRollout.ParentGraphicsLayoutArea = this.ParentGraphicsLayoutArea.Clone(window, page, FinishesLibElements);

            return clonedGraphicsRollout;
        }

        #endregion

        #region Deleters and Destructors

        public void Delete()
        {
            if (Utilities.IsNotNull(GraphicsCutList))
            {
                foreach (GraphicsCut cut in GraphicsCutList)
                {
                    GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 1000);

                    foreach (GraphicsOverage overage in cut.GraphicsOverageList)
                    {
                        overage.Delete();
                    }

                    foreach (GraphicsRemnantCut graphicsRemnantCut in cut.GraphicsRemnantCutList)
                    {
                        graphicsRemnantCut.Delete();
                    }

                    cut.GraphicsOverageList.Clear();

                    GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 1001);

                    cut.Delete();

                    GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 1002);

                }


                GraphicsDebugSupportRoutines.CheckForNullInPageShapeDict(Page, 1003);

                GraphicsCutList.Clear();
                GraphicsCutList = null;


                graphicsOverageList = null;
            }

            if (Utilities.IsNotNull(graphicsUndrageList))
            {
                graphicsUndrageList.ForEach(u => u.Delete());

                graphicsUndrageList = null;
            }


        }

        #endregion

        public string Guid { get; set; }

        public GraphicShape Shape { get; set; }

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }
      
 
        public string LayoutAreaGuid { get; set; }

        //private List<GraphicsCut> graphicsCutList = null;

        //public List<GraphicsCut> GraphicsCutList
        //{
        //    get
        //    {
        //        if (graphicsCutList is null)
        //        {
        //            graphicsCutList = new List<GraphicsCut>();

        //            foreach (var graphicCut in GraphicsCutList)
        //            {
                       
        //                graphicCut.ParentGraphicsRollout = this;
        //                graphicsCutList.Add(graphicCut);
        //            }
        //            //Rollout.GraphicsCutList.ForEach(c => graphicsCutList.Add(new ParentGraphicsCut(Window, Page, this, c)));
        //        }

        //        return graphicsCutList;
        //    }
        //}

        public void Add(GraphicsCut graphicsCut)
        {
            if (GraphicsCutList is null)
            {
                GraphicsCutList = new List<GraphicsCut>();
            }

            graphicsCut.ParentGraphicsRollout = this;

            GraphicsCutList.Add(graphicsCut);

           // GraphicsCutList.Add(graphicsCut);
        }

        private List<GraphicsOverage> graphicsOverageList = null;

        public List<GraphicsOverage> GraphicsOverageList
        {
            get
            {
                if (graphicsOverageList is null)
                {
                    graphicsOverageList = new List<GraphicsOverage>();

                    foreach (GraphicsCut graphicsCut in GraphicsCutList)
                    {
                        foreach (GraphicsOverage graphicsOverage in graphicsCut.GraphicsOverageList)
                        {
                            graphicsOverageList.Add(graphicsOverage);
                        }
                    }
                }

                return graphicsOverageList;
            }
        }

        private List<GraphicsUndrage> graphicsUndrageList = null;

        //public List<GraphicsUndrage> GraphicsUndrageList
        //{
        //    get
        //    {
        //        if (graphicsUndrageList is null)
        //        {
        //            graphicsUndrageList = new List<GraphicsUndrage>();

        //            GraphicsUndrageList.ForEach(u => graphicsUndrageList.Add(
        //                new GraphicsUndrage(this, Window, Page, u)));
        //        }

        //        return graphicsUndrageList;
        //    }
        //}

        public void Add(GraphicsUndrage graphicsUndrage)
        {
            if (graphicsUndrageList is null)
            {
                graphicsUndrageList = new List<GraphicsUndrage>();
            }

            graphicsUndrage.ParentGraphicsRollout = this;

            graphicsUndrageList.Add(graphicsUndrage);

            GraphicsUndrageList.Add(graphicsUndrage);
        }

        /// <summary>
        /// Generates the cuts overs and unders for a specific rollout
        /// </summary>
        /// <param name="theta">The angle of the cuts line (passed through for used in defining bounding rectangles)</param>
        /// <param name="graphicsLayoutArea">The graphics layout area associated with this rollout</param>
        public void GenerateCutsOveragesAndUndrages(
            GraphicsWindow window
            , GraphicsPage page
            , double theta
            , GraphicsLayoutArea graphicsLayoutArea
            , double drawingScaleInInches)
        {
            base.GenerateCutsOveragesAndUndrages(
                window
                , page
                , FinishesLibElements
                , theta
                , (LayoutArea)graphicsLayoutArea
                , drawingScaleInInches);
        }

        public void Draw(Color lineColor, Color fillColor)
        {
            Coordinate coord1 = RolloutRectangle.LowerLeft;
            Coordinate coord2 = RolloutRectangle.UpperRght;

            Shape = Page.DrawRectangle(this, coord1.X, coord1.Y, coord2.X, coord2.Y);

            Shape.VisioShape.Data1 = "[ParentGraphicsRollout]";

            Shape.SetLineColor(lineColor);
            Shape.SetFillColor(fillColor);
            Shape.SetFillOpacity((double)fillColor.A / 256.0);
        }

        public new void Clear()
        {
            this.GraphicsCutList.Clear();

            Clear();
        }

        public Dictionary<string, GraphicShape> GenerateGraphicShapeDict()
        {
            Dictionary<string, GraphicShape> rtrnDict = new Dictionary<string, GraphicShape>();

            if (Shape != null)
            {
                if (Shape.VisioShape != null)
                {
                    rtrnDict.Add(Shape.Guid, Shape);
                }
            }

            foreach (GraphicsCut graphicsCut in this.GraphicsCutList)
            {
                GraphicShape shape = graphicsCut.Shape;

                if (shape == null)
                {
                    continue;
                }

                if (shape.VisioShape == null)
                {
                    continue;
                }

                Dictionary<string, GraphicShape> graphicsCutShapeDict = graphicsCut.GenerateShapeDict();

                foreach (KeyValuePair<string,  GraphicShape> kvp in graphicsCutShapeDict)
                {
                    if (rtrnDict.ContainsKey(kvp.Key))
                    {
                        continue;
                    }

                    rtrnDict.Add(kvp.Key,kvp.Value);
                }

                if (rtrnDict.ContainsKey(shape.Guid))
                {
                    continue;
                }

                rtrnDict.Add(shape.Guid, shape);
            }

            foreach (GraphicsOverage graphicsOverage in this.GraphicsOverageList)
            {
                GraphicShape shape = graphicsOverage.Shape;

                if (shape == null)
                {
                    continue;
                }

                if (shape.VisioShape == null)
                {
                    continue;
                }

                Dictionary<string, GraphicShape> graphicsCutShapeDict = graphicsOverage.GenerateShapeDict();

                foreach (KeyValuePair<string, GraphicShape> kvp in graphicsCutShapeDict)
                {
                    if (rtrnDict.ContainsKey(kvp.Key))
                    {
                        continue;
                    }

                    rtrnDict.Add(kvp.Key, kvp.Value);
                }

                if (rtrnDict.ContainsKey(shape.Guid))
                {
                    continue;
                }

                rtrnDict.Add(shape.Guid, shape);
            }

            foreach (GraphicsUndrage graphicsUndrage in this.GraphicsUndrageList)
            {
                GraphicShape undrageShape = graphicsUndrage.Shape;

                if (undrageShape == null)
                {
                    continue;
                }

                if (undrageShape.VisioShape == null)
                {
                    continue;
                }

                if (rtrnDict.ContainsKey(undrageShape.Guid))
                {
                    continue;
                }

                Dictionary<string, GraphicShape> graphicsCutShapeDict = graphicsUndrage.GenerateShapeDict();

                foreach (KeyValuePair<string, GraphicShape> kvp in graphicsCutShapeDict)
                {
                    if (rtrnDict.ContainsKey(kvp.Key))
                    {
                        continue;
                    }

                    rtrnDict.Add(kvp.Key, kvp.Value);
                }

                if (!rtrnDict.ContainsKey(undrageShape.Guid))
                {
                    rtrnDict.Add(undrageShape.Guid, undrageShape);
                }
                
            }

            return rtrnDict;
        }
    }
}
