using Globals;
using CanvasLib.DoorTakeouts;
using FinishesLib;
using Utilities;
using Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using PaletteLib;
using FloorMaterialEstimator.Finish_Controls;
using SettingsLib;
using TracerLib;

namespace FloorMaterialEstimator.CanvasManager
{
    public class LineFinishManager : IDisposable
    {
        public string Guid => LineFinishBase.Guid;

        public short VisioDashType => (short)LineFinishBase.VisioLineType;

        public LineFinishBaseList LineFinishBaseList { get; set; }

        public LineFinishBase LineFinishBase { get; set; }

        public bool Filtered => LineFinishBase.Filtered;

        public bool Selected => LineFinishBase.Selected;

        public Color LineColor => LineFinishBase.LineColor;

        public double LineWidthInPts => LineFinishBase.LineWidthInPts;

        public string LineName => LineFinishBase.LineName;

        public int VisioLineType => LineFinishBase.VisioLineType;

        public LineFinishLayers LineFinishLayers { get; set; } = null;

        public UCLineFinishPaletteElement LineFinishPaletteElement => PalettesGlobal.LineFinishPalette[Guid];


        private string _visioLineColorFormula = "THEMEGUARD(RGB(0,0,0))";

        public string VisioLineColorFormula
        {
            get
            {
                return _visioLineColorFormula;
            }

            set
            {
                _visioLineColorFormula = value;
            }
        }

        public string VisioLineStyleFormula
        {
            get
            {
                return VisioDashType.ToString();
            }
            
        }

        #region Line Dictionary Elements

        //--------------------------------------------------------------------------------------------------------------//
        //                          Line dictionary elements                                                            //
        //--------------------------------------------------------------------------------------------------------------//

        public Dictionary<string, CanvasDirectedLine> LineDict
        {
            get;
            set;
        } = new Dictionary<string, CanvasDirectedLine>();

        public IEnumerable<CanvasDirectedLine> CanvasDirectedLines => LineDict.Values;

        public bool LineDictContains(string guid) => LineDict.ContainsKey(guid);

        public bool LineDictContains(CanvasDirectedLine line) => LineDictContains(line.Guid);

        public void ClearLineDict() => LineDict.Clear();

        public void AddToLineDict(CanvasDirectedLine line)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { line });
#endif
            if (LineDict.ContainsKey(line.Guid))
            {
                Tracer.TraceGen.TraceError("Attempt to add a line to LineDict in UCLineFinishPaletteElement:AddToLineDict that already exists in the dictionary", 1, true);
                return;
            }

            LineDict.Add(line.Guid, line);
        }

        public void RemoveFromLineDict(CanvasDirectedLine line)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { line });
#endif

            RemoveFromLineDict(line.Guid);
        }

        public void RemoveFromLineDict(string guid)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { guid });
#endif

            if (!LineDict.ContainsKey(guid))
            {
                Tracer.TraceGen.TraceError("Attempt to remove a line from LineDict in UCLineFinishPaletteElement:RemoveFromLineDict that does not exist in the dictionary", 1, true);
                return;
            }

            LineDict.Remove(guid);
        }

        public int LineDictCount => LineDict.Count;

        public CanvasDirectedLine GetLineFromLineDict(string guid)
        {
            if (LineDictContains(guid))
            {
                return LineDict[guid];
            }

            return null;
        }

        //--------------------------------------------------------------------------------------------------------------//

#endregion

