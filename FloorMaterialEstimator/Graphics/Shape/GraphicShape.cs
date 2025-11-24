#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicShape.cs. Project: Graphics. Created: 11/8/2024         */
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


using System.Linq;

namespace Graphics
{

    using System.Xml.Serialization;

    using Visio = Microsoft.Office.Interop.Visio;

    using Utilities;
    using System;
    using Geometry;
    using System.Drawing;
    using TracerLib;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.CodeDom;
    using System.Diagnostics.Eventing.Reader;
    using System.Windows.Forms.VisualStyles;
    using Microsoft.Office.Interop.Visio;
    using Color = System.Drawing.Color;
    using Globals;

    /// <summary>
    /// Graphic shape is the atomic primative element for all shapes.
    /// 
    /// There is a lot of defensive code here since the order of creation and deletion
    /// as well as some of the instantiation of key properties may not be clear
    /// 
    /// </summary>
    public class GraphicShape : IGraphicsShape
    {
        #region Properties
#if UsingVisio
        [XmlIgnore]
        public GraphicsPage Page
        {
            get;
            set;
        } = null;

        [XmlIgnore]
        public GraphicsWindow Window { get; set; }

        // MDD Reset 2024-12-28
        private Visio.Shape _visioShape = null;

        [XmlIgnore]
        public Visio.Shape VisioShape
        {
            get
            { 
                return _visioShape;
            }

            set
            {
                // MDD Reset 2025-02-12

                if (this.ParentObject != null)
                {
                    string pObjToString = this.ParentObject.ToString();

                    if (pObjToString.Contains("CanvasLayoutArea"))
                    {
                        ;
                    }
                }

                if (_visioShape == value) { return; }

                _visioShape = value;
            }
        }
#endif

        public ShapeType ShapeType { get; set; }

        private object _parentObject = null;

        [XmlIgnore]
        public object ParentObject
        {
            get
            {
                return _parentObject;
            }

            set
            {
                if (_parentObject == value)
                {
                    return;
                }
                _parentObject = value;
            }
        } 

        public delegate void ShapeRotatedHandler(GraphicShape shape);

        public event ShapeRotatedHandler ShapeRotated;

        public HashSet<GraphicsLayerBase> LayerSet = new HashSet<GraphicsLayerBase>();

        public bool AddToLayerSet(GraphicsLayerBase graphicsLayerBase)
        {
            if (LayerSet.Contains(graphicsLayerBase))
            {
                return false;
            }

            LayerSet.Add(graphicsLayerBase);

            return true;
        }

        public bool RemoveFromLayerSet(GraphicsLayerBase graphicsLayerBase)
        {
            if (!LayerSet.Contains(graphicsLayerBase))
            {
                return false;
            }

            LayerSet.Remove(graphicsLayerBase); 

            return true;
        }

        public GraphicsLayerBase SingleGraphicsLayer
        {
            get
            {
                if (LayerSet.Count == 0)
                {
                    return null;
                }

                if (LayerSet.Count > 1)
                {
                    throw new NotImplementedException();
                }

                return LayerSet.First();
            }
        }

        public GraphicsLayerBase GraphicsLayer
        {
            //get
            //{
            //    return _graphicsLayerBase;
            //}

            set
            {
                if (value is null)
                {
                    return;
                }

                AddToLayerSet(value);

                return;
                //_graphicsLayerBase = value;

                //if (!(_graphicsLayerBase is null))
                //{
                //    if (_graphicsLayerBase.LayerBase == null)
                //    {
                //        ;
                //    }
                //}
            }
        }

