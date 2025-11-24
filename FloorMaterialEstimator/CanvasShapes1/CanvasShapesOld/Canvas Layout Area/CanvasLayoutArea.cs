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

namespace CanvasManagerLib
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

    using FinishesLib;
    using PaletteLib;
    using SettingsLib;
    using Utilities;
    using Globals;
    using MaterialsLayout.MaterialsLayout;
    using CanvasLib.Markers_and_Guides;
    using DebugSupport;
    using TracerLib;

    /// <summary>
    /// CanvasLayoutArea is the primary object type of this application.
    /// It represents a distinct area on the drawing that is being defined for finish and seaming.
    /// </summary>
    [Serializable]
    public partial class CanvasLayoutArea : GraphicsLayoutArea, IAreaShape
    {
        // Elements not equivalent to graphics layout area.

        //CanvasManager canvasManager;

        // Elements matching graphics layout area

        //**********************************************************************************************//
        //                                                                                              //
        //                           Links between parent and offspring areas.                          //
        //                                                                                              //
        //**********************************************************************************************//

        public new CanvasLayoutArea ParentArea
        {
            get
            {
                return (CanvasLayoutArea)base.ParentArea;
            }

            set
            {
                base.ParentArea = value;
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

        //**********************************************************************************************//

        //public bool IsZeroAreaLayoutArea
        //{
        //    get
        //    {
        //        return LayoutAreaType == LayoutAreaType.ZeroArea;
        //    }

        //    set
        //    {
        //        if (value)
        //        {
        //            LayoutAreaType = LayoutAreaType.ZeroArea;
        //        }
        //    }
        //}

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


        AreaFinishLayers areaFinishLayers => this.AreaFinishManager.AreaFinishLayers;

        public Shape DrawCompositeShape(GraphicsWindow window, GraphicsPage page, bool drawPolygonOnly = false)
        {
            if (externalArea is null)
            {
                CompositeShape = null;
                return null;
            }

            ExternalArea.UCAreaFinish = this.AreaFinishManager.UCAreaPaletteElement;

            this.Shape = ExternalArea.Draw(page, this.AreaFinishManager.Color, drawPolygonOnly);

            foreach (CanvasDirectedLine externalPerimeterLine in ExternalArea)
            {
                externalPerimeterLine.SetShapeData();
                //VisioInterop.SetShapeData(externalPerimeterLine.Shape, "External Perimeter Line", "Line [" + ExternalArea.Perimeter[0].ucLine.LineName + "]", externalPerimeterLine.Guid);
            }

            ExternalArea.SetFillOpacity(this.AreaFinishManager.Opacity);

            CompositeShape = Shape;
  
            if (internalAreas.Count <= 0)
            {
                return this.Shape;
            }

            List<Shape> shapes = new List<Shape>();

            Color lineColor = ExternalArea.Perimeter[0].ucLine.LineColor; // This is a heuristic to select the color of the first line, since there may be multiple line times on the perimeter.


            foreach (CanvasDirectedPolygon internalArea in internalAreas)
            {
                Shape internalAreaShape = internalArea.Draw(lineColor, Color.FromArgb(0, 255, 255, 255));

                shapes.Add(internalAreaShape);
            }

            Shape shape = VisioGeometryEngine.Subtract(window, page, externalArea.Shape, shapes);

            ExternalArea.PolygonInternalArea = shape;

            this.Shape = ExternalArea.Shape;

            return shape;
        }

        internal void Select()
        {
            VisioInterop.SelectShape(Window, this.Shape);
        }

        public Shape DrawCompositeShapeFull(GraphicsWindow window, GraphicsPage page)
        {
            if (externalArea is null)
            {
                CompositeShape = null;
                return null;
            }

            this.Shape = ExternalArea.Draw(page, this.AreaFinishManager.Color);

            ExternalArea.SetFillOpacity(this.AreaFinishManager.Opacity);

            CompositeShape = Shape;

            List<Shape> shapeList = new List<Shape>() { Shape };

            foreach (CanvasDirectedLine exteriorLine in ExternalArea)
            {
                shapeList.Add(exteriorLine.Shape);
            }

            shapeList.ForEach(s => s.Unlock());

            Shape groupedShape = VisioInterop.GroupShapes(Window, shapeList.ToArray());

            //shapeList.ForEach(s => s.SetLineColor(Color.Orange));

            shapeList.ForEach(s => s.SetLineOpacity(0));

            //shapeList.ForEach(s => s.SetLineWidth(0));

            if (internalAreas.Count <= 0)
            {
                return groupedShape;
            }

            List<Shape> shapes = new List<Shape>();

            Color lineColor = ExternalArea.Perimeter[0].ucLine.LineColor; // This is a heuristic to select the color of the first line, since there may be multiple line times on the perimeter.

            foreach (CanvasDirectedPolygon internalArea in internalAreas)
            {
                Shape internalAreaShape = internalArea.Draw(lineColor, Color.FromArgb(0, 255, 255, 255));

                shapes.Add(internalAreaShape);
            }

            Shape layoutShape = VisioGeometryEngine.Subtract(window, page, externalArea.Shape, shapes);

            return layoutShape;
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

            Shape copyMarkerShape = CopyMarker.Draw(GlobalSettings.AreaEditSettingColor2);

            copyMarkerShape.SetShapeData("Copy Marker", "Compound Shape[" + this.AreaFinishBase.AreaName + "]", CopyMarker.Guid);
            
            Page.AddToPageShapeDict(CopyMarker);

            this.AreaFinishManager.AreaDesignStateLayer.AddShapeToLayer(CopyMarker, 1);
        }


        private CanvasDirectedPolygon externalArea;

        public new CanvasDirectedPolygon ExternalArea
        {
            get
            {
                return externalArea;
            }

            set
            {
                externalArea = value;

                base.ExternalArea = (GraphicsDirectedPolygon)externalArea;
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

            // MDD Reset

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
                canvasSeam.Delete();

                CanvasSeamList.RemoveBase(canvasSeam);
                GraphicsSeamList.RemoveBase(canvasSeam.GraphicsSeam);
                SeamList.RemoveBase(canvasSeam.GraphicsSeam.Seam);
            }

            //int count1 = SeamList.Count;
            //int count2 = GraphicsSeamList.Count;
            //int count3 = CanvasSeamList.Count;

        }

        public new void DeleteRollouts()
        {
            base.DeleteRollouts(areaFinishLayers);
        }

        public AreaFinishBase AreaFinishBase => AreaFinishManager.AreaFinishBase;


        public double GrossAreaInSqrInches()
        {
            return AreaFinishBase.GrossAreaInSqrInches;
        }

        public CoordinatedList<CanvasSeam> CanvasSeamList { get; set; }  = new CoordinatedList<CanvasSeam>();

        public DirectedLine BaseSeamLineWall { get; set; }  = null;

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

            this.AreaFinishManager.SetupAllSeamLayers();
        }

        internal void GenerateSeamsAndRolloutsNormal(DirectedLine line)
        { 
            DeleteNonManualSeams();

            DeleteRollouts();

            GraphicsDirectedPolygon externalAreaShape = (GraphicsDirectedPolygon)this.ExternalArea;

            List<GraphicsDirectedPolygon> internalAreaShapes = new List<GraphicsDirectedPolygon>();

            this.InternalAreas.ForEach(s => internalAreaShapes.Add((GraphicsDirectedPolygon)s));

            //GraphicsWindow graphicsWindow = new GraphicsWindow(this.canvasManager.VsoWindow);

            double seamWidthInLocalUnits = 0;
            double materialWidthInLocalUnits = 0;
            double materialOverlapInLocalUnits = 0;

            seamWidthInLocalUnits = CanvasManagerGlobals.SelectedAreaFinishManager.RollWidthInInches - CanvasManagerGlobals.SelectedAreaFinishManager.OverlapInInches;

            seamWidthInLocalUnits /= Page.DrawingScaleInInches;

            materialWidthInLocalUnits = CanvasManagerGlobals.SelectedAreaFinishManager.RollWidthInInches / Page.DrawingScaleInInches;

            materialOverlapInLocalUnits = CanvasManagerGlobals.SelectedAreaFinishManager.OverlapInInches;

            // This should clear the corresponding seam list for all inhereted members

            //---------------------------------------------------------------------------------------------------------//
            // The following is deleted because it deletes all seams, manual and regular and regular seams are deleted
            // in the call to DeleteNonManualSeams above
            //---------------------------------------------------------------------------------------------------------//
            //SeamList.Clear();

            base.GenerateSeamsAndRollouts(line, 0, seamWidthInLocalUnits, materialWidthInLocalUnits, materialOverlapInLocalUnits, Page.DrawingScaleInInches);

            foreach (GraphicsRollout graphicsRollout in GraphicsRolloutList)
            {
                foreach (GraphicsCut graphicsCut in graphicsRollout.GraphicsCutList)
                {
                    // Bernie, Check here
                    Shape cutShape = graphicsCut.DrawBoundingRectangleAndIndex(areaFinishLayers.CutsIndexLayer.GetBaseLayer(), Color.Red, Color.White, 1);

                    VisioInterop.SetShapeData(cutShape, "Cut Bounding Polygon", "Polygon", cutShape.Guid);

                    CanvasManagerGlobals.CanvasPage.AddToPageShapeDict(cutShape);

                    graphicsCut.Shape = cutShape;

                    cutShape.SetFillOpacity(0);

                    cutShape.SetLineWidth(1);

                    cutShape.BringToFront();

                    areaFinishLayers.CutsLayer.AddShapeToLayer(cutShape, 1);

                    VisioInterop.LockShape(cutShape);

                    //UCAreaFinish.SeamDesignStateLayer.Add(cutShape, 1);

                   // Page.CutsLayer.Add(cutShape.cutin, 1);

                    foreach (GraphicsOverage overage in graphicsCut.GraphicsOverageList)
                    {
                        Shape overageShape = overage.DrawBoundingRectangleNormal(areaFinishLayers.OversLayer.GetBaseLayer(), Color.Green, Color.White, 1);

                        overage.Shape = overageShape;


                        overageShape.SetShapeData("[Overage]", "Composite Shape", overage.Guid);

                        Page.AddToPageShapeDict(overage);

                        VisioInterop.SetFillOpacity(overageShape, 0);

                        overageShape.BringToFront();

                        areaFinishLayers.OversLayer.AddShapeToLayer(overageShape, 1);

                        VisioInterop.LockShape(overageShape);
                    }
                }

                foreach (GraphicsUndrage underage in graphicsRollout.GraphicsUndrageList)
                {
                    Shape underageShape = underage.DrawBoundingRectangle(areaFinishLayers.UndrsLayer.GetBaseLayer(), Color.Orange, Color.White, 1);

                    underage.Shape = underageShape;

                    underageShape.SetShapeData("Underage Bounding Polygon", "Polygon");

                    Page.AddToPageShapeDict(underageShape);

                    VisioInterop.SetFillOpacity(underageShape, 0);

                    underageShape.BringToFront();

                    areaFinishLayers.UndrsLayer.AddShapeToLayer(underageShape, 1);

                    VisioInterop.LockShape(underageShape);
                }
                
            }

            if (GlobalSettings.ValidateRolloutAndCutWidth)
            {
                if (!DebugChecks.ValidateRolloutsAndCuts(this, AreaFinishBase.RollWidthInInches, Page.DrawingScaleInInches))
                {
                    MessageBox.Show("Invalid rollouts or cuts generated. Please note development path.");
                }
            }

            this.AreaFinishManager.UpdateCutIndices();

            canvasManager.UpdateAreaAndSeamsStats();
            canvasManager.UpdateAreaSeamsUndrsOversDataDisplay();
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

            //GraphicsWindow graphicsWindow = new GraphicsWindow(this.canvasManager.VsoWindow);

            double seamWidthInLocalUnits = 0;
            double materialWidthInLocalUnits = 0;
            double materialOverlapInLocalUnits = 0;

            seamWidthInLocalUnits = OversGeneratorOversWidthInInches;

            seamWidthInLocalUnits /= Page.DrawingScaleInInches;

            materialWidthInLocalUnits = CanvasManagerGlobals.SelectedAreaFinishManager.RollWidthInInches / Page.DrawingScaleInInches;

            materialOverlapInLocalUnits = CanvasManagerGlobals.SelectedAreaFinishManager.OverlapInInches;

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

                    foreach (RemnantCut remnantCut in graphicsCut.Cut.RemnantCutList)
                    {
                        GraphicsRemnantCut graphicsRemnantCut = new GraphicsRemnantCut(Window, Page, remnantCut);

                        graphicsCut.GraphicsRemnantCutList.Add(graphicsRemnantCut);

                        graphicsRemnantCut.Draw(areaFinishLayers.EmbdCutsLayer.GetBaseLayer(), Color.DarkBlue, Color.White, 2);

                        VisioInterop.BringToFront(graphicsRemnantCut.Shape);
                        areaFinishLayers.EmbdCutsLayer.AddShapeToLayer(graphicsRemnantCut.Shape, 1);

                        areaFinishLayers.EmbdCutsLayer.GetBaseLayer().BringShapesToFront();
                    }

                    Window.DeselectAll();

                }
            }

            foreach (GraphicsRollout graphicsRollout in GraphicsRolloutList)
            {
                foreach (Overage overage in graphicsRollout.Rollout.EmbeddedOverageList)
                {
                    GraphicsOverage graphicsOverage = new GraphicsOverage(Window, Page, null, overage);

                    EmbeddedOversList.Add(graphicsOverage);
   
                    graphicsOverage.DrawBoundingRectangleEmbd(areaFinishLayers.EmbdOverLayer.GetBaseLayer(), Color.Gold, Color.FromArgb(0), 2);

                    areaFinishLayers.EmbdOverLayer.AddShapeToLayer(graphicsOverage.Shape, 1);
                }

                Window.DeselectAll();
            }
        }

        public void UpdateOverIndicesAndVisibility(ref int overIndx)
        {
            // We tally the graphics overage indices first so we can number them left to right, top to bottom.

            List<Tuple<GraphicsOverageIndex, Overage>> graphicsOverageIndexList = new List<Tuple<GraphicsOverageIndex, Overage>>();

            foreach (GraphicsOverage graphicsOverage in this.GraphicsOverageList)
            {
                if (graphicsOverage.Deleted)
                {
                    continue;
                }

                GraphicsOverageIndex graphicsOverageIndex = graphicsOverage.GraphicsOverageIndex;

                if (Utilities.IsNotNull(graphicsOverageIndex))
                {
                    graphicsOverageIndexList.Add(new Tuple<GraphicsOverageIndex, Overage>(graphicsOverageIndex, graphicsOverage.Overage));
                }
            }

            graphicsOverageIndexList.Sort((i1, i2) => Coordinate.Compare(i1.Item1.Location, i2.Item1.Location));

            // Now we number them


            foreach (Tuple<GraphicsOverageIndex, Overage> listElem in graphicsOverageIndexList)
            {
                listElem.Item2.OverageIndex = overIndx;
                listElem.Item1.OverageIndex = overIndx++;
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

        public void UpdateUndrIndicesAndVisibility(ref int undrIndx)
        {
            // We tally the graphics undrage indices first so we can number them left to right, top to bottom.

            List<Tuple<GraphicsUndrageIndex, Undrage>> graphicsUndrageIndexList = new List<Tuple<GraphicsUndrageIndex, Undrage>>();

            foreach (GraphicsUndrage graphicsUndrage in this.GraphicsUndrageList)
            {
                if (graphicsUndrage.Deleted)
                {
                    continue;
                }

                GraphicsUndrageIndex graphicsUndrageIndex = graphicsUndrage.GraphicsUndrageIndex;

                if (Utilities.IsNotNull(graphicsUndrageIndex))
                {
                    graphicsUndrageIndexList.Add(new Tuple<GraphicsUndrageIndex, Undrage>(graphicsUndrageIndex, graphicsUndrage.Undrage));
                }
            }

            graphicsUndrageIndexList.Sort((i1, i2) => Coordinate.Compare(i1.Item1.Location, i2.Item1.Location));

            // Now we number them

            foreach (Tuple<GraphicsUndrageIndex, Undrage> listElem in graphicsUndrageIndexList)
            {
                listElem.Item2.UndrageIndex = undrIndx;
                listElem.Item1.UndrageIndex = undrIndx++;
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

        public void UpdateCutIndicesAndVisibility(ref int cutIndex)
        {
            List<Tuple<GraphicsCutIndex, Cut>> graphicsCutIndexList = new List<Tuple<GraphicsCutIndex, Cut>>();

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
                    graphicsCutIndexList.Add(new Tuple<GraphicsCutIndex, Cut>(graphicsCutIndex, graphicsCut.Cut));
                }
            }

            graphicsCutIndexList.Sort((i1, i2) => Coordinate.Compare(i1.Item1.Location, i2.Item1.Location));

            // Now we number them

            foreach (Tuple<GraphicsCutIndex, Cut> listElem in graphicsCutIndexList)
            {
                if (listElem.Item2.Deleted)
                {
                    listElem.Item1.SetVisibility(false);

                    continue;
                }

                listElem.Item2.CutIndex = cutIndex;
                listElem.Item1.CutIndex = cutIndex++;

                listElem.Item1.SetVisibility(true);
            }
        }


        internal void UpdateCutOverAndUnderIndices(ref int cutIndex, ref int overIndx, ref int undrIndx)
        {
            UpdateCutIndicesAndVisibility(ref cutIndex);

            UpdateOverIndicesAndVisibility(ref overIndx);

            UpdateUndrIndicesAndVisibility(ref undrIndx);

            // We tally the graphics overage indices first so we can number them left to right, top to bottom.

            List<Tuple<GraphicsOverageIndex, Overage>> graphicsOverageIndexList = new List<Tuple<GraphicsOverageIndex, Overage>>();

            foreach (GraphicsOverage graphicsOverage in this.GraphicsOverageList)
            {
                if (graphicsOverage.Deleted)
                {
                    continue;
                }

                GraphicsOverageIndex graphicsOverageIndex = graphicsOverage.GraphicsOverageIndex;

                if (Utilities.IsNotNull(graphicsOverageIndex))
                {
                    graphicsOverageIndexList.Add(new Tuple<GraphicsOverageIndex, Overage>(graphicsOverageIndex, graphicsOverage.Overage));
                }
            }

            graphicsOverageIndexList.Sort((i1, i2) => Coordinate.Compare(i1.Item1.Location, i2.Item1.Location));

            // Now we number them
            foreach (Tuple<GraphicsOverageIndex, Overage> listElem in graphicsOverageIndexList)
            {
                int overageIndex = listElem.Item2.OverageIndex;

                if (Overage.OverageIndexSet.Contains(overageIndex))
                {
                    Overage.OverageIndexSet.Remove(overageIndex);
                    
                }
            }

            foreach (Tuple<GraphicsOverageIndex, Overage> listElem in graphicsOverageIndexList)
            {
                int overageIndex = Overage.OverageIndexGenerator();
                listElem.Item2.OverageIndex = overageIndex;

                GraphicsOverageIndex graphicsOverageIndex = listElem.Item1;
                graphicsOverageIndex.OverageIndex = overageIndex;
                graphicsOverageIndex.DrawOverageIndexText();
            }
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

            //GraphicsWindow graphicsWindow = new GraphicsWindow(this.canvasManager.VsoWindow);

            double rollWidthInLocalUnits = 0;

            double materialWidthInLocalUnits = 0;

            double materialOverlapInLocalUnits = 0;

            rollWidthInLocalUnits = (canvasManager.remnantSeamWidthInFeet - AreaFinishBase.OverlapInInches) * 12.0 / Page.DrawingScaleInInches;

            materialWidthInLocalUnits = canvasManager.remnantSeamWidthInFeet * 12.0 / Page.DrawingScaleInInches;

            // This should clear the corresponding seam list for all inhereted members

            SeamList.Clear();

            base.GenerateSeamsAndRollouts(line, 0, rollWidthInLocalUnits, materialWidthInLocalUnits, materialOverlapInLocalUnits, Page.DrawingScaleInInches);

            foreach (GraphicsRollout graphicsRollout in GraphicsRolloutList)
            {
                foreach (GraphicsCut graphicsCut in graphicsRollout.GraphicsCutList)
                {

                    graphicsCut.GraphicsRemnantCutList.Clear();

                    foreach (RemnantCut remnantCut in graphicsCut.Cut.RemnantCutList)
                    {
                        GraphicsRemnantCut graphicsRemnantCut = new GraphicsRemnantCut(Window, Page, remnantCut);

                        graphicsCut.GraphicsRemnantCutList.Add(graphicsRemnantCut);

                        graphicsRemnantCut.Draw(areaFinishLayers.EmbdCutsLayer.GetBaseLayer(), Color.DarkBlue, Color.White, 2);

                        areaFinishLayers.EmbdCutsLayer.AddShapeToLayer(graphicsRemnantCut.Shape, 1);

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
            CanvasSeamList.ForEach(s=>((CanvasSeam)s).UpdateSeamGraphics(seamFinishBase, selected));
        }

        /// <summary>
        /// Draws all the seams for the current layout area
        /// </summary>
        public void DrawSeams(bool includeManualSeams = true)
        {
            if (CanvasSeamList is null)
            {
                return;
            }

            int visioDashType = 1;
            double lineWidthInPts = 3;
            Color seamColor = Color.Red;

            SeamFinishBase seamBase = CanvasManagerGlobals.SelectedAreaFinishManager.FinishSeamBase;

            if (seamBase != null)
            {
                visioDashType = seamBase.VisioDashType;
                lineWidthInPts = seamBase.SeamWidthInPts;
                seamColor = seamBase.SeamColor;
            }

            foreach (CanvasSeam canvasSeam in CanvasSeamList)
            {
                if (!includeManualSeams)
                {
                    if (canvasSeam.SeamType == SeamType.Manual)
                    {
                        continue;
                    }
                }

                canvasSeam.Draw(seamColor, lineWidthInPts, visioDashType);

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
                    Shape cutShape = graphicsCut.DrawBoundingRectangleAndIndex(areaFinishLayers.CutsIndexLayer.GetBaseLayer(), Color.Red, Color.White, 1);

                    graphicsCut.Shape = cutShape;

                    cutShape.SetFillOpacity(0);

                    cutShape.SetLineWidth(1);

                    cutShape.BringToFront();

                    VisioInterop.SetShapeData(cutShape, "Cut Bounding Polygon", "Polygon", cutShape.Guid);

                    Page.AddToPageShapeDict(graphicsCut);

                    areaFinishLayers.CutsLayer.AddShapeToLayer(graphicsCut.Shape, 1);

                    VisioInterop.LockShape(graphicsCut.Shape);

                    //CutsIndexLayer.AddShape(graphicsCut.GraphicsCutIndex.Shape, 1); // Bernie, check here

                    VisioInterop.LockShape(graphicsCut.GraphicsCutIndex.Shape);

                    if (Utilities.IsNotNull(graphicsCut.GraphicsOverageList))
                    {
                        foreach (GraphicsOverage overage in graphicsCut.GraphicsOverageList)
                        {
                            Shape overageShape = overage.DrawBoundingRectangleNormal(areaFinishLayers.OversLayer.GetBaseLayer(), Color.Green, Color.White, 1);

                            overage.Shape = overageShape;

                            overage.Shape.SetShapeData("[Overage]", "Composite Shape", overage.Guid);

                            Page.AddToPageShapeDict(overage);

                            VisioInterop.SetFillOpacity(overageShape, 0);

                            overageShape.BringToFront();

                            areaFinishLayers.OversLayer.AddShapeToLayer(overageShape, 1);

                            VisioInterop.LockShape(overageShape);
                        }
                    }
                }
            }

            if (Utilities.IsNotNull(base.GraphicsUndrageList))
            {
                foreach (GraphicsUndrage underage in base.GraphicsUndrageList)
                {
                    Shape underageShape = underage.DrawBoundingRectangle(areaFinishLayers.UndrsLayer.GetBaseLayer(), Color.Orange, Color.White, 1);

                    underageShape.SetShapeData("Underage Bounding Polygon", "Polygon");

                    underage.Shape = underageShape;

                    underage.Shape.SetShapeData("[Underage]", "Composite Shape", underage.Guid);

                    Page.AddToPageShapeDict(underageShape);

                    VisioInterop.SetFillOpacity(underageShape, 0);

                    underageShape.BringToFront();

                    areaFinishLayers.UndrsLayer.AddShapeToLayer(underageShape, 1);

                    VisioInterop.LockShape(underageShape);
                }
            }
        }

        internal void RemoveSeamsAndRollouts()
        {
            DeleteNonManualSeams();

            DeleteRollouts();

            
            this.AreaFinishManager.UpdateCutIndices();

            canvasManager.UpdateAreaAndSeamsStats();
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

            double opacity;

            color = AreaFinishManager.Color;

            opacity = AreaFinishManager.Opacity;

            SetFillColor(color);

            SetFillOpacity(opacity);

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


            SetFillColor(color);

            SetFillOpacity(opacity);

        }

        internal void SetCompletedLineWidth()
        {
            ExternalArea.SetLineGraphics(canvasManager.DesignState, false, AreaShapeBuildStatus.Completed);
        }

        internal void setupBorderAreaOverageOrUndrage()
        {
            double drawingScaleInInches = CanvasManagerGlobals.CanvasPage.DrawingScaleInInches;

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

                this.BorderAreaUndrage = new Undrage(widthInInches, lngthInInches, rollWidthInInches, materialOverlapInInches, angle);

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
                double length = canvasDirectedLine.Length * Page.DrawingScaleInInches;

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

                this.ParentAreaGuid = this.ParentArea.Guid;

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


                    CanvasManagerGlobals.CanvasPage.AddToDirectedLineDict(line);

                    CanvasManagerGlobals.CanvasPage.AddToPageShapeDict(line);
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

                        CanvasManagerGlobals.CanvasPage.AddToDirectedLineDict(line);
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

        //public UCAreaFinishPaletteElement UCAreaFinish
        //{
        //    get;set;
        //}

        //public AreaFinishBase AreaFinishBase => UCAreaFinish.AreaFinishBase;

        //public AreaFinishManager AreaFinishManager => UCAreaFinish.AreaFinishManager;
        
        public AreaFinishManager AreaFinishManager { get; set; }

        // Kludge, for edit operations.

        public AreaFinishManager PrevAreaFinishManager { get; set; }

        public Shape CompositeShape;

        public new Shape Shape
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

                    //if (value.VisioShape is null)
                    //{
                    //    return;
                    //}

                    //if (string.IsNullOrEmpty(value.VisioShape.Data1))
                    //{
                    //    base.Shape.Data1 = "LayoutArea";

                    //    ExternalArea.Shape.Data1 = "LayoutArea";
                    //}

                }

                catch (Exception ex)
                {

                }
            }
        }

        public string NameID => ExternalArea.NameID;

        public bool AreaDesignStateEditModeSelected { get; set; }

        public bool AreaDesignStateSelectedForCopy { get; set; }

        public bool SeamDesignStateSelectionModeSelected { get; set; }

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
                _seamIndexTag = value;

                if (value is null)
                {
                    SeamAreaIndex = 0;
                }

                else
                {
                    SeamAreaIndex = value.SeamAreaIndex;
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

        internal void AddSeamIndexTag(double x, double y)
        {
            SeamIndexTag = new CanvasSeamTag(Window, Page, x, y, SeamAreaIndex, LayoutAreaType);
            

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
            GraphicsWindow window
            , GraphicsPage page
            , string guid
            , LayoutAreaType layoutAreaType
            , string parentAreaGuid
            , List<string> offspringAreaGuidList
            , CanvasDirectedPolygon externalPerimeter
            , List<CanvasDirectedPolygon> internalPerimeters
            , List<CanvasSeam> canvasSeamList
            , List<GraphicsRollout> rolloutList
            , UCAreaFinishPaletteElement ucAreaPaletteElement
            , AreaFinishManager areaFinishManager
            , AreaFinishManager prevAreaFinishManger
            , bool isVisible
            , double fixedWidthWidth
            , DesignState originatingDesignState
            , int prevSeamAreaIndex
            , bool areaDesignStateEditModeSelected
            , bool seamDesignStateSelectionModeSelected
            , bool seamDesignStateSubdivisionModeSelected
            , CanvasSeamTag canvasSeamTag
            , bool isZeroAreaLayoutArea
            ): base(window, page)
        {
            this.Page = page;

            this.Window = window;

            this.AreaFinishManager = areaFinishManager;

            Debug.Assert(this.AreaFinishManager != null);

            #region Validations

            if (!VisioValidations.ValidateWindowParm(Window, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid window parameter", 1, true);
            }

            if (!VisioValidations.ValidatePageParm(Page, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid page parameter", 1, true);
            }

            #endregion

            this.Guid = guid;

            this.LayoutAreaType = layoutAreaType;

            this.ExternalArea = externalPerimeter;

            this.InternalAreas = internalPerimeters;


            //InitializeSeamLayers(Window, Page);

            this.IsZeroAreaLayoutArea = isZeroAreaLayoutArea;

            this.ParentAreaGuid = parentAreaGuid;

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
      
            // Re-establish the rollouts and sub-elements in the system definitions.

            foreach (GraphicsRollout graphicsRollout in rolloutList)
            {
                RolloutList.AddBase(graphicsRollout.Rollout);
                GraphicsRolloutList.AddBase(graphicsRollout);

                // Kludge to fix problem with unknown source

                graphicsRollout.GraphicsLayoutArea = this;
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

            setupSeamListCoordination();

        }

        // Constructor for overs generator layout area

        public CanvasLayoutArea(
           GraphicsWindow window
           , GraphicsPage page
           , AreaFinishManager areaFinishManager
           //, UCAreaFinishPaletteElement ucAreaFinish
           , CanvasDirectedPolygon externalPerimeter
           , List<CanvasDirectedPolygon> internalPerimeterList
            , double oversGeneratorOversWidthInInches
           , DesignState originatingDesignState = DesignState.Area
           , LayoutAreaType layoutAreaType = LayoutAreaType.OversGenerator) :
            this
                (
                    window, page
                    ,areaFinishManager
                    ,externalPerimeter
                    ,internalPerimeterList
                    ,originatingDesignState = DesignState.Area
                    ,layoutAreaType = LayoutAreaType.OversGenerator
                )
          
        {
            this.Window = window;

            this.Page = page;

            #region Validations

            if (!VisioValidations.ValidateWindowParm(Window, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid window parameter", 1, true);
            }

            if (!VisioValidations.ValidatePageParm(Page, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid page parameter", 1, true);
            }

            #endregion

            this.AreaFinishManager = areaFinishManager;

            Debug.Assert(this.AreaFinishManager != null);

            OversGeneratorOversWidthInInches = oversGeneratorOversWidthInInches;
        }

        // Constructor for normal, color only and fixed width layout area

        public CanvasLayoutArea(
            GraphicsWindow window
            , GraphicsPage page
            , AreaFinishManager areaFinishManager
            ,CanvasDirectedPolygon externalPerimeter
            ,List<CanvasDirectedPolygon> internalPerimeterList
            ,DesignState originatingDesignState = DesignState.Area
            ,LayoutAreaType layoutAreaType = LayoutAreaType.Normal)
            : 
            base(
                window
                ,page
                , (GraphicsDirectedPolygon)externalPerimeter
                , internalPerimeterList.Select(i => (GraphicsDirectedPolygon)i).ToList())
        {
            this.Window = window;

            this.Page = page;

            this.AreaFinishManager = areaFinishManager;

            Debug.Assert(this.AreaFinishManager != null);

            #region Validations

            if (!VisioValidations.ValidateWindowParm(Window, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid window parameter", 1, true);
            }

            if (!VisioValidations.ValidatePageParm(Page, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid page parameter", 1, true);
            }

            #endregion

            //this.UCAreaFinish = ucAreaFinish;

            this.AreaFinishManager = areaFinishManager;

            this.LayoutAreaType = layoutAreaType;

            ExternalArea = externalPerimeter;
            InternalAreas = internalPerimeterList;

            //-----------------------------------------------------------------------------//
            // Initially, the 'shape' of the canvas layout area is the external perimeter. //
            // This may change if the shape becomes a 'composite shape'.                   //
            // ----------------------------------------------------------------------------//

            CompositeShape = externalArea.Shape;

            ExternalArea.ParentLayoutArea = this;

            InternalAreas.ForEach(p => p.ParentLayoutArea = this);

            OriginatingDesignState = originatingDesignState;

            Guid = GuidMaintenance.CreateGuid(this);

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                this.BorderWidthInInches = canvasManager.BaseForm.FixedWidthScaleInInches();

                this.setupBorderAreaOverageOrUndrage();
            }

            setupSeamListCoordination();
        }

        public CanvasLayoutArea(
            GraphicsWindow window
            , GraphicsPage page
            , AreaFinishManager areaFinishManager
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
                ,(GraphicsDirectedPolygon) externalPerimeter
                , internalPerimeterList.Select(i => (GraphicsDirectedPolygon) i).ToList())
        {

            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid window parameter", 1, true);
            }

            if (!VisioValidations.ValidatePageParm(page, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid page parameter", 1, true);
            }

            #endregion

            this.AreaFinishManager = areaFinishManager;

            Debug.Assert(this.AreaFinishManager != null);
            

            this.Window = window;

            this.Page = page;

            this.IsZeroAreaLayoutArea = IsZeroAreaLayoutArea;

            this.externalArea = externalPerimeter;

            this.internalAreas = internalPerimeterList;

            ExternalArea.ParentLayoutArea = this;

            InternalAreas.ForEach(p => p.ParentLayoutArea = this);

            OriginatingDesignState = originatingDesignState;

            Guid = GuidMaintenance.CreateGuid(this);

            this.LayoutAreaType = layoutAreaType;

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                this.BorderWidthInInches = canvasManager.BaseForm.FixedWidthScaleInInches();

                this.setupBorderAreaOverageOrUndrage();
            }

            setupSeamListCoordination();

        }

        public CanvasLayoutArea(
            GraphicsWindow window
            , GraphicsPage page
            , LayoutArea layoutArea
            , AreaFinishManager areaFinishManager
            , UCAreaFinishPaletteElement ucAreaFinish
            , DesignState originatingDesignState = DesignState.Area
            , bool isZeroLayoutArea = false
            , LayoutAreaType layoutAreaType = LayoutAreaType.Normal):
            base(
                window
                , page)
        {
            
            this.Window = window;

            this.Page = page;

            this.AreaFinishManager = areaFinishManager;

            Debug.Assert(this.AreaFinishManager != null);

            #region Validations

            if (!VisioValidations.ValidateWindowParm(Window, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid window parameter", 1, true);
            }

            if (!VisioValidations.ValidatePageParm(Page, "CanvasLayoutArea:Constructor"))
            {
                Tracer.TraceGen.TraceError("CanvasLayoutArea:Constructor called with invalid page parameter", 1, true);
            }

            #endregion

            this.IsZeroAreaLayoutArea = IsZeroAreaLayoutArea;

            this.LayoutAreaType = layoutAreaType;

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                this.BorderWidthInInches = canvasManager.BaseForm.FixedWidthScaleInInches();

                this.setupBorderAreaOverageOrUndrage();
            }

            this.ExternalArea = new CanvasDirectedPolygon(canvasManager, layoutArea.ExternalArea, originatingDesignState);
            
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

            setupSeamListCoordination();

            this.LayoutAreaType = layoutAreaType;

            if (this.LayoutAreaType == LayoutAreaType.FixedWidth)
            {
                this.BorderWidthInInches = canvasManager.BaseForm.FixedWidthScaleInInches();

                this.setupBorderAreaOverageOrUndrage();
            }

            AreaFinishManager.SetupAllSeamLayers();
        }

        private void setupSeamListCoordination()
        {
            base.GraphicsSeamList.ItemAdded += GraphicsSeamList_ItemAdded;
            base.GraphicsSeamList.ItemRemoved += GraphicsSeamList_ItemRemoved;
            base.GraphicsSeamList.ListCleared += GraphicsSeamList_ListCleared;
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
            canvasDirectedPolygon.ParentLayoutArea = this;

            this.InternalAreasAdd(canvasDirectedPolygon);

            Shape internalShape = canvasDirectedPolygon.CreateInternalAreaShape();

            RemoveInternalAreas(new List<Shape> { internalShape });

            foreach (CanvasDirectedLine internalPerimeterLine in canvasDirectedPolygon)
            {
                internalPerimeterLine.LineRole = LineRole.InternalPerimeter;
                internalPerimeterLine.SetShapeData();
            }


            Window?.DeselectAll();

            return canvasDirectedPolygon;
        }

        public void CreateTakeoutArea()
        {
            if (this.InternalAreas.Count <= 0)
            {
                return;
            }

            List<Shape> internalAreaShapes = InternalAreas.Select(ia => ia.Shape).ToList();

            RemoveInternalAreas(internalAreaShapes);

        }

        public void AddBackInternalAreas(List<Shape> internalAreaShapes)
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

            CanvasManagerGlobals.CanvasPage.RemoveLayoutArea(Guid);

            foreach (Shape internalShape in internalAreaShapes)
            {
                VisioInterop.SetBaseFillColor(internalShape, AreaFinishManager.UCAreaPaletteElement.BackColor);
            }

            Shape = VisioInterop.Union(Window, Page, Shape, internalAreaShapes);

            
            Guid = Shape.Guid;

            VisioInterop.SetShapeData(Shape, "Layout Area", "Compound Shape[" + this.AreaFinishManager.AreaName + "]", Guid);

            this.ExternalArea.Guid = Guid;

           
            if (AreaFinishManager.CanvasLayoutAreaDict.ContainsKey(prevGuid))
            {
                AreaFinishManager.RemoveLayoutArea(prevGuid);
                AreaFinishManager.AddLayoutArea(this);
            }

            CanvasManagerGlobals.CanvasPage.AddLayoutArea(this);

            areaFinishLayers.AreaDesignStateLayer.AddShapeToLayer(Shape, 1);
        }

        public void RemoveInternalAreas(List<Shape> internalAreaShapes)
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

            CanvasManagerGlobals.CanvasPage.RemoveLayoutArea(prevGuid);

            Page.RemoveFromPageShapeDict(prevGuid);


            //result = DebugSupportRoutines.ExternalAreaGuidCheck(this);

            Shape = VisioGeometryEngine.Subtract(Window, Page, Shape, internalAreaShapes);

            result = DebugSupportRoutines.ExternalAreaGuidCheck(this);

            result = DebugSupportRoutines.ExternalAreaGuidCheck(this);

            VisioInterop.SetShapeData(Shape, "Layout Area Shape", "Compound Shape[" + this.AreaFinishManager.AreaName + "]", Shape.Guid);

            //this.ExternalArea.Guid = Guid;

            this.Shape = Shape;

            this.AreaFinishManager.AddLayoutArea(this);

            CanvasManagerGlobals.CanvasPage.AddLayoutArea(this);

            areaFinishLayers.AreaDesignStateLayer.AddShapeToLayer(this, 1);
            areaFinishLayers.SeamDesignStateLayer.AddShapeToLayer(this, 1);
        }

        public double PerimeterLength()
        {
            if (LayoutAreaType == LayoutAreaType.ZeroArea || LayoutAreaType == LayoutAreaType.OversGenerator)
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

        public void SetFillColor(Color color)
        {
            VisioInterop.SetBaseFillColor(this.Shape, color);
        }

        public void SetFillColor(string visioFillColorFormula)
        {
            VisioInterop.SetBaseFillColor(this.Shape, visioFillColorFormula);
        }

        public void SetFillTransparancy(string visioFillTransparencyFormula)
        {
            VisioInterop.SetFillTransparency(this.Shape, visioFillTransparencyFormula);
        }

        public void SetFillOpacity(double opacity)
        {
            VisioInterop.SetFillOpacity(this.Shape, opacity);
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
            if (LayoutAreaType == LayoutAreaType.OversGenerator || LayoutAreaType == LayoutAreaType.ZeroArea)
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

                    CanvasManagerGlobals.CanvasPage.AddToDirectedLineDict(directedLine);
                }

                PolygonInternalArea.VisioShape.Data3 = Guid;
            }

            CanvasManagerGlobals.CanvasPage.AddLayoutArea(this);
            
            List<Shape> internalAreaShapes = new List<Shape>();

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

                    CanvasManagerGlobals.CanvasPage.AddToDirectedLineDict(directedLine);
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

                //VisioInterop.AddShapeToLayer(this.Shape, graphicsLayer);

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
                    graphicsLayer.RemoveShape(this, 1);
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
                    //    CanvasManagerGlobals.CanvasPage.RemoveDirectedLine(directedLine);
                    //}
                }

                foreach (CanvasDirectedPolygon internalAreaShape in InternalAreas)
                {
                    internalAreaShape.Delete(deleteAssociatedLines);

                    foreach (CanvasDirectedLine directedLine in internalAreaShape)
                    {
                        CanvasManagerGlobals.CanvasPage.RemoveFromDirectedLineDict(directedLine);
                    }
                }

                foreach (CanvasSeam canvasSeam in CanvasSeamList)
                {
                    canvasSeam.GraphicsSeam.Undraw();
                }

                if (!(SeamIndexTag is null))
                {
                    SeamIndexTag.Delete();
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("CanvasLayoutArea:RemoveFromCanvas throws an exception", ex, 1, true);
            }
        }

        // Removes the canvas layout area and all elements from the supporting system.
        //public void AddToSystem()
        //{
        //    CanvasPage currentPage = CanvasManagerGlobals.CanvasPage;

        //    if (!(this.UCAreaFinish is null))
        //    {
        //        this.UCAreaFinish.RemoveLayoutArea(this);
        //    }

        //    if (!(ExternalArea is null))
        //    {
        //        ExternalArea.RemoveFromSystem();
        //    }

        //    foreach (CanvasDirectedPolygon internalArea in InternalAreas)
        //    {
        //        internalArea.RemoveFromSystem();
        //    }

        //    //foreach (GraphicsSeam seam in CanvasSeamList)
        //    //{

        //    //}

        //    //if (!(SeamIndexTag is null))
        //    //{

        //    //}

        //}

        //// Removes the canvas layout area and all elements from the supporting system.
        //public void RemoveFromSystem()
        //{
        //    CanvasPage currentPage = CanvasManagerGlobals.CanvasPage;

        //    if (!(this.UCAreaFinish is null))
        //    {
        //        this.UCAreaFinish.RemoveLayoutArea(this);
        //    }

        //    if (!(ExternalArea is null))
        //    {
        //        ExternalArea.RemoveFromSystem();
        //    }

        //    foreach (CanvasDirectedPolygon internalArea in InternalAreas)
        //    {
        //        internalArea.RemoveFromSystem();
        //    }

        //    Page.RemoveFromPageShapeDict(this); // Remove from page shape dictionary
        //}


        internal void Delete(bool deleteAssociatedLines = false)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });
#endif

            Debug.Assert(canvasManager != null);

            //--------------------------------------------------------------------//
            // Unsubscribe from events to avoid memory leaks                      //
            //--------------------------------------------------------------------//

            base.GraphicsSeamList.ItemAdded -= GraphicsSeamList_ItemAdded;
            base.GraphicsSeamList.ItemRemoved -= GraphicsSeamList_ItemRemoved;
            base.GraphicsSeamList.ListCleared -= GraphicsSeamList_ListCleared;

            GraphicsDebugSupportRoutines.CheckForNullShapeInGraphicsLayers(Page, 1);

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

            this.CanvasManagerGlobals.CanvasPage.RemoveLayoutArea(this);

            if (Utilities.IsNotNull(CopyMarker))
            {
                AreaFinishManager.AreaDesignStateLayer.RemoveShapeFromLayer(CopyMarker, 1);

                Page.RemoveFromPageShapeDict(CopyMarker);

                CopyMarker.Delete();

                CopyMarker = null;
            }

            if (Utilities.IsNotNull(VertexEditMarker))
            {
                VertexEditMarker.Delete();

                VertexEditMarker = null;
            }

            areaFinishLayers.DeleteSeamLayers();

            Page.RemoveFromPageShapeDict(this.Guid);
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
               // , this.AreaFinishManager.UCAreaPaletteElement
                , AreaFinishManager
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

            return canvasManager.linePalette[maxGuid];
        }

        public bool IsVisible {
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

        public Shape PolygonInternalArea
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

        internal Shape Draw(GraphicsWindow window, GraphicsPage page)
        {
            //ExternalArea.Draw(page, AreaFinishBase.Color);

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
                CanvasManagerGlobals.CanvasPage.AddToDirectedLineDict(canvasDirectedLine);
            }

            foreach (CanvasDirectedPolygon internalArea in clonedLayoutArea.InternalAreas)
            {
                internalArea.ParentLayoutArea = clonedLayoutArea;

                foreach (CanvasDirectedLine canvasDirectedLine in internalArea)
                {
                    CanvasManagerGlobals.CanvasPage.AddToDirectedLineDict(canvasDirectedLine);
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
                CanvasManagerGlobals.CanvasPage.AddToDirectedLineDict(canvasDirectedLine);
            }

            foreach (CanvasDirectedPolygon internalArea in clonedLayoutArea.InternalAreas)
            {
                internalArea.ParentLayoutArea = clonedLayoutArea;

                foreach (CanvasDirectedLine canvasDirectedLine in internalArea)
                {
                    CanvasManagerGlobals.CanvasPage.AddToDirectedLineDict(canvasDirectedLine);
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
    }
}
