

namespace FloorMaterialEstimator
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    using Globals;
    using FloorMaterialEstimator.CanvasManager;
    using Utilities;
    using CanvasLib.Markers_and_Guides;

    public partial class FloorMaterialEstimatorBaseForm
    {
//        public AreaMode AreaMode
//        {
//            get
//            {
//                return SystemState.AreaMode;
//            }

//            set
//            {
//                if (SystemState.AreaMode == value)
//                {
//                    return;
//                }

//                // In this version, if the area mode state changes for whatever reason
//                // we reset the copy and paste mode

//                CanvasManager.ResetCopyAndPasteMode();

//                SystemState.AreaMode = value;
//#if DEBUG
//                Debug.Assert(DesignState == DesignState.Area);

//                string designStateText = "Design State: Area(" + value.ToString() + ")";
           
//                this.tlsDesignState.Text = designStateText;
//#endif

//                if (SystemState.AreaMode == AreaMode.Layout)
//                {
//                    this.btnAreaDesignStateLayoutMode.BackColor = Color.Orange;
//                    this.btnAreaDesignStateEditMode.BackColor = SystemColors.ControlLightLight;

//                    this.DrawingMode = DrawingMode.Default;

//                    this.grbLayoutAreaMode.Enabled = true;
//                    this.grbEditAreaMode.Enabled = false;

//                    this.clrEditAreaModeButtons();

//                }

//                if (SystemState.AreaMode == AreaMode.Edit)
//                {
//                    this.btnAreaDesignStateLayoutMode.BackColor = SystemColors.ControlLightLight;
//                    this.btnAreaDesignStateEditMode.BackColor = Color.Orange;

//                    this.DrawingMode = DrawingMode.Default;

//                    this.grbLayoutAreaMode.Enabled = false;
//                    this.grbEditAreaMode.Enabled = true;

//                    this.setEditAreaModeButtons();
//                }

//                //areaPalette.SetDesignStateFormat(DesignState.Area, btnShowAreas.Checked);
//                areaPalette.SetDesignStateFormat(DesignState.Area, SeamMode.Unknown, ShowAreas);
//            }
//        }

        public void BtnAreaDesignStateCompleteDrawing_Click(object sender, EventArgs e)
        {
            if (rbnFixedWidth.Checked)
            {
                CanvasManager.ProcessFixedWidthCompleteShape();
            }

            else
            {
                CanvasManager.ProcessPolylineCompleteShape(true);
            }

            ResetAreaTakeOutControls();
        }

        public void BtnFixedWidthJump_Click(object sender, EventArgs e)
        {
            CanvasManager.ProcessFixedWidthCompleteShape();
        }

        public void BtnAreaDesignStateCompleteDrawingByIntersection_Click(object sender, EventArgs e)
        {
            CanvasManager.ProcessPolylineCompleteShapeByIntersectionV2(FloorMaterialEstimator.CanvasManager.CanvasManager.LineCompletionOption.Ask);

            ResetAreaTakeOutControls();
        }

        public void AreaDesignStateCompleteDrawingByMaximumArea()
        {
            CanvasManager.ProcessPolylineCompleteShapeByIntersectionV2(FloorMaterialEstimator.CanvasManager.CanvasManager.LineCompletionOption.PreferMax);

            ResetAreaTakeOutControls();
        }

        public void AreaDesignStateCompleteDrawingByMinimumArea()
        {
            CanvasManager.ProcessPolylineCompleteShapeByIntersectionV2(FloorMaterialEstimator.CanvasManager.CanvasManager.LineCompletionOption.PreferMin);

            ResetAreaTakeOutControls();
        }


        public void BtnAreaDesignModeZeroLine_Click(object sender, EventArgs e)
        {
            CanvasManager.ProcessSwitchZeroLine();
        }

        private void clrEditAreaModeButtons()
        {
            Color lightOrange = Color.FromArgb(100, Color.Orange.R, Color.Orange.G, Color.Orange.B);

            switch (this.editAreaMode)
            {
                case EditAreaMode.ChangeShapesToSelected:

                    //this.btnChangeShapesToSelected.BackColor = lightOrange;
                   // this.btnDeleteShapes.BackColor = SystemColors.ControlLightLight;
                    //this.btnEditShape.BackColor = SystemColors.ControlLightLight;

                    return;

                case EditAreaMode.DeleteShapes:

                   // this.btnChangeShapesToSelected.BackColor = SystemColors.ControlLightLight;
                  //  this.btnDeleteShapes.BackColor = lightOrange;
                    //this.btnEditShape.BackColor = SystemColors.ControlLightLight;

                    return;

                case EditAreaMode.EditShapes:

                  //  this.btnChangeShapesToSelected.BackColor = SystemColors.ControlLightLight;
                  //  this.btnDeleteShapes.BackColor = SystemColors.ControlLightLight;
                   // this.btnEditShape.BackColor = lightOrange;

                    return;
            }
        }

        private void btnEditAreaChangeShapesToSelected_Click(object sender, EventArgs e)
        {
           //this.btnChangeShapesToSelected.BackColor = Color.Orange;
           // this.btnDeleteShapes.BackColor = SystemColors.ControlLightLight;
           // this.btnEditShape.BackColor = SystemColors.ControlLightLight;

            this.EditAreaMode = EditAreaMode.ChangeShapesToSelected;
        }

        public void BtnEditAreaCopyAndPasteShapes_Click(object sender, EventArgs e)
        {
            if (Utilities.IsNotNull(CanvasManager.CopySelectedLayoutArea))
            {
                CopyMarker copyMarker = CanvasManager.CopySelectedLayoutArea.CopyMarker;

                if (Utilities.IsNotNull(copyMarker))
                {
                    CurrentPage.RemoveFromPageShapeDict(copyMarker);

                    CanvasManager.CopySelectedLayoutArea.AreaFinishManager.AreaDesignStateLayer.RemoveShapeFromLayer(copyMarker, 1);

                    CanvasManager.CopySelectedLayoutArea.CopyMarker.Delete();
                }

                CanvasManager.CopySelectedLayoutArea = null;
            }

            if (this.btnCopyAndPasteShapes.BackColor == Color.Orange)
            {
                this.btnCopyAndPasteShapes.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                this.btnCopyAndPasteShapes.BackColor = Color.Orange;
            }

        }

        private void btnDeleteTempAreas_Click(object sender, EventArgs e)
        {
            foreach (CanvasLayoutArea canvasLayoutArea in CurrentPage.TemporaryLayoutAreaList)
            {
                CanvasManager.ProcessEditAreaModeActionDeleteShape(canvasLayoutArea);
            }

            CurrentPage.TemporaryLayoutAreaList.Clear();
        }

        private void btnEditAreaUndo_Click(object sender, EventArgs e)
        {
            CanvasManager.ProcessEditAreaUndo();
        }

        //private void btnEditAreaClear_Click(object sender, EventArgs e)
        //{
        //    CanvasManager.ProcessEditAreaClear();
        //}

        //private void btnEditAreaApply_Click(object sender, EventArgs e)
        //{
        //    CanvasManager.ProcessEditAreaApply(EditAreaMode);
        //}

        //private void ckbHighlightAndApply_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.ckbEditAreaHighlightAndApply.Checked)
        //    {
        //        //this.btnEditAreaApply.Enabled = true;
        //       // this.btnEditAreaClear.Enabled = true;
        //    }

        //    else
        //    {
        //        //this.btnEditAreaApply.Enabled = false;
        //       // this.btnEditAreaClear.Enabled = false;
        //    }
        //}

        //private void ToggleAreaDesignStateMode()
        //{
        //    if (AreaMode == AreaMode.Layout)
        //    {
        //        this.BtnAreaDesignStateEditMode_Click(null, null);

        //        return;
        //    }

        //    if (AreaMode == AreaMode.Edit)
        //    {
        //        this.BtnAreaDesignStateLayoutMode_Click(null, null);
        //    }

        //}
    }
}
