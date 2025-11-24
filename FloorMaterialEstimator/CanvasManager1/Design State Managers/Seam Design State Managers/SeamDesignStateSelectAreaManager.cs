//-------------------------------------------------------------------------------//
// <copyright file="SeamDesignStateSelectAreaManager.cs"                         //
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
    using System.Windows.Forms;
    using System.Collections.Generic;
    using Graphics;
    using MaterialsLayout;
    using Utilities;
    using System.Drawing;
    using CanvasLib.SeamingTool;
    using Globals;
    using Geometry;
    using FinishesLib;
    using MaterialsLayout.MaterialsLayout;

    public partial class CanvasManager
    {
        private CanvasLib.SeamingTool.SeamingTool _seamingTool = null;

        public CanvasLib.SeamingTool.SeamingTool SeamingTool
        {
            get
            {
                if (_seamingTool is null)
                {
                    _seamingTool = new CanvasLib.SeamingTool.SeamingTool(Window, Page);
                }

                return _seamingTool;
            }
        }

        #region Context Menus

        #region Extended Crosshairs Context Menu

        private ContextMenu _extendedCrosshairsContextMenu = null;

        public ContextMenu ExtendedCrosshairsContextMenu
        {
            get
            {
                if (_extendedCrosshairsContextMenu is null)
                {
                    _extendedCrosshairsContextMenu = new ContextMenu();

                    MenuItem mi;

                    mi = new MenuItem("Create Cross Guides", ExtendedCrosshairsCreateCrossGuides_Click, Shortcut.CtrlC);
                    _extendedCrosshairsContextMenu.MenuItems.Add(mi);

                    mi = new MenuItem("Create Horizontal Guide", ExtendedCrosshairsCreateHorizontalGuide_Click, Shortcut.CtrlE);
                    _extendedCrosshairsContextMenu.MenuItems.Add(mi);

                    mi = new MenuItem("Create Vertical Guide", ExtendedCrosshairsCreateVerticalGuide_Click, Shortcut.CtrlM);
                    _extendedCrosshairsContextMenu.MenuItems.Add(mi);
                }

                return _extendedCrosshairsContextMenu;
            }
        }

        private void ExtendedCrosshairsCreateCrossGuides_Click(object sender, EventArgs e)
        {
            FieldGuideController.ProcessFieldGuideClick(MouseX, MouseY);
        }

        private void ExtendedCrosshairsCreateHorizontalGuide_Click(object sender, EventArgs e)
        {
            FieldGuideController.ProcessFieldGuideClick(-100000, MouseY);

        }

        private void ExtendedCrosshairsCreateVerticalGuide_Click(object sender, EventArgs e)
        {
            FieldGuideController.ProcessFieldGuideClick(MouseX, -100000);
        }

        #endregion

        #region Seaming Tool Context Menu

        private ContextMenu _seamingToolContextMenu = null;

        public ContextMenu SeamingToolContextMenu
        {
            get
            {
                if (_seamingToolContextMenu is null)
                {
                    _seamingToolContextMenu = new ContextMenu();

                    MenuItem mi;

                    mi = new MenuItem("Align to Boundary", SeamingToolAlign_Click, Shortcut.CtrlA);
                    _seamingToolContextMenu.MenuItems.Add(mi);

                    mi = new MenuItem("Expand", SeamingToolExpand_Click, Shortcut.CtrlE);
                    _seamingToolContextMenu.MenuItems.Add(mi);

                    mi = new MenuItem("Manual Seam", SeamingToolSeamSingleLineToTool_Click, Shortcut.CtrlM);
                    _seamingToolContextMenu.MenuItems.Add(mi);

                    mi = new MenuItem("Seam Area", SeamingToolAreaSeam_Click, Shortcut.CtrlS);
                    _seamingToolContextMenu.MenuItems.Add(mi);

                    mi = new MenuItem("Subdivide Region", SeamingToolSubdivideRegion_Click, Shortcut.CtrlD);
                    _seamingToolContextMenu.MenuItems.Add(mi);

                    mi = new MenuItem("Rotate 90 Degrees", SeamingToolRotate90Degrees_Click, Shortcut.CtrlR);
                    _seamingToolContextMenu.MenuItems.Add(mi);

                    mi = new MenuItem("Align Horizonal", SeamingToolAlignHorizontal_Click, Shortcut.CtrlH);
                    _seamingToolContextMenu.MenuItems.Add(mi);

                    mi = new MenuItem("Align Vertical", SeamingToolAlignVertical_Click, Shortcut.CtrlV);
                    _seamingToolContextMenu.MenuItems.Add(mi);

                    mi = new MenuItem("Show/Hide Tool", SeamingToolShowHide_Click, Shortcut.CtrlT);
                    _seamingToolContextMenu.MenuItems.Add(mi);
                }

                return _seamingToolContextMenu;
            }
        }

        #endregion

        private ContextMenu _basicSeamSetHideable = null;

        public ContextMenu BasicSeamSetHideable
        {
            get
            {
                if (_basicSeamSetHideable is null)
                {
                    _basicSeamSetHideable = new ContextMenu();

                    _basicSeamSetHideable.MenuItems.Add("Make Hideable", BasicSeamMakeHideable_Click);
                    //_basicSeamSetHideable.MenuItems.Add("Subdivide", BasicSeamSubdivide_Click);
                }

                return _basicSeamSetHideable;
            }
        }


        private ContextMenu _basicSeamSetUnhideable = null;

        public ContextMenu BasicSeamSetUnhideable
        {
            get
            {
                if (_basicSeamSetUnhideable is null)
                {
                    _basicSeamSetUnhideable = new ContextMenu();

                    _basicSeamSetUnhideable.MenuItems.Add("Make Basic", BasicSeamMakeUnhideable_Click);
                }

                return _basicSeamSetUnhideable;
            }
        }

        //---------------------------------------------------------------------------------------------------------------//
        // The following context menu is no longer needed because the hideable manual seams are no longer allowed. Thus  //
        // to delete a manual seam, the new protocol is to alt-click on the seam.                                        //
        //---------------------------------------------------------------------------------------------------------------//

#if false
        private ContextMenu _manualSeamSetHideable = null;

        public ContextMenu ManualSeamSetHideable
        {
            get
            {
                if (_manualSeamSetHideable is null)
                {
                    _manualSeamSetHideable = new ContextMenu();

   
                    _manualSeamSetHideable.MenuItems.Add("Delete", ManualSeamDelete_Click);
                }

                return _manualSeamSetHideable;
            }
        }

        private ContextMenu _manualSeamSetUnhideable = null;

        public ContextMenu ManualSeamSetUnhideable
        {
            get
            {
                if (_manualSeamSetUnhideable is null)
                {
                    _manualSeamSetUnhideable = new ContextMenu();

                    _manualSeamSetUnhideable.MenuItems.Add("Make Basic", ManualSeamMakeUnhideable_Click);
                    _manualSeamSetUnhideable.MenuItems.Add("Delete", ManualSeamDelete_Click);
                }

                return _manualSeamSetUnhideable;
            }
        }

#endif

        #endregion

        public CanvasDirectedLine SelectedLine { get; set; } = null;

        public CanvasLayoutArea SelectedLayoutArea { get; set; } = null;

        private void processSeamDesignStateSelectAreaClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            if (button == 1)
            {
                processSeamDesignStateSelectAreaLeftClick(x, y, ref cancelDefault);
            }

            else if (button == 2)
            {
                processSeamDesignStateSelectAreaRghtClick(x, y, ref cancelDefault);
            }
        }

        private void processSeamDesignStateSelectAreaLeftClick(double x, double y, ref bool cancelDefault)
        {
            if (!KeyboardUtils.CntlKeyPressed)
            {
                processSeamDesignStateSelectAreaLeftBaseClick(x, y, ref cancelDefault);
            }

            else
            {
                processSeamDesignStateSelectAreaLeftCntlClick(x, y, ref cancelDefault);
            }
        }

        private void processSeamDesignStateSelectAreaLeftBaseClick(double x, double y, ref bool cancelDefault)
        {
         
            List<CanvasLayoutArea> selectedAreaList = CurrentPage.GetSelectedAreaShapeList(x, y, true);

            if (selectedAreaList == null)
            {
                return;
            }

            if (selectedAreaList.Count <= 0)
            {
                return;
            }

            CanvasLayoutArea selectedLayoutArea = null;

            foreach (CanvasLayoutArea layoutArea in selectedAreaList)
            {
                if (layoutArea.LayoutAreaType != LayoutAreaType.Normal && layoutArea.LayoutAreaType != LayoutAreaType.OversGenerator)
                {
                    continue;
                }


                if (layoutArea.AreaFinishManager.Guid != FinishGlobals.SelectedAreaFinish.Guid)
                {
                    FinishGlobals.AreaFinishBaseList.SelectElem(layoutArea.AreaFinishBase);
                    return;
                }

                if (layoutArea.IsSubdivided())
                {
                    continue;
                }

                selectedLayoutArea = layoutArea;

                break;
            }

            if (selectedLayoutArea is null)
            {
                return;
            }

            if (selectedLayoutArea.AreaFinishBase.MaterialsType == FinishesLib.MaterialsType.Tiles)
            {
                MessageBox.Show("The finish type for this object is tiled and therefore cannot be selected for seaming.");
                return;
            }

            FloorMaterialEstimator.DebugSupportRoutines.CheckForNullInPageShapeDict(Page, 1);

            CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(selectedLayoutArea, FinishGlobals.SelectedAreaFinish.Guid, x, y);

            FloorMaterialEstimator.DebugSupportRoutines.CheckForNullInPageShapeDict(Page, 2);

            if (selectedLayoutArea.SelectedForRemnantAnalysis && !selectedLayoutArea.SeamDesignStateSelectionModeSelected)
            {
                layoutAreaSelectedForRemnantAnalysis = null;
                selectedLayoutArea.SelectedForRemnantAnalysis = false;
            }

            SeamingTool.Select();

            selectedLayoutArea.AreaFinishManager.UpdateFinishStats();

            CurrentPage.UpdateSeamTotals();

            UpdateAreaSeamsUndrsOversDataDisplay();

            if (CanvasManagerGlobals.SelectedAreaFinishManager.MaterialsType == MaterialsType.Rolls)
            {
                StaticGlobals.OversUndersFormUpdate(true);
            }
        }

        private void processSeamDesignStateSelectAreaLeftCntlClick(double x, double y, ref bool cancelDefault)
        {
            Dictionary<string, CanvasSeam> seamDict = CurrentPage.GetSeamDictionary(CanvasManagerGlobals.SelectedAreaFinishManager);

            if (seamDict.Count <= 0)
            {
                return;
            }

            List<CanvasSeam> selectedSeamList = CurrentPage.GetSelectedSeamList(x, y, seamDict);

            if (selectedSeamList == null)
            {
                deselectAllSeams(seamDict);

                return;
            }

            if (selectedSeamList.Count <= 0)
            {
                deselectAllSeams(seamDict);

                return;
            }

            bool itemDeselected = false;

            CanvasSeam focusedCanvasSeam = null;

            foreach (CanvasSeam canvasSeam in seamDict.Values)
            {
                if (selectedSeamList.Contains(canvasSeam))
                {
                    focusedCanvasSeam = canvasSeam;

                    if (canvasSeam.Selected)
                    {
                        canvasSeam.Deselect();

                        itemDeselected = true;

                        break;
                    }

                    else
                    {
                        canvasSeam.Select();

                        break;
                    }
                }
            }

            if (itemDeselected)
            {
                return;
            }

            if (KeyboardUtils.ShiftKeyPressed)
            {
                return;
            }

            foreach (CanvasSeam canvasSeam in seamDict.Values)
            {
                if (canvasSeam == focusedCanvasSeam)
                {
                    continue;
                }
             
                canvasSeam.Deselect();
            }
          
        }

        private void deselectAllSeams(Dictionary<string, CanvasSeam> seamDict)
        {
            foreach (CanvasSeam canvasSeam in seamDict.Values)
            {
                if (canvasSeam.Selected)
                {
                    canvasSeam.Deselect();
                }
            }
        }

        private void processSeamDesignStateSelectAreaRghtClick(double x, double y, ref bool cancelDefault)
        {
            if (processSeamDesignStateProcessSeamClick(x, y))
            {
                return;
            }

            if (processSeamDesignStateProcessLineClick(x, y))
            {
                return;
            }

        }

        private bool processSeamDesignStateProcessSeamClick(double x, double y)
        {
           
            Dictionary<string, CanvasSeam> canvasSeamDict = CurrentPage.GetSeamDictionary(CanvasManagerGlobals.SelectedAreaFinishManager);

            if (canvasSeamDict is null)
            {
                return false;
            }

            if (canvasSeamDict.Count <= 0)
            {
                return false;
            }

            List<CanvasSeam> selectedCanvasSeamList = CurrentPage.GetSelectedSeamList(x, y, canvasSeamDict);
            
            if (selectedCanvasSeamList is null)
            {
                return false;
            }
           
            if (selectedCanvasSeamList.Count <= 0)
            {
                return false;
            }

            Point cursorPosition;

            foreach (CanvasSeam canvasSeam in selectedCanvasSeamList)
            {
                selectedCanvasSeam = canvasSeam;

                Point baseCursorPosition = StaticGlobals.GetCursorPosition();

                Point loclCursorPosition = this.BaseForm.PointToClient(baseCursorPosition);

                cursorPosition = loclCursorPosition;

                if (canvasSeam.SeamType == SeamType.Manual)
                {
                    if (KeyboardUtils.AltKeyPressed)
                    {
                        ManualSeamDelete_Click(null, null);

                        return true;
                    }

                    return false;

                    //if (selectedCanvasSeamList.Count == 1)
                    //{
                    //    if (canvasSeam.IsHideable)
                    //    {
                    //        ManualSeamSetUnhideable.Show(BaseForm, loclCursorPosition);
                    //    }

                    //    else
                    //    {
                    //        ManualSeamSetHideable.Show(BaseForm, loclCursorPosition);
                    //    }

                    //    return true;
                    //}
                }

                else if (canvasSeam.SeamType == SeamType.Basic)
                {
                    if (canvasSeam.IsHideable)
                    {
                        BasicSeamSetUnhideable.Show(BaseForm, loclCursorPosition);
                    }

                    else
                    {
                       BasicSeamSetHideable.Show(BaseForm, loclCursorPosition);
                    }
                }

                //else if (canvasSeam.SeamType == SeamType.Manual)
                //{
                //    Manual.Show(BaseForm, loclCursorPosition);

                //    if (canvasSeam.IsHideable)
                //    {
                //        ManualSeamSetUnhideable.Show(BaseForm, loclCursorPosition);
                //    }

                //    else
                //    {
                //        ManualSeamSetHideable.Show(BaseForm, loclCursorPosition);
                //    }
                //}

            }

            return true;
        }

        private CanvasSeam selectedCanvasSeam = null;


        private void BasicSeamSubdivide_Click(object sender, EventArgs e)
        {
            if (selectedCanvasSeam is null)
            {
                return;
            }

            CanvasLayoutArea layoutAreaForSubdivision = selectedCanvasSeam.layoutArea;

            if (layoutAreaForSubdivision is null)
            {
                return;
            }

            subdivideByLine(layoutAreaForSubdivision, selectedCanvasSeam.GraphicsSeam.Seam);

            foreach (CanvasLayoutArea offspringLayoutArea in layoutAreaForSubdivision.OffspringAreas)
            {
                Coordinate location = offspringLayoutArea.Shape.CenterPoint;

                if (!offspringLayoutArea.SeamDesignStateSelectionModeSelected)
                {
                    CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(offspringLayoutArea, FinishGlobals.SelectedAreaFinish.Guid, location.X, location.Y);
                }

                seamArea(offspringLayoutArea, selectedCanvasSeam.GraphicsSeam.Seam);
            }

            if (SeamingTool.IsVisible)
            {
                VisioInterop.SelectShape(Window, SeamingTool.Shape);
            }
        }

        private void BasicSeamMakeHideable_Click(object sender, EventArgs e)
        {
            if (selectedCanvasSeam.SeamType != SeamType.Basic)
            {
                return;
            }

            if (selectedCanvasSeam.IsHideable)
            {
                return;
            }

            selectedCanvasSeam.IsHideable = true;

            CanvasLayoutArea canvasLayoutArea = selectedCanvasSeam.layoutArea;

            //canvasLayoutArea.NormalSeamsLayer.Remove(selectedCanvasSeam.GraphicsSeam.Shape, 0);

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.NormalSeamsUnhideableLayer.RemoveShapeFromLayer(selectedCanvasSeam.GraphicsSeam.Shape, 0);

            selectedCanvasSeam.layoutArea.AreaFinishManager.SetupAllSeamLayers();
                //BaseForm.RbnSeamModeAutoSeamsShowAll.Checked
                //, BaseForm.RbnSeamModeAutoSeamsShowUnHideable.Checked
                //, BaseForm.RbnSeamModeManualSeamsShowAll.Checked
                ////, BaseForm.rbnManualSeamsShowUnhideable.Checked // Hideable manual seams are removed from the system. 2022-02-11 MDD
                //, BaseForm.ckbShowSeamModeCuts.Checked
                //, BaseForm.ckbShowSeamModeCutIndices.Checked
                //, BaseForm.ckbShowSeamModeOvers.Checked
                //, BaseForm.ckbShowSeamModeUndrs.Checked
                //, BaseForm.ckbShowEmbeddedCuts.Checked
                //, BaseForm.ckbEmbeddedOvers.Checked
                //);

        }

        private void BasicSeamMakeUnhideable_Click(object sender, EventArgs e)
        {
            if (selectedCanvasSeam.SeamType != SeamType.Basic)
            {
                return;
            }

            if (!selectedCanvasSeam.IsHideable)
            {
                return;
            }

            selectedCanvasSeam.IsHideable = false;

            CanvasLayoutArea canvasLayoutArea = selectedCanvasSeam.layoutArea;

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.NormalSeamsUnhideableLayer.RemoveShapeFromLayer(selectedCanvasSeam.GraphicsSeam.Shape, 0);

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.NormalSeamsLayer.AddShapeToLayer(selectedCanvasSeam.GraphicsSeam.Shape, 0);

            selectedCanvasSeam.layoutArea.AreaFinishManager.SetupAllSeamLayers();
                //BaseForm.RbnSeamModeAutoSeamsShowAll.Checked
                //, BaseForm.RbnSeamModeAutoSeamsShowUnHideable.Checked
                //, BaseForm.RbnSeamModeManualSeamsShowAll.Checked
                ////, BaseForm.rbnManualSeamsShowUnhideable.Checked  // Hideable manual seams are removed from the system. 2022-02-11 MDD
                //, BaseForm.ckbShowSeamModeCuts.Checked
                //, BaseForm.ckbShowSeamModeCutIndices.Checked
                //, BaseForm.ckbShowSeamModeOvers.Checked
                //, BaseForm.ckbShowSeamModeUndrs.Checked
                //, BaseForm.ckbShowEmbeddedCuts.Checked
                //, BaseForm.ckbEmbeddedOvers.Checked
                //);
            
        }

        private void ManualSeamDelete_Click(object sender, EventArgs e)
        {
            if (selectedCanvasSeam.SeamType != SeamType.Manual)
            {
                return;
            }

            CanvasLayoutArea canvasLayoutArea = selectedCanvasSeam.layoutArea;

            canvasLayoutArea.CanvasSeamList.RemoveBase(selectedCanvasSeam);
            canvasLayoutArea.GraphicsSeamList.RemoveBase(selectedCanvasSeam.GraphicsSeam);
            canvasLayoutArea.SeamList.RemoveBase(selectedCanvasSeam.GraphicsSeam.Seam);

            selectedCanvasSeam.Delete();

            CurrentPage.UpdateSeamTotals();
        }

        public GraphicsCutIndex SelectedCutIndex { get; set; } = null;
        public GraphicsOverageIndex SelectedOverageIndex { get; set; } = null;
        public GraphicsUndrageIndex SelectedUndrageIndex { get; set; } = null;

        private bool processCutOverAndUnderIndexSelect(double x, double y)
        {
            if (BaseForm.ckbShowSeamModeCuts.Checked)
            {
                if (Utilities.IsNotNull(SelectedCutIndex))
                {
                    SelectedCutIndex.Location = new Coordinate(x, y);

                    SelectedCutIndex.Shape.CenterPoint = SelectedCutIndex.Location;

                    SelectedCutIndex.Deselect();

                    SelectedCutIndex = null;

                    return true;
                }
            }

            if (BaseForm.ckbShowSeamModeOvers.Checked)
            {
                if (Utilities.IsNotNull(SelectedOverageIndex))
                {
                    SelectedOverageIndex.Location = new Coordinate(x, y);

                    SelectedOverageIndex.Shape.CenterPoint = SelectedOverageIndex.Location;

                    SelectedOverageIndex.Deselect();

                    SelectedOverageIndex = null;

                    return true;
                }
            }

            if (BaseForm.ckbShowSeamModeUndrs.Checked)
            {
                if (Utilities.IsNotNull(SelectedUndrageIndex))
                {
                    SelectedUndrageIndex.Location = new Coordinate(x, y);

                    SelectedUndrageIndex.Shape.CenterPoint = SelectedUndrageIndex.Location;

                    SelectedUndrageIndex.Deselect();

                    SelectedUndrageIndex = null;

                    return true;
                }
            }

            IGraphicsShape iGraphicsShape = CurrentPage.GetSelectedCutOverOrUnderIndexShape(x,y);

            if (iGraphicsShape is null)
            {
                return false;
            }

            switch (iGraphicsShape.ShapeType)
            {
                case ShapeType.CutIndex:

                    if (BaseForm.ckbShowSeamModeCuts.Checked)
                    {
                        SelectedCutIndex = (GraphicsCutIndex)iGraphicsShape;
                        SelectedCutIndex.Select();
                        return true;
                    }

                    break;

                case ShapeType.OverageIndex:

                    if (BaseForm.ckbShowSeamModeOvers.Checked)
                    {
                        SelectedOverageIndex = (GraphicsOverageIndex)iGraphicsShape;
                        SelectedOverageIndex.Select();
                        return true;
                    }

                    break;

                case ShapeType.UndrageIndex:

                    if (BaseForm.ckbShowSeamModeUndrs.Checked)
                    {
                        SelectedUndrageIndex = (GraphicsUndrageIndex)iGraphicsShape;
                        SelectedUndrageIndex.Select();
                        return true;
                    }

                    break;
            }

            return false;
        }

        public void processCutOverAndUnderIndexDeselect()
        {
            if (Utilities.IsNotNull(SelectedCutIndex))
            {
                SelectedCutIndex.Deselect();

                SelectedCutIndex = null;

            }

            if (Utilities.IsNotNull(SelectedOverageIndex))
            {
                SelectedOverageIndex.Deselect();

                SelectedOverageIndex = null;
            }

            if (Utilities.IsNotNull(SelectedUndrageIndex))
            {

                SelectedUndrageIndex.Deselect();

                SelectedUndrageIndex = null;
            }
        }

        private bool processSeamDesignStateProcessLineClick(double x, double y)
        {

            CanvasLayoutArea selectedLayoutArea = null;

            CanvasDirectedLine selectedLine = null;

            if (Utilities.IsNotNull(SelectedLine))
            {
                selectedLine = SelectedLine;

                selectedLayoutArea = SelectedLine.ParentLayoutArea;

                if (!selectedLayoutArea.SeamDesignStateSelectionModeSelected)
                {
                    Coordinate centroid = selectedLayoutArea.ExternalArea.Centroid();

                    CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(
                        selectedLayoutArea
                        , FinishGlobals.SelectedAreaFinish.Guid
                        , centroid.X, centroid.Y);

                }
            }

            else
            {
                List<CanvasDirectedLine> initialLineList = CurrentPage.GetSelectedLineShapeList(x, y);

                if (initialLineList is null)
                {
                    MessageBox.Show("You are in the seaming design state. Please click close to a base-line for seaming.");
                    return true;
                }

                List<CanvasLayoutArea> initialAreaList = CurrentPage.GetSelectedAreaShapeList(x, y, true);

                List<CanvasDirectedLine> lineList = new List<CanvasDirectedLine>();
                List<CanvasLayoutArea> areaList = new List<CanvasLayoutArea>();

                foreach (CanvasDirectedLine canvasDirectedLine in initialLineList)
                {
                    if (!canvasDirectedLine.IsValidLineForSeamSelection())
                    {
                        continue;
                    }

                    lineList.Add(canvasDirectedLine);
                }


                if (lineList.Count <= 0)
                {
                    return false;
                }

                else if (lineList.Count == 1)
                {
                    selectedLine = lineList[0];

                    selectedLayoutArea = selectedLine.ParentLayoutArea;

                    if ((object)selectedLayoutArea == null)
                    {
                        return false;
                    }
                }

                else
                {
                    foreach (CanvasLayoutArea area in initialAreaList)
                    {
                        if (area.IsSubdivided())
                        {
                            continue;
                        }

                        areaList.Add(area);
                    }

                    foreach (CanvasDirectedLine line in lineList)
                    {
                        if (areaList.Contains(line.ParentLayoutArea))
                        {
                            selectedLine = line;

                            selectedLayoutArea = selectedLine.ParentLayoutArea;

                            break;
                        }
                    }

                    if (selectedLine == null)
                    {
                        selectedLine = lineList[0];

                        selectedLayoutArea = selectedLine.ParentLayoutArea;
                    }
                }
            }

            if (KeyboardUtils.AltKeyPressed)
            {
                createManualSeam(selectedLayoutArea, selectedLine);

                return true;
            }

            if (selectedLayoutArea.AreaFinishManager.RollWidthInInches <= 0.0)
            {
                MessageBox.Show("Roll width must be greater than 0.");
                return true;
            }

            if (!selectedLayoutArea.SeamDesignStateSelectionModeSelected)
            {
                MessageBox.Show("Seaming of an area requires that it be selected.");
                return true;
            }

            if (selectedLayoutArea.AreaFinishManager.AreaFinishBase.SeamFinishBase is null)
            {
                MessageBox.Show("Attempt to seam an area using a finish that has not had a seam type assigned to it.");
                return true;
            }

            selectedLayoutArea.GenerateSeamsAndRollouts(selectedLine);

            // This is a kludge to fix the case where entering this section, the seam index tag
            // does not exist.

            if (selectedLayoutArea.SeamIndexTag is null)
            {
                HashSet<int> usedSeamAreaIndicies = CurrentPage.UsedSeamAreaIndices(selectedLayoutArea.AreaFinishManager.Guid, selectedLayoutArea.LayoutAreaType);

                Coordinate tagLocation = selectedLayoutArea.VoronoiLabelLocation();

                selectedLayoutArea.SeamIndexTag
                    = new CanvasSeamTag(Window, Page, tagLocation.X, tagLocation.Y, CurrentPage.GetSeamAreaIndex(usedSeamAreaIndicies), selectedLayoutArea.LayoutAreaType);

                selectedLayoutArea.SeamIndexTag.Draw();
            }

            selectedLayoutArea.SeamIndexTag.UnderlineTag();

            //if (selectedLayoutArea.LayoutAreaType == LayoutAreaType.Normal)
            //{

                CurrentPage.UpdateSeamTotals();

                selectedLayoutArea.AreaFinishManager.UpdateFinishStats();

                if (CanvasManagerGlobals.SelectedAreaFinishManager.MaterialsType == MaterialsType.Rolls)
                {
                    BaseForm.OversUndersFormUpdate();
                    BaseForm.EnableOversUndersButton(true);

                }
            //}

#if false
            if (selectedLayoutArea.LayoutAreaType == LayoutAreaType.Remnant)
            {
                double totalAreaInInches = selectedLayoutArea.AreaInSqrInches();
                double remnantAreaInInches = 0;

                foreach (GraphicsCut graphicsCut in selectedLayoutArea.GraphicsCutList)
                {
 
                    foreach (GraphicsRemnantCut graphicsRemnantCut in graphicsCut.GraphicsRemnantCutList)
                    {
                        remnantAreaInInches += graphicsRemnantCut.AreaInInches;
                    }
                }

                BaseForm.ucRemnantsView.AddRemnantArea(
                    "R" + -selectedLayoutArea.SeamIndexTag.SeamAreaIndex
                    ,selectedLayoutArea.GraphicsCutList
                    ,remnantAreaInInches
                    , totalAreaInInches);

                //MessageBox.Show("Waste Factor: " + Math.Round(100.0 * (1.0 - (remnantAreaInInches / totalAreaInInches)), 1) + "%");
            }
#endif
            //------------------------------------------------------------------------------------------------------//
            // In the following call, we indicate NOT to draw manual seams. This is because at this point, the user //
            // has clicked on a boundary point with the intention of drawing regular seams. If we were to include   //
            // manual seams (which area already in the seam list) then the manual seams would be drawn twice.       //
            //------------------------------------------------------------------------------------------------------//

            selectedLayoutArea.DrawSeams(false);

            if (SeamingTool.IsVisible)
            {
                VisioInterop.SelectShape(this.Window, SeamingTool.Shape);
            }

            return true;

        }

        /// <summary>
        /// Responds to a seaming tool align click from the context menu
        /// This operation will align the seaming tool to the line over
        /// which it is centered.
        /// </summary>
        /// <param name="sender">Event sender. Ignored.</param>
        /// <param name="e">Event parameters. Ignored.</param>
        public void SeamingToolAlign_Click(object sender, EventArgs e)
        {
            Coordinate toolLocation = SeamingTool.GetLocation();

            List<CanvasDirectedLine> lineList = CurrentPage.GetSelectedLineShapeList(toolLocation);

            if (lineList.Count > 0)
            {
                CanvasDirectedLine guideLine = lineList[0];

                double theta = guideLine.Shape.ShapeAngle + Math.PI / 2.0;

                if (theta > Math.PI)
                {
                    theta -= Math.PI;
                }



                SeamingTool.SetAngle(theta);

                return;
            }

            Dictionary<string, CanvasSeam> seamDict = CurrentPage.GetSeamDictionary(CanvasManagerGlobals.SelectedAreaFinishManager);

            if (seamDict.Count > 0)
            {

                List<CanvasSeam> selectedSeamList = CurrentPage.GetSelectedSeamList(toolLocation, seamDict);

                if (selectedSeamList.Count > 0)
                {
                    CanvasSeam canvasSeam = selectedSeamList[0];

                    double theta = canvasSeam.GraphicsSeam.Shape.ShapeAngle + Math.PI / 2.0; ;

                    if (theta > Math.PI)
                    {
                        theta -= Math.PI;
                    }

                    SeamingTool.SetAngle(theta);

                    SeamingTool.Shape.ShapeAngle = theta;
                }

                return;
            }
        }

        /// <summary>
        /// Responds to a seaming tool expand click from the context menu.
        /// This operation will expand the seaming tool to the boundary of the
        /// layout area in which it is located.
        /// </summary>
        /// <param name="sender">Event sender. Ignored.</param>
        /// <param name="e">Event parameters. Ignored.</param>
        public void SeamingToolExpand_Click(object sender, EventArgs e)
        {
            Coordinate toolLocation = SeamingTool.GetLocation();

            CanvasLayoutArea currentLayoutArea = CurrentPage.GetContainingLayoutAreaMinusInternalArea(AreaFinishManagerList.SelectedAreaFinishManager.CanvasLayoutAreas, toolLocation);

            if (currentLayoutArea is null)
            {
                return;
            }

            SeamingTool.ExpandToPolygonBoundary(currentLayoutArea.ExternalArea);
        }


        public void SeamingToolSeamSingleLineToTool_Click(object sender, EventArgs e)
        {
            Coordinate toolLocation = VisioInterop.GetShapePinLocation(SeamingTool.Shape);

            CanvasLayoutArea currentLayoutArea = CurrentPage.GetContainingLayoutArea(toolLocation);

            if (currentLayoutArea is null)
            {
                return;
            }

            DirectedLine seamToolLine = SeamingTool.ToLine();

            createManualSeam(currentLayoutArea, seamToolLine);


            SeamingTool.Show();
        }

        private void createManualSeam(CanvasLayoutArea canvasLayoutArea, DirectedLine seamLine)
        {
            Seam seam = new Seam(seamLine, SeamType.Manual, false);

            GraphicsSeam graphicsSeam = new GraphicsSeam(Window, Page, seam);

            canvasLayoutArea.BaseSeamLineWall = (DirectedLine)graphicsSeam.Seam;

            CanvasSeam canvasSeam = new CanvasSeam(Window, CurrentPage, canvasLayoutArea, graphicsSeam);

            canvasSeam.SeamType = SeamType.Manual;

            canvasLayoutArea.CanvasSeamList.AddBase(canvasSeam);
            canvasLayoutArea.GraphicsSeamList.AddBase(canvasSeam.GraphicsSeam);
            canvasLayoutArea.SeamList.AddBase(canvasSeam.GraphicsSeam.Seam);

            SeamFinishBase seamFinishBase = canvasLayoutArea.AreaFinishManager.SeamFinishBase;

            canvasSeam.GraphicsSeam.Draw(seamFinishBase);

            VisioInterop.DeselectShape(Window, canvasSeam.GraphicsSeam.Shape);

            canvasSeam.GraphicsSeam.Shape.BringToFront();

            AreaFinishManager areaFinishManager = canvasLayoutArea.AreaFinishManager;

            areaFinishManager.SeamDesignStateLayer.AddShapeToLayer(canvasSeam.GraphicsSeam.Shape, 1);

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.ManualSeamsAllLayer.AddShapeToLayer(canvasSeam.GraphicsSeam.Shape, 1);

            //canvasLayoutArea.ManualSeamsUnhideableLayer.AddShape(canvasSeam.GraphicsSeam.Shape, 1);

            CurrentPage.UpdateSeamTotals();
        }

        public void SeamingToolAreaSeam_Click(object sender, EventArgs e)
        {
            Coordinate toolLocation = SeamingTool.GetLocation();

            CanvasLayoutArea currentLayoutArea = CurrentPage.GetContainingLayoutAreaMinusInternalArea(AreaFinishManagerList.SelectedAreaFinishManager.CanvasLayoutAreas, toolLocation);

            if (currentLayoutArea is null)
            {
                return;
            }

            if (!currentLayoutArea.SeamDesignStateSelectionModeSelected)
            {
                CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(currentLayoutArea, FinishGlobals.SelectedAreaFinish.Guid, toolLocation.X, toolLocation.Y);
            }

            DirectedLine seamLine = SeamingTool.ToLine();

            seamArea(currentLayoutArea, seamLine);

            currentLayoutArea.BaseSeamLineWall = seamLine;

            if (SeamingTool.IsVisible)
            {
                VisioInterop.SelectShape(Window, SeamingTool.Shape);
            }
        }

        public bool ShowSeamingToolOnSwitchToSeamMode = false;

        /// <summary>
        /// Responds to a click of the show/hide seaming tool button.
        /// </summary>
        /// <param name="sender">Event sender. Ignored.</param>
        /// <param name="e">Event parameters. Ignored.</param>
        public void BtnShowSeamingTool_Click(object sender, MouseEventArgs e)
        {
            if (BaseForm.btnShowSeamingTool.BackColor == Color.Orange)
            {
                HideSeamingTool();

                ShowSeamingToolOnSwitchToSeamMode = false;
            }

            else
            {
                this.BaseForm.ResetAutoSelectOption();

                int btn = 2;

                if (e != null) btn = (MouseButtons.Right == e.Button ? 2 : 1);

                Window.DeselectAll();

                ShowSeamingTool(btn);

                ShowSeamingToolOnSwitchToSeamMode = true;
            }
        }

        /// <summary>
        /// Sets up the system for being in show seaming tool mode.
        /// </summary>
        public void ShowSeamingTool(int btn = 2)
        {
            BaseForm.btnShowSeamingTool.Text = "Hide Tool";

            BaseForm.btnShowSeamingTool.BackColor = Color.Orange;

            BaseForm.btnAlignTool.Enabled = true;
            BaseForm.btnSeamSingleLineToTool.Enabled = true;
            BaseForm.btnSeamArea.Enabled = true;
            BaseForm.btnSubdivideRegion.Enabled = true;

            SeamingTool.Show();
        }

        /// <summary>
        /// Sets up the system for being in hide seaming tool mode.
        /// </summary>
        public void HideSeamingTool()
        {
            BaseForm.btnShowSeamingTool.Text = "Show Tool";

            BaseForm.btnShowSeamingTool.BackColor = SystemColors.ControlLightLight;

            BaseForm.btnAlignTool.Enabled = false;
            BaseForm.btnSeamSingleLineToTool.Enabled = false;
            BaseForm.btnSeamArea.Enabled = false;
            BaseForm.btnSubdivideRegion.Enabled = false;

            if (!(SeamingTool is null))
            {
                SeamingTool.Hide();
            }
        }

        /// <summary>
        /// Responds to a click of the center seaming tool in view button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnCenterSeamingToolInView_Click(object sender, EventArgs e)
        {
            if (!SeamingTool.IsVisible)
            {
                ShowSeamingTool();
            }
            //if (BaseForm.btnShowSeamingTool.BackColor != Color.Orange)
            //{
            //    BtnShowSeamingTool_Click(null, null);
            //}

            SeamingTool.CenterInView();
        }

        //*****************************************************************************

        // This is the entry point from seaming tool.

        public void SeamingToolSubdivideRegion_Click(object sender, EventArgs e)
        {
            Coordinate toolLocation = SeamingTool.GetLocation();

            LayoutAreaForSubdivision = CurrentPage.GetContainingLayoutArea(toolLocation);

            if (LayoutAreaForSubdivision is null)
            {
                return;
            }

            DirectedLine subdividingDirectedLine = SeamingTool.ToLine();

            BuildingPolyline = new CanvasDirectedPolyline(this);


            BuildingPolyline.AddPoint(subdividingDirectedLine.Coord1, true);
            BuildingPolyline.AddPoint(subdividingDirectedLine.Coord2, true);

            SystemState.DrawingMode = DrawingMode.Polyline;

            SystemState.DrawingShape = true;


            DirectedLine frstLine = buildingPolyline.FrstLine;
            DirectedLine lastLine = buildingPolyline.LastLine;

            if (!LayoutAreaForSubdivision.ExternalArea.Intersects(frstLine) || !LayoutAreaForSubdivision.ExternalArea.Intersects(lastLine))
            {

                ProcessSeamModeCancelShapeInProgress();

                MessageBox.Show("The first and last line must intersect the area boundary. Please expand the seaming tool to the boundary points before subdividing the region.");

                return;
            }

            ProcessSubdivisionPolylineByLine();

            //this.BaseForm.btnCompleteSubdivision_Click(null, null);

            //subdivideByLine(layoutAreaForSubdivision, subdividingLine);

#if false
            foreach (CanvasLayoutArea offspringLayoutArea in layoutAreaForSubdivision.OffspringAreas)
            {
                Coordinate location = VisioInterop.GetShapeLocation(offspringLayoutArea.Shape);

                if (!offspringLayoutArea.SeamDesignStateSelectionModeSelected)
                {
                    CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(offspringLayoutArea, FinishGlobals.SelectedAreaFinish.Guid, location.X, location.Y);
                }

                seamArea(offspringLayoutArea, subdividingLine);
            }
#endif
            SeamingTool.Show();

            //VisioInterop.SelectShape(Window, SeamingTool.Shape);

            VisioInterop.BringToFront(SeamingTool.Shape);
           

            //if (SeamingTool.IsVisible)
            //{
            //    VisioInterop.SelectShape(Window, SeamingTool.Shape);

            //    VisioInterop.BringToFront(SeamingTool.Shape);
            //}
        }

        public void SeamingToolRotate90Degrees_Click(object sender, EventArgs e)
        {
            SeamingTool.RotateRight90Degrees();
        }

        public void SeamingToolAlignVertical_Click(object sender, EventArgs e)
        {
            SeamingTool.AlignVertical();
        }

        public void SeamingToolAlignHorizontal_Click(object sender, EventArgs e)
        {
            SeamingTool.AlignHorizontal();
        }

        public void SeamingToolShowHide_Click(object sender, EventArgs e)
        {
            if (SeamingTool.IsVisible)
            {
                HideSeamingTool();
            }

            else
            {
                ShowSeamingTool();
            }
        }
        private void seamArea(CanvasLayoutArea currentLayoutArea, DirectedLine directedLine)
        {
            currentLayoutArea.GenerateSeamsAndRollouts(directedLine);

            currentLayoutArea.SeamIndexTag.UnderlineTag();

            CurrentPage.UpdateSeamTotals();

            currentLayoutArea.AreaFinishManager.UpdateFinishStats();

            //BaseForm.BtnOversUnders_Click(null, null);

            BaseForm.OversUndersFormUpdate();

            currentLayoutArea.DrawSeams();
        }

        public void ReseamAllAreas()
        {
            List<CanvasLayoutArea> canvasLayoutAreaList = new List<CanvasLayoutArea>();

            foreach (CanvasLayoutArea canvasLayoutArea in CurrentPage.LayoutAreas)
            {
                if (canvasLayoutArea.IsSeamed())
                {
                    canvasLayoutAreaList.Add(canvasLayoutArea);
                }
            }

            foreach (CanvasLayoutArea canvasLayoutArea in canvasLayoutAreaList)
            {
                canvasLayoutArea.RegenerateSeamsAndCuts();

                canvasLayoutArea.AreaFinishManager.UpdateFinishStats();

                canvasLayoutArea.DrawSeams();
            }

            if (canvasLayoutAreaList.Count > 0)
            {
                UpdateAreaSeamsUndrsOversDataDisplay();
            }
        }

        public void ClearAllSeams()
        {
            List<CanvasLayoutArea> canvasLayoutAreaList = new List<CanvasLayoutArea>();

            foreach (CanvasLayoutArea canvasLayoutArea in CurrentPage.LayoutAreas)
            {
                if (canvasLayoutArea.IsSeamed())
                {
                    canvasLayoutAreaList.Add(canvasLayoutArea);
                }
            }

            foreach (CanvasLayoutArea canvasLayoutArea in canvasLayoutAreaList)
            {
                canvasLayoutArea.DeleteRollouts();

                canvasLayoutArea.DeleteSeams();

                canvasLayoutArea.AreaFinishManager.UpdateFinishStats();
            }

            if (canvasLayoutAreaList.Count > 0)
            {
                UpdateAreaSeamsUndrsOversDataDisplay();
            }
        }

        private void subdivideByLine(CanvasLayoutArea layoutAreaForSubdivision, DirectedLine subdividingLine)
        {
            // The layout area is now legitimately selected for subdivision. Set here so downstream operations work correctly.

            layoutAreaForSubdivision.SeamDesignStateSubdivisionModeSelected = true;


            GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(Window, Page, subdividingLine, LineRole.Seam);

            CanvasDirectedLine canvasDirectedLine = new CanvasDirectedLine(this, null, graphicsDirectedLine, DesignState.Seam);

            CanvasDirectedPolyline canvasDirectedPolyline = new CanvasDirectedPolyline(this);

            canvasDirectedPolyline.Add(canvasDirectedLine);

            if (!processSubdivideByLine(layoutAreaForSubdivision, canvasDirectedPolyline))
            {
                SeamingTool.Select();
                
                return;
            }

            setSubdividedAreasSelectionStatus(layoutAreaForSubdivision);

            layoutAreaForSubdivision.RemoveFromCanvas();

            // Set up seamable lines in the perimeter
            foreach (CanvasLayoutArea offspringArea in layoutAreaForSubdivision.OffspringAreas)
            {
                setSeamableLines(offspringArea, subdividingLine);
            }

            canvasDirectedPolyline.Delete();

            SeamingTool.Select();
        }

        private void setSeamableLines(CanvasLayoutArea offspringArea, DirectedLine subdividingLine)
        {
            setSeamableLines(offspringArea.ExternalArea, subdividingLine);

            foreach (CanvasDirectedPolygon canvasDirectedPolygon in offspringArea.InternalAreas)
            {
                setSeamableLines(canvasDirectedPolygon, subdividingLine);
            }
        }


        private void setSeamableLines(CanvasDirectedPolygon canvasDirectedPolygon, DirectedLine subdividingLine)
        {
            foreach (CanvasDirectedLine canvasDirectedLine in canvasDirectedPolygon)
            {
                if (subdividingLine.Contains(canvasDirectedLine))
                {
                    canvasDirectedLine.IsSeamable = true;
                }
            }
        }

        // Note. Right now, seams cannot be selected individually, so the following always defaults
        // to checking if the seaming tool is visible.

        private void ProcessSeamStateArrowKey(int keyVal)
        {
            if (BaseForm.SeamMode != SeamMode.Selection)
            {
                return;
            }

            switch (keyVal)
            {
                case 0: moveSeamStateSelectedItemLeft(); break;
                case 1: moveSeamStateSelectedItemUp(); break;
                case 2: moveSeamStateSelectedItemRght(); break;
                case 3: moveSeamStateSelectedItemDown(); break;

                default: break;
            }

           // SeamingTool.Select();

        }

        private void moveSeamStateSelectedItemDown()
        {
            if (SeamingTool.IsSelected)
            {
                MoveSelectedShapeByIncrement(SeamingTool.Shape, 0, -1);

                return;
            }

            else if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, 0, -1);

                return;
            }
        }

        private void moveSeamStateSelectedItemLeft()
        {
            if (SeamingTool.IsSelected)
            {
                MoveSelectedShapeByIncrement(SeamingTool.Shape, -1, 0);
            }

            else if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, -1, 0);
            }

        }

        private void moveSeamStateSelectedItemUp()
        {
            if (SeamingTool.IsSelected)
            {
                MoveSelectedShapeByIncrement(SeamingTool.Shape, 0, 1);
            }

            else if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, 0, 1);
            }
        }

        private void moveSeamStateSelectedItemRght()
        {
            if (SeamingTool.IsSelected)
            {
                MoveSelectedShapeByIncrement(SeamingTool.Shape, 1, 0);
            }

            else if (MeasuringStick.IsSelected())
            {
                MoveSelectedShapeByIncrement(MeasuringStick.Shape, 1, 0);
            }
        }

    }
}
