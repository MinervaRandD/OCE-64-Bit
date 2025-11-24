using System;
using System.Drawing;
using System.Reflection;
using Globals;

namespace FloorMaterialEstimator
{
    public partial class FloorMaterialEstimatorBaseForm
    {
        private void InitializeGlobals()
        {

            //---------------------------///
            // Initialize static globals //
            //---------------------------//
            SystemState.Init(
                this.btnRedoSeamsAndCuts
                , this.btnTapeMeasure
                , tbcPageAreaLine
                , this.btnZoomIn
                , this.btnZoomOut
                , this.ddbZoomPercent
                , this.btnFitCanvas
                , this.cccAreaMode
                , this.cccLineMode
                , this.BtnCounters
                , this.BtnShowLegendForm
                , this.BtnShowFieldGuides
                , this.tlsDrawoutLength
                , this.tlsDrawingShape
                , this.btnSetCustomScale
                , this.nudFixedWidthFeet
                , this.nudFixedWidthInches
                , this.btnMeasuringStick
                , this.BtnDoorTakeoutShow
                , this.BtnAreaDesignStateZeroLine
                
                // Layout Area Buttons
                , this.btnNormalLayoutArea
                , this.btnColorOnly
                , this.btnFixedWidth
                , this.btnOversGenerator

                , this.btnEmbeddLayoutAreas
                , this.nudOversGeneratorWidthFeet
                , this.nudOversGeneratorWidthInches
                , this.btnCopyAndPasteShapes
                , this.btnShowLabelEditor
                , this.tlsDrawoutLength
                , this.txbRemnantWidthFeet
                , this.txbRemnantWidthInches
                , this.ckbShowAllSeams);

            SystemGlobals.Init();

            SystemGlobals.SetupAllSeamStateSeamLayersForSelectedArea = new Action(() => AreaFinishManagerList.ForEach(m => m.SetupAllSeamLayers()));

            SystemGlobals.ActivateTapeMeasure = new Action(() => BtnTapeMeasure_Click(null, null));

            SystemGlobals.OversUndersFormUpdate = this.OversUndersFormUpdate;

            SystemGlobals.SetCursorPosition = SetCursorPosition;

            SystemGlobals.GetCursorPosition = GetCursorPosition;

            SystemGlobals.RemnantsView = this.ucRemnantsView;

            SystemGlobals.RemoveRemnantArea = this.ucRemnantsView.RemoveRemnantArea;

            SystemGlobals.AreaView = this.ucAreasView;

            SystemGlobals.SeamView = this.ucSeamsView;

            SystemState.DrawingModeChanged += SystemState_DrawingModeChanged;
  
            SystemGlobals.BaseForm = this;

            SystemGlobals.SetAreaCopyAndPastState = SetAreaCopyAndPasteShapesStatus;

            SystemGlobals.BtnFilterAreasClick = this.BtnFilterAreasClick;

            SystemGlobals.BtnFilterLinesClick = this.BtnFilterLinesClick;

            SystemState.DrawingModeChanged += SystemState_DrawingModeChanged;

            SystemGlobals.BaseForm = this;
        }
    }
}
