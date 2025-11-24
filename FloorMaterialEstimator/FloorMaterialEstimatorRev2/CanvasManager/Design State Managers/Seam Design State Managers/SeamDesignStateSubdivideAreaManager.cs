
using FinishesLib;

namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Geometry;
    using Graphics;
    using Utilities;
    using Globals;
    using TracerLib;
    using MaterialsLayout;

    using FloorMaterialEstimator.Finish_Controls;

    using Visio = Microsoft.Office.Interop.Visio;
    using FloorMaterialEstimator.Supporting_Forms;
    using SettingsLib;
    using MaterialsLayout.Subdivision;
    using System.Drawing;

    public partial class CanvasManager
    {
        public Dictionary<string, CanvasLayoutArea> layoutAreaForSubdivisionDict = new Dictionary<string, CanvasLayoutArea>();

        public void ProcessSeamDesignStateSubdivideAreaClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            cancelDefault = true;

            if (button == 1)
            {
                processSeamDesignStateSubdivideAreaLeftClick(x, y);

                return;
            }

            if (button == 2)
            {
                processSeamDesignStateSubdivideAreaRghtClick(x, y);
                
                return;
            }
        }

        private void processSeamDesignStateSubdivideAreaLeftClick(double x, double y)
        {
            if (!KeyboardUtils.ShiftKeyPressed)
            {
                SelectAreaForSubdivision(x, y);
            }

            else
            {
                SelectAreaForMerging(x, y);
            }
        }

        private void processSeamDesignStateSubdivideAreaRghtClick(double x, double y)
        {
            ContinueSubdivisionDraw(x, y);
        }

        public CanvasLayoutArea LayoutAreaForSubdivision = null; // TODO: Move this to the graphics Page.


        public void SelectAreaForSubdivision(double x, double y)
        {
            List<CanvasLayoutArea> selectedAreaList = CurrentPage.GetSelectedAreaShapeList(x, y, true);

            if (selectedAreaList is null)
            {
                return;
            }

            if (selectedAreaList.Count <= 0)
            {
                return;
            }

            CanvasLayoutArea minLayoutArea = null;
            double minAreaArea = double.MaxValue;

            foreach (CanvasLayoutArea layoutArea in selectedAreaList)
            {
                if (layoutArea.IsSubdivided())
                {
                    continue;
                }

                double nxtAreaArea = layoutArea.NetAreaInSqrInches();
                if (nxtAreaArea < minAreaArea)
                {
                    minAreaArea = nxtAreaArea;
                    minLayoutArea = layoutArea;
                }
            }

            LayoutAreaForSubdivision = minLayoutArea;

            if (LayoutAreaForSubdivision.SeamDesignStateSubdivisionModeSelected)
            {
                LayoutAreaForSubdivision.DeselectForSubdivision();

                LayoutAreaForSubdivision = null;
            }

            else
            {
                if (LayoutAreaForSubdivision.IsSeamed() || LayoutAreaForSubdivision.SeamDesignStateSelectionModeSelected)
                {
                    DialogResult dr = MessageBox.Show(
                        "This area is either seamed or selected for seaming in area selection mode. Continuing will reset this area in area selection mode and select it for subdivision. Do you want to continue?"
                        , "Select for Subdivision"
                        , MessageBoxButtons.YesNo);

                    if (dr == DialogResult.No)
                    {
                        return;
                    }

                    CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(LayoutAreaForSubdivision, FinishGlobals.SelectedAreaFinish.Guid);
                }

                foreach (CanvasLayoutArea layoutArea in CurrentPage.LayoutAreas)
                {
                    if (layoutArea.SeamDesignStateSubdivisionModeSelected)
                    {
                        layoutArea.DeselectForSubdivision();
                    }
                }

                LayoutAreaForSubdivision.SelectForSubdivision();
            }
        }

        public void ContinueSubdivisionDraw(double x, double y)
        {
            //layoutAreaForSubdivisionDict.TryGetValue(layoutAreaForSubdivision.Guid, out layoutAreaForSubdivision);

            if (LayoutAreaForSubdivision == null)
            {
                ManagedMessageBox.Show("Please select an area for subdivision");
                return;
            }

            //DrawingMode = DrawingMode.Polyline;

            bool pointOnBoundary = false;

            Coordinate testCoord = new Coordinate(x, y);

            Coordinate coord = ((GraphicsLayoutArea)LayoutAreaForSubdivision).GetNearestPointToBoundary(testCoord);

            if (!Coordinate.IsNullCoordinate(coord))
            {
                if (Coordinate.H2Distance(testCoord, coord) <= 0.25)
                {
                    x = coord.X;
                    y = coord.Y;

                    pointOnBoundary = true;
                }

            }

            if (!SystemState.DrawingShape)
            {
                Debug.Assert(BuildingPolyline == null);

                if (!pointOnBoundary && LayoutAreaForSubdivision.Contains(new Coordinate(x,y)))
                {
                    MessageBox.Show("Subdivide by line requires that the first point be on or outside the region being subdivided");
                    return;
                }

                InitializePolylineDraw(x, y, KeyboardUtils.AltKeyPressed);

                SystemState.DrawingShape = true;
            }

            else
            {
                Debug.Assert((object)BuildingPolyline != null);

                ContinuePolylineDraw(x, y, KeyboardUtils.AltKeyPressed);
            }

        }

        List<CanvasLayoutArea> AreasSelectedForMergingList = new List<CanvasLayoutArea>();

        public void SelectAreaForMerging(double x, double y)
        {
            CanvasLayoutArea layoutAreaForMerging = CurrentPage.GetContainingLayoutArea(x, y);

            if (layoutAreaForMerging is null)
            {
                return;
            }

            if (layoutAreaForMerging.IsSubdivided())
            {
                ManagedMessageBox.Show("Cannot merge this area: it is further subdivided.");

                return;
            }

            CanvasLayoutArea parentLayoutArea = layoutAreaForMerging.ParentArea;

            if (parentLayoutArea is null)
            {
                ManagedMessageBox.Show("Cannot merge this area: it is a top level layout area.");

                return;
            }

            List<CanvasLayoutArea> siblingCanvasLayoutAreaList = new List<CanvasLayoutArea>();

            foreach (CanvasLayoutArea canvasLayoutArea in parentLayoutArea.OffspringAreas)
            {
                if (canvasLayoutArea.IsSubdivided())
                {
                    ManagedMessageBox.Show("Cannot merge this area: at least one sibling is further subdivided.");

                    return;
                }

                siblingCanvasLayoutAreaList.Add(canvasLayoutArea);
            }

            if (siblingCanvasLayoutAreaList.Count <= 1)
            {
                ManagedMessageBox.Show("This area does not have any siblings for merging");

                return;
            }

            foreach (CanvasLayoutArea canvasLayoutArea in siblingCanvasLayoutAreaList)
            {
                canvasLayoutArea.SetFillColor(Color.LightGray);
            }

            DialogResult dr = ManagedMessageBox.Show("OK to merge areas in gray?", "Confirm Merge", MessageBoxButtons.OKCancel);

            if (dr != DialogResult.OK)
            {
                return;
            }

            foreach (CanvasLayoutArea siblingLayoutArea in siblingCanvasLayoutAreaList)
            {
                // Cannot update finish stats in 'RemoveLayoutArea' until all siblings are removed.

                siblingLayoutArea.AreaFinishManager.SeamDesignStateLayer.RemoveShapeFromLayer(siblingLayoutArea, 1);

                siblingLayoutArea.AreaFinishManager.RemoveLayoutArea(siblingLayoutArea, false);
            }

            parentLayoutArea.MergeOffsprings();

            parentLayoutArea.AreaFinishManager.SeamDesignStateLayer.AddShape(parentLayoutArea, 1);

            parentLayoutArea.SetSeamDesignStateFormat(SeamMode.Subdivision);

            // Make the area and the associated lines visible.

            Page.InvisibleWorkspaceLayer.RemoveShapeFromLayer(parentLayoutArea, 1);

            foreach (CanvasDirectedLine externAreaLine in parentLayoutArea.ExternalArea)
            {
                Page.InvisibleWorkspaceLayer.RemoveShapeFromLayer(externAreaLine, 1);

                externAreaLine.LineFinishManager.SeamDesignStateLayer.AddShape(externAreaLine, 1);
            }

            foreach (CanvasDirectedPolygon internalArea in parentLayoutArea.InternalAreas)
            {
                foreach (CanvasDirectedLine internalAreaLine in internalArea)
                {
                    Page.InvisibleWorkspaceLayer.RemoveShapeFromLayer(internalAreaLine, 1);

                    internalAreaLine.LineFinishManager.SeamDesignStateLayer.AddShape(internalAreaLine, 1);
                }
            }

            parentLayoutArea.SetSeamDesignStateFormat(SeamMode.Selection);

            Window?.DeselectAll();

            return;


            //AddSeamCanvasLayoutAreaToCanvas(parentLayoutArea, parentLayoutArea.UCAreaFinish);

            //if (parentLayoutArea.ParentArea is null)
            //{
            //    parentLayoutArea.RemoveFromLayer(parentLayoutArea.AreaFinishManager.SeamDesignStateLayer);


            //}

            //parentLayoutArea.ExternalArea.SetLineGraphics(DesignState.Seam, false, AreaShapeBuildStatus.Completed);

            //--------------------------------------------------------------------------------------------------------------------//

            //-----------------------------------------------------------------------------//
            // The following adds back the external perimeter and internal perimeter lines //
            // to the line dictionary. It shouldn't be necessary, but until the code is    //
            // cleaned up... Note the defensive measures.                                  //
            //-----------------------------------------------------------------------------//

            //foreach (CanvasDirectedLine externalPerimeterLine in parentLayoutArea.ExternalArea.Perimeter)
            //{
            //    UCLineFinishPaletteElement ucLine = externalPerimeterLine.ucLine;
            //    GraphicsLayer layer = ucLine.SeamDesignStateLayer.GetBaseLayer();
            //    bool visibility = layer.Visibility;

            //   // externalPerimeterLine.Delete();
            //   // externalPerimeterLine.Draw(Color.Red, 0.25);
                
            //    layer.AddShape(externalPerimeterLine.Shape, 1);
            //    layer.AddToShapeDict(externalPerimeterLine.Shape);
            //    //-------------------------------------//
            //    // Defensive. Should not be necessary. //
            //    //-------------------------------------//

            //    if (CurrentPage.DirectedLineDictContains(externalPerimeterLine))
            //    {
            //        continue;
            //    }


            //    CurrentPage.AddToDirectedLineDict(externalPerimeterLine);
            //}

            //foreach (CanvasDirectedPolygon internalArea in parentLayoutArea.InternalAreas)
            //{
            //    foreach (CanvasDirectedLine internalPerimeterLine in internalArea.Perimeter)
            //    {
            //        //-------------------------------------//
            //        // Defensive. Should not be necessary. //
            //        //-------------------------------------//

            //        if (CurrentPage.DirectedLineDictContains(internalPerimeterLine))
            //        {
            //            continue;
            //        }

            //        CurrentPage.AddToDirectedLineDict(internalPerimeterLine);
            //    }
            //}

            ////--------------------------------------------------------------------------------------------------------------------//


            //parentLayoutArea.ExternalArea.Perimeter.ForEach(l => VisioInterop.SetBaseLineOpacity(l.Shape, 0));
            //parentLayoutArea.ExternalArea.Perimeter.ForEach(l => VisioInterop.SetBaseLineColor(l.Shape, Color.Blue));

            //parentLayoutArea.InternalAreas.ForEach(
            //    ia => ia.SetLineGraphics(DesignState.Seam, false, AreaShapeBuildStatus.Completed));

        }

        private void completeSeamDesignStateSubdivisionModePolylineDraw(CanvasDirectedPolyline polyline)
        {
            CanvasLayoutArea layoutAreaForSubdivision = layoutAreaForSubdivisionDict[FinishGlobals.SelectedAreaFinish.Guid];

            RemoveMarkerAndGuides();

           if (!processSubdivideByLine(polyline))
            {
                return;
            }

            polyline.Delete();

            SystemState.DrawingShape = false;

            buildingPolyline = null;

            SystemState.DrawingMode = DrawingMode.Default;

            setSubdividedAreasSelectionStatus(layoutAreaForSubdivision);

            layoutAreaForSubdivision.RemoveFromCanvas();

            layoutAreaForSubdivisionDict.Remove(FinishGlobals.SelectedAreaFinish.Guid);

            Window?.DeselectAll();
        }

        private void setSubdividedAreasSelectionStatus(CanvasLayoutArea layoutAreaForSubdivision)
        {
            layoutAreaForSubdivision.SeamDesignStateSelectionModeSelected = false;

            if (layoutAreaForSubdivision.SeamAreaIndex != 0)
            {
                layoutAreaForSubdivision.RemoveSeamIndexTag();

                foreach (CanvasLayoutArea canvasLayoutArea in layoutAreaForSubdivision.OffspringAreas)
                {
                    Coordinate location = canvasLayoutArea.Shape.CenterPoint;

                    CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(canvasLayoutArea, layoutAreaForSubdivision.AreaFinishManager.Guid);
                }

                CurrentPage.UpdateAreaDesignStateAreaSelectionStatus(layoutAreaForSubdivision);
            }
        }

        private bool processSubdivideByLine(CanvasDirectedPolyline polyline)
        {
            CanvasLayoutArea layoutAreaForSubdivision = layoutAreaForSubdivisionDict[FinishGlobals.SelectedAreaFinish.Guid];

            return processSubdivideByLine(layoutAreaForSubdivision, polyline);
        }

        /// <summary>
        /// Complete the currently building shape. Currently only applies to polyline.
        /// </summary>
        public void ProcessSubdivisionPolylineCompleteShape(bool fromButtonClick = false)
        {
            if (SystemState.DrawingMode == DrawingMode.Default)
            {
                return;
            }

            if (!SystemState.DrawingShape)
            {
                return;
            }

            if (SystemState.DrawingMode != DrawingMode.Polyline)
            {
                return;
            }

            if (buildingPolyline == null)
            {
                return;
            }

            if (buildingPolyline.CoordList.Count < 2)
            {
                ManagedMessageBox.Show("At least 2 points must be selected to complete the current drawing.");

                return;
            }

            DirectedLine frstLine = buildingPolyline.FrstLine.ExtendStart(0.05);
            DirectedLine lastLine = buildingPolyline.LastLine.ExtendEnd(0.05);

            if (!LayoutAreaForSubdivision.ExternalArea.Intersects(frstLine) || !LayoutAreaForSubdivision.ExternalArea.Intersects(lastLine))
            {
                ProcessSeamModeCancelShapeInProgress();

                MessageBox.Show("The first and last line must intersect the area boundary.");

                return;
            }

            ProcessSubdivisionPolylineByLine();
            
        }

        private void ProcessSubdivisionPolylineByLine()
        {
           

            //RemoveMarkerAndGuides();

            bool result = processSubdivideByLine(LayoutAreaForSubdivision, buildingPolyline);

            if (!result)
            {
                return;
            }

            string layerConsistency = LayerVisibilityConsistencyCheck(LayoutAreaForSubdivision.AreaFinishManager.AreaDesignStateLayer);

            completeSubdivisionProcess(LayoutAreaForSubdivision);

            layerConsistency = LayerVisibilityConsistencyCheck(LayoutAreaForSubdivision.AreaFinishManager.AreaDesignStateLayer);

            buildingPolyline = null;
            SystemState.DrawingShape = false;

            BaseForm.BtnSeamDesignStateSelectionMode_Click(null, null);
            BaseForm.btnAutoSelect.Checked = false;

            //this.SeamingTool.Show();
            //this.SeamingTool.Select();
        }


        private bool processSubdivideByLine(CanvasLayoutArea layoutAreaForSubdivision, CanvasDirectedPolyline polyline)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { layoutAreaForSubdivision, polyline });
