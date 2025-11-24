namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Geometry;
    using Graphics;
    using Utilities;

    using System.Diagnostics;
    using Globals;
    using System.Drawing;
    using MaterialsLayout;
    using TracerLib;
    using CanvasLib.Markers_and_Guides;

    public partial class CanvasManager
    {
 
        public CanvasLayoutArea CopySelectedLayoutArea { get; set; }  = null;

        private void processCopyAndPasteAction(double x, double y)
        {
            CanvasLayoutArea canvasLayoutArea = null;

            if (CopySelectedLayoutArea is null)
            {
                canvasLayoutArea = CurrentPage.GetSelectedLayoutArea(x, y);

                if (canvasLayoutArea is null)
                {
                    MessageBox.Show("An area to copy must be selected.");

                    return;
                }

                CopySelectedLayoutArea = canvasLayoutArea;

                canvasLayoutArea.SetCopyMarker(x,y);

                return;
            }

            canvasLayoutArea = CurrentPage.GetSelectedLayoutArea(x, y);

            if (canvasLayoutArea == CopySelectedLayoutArea)
            {
                if (canvasLayoutArea.CopyMarker != null)
                {
                    Page.RemoveFromPageShapeDict(canvasLayoutArea.CopyMarker);

                    canvasLayoutArea.AreaFinishManager.AreaDesignStateLayer.RemoveShapeFromLayer(canvasLayoutArea.CopyMarker, 1);

                    canvasLayoutArea.CopyMarker.Delete();
                }

                CopySelectedLayoutArea = null;

                return;
            }

            Coordinate translateCoord = new Coordinate(x, y) - CopySelectedLayoutArea.CopyMarker.Location();

            // Check to be sure shape fits on the canvas

            foreach (Coordinate coordinate in CopySelectedLayoutArea.ExternalArea.CoordinateList())
            {
                double transX = coordinate.X + translateCoord.X;
                double transY = coordinate.Y + translateCoord.Y;

                if (transX < 0 || transX > CurrentPage.PageWidth)
                {
                    MessageBox.Show("Pasted object will fall outside canvas boundary.");
                    return;
                }


                if (transY < 0 || transY > CurrentPage.PageHeight)
                {
                    MessageBox.Show("Pasted object will fall outside canvas boundary.");
                    return;
                }
            }

            CanvasLayoutArea pasteLayoutArea = CopySelectedLayoutArea.Clone();

            pasteLayoutArea.Translate(translateCoord);

            pasteLayoutArea.Shape = pasteLayoutArea.DrawCompositeShape(Window, Page);


            if (KeyboardUtils.ShiftKeyPressed)
            {
                //pasteLayoutArea.AddAssociatedLines(Window, Page);

                pasteLayoutArea.IsZeroLayoutArea = true;

                pasteLayoutArea.AreaFinishManager.AddNormalLayoutAreaAreaStateOnly(pasteLayoutArea);

                pasteLayoutArea.SetLineGraphicsForAreaDesignState(AreaShapeBuildStatus.Completed);

                CurrentPage.AddLayoutArea(pasteLayoutArea);

                VisioInterop.SetShapeData(pasteLayoutArea.Shape, "Layout Area Shape", "Compound Shape[" + pasteLayoutArea.AreaFinishManager.AreaName + "]", pasteLayoutArea.Guid);

                VisioInterop.SetBaseLineStyle(pasteLayoutArea.Shape, 0);

                pasteLayoutArea.ExternalArea.SetLineGraphicsForBorderArea();

                pasteLayoutArea.ExternalArea.Shape.SetLineOpacity(0);

                VisioInterop.SetShapeText(pasteLayoutArea.Shape, "T", Color.Red, 22);

                pasteLayoutArea.Unlock();

                CurrentPage.TemporaryLayoutAreaList.Add(pasteLayoutArea);
            }

            else
            {
                // This creates a permenant layout area.

                VisioInterop.SetShapeData(pasteLayoutArea.Shape, "Layout Area", "Compound Shape[" + pasteLayoutArea.AreaFinishManager.AreaName + "]", pasteLayoutArea.Guid);

                // Add perimeters to associated seam mode layers //

                foreach (CanvasDirectedLine externPerimeterLine in pasteLayoutArea.ExternalArea)
                {
                    externPerimeterLine.LineFinishManager.SeamDesignStateLayer.AddShapeToLayer(externPerimeterLine, 1);
                }


                foreach (CanvasDirectedPolygon internalArea in pasteLayoutArea.InternalAreas)
                {
                    foreach (CanvasDirectedLine internalAreaLine in internalArea)
                    {
                        internalAreaLine.LineFinishManager.SeamDesignStateLayer.AddShapeToLayer(internalAreaLine, 1);
                    }
                }

                VisioInterop.SetBaseLineStyle(pasteLayoutArea.Shape, 0);

                pasteLayoutArea.AddAssociatedLines(Window, Page, SelectedLineType.LineName);

                pasteLayoutArea.AreaFinishManager.AddNormalLayoutArea(pasteLayoutArea);

                pasteLayoutArea.SetLineGraphicsForAreaDesignState(AreaShapeBuildStatus.Completed);

                CurrentPage.AddLayoutArea(pasteLayoutArea);


                pasteLayoutArea.ExternalArea.SetLineGraphicsForBorderArea();

                pasteLayoutArea.ExternalArea.Shape.SetLineOpacity(0);

                pasteLayoutArea.Lock();

            }

        }

        //public void ProcessEditAreaModeActionChangeShapeToSelected(CanvasLayoutArea canvasLayoutArea)
        //{
        //    ProcessEditAreaModeActionChangeShapeToFinish(canvasLayoutArea, FinishGlobals.SelectedAreaFinish);
        //}

        public void ProcessEditAreaModeActionChangeShapeToFinish(CanvasLayoutArea canvasLayoutArea, AreaFinishManager areaFinishManager)
        {
            MovedAreaShapes.Clear();
            RemovedAreaShapes.Clear();

            MovedAreaShapes.Add(canvasLayoutArea);

            CanvasManagerGlobals.AreaFinishManagerList.MoveCanvasLayoutAreaToFinishType(canvasLayoutArea, areaFinishManager);

            canvasLayoutArea.SetAreaDesignStateFormat(/*AreaMode.Layout,*/ true);

            canvasLayoutArea.AreaFinishManager.SetDesignStateFormat(DesignState.Area, true);
        }

        public void ProcessEditAreaModeActionDeleteShape(CanvasLayoutArea canvasLayoutArea, bool IsTopLevel = true)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { canvasLayoutArea });
