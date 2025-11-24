#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsLayerBase.cs. Project: Graphics. Created: 11/14/2024         */
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
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TracerLib;

    public class GraphicsLayerBase
    {
        private GraphicsWindow window;

        private GraphicsPage page;

        private GraphicsLayer _layerBase = null;

        public GraphicsLayer LayerBase
        {
            get
            {
                return _layerBase;
            }

            set
            {
                _layerBase = value;
            }
        }

        private string layerName;

        public GraphicsLayerStyle graphicsLayerStyle = GraphicsLayerStyle.Dynamic;

        public bool LockLayer { get; set; } = false;

        public GraphicsLayerType graphicsLayerType { get; set; }

        public bool Visibility = false;

        #region Constructors

        public GraphicsLayerBase(
            GraphicsWindow window
            , GraphicsPage page
            , string layerName
            , GraphicsLayerType graphicsLayerType
            , GraphicsLayerStyle layerStyle = GraphicsLayerStyle.Dynamic
            , bool lockLayer = false)
        {
            this.window = window;
            this.page = page;
            this.layerName = layerName;
            this.graphicsLayerStyle = layerStyle;

            LockLayer = lockLayer;
        }

        #endregion

        #region Deleters and Destructors

        public void Delete()
        {
         
            if (_layerBase is null)
            {
                return;
            }

            //----------------------------------------------------------//
            // The following is defensive, it should never be activated //
            //----------------------------------------------------------//

            if (page.GraphicsLayerDictContains(_layerBase.Guid))
            {
                page.RemoveFromGraphicsLayerDict(_layerBase.Guid);
            }

            VisioInterop.UnlockLayer(_layerBase);

            _layerBase.RemoveAllShapes();

            _layerBase.Delete();

            _layerBase = null;
        }

        ~GraphicsLayerBase()
        {
            Delete();
        }

        #endregion

        public void SetLayerVisibility(bool visible)
        {
            Visibility = visible;

            if (_layerBase is null)
            {
                return;
            }

            _layerBase.SetLayerVisibility(visible);
        }

        public void AddShape(IGraphicsShape iShape, int addType)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { iShape, addType });
#endif
            
            #region Validations

            if (iShape is null)
            {
                Tracer.TraceGen.TraceError("Call to GraphicsLayerBase:AddShape with null iShape.", 1, true);

                return;
            }

            GraphicShape shape = (GraphicShape) iShape.Shape;

            if (shape is null)
            {
                Tracer.TraceGen.TraceError("GraphicsLayerBase:AddShape attempt to add null shape.", 1, true);

                return;
            }

            string data1 = shape.Data1;

            if (string.IsNullOrEmpty(shape.Guid))
            {
                Tracer.TraceGen.TraceError("GraphicsLayerBase:AddShape attempt to add shape with null guid.", 1, true);

                return;
            }

            iShape.GraphicsLayer = this;

            #endregion

            shape.AddToLayerSet(this);

            if (_layerBase is null)
            {
                generateLayer();
            }

           
            shape.AddToLayerSet(this);

            _layerBase.AddShape(iShape, addType);
        }

        public void RemoveShapeFromLayer(IGraphicsShape iShape, int removeType = 0)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { iShape, removeType });
#endif
            #region Validations

            if (iShape is null)
            {
                Tracer.TraceGen.TraceError("Call to GraphicsLayerBase:RemoveShapeFromLayer with null iShape.", 1, true);

                return;
            }

            GraphicShape shape = iShape.Shape;

            if (shape is null)
            {
                Tracer.TraceGen.TraceError("GraphicsLayerBase:RemoveShapeFromLayer attempt to add null shape.", 1, true);

                return;
            }

            if (string.IsNullOrEmpty(shape.Guid))
            {
                Tracer.TraceGen.TraceError("GraphicsLayerBase:RemoveShapeFromLayer attempt to add shape with null guid.", 1, true);

                return;
            }

            #endregion

            if (_layerBase is null)
            {
                return;
            }

            shape.RemoveFromLayerSet(this);

            string data1 = shape.Data1;

            _layerBase.RemoveShape(iShape);

            if (_layerBase.ShapeDictCount() <= 0 && _layerBase.GraphicsLayerStyle == GraphicsLayerStyle.Dynamic)
            {
               Delete();
            }
        }

        public bool IsVisible()
        {
            return (_layerBase == null) ? false : _layerBase.Visibility;
        }

        public void Lock()
        {
            LockLayer = true;

            if (_layerBase is null)
            {
                    return;
            }

            VisioInterop.LockLayer(_layerBase);
        }

        public void UnLock()
        {
            if (_layerBase is null)
            {
                return;
            }

            VisioInterop.UnlockLayer(_layerBase);
        }

        public bool IsLocked()
        {
            if (_layerBase is null)
            {
                return false;
            }

            if (_layerBase.visioLayer is null)
            {
                return false;
            }

            return VisioInterop.IsLocked(_layerBase.visioLayer);
        }

        private void generateLayer()
        {
            _layerBase = new GraphicsLayer(this, window, page, layerName, GraphicsLayerType.Unknown, graphicsLayerStyle);

            if (!page.GraphicsLayerDictContains(_layerBase.Guid))
            {
                page.AddToGraphicsLayerDict(_layerBase);
            }

            if (LockLayer)
            {
                _layerBase.Lock();
            }

            else
            {
                _layerBase.Unlock();
            }
            //MDD Add Kludge -- add kludge to set layer type here.
            _layerBase.SetLayerVisibility(Visibility);
        }

        public void SetLayerOpacity(double opacity)
        {
            if (_layerBase is null)
            {
                generateLayer();
            }

            VisioInterop.SetLayerOpacity(_layerBase, opacity);
        }

        public GraphicsLayer GetBaseLayer()
        {
            if (_layerBase is null)
            {
                generateLayer();
            }

            return _layerBase;
        }

        public static explicit operator GraphicsLayer(GraphicsLayerBase graphicsLayerBase)
        {
            return graphicsLayerBase._layerBase;
        }
    }
}
