//-------------------------------------------------------------------------------//
// <copyright file="CanvasLayoutArea.cs"                                         //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using Geometry;
    using Graphics;
    using MaterialsLayout;

    using FloorMaterialEstimator.Finish_Controls;
    using FinishesLib;
    using PaletteLib;
    using SettingsLib;
    using Utilities;
    using Globals;
    using MaterialsLayout.MaterialsLayout;
    using CanvasLib.Markers_and_Guides;
    using DebugSupport;
    using TracerLib;
    using StatsGeneratorLib;
    using global::CanvasManager.CanvasShapes;
    using System.Security.Policy;
    using System.Runtime.CompilerServices;
    using Microsoft.Office.Interop.Visio;
    using Color = System.Drawing.Color;

    /// <summary>
    /// CanvasLayoutArea is the primary object type of this application.
    /// It represents a distinct area on the drawing that is being defined for finish and seaming.
    /// </summary>
    [Serializable]
    public partial class CanvasLayoutArea : GraphicsLayoutArea, IAreaShape
    {
        // Elements not equivalent to graphics layout area.

        CanvasManager canvasManager;

        // Elements matching graphics layout area

        //**********************************************************************************************//
        //                                                                                              //
        //                           Links between parent and offspring areas.                          //
        //                                                                                              //
        //**********************************************************************************************//

        public string fileLocation = "FloorMaterialEstistimator.CanvasManager.CanvasLayoutArea";

        public CanvasPage CurrentPage
        {
            get;
            set;
        }

        public CanvasLayoutArea _parentArea = null;

        public new CanvasLayoutArea ParentArea
        {
            get
            {
                return _parentArea;
            }

            set
            {

                _parentArea = value;
            }
        }

        public new List<CanvasLayoutArea> OffspringAreas = new List<CanvasLayoutArea>();


        public void AddToOffspringList(CanvasLayoutArea canvasLayoutArea)
        {
            this.OffspringAreas.Add(canvasLayoutArea);

            base.OffspringAreas.Add(canvasLayoutArea);
        }

        private void ClearOffspringAreas()
        {
            OffspringAreas.Clear();

            base.OffspringAreas.Clear();
        }


        internal new bool IsSubdivided()
        {
            if (this.OffspringAreas == null)
            {
                return false;
            }

            if (this.OffspringAreas.Count <= 0)
            {
                return false;
            }

            return true;
        }

        internal new bool IsSeamed()
        {
            return HasSeamsOrRollouts;
        }

        internal bool IsAutomaticSeamed()
        {
            if (RolloutList.Count > 0)
            {
                return true;
            }

            if (SeamList.Any(x => x.SeamType == SeamType.Basic))
            {
                return true;
            }
            
            return false;

            //return SeamList.Any(x => x.SeamType == SeamType.Basic);
        }
        private FinishesLibElements _finishesLibElements = null;

        public FinishesLibElements FinishesLibElements
        {
            get
            {
                return _finishesLibElements;
            }

            set
            {
                if (value == _finishesLibElements)
                {
                    return;
                }

                _finishesLibElements = value;

                base.FinishLibElements = _finishesLibElements;
            }
        }

        AreaFinishLayers areaFinishLayers => this.AreaFinishManager.AreaFinishLayers;

       // public SeamStateAreaLockIcon SeamStateAreaLockIcon { get; set; } = null;


        public GraphicShape DrawCompositeShape(GraphicsWindow window, GraphicsPage page, bool drawPolygonOnly = false)
        {

            //GraphicsDebugSupportRoutines.CheckForInvalidVisioShapeInPageShapeDict(page);

            if (ExternalArea is null)
            {
                CompositeShape = null;
                return null;
            }

            ExternalArea.UCAreaFinish = this.AreaFinishManager.UCAreaPaletteElement;

            this.Shape = ExternalArea.Draw(page, this.AreaFinishManager.Color, drawPolygonOnly);
            this.Shape.ShapeType |= ShapeType.LayoutArea;
            this.Shape.ParentObject = this;
            Page.AddToPageShapeDict(this.Shape);

            //GraphicsDebugSupportRoutines.CheckForInvalidVisioShapeInPageShapeDict(page);

            foreach (CanvasDirectedLine externalPerimeterLine in ExternalArea)
            {
                Page.AddToPageShapeDict(externalPerimeterLine.Shape);

                externalPerimeterLine.SetShapeData();
                //VisioInterop.SetShapeData(externalPerimeterLine.Shape, "External Perimeter Line", "Line [" + ExternalArea.Perimeter[0].ucLine.LineName + "]", externalPerimeterLine.Guid);
            }

            // GraphicsDebugSupportRoutines.CheckForInvalidVisioShapeInPageShapeDict(page);

            ExternalArea.SetFillOpacity(this.AreaFinishManager.Opacity);

            CompositeShape = Shape;

            if (IsSeamed())
            {
                if (SeamIndexTag != null)
                {
                    SeamIndexTag.Draw();
                    areaFinishLayers.SeamAreaIndexLayer.AddShape(SeamIndexTag, 1);
                    //this.AreaFinishManager.SeamDesignStateLayer.AddShape(SeamIndexTag.Shape, 1);
                }
            }

            if (internalAreas.Count <= 0)
            {
                return this.Shape;
            }

            List<GraphicShape> shapes = new List<GraphicShape>();

            Color lineColor = ExternalArea.Perimeter[0].LineFinishManager.LineColor; // This is a heuristic to select the color of the first line, since there may be multiple line times on the perimeter.

            // GraphicsDebugSupportRoutines.CheckForInvalidVisioShapeInPageShapeDict(page);

            foreach (CanvasDirectedPolygon internalArea in internalAreas)
            {
                GraphicShape internalAreaShape = internalArea.Draw(lineColor, Color.FromArgb(0, 255, 255, 255));

                internalAreaShape.Data1 = "[Internal Area]";

                internalAreaShape.Guid = internalArea.Guid;

                Page.AddToPageShapeDict(internalArea);

                shapes.Add(internalAreaShape);
            }

            //GraphicsDebugSupportRoutines.CheckForInvalidVisioShapeInPageShapeDict(page);

            GraphicShape originalShape = ExternalArea.Shape;

            GraphicShape shape = VisioGeometryEngine.Subtract((GraphicsLayoutArea)this, window, page, ExternalArea.Shape, shapes);

            // originalShape.VisioShape = null;

            page.RemoveFromPageShapeDict(originalShape);

            originalShape.Delete(false);


            foreach (GraphicShape graphicShape in shapes)
            {
                page.RemoveFromPageShapeDict(graphicShape);
                graphicShape.Delete(false);
            }
            // GraphicsDebugSupportRoutines.CheckForInvalidVisioShapeInPageShapeDict(page);

            ExternalArea.PolygonInternalArea = shape;

            this.Shape = ExternalArea.Shape;

            shape.Data1 = "[Layout Area]";
            shape.Data3 = this.Guid;

            // GraphicsDebugSupportRoutines.CheckForInvalidVisioShapeInPageShapeDict(page);

            page.AddToPageShapeDict(shape);

            return shape;
        }

    
        internal void AddAssociatedLines(GraphicsWindow window, GraphicsPage page, string lineName)
        {
            ExternalArea.AddAssociatedLines(window, page, lineName);

            foreach (CanvasDirectedPolygon internalArea in internalAreas)
            {
                internalArea.AddAssociatedLines(window, page, lineName);
            }

        }

        public void SetCutIndexCircleVisibility()
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });
#endif
            if (this.LayoutAreaType != LayoutAreaType.Normal)
            {
                //----------------------------------------------------//
                // Assumes only normal layout areas have cut indicies //
                //----------------------------------------------------//

                return;
            }

            if (this.GraphicsOverageList is null)
            {
                return;
            }

            if (this.GraphicsOverageList.Count <= 0)
            {
                return;
            }

            foreach (GraphicsCut graphicsCut in this.GraphicsCutList)
            {
                if (graphicsCut.GraphicsCutIndex is null)
                {
                    Tracer.TraceGen.TraceError("CanvasLayourError:SetCutIndexCircleVisibility has graphics cut without cut index.", 1, true);

                    continue;
                }

                graphicsCut.GraphicsCutIndex.SetCircleVisibility();
            }
        }

        public CopyMarker CopyMarker { get; set; } = null;

        public VertexEditMarker VertexEditMarker { get; set; } = null;

        internal void SetCopyMarker(double x, double y)
        {
            Coordinate closestVertex = GetClosestVertex(x, y);

            CopyMarker = new CopyMarker(this.Window, this.Page, closestVertex.X, closestVertex.Y, 0.125);

            GraphicShape copyMarkerShape = CopyMarker.Draw(GlobalSettings.AreaEditSettingColor2);

            copyMarkerShape.SetShapeData("[Copy Marker]", "Compound Shape[" + this.AreaFinishBase.AreaName + "]", CopyMarker.Guid);

            copyMarkerShape.ShapeType = ShapeType.CopyMarker;

            Page.AddToPageShapeDict(CopyMarker);

            this.AreaFinishManager.AreaDesignStateLayer.AddShape(CopyMarker, 1);

           copyMarkerShape.AddToLayerSet(this.AreaFinishManager.AreaDesignStateLayer);
        }


        private new CanvasDirectedPolygon _externalArea;

        public new CanvasDirectedPolygon ExternalArea
        {
            get
            {
                return _externalArea;
            }

            set
            {
                _externalArea = value;

                base.ExternalArea = (GraphicsDirectedPolygon)_externalArea;
            }
        }

        private List<CanvasDirectedPolygon> internalAreas
        {
            get;
            set;
        } = new List<CanvasDirectedPolygon>();

        public new List<CanvasDirectedPolygon> InternalAreas
        {
            get
            {
                return internalAreas;
            }

            set
            {
                internalAreas = value;
                base.InternalAreas = internalAreas.Select(i => (GraphicsDirectedPolygon)i).ToList();
            }
        }

        private void InternalAreasAdd(CanvasDirectedPolygon canvasDirectedPolygon)
        {
            if (InternalAreas == null)
            {
                InternalAreas = new List<CanvasDirectedPolygon>();
            }

            InternalAreas.Add(canvasDirectedPolygon);

            base.InternalAreasAdd((GraphicsDirectedPolygon)canvasDirectedPolygon);

        }

        public void DeleteSeams()
        {
            foreach (CanvasSeam canvasSeam in CanvasSeamList)
            {
                canvasSeam.RemoveFromSeamLayer();
                canvasSeam.Delete();
            }

            CanvasSeamList.ClearBase();
            GraphicsSeamList.ClearBase();
            SeamList.ClearBase();
        }

        public void DeleteNonManualSeams()
        {
            List<CanvasSeam> seamsToRemove = new List<CanvasSeam>();

            foreach (CanvasSeam canvasSeam in CanvasSeamList)
            {
                if (canvasSeam.SeamType != SeamType.Manual)
                {
                    seamsToRemove.Add(canvasSeam);
                }
            }

            foreach (CanvasSeam canvasSeam in seamsToRemove)
            {
                canvasSeam.RemoveFromSeamLayer();

                CanvasSeamList.RemoveBase(canvasSeam);
                GraphicsSeamList.RemoveBase(canvasSeam.GraphicsSeam);
                SeamList.RemoveBase(canvasSeam.GraphicsSeam.Seam);

                canvasSeam.Delete();

            }

            seamsToRemove.Clear();

            foreach (CanvasSeam canvasSeam in DisplayCanvasSeamList)
            {
                if (canvasSeam.SeamType != SeamType.Manual)
                {
                    seamsToRemove.Add(canvasSeam);
                }
            }

            foreach (CanvasSeam canvasSeam in seamsToRemove)
            {
                canvasSeam.RemoveFromSeamLayer();

                CanvasSeamList.RemoveBase(canvasSeam);
                GraphicsSeamList.RemoveBase(canvasSeam.GraphicsSeam);
                SeamList.RemoveBase(canvasSeam.GraphicsSeam.Seam);

                DisplayCanvasSeamList.RemoveBase(canvasSeam);

                canvasManager.CurrentPage.DisplaySeamDict.Remove(canvasSeam.Guid);

                canvasSeam.Delete();

            }
        }

        public void DeleteRollouts()
        {
            base.DeleteRollouts(areaFinishLayers);
        }

        public AreaFinishBase AreaFinishBase => AreaFinishManager.AreaFinishBase;


        public double? GrossAreaInSqrInches()
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                return null;
            }

            if (!IsSeamed())
            {
                return null;
            }

            if (this.AreaFinishBase.MaterialsType == MaterialsType.Rolls)
            {
                // MDD 2025-02-22 FIX!!!!

                // The following needs to be fixed.

                StatsGeneratorLib.AreaStatsCalculator areaStatsCalculator = new AreaStatsCalculator(Page.DrawingScaleInInches, AreaFinishBase.RollWidthInInches, AreaFinishBase.NetAreaInSqrInches);

                areaStatsCalculator.UpdateRollStats(
                    CutList, UndrageList, VirtualOverageList, VirtualUndrageList, SmallElementWidthInInches, CutExtraElement);

                return areaStatsCalculator.TotalGrossAreaInSquareFeet * 144.0;
            }

            if (this.AreaFinishBase.MaterialsType == MaterialsType.Tiles)
            {
                double perimeterInFeet = this.PerimeterLength() / 12.0;

                double wasteInSqrFeet = perimeterInFeet * (AreaFinishBase.TrimInInches / 12.0);
                double netAreaInSqrFeet = this.NetAreaInSqrInches() / 144.0;

                double grossAreaInSqrInches = (netAreaInSqrFeet + wasteInSqrFeet) * 144.0;

                return grossAreaInSqrInches;
            }

            return 0;
        }

        public CoordinatedList<CanvasSeam> CanvasSeamList { get; set; } = new CoordinatedList<CanvasSeam>();

        public CoordinatedList<CanvasSeam> DisplayCanvasSeamList { get; set; } = new CoordinatedList<CanvasSeam>();


        public DirectedLine BaseSeamLineWall { get; set; } = null;

        public bool HasSeamsOrRollouts => SeamList.Count > 0 || RolloutList.Count > 0;

        internal void RegenerateSeamsAndCuts()
        {
            if (BaseSeamLineWall is null)
            {
                return;
            }

            GenerateSeamsAndRollouts(BaseSeamLineWall);

            if (GlobalSettings.ValidateRolloutAndCutWidth)
            {
                if (!DebugChecks.ValidateRolloutsAndCuts(this, AreaFinishBase.RollWidthInInches, Page.DrawingScaleInInches))
                {
                    MessageBox.Show("Invalid rollouts or cuts generated. Please note development path.");
                }
            }
        }

        internal void GenerateSeamsAndRollouts(CanvasDirectedLine line)
        {
            BaseSeamLineWall = (DirectedLine)line;

            GenerateSeamsAndRollouts((DirectedLine)line);

            if (GlobalSettings.ValidateRolloutAndCutWidth)
            {

                if (!DebugChecks.ValidateRolloutsAndCuts(this, AreaFinishBase.RollWidthInInches, Page.DrawingScaleInInches))
                {
                    Tracer.TraceGen.TraceError("Validation of rollouts and cuts fails in CanvasLayoutArea:GenerateSeamsAndRollouts", 1, true);
                    //MessageBox.Show("Invalid rollouts or cuts generated. Please note development path.");
                }
            }
        }

        internal void GenerateSeamsAndRollouts(DirectedLine line)
        {
            if (this.LayoutAreaType == LayoutAreaType.Normal)
            {

                GenerateSeamsAndRolloutsNormal(line);

            }

            else if (this.LayoutAreaType == LayoutAreaType.Remnant)
            {
                GenerateSeamsAndRolloutsRemnant(line);
            }

            else if (this.LayoutAreaType == LayoutAreaType.OversGenerator)
            {
                GenerateSeamsAndRolloutsOversGenerator(line);
            }

            if (GlobalSettings.ValidateRolloutAndCutWidth)
            {
                if (!DebugChecks.ValidateRolloutsAndCuts(this, AreaFinishBase.RollWidthInInches, Page.DrawingScaleInInches))
                {
                    MessageBox.Show("Invalid rollouts or cuts generated. Please note development path.");
                }
            }

            this.AreaFinishManager.SetupCutNumbering();

            this.AreaFinishManager.SetupAllSeamLayers();

        }

        internal void GenerateSeamsAndRolloutsNormal(DirectedLine line)
        {
            DeleteNonManualSeams();

            DeleteRollouts();

            GraphicsDirectedPolygon externalAreaShape = (GraphicsDirectedPolygon)this.ExternalArea;

            List<GraphicsDirectedPolygon> internalAreaShapes = new List<GraphicsDirectedPolygon>();

            this.InternalAreas.ForEach(s => internalAreaShapes.Add((GraphicsDirectedPolygon)s));

            GraphicsWindow graphicsWindow = new GraphicsWindow(this.canvasManager.VsoWindow);

            double seamWidthInLocalUnits = 0;
            double materialWidthInLocalUnits = 0;
            double materialOverlapInLocalUnits = 0;

            seamWidthInLocalUnits = FinishManagerGlobals.SelectedAreaFinishManager.RollWidthInInches - FinishManagerGlobals.SelectedAreaFinishManager.OverlapInInches;

            seamWidthInLocalUnits /= Page.DrawingScaleInInches;

            materialWidthInLocalUnits = FinishManagerGlobals.SelectedAreaFinishManager.RollWidthInInches / Page.DrawingScaleInInches;

            materialOverlapInLocalUnits = FinishManagerGlobals.SelectedAreaFinishManager.OverlapInInches;

            // This should clear the corresponding seam list for all inhereted members

            //---------------------------------------------------------------------------------------------------------//
            // The following is deleted because it deletes all seams, manual and regular and regular seams are deleted
            // in the call to DeleteNonManualSeams above
            //---------------------------------------------------------------------------------------------------------//
            //SeamList.Clear();

            base.GenerateSeamsAndRollouts(line, 0, seamWidthInLocalUnits, materialWidthInLocalUnits, materialOverlapInLocalUnits, Page.DrawingScaleInInches);

            uint cutIndx = 1;
            uint ovrIndx = 1;
            uint undIndx = 1;

            UpdateCutOverAndUnderIndices(ref cutIndx, ref ovrIndx, ref undIndx);

            foreach (GraphicsRollout graphicsRollout in GraphicsRolloutList)
            {
                foreach (GraphicsCut graphicsCut in graphicsRollout.GraphicsCutList)
                {
                    GraphicShape cutShape = graphicsCut.DrawBoundingRectangleAndIndex(areaFinishLayers.CutsIndexLayer, Color.Red, Color.White, 1);

                    VisioInterop.SetShapeData(cutShape, "Cut Bounding Polygon", "Polygon", cutShape.Guid);

                    canvasManager.CurrentPage.AddToPageShapeDict(cutShape);

                    graphicsCut.Shape = cutShape;

                    cutShape.SetFillOpacity(0);

                    cutShape.SetLineWidth(1);

                    cutShape.BringToFront();

                    areaFinishLayers.CutsLayer.AddShape(cutShape, 1);

                    VisioInterop.LockShape(cutShape);

                    foreach (GraphicsOverage overage in graphicsCut.GraphicsOverageList)
                    {
                        GraphicShape overageShape = overage.DrawBoundingRectangleNormal(
                            overage
                            , areaFinishLayers.OversLayer
                            , areaFinishLayers.OversIndexLayer
                            , Color.Green
                            , Color.White
                            , 1);

                        overage.Shape = overageShape;

                        Page.AddToPageShapeDict(overage.Shape);

                        VisioInterop.SetFillOpacity(overageShape, 0);

                        overageShape.BringToFront();

                        areaFinishLayers.OversLayer.AddShape(overageShape, 1);


                        VisioInterop.LockShape(overageShape);


                        if (overage.GraphicsOverageIndex != null)
                        {
                            Page.AddToPageShapeDict(overage.GraphicsOverageIndex.Shape);

                            areaFinishLayers.OversIndexLayer.AddShape(overage.GraphicsOverageIndex.Shape, 1);

                            VisioInterop.LockShape(overage.GraphicsOverageIndex.Shape);
                        }
                    }
                }

                foreach (GraphicsUndrage underage in graphicsRollout.GraphicsUndrageList)
                {
                    GraphicShape underageShape = underage.DrawBoundingRectangle(
                        areaFinishLayers.UndrsLayer
                        , areaFinishLayers.UndrsIndexLayer
                        , Color.Orange
                        , Color.White
                        , 1);

                    underage.Shape = underageShape;

                    underageShape.SetShapeData("Underage Bounding Polygon", "Polygon");

                    Page.AddToPageShapeDict(underageShape);

                    VisioInterop.SetFillOpacity(underageShape, 0);

                    underageShape.BringToFront();

                    areaFinishLayers.UndrsLayer.AddShape(underageShape, 1);

                    VisioInterop.LockShape(underageShape);
                }

            }

            //this.AreaFinishManager.UpdateCutIndices();

            if (GlobalSettings.ValidateRolloutAndCutWidth)
            {
                if (!DebugChecks.ValidateRolloutsAndCuts(this, AreaFinishBase.RollWidthInInches, Page.DrawingScaleInInches))
                {
                    MessageBox.Show("Invalid rollouts or cuts generated. Please note development path.");
                }
            }

            //canvasManager.UpdateAreaAndSeamsStats();
            canvasManager.UpdateAreaSeamsUndrsOversDataDisplay();
        }

        internal void GenerateSeamsAndRolloutsPatternFill(DirectedLine line)
        {
            GraphicsDirectedPolygon externalAreaShape = (GraphicsDirectedPolygon)this.ExternalArea;


            List<GraphicsDirectedPolygon> internalAreaShapes = new List<GraphicsDirectedPolygon>();

            this.InternalAreas.ForEach(s => internalAreaShapes.Add((GraphicsDirectedPolygon)s));

            GraphicsWindow graphicsWindow = new GraphicsWindow(this.canvasManager.VsoWindow);

            double interlineDistanceInLocalUnits = 0;


            interlineDistanceInLocalUnits = AreaFinishManager.AreaFinishBase.FillPatternInterlineDistanceInFt;

            //interlineDistanceInLocalUnits /= Page.DrawingScaleInInches;

            base.GenerateSeamsAndRollouts(line, 0, interlineDistanceInLocalUnits, interlineDistanceInLocalUnits, 0, Page.DrawingScaleInInches);
        }

        internal void GenerateSeamsAndRolloutsOversGenerator(DirectedLine line)
        {
            DeleteNonManualSeams();

            DeleteRollouts();

            //Overage.OverageIndexGenerator = 1;
            //Undrage.UndrageIndexGenerator = 1;

            GraphicsDirectedPolygon externalAreaShape = (GraphicsDirectedPolygon)this.ExternalArea;

            List<GraphicsDirectedPolygon> internalAreaShapes = new List<GraphicsDirectedPolygon>();

            this.InternalAreas.ForEach(s => internalAreaShapes.Add((GraphicsDirectedPolygon)s));

            GraphicsWindow graphicsWindow = new GraphicsWindow(this.canvasManager.VsoWindow);

            double seamWidthInLocalUnits = 0;
            double materialWidthInLocalUnits = 0;
            double materialOverlapInLocalUnits = 0;

            seamWidthInLocalUnits = OversGeneratorOversWidthInInches;

            seamWidthInLocalUnits /= Page.DrawingScaleInInches;

            materialWidthInLocalUnits = FinishManagerGlobals.SelectedAreaFinishManager.RollWidthInInches / Page.DrawingScaleInInches;

            materialOverlapInLocalUnits = FinishManagerGlobals.SelectedAreaFinishManager.OverlapInInches;

            // This should clear the corresponding seam list for all inhereted members

            SeamList.Clear();

            foreach (GraphicsOverage overage in EmbeddedOversList)
            {
                overage.Delete();
            }

            EmbeddedOversList.Clear();


            base.GenerateSeamsAndRollouts(line, 0, seamWidthInLocalUnits, materialWidthInLocalUnits, materialOverlapInLocalUnits, Page.DrawingScaleInInches);

            foreach (GraphicsRollout graphicsRollout in GraphicsRolloutList)
            {
                foreach (GraphicsCut graphicsCut in graphicsRollout.GraphicsCutList)
                {
                    graphicsCut.GraphicsRemnantCutList.Clear();

                    foreach (RemnantCut remnantCut in graphicsCut.RemnantCutList)
                    {
                        GraphicsRemnantCut graphicsRemnantCut = new GraphicsRemnantCut(Window, Page, remnantCut);

                        graphicsCut.GraphicsRemnantCutList.Add(graphicsRemnantCut);

                        graphicsRemnantCut.Draw(areaFinishLayers.EmbdCutsLayer.GetBaseLayer(), Color.DarkBlue, Color.White, 2);

                        VisioInterop.BringToFront(graphicsRemnantCut.Shape);
                        areaFinishLayers.EmbdCutsLayer.AddShape(graphicsRemnantCut.Shape, 1);

                        areaFinishLayers.EmbdCutsLayer.GetBaseLayer().BringShapesToFront();
                    }

                    Window.DeselectAll();

                }
            }

            foreach (GraphicsRollout graphicsRollout in GraphicsRolloutList)
            {
                foreach (Overage overage in graphicsRollout.EmbeddedOverageList)
                {
                    GraphicsOverage graphicsOverage = new GraphicsOverage((GraphicsCut)overage.ParentCut, Window, Page, FinishesLibElements, overage);

                    EmbeddedOversList.Add(graphicsOverage);

                    graphicsOverage.DrawBoundingRectangleEmbd(
                        areaFinishLayers.EmbdOverLayer
                        , areaFinishLayers.OversIndexLayer
                        , Color.Gold
                        , Color.FromArgb(0)
                        , 2);

                    areaFinishLayers.EmbdOverLayer.AddShape(graphicsOverage.Shape, 1);
                }

                Window.DeselectAll();
            }
        }

        public void CreateSeamIndexTag()
        {
            HashSet<int> usedSeamAreaIndicies = canvasManager.CurrentPage.UsedSeamAreaIndices(this.AreaFinishManager.Guid, this.LayoutAreaType);

            Coordinate tagLocation = this.VoronoiLabelLocation();

            this.SeamIndexTag
                = new CanvasSeamTag(Window, Page, tagLocation.X, tagLocation.Y, canvasManager.CurrentPage.GetSeamAreaIndex(usedSeamAreaIndicies), this.LayoutAreaType);

            this.SeamIndexTag.Draw();
        }


        public void UpdateOverIndicesAndVisibility(ref uint overIndx)
        {
            // We tally the graphics overage indices first so we can number them left to right, top to bottom.

            List<GraphicsOverageIndex> graphicsOverageIndexList = new List<GraphicsOverageIndex>();

            foreach (GraphicsOverage graphicsOverage in this.GraphicsOverageList)
            {
                if (graphicsOverage.Deleted)
                {
                    continue;
                }

                GraphicsOverageIndex graphicsOverageIndex = graphicsOverage.GraphicsOverageIndex;

                if (Utilities.IsNotNull(graphicsOverageIndex))
                {
                    graphicsOverageIndexList.Add(graphicsOverageIndex);
                }
            }

            graphicsOverageIndexList.Sort((i1, i2) => Coordinate.Compare(i1.Location, i2.Location));

            // Now we number them


            foreach (GraphicsOverageIndex graphicsOverageIndex in graphicsOverageIndexList)
            {
                graphicsOverageIndex.OverageIndex = Overage.OverageIndexGenerator();
            }

            foreach (GraphicsOverage graphicsOverage in this.GraphicsOverageList)
            {
                if (graphicsOverage.Deleted)
                {
                    graphicsOverage.SetVisibility(false);

                    graphicsOverage.OverageIndex = 0;
                }

                else
                {
                    graphicsOverage.SetVisibility(true);
                }
            }
        }

        public void UpdateUndrIndicesAndVisibility(ref uint undrIndx)
        {
            // We tally the graphics undrage indices first so we can number them left to right, top to bottom.

            List<GraphicsUndrageIndex> graphicsUndrageIndexList = new List<GraphicsUndrageIndex>();

            foreach (GraphicsUndrage graphicsUndrage in this.GraphicsUndrageList)
            {
                if (graphicsUndrage.Deleted)
                {
                    continue;
                }

                GraphicsUndrageIndex graphicsUndrageIndex = graphicsUndrage.GraphicsUndrageIndex;

                if (Utilities.IsNotNull(graphicsUndrageIndex))
                {
                    graphicsUndrageIndexList.Add(graphicsUndrageIndex);
                }
            }

            graphicsUndrageIndexList.Sort((i1, i2) => Coordinate.Compare(i1.Location, i2.Location));

            // Now we number them

            foreach (GraphicsUndrageIndex listElem in graphicsUndrageIndexList)
            {
                undrIndx = Undrage.UndrageIndexGenerator();
                listElem.UndrageIndex = undrIndx;
            }

            foreach (GraphicsUndrage graphicsUndrage in this.GraphicsUndrageList)
            {
                if (graphicsUndrage.Deleted)
                {
                    graphicsUndrage.SetVisibility(false);

                    graphicsUndrage.UndrageIndex = 0;
                }

                else
                {
                    graphicsUndrage.SetVisibility(true);
                }
            }
        }

        public void UpdateCutIndicesAndVisibility(ref uint cutIndex)
        {
            List<GraphicsCut> lclGraphicsCutList = new List<GraphicsCut>();

            // We tally the graphics cut indices first so we can number them left to right, top to bottom.

            foreach (GraphicsCut graphicsCut in this.GraphicsCutList)
            {
                if (graphicsCut.Deleted)
                {
                    //graphicsCut.GraphicsCutIndex.CutIndex = -1;

                    graphicsCut.GraphicsCutIndex.SetVisibility(false);

                    continue;
                }

                GraphicsCutIndex graphicsCutIndex = graphicsCut.GraphicsCutIndex;

                if (Utilities.IsNotNull(graphicsCutIndex))
                {
                    lclGraphicsCutList.Add(graphicsCut);
                }
            }

            lclGraphicsCutList.Sort((i1, i2) => Coordinate.Compare(i1.CutOffset, i2.CutOffset));

            //lclGraphicsCutList.Sort((i1, i2) => Coordinate.Compare(i1.Item1.Location, i2.Item1.Location));

            // Now we number them

            foreach (GraphicsCut graphicsCut in lclGraphicsCutList)
            {
                graphicsCut.CutIndex = cutIndex++;

                graphicsCut.GraphicsCutIndex.DrawCutIndexText();
            }
        }


        internal void UpdateCutOverAndUnderIndices(ref uint cutIndex, ref uint overIndx, ref uint undrIndx)
        {
            UpdateCutIndicesAndVisibility(ref cutIndex);

            UpdateOverIndicesAndVisibility(ref overIndx);

            UpdateUndrIndicesAndVisibility(ref undrIndx);
        }

        internal void Lock()
        {
            VisioInterop.LockShape(this.Shape);

            this.ExternalArea.Lock();

            foreach (CanvasDirectedPolygon internalArea in InternalAreas)
            {
                internalArea.Lock();
            }
        }

        internal void Unlock()
        {
            VisioInterop.UnlockShape(this.Shape);

            this.ExternalArea.Unlock();

            foreach (CanvasDirectedPolygon internalArea in InternalAreas)
            {
                internalArea.Unlock();
            }
        }


        //public bool _isSeamStateLocked { get; set; } = false;

        //public bool IsSeamStateLocked
        //{
        //    get
        //    {
        //        return AreaFinishBase.SeamAreaLocked || _isSeamStateLocked;
                
        //    }

        //    set
        //    {
        //        if (value == _isSeamStateLocked)
        //        {
        //            return;
        //        }

        //        _isSeamStateLocked = value;

        //    }
        //}

        //private void placeLockIcon()
        //{
            // The following should never be the case, but is defensqive

            //if (SeamStateAreaLockIcon != null)
            //{
            //    removeLockIcon();
            //}

            //Coordinate seamIndexTagLocation = this.VoronoiLabelLocation();

            //double locX = seamIndexTagLocation.X;
            //double locY = seamIndexTagLocation.Y;

            //SeamStateAreaLockIcon = new SeamStateAreaLockIcon(this.Page, this.Window);

            //SeamStateAreaLockIcon.Draw(locX, locY - .375);


            //Page.AddToPageShapeDict(SeamStateAreaLockIcon.Shape);

            //this.AreaFinishManager.SeamDesignStateLockLayer.AddShape(SeamStateAreaLockIcon.Shape, 0);
        //}

        //private void removeLockIcon()
        //{
        //    // The following should never be the case, but is defensive

        //    if (SeamStateAreaLockIcon == null)
        //    {
        //        return;
        //    }

        //    this.AreaFinishManager.SeamDesignStateLockLayer.RemoveShapeFromLayer(SeamStateAreaLockIcon.Shape, 1);

        //    Page.RemoveFromPageShapeDict(SeamStateAreaLockIcon.Shape);

        //    SeamStateAreaLockIcon.Shape.Delete();


        //    SeamStateAreaLockIcon = null;
        //}
        //internal void DeleteLock()
        //{
        //    if (!this.IsSeamStateLocked)
        //    {
        //        return;
        //    }

        //    this.IsSeamStateLocked = false;

        //}
        //internal void initializeSeamStateForExistingProject()
        //{
        //    if (this._isSeamStateLocked == false)
        //    {
        //        return;
        //    }

            //if (this.SeamStateAreaLockIcon != null)
            //{
            //    return;
            //}

            //setupLockIcon();
        //}

        //private void setupLockIcon()
        //{

        //    placeLockIcon();

        //    //this.SeamDesignStateSelectionModeSelected = false;

        //    this.SetDesignStateSelectedLineGraphics();

        //    this.SetFillColorAndPattern(this.AreaFinishBase.Color, this.AreaFinishBase.Pattern);

        //    this.SetLineGraphics(DesignState.Seam, AreaShapeBuildStatus.Completed);
        //}

        internal bool IsLocked()
        {
            if (VisioInterop.IsLocked(this.Shape))
            {
                return true;
            }

            if (this.ExternalArea.IsLocked())
            {
                return true;
            }

            foreach (CanvasDirectedPolygon internalArea in InternalAreas)
            {
                if (internalArea.IsLocked())
                {
                    return true;
                }
            }

            return false;
        }


        internal void GenerateSeamsAndRolloutsRemnant(DirectedLine line)
        {
            DeleteNonManualSeams();

            DeleteRollouts();

            GraphicsDirectedPolygon externalAreaShape = (GraphicsDirectedPolygon)this.ExternalArea;

            List<GraphicsDirectedPolygon> internalAreaShapes = new List<GraphicsDirectedPolygon>();

            this.InternalAreas.ForEach(s => internalAreaShapes.Add((GraphicsDirectedPolygon)s));

            GraphicsWindow graphicsWindow = new GraphicsWindow(this.canvasManager.VsoWindow);

            double rollWidthInLocalUnits = 0;

            double materialWidthInLocalUnits = 0;

            double materialOverlapInLocalUnits = 0;

            rollWidthInLocalUnits = (SystemState.RemnantSeamWidthInFeet - AreaFinishBase.OverlapInInches) * 12.0 / Page.DrawingScaleInInches;

            materialWidthInLocalUnits = SystemState.RemnantSeamWidthInFeet * 12.0 / Page.DrawingScaleInInches;

            // This should clear the corresponding seam list for all inhereted members

            SeamList.Clear();

            base.GenerateSeamsAndRollouts(line, 0, rollWidthInLocalUnits, materialWidthInLocalUnits, materialOverlapInLocalUnits, Page.DrawingScaleInInches);

            foreach (GraphicsRollout graphicsRollout in GraphicsRolloutList)
            {
                foreach (GraphicsCut graphicsCut in graphicsRollout.GraphicsCutList)
                {

                    graphicsCut.GraphicsRemnantCutList.Clear();

                    foreach (RemnantCut remnantCut in graphicsCut.RemnantCutList)
                    {
                        GraphicsRemnantCut graphicsRemnantCut = new GraphicsRemnantCut(Window, Page, remnantCut);

                        graphicsCut.GraphicsRemnantCutList.Add(graphicsRemnantCut);

                        graphicsRemnantCut.Draw(areaFinishLayers.EmbdCutsLayer.GetBaseLayer(), Color.DarkBlue, Color.White, 2);

                        areaFinishLayers.EmbdCutsLayer.AddShape(graphicsRemnantCut.Shape, 1);

                        areaFinishLayers.EmbdCutsLayer.GetBaseLayer().BringShapesToFront();
                    }

                    this.Window?.DeselectAll();

                }
            }
        }

        internal double CutsAreaInSqrInches()
        {
            double cutsAreaInSqrInches = 0;

            foreach (Rollout rollout in this.RolloutList)
            {
                cutsAreaInSqrInches += rollout.CutsAreaInSqrInches(Page.DrawingScaleInInches);
            }

            return cutsAreaInSqrInches;
        }

        private void scaleOverages(List<Overage> graphicsOverageList)
        {
            foreach (Overage overage in graphicsOverageList)
            {
                overage.EffectiveDimensions
                    = new Tuple<double, double>(
                        overage.EffectiveDimensions.Item1 * Page.DrawingScaleInInches,
                        overage.EffectiveDimensions.Item2 * Page.DrawingScaleInInches);
            }
        }

        private void scaleUndrages(List<Undrage> graphicsUndrageList)
        {
            foreach (Undrage undrage in graphicsUndrageList)
            {
                undrage.EffectiveDimensions
                    = new Tuple<double, double>(
                        undrage.EffectiveDimensions.Item1 * Page.DrawingScaleInInches,
                        undrage.EffectiveDimensions.Item2 * Page.DrawingScaleInInches);
            }
        }

        private List<GraphicsOverage> filterOveragesByMinimums(List<GraphicsOverage> overageList)
        {
            List<GraphicsOverage> rtrnList = new List<GraphicsOverage>();

            foreach (GraphicsOverage overage in overageList)
            {
                if (overage.EffectiveDimensions.Item1 * Page.DrawingScaleInInches >= GlobalSettings.MinOverageWidthInInches &&
                    overage.EffectiveDimensions.Item2 * Page.DrawingScaleInInches >= GlobalSettings.MinOverageLengthInInches)
                {
                    rtrnList.Add(overage);
                }
            }

            return rtrnList;
        }

        public void UpdateSeamGraphics(SeamFinishBase seamFinishBase, bool selected)
        {
            CanvasSeamList.ForEach(s => ((CanvasSeam)s).UpdateSeamGraphics(seamFinishBase, selected));

            DisplayCanvasSeamList.ForEach(s => ((CanvasSeam)s).UpdateSeamGraphics(seamFinishBase, selected));
        }

        /// <summary>
        /// Draws all the seams for the current layout area
        /// </summary>
        public void DrawSeams(bool includeManualSeams = true)
        {
            HashSet<string> existingSeamGuids = null;


            if (CanvasSeamList is null)
            {
                return;
            }

            int visioDashType = 1;
            double lineWidthInPts = 3;
            Color seamColor = Color.Red;

            SeamFinishBase seamBase = FinishManagerGlobals.SelectedAreaFinishManager.FinishSeamBase;

            if (seamBase != null)
            {
                visioDashType = seamBase.VisioDashType;
                lineWidthInPts = seamBase.SeamWidthInPts;
                seamColor = seamBase.SeamColor;
            }

            foreach (CanvasSeam canvasSeam in CanvasSeamList)
            {
                seamColor = canvasSeam.SeamFinishBase.SeamColor;
                lineWidthInPts = canvasSeam.SeamFinishBase.SeamWidthInPts;
                visioDashType = canvasSeam.SeamFinishBase.VisioDashType;

                if (canvasSeam.SeamType == SeamType.Manual)
                {
                    if (!includeManualSeams)
                    {
                        continue;
                    }

                    // The following guarantees that existing seams do not get regenerated, which is a bug.

                    if (existingSeamGuids == null)
                    {
                        existingSeamGuids = GraphicsDebugSupportRoutines.GenerateExistingSeamGuids(canvasManager.CurrentPage.VisioPage);
                    }

                    if (existingSeamGuids.Contains(canvasSeam.Guid))
                    {
                        continue;
                    }
                }

                canvasSeam.Draw(seamColor, lineWidthInPts, visioDashType, true, "Seam");

                canvasSeam.AddToSeamLayer();

                canvasSeam.GraphicsSeam.Shape.BringToFront();
            }

            foreach (CanvasSeam canvasSeam in DisplayCanvasSeamList)
            {
                seamColor = canvasSeam.SeamFinishBase.SeamColor;
                lineWidthInPts = canvasSeam.SeamFinishBase.SeamWidthInPts;
                visioDashType = canvasSeam.SeamFinishBase.VisioDashType;

                canvasSeam.Draw(seamColor, lineWidthInPts, visioDashType, true, "[DisplaySeam]");

                canvasSeam.AddToSeamLayer();

                canvasSeam.GraphicsSeam.Shape.BringToFront();
            }
            // Window?.DeselectAll();
        }

        public void DrawCutsOversAndUndrs()
        {
            if (Utilities.IsNotNull(base.GraphicsCutList))
            {
                foreach (GraphicsCut graphicsCut in base.GraphicsCutList)
                {
                    // Bernie check here
                    GraphicShape cutShape = graphicsCut.DrawBoundingRectangleAndIndex(areaFinishLayers.CutsIndexLayer, Color.Red, Color.White, 1);

                    graphicsCut.Shape = cutShape;

                    cutShape.SetFillOpacity(0);

                    cutShape.SetLineWidth(1);

                    cutShape.BringToFront();

                    VisioInterop.SetShapeData(cutShape, "Cut Bounding Polygon", "Polygon", cutShape.Guid);

                    Page.AddToPageShapeDict(graphicsCut.Shape);

                    areaFinishLayers.CutsLayer.AddShape(graphicsCut.Shape, 1);

                    VisioInterop.LockShape(graphicsCut.Shape);

                    //CutsIndexLayer.AddShape(graphicsCut.GraphicsCutIndex.Shape, 1); // Bernie, check here

                    VisioInterop.LockShape(graphicsCut.GraphicsCutIndex.Shape);

                    if (Utilities.IsNotNull(graphicsCut.GraphicsOverageList))
                    {
                        foreach (GraphicsOverage overage in graphicsCut.GraphicsOverageList)
                        {
                            GraphicShape overageShape = overage.DrawBoundingRectangleNormal(
                                overage, areaFinishLayers.OversLayer
                                , areaFinishLayers.OversIndexLayer
                                , Color.Green
                                , Color.White
                                , 1);

                            overage.Shape = overageShape;

                            overage.Shape.SetShapeData("[Overage]", "Composite Shape", overage.Guid);

                            Page.AddToPageShapeDict(overage.Shape);

                            VisioInterop.SetFillOpacity(overageShape, 0);

                            overageShape.BringToFront();

                            areaFinishLayers.OversLayer.AddShape(overageShape, 1);



                            if (overage.GraphicsOverageIndex != null)
                            {

                                Page.AddToPageShapeDict(overage.GraphicsOverageIndex.Shape);


                                areaFinishLayers.OversIndexLayer.AddShape(overage.GraphicsOverageIndex.Shape, 1);

                                VisioInterop.LockShape(overage.GraphicsOverageIndex.Shape);
                            }

                            VisioInterop.LockShape(overageShape);
                        }
                    }
                }
            }

            if (Utilities.IsNotNull(base.GraphicsUndrageList))
            {
                foreach (GraphicsUndrage underage in base.GraphicsUndrageList)
                {
                    GraphicShape underageShape = underage.DrawBoundingRectangle(
                        areaFinishLayers.UndrsLayer
                        , areaFinishLayers.UndrsIndexLayer
                        , Color.Orange
                        , Color.White
                        , 1);

                    underageShape.SetShapeData("Underage Bounding Polygon", "Polygon");

                    underage.Shape = underageShape;

                    underage.Shape.SetShapeData("[Underage]", "Composite Shape", underage.Guid);

                    Page.AddToPageShapeDict(underageShape);

                    VisioInterop.SetFillOpacity(underageShape, 0);

                    underageShape.BringToFront();

                    areaFinishLayers.UndrsLayer.AddShape(underageShape, 1);

                    VisioInterop.LockShape(underageShape);
                }
            }
        }

        internal void RemoveSeamsAndRollouts(bool includeManualSeams = false)
        {
            if (includeManualSeams)
            {
                DeleteSeams();
            }

            else
            {
                DeleteNonManualSeams();
            }

            DeleteRollouts();

            this.AreaFinishManager.UpdateCutIndices();

            //canvasManager.UpdateAreaAndSeamsStats();
            canvasManager.UpdateAreaSeamsUndrsOversDataDisplay();
        }

        internal void RemoveEmbeddedOvers()
        {
            foreach (GraphicsOverage embeddedOverage in this.EmbeddedOversList)
            {
                embeddedOverage.Delete();
            }

            this.EmbeddedOversList.Clear();
        }

        private Color currSelectedColor = Color.Black;

        private double currSelectedOpacity = -1;

        #region Design State Management

        /// <summary>
        /// Sets up the format for the canvas layout area based on the current area mode design state.
        /// Warning: The recursive element is effective assuming that edits are done from the top down.
        /// If this is changed, recursive will not work. Refer to the various calls where recursive is true
        /// </summary>
        /// <param name="areaMode">The area mode in effect</param>
        /// <param name="recursive">Whether or not to apply recursively</param>
        internal void SetAreaDesignStateFormat(/*AreaMode areaMode,*/ bool recursive = false)
        {
            Color color;
            int pattern;
            double opacity;

            color = AreaFinishManager.Color;
            pattern = AreaFinishManager.Pattern;
            opacity = AreaFinishManager.Opacity;

            SetFillPattern(color, pattern);

            //if (pattern == 0)
            //{
            //    SetFillColor(color);

            //    SetFillOpacity(opacity);
            //}

            //else
            //{
            //    SetFillPattern(color, pattern);

            //    SetPatternOpacity(0);
            //}

            // Do not undraw a remnant area seam tag because it is placed on a layer that makes it disappear when in any
            // mode but Seam / Remnant Analysis

            if (Utilities.IsNotNull(this.SeamIndexTag) && this.LayoutAreaType != LayoutAreaType.Remnant)
            {
                //this.SeamIndexTag.Delete();
            }

            SetLineGraphics(DesignState.Area, AreaShapeBuildStatus.Completed);

            SetSeamLineGraphics(DesignState.Area, false);

            //areaFinishLayers.SeamDesignStateLayer.SetLayerVisibility(false);

            if (recursive)
            {
                OffspringAreas.ForEach(o => o.SetAreaDesignStateFormat(/*areaMode,*/ recursive));
            }
        }

        internal void SetSeamDesignStateFormat(SeamMode seamMode)
        {
#if true
            if (IsSubdivided())
            {
                MakeDisappear();

                return;
            }

            else
            {
                MakeReappear();
            }
#endif
            Color color;

            double opacity;

            if (seamMode == SeamMode.Subdivision && this.SeamDesignStateSubdivisionModeSelected)
            {
                color = GlobalSettings.SelectedAreaColor;

                opacity = GlobalSettings.SelectedAreaOpacity;
            }

            else
            {
                color = AreaFinishManager.Color;

                opacity = AreaFinishManager.Opacity;
            }

            if (!(this.SeamIndexTag is null))
            {
                if (this.SeamDesignStateSelectionModeSelected)
                {
                    // Do not need to redraw in case of remnant layout area because it is hidden due to layer visibility.
                    if (this.LayoutAreaType != LayoutAreaType.Remnant)
                    {
                        //this.SeamIndexTag.Redraw();

                        if (this.HasSeamsOrRollouts)
                        {
                            this.SeamIndexTag.UnderlineTag();
                        }

                        if (this.selectedForRemnantAnalysis)
                        {
                            this.SeamIndexTag.SetNolineMode(false);
                        }
                    }
                }

                else
                {
                    this.SeamIndexTag.Delete();
                }
            }

            SetLineGraphics(DesignState.Seam, AreaShapeBuildStatus.Completed);


            areaFinishLayers.SeamDesignStateLayer.SetLayerVisibility(true);

            SetFillColorAndPattern(this.AreaFinishBase.Color, this.AreaFinishBase.Pattern);

        }

        internal void SetCompletedLineWidth()
        {
            ExternalArea.SetLineGraphics(canvasManager.DesignState, false, AreaShapeBuildStatus.Completed);
        }

        internal void setupBorderAreaOverageOrUndrage()
        {
            double drawingScaleInInches = this.canvasManager.CurrentPage.DrawingScaleInInches;

            this.BorderAreaUndrage = null;
            this.BorderAreaOverage = null;

            double rollWidthInInches = this.AreaFinishManager.RollWidthInInches;

            double materialOverlapInInches = this.AreaFinishManager.OverlapInInches;

            double widthInInches = this.BorderWidthInInches;

            DirectedLine lngthLine = getFixedWidthLine(widthInInches);

            double lngthInInches = Math.Round(lngthLine.Length * drawingScaleInInches, 5);
            double angle = lngthLine.Atan();


            if (widthInInches <= rollWidthInInches * 0.5)
            {
                double width = widthInInches / drawingScaleInInches;
                double lngth = lngthInInches / drawingScaleInInches;

                Undrage undrage = new Undrage(widthInInches, lngthInInches, rollWidthInInches, materialOverlapInInches, angle);

                this.BorderAreaUndrage = new GraphicsUndrage(null, Window, Page, undrage);

                this.BorderAreaUndrage.EffectiveDimensions = new Tuple<double, double>(lngth, width);
            }

            else
            {
                double width = (rollWidthInInches - widthInInches) / drawingScaleInInches;
                double lngth = lngthInInches / drawingScaleInInches;

                this.BorderAreaOverage = new Overage(rollWidthInInches - widthInInches, lngthInInches, angle);

                this.BorderAreaOverage.EffectiveDimensions = new Tuple<double, double>(lngth, width);
            }

        }

        private DirectedLine getFixedWidthLine(double width)
        {
            if (this.Perimeter.Count != 4)
            {
                throw new Exception("Invalid perimeter for fixed width");
            }

            foreach (CanvasDirectedLine canvasDirectedLine in this.Perimeter)
            {
                double length = canvasDirectedLine.Length * this.canvasManager.CurrentPage.DrawingScaleInInches;

                if (Math.Abs(length - width) <= 1e-2)
                {
                    continue;
                }

                return (DirectedLine)canvasDirectedLine;
            }

            // Best check to be sure this is correct

            return (DirectedLine)this.Perimeter[1];
        }

        internal void SetLineDesignStateFormat(/*LineMode lineMode*/)
        {
            if (!(this.SeamIndexTag is null))
            {
                this.SeamIndexTag.Delete();
            }

            SetLineGraphics(DesignState.Line, AreaShapeBuildStatus.Completed);

            SetSeamLineGraphics(DesignState.Line, false);

            if (PatternFill != null)
            {
                PatternFill.Delete(Page);
            }
        }



        #endregion

        internal void SetupFromParentNewMethod(CanvasLayoutArea parentLayoutArea)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { parentLayoutArea });