#endif
            try
            {
                AreaFinishManager areaFinishManager = canvasLayoutArea.AreaFinishManager;

                if (Utilities.IsNotNull(CopySelectedLayoutArea))
                {
                    if (canvasLayoutArea == CopySelectedLayoutArea)
                    {
                        CopyMarker copyMarker = canvasLayoutArea.CopyMarker;

                        if (copyMarker != null)
                        {
                            Page.RemoveFromPageShapeDict(copyMarker);

                            canvasLayoutArea.AreaFinishManager.AreaDesignStateLayer.RemoveShapeFromLayer(copyMarker, 1);

                            canvasLayoutArea.CopyMarker.Delete();
                        }

                        CopySelectedLayoutArea = null;
                    }
                }

                CanvasLayoutArea parentLayoutArea = canvasLayoutArea.ParentArea;

                if (IsTopLevel)
                {
                    if (Utilities.IsNotNull(parentLayoutArea))
                    {
                        Tracer.TraceGen.TraceError("AreaEditManager:ProcessEditAreaModeActionDeleteShape error: unexpected non-null parent.", 1, true);

                        return;
                    }
                }

                MovedAreaShapes.Clear();
                RemovedAreaShapes.Clear();

                RemovedAreaShapes.Add(canvasLayoutArea);

                RemoveLayoutArea(canvasLayoutArea);

                foreach (CanvasLayoutArea offspringLayoutArea in canvasLayoutArea.OffspringAreas)
                {
                    ProcessEditAreaModeActionDeleteShape(offspringLayoutArea, false);
                }

                areaFinishManager.UpdateFinishStats();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("AreaEditManager:ProcessEditAreaModeActionDeleteShape throws an exception.", ex, 1, true);
            }
        }

