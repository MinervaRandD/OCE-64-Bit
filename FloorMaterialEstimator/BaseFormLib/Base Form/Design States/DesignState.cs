//-------------------------------------------------------------------------------//
// <copyright file="LineArea.cs" company="Bruun Estimating, LLC">                // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

using CanvasLib.Legend;

namespace FloorMaterialEstimator
{
    using System;
    using System.Drawing;
    using System.Linq;

    using System.Windows.Forms;

    using Globals;
    using Graphics;
    using SettingsLib;
    using Utilities;
    using CanvasManager;
    using CanvasLib.Filters.Area_Filter;
    using FloorMaterialEstimator.Finish_Controls;
    using System.Diagnostics;
    using CanvasLib.Filters;

    public partial class FloorMaterialEstimatorBaseForm
    {
        public delegate void DesignStateChangeHandler(DesignState prevDesignState, DesignState currDesignState, SeamMode currSeamMode);

        public event DesignStateChangeHandler DesignStateChanged;

        //private DesignState designState = DesignState.Area;

        public DesignState DesignState
        {
            get
            {
                return SystemState.DesignState;
            }

            set
            {
               
                DesignState prevDesignState = SystemState.DesignState;
                DesignState currDesignState = value;

                string designStateText = "Design State: " + value.ToString();

                if (prevDesignState == currDesignState)
                {
                    SetDesignStateButton(value);

                    return;
                }

                this.CanvasManager.LastLayoutArea = null;

                if (SystemState.DrawingShape)
                {
                    ManagedMessageBox.Show("The current drawing must be completed before changing design modes.");
                    return;
                }

                // To cover all bases, in this release whenever the design state changes
                // the copy and paste mode is cancelled regardless of where we are coming
                // from or where we are going to.

                // The following was commented out because it was causing the area to be shown in line mode.

                // CanvasManager.ResetCopyAndPasteMode();

                if (prevDesignState == DesignState.Seam && SystemState.SeamMode == SeamMode.Subdivision && SystemState.DrawingShape)
                {
                    btnCancelSubdivision_Click(null, null);
                }

                if (prevDesignState == DesignState.Area)
                {
                    // Double lines can be created in area mode, but are only intended to be temporary and shown in the current invokation of area
                    // mode. They are to be removed once area mode is exited. Note that copies of these lines are also created for line mode and are
                    // intended to be permanent in line mode. The following removes all the temporary double lines.

                    foreach (CanvasDirectedLine doubleLineToRemove in CanvasManager.DoubleLinesToRemoveWhenLeavingAreaMode)
                    {
                        CanvasManager.RemoveLineShape(doubleLineToRemove);
                    }


                    CanvasManager.DoubleLinesToRemoveWhenLeavingAreaMode.Clear();
                }

                

                SystemState.DesignState = value;

                if (DesignStateChanged != null)
                {
                   DesignStateChanged.Invoke(prevDesignState, currDesignState, SeamMode);
                }

                switch (SystemState.DesignState)    // switch on new state...
                {
                    case DesignState.Line:
                        {
                            this.pnlLineCommandPane.BringToFront();

                            LayoutLineMode = LineDrawingMode.Mode2X;

                            prevAreaModeDrawingMode = SystemState.DrawingMode;

                            SystemState.DrawingMode = DrawingMode.Line2X;

                            // Show or hide the takeouts when in line mode depending on the takeout hide button state.

                            GraphicsLayerBase layer = CanvasManager.Page.TakeoutLayer;

                            layer.SetLayerVisibility(btnDoorTakeoutShow.Text == "Hide");
                         
                            CanvasManager.HideSeamingTool();

                            if (Utilities.IsNotNull(this.ShortcutSettingsForm))
                            {
                                if (this.ShortcutSettingsForm.ShortcutMode == ShortcutMode.AreaMode || this.ShortcutSettingsForm.ShortcutMode == ShortcutMode.SeamMode)
                                {
                                    this.ShortcutSettingsForm.SetDesignStateSelection(ShortcutMode.LineMode);
                                }
                            }



                            CanvasManager.LineLayoutState = LineLayoutState.Default;

                            SetupLineState();

                            //this.DrawingMode = DrawingMode.Line;
                        }
                        break;

                    case DesignState.Area:
                        {
                            //CanvasManager.AreaHistoryList.Clear();

                            this.pnlAreaCommandPane.BringToFront();
                            SystemState.DrawingMode = prevAreaModeDrawingMode;

                            //if (currentPage.LayoutAreas.Count() <= 0)
                            //{
                            //    this.AreaMode = AreaMode.Layout;
                            //}

                            // Hide the takeouts when in area mode.

                            GraphicsLayerBase layer = CanvasManager.Page.TakeoutLayer;

                            layer.SetLayerVisibility(false);

                            CanvasManager.HideSeamingTool();

                            if (Utilities.IsNotNull(this.ShortcutSettingsForm))
                            {
                                if (this.ShortcutSettingsForm.ShortcutMode == ShortcutMode.LineMode || this.ShortcutSettingsForm.ShortcutMode == ShortcutMode.SeamMode)
                                {
                                    this.ShortcutSettingsForm.SetDesignStateSelection(ShortcutMode.AreaMode);
                                }
                            }


                            this.tlsDesignState.Text = designStateText;

                            SetupAreaState();

                        }
                        break;

                    case DesignState.Seam:
                        {
                            this.pnlSeamCommandPane.BringToFront();
                            SystemState.DrawingMode = prevAreaModeDrawingMode;

                            if (this.btnShowSeamingTool.BackColor == Color.Orange)
                            {
                                CanvasManager.SeamingTool.Show();
                            }

                            else
                            {
                                CanvasManager.SeamingTool.Hide();
                            }

                            // Hide the takeouts when in seam mode.

                            GraphicsLayerBase layer = CanvasManager.Page.TakeoutLayer;

                            layer.SetLayerVisibility(false);

                            if (Utilities.IsNotNull(this.ShortcutSettingsForm))
                            {
                                if (this.ShortcutSettingsForm.ShortcutMode == ShortcutMode.AreaMode || this.ShortcutSettingsForm.ShortcutMode == ShortcutMode.LineMode)
                                {
                                    this.ShortcutSettingsForm.SetDesignStateSelection(ShortcutMode.SeamMode);
                                }
                            }

                            SeamMode = SeamMode.Selection;

                            SetupSeamState();

                            this.tlsDesignState.Text = designStateText;
                        }
                        break;

                    default:
                        break;
                }

                if (CanvasManager.Window != null)
                {
                    CanvasManager.Window.DeselectAll();
                }

                SetDesignStateButton(value);

            }
        }

