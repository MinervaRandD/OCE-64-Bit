
namespace CanvasShapes
{
    using Graphics;
    using FinishesLib;
    using PaletteLib;
    using Globals;
    using MaterialsLayout;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;
    using StatsGeneratorLib;
    using TracerLib;
    using OversUnders;
    using CanvasLib.Labels;
    using System.Drawing;
     using FloorMaterialEstimator.Finish_Controls;

    public class AreaFinishManager : IDisposable
    {
        public string Guid => AreaFinishBase.Guid;

        private GraphicsWindow window { get; }

        private GraphicsPage page { get; }

        public AreaFinishBaseList AreaFinishBaseList { get; set; }

        public AreaFinishBase AreaFinishBase { get; set; }

        public UCAreaFinishPaletteElement UCAreaPaletteElement => PalettesGlobal.AreaFinishPalette[Guid];

        public Dictionary<string, GraphicsLabel> GraphicsLabelDict { get; set; } = new Dictionary<string, GraphicsLabel>();

        public IEnumerable<GraphicsLabel> GraphicsLabels => GraphicsLabelDict.Values;

        public double RollWidthInInches
        {
            get
            {
                return AreaFinishBase.RollWidthInInches;
            }

            set
            {
                AreaFinishBase.RollWidthInInches = value;
            }
        }

        #region Layers

        //------------------------------------------------------------------------------------------------//
        // In the following, the layers are created on demand so as to reduce the total number of layers. //
        // It appears that too many layers slow the visio rendering process                               //
        //------------------------------------------------------------------------------------------------//

        public GraphicsLayerBase AreaDesignStateLayer => AreaFinishLayers.AreaDesignStateLayer;

        public GraphicsLayerBase SeamDesignStateLayer => AreaFinishLayers.SeamDesignStateLayer;

        public GraphicsLayerBase RemnantSeamDesignStateLayer => AreaFinishLayers.RemnantSeamDesignStateLayer;

        public GraphicsLayerBase AreaFinishLabelLayer => AreaFinishLayers.AreaFinishLabelLayer;

        #endregion

        public MaterialsType MaterialsType
        {
            get
            {
                return AreaFinishBase.MaterialsType;
            }

            set
            {
                AreaFinishBase.MaterialsType = value;
            }
        }

        public double RollRepeatWidthInInches => AreaFinishBase.RollRepeatWidthInInches;

        public double RollRepeatLengthInInches => AreaFinishBase.RollRepeatLengthInInches;

        public Dictionary<string, CanvasLayoutArea> CanvasLayoutAreaDict = new Dictionary<string, CanvasLayoutArea>();

        public bool LayoutAreaDictContains(CanvasLayoutArea canvasLayoutArea)
        {
            return LayoutAreaDictContains(canvasLayoutArea.Guid);
        }

        public bool LayoutAreaDictContains(string guid)
        {
            return CanvasLayoutAreaDict.ContainsKey(guid);
        }
        

        public void AddLabel(GraphicsLabel graphicsLabel)
        {
            graphicsLabel.Label.Guid = this.AreaFinishBase.Guid;
            this.GraphicsLabelDict.Add(graphicsLabel.Guid, graphicsLabel);
            AreaFinishLayers.AreaFinishLabelLayer.AddShapeToLayer(graphicsLabel.Shape, 1);
        }

        public void ResetLabelList()
        {
            var graphicLabels = new List<GraphicsLabel>(GraphicsLabelDict.Values);

            foreach (var graphicsLabel in graphicLabels)
            {
                RemoveLabel(graphicsLabel);
            }

            GraphicsLabelDict.Clear();
        }

        public void RemoveLabel(GraphicsLabel graphicsLabel)
        {
            AreaFinishLayers.AreaFinishLabelLayer.RemoveShapeFromLayer(graphicsLabel.Shape, 1);

            graphicsLabel.Delete();

            this.GraphicsLabelDict.Remove(graphicsLabel.Guid);
        }

        internal void DeleteShapes()
        {
            List<CanvasLayoutArea> layoutAreaList = new List<CanvasLayoutArea>(CanvasLayoutAreas);

            foreach (CanvasLayoutArea layoutArea in layoutAreaList)
            {
                CanvasLayoutAreaDict.Remove(layoutArea.Guid);
            }
        }

        public void AddLayoutArea(CanvasLayoutArea canvasLayoutArea)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { canvasLayoutArea });