#endif

            try
            {
                UCLineFinishPaletteElement ucLine = majorityAreaFinish(parentLayoutArea); // this is a heuristic. Use the first line to define.

                LineFinishManager lineFinishManager = canvasManager.LineFinishManagerList[ucLine.Guid];

                this.ParentArea = parentLayoutArea;

                //this.ParentAreaGuid = this.ParentArea.Guid;

                ExternalArea.Guid = this.Guid;

                foreach (CanvasDirectedLine line in ExternalArea)
                {
                    line.Draw(ucLine.LineColor, ucLine.LineWidthInPts);

                    line.ucLine = ucLine;

                    line.LineFinishManager = lineFinishManager;

                    lineFinishManager.AddLine(line);

                    lineFinishManager.AddLineToLayer(line.Guid, DesignState.Seam, SeamMode.Unknown);

                    line.SetShapeData();

                    // VisioInterop.SetShapeData(line.Shape, "External Perimeter Line", "Line[" + ucLine.LineName + "]", line.Guid);

                    line.ParentPolygon = ExternalArea;

                    Microsoft.Office.Interop.Visio.Shape visioShape = line.Shape.VisioShape;


                    canvasManager.CurrentPage.AddToDirectedLineDict(line);

                    canvasManager.CurrentPage.AddToPageShapeDict(line);
                }

                foreach (CanvasDirectedPolygon internalArea in InternalAreas)
                {
                    //internalArea.CreateInternalAreaShape();

                    foreach (CanvasDirectedLine line in internalArea)
                    {
                        line.Draw(ucLine.LineColor, ucLine.LineWidthInPts);

                        line.ucLine = ucLine;

                        lineFinishManager.AddLine(line);
                        lineFinishManager.AddLineToLayer(line.Guid, DesignState.Seam, SeamMode.Unknown);

                        // The following is a patch. Due to the poorly structured code, this should happen at a
                        // different layer.

                        // This sets the line name in the 
                        VisioInterop.SetShapeData1(line.Shape, line.ucLine.LineName);

                        line.ParentPolygon = internalArea;

                        canvasManager.CurrentPage.AddToDirectedLineDict(line);
                    }
                }

                //UCAreaFinish = parentLayoutArea.UCAreaFinish;

                AreaFinishManager areaFinishManager = parentLayoutArea.AreaFinishManager;

                areaFinishManager.AddNormalLayoutAreaSeamStateOnly(this);

                //this.SetLineGraphics(
                //    DesignState.Seam, false, AreaShapeBuildStatus.Completed); 

                //MDD Reset
                //this.CreateTakeoutArea();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in CanvasLayoutArea:SetupFromParentNewMethod", ex, 1, true);
            }
        }

        private UCLineFinishPaletteElement majorityAreaFinish(CanvasLayoutArea layoutArea)
        {
            Dictionary<UCLineFinishPaletteElement, int> finishDict = new Dictionary<UCLineFinishPaletteElement, int>();

            foreach (CanvasDirectedLine line in layoutArea.ExternalArea)
            {
                if (!finishDict.ContainsKey(line.ucLine))
                {
                    finishDict[line.ucLine] = 1;
                }

                else
                {
                    finishDict[line.ucLine] += 1;
                }
            }

            UCLineFinishPaletteElement maxElement = layoutArea.ExternalArea.Perimeter[0].ucLine;
            int maxCount = finishDict[layoutArea.ExternalArea.Perimeter[0].ucLine];

            foreach (KeyValuePair<UCLineFinishPaletteElement, int> kvp in finishDict)
            {
                if (kvp.Value > maxCount)
                {
                    maxElement = kvp.Key;
                    maxCount = kvp.Value;
                }
            }

            return maxElement;
        }

        internal void SendToBack()
        {
            VisioInterop.SendToBack(ExternalArea.Shape);

            InternalAreas.ForEach(s => VisioInterop.SendToBack(s.Shape));
        }

        internal void BringToFront()
        {
            VisioInterop.BringToFront(ExternalArea.Shape);

            //InternalAreas.ForEach(s => s.Shape.BringToFront()); // MDD Reset
        }

        public void MakeDisappear()
        {
            IsVisible = false;

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                return;
            }

            ExternalArea.MakeDisappear();

            // InternalAreas.ForEach(s => s.MakeDisappear());
        }

        public void MakeReappear()
        {
            IsVisible = true;

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                return;
            }

            ExternalArea.MakeReappear();

            // InternalAreas.ForEach(s => s.MakeReappear());
        }


        public AreaFinishManager AreaFinishManager { get; set; }

        public PatternFill FillPattern { get; set; } = null;

        // Kludge, for edit operations.

        public AreaFinishManager PrevAreaFinishManager { get; set; }

        public GraphicShape CompositeShape;

        public new GraphicShape Shape
        {
            get
            {
                if (ExternalArea is null)
                {
                    return null;
                }

                return ExternalArea.Shape;
            }

            set
            {
                Tracer.TraceGen.TraceMethodCall(1, false, new object[] { value });

                if (value is null)
                {
                    Tracer.TraceGen.TraceError("CanvasLayoutArea:Shape:set called with null value", 1, true);

                    return;
                }

                try
                {
                    base.Shape = value;

                    if (ExternalArea is null)
                    {
                        return;
                    }

                    ExternalArea.Shape = value;

                    if (value is null)
                    {
                        return;
                    }

                }

                catch (Exception ex)
                {

                }
            }
        }

        public bool AreaDesignStateEditModeSelected { get; set; }

        public bool AreaDesignStateSelectedForCopy { get; set; }

        private bool _seamDesignStateSelectionModeSelected = false;

        public bool SeamDesignStateSelectionModeSelected
        {
            get
            {
                return _seamDesignStateSelectionModeSelected;
            }
            set
            {
                _seamDesignStateSelectionModeSelected = value;
            }
        }

        public bool SeamDesignStateSubdivisionModeSelected { get; set; }

        public DesignState OriginatingDesignState { get; set; }

        //public int SeamAreaIndex => SeamIndexTag is null ? 0 : SeamIndexTag.SeamAreaIndex;

        public int PrevSeamAreaIndex { get; set; } = 0;

        private CanvasSeamTag _seamIndexTag = null;

        public CanvasSeamTag SeamIndexTag
        {
            get
            {
                 return _seamIndexTag;
            }

            set
            {
                if (_seamIndexTag == value)
                {
                    return;
                }

                _seamIndexTag = value;

                if (value is null)
                {
                    _seamIndexTag = null;
                }

                else
                {
                    _seamIndexTag = value;
                }
            }
        }

        private bool selectedForRemnantAnalysis = false;

        public bool SelectedForRemnantAnalysis
        {
            get
            {
                return selectedForRemnantAnalysis;
            }

            set
            {
                selectedForRemnantAnalysis = value;

                if (selectedForRemnantAnalysis)
                {
                    if (Utilities.IsNotNull(SeamIndexTag))
                    {
                        SeamIndexTag.SetNolineMode(false);
                    }
                }

                else
                {
                    if (Utilities.IsNotNull(SeamIndexTag))
                    {
                        SeamIndexTag.SetNolineMode(true);
                    }
                }
            }
        }

        internal void AddSeamIndexTag(CanvasSeamTag seamIndexTag)
        {
            throw new NotImplementedException();
        }

        public void RemoveSeamIndexTag()
        {
            if (!(SeamIndexTag is null))
            {
                SeamIndexTag.Delete();
                SeamIndexTag = null;
            }
        }

        #region Constructors

        public CanvasLayoutArea(
            CanvasManager canvasManager
            , string guid
            , LayoutAreaType layoutAreaType
            , string parentAreaGuid
            , List<string> offspringAreaGuidList
            , CanvasDirectedPolygon externalPerimeter
            , List<CanvasDirectedPolygon> internalPerimeters
            , List<CanvasSeam> canvasSeamList
            , List<CanvasSeam> displayCanvasSeamList
            , List<GraphicsRollout> rolloutList
            , UCAreaFinishPaletteElement ucAreaPaletteElement
            , AreaFinishManager areaFinishManager
            , AreaFinishManager prevAreaFinishManger
            , FinishesLibElements finishLibElements
            , bool isVisible
            //, bool isSeamStateLocked
            , double fixedWidthWidth
            , DesignState originatingDesignState
            , int prevSeamAreaIndex
            , bool areaDesignStateEditModeSelected
            , bool seamDesignStateSelectionModeSelected
            , bool seamDesignStateSubdivisionModeSelected
            , CanvasSeamTag canvasSeamTag
            , bool isZeroAreaLayoutArea
            ) : base(
                canvasManager.Window
                , canvasManager.CurrentPage
                , finishLibElements)
        {
            // MDD Reset 2025-02-08

            if (externalPerimeter == null)
            {
                ;
            }

            this.CreatedDateTime = DateTime.Now;

            this.canvasManager = canvasManager;

            this.Page = canvasManager.Page;

            this.Window = canvasManager.Window;

            this.AreaFinishManager = areaFinishManager;

            Debug.Assert(this.AreaFinishManager != null);

            #region Validations

            if (!VisioValidations.ValidateWindowParm(Window, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid Window parameter", 1, true);
            }

            if (!VisioValidations.ValidatePageParm(Page, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid Page parameter", 1, true);
            }

            #endregion

            this.Guid = guid;

            this.LayoutAreaType = layoutAreaType;

            this.ExternalArea = externalPerimeter;

            // MDD Reset 2025-02-08

            if (externalPerimeter == null)
            {
                ;
            }
            this.InternalAreas = internalPerimeters;

            // Note that we do not actually set up the seam state locks here. This is done the first time
            // the system transitions to seam state.

            //this._isSeamStateLocked = isSeamStateLocked;

            //InitializeSeamLayers(Window, Page);

            this.IsZeroAreaLayoutArea = isZeroAreaLayoutArea;

            this.OffspringAreaGuidList = offspringAreaGuidList;

            // Re-establish the canvas seams in the system definitions.

            foreach (CanvasSeam canvasSeam in canvasSeamList)
            {
                canvasSeam.layoutArea = this;
                canvasSeam.SeamFinishBase = canvasSeam.layoutArea.AreaFinishManager.SeamFinishBase;

                // There are three levels of 'lists' that maintain the seams at different levels of abstraction.
                // We add to each list explicitly.

                CanvasSeamList.AddBase(canvasSeam);
                GraphicsSeamList.AddBase(canvasSeam.GraphicsSeam);
                SeamList.AddBase(canvasSeam.GraphicsSeam.Seam);
            }

            foreach (CanvasSeam displayCanvasSeam in displayCanvasSeamList)
            {
                displayCanvasSeam.layoutArea = this;
                displayCanvasSeam.SeamFinishBase = displayCanvasSeam.layoutArea.AreaFinishManager.SeamFinishBase;

                DisplayCanvasSeamList.AddBase(displayCanvasSeam);
                GraphicsDisplaySeamList.AddBase(displayCanvasSeam.GraphicsSeam);
                DisplaySeamList.AddBase(displayCanvasSeam.GraphicsSeam.Seam);
            }
            // Re-establish the rollouts and sub-elements in the system definitions.

            foreach (GraphicsRollout graphicsRollout in rolloutList)
            {
                RolloutList.AddBase(graphicsRollout);
                GraphicsRolloutList.AddBase(graphicsRollout);

                // Kludge to fix problem with unknown source

                graphicsRollout.ParentGraphicsLayoutArea = this;
            }

            this.PrevAreaFinishManager = prevAreaFinishManger;

            this.IsVisible = IsVisible;

            this.BorderWidthInInches = fixedWidthWidth;

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                this.setupBorderAreaOverageOrUndrage();
            }


            this.OriginatingDesignState = originatingDesignState;

            //this.SeamAreaIndex = seamAreaIndex;

            this.SeamIndexTag = canvasSeamTag;

            this.PrevSeamAreaIndex = prevSeamAreaIndex;

            this.AreaDesignStateEditModeSelected = areaDesignStateEditModeSelected;

            this.SeamDesignStateSelectionModeSelected = seamDesignStateSelectionModeSelected;

            this.SeamDesignStateSubdivisionModeSelected = seamDesignStateSubdivisionModeSelected;

            this.ParentAreaGuid = parentAreaGuid;


            setupCommon();

        }

        // Constructor for overs generator layout area

        public CanvasLayoutArea(
           CanvasManager canvasManager
           , AreaFinishManager areaFinishManager
           , FinishesLibElements finishLibElements
           , CanvasDirectedPolygon externalPerimeter
           , List<CanvasDirectedPolygon> internalPerimeterList
            , double oversGeneratorOversWidthInInches
           , DesignState originatingDesignState = DesignState.Area
           , LayoutAreaType layoutAreaType = LayoutAreaType.OversGenerator) :
            this
                (
                     canvasManager
                    //,ucAreaFinish
                    , areaFinishManager
                    , finishLibElements
                    , externalPerimeter
                    , internalPerimeterList
                    , originatingDesignState = DesignState.Area
                    , layoutAreaType = LayoutAreaType.OversGenerator
                )

        {

            // MDD Reset 2025-02-08

            if (externalPerimeter == null)
            {
                ;
            }

            this.CreatedDateTime = DateTime.Now;

            this.Window = canvasManager.Window;

            this.Page = canvasManager.Page;

            #region Validations

            if (!VisioValidations.ValidateWindowParm(Window, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid Window parameter", 1, true);
            }

            if (!VisioValidations.ValidatePageParm(Page, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid Page parameter", 1, true);
            }

            #endregion

            this.AreaFinishManager = areaFinishManager;

            Debug.Assert(this.AreaFinishManager != null);

            OversGeneratorOversWidthInInches = oversGeneratorOversWidthInInches;
        }

        // Constructor for normal, color only and fixed width layout area

        public CanvasLayoutArea(
            CanvasManager canvasManager

            , AreaFinishManager areaFinishManager
            , FinishesLibElements finishesLibElements
            , CanvasDirectedPolygon externalPerimeter
            , List<CanvasDirectedPolygon> internalPerimeterList
            , DesignState originatingDesignState = DesignState.Area
            , LayoutAreaType layoutAreaType = LayoutAreaType.Normal)
            :
            base(
                canvasManager.Window
                , canvasManager.Page
                , finishesLibElements
                , (GraphicsDirectedPolygon)externalPerimeter
                , internalPerimeterList.Select(i => (GraphicsDirectedPolygon)i).ToList())
        {
            // MDD Reset 2025-02-08

            if (externalPerimeter == null)
            {
                ;
            }
            Debug.Assert(!(canvasManager is null));
            Debug.Assert(!(externalPerimeter is null));

            this.CreatedDateTime = DateTime.Now;

            this.canvasManager = canvasManager;

            this.Window = new GraphicsWindow(canvasManager.VsoWindow);

            this.Page = canvasManager.CurrentPage;

            this.AreaFinishManager = areaFinishManager;

            this.FinishesLibElements = finishesLibElements;

            Debug.Assert(this.AreaFinishManager != null);

            #region Validations

            if (!VisioValidations.ValidateWindowParm(Window, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid Window parameter", 1, true);
            }

            if (!VisioValidations.ValidatePageParm(Page, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid Page parameter", 1, true);
            }

            #endregion

            //this.UCAreaFinish = ucAreaFinish;

            this.AreaFinishManager = areaFinishManager;

            if (layoutAreaType == LayoutAreaType.FixedWidth)
            {
                this.LayoutAreaType = LayoutAreaType.Normal;
                this.CreatedFromFixedWidth = true;
            }

            else
            {
                this.LayoutAreaType = layoutAreaType;
                this.CreatedFromFixedWidth = false;
            }


            ExternalArea = externalPerimeter;
            InternalAreas = internalPerimeterList;

            //-----------------------------------------------------------------------------//
            // Initially, the 'shape' of the canvas layout area is the external perimeter. //
            // This may change if the shape becomes a 'composite shape'.                   //
            // ----------------------------------------------------------------------------//

            CompositeShape = ExternalArea.Shape;

            ExternalArea.ParentLayoutArea = this;

            InternalAreas.ForEach(p => p.ParentLayoutArea = this);

            OriginatingDesignState = originatingDesignState;

            // Not sure why this is necessary, but need to clean this up eventually.

            if (string.IsNullOrEmpty(Guid))
            {
                Guid = GuidMaintenance.CreateGuid(this);
            }

            // For debugging.

            if (this.Shape.Guid != Guid)
            {
                throw new NotImplementedException();
            }

            if (this.Shape.Guid != this.Shape.Data3)
            {
                throw new NotImplementedException();
            }

            if (this.Shape.VisioShape.Data3 != this.Shape.Data3)
            {
                throw new NotImplementedException();
            }

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                this.BorderWidthInInches = canvasManager.FixedWidthScaleInInches();

                this.setupBorderAreaOverageOrUndrage();
            }

            setupCommon();
        }

        public CanvasLayoutArea(
            CanvasManager canvasManager
            , GraphicsWindow window
            , GraphicsPage page
            , AreaFinishManager areaFinishManager
            , FinishesLibElements finishLibElements
            , UCAreaFinishPaletteElement ucAreaFinish
            , CanvasDirectedPolygon externalPerimeter
            , List<CanvasDirectedPolygon> internalPerimeterList
            , DesignState originatingDesignState = DesignState.Area
            , bool isZeroLayoutArea = false
            , LayoutAreaType layoutAreaType = LayoutAreaType.Normal
            )
            :
            base(window
                , page
                , finishLibElements
                , (GraphicsDirectedPolygon)externalPerimeter
                , internalPerimeterList.Select(i => (GraphicsDirectedPolygon)i).ToList())
        {
            // MDD Reset 2025-02-08

            if (externalPerimeter == null)
            {
                ;
            }
            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid Window parameter", 1, true);
            }

            if (!VisioValidations.ValidatePageParm(page, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid Page parameter", 1, true);
            }

            #endregion

            this.CreatedDateTime = DateTime.Now;

            this.AreaFinishManager = areaFinishManager;

            this.FinishesLibElements = finishLibElements;

            Debug.Assert(this.AreaFinishManager != null);

            this.canvasManager = canvasManager;

            this.Window = window;

            this.Page = page;

            this.IsZeroAreaLayoutArea = IsZeroAreaLayoutArea;

            this.ExternalArea = externalPerimeter;

            this.internalAreas = internalPerimeterList;

            ExternalArea.ParentLayoutArea = this;

            InternalAreas.ForEach(p => p.ParentLayoutArea = this);

            OriginatingDesignState = originatingDesignState;

            Guid = GuidMaintenance.CreateGuid(this);

            this.LayoutAreaType = layoutAreaType;

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                this.BorderWidthInInches = canvasManager.FixedWidthScaleInInches();

                this.setupBorderAreaOverageOrUndrage();
            }

            setupCommon();

        }

        public CanvasLayoutArea(
            CanvasManager canvasManager
            , LayoutArea layoutArea
            , AreaFinishManager areaFinishManager
            , FinishesLibElements finishManagerElements
            , UCAreaFinishPaletteElement ucAreaFinish
            , DesignState originatingDesignState = DesignState.Area
            , bool isZeroLayoutArea = false
            , LayoutAreaType layoutAreaType = LayoutAreaType.Normal) :
            base(
                canvasManager.Window
                , canvasManager.Page
                , finishManagerElements)
        {
            Debug.Assert(!(canvasManager is null));
            Debug.Assert(!(layoutArea is null));

            this.CreatedDateTime = DateTime.Now;

            this.canvasManager = canvasManager;

            this.Window = canvasManager.Window;


            this.Page = canvasManager.CurrentPage;

            this.AreaFinishManager = areaFinishManager;

            Debug.Assert(this.AreaFinishManager != null);

            #region Validations

            if (!VisioValidations.ValidateWindowParm(Window, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid Window parameter", 1, true);
            }

            if (!VisioValidations.ValidatePageParm(Page, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid Page parameter", 1, true);
            }

            #endregion

            this.IsZeroAreaLayoutArea = IsZeroAreaLayoutArea;

            this.LayoutAreaType = layoutAreaType;

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                this.BorderWidthInInches = canvasManager.FixedWidthScaleInInches();

                this.setupBorderAreaOverageOrUndrage();
            }

            this.ExternalArea = new CanvasDirectedPolygon(canvasManager, layoutArea.ExternalArea, originatingDesignState);
            // MDD Reset 2025-02-08

            if (ExternalArea == null)
            {
                ;
            }
            this.ExternalArea.ParentLayoutArea = this;

            this.ExternalArea.UCAreaFinish = ucAreaFinish;

            base.ExternalArea = (GraphicsDirectedPolygon)this.ExternalArea;

            this.OriginatingDesignState = originatingDesignState;

            Shape = this.ExternalArea.Shape;

            foreach (DirectedPolygon ia in layoutArea.InternalAreas)
            {
                CanvasDirectedPolygon internalArea = new CanvasDirectedPolygon(canvasManager, ia, originatingDesignState);

                internalArea.ParentLayoutArea = this;

                InternalAreasAdd(internalArea);
            }

            setupCommon();

            this.LayoutAreaType = layoutAreaType;

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                this.BorderWidthInInches = canvasManager.FixedWidthScaleInInches();

                this.setupBorderAreaOverageOrUndrage();
            }

            AreaFinishManager.SetupAllSeamLayers();
        }

        private void setupCommon()
        {
            base.GraphicsSeamList.ItemAdded += GraphicsSeamList_ItemAdded;
            base.GraphicsSeamList.ItemRemoved += GraphicsSeamList_ItemRemoved;
            base.GraphicsSeamList.ListCleared += GraphicsSeamList_ListCleared;

            GlobalSettings.AreaIndexFontInPtsChanged += GlobalSettings_AreaIndexFontInPtsChanged;
            this.AreaFinishManager.AreaFinishBase.FillPatternLineInterlineDistanceInFtChanged += AreaFinishBase_FillPatternLineInterlineDistanceInFtChanged;

            base.ParentAreaGuid = Guid;
            base.ParentArea = this;

            this.ShapeType = ShapeType.LayoutArea;

            if (this.Shape != null)
            {
                Shape.ShapeType = ShapeType.LayoutArea;
                Shape.ParentObject = this;
            }
        }

        private void AreaFinishBase_FillPatternLineInterlineDistanceInFtChanged(AreaFinishBase finishBase, double InterlineDistanceInFt)
        {
            SetFillPattern();
        }

        private void GlobalSettings_AreaIndexFontInPtsChanged(double areaIndexFontInPts)
        {
            if (SeamIndexTag is null)
            {
                return;
            }

            if (SeamIndexTag.Shape is null)
            {
                return;
            }

            VisioInterop.SetTextFontSize(this.SeamIndexTag.Shape, (int)Math.Round(areaIndexFontInPts));

        }

        private void GraphicsSeamList_ItemAdded(GraphicsSeam graphicsSeam)
        {
            CanvasSeam canvasSeam = new CanvasSeam(Window, Page, this, graphicsSeam);

            this.CanvasSeamList.Add(new CanvasSeam(Window, Page, this, graphicsSeam));
        }

        private void GraphicsSeamList_ItemRemoved(GraphicsSeam graphicsSeam)
        {
            foreach (CanvasSeam canvasSeam in CanvasSeamList)
            {
                if (canvasSeam.GraphicsSeam == graphicsSeam)
                {
                    CanvasSeamList.Remove(canvasSeam);
                }
            }
        }

        private void GraphicsSeamList_ListCleared()
        {
            CanvasSeamList.Clear();
        }


        #endregion

        public CanvasDirectedPolygon AddInternalPerimeter(CanvasDirectedPolygon canvasDirectedPolygon, UCAreaFinishPaletteElement ucFinish)
        {
            SystemGlobals.DebugFlag = true;

            canvasDirectedPolygon.ParentLayoutArea = this;

            this.InternalAreasAdd(canvasDirectedPolygon);


            GraphicShape internalShape = canvasDirectedPolygon.CreateInternalAreaShape();

            RemoveInternalAreas(new List<GraphicShape> { internalShape });

            foreach (CanvasDirectedLine internalPerimeterLine in canvasDirectedPolygon)
            {
                internalPerimeterLine.LineRole = LineRole.InternalPerimeter;
                internalPerimeterLine.SetShapeData();

                this.areaFinishLayers.AreaPerimeterLayer.AddShape(internalPerimeterLine, 1);
            }

            SystemGlobals.DebugFlag = false;

            Window?.DeselectAll();

            return canvasDirectedPolygon;
        }

        public void CreateTakeoutArea()
        {
            if (this.InternalAreas.Count <= 0)
            {
                return;
            }

            List<GraphicShape> internalAreaShapes = InternalAreas.Select(ia => ia.Shape).ToList();

            RemoveInternalAreas(internalAreaShapes);

        }

        public void AddBackInternalAreas(List<GraphicShape> internalAreaShapes)
        {
            if (internalAreaShapes == null)
            {
                return;
            }

            if (internalAreaShapes.Count <= 0)
            {
                return;
            }

            string prevGuid = this.Guid;

            //UCAreaFinishPaletteElement ucFinish = this.UCAreaFinish;

            areaFinishLayers.AreaDesignStateLayer.RemoveShapeFromLayer(Shape, 1);

            canvasManager.CurrentPage.RemoveLayoutArea(Guid);

            foreach (GraphicShape internalShape in internalAreaShapes)
            {
                VisioInterop.SetBaseFillColor(internalShape, AreaFinishManager.UCAreaPaletteElement.BackColor);
            }

            Shape = VisioInterop.Union(this, Window, Page, Shape, internalAreaShapes);


            Guid = Shape.Guid;

            VisioInterop.SetShapeData(Shape, "[Layout Area]", "Compound Shape[" + this.AreaFinishManager.AreaName + "]", Guid);

            this.ExternalArea.Guid = Guid;


            if (AreaFinishManager.CanvasLayoutAreaDict.ContainsKey(prevGuid))
            {
                AreaFinishManager.RemoveLayoutArea(prevGuid);
                AreaFinishManager.AddLayoutArea(this);
            }

            canvasManager.CurrentPage.AddLayoutArea(this);

            areaFinishLayers.AreaDesignStateLayer.AddShape(Shape, 1);
        }

        public void RemoveInternalAreas(List<GraphicShape> internalAreaShapes)
        {
            if (internalAreaShapes == null)
            {
                return;
            }

            string result = string.Empty;

            if (internalAreaShapes.Count <= 0)
            {
                return;
            }

            string prevGuid = this.Guid;

            //UCAreaFinishPaletteElement ucFinish = this.UCAreaFinish;

            //AreaFinishManager areaFinishManager = ucFinish.AreaFinishManager;

            areaFinishLayers.AreaDesignStateLayer.RemoveShapeFromLayer(this, 1);
            areaFinishLayers.SeamDesignStateLayer.RemoveShapeFromLayer(this, 1);

            this.AreaFinishManager.RemoveLayoutArea(this.Guid);

            canvasManager.CurrentPage.RemoveLayoutArea(prevGuid);

            Page.RemoveFromPageShapeDict(prevGuid);


            result = DebugSupportRoutines.ExternalAreaGuidCheck(this);

            Shape = VisioGeometryEngine.Subtract(this, Window, Page, Shape, internalAreaShapes);

            result = DebugSupportRoutines.ExternalAreaGuidCheck(this);

            result = DebugSupportRoutines.ExternalAreaGuidCheck(this);

            VisioInterop.SetShapeData(Shape, "Layout Area Shape", "Compound Shape[" + this.AreaFinishManager.AreaName + "]", Shape.Guid);

            //this.ExternalArea.Guid = Guid;

            this.Shape = Shape;

            this.AreaFinishManager.AddLayoutArea(this);

            canvasManager.CurrentPage.AddLayoutArea(this);

            areaFinishLayers.AreaDesignStateLayer.AddShape(this, 1);
            areaFinishLayers.SeamDesignStateLayer.AddShape(this, 1);
        }

        public double PerimeterLength()
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                return 0;
            }

            if (LayoutAreaType == LayoutAreaType.ColorOnly || LayoutAreaType == LayoutAreaType.OversGenerator)
            {
                return 0;
            }

            double externalPerimeterLength = ExternalArea.PerimeterLength();
            double internalPerimeterLength = InternalAreas.Sum(s => s.PerimeterLength());

            return externalPerimeterLength + internalPerimeterLength;
            //return ExternalPerimeter.PerimeterLength() + InternalPerimeters.Sum(s => s.PerimeterLength());
        }

        public double PerimeterLengthIncludingZeroLines()
        {
            if (LayoutAreaType == LayoutAreaType.OversGenerator)
            {
                return 0;
            }

            double externalPerimeterLength = ExternalArea.PerimeterLengthIncludingZeroLines();
            double internalPerimeterLength = InternalAreas.Sum(s => s.PerimeterLengthIncludingZeroLines());

            return externalPerimeterLength + internalPerimeterLength;
            //return ExternalPerimeter.PerimeterLength() + InternalPerimeters.Sum(s => s.PerimeterLength());
        }

        public void SetFillColorAndPattern(Color color, int pattern)
        {
            if (this.AreaFinishBase.Pattern == 0)
            {
                this.SetFillColor(this.AreaFinishBase.Color);
                this.SetFillOpacity(this.AreaFinishBase.Opacity);
            }

            else
            {
                //**************************************************************************************************//
                // Apparantly the order of the following three calls is important to get the correct overall effect //
                //**************************************************************************************************//

                this.SetFillColor(this.AreaFinishBase.Color);
                this.SetFillOpacity(this.AreaFinishBase.Opacity);
                this.SetFillPattern(this.AreaFinishBase.Color, this.AreaFinishBase.Pattern);

            }

        }

        public void SetFillColor(Color color)
        {
            VisioInterop.SetBaseFillColor(this.Shape, color);
        }

        public void SetFillColor(string visioFillColorFormula)
        {
            VisioInterop.SetBaseFillColor(this.Shape, visioFillColorFormula);
        }

        public void SetPatternColor(Color color)
        {
            VisioInterop.SetPatternColor(this.Shape, color);
        }

        public void SetPatternColor(string visioFillColorFormula)
        {
            VisioInterop.SetPatternColor(this.Shape, visioFillColorFormula);
        }

        public void SetFillTransparancy(string visioFillTransparencyFormula)
        {
            VisioInterop.SetFillTransparency(this.Shape, visioFillTransparencyFormula);
        }

        public void SetFillOpacity(double opacity)
        {
            VisioInterop.SetFillOpacity(this.Shape, opacity);
        }

        public void SetPatternOpacity(double opacity)
        {
            VisioInterop.SetPatternOpacity(this.Shape, opacity);
        }

        public void SetPatternTransparency(string visioTransparencyFormula)
        {
            VisioInterop.SetPatternTransparency(this.Shape, visioTransparencyFormula);
        }

        public PatternFill PatternFill { get; set; } = null;

        public void SetFillPattern()
        {
            Color color = this.AreaFinishBase.Color;
            int pattern = this.AreaFinishBase.Pattern;

            SetFillPattern(color, pattern);
        }

        public void SetFillPattern(Color color, int pattern)
        {
            if (PatternFill != null)
            {
                //if (!PatternFill.NeedsUpdating())
                //{
                //    return;
                //}

                PatternFill.Delete(Page);
                PatternFill = null;
            }

            if (pattern == 0)
            {

                this.SetFillColor(color);
                this.SetFillOpacity(this.AreaFinishBase.Opacity);
            }

            else
            {
                PatternFill = new PatternFill(this, this.AreaFinishManager);

                PatternFill.GeneratePatternLines();

                PatternFill.Draw(Page);

                SetFillOpacity(0);

                Window.DeselectAll();
            }

            //VisioInterop.SetFillPattern(this.Shape, pattern.ToString(), color);
        }

        public void SetLineGraphics(DesignState designState, AreaShapeBuildStatus buildStatus)
        {
            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                SetLineGraphicsForBorderArea();

                return;
            }

            switch (designState)
            {
                case DesignState.Area: SetLineGraphicsForAreaDesignState(buildStatus); return;
                case DesignState.Line: SetLineGraphicsForLineDesignState(buildStatus); return;
                case DesignState.Seam: SetLineGraphicsForSeamDesignState(buildStatus); return;

                default: return;
            }
        }

        public void SetLineGraphicsForBorderArea()
        {
            ExternalArea.SetLineGraphicsForBorderArea();
        }

        public void SetLineGraphicsForAreaDesignState(AreaShapeBuildStatus buildStatus)
        {
            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                return;
            }

            this.Shape.SetLineStyle(0);

            ExternalArea.HideLineGraphics();

            InternalAreas.ForEach(s => s.HideLineGraphics());
        }

        public void SetLineGraphicsForLineDesignState(AreaShapeBuildStatus buildStatus)
        {
            // No line graphics associated with fixed width areas.

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                return;
            }

            ExternalArea.HideLineGraphics();

            InternalAreas.ForEach(s => s.HideLineGraphics());
        }

        public void SetLineGraphicsForSeamDesignState(AreaShapeBuildStatus buildStatus)
        {
            // No line graphics associated with border areas.

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                return;
            }


            if (!IsSubdivided())
            {
                // This is a leaf canvas layout area. Set all perimeter lines to their default status;

                //ExternalArea.ShowLineGraphics();
                this.Shape.SetLineStyle(VisioLineStyle.Solid);

                foreach (CanvasDirectedLine canvasDirectedLine in ExternalArea)
                {

                    VisioInterop.BringToFront(canvasDirectedLine.Shape);

                    canvasDirectedLine.SetBaseLineWidth(0.25);
                    canvasDirectedLine.SetBaseLineColor(Color.Gray);
                }

                //InternalAreas.ForEach(s => s.ShowLineGraphics());


                return;
            }

            else
            {
                // This is not a leaf canvas layout area

                ExternalArea.HideLineGraphics();

                InternalAreas.ForEach(s => s.HideLineGraphics());

                return;
            }

        }

        public void SetLineGraphics(DesignState designState, bool selected, AreaShapeBuildStatus buildStatus)
        {
            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                return;
            }

            if (Utilities.IsNotNull(ExternalArea))
            {
                ExternalArea.SetLineGraphics(designState, selected, buildStatus);
            }

            if (Utilities.IsNotNull(internalAreas))
            {
                InternalAreas.ForEach(s => s.SetLineGraphics(designState, selected, buildStatus));
            }
        }

        public void SetSeamLineGraphics(DesignState designState, bool selected)
        {
            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                return;
            }

            CanvasSeamList.ForEach(s => ((CanvasSeam)s).SetSeamLineWidth(designState, selected));
        }

        public void SetSeamTagIndexVisibility(bool selected)
        {
            if (this.LayoutAreaType == LayoutAreaType.Remnant)
            {
                return;
            }

            if (this.SeamIndexTag is null)
            {
                return;
            }

            if (this.SeamDesignStateSelectionModeSelected && selected)
            {
                this.SeamIndexTag.Redraw();
            }

            else
            {
                this.SeamIndexTag.Delete();
            }
        }

        public double NetAreaInSqrInches()
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                return 0;
            }

            if (LayoutAreaType == LayoutAreaType.OversGenerator || LayoutAreaType == LayoutAreaType.ColorOnly)
            {
                return 0;
            }

            double area = ExternalArea.NetAreaInSqrInches();

            area -= InternalAreas.Sum(s => s.NetAreaInSqrInches());

            return area;
        }

        //public double GrossAreaInSqrInches()
        //{
        //    return AreaFinishBase.GrossAreaInSqrInches;
        //}

        internal double TotalSeamLength()
        {
            return this.CanvasSeamList.Select(s => ((CanvasSeam)s).GraphicsSeam.Length()).Sum() / 12.0;
        }

        internal void SetDesignStateSelectedLineGraphics()
        {
            if (ExternalArea != null)
            {
                ExternalArea.SetDesignStateSelectedLineGraphics();
            }

            foreach (CanvasDirectedPolygon internalArea in InternalAreas)
            {
                internalArea.SetDesignStateSelectedLineGraphics();
            }
        }

        internal void AddBackToCanvas()
        {
            // Note: need to account for the situation where either the area finish or line finish has been removed.

            // MDD Reset

            if (!(OffspringAreas is null))
            {
                OffspringAreas.ForEach(o => o.AddBackToCanvas());
            }

            if (ExternalArea != null)
            {
                ExternalArea.Redraw(DesignState.Area, AreaShapeBuildStatus.Completed);

                if (!(AreaFinishManager is null))
                {
                    if (!AreaFinishManager.CanvasLayoutAreaDict.ContainsKey(this.Guid))
                    {
                        this.AreaFinishManager.AddNormalLayoutArea(this);
                    }
                }

                foreach (CanvasDirectedLine directedLine in ExternalArea)
                {
                    if (directedLine.ucLine != null)
                    {
                        directedLine.LineFinishManager.AddLineFull(directedLine);
                    }

                    //canvasManager.ShapeDict.Add(directedLine.Guid, directedLine); // This call duplicates the call through add

                    canvasManager.CurrentPage.AddToDirectedLineDict(directedLine);
                }

                PolygonInternalArea.VisioShape.Data3 = Guid;
            }

            this.canvasManager.CurrentPage.AddLayoutArea(this);

            List<GraphicShape> internalAreaShapes = new List<GraphicShape>();

            foreach (CanvasDirectedPolygon internalAreaShape in InternalAreas)
            {
                internalAreaShape.Redraw(DesignState.Area, AreaShapeBuildStatus.Completed);

                internalAreaShapes.Add(internalAreaShape.Shape);

                foreach (CanvasDirectedLine directedLine in internalAreaShape)
                {
                    if (directedLine.ucLine != null)
                    {
                        directedLine.LineFinishManager.AddLineFull(directedLine);
                    }

                    //canvasManager.ShapeDict.Add(directedLine.Guid, directedLine);

                    canvasManager.CurrentPage.AddToDirectedLineDict(directedLine);
                }
            }


            if (internalAreaShapes.Count > 0)
            {
                RemoveInternalAreas(internalAreaShapes);
            }

            if (!(SeamIndexTag is null))
            {
                SeamIndexTag.Redraw();
            }
            // The following is a kludge due to a design error. The UCAreaFinish element will recursively add offspring back to the 
            // finish, so you do not want offspring areas to be added back as in the statement below.

            if (!(this.ParentArea is null))
            {
                return;
            }

            this.SetLineGraphics(canvasManager.DesignState, AreaShapeBuildStatus.Completed);

            Window?.DeselectAll();

        }

        internal void AddToLayer(GraphicsLayer graphicsLayer)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { graphicsLayer.ToString() });