        public void SetupLineState()
        {
            CurrentPage.AreaLegendLayer.SetLayerVisibility(false);
            CurrentPage.LineLegendLayer.SetLayerVisibility(CanvasManager.LegendController.LineModeLegend.Visible);

            CurrentPage.AreaModeGlobalLayer.SetLayerVisibility(false);

            CurrentPage.LineModeGlobalLayer.SetLayerVisibility(true);
            CurrentPage.SeamModeGlobalLayer.SetLayerVisibility(false);

            CanvasManager.CounterController.ResetAllCounterLayerVisibility();

            AreaFinishManagerList.SetupAllSeamLayers();
              
            if (this.btnAutoSelect.Checked)
            {
                ResetAutoSelectOption();
            }
        }

        public void SetupAreaState()
        {
            CurrentPage.AreaLegendLayer.SetLayerVisibility(CanvasManager.LegendController.AreaModeLegend.Visible);
            CurrentPage.LineLegendLayer.SetLayerVisibility(false);

            CurrentPage.AreaModeGlobalLayer.SetLayerVisibility(true);
            CurrentPage.LineModeGlobalLayer.SetLayerVisibility(false);
            CurrentPage.SeamModeGlobalLayer.SetLayerVisibility(false);

            CanvasManager.CounterController.ResetAllCounterLayerVisibility();

            SetupAreaSeamsState();

            if (this.btnAutoSelect.Checked)
            {
                ResetAutoSelectOption();
            }
        }

        public void ResetDesignLayers()
        {
            switch (DesignState)
            {
                case DesignState.Area:
                    SetupAreaState();
                    break;

                case DesignState.Seam:
                    SetupSeamState();
                    break;

                default:
                    break;
            }
        }

        private void SetupAreaSeamsState()
        {
            AreaFinishManagerList.SetupAllSeamLayers();
                
            // The following should not be needed. But without it, when the show cut index is checked,
            // the indices appear with the circles regardsless of other considerations. Some day will 
            // figure it out.

            foreach (AreaFinishManager areaFinishManager in this.AreaFinishManagerList)
            {
                areaFinishManager.SetCutIndexCircleVisibility();
            }
            //CanvasManager.CurrentPage.CutsIndexLayer.SetLayerVisibility(showIndexes);
        }

        private void SetupSeamState()
        {
            CurrentPage.AreaLegendLayer.SetLayerVisibility(false);
            CurrentPage.LineLegendLayer.SetLayerVisibility(false);

            CurrentPage.AreaModeGlobalLayer.SetLayerVisibility(false);
            CurrentPage.LineModeGlobalLayer.SetLayerVisibility(false);
            CurrentPage.SeamModeGlobalLayer.SetLayerVisibility(true);

            CanvasManager.CounterController.SetAllCounterLayersToInvisibile();

            AreaFinishManagerList.SetupAllSeamLayers();
               
            this.btnAutoSelect.Checked = true;

            seamPalette.UpdateSeamList();

            SetupSeamModeDesignState(selectedAreaFinishManager);

            if (CanvasManager.ShowSeamingToolOnSwitchToSeamMode)
            {
                CanvasManager.ShowSeamingTool();
            }

            // The following should not be needed. But without it, when the show cut index is checked,
            // the indices appear with the circles regardsless of other considerations. Some day will 
            // figure it out.

            foreach (AreaFinishManager areaFinishManager in this.AreaFinishManagerList)
            {
                areaFinishManager.SetCutIndexCircleVisibility();
            }
        }

        public void ToggleDesignStateMode()
        {
            switch (DesignState)
            {
               // case DesignState.Area: ToggleAreaDesignStateMode(); return;

                //case DesignState.Line: ToggleLineDesingStateMode(); return;

                case DesignState.Seam: ToggleSeamDesignStateMode(); return;
            }
        }

        //private void AreaFilters_SeamFilterChanged(AreaFilters filter)
        //{
            
        //    switch (DesignState)
        //    {
        //        case DesignState.Area:
        //            SetupAreaSeamsState();
        //                break;

        //        default:
        //            break;
        //    }
        //}


    }

}