        public void RemoveFromAllLayers()
        {
            List<GraphicsLayerBase> graphicsLayerBaseList = new List<GraphicsLayerBase>();

            foreach (GraphicsLayer graphicsLayer in Page.GraphicsLayers)
            {
                if (!graphicsLayer.ShapeDictContains(this))
                {
                    continue;
                }

                GraphicsLayerBase graphicsLayerBase = graphicsLayer.ParentGraphicLayerBase;

                if (graphicsLayerBase == null)
                {
                    continue;
                }

                graphicsLayerBaseList.Add(graphicsLayerBase);

            }

            foreach (GraphicsLayerBase graphicsLayerBase in graphicsLayerBaseList)
            { 
                bool layerIsLocked = false;

                if (graphicsLayerBase.IsLocked())
                {
                    layerIsLocked = true;
                    graphicsLayerBase.UnLock();
                }

                graphicsLayerBase.RemoveShapeFromLayer(this);

                if (layerIsLocked)
                {
                    graphicsLayerBase.Lock();
                }
            }
        }
        private string _guid = string.Empty;

        public string Guid
        {
            get
            {
                return _guid;
            }

            set
            {
                _guid = value;

     
                if (Utilities.IsNotNull(VisioShape))
                {
                    try
                    {
                        VisioShape.Data3 = value;
                    }

                    catch
                    {
                        ;
                    }
                }
            }
        }

        private string _data1 = string.Empty;

        public string Data1
        {
            get
            {
                try
                {
                    return VisioShape is null ? _data1 : VisioShape.Data1;
                }

                catch (Exception ex)
                {
                    return string.Empty;
                }
            }

            set
            {
                if (_data1 == value)
                {
                    return;
                }

                _data1 = value;

                if (VisioShape != null)
                {
                    VisioInterop.SetShapeData1(this, value);
                }

            }
        }

        private string _data2 = string.Empty;

        public string Data2
        {
            get
            {
                // MDD Reset

              
                return VisioShape is null ? _data2 : VisioShape.Data2;
            }

            set
            {
                if (_data2 == value)
                {
                    return;
                }

                _data2 = value;

                if (VisioShape != null)
                {
                    VisioInterop.SetShapeData2(this, value);
                }
            }
        }

        private string _data3 = string.Empty;

        public string Data3
        {
            get
            {
                return VisioShape is null ? _data3 : VisioShape.Data3;
            }

            set
            {
                if (_data3 == value)
                {
                    return;
                }

                _data3 = value;

                if (VisioShape != null)
                {
                    VisioInterop.SetShapeData3(this, value);
                }

            }
        }

        GraphicShape IGraphicsShape.Shape
        {
            get
            {
                return this;
            }

            set
            {

            }
        }

        //private bool visioShapeBeforeDeleteSet { get; set; } = false;

        #endregion

        #region Constructors
        public GraphicShape() { }
        
        public GraphicShape(object parentObject, GraphicsWindow window, GraphicsPage page, GraphicsLayerBase graphicsLayerBase)
        {
            
            if (parentObject is null)
            {
                throw new NotImplementedException();
            }

            if (window is null || page is null)
            {
                throw new NotImplementedException();
            }

            this.ParentObject = parentObject;

            this.Window = window;

            this.Page = page;

            this.GraphicsLayer = graphicsLayerBase;
        }

        public GraphicShape(object parentObject, GraphicsWindow window, GraphicsPage page, Visio.Shape visioShape, ShapeType shapeType)
        {
            this.ParentObject = parentObject;

            this.Window = window;

            this.Page = page;

            this.VisioShape = visioShape;

            this.ShapeType = shapeType;

                this.Guid = GuidMaintenance.CreateGuid(this);
           
            this.VisioShape.Data3 = this.Guid;

            this.VisioShape.Data2 = shapeType.ToString();

           // visioShapeBeforeDeleteSet = true;

           // VisioShape.FormulaChanged += VisioShape_FormulaChanged;
        }

        public GraphicShape(object parentObject, GraphicsWindow window, GraphicsPage page, Visio.Shape visioShape, ShapeType shapeType, string guid = null)
        {
            

            if (window is null || page is null)
            {
                throw new NotImplementedException();
            }

            this.ParentObject = parentObject;

            this.Window = window;

            this.Page = page;

            this.VisioShape = visioShape;

            this.ShapeType = shapeType;

            if (guid is null)
            {
                this.Guid = GuidMaintenance.CreateGuid(this);
            }

            else
            {
                {
                    this.Guid = guid;
                }
            }

            this.VisioShape.Data3 = this.Guid;

            this.VisioShape.Data2 = shapeType.ToString();
        }