#endif

            try
            {
                graphicsLayer.AddShape(this.Shape, 1);

                //VisioInterop.AddShape(this.Shape, graphicsLayer);

                //---------------------------------------------------------//
                // The following has been removed because it is not        //
                // necessary to add the lines to the area layers as they   //
                // are in their own line oriented layers.                  //
                //---------------------------------------------------------//

#if REMOVED
                if (Utilities.IsNotNull(ExternalArea))
                {
                    ExternalArea.AddToLayer(graphicsLayer);
                }

                foreach (CanvasDirectedPolygon internalAreaShape in InternalAreas)
                {
                    internalAreaShape.AddToLayer(graphicsLayer);
                }
#endif
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("CanvasLayoutArea:AddToLayer throws an exception", ex, 1, true);
            }
        }

        internal void RemoveFromLayer(GraphicsLayer graphicsLayer)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });
#endif
            try
            {
                // Remove area shape from seam layer

                if (this.Shape != null)
                {
                    graphicsLayer.RemoveShape(this);
                }

                if (Utilities.IsNotNull(ExternalArea))
                {
                    ExternalArea.RemoveFromLayer(graphicsLayer);
                }

                foreach (CanvasDirectedPolygon internalAreaShape in InternalAreas)
                {
                    internalAreaShape.RemoveFromLayer(graphicsLayer);
                }

                //foreach (CanvasSeam canvasSeam in CanvasSeamList)
                //{
                //    canvasSeam.GraphicsSeam.Undraw();
                //}

                if (Utilities.IsNotNull(SeamIndexTag))
                {
                    SeamIndexTag.Delete();
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("CanvasLayoutArea:RemoveFromSeamLayer throws an exception", ex, 1, true);
            }
        }

        /// <summary>
        /// Removes the canvas layout area and all elements from the visio surface
        /// </summary>
        internal void RemoveFromCanvas(bool deleteAssociatedLines = false)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });
