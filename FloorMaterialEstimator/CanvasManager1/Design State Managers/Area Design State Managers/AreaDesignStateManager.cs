namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
  
    using System.Diagnostics;
    using SettingsLib;
    using Globals;
    using System.Drawing;
    using Graphics;
    using PaletteLib;
    using Utilities;
    using MaterialsLayout;
    using Geometry;
    using MaterialsLayout.Embedded_Layout_Generation;
    using TracerLib;
    using FinishesLib;
    using Geometry;
    using System.Windows.Forms;

    public partial class CanvasManager
    {
        //public List<CanvasLayoutArea> AreaHistoryList = new List<CanvasLayoutArea>();

        public CanvasLayoutArea LastLayoutArea = null;

        public List<CanvasLayoutArea> RemovedAreaShapes = new List<CanvasLayoutArea>();

        public List<CanvasLayoutArea> MovedAreaShapes = new List<CanvasLayoutArea>();

        public void ProcessAreaDesignStateClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            cancelDefault = true;

            processAreaDesignStateLayoutModeClick(button, keyButtonState, x, y, ref cancelDefault);
           
            SystemState.CurrentProjectChanged = true;
        }


        private void ProcessAreaModeArrowClick(int arrowKey)
        {
            switch (arrowKey)
            {
                case 0: /* left arrow */
                    SnapToLeftGuideLine();
                    break;

                case 1: /* up arrow */
                    MoveToUpperGuideLine();
                    break;

                case 2: /* right arrow */
                    SnapToRghtGuideLine();
                    break;

                case 3: /* down arrow */
                    MoveToLowerGuideLine();
                    break;
            }
        }

        private void SnapToLeftGuideLine()
        {
            if (!SystemState.BtnShowFieldGuides.Checked)
            {
                return;
            }

            Point p;

            if (FieldGuideController.SnapToLeftFieldGuide(MouseX, MouseY, 0, StaticGlobals.GetCursorPosition(), out p))
            {
                StaticGlobals.SetCursorPosition(p);
            }
        }

        private void SnapToRghtGuideLine()
        {
            if (!SystemState.BtnShowFieldGuides.Checked)
            {
                return;
            }

            Point p;

            if (FieldGuideController.SnapToRghtFieldGuide(MouseX, MouseY, 0, StaticGlobals.GetCursorPosition(), out p))
            {
                StaticGlobals.SetCursorPosition(p);
            }
        }

        private void MoveToLeftGuideLine()
        {
            if (!SystemState.BtnShowFieldGuides.Checked)
            {
                return;
            }
         
            double? guideX = FieldGuideController.GetLeftGuideLineX(MouseX);

            if (guideX.HasValue)
            {
                Point p;

                p = VisioInterop.MapVisioToScreenCoords(axDrawingControl, VsoWindow, guideX.Value, MouseY, GlobalSettings.ShowRulers);

                StaticGlobals.SetCursorPosition(p);
            }
        }

        private void MoveToUpperGuideLine()
        {
            if (!SystemState.BtnShowFieldGuides.Checked)
            {
                return;
            }

            double? guideY = FieldGuideController.GetUpprGuideLineY(MouseY);

            if (guideY.HasValue)
            {
                Point p;

                p = VisioInterop.MapVisioToScreenCoords(axDrawingControl, VsoWindow, MouseX, guideY.Value, GlobalSettings.ShowRulers);

                StaticGlobals.SetCursorPosition(p);
            }
        }

        private void MoveToRightGuideLine()
        {
            if (!SystemState.BtnShowFieldGuides.Checked)
            {
                return;
            }

            double? guideX = FieldGuideController.GetRghtGuideLineX(MouseX);

            if (guideX.HasValue)
            {
                Point p;

                p = VisioInterop.MapVisioToScreenCoords(axDrawingControl, VsoWindow, guideX.Value, MouseY, GlobalSettings.ShowRulers);

                StaticGlobals.SetCursorPosition(p);
            }
        }

        private void MoveToLowerGuideLine()
        {
            if (!SystemState.BtnShowFieldGuides.Checked)
            {
                return;
            }

            double? guideY = FieldGuideController.GetLowrGuideLineY(MouseY);

            if (guideY.HasValue)
            {
                Point p;

                p = VisioInterop.MapVisioToScreenCoords(axDrawingControl, VsoWindow, MouseX, guideY.Value, GlobalSettings.ShowRulers);

                StaticGlobals.SetCursorPosition(p);
            }
        }


        public void ProcessAreaModeFinishNumericShortCut(int key)
        {
            int position = key - 1;

            if (position < 0 || position >= FinishGlobals.AreaFinishBaseList.Count)
            {
                return;
            }

            FinishGlobals.AreaFinishBaseList.SelectElem(position);

            if (true) // KeyboardUtils.CntlKeyPressed) Control key is screwing up visio
            {
                ProcessAreaModeSetAreasToSelectedMaterial();
            }
        }

        public void ProcessAreaModeSetAreasToSelectedMaterial()
        {
            AreaFinishManager areaFinishManager = CanvasManagerGlobals.SelectedAreaFinishManager;

            foreach (CanvasLayoutArea canvasLayoutArea in CurrentPage.AreaDesignStateSelectedAreas())
            {
                ProcessEditAreaModeActionChangeShapeToFinish(canvasLayoutArea, areaFinishManager);
                CurrentPage.SetAreaDesignStateAreaSelectionStatus(canvasLayoutArea, false);
            }
        }

        private void completeAreaDesignStatePolylineDraw(CanvasDirectedPolyline BuildingPolyline, int finishIndex = 0)
        {
  
            Debug.Assert(BuildingPolyline != null, "Attempt to complete polyline on null building polyline");

            Debug.Assert(BuildingPolyline.Count >= 3, "Attempt to complete polyline on too few lines.");

            //Coordinate frstCoord = BuildingPolyline.GetFirstCoordinate();

            CanvasDirectedLine lastLine = BuildingPolyline.LastLine;
            CanvasDirectedLine frstLine = BuildingPolyline.FrstLine;

            RemoveMarkerAndGuides();

            if (Utilities.IsNotNull(BuildingPolyline.StartMarker))
            {
                buildingPolyline.StartMarker.Delete();
                buildingPolyline.StartMarker = null;
            }

            frstLine.SetLineStartpoint(lastLine.GetLineEndpoint());

            //frstLine.SyncWithVisio();
            UCAreaFinishPaletteElement ucAreaFinish = null;

            AreaFinishManager areaFinishManager = null;

            if (finishIndex > 0)
            {
                ucAreaFinish = PalettesGlobal.AreaFinishPalette[finishIndex - 1];
                areaFinishManager = CanvasManagerGlobals.AreaFinishManagerList[finishIndex - 1];
            }

            AddAreaPolylineToCanvas(BuildingPolyline, areaFinishManager, ucAreaFinish, null, null, !(SystemState.RbnColorOnly.Checked || SystemState.RbnOversGenerator.Checked));

            CanvasManagerGlobals.LineFinishManagerList.UpdateFinishStats();

            //-------------------------------------------------------//
            // Only update overs and unders if it is a roll product. //
            // This is because you update the stats in this call.    //
            //-------------------------------------------------------//

#if TEMPREMOVED
            if (selectedFinishType.AreaFinishBase.MaterialsType == FinishesLib.MaterialsType.Rolls)
            {
                StaticGlobals.OversUndersFormUpdate(true);
            }
#endif
            //this.BuildingPolyline.Delete();

            this.BuildingPolyline = null;

            SystemState.DrawingShape = false;

            //------------------------------------------------------------------------------------------//
            // The following call is necessary because line layers may have been dynamically created,   //
            // with the default visibility to visible, but we want them not to be visible in area mode. //
            //------------------------------------------------------------------------------------------//

            CanvasManagerGlobals.SelectedLineFinishManager.SetLineState(DesignState.Area, SeamMode, true);

            SystemState.DrawingMode = DrawingMode.Default;

            
        }

        /// <summary>
        /// This routine is called once a polyline has been completed to add the polyline to the canvas.
        /// </summary>
        /// <param name="canvasDirectedPolyline">The building polyline that has been completed</param>
        /// <param name="ucAreaFinish">The area finish that to add the resulting area to</param>
        public void AddAreaPolylineToCanvas(
            CanvasDirectedPolyline canvasDirectedPolyline
            , AreaFinishManager areaFinishManager = null
            , UCAreaFinishPaletteElement ucAreaFinish = null
            , LineFinishManager lineFinishManager = null
            , UCLineFinishPaletteElement ucLineFinish = null
            , bool createAssociatedLineModeLines = true)
        {
            if (areaFinishManager is null)
            {
                areaFinishManager = CanvasManagerGlobals.SelectedAreaFinishManager;
            }

            if (ucAreaFinish is null)
            {
                ucAreaFinish = selectedFinishType;
            }

            if (lineFinishManager is null)
            {
                lineFinishManager = SelectedLineFinishManager;
            }

            if (ucLineFinish == null)
            {
                ucLineFinish = SelectedLineType;
            }
#if DEBUG
            canvasDirectedPolyline.ValidateConsistentPerimeter();
#endif

            canvasDirectedPolyline.BuildStatus = AreaShapeBuildStatus.Completed;

            CanvasDirectedPolygon canvasDirectedPolygon = addAreaPolylineToCanvasBase(canvasDirectedPolyline, ucAreaFinish, ucLineFinish, createAssociatedLineModeLines);

            if (SystemState.TakeoutAreaMode)
            {
                addTakeoutAreaToCanvas(CurrentPage.SelectedLayoutArea, canvasDirectedPolygon, ucAreaFinish);
            }

            else if (SystemState.TakeoutAreaAndFillMode)
            {
                addTakeoutAreaToCanvas(CurrentPage.SelectedLayoutArea, canvasDirectedPolygon, CurrentPage.SelectedLayoutArea.AreaFinishManager.UCAreaPaletteElement);

                canvasDirectedPolygon = addAreaPolylineToCanvasBase(canvasDirectedPolyline, ucAreaFinish, ucLineFinish, createAssociatedLineModeLines);

                AddInternalAreaToCanvas(canvasDirectedPolygon, areaFinishManager, ucAreaFinish, lineFinishManager, ucLineFinish);
                //AddInternalAreaToCanvas(canvasDirectedPolygon.Clone(), ucAreaFinish, ucLineFinish);
            }

            else
            {
                AddInternalAreaToCanvas(canvasDirectedPolygon, areaFinishManager, ucAreaFinish, lineFinishManager, ucLineFinish);
            }

            canvasDirectedPolyline.Delete();

            AreaFinishManager selectedAreaFinish = CanvasManagerGlobals.AreaFinishManagerList.SelectedAreaFinishManager;

            selectedAreaFinish.SetDesignStateFormat(this.DesignState, SeamMode, selectedAreaFinish.Filtered, true);

            Window?.DeselectAll();
        }

        private CanvasDirectedPolygon addAreaPolylineToCanvasBase(
            CanvasDirectedPolyline canvasDirectedPolyline
            , UCAreaFinishPaletteElement ucAreaFinish
            , UCLineFinishPaletteElement ucLineFinish
            , bool createAssociatedLineModeLines)
        {

            // First we create two sets of lines, one will be for the line design state and the other will be the boundary of the 
            // layout area.

            List<CanvasDirectedLine> canvasLayoutAreaLineList = new List<CanvasDirectedLine>();

            foreach (CanvasDirectedLine canvasDirectedLine in canvasDirectedPolyline)
            {
                UCLineFinishPaletteElement ucLine = canvasDirectedLine.ucLine;

                LineFinishManager lineFinishManager = canvasDirectedLine.LineFinishManager;

                CanvasDirectedLine lineClonedCanvasDirectedLine = null;

                CanvasDirectedLine areaClonedCanvasDirectedLine = null;

                if (createAssociatedLineModeLines)
                {
                    lineClonedCanvasDirectedLine = canvasDirectedLine.Clone();

                    lineClonedCanvasDirectedLine.ucLine = canvasDirectedLine.ucLine;

                    lineClonedCanvasDirectedLine.LineRole = LineRole.AssociatedLine;

                    // This forces the system to think that the line was created in line mode

                    lineClonedCanvasDirectedLine.OriginatingDesignState = DesignState.Line;

                    Shape shape = lineClonedCanvasDirectedLine.Draw();

                    lineClonedCanvasDirectedLine.SetShapeData();

                    //VisioInterop.SetShapeData(shape, "Associated Line", "Line[" + ucLine.LineName + "]", shape.Guid);

                    lineFinishManager.AddLine(lineClonedCanvasDirectedLine);

                    lineFinishManager.AddLineToLayer(lineClonedCanvasDirectedLine.Guid, DesignState.Line, SeamMode.Unknown);

                    CurrentPage.AddToDirectedLineDict(lineClonedCanvasDirectedLine);

                    LineHistoryList.Add(lineClonedCanvasDirectedLine);
                }

                areaClonedCanvasDirectedLine = canvasDirectedLine.Clone();

                areaClonedCanvasDirectedLine.ucLine = canvasDirectedLine.ucLine;

                areaClonedCanvasDirectedLine.LineRole = LineRole.ExternalPerimeter;

                if (createAssociatedLineModeLines)
                {
                    areaClonedCanvasDirectedLine.AssociatedDirectedLine = lineClonedCanvasDirectedLine;

                    lineClonedCanvasDirectedLine.AssociatedDirectedLine = areaClonedCanvasDirectedLine;
                }

                else
                {
                    areaClonedCanvasDirectedLine.AssociatedDirectedLine = null;
                }

                // This forces the system to think that the line was created in area mode

                areaClonedCanvasDirectedLine.OriginatingDesignState = DesignState.Area;

                canvasLayoutAreaLineList.Add(areaClonedCanvasDirectedLine);

            }

            // Now construct a canvas directed polygon from line list.

            CanvasDirectedPolygon canvasDirectedPolygon = new CanvasDirectedPolygon(this, Window, Page);

            foreach (CanvasDirectedLine canvasDirectedLine in canvasLayoutAreaLineList)
            {
                LineFinishManager lineFinishManager = canvasDirectedLine.LineFinishManager;

                UCLineFinishPaletteElement ucLine = canvasDirectedLine.ucLine;

                Shape shape = canvasDirectedLine.Draw();

                lineFinishManager.AddLine(canvasDirectedLine);

                // VisioInterop.SetShapeData(shape, "External Perimeter Line", "Line[" + ucLine.LineName + "]", shape.Guid);

                canvasDirectedLine.SetShapeData();

                //ucLine.AddLineToLayer(canvasDirectedLine.Guid, DesignState.Area);

                canvasDirectedPolygon.PerimeterAdd(canvasDirectedLine);

                canvasDirectedLine.ParentPolygon = canvasDirectedPolygon;

                CurrentPage.AddToDirectedLineDict(canvasDirectedLine);

                if (!SystemState.RbnColorOnly.Checked)
                {

                    //-------------------------------------------------------//
                    // Do not show lines for color only areas in seam mode   //
                    //                                                       //
                    // Note: This is a bad place to put this, but convenient //
                    // for a quick kludge.                                   //
                    //-------------------------------------------------------//

                    lineFinishManager.AddLineToLayer(canvasDirectedLine.Guid, DesignState.Seam, SeamMode.Unknown);
                }

                canvasDirectedPolygon.Lock();
            }

            return canvasDirectedPolygon;
        }

        private void addTakeoutAreaToCanvas(CanvasLayoutArea canvasLayoutArea, CanvasDirectedPolygon canvasDirectedPolygon, UCAreaFinishPaletteElement ucAreaFinish)
        {
            if (canvasLayoutArea is null)
            {
                return;
            }

            if (canvasDirectedPolygon is null)
            {
                return;
            }

            CanvasDirectedPolygon polygon = canvasLayoutArea.AddInternalPerimeter(canvasDirectedPolygon, ucAreaFinish);

            polygon.SetLineGraphics(DesignState, false, AreaShapeBuildStatus.Completed);

            canvasLayoutArea.AreaFinishManager.UpdateFinishStats();

            if (CanvasManagerGlobals.SelectedAreaFinishManager.MaterialsType == FinishesLib.MaterialsType.Rolls)
            {
                StaticGlobals.OversUndersFormUpdate(true);
            }

            LastLayoutArea = canvasLayoutArea;

#if DEBUG
            if (!canvasLayoutArea.AreaFinishManager.LayoutAreaDictContains(canvasLayoutArea))
            {
                Tracer.TraceGen.TraceError("In AreaDesignStateManager:addTakeoutAreaToCanvas canvasLayoutArea expected in layout area dictionary but not found there.", 1, true);
            }
#endif
            canvasLayoutArea.AreaFinishManager.RemoveLayoutArea(canvasLayoutArea);

            canvasLayoutArea.AreaFinishManager.AddNormalLayoutArea(canvasLayoutArea);

            canvasLayoutArea.Lock();

        }

        public void AddInternalAreaToCanvas(
            CanvasDirectedPolygon canvasDirectedPolygon
            , AreaFinishManager areaFinishManager
            , UCAreaFinishPaletteElement ucAreaFinish
            , LineFinishManager lineFinishManager
            , UCLineFinishPaletteElement ucLineFinish)
        {
            if (SystemState.EmbedLayoutAreas)
            {
                addEmbeddedInternalAreaToCanvas(canvasDirectedPolygon, areaFinishManager, ucAreaFinish, lineFinishManager, ucLineFinish);
            }

            else
            {
                AddAreaOverlappingInternalAreaToCanvas(canvasDirectedPolygon, ucAreaFinish, areaFinishManager);
            }
        }

        /// <summary>
        /// Adds embedded layout area to the system. 
        /// </summary>
        /// <param name="canvasDirectedPolyline">The polyline that defines the exterior of the layout area</param>
        /// <param name="ucAreaFinish">The area finish element</param>
        /// <param name="ucLineFinish">The line finish element</param>
        private void addEmbeddedInternalAreaToCanvas(
            CanvasDirectedPolygon canvasDirectedPolygon
            , AreaFinishManager areaFinishManager
            , UCAreaFinishPaletteElement ucAreaFinish
            , LineFinishManager lineFinishManager
            , UCLineFinishPaletteElement ucLineFinish)
        {
            List<LayoutArea> layoutAreaList = new List<LayoutArea>();

            foreach (CanvasLayoutArea currentLayoutArea in CurrentPage.LayoutAreas)
            {
                if (currentLayoutArea.IsSubdivided())
                {
                    continue;
                }

                layoutAreaList.Add((LayoutArea)currentLayoutArea);
            }

            if (layoutAreaList.Count <= 0)
            {
                AddAreaOverlappingInternalAreaToCanvas(canvasDirectedPolygon, ucAreaFinish, areaFinishManager);

                return;
            }

            // Generate the embedded layout areas

            List<DirectedLine> lineList = canvasDirectedPolygon.ConvertAll(l => (DirectedLine)l);

            DirectedPolygon externalArea = new DirectedPolygon(lineList);

            EmbeddedLayoutGenerator embeddedLayoutGenerator = new EmbeddedLayoutGenerator(externalArea, layoutAreaList);

            List<LayoutArea> results = embeddedLayoutGenerator.GenerateEmbeddedLayoutAreas();
            

            // Add the layout areas to the system. Note that unlike the case of the overlap polygon we have to 
            // add each internal and external area to the system at this point.

            foreach (LayoutArea layoutArea in results)
            {
                CanvasLayoutArea canvasLayoutArea = new CanvasLayoutArea(this, layoutArea, areaFinishManager, ucAreaFinish);

                // We have to manually add each layout area to the system and to the visio surface.
                // In the case where there is a non-embededed (overlapping) layout area, this happens
                // as the area is being built.

                // Note. Probably best to encapsulate this process as a method somewhere down the road.

                canvasLayoutArea.ExternalArea.CreateInternalAreaShape();

                //canvasLayoutArea.UCAreaFinish = ucAreaFinish;

                //AreaFinishManager areaFinishManager = ucAreaFinish.AreaFinishManager;

                foreach (CanvasDirectedLine canvasDirectedLine in canvasLayoutArea.ExternalArea)
                {
                    canvasDirectedLine.ParentPolygon = canvasLayoutArea.ExternalArea;

                    canvasDirectedLine.Draw(ucLineFinish.LineColor, ucLineFinish.LineWidthInPts);

                    lineFinishManager.AddLineFull(canvasDirectedLine);

                    CurrentPage.AddToDirectedLineDict(canvasDirectedLine);
                }

                if (canvasLayoutArea.InternalAreas.Count > 0)
                {
                    foreach (CanvasDirectedPolygon internalArea in canvasLayoutArea.InternalAreas)
                    {
                        internalArea.CreateInternalAreaShape();

                        foreach (CanvasDirectedLine canvasDirectedLine in internalArea.Perimeter)
                        {
                            canvasDirectedLine.ParentPolygon = canvasLayoutArea.ExternalArea;

                            canvasDirectedLine.Draw(ucLineFinish.LineColor, ucLineFinish.LineWidthInPts);

                            lineFinishManager.AddLineFull(canvasDirectedLine);

                            CurrentPage.AddToDirectedLineDict(canvasDirectedLine);
                        }
                    }
                }

                AddAreaCanvasLayoutAreaToCanvas(canvasLayoutArea, areaFinishManager);

                if (canvasLayoutArea.InternalAreas.Count > 0)
                {
                    List<Shape> internalAreaShapes = canvasLayoutArea.InternalAreas.ConvertAll(i => i.Shape);

                    canvasLayoutArea.RemoveInternalAreas(internalAreaShapes);
                }
            }

        }

        public void AddAreaOverlappingInternalAreaToCanvas(
            CanvasDirectedPolygon canvasDirectedPolygon
            , UCAreaFinishPaletteElement ucAreaFinish
            , AreaFinishManager areaFinishManager
            )
        {
            canvasDirectedPolygon.CreateInternalAreaShape();

            canvasDirectedPolygon.UCAreaFinish = ucAreaFinish;

            CanvasLayoutArea canvasLayoutArea = null;

            if (CurrentLayoutType == LayoutAreaType.OversGenerator)
            {
                double oversGeneratorOversWidthInInches
               = (double)SystemState.NudOversGeneratorWidthFeet.Value * 12.0 +
                 (double)SystemState.NudOversGeneratorWidthInches.Value;

                canvasLayoutArea = new CanvasLayoutArea(
                   this
                    , areaFinishManager
                   //, ucAreaFinish
                   , canvasDirectedPolygon
                   , new List<CanvasDirectedPolygon>()
                   , oversGeneratorOversWidthInInches
                   , DesignState.Area
                   , CurrentLayoutType);
            }

            else
            {

                canvasLayoutArea = new CanvasLayoutArea(
                    this
                    //, ucAreaFinish
                    , areaFinishManager
                    , canvasDirectedPolygon
                    , new List<CanvasDirectedPolygon>()
                    , DesignState.Area
                    , CurrentLayoutType);
            }

            AddAreaCanvasLayoutAreaToCanvas(canvasLayoutArea, areaFinishManager);
        }

        public void AddAreaCanvasLayoutAreaToCanvas(CanvasLayoutArea canvasLayoutArea, AreaFinishManager areaFinishManager)
        {
            CurrentPage.SelectedLayoutArea = canvasLayoutArea;

            if (SystemState.DesignState == DesignState.Area || SystemState.DesignState == DesignState.Seam)
            {
                canvasLayoutArea.SetCompletedLineWidth();
            }

            if (canvasLayoutArea.LayoutAreaType == LayoutAreaType.ZeroArea)
            {
                //---------------------------------------------------------//
                // Do not show zero (color only) layout areas in seam mode //
                //---------------------------------------------------------//

                areaFinishManager.AddNormalLayoutAreaAreaStateOnly(canvasLayoutArea, true);
            }

            else
            {
                areaFinishManager.AddNormalLayoutArea(canvasLayoutArea);
            }

            CurrentPage.AddLayoutArea(canvasLayoutArea);

            canvasLayoutArea.Shape.LockSelected(1);

            canvasLayoutArea.SetAreaDesignStateFormat();

            //this.AreaHistoryList.Add(canvasLayoutArea);

            LastLayoutArea = canvasLayoutArea;
        }

        public void AddSeamCanvasLayoutAreaToCanvas(CanvasLayoutArea canvasLayoutArea, AreaFinishManager areaFinishManager)
        {
            CurrentPage.SelectedLayoutArea = canvasLayoutArea;

            if (SystemState.DesignState == DesignState.Area || SystemState.DesignState == DesignState.Seam)
            {
                canvasLayoutArea.SetCompletedLineWidth();
            }

            areaFinishManager.AddNormalLayoutAreaSeamStateOnly(canvasLayoutArea);

            CurrentPage.AddLayoutArea(canvasLayoutArea);

            canvasLayoutArea.Shape.LockSelected(1);


            //this.AreaHistoryList.Add(canvasLayoutArea);

            LastLayoutArea = canvasLayoutArea;
        }
    }
}
