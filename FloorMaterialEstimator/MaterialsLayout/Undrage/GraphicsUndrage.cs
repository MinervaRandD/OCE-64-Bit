#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsUndrage.cs. Project: MaterialsLayout. Created: 11/14/2024         */
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
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using SettingsLib;
    using Geometry;
    using Graphics;
    using Utilities;

    using Visio = Microsoft.Office.Interop.Visio;
    using FinishesLib;

    /// <summary>
    /// Graphics level representation of an underage
    /// </summary>
    public class GraphicsUndrage: Undrage, IGraphicsShape
    {
        #region Undrage Indexing

        public uint UndrageIndex
        {
            get
            {
                if (this.GraphicsUndrageIndex == null)
                {
                    throw new InvalidOperationException();
                }

                return this.GraphicsUndrageIndex.UndrageIndex;
            }

            set
            {
                if (this.GraphicsUndrageIndex == null)
                {
                    throw new InvalidOperationException();
                }

                if (this.GraphicsUndrageIndex.UndrageIndex == value)
                {
                    return;
                }

                this.GraphicsUndrageIndex.UndrageIndex = value;
            }
        }

        #endregion

        #region Key Elements

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

            }
        }
        #endregion

        #region Constructors and Cloners

        public GraphicsUndrage(
            GraphicsRollout parentArea
            , GraphicsWindow window
            , GraphicsPage page
            , Undrage undrage
            , string guid = null)
        {
            this.Window = window;

            this.Page = page;

            this.Guid = guid;

            this.ParentGraphicsRollout = parentArea;

            this.BoundingRectangle = undrage.BoundingRectangle;

            GlobalSettings.UnderageIndexFontInPtsChanged += GlobalSettings_UnderageIndexFontInPtsChanged;
            
            Coordinate center = Coordinate.NullCoordinate;

            if (undrage.BoundingRectangle != null)
            {
                center = undrage.BoundingRectangle.Center;
            }

            GraphicsUndrageIndex = new GraphicsUndrageIndex(
                this
                , window
                , page
                , FinishesLibElements.AreaFinishLayers.UndrsIndexLayer
                , null
                , center
                , GlobalSettings.CutIndexCircleRadius);
        }

        public GraphicsUndrage(
            GraphicsRollout parentArea
            , GraphicsWindow window
            , GraphicsPage page
            , Undrage undrage)
        {
            this.Window = window;

            this.Page = page;

            this.Guid = GuidMaintenance.CreateGuid(this);

            this.ParentGraphicsRollout = parentArea;

            //Undrage = undrage;

            GlobalSettings.UnderageIndexFontInPtsChanged += GlobalSettings_UnderageIndexFontInPtsChanged;


            Coordinate center = Coordinate.NullCoordinate;

            this.BoundingRectangle = undrage.BoundingRectangle;

            if (undrage.BoundingRectangle != null)
            {
                center = undrage.BoundingRectangle.Center;
            }

            GraphicsUndrageIndex = new GraphicsUndrageIndex(
                this
                , window
                , page
                , FinishesLibElements.AreaFinishLayers.UndrsIndexLayer
                , null
                , center
                , GlobalSettings.CutIndexCircleRadius);

        }

        public GraphicsUndrage(
            GraphicsRollout parentArea
            , GraphicsWindow window
            , GraphicsPage page
            , Geometry.Rectangle boundingRectangle)
        {
            this.Window = window;

            this.Page = page;

            this.Guid = GuidMaintenance.CreateGuid(this);

            this.ParentGraphicsRollout = parentArea;

            //Undrage = undrage;

            GlobalSettings.UnderageIndexFontInPtsChanged += GlobalSettings_UnderageIndexFontInPtsChanged;

            this.BoundingRectangle = boundingRectangle;

            Coordinate center = Coordinate.NullCoordinate;

            if (boundingRectangle != null)
            {
                center = boundingRectangle.Center;
            }

            GraphicsUndrageIndex = new GraphicsUndrageIndex(
                this
                , window
                , page
                , FinishesLibElements.AreaFinishLayers.UndrsIndexLayer
                , null
                , center
                , GlobalSettings.CutIndexCircleRadius);

        }

        public GraphicsUndrage(
            GraphicsRollout parentArea
            , GraphicsWindow window
            , GraphicsPage page
            , FinishesLibElements finishesLibElements
            , Geometry.Rectangle boundingRectangle) : base()
        {
            this.Window = window;

            this.Page = page;

            this.FinishesLibElements = finishesLibElements;

            this.ParentGraphicsRollout = parentArea;

            this.Guid = GuidMaintenance.CreateGuid(this);

            this.BoundingRectangle = boundingRectangle;

            GlobalSettings.UnderageIndexFontInPtsChanged += GlobalSettings_UnderageIndexFontInPtsChanged;

            Coordinate center = Coordinate.NullCoordinate;

            if (boundingRectangle != null)
            {
                center = boundingRectangle.Center;
            }
             
            GraphicsUndrageIndex = new GraphicsUndrageIndex(
                this
                , window
                , page
                , finishesLibElements.AreaFinishLayers.UndrsIndexLayer
                , null
                , center
                , GlobalSettings.CutIndexCircleRadius);
         
        }
        public GraphicsUndrage Clone(GraphicsRollout parentArea, GraphicsWindow window, GraphicsPage page)
        {
            GraphicsRollout clonedGraphicsRollout = parentArea.Clone(parentArea.ParentGraphicsLayoutArea, window, page);

            return new GraphicsUndrage(parentArea, window, page, this.BoundingRectangle)
            {
                MaterialWidth = MaterialWidth,

                OverrideEffectiveDimensions = OverrideEffectiveDimensions,

                BoundingRectangle = BoundingRectangle,

                EffectiveDimensions = EffectiveDimensions,

                Deleted = Deleted,

                MaterialOverlap = MaterialOverlap,

                RemnantType = RemnantType,

                UndrageIndex = UndrageIndex,
            };

        }


        #endregion

        #region Deleters and Destructors

        public void Delete()
        {
            GlobalSettings.UnderageIndexFontInPtsChanged -= GlobalSettings_UnderageIndexFontInPtsChanged;

            if (Utilities.IsNotNull(Shape))
            {
                Page.RemoveFromPageShapeDict(Shape);

                Shape.Delete();
            }

            if (Utilities.IsNotNull(GraphicsUndrageIndex))
            {
                Undrage.RemoveIndex(UndrageIndex);

                GraphicsUndrageIndex.Delete();
            }
        }

        #endregion
        
    

        #region Properties
        #region IGraphicsShape Properties
        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public GraphicShape Shape { get; set; }

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

        public ShapeType ShapeType
        {
            get
            {
                return ShapeType.Polygon;
            }
        }

        public string Guid { get; set; }

        #endregion

        public string GraphicsRolloutGuid => ParentGraphicsRollout.Guid;

        public GraphicsUndrageIndex GraphicsUndrageIndex
        {
            get;
            set;
        }
       
        #endregion

        private void GlobalSettings_UnderageIndexFontInPtsChanged(double underageIndexFontInPts)
        {
            if (GraphicsUndrageIndex is null)
            {
                return;
            }

            if (GraphicsUndrageIndex.Shape is null)
            {
                return;
            }

            VisioInterop.SetTextFontSize(GraphicsUndrageIndex.Shape, (int) Math.Round(underageIndexFontInPts));
        }

        public GraphicShape Draw(Color penColor, Color fillColor)
        {
            //System.Array coordinates = getCoordinates();

            double x1 = BoundingRectangle.UpperLeft.X;
            double y1 = BoundingRectangle.UpperLeft.Y;

            double x2 = BoundingRectangle.LowerRght.X;
            double y2 = BoundingRectangle.LowerRght.Y;

            Visio.Shape shape = Page.VisioPage.DrawRectangle(x1, y1, x2, y2);

            Shape = new GraphicShape(this, Window, Page, shape, ShapeType.Rectangle);

            Shape.SetLineColor(penColor);
            Shape.SetFillColor(fillColor);

            return Shape;
        }



        public override string ToString()
        {
            return '(' + EffectiveDimensions.Item1.ToString("#,##0.0") + " x " + EffectiveDimensions.Item2.ToString("#,##0.0") + ')';
        }

        public string ToString(double drawingScale)
        {
            return '(' + (EffectiveDimensions.Item1 * drawingScale / 12.0).ToString("#,##0.0") + " x " + (EffectiveDimensions.Item2 * drawingScale / 12.0).ToString("#,##0.0") + ')';
        }

        /// <summary>
        /// Draws the bounding rectangle for the underage
        /// </summary>
        /// <param name="penColor">The pen color for the region drawn</param>
        /// <param name="fillColor">The fill color for the region drawn</param>
        /// <param name="lineWidthInPts">The width of the bounding rectangle perimeter line</param>
        /// <returns>The graphics shape of the bonding rectangle</returns>
        public GraphicShape DrawBoundingRectangle(
            GraphicsLayerBase undrsLayer
            , GraphicsLayerBase undrsIndxLayer
            , Color penColor
            , Color fillColor
            , int lineWidthInPts)
        {
          
            DirectedPolygon boundingPolygon = (DirectedPolygon)BoundingRectangle;

            GraphicsDirectedPolygon graphicsBoundingPolygon = new GraphicsDirectedPolygon(this.Window, this.Page, boundingPolygon);

            Shape = graphicsBoundingPolygon.Draw(penColor, fillColor);

            Shape.SetLineWidth(lineWidthInPts);

            Shape.GraphicsLayer = undrsLayer;

            if (GraphicsUndrageIndex is null)
            {

                Coordinate center = graphicsBoundingPolygon.Centroid();

                GraphicsUndrageIndex = new GraphicsUndrageIndex(
                    this
                    , this.Window
                    , this.Page
                    , FinishesLibElements.AreaFinishLayers.UndrsIndexLayer
                    , null
                    , center
                    , GlobalSettings.CutIndexCircleRadius);

                //center.X = center.X + 0.33;

                GraphicsUndrageIndex.Draw();

                GraphicsUndrageIndex.Shape.ParentObject = this;

                if (GraphicsUndrageIndex.UndrageIndex > 0)
                {
                    GraphicsUndrageIndex.DrawUndrageIndexText();

                    undrsIndxLayer.AddShape(GraphicsUndrageIndex.Shape, 1);
                }
            }

            else
            {
                Coordinate center = graphicsBoundingPolygon.Centroid();

                //center.X = center.X + 0.33;

                GraphicsUndrageIndex.Location = center;

                GraphicsUndrageIndex.Draw();

                // GraphicsUndrageIndex.Shape.ParentObject = this;

                undrsIndxLayer.AddShape(GraphicsUndrageIndex.Shape, 1);
                Page.AddToPageShapeDict(GraphicsUndrageIndex.Shape);


                if (GraphicsUndrageIndex.UndrageIndex > 0)
                {
                    GraphicsUndrageIndex.DrawUndrageIndexText();

                }
            }


            return Shape;
        }

        public void SetVisibility(bool visible)
        {
            if (Shape is null)
            {
                return;
            }

            VisioInterop.SetShapeLineVisibility(Shape, visible);

            if (Utilities.IsNotNull(GraphicsUndrageIndex))
            {
                GraphicsUndrageIndex.SetVisibility(visible);
            }
        }

        internal Dictionary<string, GraphicShape> GenerateShapeDict()
        {
            Dictionary<string, GraphicShape> rtrnDict = new Dictionary<string, GraphicShape>();

            if (this.Shape is null)
            {
                return rtrnDict;
            }

            if (this.Shape.VisioShape is null)
            {
                return rtrnDict;
            }

            if (this.GraphicsUndrageIndex != null)
            {
                GraphicShape underageIndexShape = this.GraphicsUndrageIndex.Shape;

                if (underageIndexShape != null)
                {
                    if (underageIndexShape.VisioShape != null)
                    {
                        if (!rtrnDict.ContainsKey(underageIndexShape.Guid))
                        {
                            rtrnDict[underageIndexShape.Guid] = underageIndexShape;
                        }
                    }
                }
            }
            
            if (!rtrnDict.ContainsKey(Shape.Guid))
            {
                rtrnDict[Shape.Guid] = Shape;
            }

            return rtrnDict;
        }

        internal void UpdateIndexLocation()
        {
            if (this.GraphicsUndrageIndex == null)
            {
                return;
            }

            if (this.BoundingRectangle == null)
            {
                return;
            }

            this.GraphicsUndrageIndex.Location = this.BoundingRectangle.Center;
            
        }
    }
}