#endif
            try
            {
                Debug.Assert(!(canvasManager is null));

                if (Utilities.IsNotNull(ExternalArea))
                {
                    ExternalArea.Delete(deleteAssociatedLines);

                    //---------------------------------------------------------//
                    // The following is not necessary because the lines are    //
                    // deleted in the call to ExternalArea.Delete              //
                    //---------------------------------------------------------//

                    //foreach (CanvasDirectedLine directedLine in ExternalArea)
                    //{
                    //    canvasManager.CurrentPage.RemoveDirectedLine(directedLine);
                    //}
                }

                foreach (CanvasDirectedPolygon internalAreaShape in InternalAreas)
                {
                    internalAreaShape.Delete(deleteAssociatedLines);

                    foreach (CanvasDirectedLine directedLine in internalAreaShape)
                    {
                        canvasManager.CurrentPage.RemoveFromDirectedLineDict(directedLine);
                    }
                }

                foreach (CanvasSeam canvasSeam in CanvasSeamList)
                {
                    canvasSeam.GraphicsSeam.Undraw();
                }

                if (!(SeamIndexTag is null))
                {
                    AreaFinishManager.SeamDesignStateLayer.RemoveShapeFromLayer(SeamIndexTag, 1);

                    SeamIndexTag.Delete();
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("CanvasLayoutArea:RemoveFromCanvas throws an exception", ex, 1, true);
            }
        }


        internal void Delete(bool deleteAssociatedLines = false)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });
