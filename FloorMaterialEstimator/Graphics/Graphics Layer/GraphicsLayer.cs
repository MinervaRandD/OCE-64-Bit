#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsLayer.cs. Project: Graphics. Created: 11/14/2024         */
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



namespace Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TracerLib;
    using Visio = Microsoft.Office.Interop.Visio;

    using Utilities;
    using System.Xml.Serialization;
    using Microsoft.Office.Interop.Visio;

    public class GraphicsLayer
    {
        public string Guid { get; set; }

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public Visio.Layer visioLayer { get; set; }


        public string LayerName { get; set; }

        public GraphicsLayerStyle GraphicsLayerStyle { get; set; }

        public GraphicsLayerType GraphicsLayerType { get; set; }

        [XmlIgnore]
        public GraphicsLayerBase ParentGraphicLayerBase { get; set; } = null;
        //public string LayerName => visioLayer is null ? string.Empty : visioLayer.Name;

        //-------------------------------------------------  Shape Dictionary -------------------------------------//

        public Dictionary<string, IGraphicsShape> ShapeDict { get; set; } = new Dictionary<string, IGraphicsShape>();

        public IEnumerable<IGraphicsShape> Shapes => ShapeDict.Values;

        #region Constructors

        public GraphicsLayer(
            GraphicsLayerBase parentGraphicsLayerBase
            , GraphicsWindow graphicsWindow
            , GraphicsPage graphicsPage
            , string layerName
            , GraphicsLayerType graphicsLayerType
            , GraphicsLayerStyle graphicsLayerStype)
        {
            Guid = GuidMaintenance.CreateGuid(this);

            ParentGraphicLayerBase = parentGraphicsLayerBase;

            Window = graphicsWindow;

            Page = graphicsPage;

            LayerName = layerName;

            visioLayer = Page.AddLayer(Guid, layerName);

            GraphicsLayerStyle = graphicsLayerStype;

            GraphicsLayerType = graphicsLayerType;

            Page.AddToGraphicsLayerDict(this);

            visibility = false;
        }

        #endregion

        #region Deleters and Destructors

        public void Delete()
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });
#endif
            #region Validations

            if (this.visioLayer is null)
            {
                Tracer.TraceGen.TraceError("Attempt to delete layer in call to GraphicsLayer:Delete0 with null visio layer", 1, true);

                return;
            }

            if (ShapeDictCount() > 0)
            {
                Tracer.TraceGen.TraceError("Attempt to delete layer in call to GraphicsLayer:Delete0 with shapes still in the layer", 1, true);
            }

            #endregion

            VisioInterop.DeleteLayer(this);

            Guid = string.Empty;

            visioLayer = null;
        }

        //~GraphicsLayer()
        //{
        //    Delete();
        //}
        #endregion

        public void AddToShapeDict(IGraphicsShape iShape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { iShape });
#endif

            #region Validations

        
            if (iShape is null)
            {
                Tracer.TraceGen.TraceError("Call to GraphicsLayer:AddToShapeDict with null iShape.", 1, true);
            }

            GraphicShape shape = (GraphicShape) iShape.Shape;

            string data1 = shape.Data1;

            if (shape is null)
            {
                Tracer.TraceGen.TraceError("Call to GraphicsLayer:AddToShapeDict with null shape.", 1, true);

                return;
            }

            if (shape.VisioShape is null)
            {
                Tracer.TraceGen.TraceError("Call to GraphicsLayer:AddToShapeDict with null shape.visioShape", 1, true);

                return;
            }

            #endregion

            if (ShapeDict.ContainsKey(shape.Guid))
            {
                Tracer.TraceGen.TraceError("Attempt to add a shape in GraphicsLayer:AddToShapeDict that already exists in the dictionary", 1, true);

                return;
            }

            ShapeDict.Add(shape.Guid, iShape);
        }
       
        public void RemoveFromShapeDict(string guid)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { guid });
#endif
            if (!ShapeDict.ContainsKey(guid))
            {
                Tracer.TraceGen.TraceError("Attempt to remove a shape in GraphicsLayer:RemoveFromShapeDict that doesn't exists in the dictionary", 1, true);

                return;
            }

            ShapeDict.Remove(guid);
        }

        public bool ShapeDictContains(IGraphicsShape iShape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { iShape });
#endif
            #region Validations

            if (iShape is null)
            {
                Tracer.TraceGen.TraceError("Call to GraphicsLayer:ShapeDictContains with null iShape.", 1, true);
            }

            GraphicShape shape = (GraphicShape)iShape;

            if (shape is null)
            {
                Tracer.TraceGen.TraceError("Call to GraphicsLayer:ShapeDictContains with null shape.", 1, true);

                return false;
            }

            if (shape.VisioShape is null)
            {
                Tracer.TraceGen.TraceError("Call to GraphicsLayer:ShapeDictContains with null shape.visioShape", 1, true);

                return false;
            }

            #endregion

            string data1 = shape.Data1;

            return ShapeDictContains(shape.Guid);
        }

        public bool ShapeDictContains(string guid)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { guid });
