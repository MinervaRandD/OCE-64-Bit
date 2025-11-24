
namespace FloorMaterialEstimator.CanvasManager
{
    using FloorMaterialEstimator.Finish_Controls;
    using Geometry;
    using Graphics;
    using PaletteLib;
    using FinishesLib;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Utilities;
    using Globals;
    using System;
    using MaterialsLayout;
    using System.Drawing;
    using System.Windows.Input;

    public partial class CanvasManager
    {
        private void processAreaDesignStateLayoutModeClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            if (button == 1)
            {
                processAreaDesignStateLayoutModeLeftClick(button, x, y);

                return;
            }

            if (button == 2)
            {
                processAreaDesignStateLayoutModeRghtClick(x, y);

                return;
            }
        }


        private void processAreaDesignStateLayoutModeLeftClick(int button, double x, double y)
        {
            if (SystemState.FixedWidthMode && SystemState.DrawingShape)
            {
                this.BorderManager.BorderDrawingModeClick(1, x, y);
            }

            else if (SystemState.BtnCopyAndPasteShapes.BackColor == Color.Orange)
            {
                if (button == 1)
                {
                    processCopyAndPasteAction(x, y);
                }

            }

            else
            {
                processAreaDesignStateLayoutModeNormalLeftClick(x, y);
            }
        }

        private void processAreaDesignStateLayoutModeNormalLeftClick(double x, double y)
        { 
            CanvasLayoutArea canvasLayoutArea = CurrentPage.GetSelectedToplevelLayoutArea(x, y);

            if (canvasLayoutArea is null)
            {

                foreach (CanvasLayoutArea selectedArea in CurrentPage.AreaDesignStateSelectedAreas())
                {
                    if (selectedArea != canvasLayoutArea)
                    {
                        CurrentPage.SetAreaDesignStateAreaSelectionStatus(selectedArea);
                    }
                }

                RaiseAreaSelectionChangedEvent();

                return;
            }

            if (KeyboardUtils.ShiftKeyPressed)
            {
                CurrentPage.UpdateAreaDesignStateAreaSelectionStatus(canvasLayoutArea);
                RaiseAreaSelectionChangedEvent();
                return;
            }

            if (canvasLayoutArea.AreaDesignStateEditModeSelected)
            {
                CurrentPage.SetAreaDesignStateAreaSelectionStatus(canvasLayoutArea);
                RaiseAreaSelectionChangedEvent();
                return;
            }

            foreach (CanvasLayoutArea selectedArea in CurrentPage.AreaDesignStateSelectedAreas())
            {
                if (selectedArea != canvasLayoutArea)
                {
                    CurrentPage.SetAreaDesignStateAreaSelectionStatus(selectedArea);
                }
            }

            CurrentPage.UpdateAreaDesignStateAreaSelectionStatus(canvasLayoutArea);

            RaiseAreaSelectionChangedEvent();

            //if (canvasLayoutArea != null)
            //{
            //      canvasLayoutArea.Select();
            //}
        }

        public void processAreaDesignStateLayoutModeNormalSelectDrag(IEnumerable<GraphicShape> selectedShapes)
        {
            HashSet<CanvasLayoutArea> selectedCanvasLayoutAreas = new HashSet<CanvasLayoutArea>();

            foreach (GraphicShape graphicShape in selectedShapes)
            {
                const ulong compare = (ulong)ShapeType.LayoutArea | (ulong)ShapeType.LayoutAreaShape | (ulong)ShapeType.Polyline;

                // The following selection process is obfuscated. But it is necessary because the code (specifically with loading
                // projects is obfuscated. It should be cleaned up.

                if (((ulong)graphicShape.ShapeType & compare) != 0)
                {
                    CanvasLayoutArea canvasLayoutArea = null;

                    //if (CurrentPage.LayoutAreaDict.ContainsKey(graphicShape.Guid))
                    //{
                    //    canvasLayoutArea = CurrentPage.LayoutAreaDict[graphicShape.Guid];
                    //}

                    //else
                    //{
                    try
                    {
                        // Kludge. See comment above.

                        canvasLayoutArea = (CanvasLayoutArea)graphicShape.ParentObject;
                    }
                    catch (Exception e)
                    {

                    }
                    //}

                    if (canvasLayoutArea != null)
                    {
                        this.CurrentPage.SetAreaDesignStateAreaToSelected(canvasLayoutArea);
                        selectedCanvasLayoutAreas.Add(canvasLayoutArea);
                    }
                }

            }

            if (!KeyboardUtils.ShiftKeyPressed)
            {
                foreach (CanvasLayoutArea canvasLayoutArea in CurrentPage.LayoutAreas)
                {
                    if (!canvasLayoutArea.AreaDesignStateEditModeSelected)
                    {
                        continue;
                    }

                    if (selectedCanvasLayoutAreas.Contains(canvasLayoutArea))
                    {
                        continue;
                    }

                    CurrentPage.SetAreaDesignStateAreaToDeselected(canvasLayoutArea);
                }
            }

            if (selectedCanvasLayoutAreas.Count > 0)
            {
                RaiseAreaSelectionChangedEvent();
            }

            return;
        }