#endif

            try
            {

                if (Utilities.IsNotNull(polyline.StartMarker))
                {
                    polyline.StartMarker.Delete();
                }

                RemoveMarkerAndGuides();

                //layoutAreaForSubdivision.SubdivideByLine(polyline);

                //---------------------------------------------------------------------------------------------------------//
                // In this approach, the underlying Visio geometry engine is used to subdivide the shape.                  //
                // This is done because it is much more reliable that building the corresponding logic from scratch.       //
                // However, the geometry engine has side effects, most notably that it removes the original shape from the //
                // canvas and much of the following logic is needed to deal with this. The process is pretty tricky and    //
                // may be hard to follow.                                                                                  //
                //---------------------------------------------------------------------------------------------------------//

                ((GraphicsLayoutArea)layoutAreaForSubdivision).Shape = layoutAreaForSubdivision.Shape;

                // Need to remove start marker here because otherwise it gets repeated by the visio engine and leaves
                // a circle on the canvas.
                // This is where the subdivision actually occurs. What gets returned is a list of shapes of subdivided regions. These shapes
                // have already been placed on the canvas by the Visio geometry engine.
                //string layerConsistency = LayerVisibilityConsistencyCheck(layoutAreaForSubdivision.AreaFinishManager.AreaDesignStateLayer);

                List<GraphicShape> subdivideShapeList = layoutAreaForSubdivision.Divide(Window, Page, polyline.GetCoordinateList());

                if (subdivideShapeList.Count <= 0)
                {
                    return false;
                }

                List<CanvasLayoutArea> subdividedAreaList = new List<CanvasLayoutArea>();

                List<DirectedPolygon> internalBoundaryList = new List<DirectedPolygon>();

                //layerConsistency = LayerVisibilityConsistencyCheck(layoutAreaForSubdivision.AreaFinishManager.AreaDesignStateLayer);

                foreach (GraphicShape shape in subdivideShapeList)
                {
                    internalBoundaryList.Clear();

                    DirectedPolygon externalBoundary = VisioInterop.GetShapeBoundaries(shape, internalBoundaryList);

                    //internalBoundaryList.Clear();

                    LayoutArea layoutArea = new LayoutArea(externalBoundary, internalBoundaryList);

                    // The corresponding shapes are already on the canvas (Visio puts them there). Now we construct layout areas around these shapes for
                    // internal maintenance. Normally, it would be the other way around: The layout area is constructed first and then drawn onto the canvas.

                    FinishesLibElements finishesLibElements = new FinishesLibElements
                        (layoutAreaForSubdivision.AreaFinishManager.AreaFinishBase
                        , null
                        , null
                        , layoutAreaForSubdivision.AreaFinishManager.AreaFinishLayers
                        , null
                        , null);

                    CanvasLayoutArea canvasLayoutArea = new CanvasLayoutArea(
                        this
                        , layoutArea
                        , layoutAreaForSubdivision.AreaFinishManager
                        , finishesLibElements
                        , layoutAreaForSubdivision.AreaFinishManager.UCAreaPaletteElement
                        , DesignState.Seam);

                    canvasLayoutArea.Guid = shape.Guid;

                    VisioInterop.SetShapeData(shape, "Subdivison Layout Area", "Complex Shape[" + layoutAreaForSubdivision.AreaFinishManager.AreaName + "]", shape.Guid);

                    canvasLayoutArea.Shape = shape;

                    subdividedAreaList.Add(canvasLayoutArea);
                }

                if (subdividedAreaList.Count <= 0)
                {
                    return false;
                }

                generateOffspringLayoutAreas(layoutAreaForSubdivision, subdividedAreaList);

                BaseForm.SetupSeamModeDesignState(layoutAreaForSubdivision.AreaFinishManager);

                //layerConsistency = LayerVisibilityConsistencyCheck(layoutAreaForSubdivision.AreaFinishManager.AreaDesignStateLayer);

                return true;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in SeamDesignStateSubdivideAreaManager:processSubdivideByLine", ex, 1, true);

                return false;
            }
        }


        private void generateOffspringLayoutAreas(CanvasLayoutArea layoutAreaForSubdivision, List<CanvasLayoutArea> subdividedAreaList)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { layoutAreaForSubdivision, subdividedAreaList });