#if false
        public void ProcessEditAreaApply(EditAreaMode editAreaMode)
        {
            List<CanvasLayoutArea> selectedAreas = CurrentPage.AreaDesignStateSelectedAreas();

            if (selectedAreas.Count <= 0)
            {
                // Nothing to work on. Note that here we do not clear the moved and removed area shapes. This is so that undo
                // will work even if the user has happened to click on the apply button twice in a row.

                return;
            }
            
            MovedAreaShapes.Clear();
            RemovedAreaShapes.Clear();

            HashSet<string> ImpactedAreaFinishes = new HashSet<string>();

            if (editAreaMode == EditAreaMode.ChangeShapesToSelected)
            {
                HashSet<int> normalAreaUsedSeamAreaIndices = CurrentPage.UsedSeamAreaIndices(areaPalette.SelectedFinish.Guid,  LayoutAreaType.Normal);
                HashSet<int> overagesAreaUsedSeamAreaIndices = CurrentPage.UsedSeamAreaIndices(areaPalette.SelectedFinish.Guid, LayoutAreaType.OversGenerator);
                HashSet<int> remnantAreaUsedSeamAreaIndices = CurrentPage.UsedSeamAreaIndices(areaPalette.SelectedFinish.Guid, LayoutAreaType.Remnant);

                foreach (CanvasLayoutArea canvasLayoutArea in CurrentPage.AreaDesignStateSelectedAreas())
                {
                    if (canvasLayoutArea.UCAreaFinish.Guid == areaPalette.SelectedFinish.Guid)
                    {
                        continue;
                    }

                    MovedAreaShapes.Add(canvasLayoutArea);

                    canvasLayoutArea.PrevUCAreaFinish = canvasLayoutArea.UCAreaFinish;

                    canvasLayoutArea.PrevSeamAreaIndex = canvasLayoutArea.SeamAreaIndex;

                    areaPalette.MoveCanvasLayoutAreaToSelectedFinishType(canvasLayoutArea, false);

                    canvasLayoutArea.SetAreaDesignStateFormat(/*AreaMode.Edit, */true);

                    CurrentPage.SetAreaDesignStateAreaSelectionStatus(canvasLayoutArea, false);
                }

                foreach (CanvasLayoutArea canvasLayoutArea in MovedAreaShapes)
                {

                    if (!canvasLayoutArea.SeamDesignStateSelectionModeSelected)
                    {
                        Debug.Assert(canvasLayoutArea.SeamIndexTag is null);

                        continue;
                    }

                    Debug.Assert(!(canvasLayoutArea.SeamIndexTag is null));

                    double x = canvasLayoutArea.SeamIndexTag.X;
                    double y = canvasLayoutArea.SeamIndexTag.Y;

                    canvasLayoutArea.SeamIndexTag.Delete();
                    canvasLayoutArea.SeamIndexTag = null;

                    HashSet<int> usedSeamAreaIndices = null;

                    if (canvasLayoutArea.LayoutAreaType == LayoutAreaType.Normal)
                    {
                        usedSeamAreaIndices = normalAreaUsedSeamAreaIndices;
                    }

                    else if (canvasLayoutArea.LayoutAreaType == LayoutAreaType.OversGenerator)
                    {
                        usedSeamAreaIndices = overagesAreaUsedSeamAreaIndices;
                    }

                    else if (canvasLayoutArea.LayoutAreaType == LayoutAreaType.Remnant)
                    {
                        usedSeamAreaIndices = remnantAreaUsedSeamAreaIndices;
                    }

                    canvasLayoutArea.SeamIndexTag = new CanvasSeamTag(Window, Page, x, y, CurrentPage.GetSeamAreaIndex(usedSeamAreaIndices), canvasLayoutArea.LayoutAreaType);

                    usedSeamAreaIndices.Add(canvasLayoutArea.SeamAreaIndex);


                    canvasLayoutArea.SeamIndexTag.Draw();

                }

                //BaseForm.btnEditAreaUndo.Enabled = true;
            }

            else if (editAreaMode == EditAreaMode.DeleteShapes)
            {
                foreach (CanvasLayoutArea canvasLayoutArea in CurrentPage.AreaDesignStateSelectedAreas())
                {
            
                    RemovedAreaShapes.Add(canvasLayoutArea);

                    RemoveLayoutArea(canvasLayoutArea);
                }

                //BaseForm.btnEditAreaUndo.Enabled = true;
            }

            else if (editAreaMode == EditAreaMode.EditShapes)
            {
                MessageBox.Show("Edit mode not implemented");
            }

            ProcessEditAreaClear();

#if DEBUG
            BaseForm.UpdateDebugForm();
#endif

            
        }