#endif

            Debug.Assert(canvasManager != null);

            deleteCommon();

            //removeLockIcon();

            GraphicsDebugSupportRoutines.CheckForNullShapeInGraphicsLayers(Page, 1);

            Shape.Delete();

            DeleteRollouts();

            GraphicsDebugSupportRoutines.CheckForNullShapeInGraphicsLayers(Page, 2);

            if (this.LayoutAreaType == LayoutAreaType.Remnant)
            {
                if (Utilities.IsNotNull(this.SeamIndexTag))
                {
                    string seamAreaIndex = 'R' + (-this.SeamIndexTag.SeamAreaIndex).ToString();
                    canvasManager.BaseForm.ucRemnantsView.RemoveRemnantArea(seamAreaIndex);
                }
            }

            if (PatternFill != null)
            {
                PatternFill.Delete(Page);
            }

            DeleteSeams();

            if (ExternalArea != null)
            {
                ExternalArea.Delete(deleteAssociatedLines);

                ExternalArea = null;
            }

            foreach (CanvasDirectedPolygon internalAreaShape in InternalAreas)
            {
                internalAreaShape.Delete(deleteAssociatedLines);
            }

            InternalAreas.Clear();

            foreach (CanvasSeam canvasSeam in CanvasSeamList)
            {
                canvasSeam.Delete();
            }

            CanvasSeamList.ClearBase();
            GraphicsSeamList.ClearBase();
            SeamList.ClearBase();

            foreach (GraphicsCut graphicsCut in this.GraphicsCutList)
            {
                graphicsCut.Delete();
            }

            GraphicsCutList.Clear();

            foreach (GraphicsOverage graphicsOverage in this.GraphicsOverageList)
            {
                graphicsOverage.Delete();
            }

            GraphicsOverageList.Clear();

            foreach (GraphicsUndrage graphicsUnderage in this.GraphicsUndrageList)
            {
                graphicsUnderage.Delete();
            }

            GraphicsUndrageList.Clear();

            if (!(SeamIndexTag is null))
            {
                SeamIndexTag.Delete();
                SeamIndexTag = null;
            }

            this.canvasManager.CurrentPage.RemoveLayoutArea(this);

            if (Utilities.IsNotNull(CopyMarker))
            {
                AreaFinishManager.AreaDesignStateLayer.RemoveShapeFromLayer(CopyMarker, 1);

                Page.RemoveFromPageShapeDict(CopyMarker.Shape);

                CopyMarker.Delete();

                CopyMarker = null;
            }

            if (Utilities.IsNotNull(VertexEditMarker))
            {
                VertexEditMarker.Delete();

                VertexEditMarker = null;
            }

            //areaFinishLayers.DeleteSeamLayers();

            Page.RemoveFromPageShapeDict(this.Guid);
        }

        private void deleteCommon()
        {
            //--------------------------------------------------------------------//
            // Unsubscribe from events to avoid memory leaks                      //
            //--------------------------------------------------------------------//

            base.GraphicsSeamList.ItemAdded -= GraphicsSeamList_ItemAdded;
            base.GraphicsSeamList.ItemRemoved -= GraphicsSeamList_ItemRemoved;
            base.GraphicsSeamList.ListCleared -= GraphicsSeamList_ListCleared;

            GlobalSettings.AreaIndexFontInPtsChanged -= GlobalSettings_AreaIndexFontInPtsChanged;
        }

        public List<CanvasLayoutArea> Intersect(CanvasDirectedPolygon canvasPolygon, DesignState designState)
        {
            LayoutArea layoutArea = (LayoutArea)this;

            List<LayoutArea> results = layoutArea.Intersect((DirectedPolygon)canvasPolygon);

            UCLineFinishPaletteElement ucLine = MaxLineType();

            List<CanvasLayoutArea> returnList = layoutAreaListToCanvasLayoutAreaList(results, ucLine, designState);

            return returnList;
        }

        public List<CanvasLayoutArea> Subtract(CanvasDirectedPolygon canvasPolygon, DesignState designState)
        {
            LayoutArea layoutArea = (LayoutArea)this;

            List<LayoutArea> results = layoutArea.Subtract((DirectedPolygon)canvasPolygon);

            UCLineFinishPaletteElement ucLine = MaxLineType();

            List<CanvasLayoutArea> returnList = layoutAreaListToCanvasLayoutAreaList(results, ucLine, designState);

            return returnList;
        }

        private List<CanvasLayoutArea> layoutAreaListToCanvasLayoutAreaList(List<LayoutArea> layoutAreaList, UCLineFinishPaletteElement ucLine, DesignState designState)
        {
            List<CanvasLayoutArea> returnList = new List<CanvasLayoutArea>();

            foreach (LayoutArea layoutArea1 in layoutAreaList)
            {
                returnList.Add(ConvertToCanvasLayoutArea(layoutArea1, ucLine, designState));

            }

            return returnList;
        }

        private CanvasLayoutArea ConvertToCanvasLayoutArea(LayoutArea layoutArea, UCLineFinishPaletteElement ucLine, DesignState designState)
        {

            GraphicsDirectedPolygon graphicsDirectedPolygon = new GraphicsDirectedPolygon(this.Window, this.Page, layoutArea.ExternalArea);

            CanvasDirectedPolygon externalPerimeter = new CanvasDirectedPolygon(
                this.canvasManager, this.Window, this.Page, graphicsDirectedPolygon, ucLine, designState);

            List<CanvasDirectedPolygon> internalPerimeters = new List<CanvasDirectedPolygon>();

            foreach (DirectedPolygon internalPerimeter in layoutArea.InternalAreas)
            {
                GraphicsDirectedPolygon internalDirectedPolygon = new GraphicsDirectedPolygon(this.Window, this.Page, internalPerimeter);

                internalPerimeters.Add(new CanvasDirectedPolygon(
                    this.canvasManager, this.Window, this.Page, internalDirectedPolygon, ucLine, DesignState.Seam));
            }

            CanvasLayoutArea canvasLayoutArea = new CanvasLayoutArea(
                this.canvasManager
                , AreaFinishManager
                , FinishesLibElements
                , externalPerimeter
                , internalPerimeters
                , DesignState.Seam);

            return canvasLayoutArea;
        }

        public UCLineFinishPaletteElement MaxLineType()
        {
            Dictionary<string, int> lineTypeDict = new Dictionary<string, int>();

            if (!(this.ExternalArea is null))
            {
                foreach (CanvasDirectedLine line in this.ExternalArea)
                {
                    string guid = line.ucLine.Guid;

                    if (!lineTypeDict.ContainsKey(guid))
                    {
                        lineTypeDict[guid] = 1;
                    }

                    else
                    {
                        lineTypeDict[guid]++;
                    }
                }
            }

            foreach (CanvasDirectedPolygon internalPerimeter in this.internalAreas)
            {
                foreach (CanvasDirectedLine line in internalPerimeter)
                {
                    string guid = line.ucLine.Guid;

                    if (!lineTypeDict.ContainsKey(guid))
                    {
                        lineTypeDict[guid] = 1;
                    }

                    else
                    {
                        lineTypeDict[guid]++;
                    }
                }
            }

            string maxGuid = string.Empty;

            int maxCount = 0;

            foreach (KeyValuePair<string, int> kvp in lineTypeDict)
            {
                if (kvp.Value > maxCount)
                {
                    maxGuid = kvp.Key;
                    maxCount = kvp.Value;
                }
            }

            return canvasManager.LineFinishPalette[maxGuid];
        }

        public bool IsVisible
        {
            get;
            set;
        } = true;

        public List<CanvasDirectedLine> Perimeter
        {
            get
            {
                return ExternalArea.Perimeter;
            }

            set => throw new NotImplementedException();
        }

        public GraphicShape PolygonInternalArea
        {
            get
            {
                return ExternalArea.PolygonInternalArea;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private AreaShapeBuildStatus buildStatus = AreaShapeBuildStatus.Unknown;

        public AreaShapeBuildStatus BuildStatus
        {
            get
            {
                return buildStatus;
            }

            set
            {
                buildStatus = value;
            }
        }

        internal GraphicShape Draw(GraphicsWindow window, GraphicsPage page)
        {
            //ExternalArea.Draw(Page, AreaFinishBase.Color);

            //this.Shape = ExternalArea.Shape;

            DrawCompositeShape(window, page);

            return this.Shape;
        }


        internal CanvasLayoutArea GetSibling()
        {

            if (ParentArea is null)
            {
                return null;
            }

            if (ParentArea.OffspringAreas.Count > 2)
            {
                throw new NotImplementedException();
            }

            foreach (CanvasLayoutArea siblingLayoutArea in ParentArea.OffspringAreas)
            {
                if (siblingLayoutArea != this)
                {
                    if (siblingLayoutArea.IsSubdivided())
                    {
                        return null;
                    }

                    return siblingLayoutArea;
                }
            }

            return null;
        }

        internal List<CanvasLayoutArea> GetSiblings()
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });

            List<CanvasLayoutArea> rtrnList = new List<CanvasLayoutArea>();

            try
            {

                if (ParentArea is null)
                {
                    return rtrnList;
                }


                foreach (CanvasLayoutArea siblingLayoutArea in ParentArea.OffspringAreas)
                {
                    if (siblingLayoutArea != this)
                    {
                        if (siblingLayoutArea.IsSubdivided())
                        {
                            continue;
                        }

                        rtrnList.Add(siblingLayoutArea);
                    }
                }

                return rtrnList;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("CanvasLayoutArea:GetSiblings throws an exception", ex, 1, true);

                return null;
            }
        }

        public CanvasLayoutArea Clone()
        {
            CanvasDirectedPolygon clonedExternalArea = this.ExternalArea.Clone();

            List<CanvasDirectedPolygon> clonedInternalAreaList = new List<CanvasDirectedPolygon>();

            foreach (CanvasDirectedPolygon internalArea in this.internalAreas)
            {
                CanvasDirectedPolygon clonedInternalArea = internalArea.Clone();

                clonedInternalAreaList.Add(clonedInternalArea);
            }

            CanvasLayoutArea clonedLayoutArea
                = new CanvasLayoutArea(
                    this.canvasManager
                    , this.Window
                    , this.Page
                    , this.AreaFinishManager
                    , this.FinishesLibElements
                    , this.AreaFinishManager.UCAreaPaletteElement
                    , clonedExternalArea
                    , clonedInternalAreaList
                    , this.OriginatingDesignState
                    , this.IsZeroAreaLayoutArea
                    )
                {
                    IsZeroAreaLayoutArea = IsZeroAreaLayoutArea
                };

            clonedLayoutArea.Guid = GuidMaintenance.CreateGuid(clonedLayoutArea);

            //clonedLayoutArea.UCAreaFinish = this.UCAreaFinish;

            clonedLayoutArea.ExternalArea.ParentLayoutArea = clonedLayoutArea;

            foreach (CanvasDirectedLine canvasDirectedLine in clonedLayoutArea.ExternalArea)
            {
                this.canvasManager.CurrentPage.AddToDirectedLineDict(canvasDirectedLine);
            }

            foreach (CanvasDirectedPolygon internalArea in clonedLayoutArea.InternalAreas)
            {
                internalArea.ParentLayoutArea = clonedLayoutArea;

                foreach (CanvasDirectedLine canvasDirectedLine in internalArea)
                {
                    this.canvasManager.CurrentPage.AddToDirectedLineDict(canvasDirectedLine);
                }
            }

            return clonedLayoutArea;

        }

        public CanvasLayoutArea CloneBasic(GraphicsWindow window, GraphicsPage page)
        {
            CanvasDirectedPolygon clonedExternalArea = this.ExternalArea.CloneBasic(window, page);

            List<CanvasDirectedPolygon> clonedInternalAreaList = new List<CanvasDirectedPolygon>();

            foreach (CanvasDirectedPolygon internalArea in this.internalAreas)
            {
                CanvasDirectedPolygon clonedInternalArea = internalArea.CloneBasic(window, page);

                clonedInternalAreaList.Add(clonedInternalArea);
            }

            CanvasLayoutArea clonedLayoutArea
                = new CanvasLayoutArea(
                    null //this.canvasManager
                    , window
                    , page
                    , this.AreaFinishManager
                    , this.FinishesLibElements
                    , this.AreaFinishManager.UCAreaPaletteElement
                    , clonedExternalArea
                    , clonedInternalAreaList
                    , this.OriginatingDesignState
                    , this.IsZeroAreaLayoutArea
                    )
                {
                    IsZeroAreaLayoutArea = IsZeroAreaLayoutArea
                };

            clonedLayoutArea.Guid = GuidMaintenance.CreateGuid(clonedLayoutArea);

            // clonedLayoutArea.UCAreaFinish = this.UCAreaFinish;

            clonedLayoutArea.ExternalArea.ParentLayoutArea = clonedLayoutArea;

            foreach (CanvasDirectedLine canvasDirectedLine in clonedLayoutArea.ExternalArea)
            {
                this.canvasManager.CurrentPage.AddToDirectedLineDict(canvasDirectedLine);
            }

            foreach (CanvasDirectedPolygon internalArea in clonedLayoutArea.InternalAreas)
            {
                internalArea.ParentLayoutArea = clonedLayoutArea;

                foreach (CanvasDirectedLine canvasDirectedLine in internalArea)
                {
                    this.canvasManager.CurrentPage.AddToDirectedLineDict(canvasDirectedLine);
                }
            }

            return clonedLayoutArea;

        }

        internal Coordinate GetSelectedVertex(double x, double y, out CanvasDirectedLine line1, out CanvasDirectedLine line2)
        {
            Coordinate vertex = ExternalArea.GetSelectedVertex(x, y, out line1, out line2);

            if (!Coordinate.IsNullCoordinate(vertex))
            {
                return vertex;
            }

            foreach (CanvasDirectedPolygon interiorArea in InternalAreas)
            {
                vertex = interiorArea.GetSelectedVertex(x, y, out line1, out line2);

                if (Coordinate.IsNullCoordinate(vertex))
                {
                    return vertex;
                }
            }

            return Coordinate.NullCoordinate;

        }


        public Coordinate UpperLeftmostPoint()
        {
            double _minX = double.MaxValue;
            double _maxY = double.MinValue;

            foreach (CanvasDirectedLine line in Perimeter)
            {
                double minX = line.MinX;
                double maxY = line.MaxY;

                if (minX < _minX)
                {
                    _minX = minX;
                    _maxY = maxY;
                }

                else if (minX == _minX)
                {
                    if (maxY > _maxY)
                    {
                        _minX = minX;
                        _maxY = maxY;
                    }
                }
            }

            return new Coordinate(_minX, _maxY);
        }

        public Dictionary<string, GraphicShape> GenerateGraphicShapeDict()
        {
            Dictionary<string, GraphicShape> rtrnDict = ExternalArea.GenerateGraphicShapeDict();

            foreach (CanvasDirectedPolygon internalArea in internalAreas)
            {
                Dictionary<string, GraphicShape> internalAreaShapeDict = internalArea.GenerateGraphicShapeDict();

                foreach (KeyValuePair<string, GraphicShape> kvp in internalAreaShapeDict)
                {
                    if (kvp.Value == null)
                    {
                        continue;
                    }

                    if (kvp.Value.VisioShape == null)
                    {
                        continue;
                    }

                    if (rtrnDict.ContainsKey(kvp.Key))
                    {
#if DEBUG
                        CanvasLayoutAreaLogMessage("Attempt to add duplicate graphic shape " + kvp.Value.ToString() + " to dictionary while generating internal area shape list.");
#endif
                        continue;
                    }

                    rtrnDict.Add(kvp.Key, kvp.Value);
                }
            }

            foreach (GraphicsRollout graphicsRollout in this.GraphicsRolloutList)
            {
                Dictionary<string, GraphicShape> graphicsRolloutShapeDict = graphicsRollout.GenerateGraphicShapeDict();

                foreach (KeyValuePair<string, GraphicShape> kvp in graphicsRolloutShapeDict)
                {
                    if (kvp.Value == null)
                    {
                        continue;
                    }

                    if (kvp.Value.VisioShape == null)
                    {
                        continue;
                    }

                    if (rtrnDict.ContainsKey(kvp.Key))
                    {
                        continue;
                    }

                    rtrnDict.Add(kvp.Key, kvp.Value);
                }
            }

            foreach (GraphicsSeam graphicSeam in this.GraphicsSeamList)
            {
                Dictionary<string, GraphicShape> canvasSeamDict = graphicSeam.GenerateGrahpicsShapeDict();

                foreach (KeyValuePair<string, GraphicShape> kvp in canvasSeamDict)
                {

                    if (rtrnDict.ContainsKey(kvp.Key))
                    {
                        continue;
                    }

                    rtrnDict[kvp.Key] = kvp.Value;
                }


            }

            // Recursive call to get all shapes from offspring areas

            foreach (CanvasLayoutArea offspringLayoutArea in this.OffspringAreas)
            {
                Dictionary<string, GraphicShape> offspringGraphicShapeDict = offspringLayoutArea.GenerateGraphicShapeDict();

                foreach (KeyValuePair<string, GraphicShape> kvp in offspringGraphicShapeDict)
                {

                    if (rtrnDict.ContainsKey(kvp.Key))
                    {
#if DEBUG
                        CanvasLayoutAreaLogMessage("Attempt to add duplicate graphic shape " + kvp.Value.ToString() + " to dictionary while generating offspring shape list.");
#endif
                        continue;
                    }

                    rtrnDict.Add(kvp.Key, kvp.Value);
                }
            }

            return rtrnDict;
        }

        public static explicit operator GraphicShape(CanvasLayoutArea canvasLayoutArea)
        {
            return ((GraphicsLayoutArea)canvasLayoutArea).Shape;
        }

        static void CanvasLayoutAreaLogMessage(
            string message,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            //message += "\n" + $"File: {filePath}\n" + $"Line: {lineNumber}\n" + $"Member: {memberName}";
            //Console.WriteLine(message);

            DialogResult dr = MessageBoxAdv.Show(message, "Error in Generating Graphic Shape List", MessageBoxAdv.Buttons.OKCancel, MessageBoxAdv.Icon.Error);

            if (dr != DialogResult.OK)
            {
                Debugger.Break();
            }
        }
    }
}