        #endregion

        #region Destructors and Deleters


        public void Delete()
        {
            Delete(true);
        }

        public void Delete(bool deleteVisioShape = true)
        {
           
            if (this.IsLocked)
            {
                Unlock();
            }

            RemoveFromAllLayers();

          
            if (this.VisioShape != null)
            {
                // There is something weird about the sequence related to garbage collection that causes a problem when called from the destructor.
                if (deleteVisioShape)
                {
                    VisioInterop.DeleteShape(this.VisioShape);
                }

                this.VisioShape = null;
            }

            //if (layerIsLocked)
            //{
            //    this.GraphicsLayer.Lock();
            //}

            this.GraphicsLayer = null;
            this.ShapeType = ShapeType.Unknown;
            this.Guid = string.Empty;
            this.ParentObject = null;
        }

        

        #endregion
        //private void VisioShape_FormulaChanged(Visio.Cell Cell)
        //{
        //    if (Cell.Name.Contains("Angle"))
        //    {


        //        if (ShapeRotated == null)
        //        {
        //            return;
        //        }

        //        ShapeRotated.Invoke(this);

        //        return;
        //    }
        //}

        [XmlIgnore]
        public double ShapeAngle
        {
            get
            {
                return VisioInterop.GetShapeAngle(this);
            }
            set
            {
                VisioInterop.SetShapeAngle(this, value);
            }
        }

        [XmlIgnore]
        public Coordinate CenterPoint
        {
            get
            {
                return VisioInterop.GetShapeCenter(this);
            }
            set
            {
                VisioInterop.SetShapeLocation(this, value.X, value.Y);
            }
        }

        [XmlIgnore]
        public Coordinate UpperLeftLocation
        {
            get
            {
                return VisioInterop.GetShapeUpperLeftLocation(this);
            }
        }

        [XmlIgnore]
        public Coordinate LowerRightLocation
        {
            get
            {
                return VisioInterop.GetShapeLowerRightLocation(this);
            }
        }

        [XmlIgnore]
        public double Height
        {
            get
            {
                return VisioInterop.GetShapeHeight(this);
            }
            set
            {
                VisioInterop.SetShapeHeight(this, value);
            }
        }

        [XmlIgnore]
        public double Width
        {
            get
            {
                return VisioInterop.GetShapeWidth(this);
            }
            set
            {
                VisioInterop.SetShapeWidth(this, value);
            }
        }
        public bool IsLocked
        {
            get
            {
                return VisioInterop.IsLocked(this);
            }

        }

        public void Lock()
        {
            VisioInterop.LockShape(this);
        }

        public void Unlock()
        {
            VisioInterop.UnlockShape(this);
        }

    

        public void ShowShapeOutline(bool outline)
        {
            VisioInterop.SetNolineMode(this, !outline);
        }

        public void LockSelected(int selectKey)
        {
            VisioInterop.LockShapeSelected(this, selectKey);
        }

        public void BringToFront()
        {
            VisioInterop.BringToFront(this);
        }

        public void SendToBack()
        {
            VisioInterop.SendToBack(this);
        }

        public void SetShapeText(string text, Color color, double fontSizePts)
        {
            //GraphicsLayerBase graphicsLayer = this.GraphicsLayer;

            //bool isLocked = false;

            //if (graphicsLayer != null)
            //{
            //    isLocked = true;

            //    graphicsLayer.UnLock();
            //}

            // MDD Reset 2024-12-21

            string data1 = this.Data1;

            VisioInterop.SetShapeText(this, text, color, fontSizePts);

            //if (isLocked)
            //{
            //    graphicsLayer.Lock();
            //}
        }

        public void SetFill(string fillFormula)
        {
            VisioInterop.SetShapeFill(this, fillFormula);
        }