#endif
            return ShapeDict.ContainsKey(guid);
        }

        public int ShapeDictCount()
        {
            return ShapeDict.Count;
        }

        public void ShapeDictClear()
        {
            this.ShapeDict.Clear();
        }

        //---------------------------------------------------------------------------------------------------------//


        private bool visibility = true;

        public bool Visibility
        {
            get
            {
                return visibility;
            }

            set
            {
                visibility = value;
            }
        }

        public bool IsLocked
        {
            get
            {
                return VisioInterop.IsLocked(visioLayer);
            }
        }

       

        public void SetLayerVisibility(bool visible)
        {
            VisioInterop.SetLayerVisibility(this, visible);

            this.visibility = visible;
        }

        /// <summary>
        /// Adds a shape to the graphics layer
        /// </summary>
        /// <param name="shape">The graphics shape to add</param>
        /// <param name="v">0 means remove all other shapes, 1 means leave current shapes on the layer in place</param>
        public void AddShape(IGraphicsShape iShape, int v)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { iShape, v });
#endif
            try
            {
                #region Validations

                if (iShape is null)
                {
                    Tracer.TraceGen.TraceError("Call to GraphicsLayer:AddShape with null iShape.", 1, true);
                }

                GraphicShape shape = (GraphicShape)iShape.Shape;

                if (shape is null)
                {
                    Tracer.TraceGen.TraceError("GraphicsLayer:AddShape attempt to add null shape.", 1, true);

                    return;
                }

                if (string.IsNullOrEmpty(shape.Guid))
                {
                    Tracer.TraceGen.TraceError("GraphicsLayer:AddShape attempt to add shape with null guid.", 1, true);

                    return;
                }

                if (ShapeDictContains(shape.Guid))
                {
                   Tracer.TraceGen.TraceError("GraphicsLayer:AddShape attempt to add shape which is already in the ShapeDict.", 1, true);

                    return;
                }

                #endregion

                string data1 = shape.Data1;

                AddToShapeDict(iShape);

                VisioInterop.AddShapeToLayer(shape, this);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("GraphicsLayer:AddShape throws and exception", ex, 1, true);
            }

        }

        public void RemoveShape(IGraphicsShape iShape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { iShape, v });
#endif
            try
            {
                #region Validations

                if (iShape is null)
                {
                    Tracer.TraceGen.TraceError("Call to GraphicsLayer:RemoveShape with null iShape.", 1, true);
                }

                GraphicShape shape = iShape.Shape;

                if (shape is null)
                {
                    Tracer.TraceGen.TraceError("GraphicsLayer:RemoveShape attempt to remove null shape.", 1, true);

                    return;
                }

                if (string.IsNullOrEmpty(shape.Guid))
                {
                    Tracer.TraceGen.TraceError("GraphicsLayer:RemoveShape attempt to add shape with null guid.", 1, true);

                    return;
                }

                #endregion

                //----------------------------------------------------------------------------------//
                // Layers should not actually remove or delete the shapes. It should only remove    //
                // the shape from the layer. The following is DEFENSIVE, but it should never be     //
                // needed.                                                                          //
                //----------------------------------------------------------------------------------//

                if (ShapeDictContains(iShape.Guid))
                {
                    RemoveFromShapeDict(iShape.Guid);
                }

                Visio.Shape visioShape = shape.VisioShape;

                if (visioShape != null)
                {
                    VisioInterop.RemoveShapeFromLayer(visioShape, this);
                }

            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("GraphicsLayer:RemoveShape throws and exception", ex, 1, true);
            }
        }

        public void BringShapesToFront()
        {
            foreach (IGraphicsShape iShape in this.Shapes)
            {
                ((GraphicShape)iShape).BringToFront();
            }
        }

        public void RemoveAllShapes()
        {
            List<IGraphicsShape> shapeList = new List<IGraphicsShape>(Shapes);

            foreach (IGraphicsShape iShape in shapeList)
            {
                VisioInterop.RemoveShapeFromLayer((GraphicShape)iShape.Shape, this);
            }

            // Should be clear anyway. This is defensive.

            ShapeDictClear();
        }

        public void Unlock()
        {
            if (visioLayer is null)
            {
                return;
            }

            VisioInterop.UnlockLayer(this);
        }

        public void Lock()
        {
            if (visioLayer is null)
            {
                return;
            }

            VisioInterop.LockLayer(this);
        }

        public override string ToString()
        {
            Visio.Layer visioLayer = this.visioLayer;

            string guid = this.Guid;

            if (guid.Length > 8)
            {
                guid = guid.Substring(0, 8);
            }

            return "Layer Name: " + LayerName + ", Layer Type: " + GraphicsLayerType.ToString() + ", Guid: " + guid;
        }
    }
}
