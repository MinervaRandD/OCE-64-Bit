#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsPageLayers.cs. Project: Graphics. Created: 11/14/2024         */
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
    using System.Collections.Generic;
    using Visio = Microsoft.Office.Interop.Visio;
    using TracerLib;
    using System;

    public partial class GraphicsPage
    {
        #region Graphics Layers Maintenance

        //---------------------------------------------------------------------------------------------------------//
        //----------------------------------------- Graphic Layers Maintenance-------------------------------------//


        public IEnumerable<GraphicsLayer> GraphicsLayers => GraphicsLayerDict.Values;

        public Dictionary<string, GraphicsLayer> GraphicsLayerDict = new Dictionary<string, GraphicsLayer>();

        // Note: The purpose of the following routines is to insulate the access to the graphics layer dictionary from direct calls.
        // This allows for validation and facilitates debugging.

        public bool GraphicsLayerDictContains(GraphicsLayer graphicsLayer)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { graphicsLayer });
#endif

            #region Validations

            if (graphicsLayer is null)
            {
                Tracer.TraceGen.TraceError("Call to GraphicsPage:GraphicsLayerDictContains with null graphics layer", 1, true);

                return true;
            }

            #endregion

            string guid = graphicsLayer.Guid;

            return GraphicsLayerDictContains(guid);
        }

        public bool GraphicsLayerDictContains(string guid)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { guid });
#endif

            return GraphicsLayerDict.ContainsKey(guid);
        }

        public void AddToGraphicsLayerDict(GraphicsLayer graphicsLayer)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { graphicsLayer });
#endif

            #region Validations

            if (graphicsLayer.visioLayer is null)
            {
                Tracer.TraceGen.TraceError("Attempt to add a graphics layer to the graphics layer dictionary with visio layer null in GraphicsPage:AddToGraphicsLayerDict", 1, true);

                return;
            }

            if (GraphicsLayerDict.ContainsKey(graphicsLayer.Guid))
            {
                Tracer.TraceGen.TraceError("Attempt to add a graphics layer to the graphics layer dictionary with a key already in the dictionary GraphicsPage:AddToGraphicsLayerDict", 1, true);

                return;
            }

            #endregion

            GraphicsLayerDict.Add(graphicsLayer.Guid, graphicsLayer);
        }

        public void RemoveFromGraphicsLayerDict(GraphicsLayer layer)
        {
            #region Validations

            if (!GraphicsLayerDict.ContainsKey(layer.Guid))
            {
                //--------------------------------------------------------------------------//
                // Following check is removed because removal of layers can legitimately be //
                // called from several locations.                                           //
                //--------------------------------------------------------------------------//

#if REMOVED
                Tracer.TraceGen.TraceError("Attempt to remove a graphics layer '" + layer.LayerName + "' from the graphics layer dictionary which does not exist in the dictionary in GraphicsPage:RemoveFromGraphicsLayerDict", 1, true);
#endif
                return;
            }

            #endregion

            RemoveFromGraphicsLayerDict(layer.Guid);
        }

        public void RemoveFromGraphicsLayerDict(string guid)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { guid });
#endif

            #region Validations

            if (!GraphicsLayerDict.ContainsKey(guid))
            {
                //--------------------------------------------------------------------------//
                // Following check is removed because removal of layers can legitimately be //
                // called from several locations.                                           //
                //--------------------------------------------------------------------------//

#if REMOVED
                Tracer.TraceGen.TraceError("Attempt to remove a graphics layer from the graphics layer dictionary which does not exist in the dictionary in GraphicsPage:RemoveFromGraphicsLayerDict", 1, true);
#endif
                return;
            }

            #endregion

            GraphicsLayerDict.Remove(guid);
        }

        public Visio.Layer AddLayer(string guid, string layerName)
        {
            Visio.Layer visioLayer = null;

            try
            {
              visioLayer = VisioPage.Layers.Add(layerName);

            }
            catch
            {

            }
          
            visioLayer.NameU = guid;

            return visioLayer;
        }

        //---------------------------------------------------------------------------------------------------------//
        #endregion

        #region Global Layers

        //------------------------------------//
        //                Global layers       //
        //------------------------------------//

        private void initializeGlobalLayers()
        {
            TakeoutLayer = new GraphicsLayerBase(Window, this, "[GraphicsPage:TakeoutLayer]", GraphicsLayerType.GraphicsPage_TakeoutLayer, GraphicsLayerStyle.Static);
            AreaModeGlobalLayer = new GraphicsLayerBase(Window, this, "[AreaMode:GlobalLayer]", GraphicsLayerType.AreaMode_GlobalLayer, GraphicsLayerStyle.Static);
            LineModeGlobalLayer = new GraphicsLayerBase(Window, this, "[LineMode:GlobalLayer]", GraphicsLayerType.LineMode_GlobalLayer, GraphicsLayerStyle.Static);
            SeamModeGlobalLayer = new GraphicsLayerBase(Window, this, "[SeamMode:GlobalLayer]", GraphicsLayerType.SeamMode_GlobalLayer, GraphicsLayerStyle.Static);
            DrawingLayer = new GraphicsLayerBase(Window, this, "[DrawingLayer]", GraphicsLayerType.DrawingLayer, GraphicsLayerStyle.Static);
            AreaLegendLayer = new GraphicsLayerBase(Window, this, "[AreaMode:LegendLayer]", GraphicsLayerType.AreaMode_LegandLayer, GraphicsLayerStyle.Static, true);
            LineLegendLayer = new GraphicsLayerBase(Window, this, "[LineMode:LegendLayer]", GraphicsLayerType.LineMode_LegandLayer, GraphicsLayerStyle.Static, true);
            ExtendedCrosshairsLayer = new GraphicsLayerBase(Window, this, "[ExtendedCrossHairs]", GraphicsLayerType.ExtendedCrossHairs, GraphicsLayerStyle.Static);
            InvisibleWorkspaceLayer = new GraphicsLayerBase(Window, this, "[InvisibilityWorkspace]", GraphicsLayerType.InvisibilityWorkspace, GraphicsLayerStyle.Static);
            //LockIconLayer = new GraphicsLayerBase(Window, this, "[SeamMode:LockIconLayer]", GraphicsLayerType.SeamMode_SeamDesignStateLayer, GraphicsLayerStyle.Static, true);
            
            InvisibleWorkspaceLayer.SetLayerVisibility(false);
           // LockIconLayer.SetLayerVisibility(false);
        }
      
        public GraphicsLayerBase TakeoutLayer { get; private set; } 
       
        public GraphicsLayerBase AreaModeGlobalLayer { get; private set; }

        public GraphicsLayerBase LineModeGlobalLayer { get; private set; }

        public GraphicsLayerBase SeamModeGlobalLayer { get; private set; }

        public GraphicsLayerBase DrawingLayer { get; private set; }

        public GraphicShape Drawing { get; set; }

        public GraphicsLayerBase AreaLegendLayer { get; private set; }

        public GraphicsLayerBase LineLegendLayer { get; private set; }

        public GraphicsLayerBase ExtendedCrosshairsLayer { get; private set; }


        //-----------------------------------------------------------------------------//
        // The following is a general workspace layer used to hide shapes temporarily. //
        // Mainly used in subdivision of layout areas.                                 //
        //-----------------------------------------------------------------------------//

        public GraphicsLayerBase InvisibleWorkspaceLayer { get; private set; }
        #endregion
    }
}