        public void RaiseAreaSelectionChangedEvent()
        {
            if (this.AreaSelectionChangedEvent != null)
            {
                List<CanvasLayoutArea> selectedShapes = new List<CanvasLayoutArea>();

                foreach (CanvasLayoutArea selectedArea in AreaDesignStateSelectedAreas)
                {
                    if (selectedArea.LayoutAreaType != LayoutAreaType.ColorOnly)
                    {
                        selectedShapes.Add(selectedArea);
                    }
                }

                this.AreaSelectionChangedEvent.Invoke(selectedFinishType, selectedShapes);
            }
        }

        private void processAreaDesignStateLayoutModeRghtClick(double x, double y)
        {
            if (SelectedAreaFinishManager.Filtered)
            {
                MessageBoxAdv.Show(
                    "Selected area finish is filtered. Clear filter to continue.", "Selected Area Finish Is Filtered",MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Exclamation);
                return;
            }

            if (SystemState.TakeoutAreaMode || SystemState.TakeoutAreaAndFillMode /* || KeyboardUtils.TKeyPressed */)
            {
                processAreaLayoutTakeoutAreaModeClick(x, y);
            }

            else if (SystemState.CounterMode)
            {
                CounterController.ProcessCounterModeClick(x, y);
            }

            else if (SystemState.AreaModeLabelMode)
            {
                LabelManager.ProcessLabelModeClick(x, y);
            }

            else if (SystemState.FixedWidthMode)
            {
                if (SystemState.FixedWidthInInches == 0)
                {
                    MessageBoxAdv.Show("Fixed width must be non-zero", "Fixed Width Is Zero", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Error);
                    return;
                }

                this.BorderManager.BorderDrawingModeClick(2, x, y);
            }
            
            else if (SystemState.OversGeneratorMode)
            {
                if (SystemState.OversGeneratorWidthInInches == 0)
                {
                    MessageBoxAdv.Show("Overs generator seam width must be non-zero", "Seam Width Is Zero", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Error);
                    return;
                }

                processAreaLayoutNonTakeoutAreaModeRghtClick(x, y);
            }

            else
            {
                processAreaLayoutNonTakeoutAreaModeRghtClick(x, y);
            }
        }

