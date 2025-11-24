

namespace FloorMaterialEstimator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Drawing;
    using System.Diagnostics;
    using Utilities;
    using Globals;
    using FloorMaterialEstimator.CanvasManager;
    using Geometry;

    public partial class FloorMaterialEstimatorBaseForm
    {

        private void clrEditLineModeButtons()
        {
            Color lightOrange = Color.FromArgb(100, Color.Orange.R, Color.Orange.G, Color.Orange.B);

            switch (this.EditLineMode)
            {
                case EditLineMode.ChangeLinesToSelected:

                 //   this.btnEditLineChangeLinesToSelected.BackColor = lightOrange;
                    //this.btnEditLineDeleteLines.BackColor = SystemColors.ControlLightLight;
                    //this.btnSetLinesTo1X.BackColor = SystemColors.ControlLightLight;
                    //this.btnSetLinesTo2X.BackColor = SystemColors.ControlLightLight;

                    return;

                case EditLineMode.DeleteLines:

                  //  this.btnEditLineChangeLinesToSelected.BackColor = SystemColors.ControlLightLight;
                    //this.btnEditLineDeleteLines.BackColor = lightOrange;
                   // this.btnSetLinesTo1X.BackColor = SystemColors.ControlLightLight;
                   // this.btnSetLinesTo2X.BackColor = SystemColors.ControlLightLight;

                    return;

                case EditLineMode.SetLinesTo1X:

                   // this.btnEditLineChangeLinesToSelected.BackColor = SystemColors.ControlLightLight;
                    //this.btnEditLineDeleteLines.BackColor = SystemColors.ControlLightLight;
                   // this.btnSetLinesTo1X.BackColor = lightOrange;
                   // this.btnSetLinesTo2X.BackColor = SystemColors.ControlLightLight;

                    return;

                case EditLineMode.SetLinesTo2X:

                    //this.btnEditLineChangeLinesToSelected.BackColor = SystemColors.ControlLightLight;
                    //this.btnEditLineDeleteLines.BackColor = SystemColors.ControlLightLight;
                    //this.btnSetLinesTo1X.BackColor = SystemColors.ControlLightLight;
                   // this.btnSetLinesTo2X.BackColor = lightOrange;

                    return;
            }
        }

        //private void btnEditLineChangeLinesToSelected_Click(object sender, EventArgs e)
        //{
        //    //this.btnEditLineChangeLinesToSelected.BackColor = Color.Orange;
        //    //this.btnEditLineDeleteLines.BackColor = SystemColors.ControlLightLight;
        //    //this.btnSetLinesTo1X.BackColor = SystemColors.ControlLightLight;
        //    //this.btnSetLinesTo2X.BackColor = SystemColors.ControlLightLight;

        //    this.EditLineMode = EditLineMode.ChangeLinesToSelected;
        //}

        //private void btnEditLineDeleteLines_Click(object sender, EventArgs e)
        //{
        //    this.btnEditLineChangeLinesToSelected.BackColor = SystemColors.ControlLightLight;
        //    this.btnEditLineDeleteLines.BackColor = Color.Orange;
        //    this.btnSetLinesTo1X.BackColor = SystemColors.ControlLightLight;
        //    this.btnSetLinesTo2X.BackColor = SystemColors.ControlLightLight;

        //    this.EditLineMode = EditLineMode.DeleteLines;
        //}

        //private void btnSetLineTo1X_Click(object sender, EventArgs e)
        //{
        //    this.btnEditLineChangeLinesToSelected.BackColor = SystemColors.ControlLightLight;
        //    //this.btnEditLineDeleteLines.BackColor = SystemColors.ControlLightLight;
        //   // this.btnSetLinesTo1X.BackColor = Color.Orange;
        //    //this.btnSetLinesTo2X.BackColor = SystemColors.ControlLightLight;

        //    this.editLineMode = EditLineMode.SetLinesTo1X;
        //}

        //private void btnSetLinesTo2X_Click(object sender, EventArgs e)
        //{
        //    this.btnEditLineChangeLinesToSelected.BackColor = SystemColors.ControlLightLight;
        //   // this.btnEditLineDeleteLines.BackColor = SystemColors.ControlLightLight;
        //  //  this.btnSetLinesTo1X.BackColor = SystemColors.ControlLightLight;
        //    this.btnSetLinesTo2X.BackColor = Color.Orange;

        //    this.editLineMode = EditLineMode.SetLinesTo2X;
        //}

        private void btnEditLineUndo_Click(object sender, EventArgs e)
        {
            CanvasManager.ProcessEditLineUndo();
        }

        public void BtnLayoutLineJump_Click(object sender, EventArgs e)
        {

            // Defensive. You should never hit this.

            if (DesignState != DesignState.Line)
            {
                return;
            }

            CurrentPage.RemoveAllGuides();
            CurrentPage.RemoveLineModeStartMarker();

            // It should always be in the mode anyway

            CanvasManager.ResetLineDrawState();

            
            CanvasManager.MostRecentlyCompletedDoubleLine = null;

            SystemState.DrawingMode = DrawingMode.Line1X;

           CanvasManager.LineLayoutState = LineLayoutState.Default;

            LayoutLineMode = LineDrawingMode.Mode1X; // Added late as requested by Martin.
        }

        public void BtnLayoutLine1XMode_Click(object sender, EventArgs e)
        {
            CanvasManager.ResetLineDrawState();

            LayoutLineMode = LineDrawingMode.Mode1X;

            // The following is a kludge to guarantee that the new sequence label is set up correctly //

            LblNewSequence.BackColor = Color.Orange;
        }

        private void LblNewSequence_Click(object sender, EventArgs e)
        {
            BtnLayoutLine1XMode_Click(null, null);
        }

        public void btnLayoutLine2XMode_Click(object sender, EventArgs e)
        {
            CanvasManager.ResetLineDrawState();

            CanvasManager.MostRecentlyCompletedDoubleLine = null;

            LayoutLineMode = LineDrawingMode.Mode2X;
        }

        public void BtnLayoutLineDuplicate_Click(object sender, EventArgs e)
        {
            LayoutLineMode = LineDrawingMode.Duplicate;
        }

        private LineDrawingMode resetLineDrawingMode = LineDrawingMode.Unknown;

        public void BtnDoorTakeoutActivate_Click(object sender, EventArgs e)
        {
            resetLineDrawingMode = LayoutLineMode;

            CanvasManager.ResetLineDrawState();

            if (BtnDoorTakeoutActivate.Text == "Activate")
            {
                LayoutLineMode = LineDrawingMode.TakeoutArea;
                BtnDoorTakeoutActivate.Text = "Deactivate";
                BtnDoorTakeoutActivate.BackColor = Color.Orange;
            }

            else
            {
                LayoutLineMode = LineDrawingMode.Mode2X;

                BtnDoorTakeoutActivate.Text = "Activate";
                BtnDoorTakeoutActivate.BackColor = SystemColors.Control;
            }

            if (cccLineMode.BtnActivate.Text == "Deactivate")
            {
               cccLineMode.Deactivate() ;
            }
        }

        public void DeactivateDoorTakeoutFromCounters()
        {
            LayoutLineMode = resetLineDrawingMode;
        }

        private void txbDoorTakeoutOther_TextChanged(object sender, EventArgs e)
        {
            string otherTakeoutStr = this.txbDoorTakeoutOther.Text.Trim();

            Utilities.SetTextFormatForValidPositiveDouble(this.txbDoorTakeoutOther);
        }

        private void setLayoutLineModeButtons()
        {
            switch (this.LayoutLineMode)
            {
                case LineDrawingMode.Duplicate:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineDuplicate.BackColor = Color.Orange;
                    this.BtnDoorTakeoutActivate.BackColor = SystemColors.ControlLightLight;

                    return;

                case LineDrawingMode.Mode1X:

                    //---------------------------------------------------------------------//
                    // The following change means that the jump command is activated the   //
                    // same way as the line mode 1x command. Requested late by Martin.     //
                    //---------------------------------------------------------------------//

                    //this.btnLayoutLineJump.BackColor = SystemColors.ControlLightLight;
                    this.LblNewSequence.BackColor = Color.Orange;


                    this.BtnLayoutLine1XMode.BackColor = Color.Orange;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.ControlLightLight;
                    this.BtnDoorTakeoutActivate.BackColor = SystemColors.ControlLightLight;

                    return;

                case LineDrawingMode.Mode2X:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLine2XMode.BackColor = Color.Orange;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.ControlLightLight;
                    this.BtnDoorTakeoutActivate.BackColor = SystemColors.ControlLightLight;

                    return;

                case LineDrawingMode.TakeoutArea:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.ControlLightLight;
                    this.BtnDoorTakeoutActivate.BackColor = Color.Orange;

                    return;
            }
        }

        private void clrLayoutLineModeButtons()
        {
            Color lightOrange = Color.FromArgb(100, Color.Orange.R, Color.Orange.G, Color.Orange.B);

            switch (this.LayoutLineMode)
            {
                case LineDrawingMode.Duplicate:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineDuplicate.BackColor = lightOrange;
                    this.BtnDoorTakeoutActivate.BackColor = SystemColors.ControlLightLight;

                    return;

                case LineDrawingMode.Mode1X:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = lightOrange;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.ControlLightLight;
                    this.BtnDoorTakeoutActivate.BackColor = SystemColors.ControlLightLight;

                    return;

                case LineDrawingMode.Mode2X:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLine2XMode.BackColor = lightOrange;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.ControlLightLight;
                    this.BtnDoorTakeoutActivate.BackColor = SystemColors.ControlLightLight;

                    return;

                case LineDrawingMode.TakeoutArea:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.ControlLightLight;
                    this.BtnDoorTakeoutActivate.BackColor = lightOrange;

                    return;
            }
        }

        //private void ToggleLineDesingStateMode()
        //{
        //    if (LineMode == LineMode.Layout)
        //    {
        //        this.BtnLineDesignStateEditMode_Click(null, null);

        //        return;
        //    }

        //    if (LineMode == LineMode.Edit)
        //    {
        //        this.BtnLineDesignStateLayoutMode_Click(null, null);
        //    }
        //}

    }
}