#region Layers

        //------------------------------------------------------------------------------------------------//
        // In the following, the layers are created on demand so as to reduce the total number of layers. //
        // It appears that too many layers slow the visio rendering process                               //
        //------------------------------------------------------------------------------------------------//

        public GraphicsLayerBase AreaDesignStateLayer => LineFinishLayers.AreaDesignStateLayer;

        public GraphicsLayerBase LineDesignStateLayer => LineFinishLayers.LineDesignStateLayer;

        public GraphicsLayerBase SeamDesignStateLayer => LineFinishLayers.SeamDesignStateLayer;

        public GraphicsLayerBase RemnantSeamDesignStateLayer => LineFinishLayers.RemnantSeamDesignStateLayer;

        public GraphicsLayerBase LineDesignStateAssociatedLineLayer => LineFinishLayers.LineDesignStateAssociatedLineLayer;

        //private GraphicsLayer _areaDesignStateLayer = null;

        //public GraphicsLayer AreaDesignStateLayer
        //{
        //    get
        //    {
        //        if (_areaDesignStateLayer is null)
        //        {
        //            _areaDesignStateLayer = new GraphicsLayer(Window, Page, "[LineFinishManager]" + LineFinishBase.LineName + "_AreaDesignStateLayer", GraphicsLayerType.Dynamic);

        //        }

        //        return _areaDesignStateLayer;
        //    }
        //}

        //public void AreaDesignStateLayer_SetLayerVisibility(bool visible)
        //{
        //    if (_areaDesignStateLayer is null)
        //    {
        //        return;
        //    }

        //    _areaDesignStateLayer.SetLayerVisibility(visible);
        //}

        //public void AreaDesignStateLayer_Delete()
        //{
        //    if (_areaDesignStateLayer is null)
        //    {
        //        return;
        //    }

        //    if (Page.GraphicsLayerDictContains(_areaDesignStateLayer))
        //    {
        //        Page.RemoveFromGraphicsLayerDict(_areaDesignStateLayer);
        //    }

        //    _areaDesignStateLayer.Delete0();

        //    _areaDesignStateLayer = null;
        //}

        //private GraphicsLayer _lineDesignStateLayer = null;

        //public GraphicsLayer LineDesignStateLayer
        //{
        //    get
        //    {
        //        if (_lineDesignStateLayer is null)
        //        {
        //            _lineDesignStateLayer = new GraphicsLayer(Window, Page, "[LineFinishManager]" + LineFinishBase.LineName + "_LineDesignStateLayer", GraphicsLayerType.Dynamic);

        //        }

        //        return _lineDesignStateLayer;
        //    }
        //}

        //public void LineDesignStateLayer_SetLayerVisibility(bool visible)
        //{
        //    if (_lineDesignStateLayer is null)
        //    {
        //        return;
        //    }

        //    _lineDesignStateLayer.SetLayerVisibility(visible);
        //}

        //public void LineDesignStateLayer_Delete()
        //{
        //    if (_lineDesignStateLayer is null)
        //    {
        //        return;
        //    }

        //    if (Page.GraphicsLayerDictContains(_lineDesignStateLayer))
        //    {
        //        Page.RemoveFromGraphicsLayerDict(_lineDesignStateLayer);
        //    }

        //    _lineDesignStateLayer.Delete0();

        //    _lineDesignStateLayer = null;
        //}

        //private GraphicsLayer _seamDesignStateLayer = null;

        //public GraphicsLayer SeamDesignStateLayer
        //{
        //    get
        //    {
        //        if (_seamDesignStateLayer is null)
        //        {
        //            _seamDesignStateLayer = new GraphicsLayer(Window, Page, "[LineFinishManager]" + LineFinishBase.LineName + "_SeamDesignStateLayer", GraphicsLayerType.Dynamic);

        //            VisioInterop.LockLayer(_seamDesignStateLayer);
        //        }


        //        return _seamDesignStateLayer;
        //    }

        //    set
        //    {
        //        _seamDesignStateLayer = value;
        //    }
        //}

        //public void SeamDesignStateLayer_SetLayerVisibility(bool visible)
        //{
        //    if (_seamDesignStateLayer is null)
        //    {
        //        return;
        //    }

        //    _seamDesignStateLayer.SetLayerVisibility(visible);
        //}

        //public void SeamDesignStateLayer_Delete()
        //{
        //    if (_seamDesignStateLayer is null)
        //    {
        //        return;
        //    }

        //    if (Page.GraphicsLayerDictContains(_seamDesignStateLayer))
        //    {
        //        Page.RemoveFromGraphicsLayerDict(_seamDesignStateLayer);
        //    }

        //    _seamDesignStateLayer.Delete0();

        //    _seamDesignStateLayer = null;
        //}

        //private GraphicsLayer _remnantSeamDesignStateLayer = null;

        //public GraphicsLayer RemnantSeamDesignStateLayer
        //{
        //    get
        //    {
        //        if (_remnantSeamDesignStateLayer is null)
        //        {
        //            _remnantSeamDesignStateLayer = new GraphicsLayer(Window, Page, "[LineFinishManager]" + LineFinishBase.LineName + "_RemnantSeamDesignStateLayer", GraphicsLayerType.Dynamic);

        //            //VisioInterop.LockLayer(_seamDesignStateLayer);
        //        }


        //        return _remnantSeamDesignStateLayer;
        //    }

        //    set
        //    {
        //        _remnantSeamDesignStateLayer = value;
        //    }
        //}

        //public void RemnantSeamDesignStateLayer_SetLayerVisibility(bool visible)
        //{
        //    if (_remnantSeamDesignStateLayer is null)
        //    {
        //        return;
        //    }

        //    _remnantSeamDesignStateLayer.SetLayerVisibility(visible);
        //}


        //public void RemnantSeamDesignStateLayer_Delete()
        //{
        //    if (_remnantSeamDesignStateLayer is null)
        //    {
        //        return;
        //    }

        //    if (Page.GraphicsLayerDictContains(_remnantSeamDesignStateLayer))
        //    {
        //        Page.RemoveFromGraphicsLayerDict(_remnantSeamDesignStateLayer);
        //    }

        //    _remnantSeamDesignStateLayer.Delete0();

        //    _remnantSeamDesignStateLayer = null;
        //}

        #endregion

        #region Takeout Areas

        //---------------------------------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------------------------//

        //-------------------------------------- Elements related to Graphic Takeout Areas ------------------------------- //

        public Dictionary<string, DoorTakeout> GraphicsTakeoutAreaDict { get; set; } = new Dictionary<string, DoorTakeout>();

        public void AddToGraphicsTakeoutAreaDict(DoorTakeout takeoutArea)
        {
            string guid = takeoutArea.Guid;

            if (string.IsNullOrEmpty(guid))
            {
                Tracer.TraceGen.TraceError("Attempt to add a takeout to GraphicsTakeoutAreaDict with a null guid in GraphicsPage:AddToGraphicsTakeoutAreaDict.", 1, true);
                return;
            }

            if (GraphicsTakeoutAreaDictContains(guid))
            {
                Tracer.TraceGen.TraceError("Attempt to add a shape to GraphicsTakeoutAreaDict with an existing guid in GraphicsPage:GraphicsTakeoutAreaDict.", 1, true);
                return;
            }

            GraphicsTakeoutAreaDict.Add(guid, takeoutArea);
        }

        public void RemoveFromGraphicsTakeoutAreaDict(DoorTakeout takeoutArea)
        {
            string guid = takeoutArea.Guid;

            RemoveFromGraphicsTakeoutAreaDict(guid);
        }

        public void RemoveFromGraphicsTakeoutAreaDict(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                Tracer.TraceGen.TraceError("Attempt to remove a takeout to GraphicsTakeoutAreaDict with a null guid in GraphicsPage:RemoveFromGraphicsTakeoutAreaDict.", 1, true);
                return;
            }

            if (!GraphicsTakeoutAreaDictContains(guid))
            {
                Tracer.TraceGen.TraceError("Attempt to remove a shape to GraphicsTakeoutAreaDict which does not exist in GraphicsPage:GraphicsTakeoutAreaDict.", 1, true);
                return;
            }

            GraphicsTakeoutAreaDict.Remove(guid);

        }

        public IEnumerable<DoorTakeout> DoorTakeoutList => GraphicsTakeoutAreaDict.Values;

        public bool GraphicsTakeoutAreaDictContains(string guid)
        {
            return GraphicsTakeoutAreaDict.ContainsKey(guid);
        }

        #endregion


        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }


        public LineFinishManager(
            GraphicsWindow window
            , GraphicsPage page
            , LineFinishBaseList lineFinishBaseList
            , LineFinishBase lineFinishBase)
        {
            this.LineFinishBaseList = lineFinishBaseList;

            this.LineFinishBase = lineFinishBase;

            this.Window = window;

            this.Page = page;

            updateBaseLineColor(this.LineFinishBase.LineColor);
            updateBaseLineStyle((short)this.LineFinishBase.VisioLineType);
            updateBaseLineWidth(this.LineFinishBase.LineWidthInPts);

            this.LineFinishLayers = new LineFinishLayers(window, page, lineFinishBase.LineName);

            lineFinishBase.LineColorChanged += LineFinishBase_LineColorChanged;

            lineFinishBase.LineTypeChanged += LineFinishBase_LineTypeChanged;

            lineFinishBase.LineWidthChanged += LineFinishBase_LineWidthChanged;
            lineFinishBase.FilteredChanged += LineFinishBase_FilteredChanged;
        }

        private void LineFinishBase_FilteredChanged(LineFinishBase LineFinishBase, bool filtered)
        {
            SetLineState(SystemState.DesignState, SystemState.SeamMode, Selected);
        }


        private void LineFinishBase_LineWidthChanged(LineFinishBase LineFinishBase, double lineWidthInPts)
        {
           
            //foreach (DoorTakeout doorTakeout in DoorTakeoutList)
            //{
            //    VisioInterop.SetLineWidth(doorTakeout.Shape, lineWidthInPts);
            //}

            foreach (var line in this.LineDict.Values)
            {
                line.SetBaseLineWidth(lineWidthInPts);
            }
        }

        private void LineFinishBase_LineTypeChanged(LineFinishBase lineFinishBase, int lineType)
        {
            //foreach (DoorTakeout doorTakeout in DoorTakeoutList)
            //{
            //    VisioInterop.SetBaseLineStyle(doorTakeout.Shape, lineType);
            //}

            foreach (var line in this.LineDict.Values)
            {
                line.SetBaseLineStyle(lineType.ToString());
            }
        }

        private void LineFinishBase_LineColorChanged(LineFinishBase LineFinishBase, System.Drawing.Color lineColor)
        {
            foreach (DoorTakeout doorTakeout in DoorTakeoutList)
            {
                VisioInterop.SetBaseLineColor(doorTakeout.Shape, lineColor);
                VisioInterop.SetBaseTextColor(doorTakeout.Shape, lineColor);
            }

            foreach (var line in this.LineDict.Values)
            {
                line.SetBaseLineColor(lineColor);
            }
        }

        public void SetLineState(DesignState designState, SeamMode seamMode, bool selected)
        {
            if (LineDictCount <= 0)
            {
                return;
            }

            if (Filtered)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                LineDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(false);

                return;
            }


            if (designState == DesignState.Area)
            {
                AreaDesignStateLayer.SetLayerVisibility(true);
                LineDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(false);
                RemnantSeamDesignStateLayer.SetLayerVisibility(false);
                LineDesignStateAssociatedLineLayer.SetLayerVisibility(false);

                foreach (CanvasDirectedLine line in CanvasDirectedLines)
                {
                    if (line.RemoveOnReturnToAreaMode)
                    {
                        AreaDesignStateLayer.RemoveShapeFromLayer(line.Shape, 1);
                    }

                    else
                    {
                        line.SetLineGraphics(DesignState.Area, selected, AreaShapeBuildStatus.Completed);
                    }
                }
            }

            else if (designState == DesignState.Line)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                LineDesignStateLayer.SetLayerVisibility(true);
                SeamDesignStateLayer.SetLayerVisibility(false);
                RemnantSeamDesignStateLayer.SetLayerVisibility(false);
                LineDesignStateAssociatedLineLayer.SetLayerVisibility(GlobalSettings.ShowAreaOutlineInLineMode);

                foreach (CanvasDirectedLine line in CanvasDirectedLines)
                {
                    // MDD Must Debug //

                    if ((line.LineRole == LineRole.AssociatedLine || line.LineRole == LineRole.ExternalPerimeter) && !GlobalSettings.ShowAreaOutlineInLineMode)
                    {
                        line.SetLineGraphics(DesignState.Area, false, AreaShapeBuildStatus.Completed);
                        continue;
                    }

                    line.SetLineGraphics(DesignState.Line, selected, AreaShapeBuildStatus.Completed);
                }
            }

            else if (designState == DesignState.Seam)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                LineDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(true);
                LineDesignStateAssociatedLineLayer.SetLayerVisibility(false);

                if (seamMode == SeamMode.Remnant)
                {
                    RemnantSeamDesignStateLayer.SetLayerVisibility(true);
                }

                else
                {
                    RemnantSeamDesignStateLayer.SetLayerVisibility(false);
                }

                foreach (CanvasDirectedLine line in CanvasDirectedLines)
                {
                    line.SetLineGraphics(DesignState.Seam, selected, AreaShapeBuildStatus.Completed);
                }
            }
        }

        internal void UpdateFinishStats()
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                LineFinishBase.LengthInInches = 0;

                return;
            }

            double lengthInInches = 0.0;

            double scale = Page.DrawingScaleInInches;

            foreach (CanvasDirectedLine canvasDirectedLine in this.CanvasDirectedLines)
            {

                if (canvasDirectedLine.IsZeroLine)
                {
                    continue;
                }

                if (canvasDirectedLine.LineRole == LineRole.NullPerimeter || canvasDirectedLine.LineRole == LineRole.ExternalPerimeter)
                {
                    continue;
                }

                if (canvasDirectedLine.LineCompoundType == LineCompoundType.Single)
                {
                    lengthInInches += canvasDirectedLine.GetScaledLineLength(scale);
                }

                else if (canvasDirectedLine.LineCompoundType == LineCompoundType.Double)
                {
                    lengthInInches += 2.0 * canvasDirectedLine.GetScaledLineLength(scale);
                }

            }

            foreach (DoorTakeout takeout in DoorTakeoutList)
            {
                lengthInInches -= Utilities.Utilities.FeetToInches(takeout.TakeoutAmount);
            }

            LineFinishBase.LengthInInches = lengthInInches;

        }

        internal void AddLineFull(CanvasDirectedLine line, bool updateColor = true)
        {
         
            AddToLineDict(line);

            line.ucLine = this.LineFinishPaletteElement;

            if (updateColor)
            {
                line.SetBaseLineColor(LineFinishBase.LineColor);
            }

            line.SetBaseLineStyle(LineFinishBase.VisioLineType.ToString());

            if (line.LineCompoundType == LineCompoundType.Double)
            {
                line.SetBaseLineWidth(2 * LineFinishBase.LineWidthInPts);
            }

            else
            {
                line.SetBaseLineWidth(LineFinishBase.LineWidthInPts);
            }


           // Visio.Shape visioShape = line.Shape.VisioShape;

            if (line.OriginatingDesignState == DesignState.Area || line.OriginatingDesignState == DesignState.Seam)
            {
                AreaDesignStateLayer.AddShape(line.Shape, 1);
                SeamDesignStateLayer.AddShape(line.Shape, 1);

                if (line.IsZeroLine)
                {
                    line.SetBaseLineStyle(FinishGlobals.ZeroLineBase.VisioLineType);
                }
            }

            else
            {


                //var visible = LineModeLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU;
            }

            LineDesignStateLayer.AddShape(line.Shape, 1);

            LineDesignStateLayer.SetLayerVisibility(!Filtered);

            UpdateFinishStats();
        }

        public void AddLine(CanvasDirectedLine canvasDirectedLine)
        {

            AddToLineDict(canvasDirectedLine);

            canvasDirectedLine.ucLine = LineFinishPaletteElement;
        }

        internal void RemoveLineFull(CanvasDirectedLine line)
        {

            AreaDesignStateLayer.RemoveShapeFromLayer(line.Shape, 1);
            LineDesignStateLayer.RemoveShapeFromLayer(line.Shape, 1);
            SeamDesignStateLayer.RemoveShapeFromLayer(line.Shape, 1);
            RemnantSeamDesignStateLayer.RemoveShapeFromLayer(line.Shape, 1);

            //--------------------------------------------------------//
            // The following check to see if the line is in the       //
            // dictionary should not be necessary, as it should       //
            // alwasy be at this point. But this is defensive until   //
            // the code gets cleaned up.                              //
            //--------------------------------------------------------//
            if (LineDictContains(line.Guid)) // MDD Reset
            {
                RemoveFromLineDict(line);
            }

            VisioInterop.DeleteShape(line.Shape);

            UpdateFinishStats();

        }

        internal void RemoveLine(CanvasDirectedLine line)
        {
            AreaDesignStateLayer.RemoveShapeFromLayer(line, 1);
            LineDesignStateLayer.RemoveShapeFromLayer(line, 1);
            SeamDesignStateLayer.RemoveShapeFromLayer(line, 1);
            RemnantSeamDesignStateLayer.RemoveShapeFromLayer(line, 1);

            //--------------------------------------------------------//
            // The following check to see if the line is in the       //
            // dictionary should not be necessary, as it should       //
            // alwasy be at this point. But this is defensive until   //
            // the code gets cleaned up.                              //
            //--------------------------------------------------------//
            if (LineDictContains(line.Guid)) // MDD Reset
            {
                RemoveFromLineDict(line);
            }

            UpdateFinishStats();


        }

        public void AddLineToLayer(string guid, DesignState designState, SeamMode seamMode)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return;
            }

            if (!LineDictContains(guid))
            {
                return;
            }

            CanvasDirectedLine canvasDirectedLine = GetLineFromLineDict(guid);

            switch (designState)
            {
                case DesignState.Area: AreaDesignStateLayer.AddShape(canvasDirectedLine.Shape, 1); return;
                case DesignState.Line: LineDesignStateLayer.AddShape(canvasDirectedLine.Shape, 1); return;
                case DesignState.Seam:

                    if (seamMode == SeamMode.Remnant)
                    {
                        RemnantSeamDesignStateLayer.AddShape(canvasDirectedLine.Shape, 1); return;
                    }

                    else
                    {
                        SeamDesignStateLayer.AddShape(canvasDirectedLine.Shape, 1); return;
                    }

                default: return;
            }
        }


        internal void SetLayerVisibility(DesignState lineArea)
        {
            SetLineState(lineArea, SystemState.SeamMode, Selected);
        }

        private void updateBaseLineColor(Color updtColor)
        {
            VisioLineColorFormula =
                string.Format("THEMEGUARD(RGB({0},{1},{2}))", updtColor.R, updtColor.G, updtColor.B);

            foreach (CanvasDirectedLine l in CanvasDirectedLines)
            {
                l.SetBaseLineColor(VisioLineColorFormula);
            }
        }

        private void updateBaseLineWidth(double updtWidthInPts)
        {
            foreach (CanvasDirectedLine l in CanvasDirectedLines)
            {
                l.SetBaseLineWidth((double)updtWidthInPts);
            }
        }

        private void updateBaseLineStyle(short visioLineStyle)
        {
            //VisioDashType = visioLineStyle;

            foreach (CanvasDirectedLine l in CanvasDirectedLines)
            {
                l.SetBaseLineStyle(VisioLineStyleFormula);
            }
        }

        public void Delete()
        {
            DeleteLayers();


            LineFinishBase.LineColorChanged -= LineFinishBase_LineColorChanged;

            LineFinishBase.LineTypeChanged -= LineFinishBase_LineTypeChanged;

            LineFinishBase.LineWidthChanged-= LineFinishBase_LineWidthChanged;

            LineFinishBase.FilteredChanged -= LineFinishBase_FilteredChanged;
        }

        public void DeleteLayers()
        {
            AreaDesignStateLayer.Delete();
            LineDesignStateLayer.Delete();
            SeamDesignStateLayer.Delete();
            RemnantSeamDesignStateLayer.Delete();
        }



        public void Dispose()
        {
           
        }
    }
}