        private void processAreaLayoutNonTakeoutAreaModeRghtClick(double x, double y)
        {
            int? numericKeyPressed = KeyboardUtils.GetKeyboardNumericKeyPressed();

            if (numericKeyPressed.HasValue)
            {
                processAreaLayoutNonTakeoutAreaModeRghtNumericClick(x, y, numericKeyPressed.Value); // Apologies for the long name.

                return;

            }

            numericKeyPressed = KeyboardUtils.GetKeypadNumericKeyPressed();

            if (numericKeyPressed.HasValue)
            {
                processAreaLayoutNonTakeoutAreaModeRghtNumericClick(x, y, numericKeyPressed.Value); // Apologies for the long name.

                return;

            }

            //////////////////////////////////////////////////////////////////////////////////////
            // MDD Revamp: The following code was re-added to create double lines in area mode  //
            // It somehow got deleted but wasn't supposed to                                    //
            //////////////////////////////////////////////////////////////////////////////////////
            ///
            if (KeyboardUtils.F1KeyPressed)
            {
                // In the following, the line is added twice, once for area mode and once for line mode.
                // When lines are created under this condition, the area mode line is temporary and deleted next time return to area mode is made,
                // but the line in line mode is maintained.

                if (!Add2XLineFromGuidesHorizontal(x, y, DesignState.Area)) // Area mode line: Note that design state is set to area so the system knows that the line was created in area mode and can react appropriately
                {
                    return;
                }
                    
                Add2XLineFromGuidesHorizontal(x, y, DesignState.Line); // Line mode line

                return;
            }

            if (KeyboardUtils.F2KeyPressed)
            {
                // In the following, the line is added twice, once for area mode and once for line mode.
                // When lines are created under this condition, the area mode line is temporary and deleted next time return to area mode is made,
                // but the line in line mode is maintained.

                if (!Add2XLineFromGuidesVertical(x, y, DesignState.Area))// Area mode line: Note that design state is set to area so the system knows that the line was created in area mode and can react appropriately
                {
                    return;
                }
                
                Add2XLineFromGuidesVertical(x, y, DesignState.Line);// Line mode line.

                return;
            }

            /////////////////////////////
            // End of revamped section //
            /////////////////////////////

            if (KeyboardUtils.DKeyPressed)
            {
                processAreaLayoutNonTakeoutAreaModeRghtDeleteClick(x, y); // Apologies for the long name.

                return;
            }

            CurrentPage.LastPointDrawn = new Coordinate(x, y);

            if (KeyboardUtils.IsKeyPressed((int)Key.A) && buildingPolyline is null)
            {
                if (!Add2XLineFromGuidesHorizontal(x, y, DesignState.Line))
                {
                    return;
                }

                Add2XLineFromGuidesHorizontal(x, y, DesignState.Area);

                return;
            }

            if (KeyboardUtils.IsKeyPressed((int)Key.S) && buildingPolyline is null)
            {
                if (!Add2XLineFromGuidesVertical(x, y, DesignState.Line))
                {
                    return;
                }

                Add2XLineFromGuidesVertical(x, y, DesignState.Area);

                return;
            }

            if (!SystemState.DrawingShape || BuildingPolyline is null)
            {
                Debug.Assert(BuildingPolyline is null);

                InitializePolylineDraw(x, y, KeyboardUtils.AltKeyPressed);

                // When adding the first line from guides, downstream logic may mean that the polyline is
                // in fact not started. It is important to reset the system in this case.

                if (BuildingPolyline is null)
                {
                    SystemState.DrawingShape = false;

                    return;
                }

                if (BuildingPolyline.CoordList.Count <= 0)
                {
                    BuildingPolyline = null;

                    SystemState.DrawingShape = false;

                    return;
                }

                SystemState.DrawingShape = true;
            }

            else
            {
                Debug.Assert(!(BuildingPolyline is null));
             
                ContinuePolylineDraw(x, y, KeyboardUtils.AltKeyPressed);
            }
        }

        private void processAreaLayoutNonTakeoutAreaModeRghtNumericClick(double x, double y, int numericKey)
        {
            if (SystemState.OversGeneratorMode && SystemState.OversGeneratorWidthInInches == 0)
            {
                MessageBoxAdv.Show("Overs generator seam width must be non-zero", "Seam Width Is Zero", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Error);
                return;
            }

            keyUsedAsShiftKey = true;

            if (SystemState.DrawingShape || !(this.BuildingPolyline is null))
            {
                ManagedMessageBox.Show("Complete current drawing before using the auto-fill function.");
                return;
            }

            if (numericKey == 0)
            {
                numericKey = FinishGlobals.AreaFinishBaseList.SelectedItemIndex + 1;
            }

#if false
            CanvasLayoutArea canvasLayoutArea = CurrentPage.GetSelectedLayoutArea(x, y);

            if (canvasLayoutArea != null)
            {
                if (canvasLayoutArea.AreaDesignStateEditModeSelected)
                {
                    ProcessEditAreaModeActionChangeShapeToFinish(canvasLayoutArea, PalettesGlobal.AreaFinishPalette[numericKey - 1]);

                    return;
                }
            }
#endif

            SystemState.DrawingShape = true;

            SystemState.DrawingMode = DrawingMode.Polyline;

            BuildingPolyline = new CanvasDirectedPolyline(this);

            BuildingPolyline.BuildStatus = AreaShapeBuildStatus.Building;

            if (BuildingPolyline.AutoCompleteFromGuides(x, y, numericKey))
            {
                return;
            }
            // If the click is in an existing area, process is to change this area to the new area.


            CanvasLayoutArea canvasLayoutArea = CurrentPage.GetSelectedLayoutArea(x, y);

            if (canvasLayoutArea != null)
            {
                    ProcessEditAreaModeActionChangeShapeToFinish(canvasLayoutArea, FinishManagerGlobals.AreaFinishManagerList[numericKey - 1]);

                    return;
            }

        }