#endif

            if (subdividedAreaList.Count <= 0)
            {
                return;
            }

            try
            {
                foreach (CanvasLayoutArea canvasLayoutArea in subdividedAreaList)
                {
                    string data1 = "Subdivision LayoutArea";
                    string data2 = "Complex Shape[" + layoutAreaForSubdivision.AreaFinishManager.AreaName + "]";
                    string data3 = canvasLayoutArea.Guid;

                    VisioInterop.SetShapeData(canvasLayoutArea.Shape, data1, data2, data3);

                    canvasLayoutArea.Lock();

                    layoutAreaForSubdivision.AddToOffspringList(canvasLayoutArea);

                    canvasLayoutArea.SetupFromParentNewMethod(layoutAreaForSubdivision);

                    canvasLayoutArea.BringToFront();

                    // Add the subdivided region into the system.

                    CurrentPage.AddLayoutArea(canvasLayoutArea);
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in SeamDesignStateSubdivideAreaManager:generateOffspringLayoutAreasNewMethod", ex, 1, true);
            }
        }

        private void completeSubdivisionProcess(CanvasLayoutArea layoutAreaForSubdivision)
        {

            //------------------------------------------------------------------------------------------------------------//
            // There are two basic situations to deal with: (1) Subdivide a top level area and (2) subdivide a leaf area. //
            // When subdividing a top level area, the area itself is taken off the seam mode layers but left in the area  //
            // mode layers becauase the area must still appear in area mode. When subdividing a leaf area, the area is    //
            // removed from the seam mode layers (it should not be in the area mode layers anyway) and put in the         //
            // invisibility workspace layer to hide it.                                                                   //
            //------------------------------------------------------------------------------------------------------------//

            GraphicsLayerBase areaDesignStateLayer = layoutAreaForSubdivision.AreaFinishManager.AreaFinishLayers.AreaDesignStateLayer;
            GraphicsLayerBase seamDesignStateLayer = layoutAreaForSubdivision.AreaFinishManager.AreaFinishLayers.SeamDesignStateLayer;

            RemoveMarkerAndGuides();

            buildingPolyline.Delete();

            SystemState.DrawingShape = false;

            buildingPolyline = null;

            SystemState.DrawingMode = DrawingMode.Default;

            layoutAreaForSubdivision.SeamDesignStateSubdivisionModeSelected = false;

            layoutAreaForSubdivision.Shape.VisioShape = null;

            setSubdividedAreasSelectionStatus(layoutAreaForSubdivision);

            if (layoutAreaForSubdivision.ParentArea is null)
            {
                //--------------------------------------------------------------------------------------------------//
                // In this case, the layout area that gets subdivided is the original (high level) shape.           //
                // The visio geometry engine removes the original shapes from the canvas and returns a new shape,   //
                // and so, to keep things consistent, the original shape must be removed from the system and then   //
                // the shape that visio returns must be added into the system at the top level in order to maintain //
                // it for area mode.                                                                                //
                //--------------------------------------------------------------------------------------------------//

                areaDesignStateLayer.RemoveShapeFromLayer(layoutAreaForSubdivision, 1);

                seamDesignStateLayer.RemoveShapeFromLayer(layoutAreaForSubdivision, 1);

                string guid = layoutAreaForSubdivision.Guid; // Save Guid to reset

                layoutAreaForSubdivisionDict.Remove(guid);

                CurrentPage.RemoveLayoutArea(layoutAreaForSubdivision); // Remove previous version of the top level layout area

                GraphicShape shape = layoutAreaForSubdivision.DrawCompositeShape(Window, Page, true); // Draw a new version (put it back) on the canvas because visio subdivide will have removed it.
                
                layoutAreaForSubdivision.Shape = shape;

                layoutAreaForSubdivision.Guid = guid;

                layoutAreaForSubdivision.Shape.Guid = guid;

                VisioInterop.SetShapeData(shape, "Layout Area Shape", "Compound Shape[" + layoutAreaForSubdivision.AreaFinishManager.AreaName + "]", guid);

                layoutAreaForSubdivision.ExternalArea.Guid = guid;

                layoutAreaForSubdivision.AreaFinishManager.AddNormalLayoutAreaAreaStateOnly(layoutAreaForSubdivision, true);   // Put the layout area back in the finish list

                CurrentPage.AddLayoutArea(layoutAreaForSubdivision); // Add the layout area back into the system.


                //-----------------------------------------------------------------------------------------------------------------------------//
                //-----------------------------------------------------------------------------------------------------------------------------//

                layoutAreaForSubdivision.SetCompletedLineWidth();

                layoutAreaForSubdivision.Lock();
            }

            else
            {
                //--------------------------------------------------------------------------------------------------//
                // In this case, the layout area that gets subdivided from a subdivided area. As with the top level //
                // area, the visio geometry engine removes the original shapes from the canvas and returns a new    //
                // shape, and so, to keep things consistent, the original shape must be removed from the system and //
                // then the shape that visio returns must be added into the system but made to be invisible.        //
                //--------------------------------------------------------------------------------------------------//

                //------------------------------------------------//
                // The subdivided area is in the seam layer only. //
                //------------------------------------------------//

                seamDesignStateLayer.RemoveShapeFromLayer(layoutAreaForSubdivision, 1);

                string guid = layoutAreaForSubdivision.Guid; // Save Guid to reset

                layoutAreaForSubdivisionDict.Remove(guid);

                CurrentPage.RemoveLayoutArea(layoutAreaForSubdivision); // Remove previous version of area to be subdivided

                GraphicShape shape = layoutAreaForSubdivision.DrawCompositeShape(Window, Page, true); // Draw a new version (put it back) on the canvas because visio subdivide will have removed it.

                layoutAreaForSubdivision.Shape = shape;

                layoutAreaForSubdivision.Guid = guid;

                layoutAreaForSubdivision.Shape.Guid = guid;

                VisioInterop.SetShapeData(shape, "Layout Area Shape", "Compound Shape[" + layoutAreaForSubdivision.AreaFinishManager.AreaName + "]", guid);

                layoutAreaForSubdivision.ExternalArea.Guid = guid;

                //-------------------------------------------------------------------------//
                // We add this new area, reconstructed to the invisibility workspace layer //
                // so it does not show up on the canvas.                                   //
                //-------------------------------------------------------------------------//

                CurrentPage.AddLayoutArea(layoutAreaForSubdivision); // Add the layout area back into the system.

                Page.InvisibleWorkspaceLayer.AddShape(layoutAreaForSubdivision, 1);

                layoutAreaForSubdivision.Lock();
            }

            //-----------------------------------------------------------------------------------------------------------------------------//
            //  The following adds the lines back into the shape list so that the graphics shape list is consistent with the visio shapes. //
            //-----------------------------------------------------------------------------------------------------------------------------//

            foreach (CanvasDirectedLine externalAreaLine in layoutAreaForSubdivision.ExternalArea)
            {
                Page.AddToPageShapeDict(externalAreaLine);

                externalAreaLine.LineFinishManager.SeamDesignStateLayer.RemoveShapeFromLayer(externalAreaLine, 1);

                Page.InvisibleWorkspaceLayer.AddShape(externalAreaLine, 1); // Makes the lines invisible since not needed.
            }

            foreach (CanvasDirectedPolygon internalArea in layoutAreaForSubdivision.InternalAreas)
            {
                foreach (CanvasDirectedLine internalAreaLine in internalArea)
                {
                    Page.AddToPageShapeDict(internalArea);

                    internalAreaLine.LineFinishManager.SeamDesignStateLayer.RemoveShapeFromLayer(internalAreaLine, 1);

                    Page.InvisibleWorkspaceLayer.AddShape(internalAreaLine, 1); // Makes the lines invisible since not needed.
                }
            }

            Window?.DeselectAll();

        }
    }
}
