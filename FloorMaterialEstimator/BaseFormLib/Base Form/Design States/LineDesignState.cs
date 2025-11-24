

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

        //public LineMode LineMode
        //{
        //    get
        //    {
        //        return SystemState.LineMode;
        //    }

//    set
//    {
//        LineMode prevLineMode = SystemState.LineMode;
//        LineMode currLineMode = value;

//        if (prevLineMode == currLineMode)
//        {
//            return;
//        }

//        SystemState.LineMode = value;

//        exitLineMode(prevLineMode);
//        entrLineMode(currLineMode);
//    }
//}

//private void exitLineMode(LineMode prevLineMode)
//{
//    if (!(CanvasManager.LineModeStartMarker is null))
//    {
//        CanvasManager.LineModeStartMarker.Delete();
//        CanvasManager.LineModeStartMarker = null;
//    }

//    CanvasManager.ResetLineDrawState();

//}

#if false
        private void entrLineMode(LineMode currLineMode)
        { 
               
#if DEBUG
            Debug.Assert(DesignState == DesignState.Line);

            string designStateText = "Design State: Line(" + currLineMode.ToString() + ")";

            this.tlsDesignState.Text = designStateText;
                
#endif

            if (SystemState.LineMode == LineMode.Layout)
            {
                this.btnLineDesignStateLayoutMode.BackColor = Color.Orange;
                this.btnLineDesignStateEditMode.BackColor = SystemColors.ControlLightLight;

                this.grbLayoutLineMode.Enabled = true;
                this.grbEditLineMode.Enabled = false;

                foreach (CanvasDirectedLine canvasDirectedLine in currentPage.SelectedLines)
                {
                    canvasDirectedLine.SetLineGraphics(DesignState.Line, false, AreaShapeBuildStatus.Completed);
                }

                currentPage.SelectedLineDict.Clear();

                clrEditLineModeButtons();
                setLayoutLineModeButtons();
            }

            if (SystemState.LineMode == LineMode.Edit)
            {
                this.btnLineDesignStateLayoutMode.BackColor = SystemColors.ControlLightLight;
                this.btnLineDesignStateEditMode.BackColor = Color.Orange;

                this.grbLayoutLineMode.Enabled = false;
                this.grbEditLineMode.Enabled = true;

                setEditLineModeButtons();
                clrLayoutLineModeButtons();
            }
        }
#endif
        //private void setEditLineModeButtons()
        //{
        //    switch (this.EditLineMode)
        //    {
        //        case EditLineMode.ChangeLinesToSelected:

        //            this.btnEditLineChangeLinesToSelected.BackColor = Color.Orange;
        //            //this.btnEditLineDeleteLines.BackColor = SystemColors.ControlLightLight;
        //          //  this.btnSetLinesTo1X.BackColor = SystemColors.ControlLightLight;
        //            this.btnSetLinesTo2X.BackColor = SystemColors.ControlLightLight;

        //            return;

        //        case EditLineMode.DeleteLines:

        //            this.btnEditLineChangeLinesToSelected.BackColor = SystemColors.ControlLightLight;
        //            //this.btnEditLineDeleteLines.BackColor = Color.Orange;
        //          //  this.btnSetLinesTo1X.BackColor = SystemColors.ControlLightLight;
        //            this.btnSetLinesTo2X.BackColor = SystemColors.ControlLightLight;

        //            return;

        //        case EditLineMode.SetLinesTo1X:

        //            this.btnEditLineChangeLinesToSelected.BackColor = SystemColors.ControlLightLight;
        //            //this.btnEditLineDeleteLines.BackColor = SystemColors.ControlLightLight;
        //          //  this.btnSetLinesTo1X.BackColor = Color.Orange;
        //            this.btnSetLinesTo2X.BackColor = SystemColors.ControlLightLight;

        //            return;

        //        case EditLineMode.SetLinesTo2X:

        //            this.btnEditLineChangeLinesToSelected.BackColor = SystemColors.ControlLightLight;
        //           // this.btnEditLineDeleteLines.BackColor = SystemColors.ControlLightLight;
        //            this.btnSetLinesTo1X.BackColor = SystemColors.ControlLightLight;
        //            this.btnSetLinesTo2X.BackColor = Color.Orange;

        //            return;
        //    }
        //}

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

        public void BtnLayoutLineActivate_Click(object sender, EventArgs e)
        {
            CanvasManager.ResetLineDrawState();

            if (btnLayoutLineActivate.Text == "Activate")
            {
                LayoutLineMode = LineDrawingMode.TakeoutArea;
            }

            else
            {
                LayoutLineMode = LineDrawingMode.Mode2X;
            }
        }

        private void txbDoorTakeoutOther_TextChanged(object sender, EventArgs e)
        {
            string otherTakeoutStr = this.txbDoorTakeoutOther.Text.Trim();

            Utilities.SetTextFormatForValidPositiveDouble(this.txbDoorTakeoutOther);

            //if (!Utilities.IsValidPosDbl(otherTakeoutStr))
            //{
            //    txbDoorTakeoutOther.BackColor = Color.Pink;
            //}

            //else
            //{
            //    txbDoorTakeoutOther.BackColor = Color.White;
            //}
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
                    this.btnLayoutLineActivate.BackColor = SystemColors.ControlLightLight;

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
                    this.btnLayoutLineActivate.BackColor = SystemColors.ControlLightLight;

                    return;

                case LineDrawingMode.Mode2X:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLine2XMode.BackColor = Color.Orange;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineActivate.BackColor = SystemColors.ControlLightLight;

                    return;

                case LineDrawingMode.TakeoutArea:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineActivate.BackColor = Color.Orange;

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
                    this.btnLayoutLineActivate.BackColor = SystemColors.ControlLightLight;

                    return;

                case LineDrawingMode.Mode1X:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = lightOrange;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineActivate.BackColor = SystemColors.ControlLightLight;

                    return;

                case LineDrawingMode.Mode2X:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLine2XMode.BackColor = lightOrange;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineActivate.BackColor = SystemColors.ControlLightLight;

                    return;

                case LineDrawingMode.TakeoutArea:

                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.ControlLightLight;
                    this.btnLayoutLineActivate.BackColor = lightOrange;

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