        private void processAreaLayoutNonTakeoutAreaModeRghtDeleteClick(double x, double y)
        {
            CanvasLayoutArea canvasLayoutArea = CurrentPage.GetSelectedLayoutArea(x, y);

            if (!(canvasLayoutArea is null))
            {
                ProcessEditAreaModeActionDeleteShape(canvasLayoutArea);
            }
        }

        private void processAreaLayoutTakeoutAreaModeClick(double x, double y)
        {
            if (KeyboardUtils.KKeyPressed)
            {
                if (processAreaLayoutTakeoutGuideClick(x, y))
                {
                    if (!(BuildingPolyline is null))
                    {
                        BuildingPolyline.Delete();

                        BuildingPolyline = null;
                    }

                    SystemState.DrawingShape = false;
                }
            }

            else
            {
                int? numericKeyValue = KeyboardUtils.GetNumericKeyPressed();

                if (numericKeyValue.HasValue)
                {
                    if (numericKeyValue.Value >= AreaFinishPalette.Count)
                    {
                        ManagedMessageBox.Show("There is no area finish number " + numericKeyValue.Value + ".");
                        return;
                    }

                    if (processAreaLayoutTakeoutGuideClick(x, y, numericKeyValue.Value))
                    {
                        if (!(BuildingPolyline is null))
                        {
                            BuildingPolyline.Delete();

                            BuildingPolyline = null;
                        }

                        SystemState.DrawingShape = false;
                    }
                }

                else
                {
                    processAreaLayoutTakeoutNormalClick(x, y);
                }
            }

        }

        private bool processAreaLayoutTakeoutGuideClick(double x, double y, int? fillFinishIndex = null)
        {
            double? xMin = null;
            double? xMax = null;
            double? yMin = null;
            double? yMax = null;

            FieldGuideController.GetBoundingGuides(x, y, out xMin, out xMax, out yMin, out yMax);

            if (xMin == null)
            {
                ManagedMessageBox.Show("No left guideline found.");
                return false;
            }

            if (xMax == null)
            {
                ManagedMessageBox.Show("No right guideline found.");
                return false;
            }

            if (yMax == null)
            {
                ManagedMessageBox.Show("No upper guideline found.");
                return false;
            }


            if (yMin == null)
            {
                ManagedMessageBox.Show("No lower guideline found.");
                return false;
            }

            Coordinate coord1 = new Coordinate(xMin.Value, yMin.Value);
            Coordinate coord2 = new Coordinate(xMin.Value, yMax.Value);
            Coordinate coord3 = new Coordinate(xMax.Value, yMax.Value);
            Coordinate coord4 = new Coordinate(xMax.Value, yMin.Value);

            CanvasLayoutArea canvasLayoutArea = CurrentPage.GetContainingLayoutArea(coord1, coord2, coord3, coord4);

            if (canvasLayoutArea is null)
            {
                ManagedMessageBox.Show("Takeout area must be fully contained within an existing layout area");
                return false;
            }

            //DirectedPolygon directedPolygon = new DirectedPolygon(new List<Coordinate>() { coord1, coord2, coord3, coord4 });

            CanvasDirectedPolyline directedPolyline = new CanvasDirectedPolyline(this);

            directedPolyline.AddPoints
                (new List<Coordinate>() { coord1, coord2, coord3, coord4, coord1 }, true);

            CanvasDirectedPolygon directedPolygon = new CanvasDirectedPolygon(
                this, directedPolyline);
                
            if (!canvasLayoutArea.Contains(directedPolygon))
            {
                return false;
            }

            List<CanvasDirectedLine> lineList = new List<CanvasDirectedLine>();

            //CanvasDirectedPolygon canvasDirectedPolygon = new CanvasDirectedPolygon(this, directedPolygon, BaseForm.selectedLineFinish, DesignState.Area);

            AddAreaPolylineToCanvas(
                directedPolyline
                , FinishManagerGlobals.SelectedAreaFinishManager
                , AreaFinishPalette.SelectedFinish
                , FinishManagerGlobals.SelectedLineFinishManager
                , LineFinishPalette.SelectedLine
                , true);
            

            if (!fillFinishIndex.HasValue)
            {
                return true;
            }

            UCAreaFinishPaletteElement ucAreaFinish = null;
            
            if (fillFinishIndex.Value <= 0)
            {
                ucAreaFinish = AreaFinishPalette.SelectedFinish;
            }

            else
            {
                ucAreaFinish = AreaFinishPalette[fillFinishIndex.Value - 1];
            }

            UCLineFinishPaletteElement ucLineFinish = LineFinishPalette.SelectedLine;

            AreaFinishManager areaFinishManager = FinishManagerGlobals.SelectedAreaFinishManager;

            LineFinishManager lineFinishManager = FinishManagerGlobals.SelectedLineFinishManager;

            AddInternalAreaToCanvas(directedPolygon, areaFinishManager, ucAreaFinish, lineFinishManager, ucLineFinish);

            return true;
        }