            try
            {
                string guid = canvasLayoutArea.Guid;

                if (CanvasLayoutAreaDict.ContainsKey(guid))
                {
                    Tracer.TraceGen.TraceError("AreaFinishManager:AddLayoutArea attempt to add layout area that does already exist in CanvasLayoutAreaDict.");

                    return;
                }

                CanvasLayoutAreaDict.Add(guid, canvasLayoutArea);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("AreaFinishManager:AddLayoutArea throws an exception.", ex, 1, true);
            }
        }

        public void RemoveLayoutArea(CanvasLayoutArea canvasLayoutArea)
        {
            RemoveLayoutArea(canvasLayoutArea.Guid);


            this.AreaDesignStateLayer.RemoveShapeFromLayer(canvasLayoutArea, 1);
            this.SeamDesignStateLayer.RemoveShapeFromLayer(canvasLayoutArea, 1);
        }

        public void RemoveLayoutArea(string guid)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { guid });

            try
            {
                if (!CanvasLayoutAreaDict.ContainsKey(guid))
                {
                    Tracer.TraceGen.TraceError("AreaFinishManager:RemoveLayoutArea attempt to remove layout area that does not exist in CanvasLayoutAreaDict.");

                    return;
                }

                CanvasLayoutAreaDict.Remove(guid);

            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("AreaFinishManager:RemoveLayoutArea throws an exception.", ex, 1, true);
            }
        }

        public IEnumerable<CanvasLayoutArea> CanvasLayoutAreas => CanvasLayoutAreaDict.Values;

        public Dictionary<string, CanvasLayoutArea> RemnantCanvasLayoutAreaDict = new Dictionary<string, CanvasLayoutArea>();

        public List<VirtualOverage> VirtualOverageList { get; set; } = new List<VirtualOverage>();

        public List<VirtualUndrage> VirtualUndrageList { get; set; } = new List<VirtualUndrage>();

        public List<VirtualCut> VirtualCutsList { get; set; } = new List<VirtualCut>();

        public List<Cut> CutList { get; set; } = new List<Cut>();

        public List<Undrage> UndrageList { get; set; } = new List<Undrage>();

        public double SmallElementWidthInInches { get; set; }

        public double CutExtraElement { get; set; }

        public IEnumerable<GraphicsLayoutArea> RemanantCanvasLayoutAreas => RemnantCanvasLayoutAreaDict.Values;

        //private string _visioFillColorFormula = "THEMEGUARD(RGB(255,255,255))";

        //public string VisioFillColorFormula
        //{
        //    get
        //    {
        //        return _visioFillColorFormula;
        //    }

        //    set
        //    {
        //        _visioFillColorFormula = value;
        //    }
        //}

        //private string _visioFillTransparencyFormula = "100%";

        //public string VisioFillTransparencyFormula
        //{
        //    get
        //    {
        //        return _visioFillTransparencyFormula;
        //    }

        //    set
        //    {
        //        _visioFillTransparencyFormula = value;
        //    }
        //}

        /// <summary>
        /// Returns a boolean indicating that AT LEAST 1 canvas layout area has seams
        /// </summary>
        /// <returns></returns>
        public bool IsSeamed()
        {
            foreach (CanvasLayoutArea layoutArea in CanvasLayoutAreas)
            {
                if (layoutArea.IsSeamed())
                {
                    return true;
                }
            }

            return false;
        }

        public double GrossAreaInSqrInches
        {
            get
            {
                return AreaFinishBase.GrossAreaInSqrInches;
            }

            set
            {
                AreaFinishBase.GrossAreaInSqrInches = value;
            }
        }

        public int OverlapInInches
        {
            get
            {
                return AreaFinishBase.OverlapInInches;
            }

            set
            {
                AreaFinishBase.OverlapInInches = value;
            }
        }

        public int ExtraInchesPerCut
        {
            get
            {
                return AreaFinishBase.ExtraInchesPerCut;
            }

            set
            {
                AreaFinishBase.ExtraInchesPerCut = value;
            }
        }

        public double NetAreaInSqrInches
        {
            get
            {
                return AreaFinishBase.NetAreaInSqrInches;
            }

            set
            {
                AreaFinishBase.NetAreaInSqrInches = value;
            }
        }

        //public MaterialsType MaterialType { get; internal set; }

        public string AreaName => AreaFinishBase.AreaName;

        public AreaFinishLayers AreaFinishLayers { get; set; } = null;

        public bool Selected
        {
            get
            {
                return AreaFinishBase.Selected;
            }

            set
            {
                AreaFinishBase.Selected = value;
            }
        }

        public bool Filtered => AreaFinishBase.Filtered;

        public Color Color => AreaFinishBase.Color;

        public double Opacity => AreaFinishBase.Opacity;

        public SeamFinishBase SeamFinishBase => AreaFinishBase.SeamFinishBase;


        public List<MaterialArea> OversMaterialAreaList = new List<MaterialArea>();
        public List<MaterialArea> UndrsMaterialAreaList = new List<MaterialArea>();

        public void UpdateStats()
        {
            if (AreaFinishBase.MaterialsType == MaterialsType.Rolls)
            {
                StatsGeneratorLib.AreaStatsCalculator areaStatsCalculator = new AreaStatsCalculator(page.DrawingScaleInInches, AreaFinishBase.RollWidthInInches, AreaFinishBase.NetAreaInSqrInches);

                areaStatsCalculator.UpdateRollStats(
                    CutList, UndrageList, VirtualOverageList, VirtualUndrageList, SmallElementWidthInInches, CutExtraElement);

                AreaFinishBase.GrossAreaInSqrInches = areaStatsCalculator.TotalGrossAreaInSquareFeet / 144.0;
            }

            else if (AreaFinishBase.MaterialsType == MaterialsType.Tiles)
            {
                StatsGeneratorLib.AreaStatsCalculator areaStatsCalculator = new AreaStatsCalculator(page.DrawingScaleInInches, AreaFinishBase.RollWidthInInches, AreaFinishBase.NetAreaInSqrInches);

                areaStatsCalculator.UpdateTileStats(AreaFinishBase.PerimeterInInches, AreaFinishBase.TrimInInches, AreaFinishBase.NetAreaInSqrInches);

                AreaFinishBase.GrossAreaInSqrInches = areaStatsCalculator.TotalGrossAreaInSquareFeet / 144.0;
            }
        }

        internal void UpdateFinishStats()
        {
            double grossAreaInSqrInches = CanvasLayoutAreas.Where(a => a.OffspringAreas.Count <= 0 && !a.IsZeroAreaLayoutArea).Sum(a => a.GrossAreaInSqrInches()); ;
            double netAreaInSqrInches = CanvasLayoutAreas.Where(a => a.OffspringAreas.Count <= 0 && !a.IsZeroAreaLayoutArea).Sum(a => a.NetAreaInSqrInches());
            double perimeterInInches = CanvasLayoutAreas.Where(a => a.ParentArea == null && !a.IsBorderArea).Sum(p => p.PerimeterLengthIncludingZeroLines());

            int count = CanvasLayoutAreas.Count(a =>
                    a.LayoutAreaType != LayoutAreaType.OversGenerator
                    && a.LayoutAreaType != LayoutAreaType.ZeroArea
                    && !a.IsSubdivided()
                    && a.IsVisible);

            double? wasteRatio = 0.0;

            if (this.MaterialsType == MaterialsType.Rolls)
            {
                wasteRatio = calculateRollWasteFactor(netAreaInSqrInches, perimeterInInches, AreaFinishBase.RollWidthInInches);
            }

            else if (this.MaterialsType == MaterialsType.Tiles)
            {
                wasteRatio = calculateTileWasteFactor(netAreaInSqrInches, perimeterInInches);
            }

            grossAreaInSqrInches = netAreaInSqrInches;

            if (wasteRatio.HasValue)
            {
                grossAreaInSqrInches *= (1.0 + wasteRatio.Value);
            }

            AreaFinishBase.UpdateFinishStats(count, netAreaInSqrInches, grossAreaInSqrInches, perimeterInInches, wasteRatio);
        }

        private double? calculateRollWasteFactor(double netAreaInSqrInches, double perimeterInInches, double rollWidthInInches)
        {
            OversMaterialAreaList.Clear();
            UndrsMaterialAreaList.Clear();

            RolloutStatsCalculator rolloutStatsCalculator = new RolloutStatsCalculator(page.DrawingScaleInInches, rollWidthInInches);

            throw new NotImplementedException();

            // Fix the following
            double? wasteRatio = null;// rolloutStatsCalculator.CalculateRolloutStats(CanvasLayoutAreas);

            OversMaterialAreaList.AddRange(rolloutStatsCalculator.OversMaterialAreaList);
            UndrsMaterialAreaList.AddRange(rolloutStatsCalculator.UndrsMaterialAreaList);

            if (!SystemState.ScaleHasBeenSet)
            {
                return null;
            }

            if (netAreaInSqrInches <= 0)
            {
                return null;
            }

            return wasteRatio;
        }

        private double? calculateTileWasteFactor(double netAreaInSqrInches, double perimeterInInches)
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                return null;
            }

            if (netAreaInSqrInches <= 0)
            {
                return null;
            }

            double trimInInches = AreaFinishBase.TrimInInches;

            double perimeterInLinearFeet = perimeterInInches / 12.0;

            double waste = (trimInInches / 12.0) * perimeterInLinearFeet;

            double netAreaInSqrFeet = netAreaInSqrInches / 144.0;

            double wasteRatio = (netAreaInSqrFeet + waste) / netAreaInSqrFeet - 1.0;

            return wasteRatio;
        }

        public bool CanvasAreasAreSeamed()
        {
            foreach (CanvasLayoutArea canvasLayoutArea in CanvasLayoutAreas)
            {
                if (canvasLayoutArea.HasSeamsOrRollouts)
                {
                    return true;
                }
            }

            return false;
        }

        public void UpdateCutIndices()
        {
            int cutIndex = 1;
            int overIndex = 1;
            int undrIndex = 1;

            foreach (CanvasLayoutArea canvasLayoutArea in this.CanvasLayoutAreas)
            {
                canvasLayoutArea.UpdateCutOverAndUnderIndices(ref cutIndex, ref overIndex, ref undrIndex);
            }
        }

        public AreaFinishManager(
            GraphicsWindow window
            , GraphicsPage page
            , AreaFinishBaseList areaFinishBaseList
            , AreaFinishBase areaFinishBase)
        {
            this.window = window;

            this.page = page;

            this.AreaFinishBaseList = areaFinishBaseList;

            this.AreaFinishBase = areaFinishBase;

            this.AreaFinishBase.FinishSeamChanged += AreaFinishBase_FinishSeamChanged;

            this.AreaFinishBase.FinishSeamGraphicsChanged += AreaFinishBase_FinishSeamGraphicsChanged;

            this.AreaFinishBase.FilteredChanged += AreaFinishBase_FilteredChanged;

            this.AreaFinishBase.ColorChanged += AreaFinishBase_ColorChanged;

            this.AreaFinishBase.RollWidthChanged += AreaFinishBase_RollWidthChanged;

            this.AreaFinishBase.MaterialsTypeChanged += AreaFinishBase_MaterialsTypeChanged;

            this.AreaFinishBase.TileTrimChanged += AreaFinishBase_TileTrimChanged;

            this.AreaFinishLayers = new AreaFinishLayers(window, page, areaFinishBase.AreaName);
        }

        private void AreaFinishBase_TileTrimChanged(AreaFinishBase finishBase, double tileTrimInInches)
        {
            UpdateFinishStats();
        }

        private void AreaFinishBase_MaterialsTypeChanged(AreaFinishBase finishBase, MaterialsType materialsType)
        {
            RolloutStatsCalculator rolloutStatsCalculator = new RolloutStatsCalculator(page, AreaFinishBase);
            rolloutStatsCalculator.CalculateRolloutStats(CanvasLayoutAreas);
        }

        private void AreaFinishBase_RollWidthChanged(AreaFinishBase finishBase, double rollWidthInInches)
        {
            RolloutStatsCalculator rolloutStatsCalculator = new RolloutStatsCalculator(page, AreaFinishBase);
            rolloutStatsCalculator.CalculateRolloutStats(CanvasLayoutAreas);

            bool updateRequired = false;

            foreach (CanvasLayoutArea canvasLayoutArea in CanvasLayoutAreas)
            {
                if (canvasLayoutArea.HasSeamsOrRollouts)
                {
                    updateRequired = true;
                }
            }

            if (updateRequired)
            {
                SystemState.BtnRedoSeamsAndCuts.Image = SystemGlobals.RedoSeamsOnImage;
            }
        }

        private void AreaFinishBase_ColorChanged(AreaFinishBase finishBase, Color color)
        {
            UpdateBaseColor(color);
            UpdateOpacity(color);
        }

        private void AreaFinishBase_FilteredChanged(AreaFinishBase finishBase, bool filtered)
        {
            SetDesignStateFormat(SystemState.DesignState, SystemState.SeamMode, filtered, SystemState.ShowAreas);

            //SetupSeamFilters();
        }

        private void AreaFinishBase_FinishSeamGraphicsChanged(AreaFinishBase finishBase, SeamFinishBase seamFinishBase)
        {
            updateSeamGraphics();
        }

        private void AreaFinishBase_FinishSeamChanged(AreaFinishBase finishBase, SeamFinishBase seamFinishBase)
        {
            updateSeamGraphics();
        }

        private void updateSeamGraphics()
        {
            foreach (CanvasLayoutArea layoutArea in this.CanvasLayoutAreas)
            {
                layoutArea.UpdateSeamGraphics(AreaFinishBase.SeamFinishBase, AreaFinishBaseList.SelectedItem == AreaFinishBase);
            }
        }

        public void SetCutIndexCircleVisibility()
        {
            foreach (CanvasLayoutArea layoutArea in this.CanvasLayoutAreas)
            {
                layoutArea.SetCutIndexCircleVisibility();
            }
        }


        #region Design State Management

        public void SetDesignStateFormat(DesignState designState, bool showAreas)
        {
            // At this point, the following simply changes the layer visibility. Eventually, everything below show be
            // moved to AreaFinishManager

            this.SetDesignStateFormat(designState, SeamMode.Unknown, this.Filtered, showAreas);

            // The following should eventually be moved to AreaFinishManager;

            if (designState == DesignState.Area)
            {
                foreach (CanvasLayoutArea layoutArea in this.CanvasLayoutAreas)
                {
                    layoutArea.SetAreaDesignStateFormat(false);
                }
            }

            else if (designState == DesignState.Line)
            {
                foreach (CanvasLayoutArea layoutArea in this.CanvasLayoutAreas)
                {
                    layoutArea.SetLineDesignStateFormat();
                }
            }

            else if (designState == DesignState.Seam)
            {
                foreach (CanvasLayoutArea layoutArea in this.CanvasLayoutAreas)
                {
                    layoutArea.SetSeamDesignStateFormat(SystemState.SeamMode);
                }

            }
        }

        public void UpdateBaseColor(Color updtColor)
        {
            string visioFillColorFormula =
                string.Format("THEMEGUARD(RGB({0},{1},{2}))", updtColor.R, updtColor.G, updtColor.B);

            foreach (CanvasLayoutArea canvasLayoutArea in CanvasLayoutAreas)
            {
                canvasLayoutArea.SetFillColor(visioFillColorFormula);
            }
        }

        public void UpdateOpacity(Color updtColor)
        {
            double opacity = 100.0 - Math.Max(Math.Min(100.0 * ((double)AreaFinishBase.Color.A) / 255.0, 100.0), 0.0);

            string visioFillTransparencyFormula = opacity.ToString("0.00000000") + "%";

            foreach (CanvasLayoutArea canvasLayoutArea in CanvasLayoutAreas)
            {
                canvasLayoutArea.SetFillTransparancy(visioFillTransparencyFormula);
            }
        }

        public void SetDesignStateFormat(DesignState designState, SeamMode seamMode, bool filtered, bool showAreas)
        {
            if (filtered || !showAreas)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(false);

                AreaFinishLayers.RemnantSeamDesignStateLayer.SetLayerVisibility(false);
                AreaFinishLayers.AreaFinishLabelLayer.SetLayerVisibility(false);

                return;
            }

            if (designState == DesignState.Area)
            {
                AreaDesignStateLayer.SetLayerVisibility(true);
                SeamDesignStateLayer.SetLayerVisibility(false);

                AreaFinishLayers.RemnantSeamDesignStateLayer.SetLayerVisibility(false);

                foreach (CanvasLayoutArea layoutArea in this.CanvasLayoutAreas)
                {
                    if (layoutArea.ParentArea is null)
                    {
                        layoutArea.SetAreaDesignStateFormat(/*SystemState.AreaMode*/);
                    }
                }

            }

            else if (designState == DesignState.Line)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(false);

                AreaFinishLayers.RemnantSeamDesignStateLayer.SetLayerVisibility(false);
                AreaFinishLayers.AreaFinishLabelLayer.SetLayerVisibility(false);

                foreach (CanvasLayoutArea layoutArea in this.CanvasLayoutAreas)
                {
                    if (!layoutArea.IsSubdivided())
                    {
                        layoutArea.SetLineDesignStateFormat(/*SystemState.LineMode*/);
                    }
                }
            }

            else if (designState == DesignState.Seam)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(true);

                foreach (CanvasLayoutArea layoutArea in this.CanvasLayoutAreas)
                {
                    if (!layoutArea.IsSubdivided())
                    {
                        layoutArea.SetSeamDesignStateFormat(SystemState.SeamMode);
                    }
                }

                if (seamMode == SeamMode.Remnant)
                {
                    AreaFinishLayers.RemnantSeamDesignStateLayer.SetLayerVisibility(true);
                }

                else
                {
                    AreaFinishLayers.RemnantSeamDesignStateLayer.SetLayerVisibility(false);
                }
            }
        }

        public void SetupSeamModeDesignState(string selectedFinishGuid)
        {
            if (Guid != selectedFinishGuid || this.AreaFinishBase.MaterialsType != MaterialsType.Rolls)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(false);
            }

            else
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(true);
            }

            foreach (CanvasLayoutArea layoutArea in CanvasLayoutAreas)
            {
              
                layoutArea.SetLineGraphics(
                    SystemState.DesignState,
                    layoutArea.SeamDesignStateSelectionModeSelected && (layoutArea.AreaFinishManager.Guid == selectedFinishGuid), AreaShapeBuildStatus.Completed);

                layoutArea.SetSeamLineGraphics(SystemState.DesignState, layoutArea.AreaFinishManager.Guid == selectedFinishGuid);

                //layoutArea.SetSeamTagIndexVisibility(layoutArea.AreaFinishManager.Guid == selectedFinishGuid);
            }

        }

        #endregion

        internal void AddNormalLayoutAreaSeamStateOnly(CanvasLayoutArea canvasLayoutArea, bool updateColor = true)
        {
            addNormalLayoutArea(canvasLayoutArea, updateColor);

            canvasLayoutArea.AddToLayer(this.SeamDesignStateLayer.GetBaseLayer());
        }

        internal void AddNormalLayoutAreaAreaStateOnly(CanvasLayoutArea canvasLayoutArea, bool updateColor = true)
        {
            addNormalLayoutArea(canvasLayoutArea, updateColor);

            this.AreaDesignStateLayer.AddShapeToLayer(canvasLayoutArea, 1);
            //canvasLayoutArea.AddToLayer(AreaDesignStateLayer);
        }

        internal void AddNormalLayoutArea(CanvasLayoutArea canvasLayoutArea, bool recursive = true, bool updateColor = true)
        {
            addNormalLayoutArea(canvasLayoutArea, updateColor);

            //canvasLayoutArea.AddToLayer(AreaFinishManager.SeamDesignStateLayer.GetBaseLayer());

            this.SeamDesignStateLayer.AddShapeToLayer(canvasLayoutArea, 1);
            this.AreaDesignStateLayer.AddShapeToLayer(canvasLayoutArea, 1);
            //canvasLayoutArea.AddToLayer(AreaDesignStateLayer);

            if (!recursive)
            {
                return;
            }

            if (canvasLayoutArea.OffspringAreas is null)
            {
                return;
            }

            foreach (CanvasLayoutArea offspringArea in canvasLayoutArea.OffspringAreas)
            {
                AddNormalLayoutArea(offspringArea, recursive, updateColor);
            }
        }

        private void addNormalLayoutArea(CanvasLayoutArea canvasLayoutArea, bool updateColor = true)
        {
            this.AddLayoutArea(canvasLayoutArea);

           // canvasLayoutArea.UCAreaFinish = this.UCAreaPaletteElement;

            VisioInterop.SetShapeData(canvasLayoutArea.PolygonInternalArea, "Layout Area", "Compound Shape[" + this.AreaName + "]", canvasLayoutArea.Guid);
            //canvasLayoutArea.PolygonInternalArea.VisioShape.Data2 = this.AreaName;
            //canvasLayoutArea.PolygonInternalArea.VisioShape.Data3 = canvasLayoutArea.Guid;

            // MDD Reset

            //canvasLayoutArea.PolygonInternalArea.ShowShapeOutline(false); // MDD Reset

            //----------------------------------------------------------------------------------------------------//
            // The following may be redundant because the AreaFinishManager is set in the constructor (hopefully) //
            //----------------------------------------------------------------------------------------------------//

            canvasLayoutArea.AreaFinishManager = this;

            if (updateColor)
            {
                canvasLayoutArea.SetFillColor(this.AreaFinishBase.Color);
                canvasLayoutArea.SetFillOpacity(this.AreaFinishBase.Opacity);
            }

            // The following contributes significantly to slow load time, so it has been commented out.

            //canvasLayoutArea.ExternalArea.BringPerimeterToFront();

            canvasLayoutArea.DrawSeams();

            canvasLayoutArea.DrawCutsOversAndUndrs();

            if (canvasLayoutArea.OriginatingDesignState == DesignState.Area)
            {
                //areaDesignStateLayer.Add(canvasLayoutArea.PolygonInternalArea, 1);

                foreach (CanvasDirectedPolygon internalPolygon in canvasLayoutArea.InternalAreas)
                {
                    internalPolygon.BringPerimeterToFront();
                }
            }

            this.UpdateFinishStats();

            if (!(canvasLayoutArea.SeamIndexTag is null))
            {
                canvasLayoutArea.SeamIndexTag.BringToFront();
            }
        }

        internal void AddRemnantLayoutArea(CanvasLayoutArea canvasLayoutArea)
        {
            this.AddLayoutArea(canvasLayoutArea);

            // this.CanvasLayoutAreaDict.Add(canvasLayoutArea.Guid, canvasLayoutArea);

            //canvasLayoutArea.UCAreaFinish = this.UCAreaPaletteElement;

            canvasLayoutArea.PolygonInternalArea.VisioShape.Data1 = this.AreaName;
            canvasLayoutArea.PolygonInternalArea.VisioShape.Data3 = canvasLayoutArea.Guid;

            canvasLayoutArea.PolygonInternalArea.ShowShapeOutline(false);

            canvasLayoutArea.SetFillColor(this.AreaFinishBase.Color);
            canvasLayoutArea.SetFillOpacity(this.AreaFinishBase.Opacity);

            //canvasLayoutArea.ExternalArea.BringPerimeterToFront();


            this.AreaFinishLayers.RemnantSeamDesignStateLayer.AddShapeToLayer(canvasLayoutArea.PolygonInternalArea, 1);

            canvasLayoutArea.DrawSeams();

            window?.DeselectAll();

            if (!(canvasLayoutArea.SeamIndexTag is null))
            {
                canvasLayoutArea.SeamIndexTag.BringToFront();
            }
        }

        internal void RemoveLayoutArea(CanvasLayoutArea canvasLayoutArea, bool updateFinishStats = true)
        {
            this.RemoveLayoutArea(canvasLayoutArea.Guid);

            canvasLayoutArea.DeleteRollouts();
            canvasLayoutArea.DeleteSeams();

            Shape shape = canvasLayoutArea.Shape;

            if (Utilities.IsNotNull(shape))
            {
                shape.SetShapeData(string.Empty, string.Empty, string.Empty);
                
                //Visio.Shape visioShape = shape.VisioShape;

                //if (Utilities.IsNotNull(visioShape))
                //{
                //    visioShape.Data1 = string.Empty;
                //    visioShape.Data3 = string.Empty;
                //}

                this.AreaFinishLayers.AreaDesignStateLayer.RemoveShapeFromLayer(canvasLayoutArea, 1);
                this.AreaFinishLayers.SeamDesignStateLayer.RemoveShapeFromLayer(shape, 1);

                //shape.Delete();
            }

            //canvasLayoutArea.UndrawSeams();

            if (updateFinishStats)
            {
                this.UpdateFinishStats();
            }
        }

        internal void RemoveLayoutAreaFromFinish(CanvasLayoutArea canvasLayoutArea, bool updateFinishStats = true)
        {
            this.RemoveLayoutArea(canvasLayoutArea.Guid);

            canvasLayoutArea.DeleteRollouts();
            canvasLayoutArea.DeleteSeams();

            Shape shape = canvasLayoutArea.Shape;

            if (Utilities.IsNotNull(shape))
            {
                this.AreaFinishLayers.AreaDesignStateLayer.RemoveShapeFromLayer(canvasLayoutArea, 1);
                this.AreaFinishLayers.SeamDesignStateLayer.RemoveShapeFromLayer(canvasLayoutArea, 1);
            }

            if (updateFinishStats)
            {
                this.UpdateFinishStats();
            }
        }

        internal void DeleteLayers()
        {
            AreaDesignStateLayer.Delete();
            SeamDesignStateLayer.Delete();
        }

        public void Delete()
        {
            page.RemoveFromGraphicsLayerDict(SeamDesignStateLayer.GetBaseLayer());

            //AreaDesignStateLayer.Delete0();
            SeamDesignStateLayer.Delete();

            AreaFinishLayers.Delete();

            this.Dispose();
        }

        public void Dispose()
        {
            this.AreaFinishBase.FinishSeamChanged -= AreaFinishBase_FinishSeamChanged;

            this.AreaFinishBase.FinishSeamGraphicsChanged -= AreaFinishBase_FinishSeamGraphicsChanged;

            this.AreaFinishBase.FilteredChanged -= AreaFinishBase_FilteredChanged;

            this.AreaFinishBase.ColorChanged -= AreaFinishBase_ColorChanged;

            this.AreaFinishBase.RollWidthChanged -= AreaFinishBase_RollWidthChanged;

            this.AreaFinishBase.MaterialsTypeChanged -= AreaFinishBase_MaterialsTypeChanged;

        }

        internal void SetupNormalSeamsLayersForSelectedFinish(bool showSeams)
        {
            AreaFinishLayers.NormalSeamsLayer.SetLayerVisibility(this.Selected && showSeams && !this.Filtered);
        }

        internal void SetupNormalSeamsHideableLayersForSelectedFinish(bool showSeams)
        {
            AreaFinishLayers.NormalSeamsUnhideableLayer.SetLayerVisibility(Selected && showSeams && !this.Filtered);
        }

        internal void SetupManualSeamsLayersForSelectedFinish(bool showSeams)
        {
            AreaFinishLayers.ManualSeamsAllLayer.SetLayerVisibility(Selected && showSeams && !this.Filtered);
        }

        internal void SetupCutsLayersForSelectedFinish(bool showCuts)
        {
            AreaFinishLayers.CutsLayer.SetLayerVisibility(Selected && showCuts && !this.Filtered);
        }

        internal void SetupCutIndicesLayersForSelectedFinish(bool showCutIndices)
        {
            AreaFinishLayers.CutsIndexLayer.SetLayerVisibility(Selected && showCutIndices && !this.Filtered);

            AreaFinishLabelLayer.SetLayerVisibility(Selected && showCutIndices && !this.Filtered);
        }

        internal void SetupOversLayersForSelectedFinish(bool showOvers)
        {
            AreaFinishLayers.OversLayer.SetLayerVisibility(Selected && showOvers && !this.Filtered);
        }

        internal void SetupUndrsLayersForSelectedFinish(bool showUndrs)
        {
            AreaFinishLayers.UndrsLayer.SetLayerVisibility(Selected && showUndrs && !this.Filtered);
        }

        internal void SetupEmbdCutsLayerForSelectedFinish(bool showEmbdCuts)
        {
            AreaFinishLayers.EmbdCutsLayer.SetLayerVisibility(Selected && showEmbdCuts && !this.Filtered);
        }

        internal void SetupEmbdOverLayerForSelectedFinish(bool showEmbdOvers)
        {
            AreaFinishLayers.EmbdOverLayer.SetLayerVisibility(Selected && showEmbdOvers && !this.Filtered);
        }

        internal void SetupAllSeamLayers()
        {
            if (SystemState.DesignState == DesignState.Area)
            {
                AreaFinishLayers.SetupAllSeamLayers(
                    true
                    , Selected
                    , Filtered
                    , SystemState.AreaModeAutoSeamsShowAll
                    , SystemState.AreaModeAutoSeamsShowUnHideable
                    , SystemState.SeamModeManualSeamsShowAll
                    , false
                    , SystemState.ShowAreaModeCutIndices
                    , false
                    , false
                    , false
                    , false);

                return;
            }

            if (SystemState.DesignState == DesignState.Line)
            {
                AreaFinishLayers.SetupAllSeamLayers(
                    false // Not in area design state
                    , Selected
                    , Filtered
                    , false
                    , false
                    , false
                    , false
                    , false
                    , false
                    , false
                    , false
                    , false);

                return;
            }

            if (SystemState.DesignState == DesignState.Seam)
            {
                AreaFinishLayers.SetupAllSeamLayers(
                    false // Not in area design state
                    , Selected
                    , Filtered
                    , SystemState.SeamModeAutoSeamsShowAll
                    , SystemState.SeamModeAutoSeamsShowAll
                    , SystemState.SeamModeManualSeamsShowAll
                    , SystemState.ShowSeamModeCuts
                    , SystemState.ShowSeamModeCutIndices
                    , SystemState.ShowSeamModeOvers
                    , SystemState.ShowSeamModeUndrs
                    , SystemState.ShowEmbeddedCuts
                    , SystemState.ShowEmbeddedOvers);

                return;
            }
        }

        public double TileWidthInInches
        {
            get
            {
                return AreaFinishBase.TileWidthInInches;
            }

            set
            {
                AreaFinishBase.TileWidthInInches = value;
            }
        }


        public double TileHeightInInches
        {
            get
            {
                return AreaFinishBase.TileHeightInInches;
            }

            set
            {
                AreaFinishBase.TileHeightInInches = value;
            }
        }

        public SeamFinishBase FinishSeamBase
        {
            get
            {
                return AreaFinishBase.SeamFinishBase;
            }

            set
            {
                AreaFinishBase.SeamFinishBase = value;
            }
        }

    }
}
