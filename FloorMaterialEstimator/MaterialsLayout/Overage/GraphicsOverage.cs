#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsOverage.cs. Project: MaterialsLayout. Created: 11/14/2024         */
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
    using Graphics;
    using Geometry;
    using Utilities;
    using FinishesLib;


    /// <summary>
    /// Graphics level representation of an overage
    /// </summary>
    public class GraphicsOverage : Overage, IGraphicsShape
    {
        #region Overage Indexing

        //public uint UndrageIndex
        //{
        //    get
        //    {
        //        if (this.GraphicsOverageIndex == null)
        //        {
        //            throw new InvalidOperationException();
        //        }

        //        return this.GraphicsOverageIndex.OverageIndex;
        //    }

        //    set
        //    {
        //        if (this.GraphicsOverageIndex == null)
        //        {
        //            throw new InvalidOperationException();
        //        }

        //        if (this.GraphicsOverageIndex.OverageIndex == value)
        //        {
        //            return;
        //        }

        //        this.GraphicsOverageIndex.OverageIndex = value;
        //    }
        //}
        public GraphicsOverageIndex GraphicsOverageIndex
        {
            get;
            set;
        }


        #endregion

        #region Key Elements

        private GraphicsCut _parentGraphicsCut = null;
        
        public GraphicsCut ParentGraphicsCut
        {
            get
            {
                if (_parentGraphicsCut == null)
                {
                    throw new NotImplementedException();
                }

                return _parentGraphicsCut;
            }

            set
            {
                if (_parentGraphicsCut == value)
                {
                    return;
                }

                _parentGraphicsCut = value;
            }
        }

        private FinishesLibElements _finishesLibElements = null;

        public FinishesLibElements FinishesLibElements
        {
            get
            {
                if (_finishesLibElements == null)
                {
                    throw new NotImplementedException();
                }

                return _finishesLibElements;
            }

            set
            {
                if (_finishesLibElements == value)
                {
                    return;
                }

                _finishesLibElements = value;
            }
        }

        #endregion

        #region Constructors and Cloners

        public GraphicsOverage(
            GraphicsCut parentArea
            , GraphicsWindow window
            , GraphicsPage page
            , FinishesLibElements finishesLibElements
            , Overage overage) : base(overage)
        {
            this.Window = window;

            this.Page = page;

            this.FinishesLibElements = finishesLibElements;

            ParentGraphicsCut = parentArea;

            Guid = GuidMaintenance.CreateGuid(this);

            GlobalSettings.OverageIndexFontInPtsChanged += GlobalSettings_OverageIndexFontInPtsChanged;

            Coordinate center = Coordinate.NullCoordinate;

            if (overage.BoundingRectangle != null)
            {
                center = overage.BoundingRectangle.Center;
            }

            GraphicsOverageIndex = new GraphicsOverageIndex(
                this
                , window
                , page
                , FinishesLibElements.AreaFinishLayers.OversIndexLayer
                , null
                , center
                , GlobalSettings.CutIndexCircleRadius);
        }

        public GraphicsOverage(
            GraphicsCut parentArea
            , GraphicsWindow window
            , GraphicsPage page
            , FinishesLibElements finishesLibElements
            , Geometry.Rectangle overageRectangle) : base(overageRectangle)
        {
            this.Window = window;

            this.Page = page;

            Guid = GuidMaintenance.CreateGuid(this);

            ParentGraphicsCut = parentArea;

            GlobalSettings.OverageIndexFontInPtsChanged += GlobalSettings_OverageIndexFontInPtsChanged;

            this.FinishesLibElements = finishesLibElements;


            Coordinate center = Coordinate.NullCoordinate;

            if (overageRectangle != null)
            {
                center = overageRectangle.Center;
            }

            GraphicsOverageIndex = new GraphicsOverageIndex(
                this
                , window
                , page
                , FinishesLibElements.AreaFinishLayers.OversIndexLayer
                , null
                , center
                , GlobalSettings.CutIndexCircleRadius);
        }

        #endregion

        #region Deleters and Destructors


        public new void Delete()
        {
            GlobalSettings.OverageIndexFontInPtsChanged -= GlobalSettings_OverageIndexFontInPtsChanged;

            if (Utilities.IsNotNull(GraphicsOverageIndex))
            {
                GraphicsOverageIndex.Delete();
                GraphicsOverageIndex = null;
            }

            if (Shape is null)
            {
                return;
            }

            Shape.RemoveFromAllLayers();
            Page.RemoveFromPageShapeDict(Shape);

            Shape.Delete();

            Shape = null;

        }

        #endregion
        

        #region Properties

        #region IGraphicsShape Properties

        public GraphicsWindow Window { get; set; } = null;

        public GraphicsPage Page { get; set; } = null;

        public GraphicShape Shape { get; set; } = null;

        public GraphicsLayerBase GraphicsLayer
        {
            get { return GraphicsOverageLayer; }
            set { GraphicsOverageLayer = value; }
        }

        private GraphicsLayerBase _graphicsOverageLayer = null;

        public GraphicsLayerBase GraphicsOverageLayer
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

        public GraphicsLayerBase GraphicsOverageIndexLayer { get; set; } = null;

        public ShapeType ShapeType { get; set; } = ShapeType.Overage;

        public string Guid { get; set; } = null;

        #endregion

      
        #endregion

        private void GlobalSettings_OverageIndexFontInPtsChanged(double overageIndexFontInPts)
        {
            if (GraphicsOverageIndex is null)
            {
                return;
            }

            if (GraphicsOverageIndex.Shape is null)
            {
                return;
            }

            VisioInterop.SetTextFontSize(GraphicsOverageIndex.Shape, (int) Math.Round(overageIndexFontInPts));
        }
     
        public override string ToString()
        {
            return '+' + EffectiveDimensions.Item1.ToString("#,##0.0") + " x " + EffectiveDimensions.Item2.ToString("#,##0.0");
        }

        public GraphicShape DrawBoundingRectangleNormal(
            object parentObject
            , GraphicsLayerBase oversLayer
            , GraphicsLayerBase graphicsIndexLayer
            , Color overagePenColor
            , Color overageFillColor
            , int lineWidthInPts)
        {
             if (BoundingRectangle is null)
            {
                return null;
            }

            DirectedPolygon boundingPolygon = (DirectedPolygon)BoundingRectangle;

            GraphicsDirectedPolygon graphicsBoundingPolygon = new GraphicsDirectedPolygon(this.Window, this.Page, boundingPolygon);

            GraphicShape boundingRectangleShape = graphicsBoundingPolygon.Draw(overagePenColor, overageFillColor);

            boundingRectangleShape.SetFillOpacity(0);

            boundingRectangleShape.SetLineWidth(lineWidthInPts);

            Coordinate center = graphicsBoundingPolygon.Centroid();

            if (GraphicsOverageIndex is null)
            {
              

                GraphicsOverageIndex = new GraphicsOverageIndex(
                    this
                    ,this.Window
                    , this.Page
                    , graphicsIndexLayer
                    , null
                    , center
                    , 0.2);

                
               //  GraphicsOverageIndex.OverageIndex = this.OverageIndex;
            }

            else
            {
                GraphicsOverageIndex.Location = center;
            }

            GraphicsOverageIndex.Draw();

            if (GraphicsOverageIndex.OverageIndex > 0)
            {
                GraphicsOverageIndex.DrawOverageIndexText();
            }

            GraphicShape overageShape = boundingRectangleShape;
           
            overageShape.Window = Window;
            overageShape.Page = Page;
            overageShape.ParentObject = parentObject;
            overageShape.Guid = GuidMaintenance.CreateGuid(overageShape);
            overageShape.ShapeType = ShapeType.Overage;

            overageShape.SetShapeData("[Overage]", "Composite Shape", overageShape.Guid);

            if (overageShape != null)
            {
                oversLayer.AddShape(overageShape, 1);
            }

            GraphicShape circleTagShape = null;

            if (GraphicsOverageIndex != null)
            {
                GraphicCircleTag circleTag = (GraphicCircleTag)GraphicsOverageIndex;

                if (circleTag != null)
                {
                    if (circleTag.Shape != null)
                    {
                        circleTagShape = circleTag.Shape;
                    }
                }
            }

            Console.WriteLine(Environment.StackTrace);

            if (circleTagShape != null)
            {
                graphicsIndexLayer.AddShape(circleTagShape, 1);
            }

            return overageShape;
        }

        public GraphicShape DrawBoundingRectangleEmbd(
            GraphicsLayerBase embdOverLayer
            , GraphicsLayerBase overIndexLayer
            , Color overagePenColor
            , Color overageFillColor
            , int lineWidthInPts)
        {
            if (BoundingRectangle is null)
            {
                return null;
            }

            DirectedPolygon boundingPolygon = (DirectedPolygon)BoundingRectangle;

            GraphicsDirectedPolygon graphicsBoundingPolygon = new GraphicsDirectedPolygon(this.Window, this.Page, boundingPolygon);

            GraphicShape boundingRectangleShape = graphicsBoundingPolygon.Draw(overagePenColor, overageFillColor);

            this.Shape = boundingRectangleShape;

            VisioInterop.SetLineWidth(boundingRectangleShape, lineWidthInPts);
            embdOverLayer.AddShape(boundingRectangleShape, 1);

            if (GraphicsOverageIndex is null)
            {

                Coordinate center = graphicsBoundingPolygon.Centroid();

                GraphicsOverageIndex = new GraphicsOverageIndex(
                    this
                    , this.Window
                    , this.Page
                    , overIndexLayer
                    , null
                    , center
                    , 0.2);

                GraphicsOverageIndex.OverageIndex = this.OverageIndex;
            }

            GraphicsOverageIndex.Draw();

            if (GraphicsOverageIndex.OverageIndex > 0)
            {
                GraphicsOverageIndex.DrawOverageIndexText();
            }

            embdOverLayer.AddShape(GraphicsOverageIndex.Shape, 1);

            return boundingRectangleShape;
        }

        public void SetVisibility(bool visible)
        {
            if (Shape is null)
            {
                return;
            }

            VisioInterop.SetShapeLineVisibility(Shape, visible);

            if (Utilities.IsNotNull(GraphicsOverageIndex))
            {
                GraphicsOverageIndex.SetVisibility(visible);
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

            if (!rtrnDict.ContainsKey(Shape.Guid))
            {
                rtrnDict[Shape.Guid] = Shape;
            }

            if (this.GraphicsOverageIndex == null)
            {
                return rtrnDict;
            }

            if (this.GraphicsOverageIndex.Shape is null)
            {
                return rtrnDict;
            }


            if (!rtrnDict.ContainsKey(this.GraphicsOverageIndex.Shape.Guid))
            {
                rtrnDict[this.GraphicsOverageIndex.Shape.Guid] = this.GraphicsOverageIndex.Shape;
            }

            return rtrnDict;
        }

        internal void UpdateIndexLocation()
        {
            if (this.GraphicsOverageIndex == null)
            {
                return;
            }

            if (this.BoundingRectangle == null)
            {
                return;
            }

            this.GraphicsOverageIndex.Location = this.BoundingRectangle.Center;
        }
    }
}