        private void processAreaLayoutTakeoutNormalClick(double x, double y)
        {
            if (!SystemState.DrawingShape)
            {
                Debug.Assert(BuildingPolyline is null);

                CanvasLayoutArea canvasLayoutArea = CurrentPage.GetSelectedLayoutArea(x, y);

                if (canvasLayoutArea is null)
                {
                    // Not within layout area. Ignore click.

                    ManagedMessageBox.Show("You are in takeout area mode. Please click within an existing area to create a takeout area.");

                    return;
                }

                if (canvasLayoutArea.IsSeamed())
                {
                    ManagedMessageBox.Show("The current area is seamed. Please return to seam mode and remove seams before adding a takeout area.");

                    return;
                }

                CurrentPage.SelectedLayoutArea = canvasLayoutArea;

                InitializePolylineDraw(x, y, false); // For the time being, until I get issues with this worked out.

                CurrentPage.LastPointDrawn = new Coordinate(x, y);

                //InitializePolylineDraw(x, y, BaseForm.CntlKeyPressed);

                SystemState.DrawingShape = true;
            }

            else
            {
                Debug.Assert(!(BuildingPolyline is null));
               
                CanvasLayoutArea canvasLayoutArea = CurrentPage.GetSelectedLayoutArea(x, y);

                if (canvasLayoutArea is null)
                {

                    ManagedMessageBox.Show("You are in takeout area mode. Please click within an existing area to continue a takeout area.");
                    // You've clicked outside a layout area. Ignore the click.

                    return;
                }

                if (canvasLayoutArea.IsSeamed())
                {
                    ManagedMessageBox.Show("The current area is seamed. Please return to seam mode and remove seams before adding a takeout area.");

                    return;
                }

                if (canvasLayoutArea.IsSeamed())
                {
                    ManagedMessageBox.Show("The current area is seamed. Please return to seam mode and remove seams before adding a takeout area.");

                    return;
                }

                if (CurrentPage.SelectedLayoutArea.Guid != canvasLayoutArea.Guid)
                {
                    // You've clicked in a different layout area. Ignore the click.

                    ManagedMessageBox.Show("In takeout area mode, you must click within the same area until the area is completed.");

                    return;
                }

                ContinuePolylineDraw(x, y, false); // For the time being don't allow using guides in this mode until issues are worked out.

                //ContinuePolylineDraw(x, y, BaseForm.CntlKeyPressed);
            }
        }

        private void ProcessAreaStateArrowKey(int keyVal)
        {
            if (SystemState.SeamMode != SeamMode.Selection)
            {
                return;
            }

            switch (keyVal)
            {
                case 0: moveAreaStateSelectedItemLeft(); break;
                case 1: moveAreaStateSelectedItemUp(); break;
                case 2: moveAreaStateSelectedItemRght(); break;
                case 3: moveAreaStateSelectedItemDown(); break;

                default: break;
            }
        }

        private void moveAreaStateSelectedItemDown()
        {
            if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, 0, -1);
            }
        }

        private void moveAreaStateSelectedItemLeft()
        {
            if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, -1, 0);
            }
        }

        private void moveAreaStateSelectedItemUp()
        {
            if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, 0, 1);
            }
        }

        private void moveAreaStateSelectedItemRght()
        {
            if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, 1, 0);
            }
        }

        public void LineFinishSelectionChanged(UCLineFinishPaletteElement ucLine)
        {
            if (!SystemState.DrawingShape)
            {
                return;
            }

            if (BuildingPolyline is null)
            {
                return;
            }
        }
    }
}