#endif
        public void ProcessEditAreaUndo()
        {
            //this.AreaDesignStateSelectedAreaDict.Clear();

            foreach (CanvasLayoutArea canvasLayoutArea in RemovedAreaShapes)
            {
                HashSet<int> usedSeamAreaIndicies = CurrentPage.UsedSeamAreaIndices(canvasLayoutArea.AreaFinishManager.Guid, canvasLayoutArea.LayoutAreaType);
               
                canvasLayoutArea.AddBackToCanvas();

                canvasLayoutArea.SetAreaDesignStateFormat(/*AreaMode.Edit, */true);

                if (!canvasLayoutArea.SeamDesignStateSelectionModeSelected)
                {
                    Debug.Assert(canvasLayoutArea.SeamIndexTag is null);

                    continue;
                }

                Debug.Assert(!(canvasLayoutArea.SeamIndexTag is null));

                // The following assures that the original location of the index tag on the canvas is
                // maintained for the new index tag.

                double x = canvasLayoutArea.SeamIndexTag.X;
                double y = canvasLayoutArea.SeamIndexTag.Y;

                canvasLayoutArea.SeamIndexTag.Delete();
                canvasLayoutArea.SeamIndexTag = null;

                canvasLayoutArea.SeamIndexTag = new CanvasSeamTag(Window, Page, x, y, CurrentPage.GetSeamAreaIndex(usedSeamAreaIndicies), canvasLayoutArea.LayoutAreaType);

                canvasLayoutArea.SeamIndexTag.Draw();

                //if (BaseForm.ckbEditAreaHighlightAndApply.Checked)
                //{
                //    CurrentPage.UpdateAreaDesignStateAreaSelectionStatus(canvasLayoutArea);
                //}
            }

            foreach (CanvasLayoutArea canvasLayoutArea in MovedAreaShapes)
            {
                if (!canvasLayoutArea.SeamDesignStateSelectionModeSelected)
                {
                    Debug.Assert(canvasLayoutArea.SeamIndexTag is null);

                    continue;
                }

                Debug.Assert(!(canvasLayoutArea.SeamIndexTag is null));

                if (canvasLayoutArea.SeamDesignStateSelectionModeSelected)
                {
                    HashSet<int> usedSeamAreaIndicies = CurrentPage.UsedSeamAreaIndices(canvasLayoutArea.PrevAreaFinishManager.Guid, canvasLayoutArea.LayoutAreaType);

                    double x = canvasLayoutArea.SeamIndexTag.X;
                    double y = canvasLayoutArea.SeamIndexTag.Y;

                    canvasLayoutArea.SeamIndexTag.Delete();
                    canvasLayoutArea.SeamIndexTag = null;

                    canvasLayoutArea.SeamIndexTag = new CanvasSeamTag(Window, Page, x, y, CurrentPage.GetSeamAreaIndex(usedSeamAreaIndicies),canvasLayoutArea.LayoutAreaType);

                    canvasLayoutArea.SeamIndexTag.Draw();


                    //canvasLayoutArea.AddSeamIndexTag();
                }

                AreaFinishManagerList.MoveCanvasLayoutAreaToFinishType(canvasLayoutArea, canvasLayoutArea.PrevAreaFinishManager);

                canvasLayoutArea.SetAreaDesignStateFormat(/*AreaMode.Edit, */ true);

                canvasLayoutArea.PrevAreaFinishManager = canvasLayoutArea.AreaFinishManager;

                //if (BaseForm.ckbEditAreaHighlightAndApply.Checked)
                //{
                //    CurrentPage.UpdateAreaDesignStateAreaSelectionStatus(canvasLayoutArea);
                //}
            }

            RemovedAreaShapes.Clear();
            MovedAreaShapes.Clear();

            //BaseForm.btnEditAreaUndo.Enabled = false;

#if DEBUG
            BaseForm.UpdateDebugForm();
#endif
        }

        //public void ProcessEditAreaClear()
        //{
        //    foreach (IAreaShape areaShape in CurrentPage.AreaDesignStateSelectedAreas())
        //    {
        //        CanvasLayoutArea canvasLayoutArea = (CanvasLayoutArea)areaShape;

        //        canvasLayoutArea.SetFillColor(areaShape.UCAreaFinish.visioFillColorFormula);
        //        canvasLayoutArea.SetFillTransparancy(areaShape.UCAreaFinish.visioFillTransparencyFormula);
        //    }

        //    //AreaDesignStateSelectedAreaDict.Clear();
        //}
    }
}