        public void SetPattern(int pattern)
        {
            VisioInterop.SetFillPattern(this, pattern.ToString());
        }

        public void SetLineWidth(double lineWidthPts)
        {
            VisioInterop.SetLineWidth(this, lineWidthPts);
        }

        public void SetEndpointArrows(int arrowType, int arrowSize)
        {
            VisioInterop.SetStartpointArrow(this, arrowType, arrowSize);
            VisioInterop.SetEndpointArrow(this, arrowType, arrowSize);
        }

        public void SetFillOpacity(double opacity)
        {
            VisioInterop.SetFillOpacity(this, opacity);
        }

        public void SetFillColor(Color color)
        {
            VisioInterop.SetBaseFillColor(this, color);
        }

        public void SetFillColor(string visioFillColorFormula)
        {
            VisioInterop.SetBaseFillColor(this, visioFillColorFormula);
        }

        public void SetLineStyle(VisioLineStyle lineStyle)
        {
            VisioInterop.SetBaseLineStyle(this, lineStyle);
        }

        public void SetBaseLineStyle(int visioLineType)
        {
            VisioInterop.SetBaseLineStyle(this, visioLineType);
        }

        public void SetBaseLineColor(Color lineColor)
        {
            VisioInterop.SetBaseLineColor(this, lineColor);
        }

        public void SetLineStyle(string colorFormula)
        {
            VisioInterop.SetBaseLineStyle(this, colorFormula);
        }

        public void SetLineType(int lineType)
        {
            VisioInterop.SetBaseLineStyle(this, lineType);
        }

        public void SetBaseLineWidth(double lineWidthInPts)
        {
            VisioInterop.SetLineWidth(this, lineWidthInPts);
        }

        public void SetLineColor(Color color)
        {
            VisioInterop.SetBaseLineColor(this, color);
        }

        public void SetLineOpacity(double opacity)
        {
            VisioInterop.SetBaseLineOpacity(this, opacity);
        }

        public void SetLineText(string text)
        {
            VisioInterop.SetLineText(this, text);
        }

        public void SetTextFontSize(int fontSize)
        {
            VisioInterop.SetTextFontSize(this, fontSize);
        }

        public void SetTextVerticalAlignment(int alignment)
        {
            VisioInterop.SetTextVerticalAlignment(this, alignment.ToString());
        }

        public void SetTextHorizontalAlignment(int alignment)
        {
            VisioInterop.SetTextHorizontalAlignment(this, alignment.ToString());
        }

        public void SetTextColor(Color color)
        {
            VisioInterop.SetTextColor(this, color);
        }

        public void SetFillTransparency(string visioFillTransparencyFormula)
        {
            VisioInterop.SetFillTransparency(this, visioFillTransparencyFormula);
        }

        public void SetShapeData(string data1, string data2)
        {
            Data1 = data1;

            Data2 = data2;

            Data3 = this._guid;
        }


        public void SetShapeData(string data1, string data2, string data3)
        {
            Data1 = data1;

            Data2 = data2;

            Data3 = data3;

            this._guid = data3;
        }


        public void SetShapeData1(string data1)
        {
            Data1 = data1;
        }

        public void SetShapeData2(string data2)
        {
            Data2 = data2;
        }

        public void SetShapeData3(string data3)
        {
            Data3 = data3;
        }

        public void SetFillOpacity(GraphicShape shape, double opacity)
        {
            VisioInterop.SetFillOpacity(this, opacity);
        }


        public void CenterText()
        {
            VisioInterop.CenterText(this);
        }

        public override string ToString()
        {
            //if (VisioShape is null)
            //{
            //    return "{null, null, null}";
            //}

            string data1 = "null";

            string data2 = "null";

            string data3 = "null";

            try
            {
                data1 = Data1;
            }

            catch { }

            try
            {
                data2 = Data2;
            }

            catch { }

            try
            {
                data3 = Data3;
            }

            catch { }

            
            return "{" + data1 + ", " + data2 + ", " + data3 + "}";
        }

