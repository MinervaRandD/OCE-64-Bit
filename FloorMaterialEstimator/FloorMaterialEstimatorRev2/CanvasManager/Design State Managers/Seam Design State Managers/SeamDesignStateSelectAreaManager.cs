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
    using System.Runtime.Remoting.Channels;


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
                    _basicSeamSetHideable.MenuItems.Add("Create Display Seam", CreateDisplaySeam_Click);

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
                    _basicSeamSetUnhideable.MenuItems.Add("Create Display Seam", CreateDisplaySeam_Click);
                }

                return _basicSeamSetUnhideable;
            }
        }


        private ContextMenu _displaySeamOptionsMakeHideable = null;

        public ContextMenu DisplaySeamOptionsMakeHideable
        {
            get
            {
                if (_displaySeamOptionsMakeHideable is null)
                {
                    _displaySeamOptionsMakeHideable = new ContextMenu();

                    _displaySeamOptionsMakeHideable.MenuItems.Add("Make Display Seam Hideable", BasicSeamMakeHideable_Click);
                    _displaySeamOptionsMakeHideable.MenuItems.Add("Delete Display Seam", DeleteDisplaySeam_Click);
                }

                return _displaySeamOptionsMakeHideable;
            }
        }

        private ContextMenu _manualSeamsOptions = null;

        public ContextMenu ManualSeamsOptions
        {
            get
            {
                if (_manualSeamsOptions is null)
                {
                    _manualSeamsOptions = new ContextMenu();

                    _manualSeamsOptions.MenuItems.Add("Delete Manual Seam", DeleteManualSeam_Click);
            
                }

                return _manualSeamsOptions;
            }
        }

        private ContextMenu _displaySeamOptionsMakeBasic = null;

        public ContextMenu DisplaySeamOptionsMakeBasic
        {
            get
            {
                if (_displaySeamOptionsMakeBasic is null)
                {
                    _displaySeamOptionsMakeBasic = new ContextMenu();

                    _displaySeamOptionsMakeBasic.MenuItems.Add("Make Display Seam Basic", BasicSeamMakeUnhideable_Click);
                    _displaySeamOptionsMakeBasic.MenuItems.Add("Delete Display Seam", DeleteDisplaySeam_Click);
                }

                return _displaySeamOptionsMakeBasic;
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

        public CanvasDirectedLine SelectedLine
        {
            get;
            set;
        } = null;

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
            if (processSeamDesignStateProcessAreaLockClick(x, y))
            {
                return;
            }

            if (mouseOverDisplaySeam(x, y) != null) // MDD Reset This
            {
                return;
            }

            List<CanvasLayoutArea> selectedAreaList = CurrentPage.GetSelectedAreaShapeList(x, y, true);

            if (selectedAreaList == null)
            {
                return;
            }

            if (selectedAreaList.Count <= 0)
            {
                return;
            }

            List<CanvasLayoutArea> selectedLayoutAreaList = new List<CanvasLayoutArea>();

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

                selectedLayoutAreaList.Add (layoutArea);

            }

            if (selectedLayoutAreaList.Count <= 0)
            {
                return;
            }

            CanvasLayoutArea selectedLayoutArea = getPreferredLayoutArea(selectedLayoutAreaList);

            if (selectedLayoutArea == null)
            {
                return;
            }

            if (selectedLayoutArea.AreaFinishBase.MaterialsType == FinishesLib.MaterialsType.Tiles)
            {
                ManagedMessageBox.Show("The finish type for this object is tiled and therefore cannot be selected for seaming.");
                return;
            }

            FloorMaterialEstimator.DebugSupportRoutines.CheckForNullInPageShapeDict(Page, 1);

            CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(selectedLayoutArea, FinishGlobals.SelectedAreaFinish.Guid);

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

            if (FinishManagerGlobals.SelectedAreaFinishManager.MaterialsType == MaterialsType.Rolls)
            {
                SystemGlobals.OversUndersFormUpdate(true);
            }
        }

        private CanvasLayoutArea getPreferredLayoutArea(List<CanvasLayoutArea> selectedLayoutAreaList)
        {
            if (selectedLayoutAreaList.Count == 0) { return null; }

            if (selectedLayoutAreaList.Count == 1)
            {
                return selectedLayoutAreaList[0];
            }

            List<CanvasLayoutArea> rollCurrentLayoutList = null;

            List<CanvasLayoutArea> tileCurrentLayoutList = null;

            List<CanvasLayoutArea> rollNonCurrentLayoutList = null;

            List<CanvasLayoutArea> tileNonCurrentLayoutList = null;

            var selectedAreaFinishBase = BaseForm.selectedAreaFinish.AreaFinishBase;

            foreach (var layoutArea in selectedLayoutAreaList)
            {
                var areaFinishBase = layoutArea.AreaFinishBase;

                if (areaFinishBase == selectedAreaFinishBase)
                {
                    if (areaFinishBase.MaterialsType == MaterialsType.Rolls)
                    {
                        if (rollCurrentLayoutList == null)
                        {
                            rollCurrentLayoutList = new List<CanvasLayoutArea>();
                        }

                        rollCurrentLayoutList.Add(layoutArea);
                    }

                    else if (areaFinishBase.MaterialsType == MaterialsType.Tiles)
                    {
                        if (tileCurrentLayoutList == null)
                        {
                            tileCurrentLayoutList = new List<CanvasLayoutArea>();
                        }

                        tileCurrentLayoutList.Add(layoutArea);
                    }
                }

                else
                {
                    if (areaFinishBase.MaterialsType == MaterialsType.Rolls)
                    {
                        if (rollNonCurrentLayoutList == null)
                        {
                            rollNonCurrentLayoutList = new List<CanvasLayoutArea>();
                        }

                        rollNonCurrentLayoutList.Add(layoutArea);
                    }

                    else if (areaFinishBase.MaterialsType == MaterialsType.Tiles)
                    {
                        if (tileNonCurrentLayoutList == null)
                        {
                            tileNonCurrentLayoutList = new List<CanvasLayoutArea>();
                        }

                        tileNonCurrentLayoutList.Add(layoutArea);
                    }
                }

                if (rollCurrentLayoutList != null)
                {
                    return rollCurrentLayoutList[0]; // arbitrarily choose the first one.
                }

                if (tileCurrentLayoutList != null)
                {
                    return tileCurrentLayoutList[0];
                }

                if (rollNonCurrentLayoutList != null)
                {
                    return rollNonCurrentLayoutList[0];
                }

                if (tileNonCurrentLayoutList != null)
                {
                    return tileNonCurrentLayoutList[0];
                }
            }

            if (rollCurrentLayoutList != null)
            {
                return rollCurrentLayoutList[0]; // arbitrarily choose the first one.
            }

            if (tileCurrentLayoutList != null)
            {
                return tileCurrentLayoutList[0];
            }

            if (rollNonCurrentLayoutList != null)
            {
                return rollNonCurrentLayoutList[0];
            }

            if (tileNonCurrentLayoutList != null)
            {
                return tileNonCurrentLayoutList[0];
            }

            return null;
        }

        private void processSeamDesignStateSelectAreaLeftCntlClick(double x, double y, ref bool cancelDefault)
        {
            Dictionary<string, CanvasSeam> seamDict = CurrentPage.GetSeamDictionary(FinishManagerGlobals.SelectedAreaFinishManager);

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
            if (processSeamDesignStateProcessAreaLockClick(x, y))
            {
                return;
            }

            if (processSeamDesignStateProcessDisplaySeamClick(x,y))
            {
                return;
            }

            if (processSeamDesignStateProcessSeamClick(x, y))
            {
                return;
            }

            if (processSeamDesignStateProcessLineClick(x, y))
            {
                return;
            }

        }

        private bool processSeamDesignStateProcessAreaLockClick(double x, double y)
        {
           
            CanvasLayoutArea layoutArea = CurrentPage.GetSelectedLayoutArea(x, y, MaterialsType.Any, LayoutAreaType.Normal);

            if (layoutArea == null)
            {
                return false;
            }

            if (KeyboardUtils.QKeyPressed)
            {
                if (SystemState.RbnHideAllLocks.Checked)
                {
                    return true;
                }

                if (layoutArea.IsSeamStateLocked)
                {
                    layoutArea.IsSeamStateLocked = false;

                    return true;
                }

                else
                {
                    layoutArea.IsSeamStateLocked= true;

                    return true;
                }
            }

            if (layoutArea.IsSeamStateLocked)
            {
                return true;
            }

            return false;
        }

        private bool processSeamDesignStateProcessDisplaySeamClick(double x, double y)
        {
            GraphicShape displaySeamShape = mouseOverDisplaySeam(x, y);

            if (displaySeamShape == null)
            {
                return false;
            }

            if (!CurrentPage.DisplaySeamDict.ContainsKey(displaySeamShape.Guid))
            {
                return false;
            }

            selectedCanvasSeam = CurrentPage.DisplaySeamDict[displaySeamShape.Guid];

            Point baseCursorPosition = SystemGlobals.GetCursorPosition();

            Point loclCursorPosition = this.BaseForm.PointToClient(baseCursorPosition);

            if (selectedCanvasSeam.IsHideable)
            {
                DisplaySeamOptionsMakeBasic.Show(BaseForm, loclCursorPosition);
            }

            else
            {
                DisplaySeamOptionsMakeHideable.Show(BaseForm, loclCursorPosition);
            }
            

            displaySeamShape.Unlock();

            VisioInterop.SelectShape(Window, displaySeamShape);

            return true;
        }

        private bool processSeamDesignStateProcessSeamClick(double x, double y)
        {
           
            Dictionary<string, CanvasSeam> canvasSeamDict = CurrentPage.GetSeamDictionary(FinishManagerGlobals.SelectedAreaFinishManager);

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

                Point baseCursorPosition = SystemGlobals.GetCursorPosition();

                Point loclCursorPosition = this.BaseForm.PointToClient(baseCursorPosition);

                cursorPosition = loclCursorPosition;

                if (canvasSeam.SeamType == SeamType.Manual)
                {
                    if (KeyboardUtils.AltKeyPressed)
                    {
                        ManualSeamDelete_Click(null, null);

                        return true;
                    }

                    else
                    {
                        ManualSeamsOptions.Show(BaseForm, cursorPosition);

                        return true;
                    }
                   // return false;

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

                //else if (canvasSeam.SeamType == SeamType.Display)
                //{
                //    DisplaySeamDelete.Show(BaseForm, loclCursorPosition);
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
                    CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(offspringLayoutArea, FinishGlobals.SelectedAreaFinish.Guid);
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
            if (selectedCanvasSeam.SeamType != SeamType.Basic && (selectedCanvasSeam.SeamType != SeamType.Display))
            {
                return;
            }

            if (selectedCanvasSeam.IsHideable)
            {
                return;
            }

            selectedCanvasSeam.IsHideable = true;

            CanvasLayoutArea canvasLayoutArea = selectedCanvasSeam.layoutArea;

            // canvasLayoutArea.AreaFinishManager.AreaFinishLayers.NormalSeamsLayer.RemoveShapeFromLayer(selectedCanvasSeam.GraphicsSeam.Shape, 0);

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.NormalSeamsUnhideableLayer.RemoveShapeFromLayer(selectedCanvasSeam.GraphicsSeam.Shape, 0);

            selectedCanvasSeam.layoutArea.AreaFinishManager.SetupAllSeamLayers();
        }

        private void CreateDisplaySeam_Click(object sender, EventArgs e)
        {
            

            CanvasSeam displaySeam = selectedCanvasSeam.Clone();

            displaySeam.SeamType = SeamType.Display;

            displaySeam.GraphicsSeam.Draw(displaySeam.SeamFinishBase.SeamColor, displaySeam.SeamFinishBase.SeamWidthInPts, displaySeam.SeamFinishBase.VisioDashType, false);

            BasicSeamMakeHideable_Click(null, null);

            CanvasLayoutArea canvasLayoutArea = displaySeam.layoutArea;

            canvasLayoutArea.DisplayCanvasSeamList.Add(displaySeam);

            selectedCanvasSeam.layoutArea.AreaFinishManager.SetupAllSeamLayers();

            selectedCanvasSeam = displaySeam;

            displaySeam.SeamType = SeamType.Display;

            displaySeam.GraphicsSeam.Shape.Data1 = "[DisplaySeam]";

            displaySeam.layoutArea = canvasLayoutArea;

            displaySeam.SeamFinishBase = displaySeam.SeamFinishBase;

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.NormalSeamsLayer.AddShape(displaySeam.GraphicsSeam, 0);

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.NormalSeamsUnhideableLayer.AddShape(displaySeam.GraphicsSeam, 0);

            CurrentPage.DisplaySeamDict.Add(displaySeam.GraphicsSeam.Shape.Guid, displaySeam);

            //canvasLayoutArea.SeamList.Add(displaySeam);
        }

        private void BasicSeamMakeUnhideable_Click(object sender, EventArgs e)
        {
            if (selectedCanvasSeam.SeamType != SeamType.Basic && selectedCanvasSeam.SeamType != SeamType.Display)
            {
                return;
            }

            if (!selectedCanvasSeam.IsHideable)
            {
                return;
            }

            selectedCanvasSeam.IsHideable = false;

            CanvasLayoutArea canvasLayoutArea = selectedCanvasSeam.layoutArea;

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.NormalSeamsUnhideableLayer.AddShape(selectedCanvasSeam.GraphicsSeam.Shape, 0);

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.NormalSeamsLayer.AddShape(selectedCanvasSeam.GraphicsSeam.Shape, 0);

            selectedCanvasSeam.layoutArea.AreaFinishManager.SetupAllSeamLayers();
        }


        private void DeleteDisplaySeam_Click(object sender, EventArgs e)
        {
            
            if (selectedCanvasSeam.SeamType != SeamType.Display)
            {
                return;
            }

            CanvasLayoutArea canvasLayoutArea = selectedCanvasSeam.layoutArea as CanvasLayoutArea;

            canvasLayoutArea.DisplayCanvasSeamList.Remove(selectedCanvasSeam);

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.NormalSeamsLayer.RemoveShapeFromLayer(selectedCanvasSeam.GraphicsSeam, 0);

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.NormalSeamsUnhideableLayer.RemoveShapeFromLayer(selectedCanvasSeam.GraphicsSeam, 0);

            if (CurrentPage.DisplaySeamDict.ContainsKey(selectedCanvasSeam.Guid))
            {
                CurrentPage.DisplaySeamDict.Remove(selectedCanvasSeam.Guid);
            }

            selectedCanvasSeam.Delete();

            selectedCanvasSeam = null;

        }
        
        private void DeleteManualSeam_Click(object sender, EventArgs e)
        {
            if (selectedCanvasSeam.SeamType != SeamType.Manual)
            {
                return;
            }

            CanvasLayoutArea canvasLayoutArea = selectedCanvasSeam.layoutArea as CanvasLayoutArea;

            canvasLayoutArea.CanvasSeamList.Remove(selectedCanvasSeam);

            canvasLayoutArea.AreaFinishManager.AreaFinishLayers.ManualSeamsAllLayer.RemoveShapeFromLayer(selectedCanvasSeam.GraphicsSeam, 0);

            selectedCanvasSeam.Delete();

            selectedCanvasSeam = null;
        }

        private void SelectDisplaySeam_Click(object sender, EventArgs e)
        {

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

            AreaFinishManager areaFinishManager = canvasLayoutArea.AreaFinishManager;

            areaFinishManager.SeamDesignStateLayer.RemoveShapeFromLayer(selectedCanvasSeam.GraphicsSeam.Shape, 1);

            areaFinishManager.AreaFinishLayers.ManualSeamsAllLayer.RemoveShapeFromLayer(selectedCanvasSeam.GraphicsSeam.Shape, 1);

            selectedCanvasSeam.Delete();
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
                    SelectedUndrageIndex.Location = new Coordinate(x+0.25, y);

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
                        , FinishGlobals.SelectedAreaFinish.Guid);

                }
            }

            else
            {
                List<CanvasDirectedLine> initialLineList = CurrentPage.GetSelectedLineShapeList(x, y);

                if (initialLineList is null)
                {
                    ManagedMessageBox.Show("You are in the seaming design state. Please click close to a base-line for seaming.");
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
                ManagedMessageBox.Show("Roll width must be greater than 0.");
                return true;
            }

            if (!selectedLayoutArea.SeamDesignStateSelectionModeSelected)
            {
                ManagedMessageBox.Show("Seaming of an area requires that it be selected.");
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
                selectedLayoutArea.CreateSeamIndexTag();

               
            }

            selectedLayoutArea.SeamIndexTag.UnderlineTag();
            var seamAreaIndexLayer = selectedLayoutArea.AreaFinishManager.AreaFinishLayers.SeamAreaIndexLayer;
            var seamIndexTag = selectedLayoutArea.SeamIndexTag;

            seamAreaIndexLayer.AddShape(seamIndexTag.Shape, 1);
            seamIndexTag.Shape.LayerSet.Add(seamAreaIndexLayer);

            CurrentPage.UpdateSeamTotals();

            selectedLayoutArea.AreaFinishManager.UpdateFinishStats();

            if (FinishManagerGlobals.SelectedAreaFinishManager.MaterialsType == MaterialsType.Rolls)
            {
                BaseForm.OversUndersFormUpdate();
                BaseForm.EnableOversUndersButton(true);

            }
           

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

            Dictionary<string, CanvasSeam> seamDict = CurrentPage.GetSeamDictionary(FinishManagerGlobals.SelectedAreaFinishManager);

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

           // areaFinishManager.SeamDesignStateLayer.AddShape(canvasSeam.GraphicsSeam.Shape, 1);

            areaFinishManager.AreaFinishLayers.ManualSeamsAllLayer.AddShape(canvasSeam.GraphicsSeam.Shape, 1);

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
                CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(currentLayoutArea, FinishGlobals.SelectedAreaFinish.Guid);
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

            currentLayoutArea.DrawSeams(false);

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
                canvasLayoutArea.SeamDesignStateSelectionModeSelected = false;

                if (!(canvasLayoutArea.SeamIndexTag is null))
                {
                    canvasLayoutArea.AreaFinishManager.SeamDesignStateLayer.RemoveShapeFromLayer(canvasLayoutArea.SeamIndexTag, 1);

                    canvasLayoutArea.SeamIndexTag.Delete();
                }

                if (canvasLayoutArea.IsSeamed())
                {

                    canvasLayoutArea.RemoveSeamIndexTag();
                    canvasLayoutArea.RemoveSeamsAndRollouts();
                    canvasLayoutArea.RemoveEmbeddedOvers();


                    canvasLayoutArea.BaseSeamLineWall = null;

                    canvasLayoutAreaList.Add(canvasLayoutArea);

                    canvasLayoutAreaList.Add(canvasLayoutArea);
                }
            }

            //foreach (CanvasLayoutArea canvasLayoutArea in canvasLayoutAreaList)
            //{
            //    canvasLayoutArea.DeleteRollouts();

            //    canvasLayoutArea.DeleteSeams();

            //    canvasLayoutArea.AreaFinishManager.UpdateFinishStats();
            //}

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
            if (SystemState.SeamMode != SeamMode.Selection)
            {
                return;
            }

            string selectedShapeGuid = VisioInterop.GetSelectedShapeGuid(this.Window);

            if (selectedShapeGuid== null)
            {
                return;
            }

   

            if (!CurrentPage.PageShapeDict.ContainsKey(selectedShapeGuid))
            {
                return;
            }

            GraphicShape graphicShape = CurrentPage.PageShapeDict[selectedShapeGuid];

            string shapeType = graphicShape.Data1;

            if (shapeType != "[MeasuringStick]" && shapeType != "[SeamingTool]" && shapeType != "[DisplaySeam]")
            {
                return;
            }
            
            switch (keyVal)
            {
                case 0: moveSeamStateSelectedItemLeft(graphicShape); break;
                case 1: moveSeamStateSelectedItemUp(graphicShape); break;
                case 2: moveSeamStateSelectedItemRght(graphicShape); break;
                case 3: moveSeamStateSelectedItemDown(graphicShape); break;

                default: break;
            }

           // SeamingTool.Select();

        }

        private void moveSeamStateSelectedItemDown(GraphicShape graphicShape)
        {
            MoveSelectedShapeByIncrement(graphicShape, 0, -1);
        }

        private void moveSeamStateSelectedItemLeft(GraphicShape graphicShape)
        {
            MoveSelectedShapeByIncrement(graphicShape, -1, 0);
        }

        private void moveSeamStateSelectedItemUp(GraphicShape graphicShape)
        {    
            MoveSelectedShapeByIncrement(graphicShape, 0, 1);
        }

        private void moveSeamStateSelectedItemRght(GraphicShape graphicShape)
        {     
            MoveSelectedShapeByIncrement(graphicShape, 1, 0);
        }

    }
}
