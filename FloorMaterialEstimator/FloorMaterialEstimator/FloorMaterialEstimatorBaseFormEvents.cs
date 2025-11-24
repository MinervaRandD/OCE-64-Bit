//-------------------------------------------------------------------------------//
// <copyright file="FloorMaterialEstimatorBaseFormEvents.cs"                     //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Drawing;

    using FloorMaterialEstimator.Utilities;
    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.ShortcutsAndSettings;
    using FloorMaterialEstimator.Supporting_Forms;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class FloorMaterialEstimatorBaseForm
    {
        internal UCLine selectedLine
        {
            get
            {
                return linePallet.selectedLine;
            }
        }

        internal UCFinish selectedFinish
        {
            get
            {
                return finishPallet.selectedFinish;
            }
        }

        private void FloorMaterialEstimatorBaseForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.WindowState = FormWindowState.Maximized;
        }

        private void loadDrawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvasManager.LoadDrawing();
        }

        private void ToolStripZoomIn_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void ToolStripZoomIn_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripZoomOut_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripZoomPercent_Click(object sender, EventArgs e)
        {
            // Is this needed?
        }

        private void ToolStripFitWidth_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripFitHeight_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripPanMode_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripDrawMode_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripAreaMode_Click(object sender, EventArgs e)
        {
            LineAreaMode = LineArea.Area;
        }

        private void ToolStripLineMode_Click(object sender, EventArgs e)
        {
            if (canvasManager.DrawingShape)
            {
                MessageBox.Show("Cannot change to line mode while a drawing is being completed.");
                return;
            }

            LineAreaMode = LineArea.Line;
        }
        
        private void BtnShowAreas_Click(object sender, EventArgs e)
        {
            foreach (UCFinish ucFinish in finishPallet.finishDict.Values)
            {
                if (ucFinish.bIsFiltered)
                {
                    continue;
                }

                Visio.Layer layer = ucFinish.layer;

                if (layer != null)
                {
                    Utilities.Graphics.SetLayerVisibility(layer, true);
                }
            }

            this.btnShowAreas.Checked = true;
            this.btnHideAreas.Checked = false;
        }

        private void BtnHideAreas_Click(object sender, EventArgs e)
        {
            foreach (UCFinish ucFinish in finishPallet.finishDict.Values)
            {
                Visio.Layer layer = ucFinish.layer;

                if (layer != null)
                {
                    Utilities.Graphics.SetLayerVisibility(layer, false);
                }
            }

            this.btnShowAreas.Checked = false;
            this.btnHideAreas.Checked = true;
        }

        private void ToolStripGreyAreas_Click(object sender, EventArgs e)
        {

        }

        private void BtnShowImage_Click(object sender, EventArgs e)
        {
            canvasManager.ShowDrawing(true);

            btnShowImage.Checked = true;
            btnHideImage.Checked = false;
        }

        private void BtnHideImage_Click(object sender, EventArgs e)
        {
            canvasManager.ShowDrawing(false);

            btnShowImage.Checked = false;
            btnHideImage.Checked = true;
        }

        private void ToolStripEditAreas_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripEditLines_Click(object sender, EventArgs e)
        {

        }


        private void ToolStripMeasureLines_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripAlignToGrid_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripCounters_Click(object sender, EventArgs e)
        {

        }


        private void ToolStripSelectColors_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripSettings_Click(object sender, EventArgs e)
        {

        }

        internal void SetFilteredAreaToolsStatus(bool bChecked)
        {
            this.tlbFilterAreas.Checked = bChecked;
        }

        internal void SetFilteredLineToolsStatus(bool bChecked)
        {
            this.tlsFilterLines.Checked = bChecked;
        }

        private void BtnFilterAreas_Click(object sender, EventArgs e)
        {
            AreaFilter areaFilter = new AreaFilter(this);

            areaFilter.ShowDialog();
        }

        private void BtnFilterLines_Click(object sender, EventArgs e)
        {
            LineFilter lineFilter = new LineFilter(this);

            lineFilter.ShowDialog();
        }

        private void TsbGlobalSettings_Click(object sender, EventArgs e)
        {
            GlobalSettingsForm globalSettingsForm = new GlobalSettingsForm();

            DialogResult dialogResult = globalSettingsForm.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            if (globalSettingsForm.rbnNormalOperatingMode.Checked)
            {
                GlobalSettings.OperatingModeSetting = OperatingMode.Normal;
            }

            else if (globalSettingsForm.rbnDevelopmentOperatingMode.Checked)
            {
                GlobalSettings.OperatingModeSetting = OperatingMode.Development;
            }


            if (globalSettingsForm.rbnShowLineDrawout.Checked)
            {
                GlobalSettings.LineDrawoutModeSetting = LineDrawoutMode.ShowLineDrawout;
            }

            else if (globalSettingsForm.rbnHideLineDrawout.Checked)
            {
                GlobalSettings.LineDrawoutModeSetting = LineDrawoutMode.HideLineDrawout;
            }

            GlobalSettings.SnapToAxisSetting = globalSettingsForm.ckbSnapToAxis.Checked;

            double dTemp = 0;
            int iTemp = 0;

            if (double.TryParse(globalSettingsForm.txbSnapToAxisResolutionInDegrees.Text.Trim(), out dTemp))
            {
                GlobalSettings.SnapResolutionInDegrees = dTemp;
            }

            if (int.TryParse(globalSettingsForm.txbYGridLineCount.Text.Trim(), out iTemp))
            {
                GlobalSettings.YGridlineCountSetting = iTemp;
            }

            if (double.TryParse(globalSettingsForm.txbGridLineOffset.Text.Trim(), out dTemp))
            {
                GlobalSettings.GridlineOffsetSetting = dTemp;
            }
        }

        #region Drawing mode button events

        private void btnCursorDefault_Click(object sender, EventArgs e)
        {
            DrawingMode = DrawingMode.Default;
        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            DrawingMode = DrawingMode.Line;
        }

        private void btnDrawRectangle_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rectangle drawing not currently activated.");

            return;

            //drawingMode = DrawingMode.Rectangle;
        }

        private void btnDrawPolyline_Click(object sender, EventArgs e)
        {
            DrawingMode = DrawingMode.Polyline;
        }

        private void BtnSetCustomScale_Click(object sender, EventArgs e)
        {
            DrawingMode = DrawingMode.ScaleLine;
        }


        private void btnTapeMeasure_Click(object sender, EventArgs e)
        {
            DrawingMode = DrawingMode.TapeMeasure;
        }
        #endregion
    }
}