        public GraphicShape Subtract(object parentShape, GraphicsWindow window, GraphicsPage page, List<GraphicShape> subtrahendShapes)
        {
            return VisioGeometryEngine.Subtract(parentShape, Window, Page, this, subtrahendShapes);
        }

        public void SyncWithCanvas()
        {
            if (this.VisioShape is null)
            {
                return;
            }

            //if (this.Data1.Contains("Cut Index"))
            //{
                
            //    ;
            //}
            if (this.ShapeType == ShapeType.Line)
            {
                if (!Page.PageShapeDict.ContainsKey(this.Guid))
                {
                    return;
                }

                GraphicsDirectedLine line = (GraphicsDirectedLine)Page.PageShapeDict[this.Guid];

                VisioInterop.SyncLineToVisio(line);
            }

            else if (this.ShapeType == ShapeType.Rectangle)
            {
                GraphicsRectangle rectangle = (GraphicsRectangle)Page.PageShapeDict[this.Guid];

                //VisioInterop.SyncRectangleToVisio(rectangle);
            }

            else if (this.ShapeType == ShapeType.Circle) 
            {
 
            }

            else if (this.ShapeType == ShapeType.Polyline)
            {
                //GraphicsDirectedPolyline polyline = (GraphicsDirectedPolyline)Page.PageShapeDict[this.Guid];

                //VisioInterop.SyncPolylineToVisio(polyline);
            }

            else if ((this.ShapeType & ShapeType.CircleTag) != 0)
            {
                string data1 = this.Data1;

                var circleTag = (GraphicCircleTag)this;

                if (circleTag != null)
                {
                    VisioInterop.SyncCircleTagToVisio(circleTag);
                }
            }
           

            
        }


        #region Casts
        // This region defines casts for shape.


        /// <summary>
        /// Cast GraphicShape to GraphicDirectedLine
        /// </summary>
        /// <param name="shape">The GraphicShape to be cast</param>
        public static explicit operator GraphicsDirectedLine(GraphicShape shape)
        {
            if (shape == null)
            {
                return null;
            }

            if (shape.ParentObject is null)
            {
                return null;
            }

            object parentShape = shape.ParentObject;

            if (!(parentShape is GraphicsDirectedLine))
            {
                return null;
            }

            return (GraphicsDirectedLine) parentShape;
        }

        /// <summary>
        /// Cast GraphicShape to GraphicsRectangle
        /// </summary>
        /// <param name="shape">The GraphicShape to be cast</param>
        public static explicit operator GraphicsRectangle(GraphicShape shape)
        {
            if (shape == null)
            {
                return null;
            }

            if ((shape.ShapeType & ShapeType.Rectangle) == 0)
            {
                return null;
            }

            if (shape.ParentObject is null)
            {
                return null;
            }

            object parentShape = shape.ParentObject;

            return (GraphicsRectangle)parentShape;
        }

        /// <summary>
        /// Cast GraphicShape to GraphicsCircle
        /// </summary>
        /// <param name="shape">The GraphicShape to be cast</param>
        public static explicit operator GraphicsCircle(GraphicShape shape)
        {
            if (shape == null)
            {
                return null;
            }

            if ((shape.ShapeType & ShapeType.Circle) == 0)
            {
                return null;
            }

            if (shape.ParentObject is null)
            {
                return null;
            }

            object parentShape = shape.ParentObject;

            return (GraphicsCircle)parentShape;
        }

        /// <summary>
        /// Cast GraphicShape to GraphicsDirectedPolyline
        /// </summary>
        /// <param name="shape">The GraphicShape to be cast</param>
        public static explicit operator GraphicsDirectedPolyline(GraphicShape shape)
        {
            if (shape == null)
            {
                return null;
            }

            if (shape.ParentObject is null)
            {
                return null;
            }

            object parentShape = shape.ParentObject;

            if (!(parentShape is GraphicsDirectedPolyline))
            {
                return null;
            }

            return (GraphicsDirectedPolyline)parentShape;
        }
       
        #endregion // End of Casts region

    }
}
