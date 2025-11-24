//-------------------------------------------------------------------------------//
// <copyright file="FloorMaterialEstimatorBaseForm.cs"                           //
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
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.ShortcutsAndSettings;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class FloorMaterialEstimatorBaseForm : Form
    {
        private int cntlPanesBaseLocY = 32;
        private int pageAreaLineWidth = 380;

        private int tlsBaseLocY;

        public DrawingMode DrawingMode
        {
            get
            {
                if (btnCursorDefault.Checked)
                {
                    return DrawingMode.Default;
                }

                if (btnDrawLine.Checked)
                {
                    return DrawingMode.Line;
                }

                if (btnDrawRectangle.Checked)
                {
                    return DrawingMode.Rectangle;
                }

                if (btnDrawPolyline.Checked)
                {
                    return DrawingMode.Polyline;
                }

                if (btnSetCustomScale.Checked)
                {
                    return DrawingMode.ScaleLine;
                }

                if (btnTapeMeasure.Checked)
                {
                    return DrawingMode.TapeMeasure;
                }

                throw new NotImplementedException();
            }

            set
            {

                if (value == DrawingMode.Default)
                {
                    btnCursorDefault.Checked = true;
                    btnDrawLine.Checked = false;
                    btnDrawRectangle.Checked = false;
                    btnDrawPolyline.Checked = false;
                    btnSetCustomScale.Checked = false;
                    btnTapeMeasure.Checked = false;

                    return;
                }

                if (value == DrawingMode.Line)
                {
                    btnCursorDefault.Checked = false;
                    btnDrawLine.Checked = true;
                    btnDrawRectangle.Checked = false;
                    btnDrawPolyline.Checked = false;
                    btnSetCustomScale.Checked = false;
                    btnTapeMeasure.Checked = false;

                    return;
                }

                if (value == DrawingMode.Rectangle)
                {
                    btnCursorDefault.Checked = false;
                    btnDrawLine.Checked = false;
                    btnDrawRectangle.Checked = true;
                    btnDrawPolyline.Checked = false;
                    btnSetCustomScale.Checked = false;
                    btnTapeMeasure.Checked = false;

                    return;
                }

                if (value == DrawingMode.Polyline)
                {
                    btnCursorDefault.Checked = false;
                    btnDrawLine.Checked = false;
                    btnDrawRectangle.Checked = false;
                    btnDrawPolyline.Checked = true;
                    btnSetCustomScale.Checked = false;
                    btnTapeMeasure.Checked = false;

                    return;
                }

                if (value == DrawingMode.ScaleLine)
                {
                    btnCursorDefault.Checked = false;
                    btnDrawLine.Checked = false;
                    btnDrawRectangle.Checked = false;
                    btnDrawPolyline.Checked = false;
                    btnSetCustomScale.Checked = true;
                    btnTapeMeasure.Checked = false;

                    return;
                }

                if (value == DrawingMode.TapeMeasure)
                {
                    btnCursorDefault.Checked = false;
                    btnDrawLine.Checked = false;
                    btnDrawRectangle.Checked = false;
                    btnDrawPolyline.Checked = false;
                    btnSetCustomScale.Checked = false;
                    btnTapeMeasure.Checked = true;

                    return;
                }
            }
        }

        public UCFinishPallet finishPallet;
        public UCLinePallet linePallet;

        private CanvasManager.CanvasManager canvasManager;

        private LineArea lineAreaMode = LineArea.Area;

        public LineArea LineAreaMode
        {
            get
            {
                return lineAreaMode;
            }

            set
            {
                lineAreaMode = value;

                if (lineAreaMode == LineArea.Line)
                {
                    canvasManager.SelectLineAreaMode(LineArea.Line);
                }

                else
                {
                    canvasManager.SelectLineAreaMode(LineArea.Area);
                }

                SetAreaLineModeButton(value);
            }
        }

        public void SetAreaLineModeButton(string name)
        {
            if (name == "Area Mode")
            {
                SetAreaLineModeButton(LineArea.Area);
            }

            else if (name == "Line Mode")
            {
                SetAreaLineModeButton(LineArea.Line);
            }
        }

        public void SetAreaLineModeButton(LineArea lineArea)
        {
            if (lineArea == LineArea.Line)
            {
                btnLineMode.Checked = true;
                btnAreaMode.Checked = false;
            }

            else
            {
                btnLineMode.Checked = false;
                btnAreaMode.Checked = true;
            }
        }

        public FloorMaterialEstimatorBaseForm()
        {
            InitializeComponent();

            tlsBaseLocY = tlsMainToolStrip.Location.Y + tlsMainToolStrip.Height + 2;
            
            this.tbcPageAreaLine.Location = new Point(0, cntlPanesBaseLocY);

            string finishIniFilePath = string.Empty;

            if (Program.AppConfig.ContainsKey("finishinifilepath"))
            {
                finishIniFilePath = Program.AppConfig["finishinifilepath"];
            }

            GlobalSettings.Initialize();

            canvasManager = new CanvasManager.CanvasManager(this, this.axDrawingControl);

            finishPallet = new UCFinishPallet();
            finishPallet.Init(canvasManager, finishIniFilePath);

            linePallet = new UCLinePallet();
            linePallet.Init(canvasManager, finishIniFilePath);

            this.tbpAreas.Controls.Add(finishPallet);
            this.tbpLines.Controls.Add(linePallet);

            this.btnAreaMode.Checked = true;
            this.btnLineMode.Checked = false;

            setStatusBarSize();
            setTbcPageAreaSize();
            setAxAreaSize();
            setToolbarSize();

            this.SizeChanged += FloorMaterialEstimatorBaseForm_SizeChanged;

            this.tlbFilterAreas.Click += BtnFilterAreas_Click;
            this.tlsFilterLines.Click += BtnFilterLines_Click;

            DrawingMode = DrawingMode.Default;

            ClearLineLengthStatusStripDisplay();

            this.tsbGlobalSettings.Click += TsbGlobalSettings_Click;

            tlsMainToolStrip.Renderer = new Utilities.CustomToolbarButtonRenderer();

            //btnAreaMode.CheckedChanged += BtnAreaMode_CheckedChanged;
            //btnAreaMode.Paint += BtnAreaMode_Paint;
        }

        private void FloorMaterialEstimatorBaseForm_SizeChanged(object sender, EventArgs e)
        {
            setTbcPageAreaSize();
            setAxAreaSize();
            setStatusBarSize();
            setToolbarSize();
        }

        private void setToolbarSize()
        {
            int formSizeX = this.Width;

            this.lblSeparator.Width = Math.Max(formSizeX - 1480, 16);
        }

        private void setTbcPageAreaSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int cntlSizeY = formSizeY - cntlPanesBaseLocY - 64;

            this.tbcPageAreaLine.Location = new Point(1, tlsBaseLocY);
            this.tbcPageAreaLine.Size = new System.Drawing.Size(pageAreaLineWidth, cntlSizeY);

            foreach (TabPage tp in this.tbcPageAreaLine.TabPages)
            {
                tp.Size = this.tbcPageAreaLine.Size;
            }

            finishPallet.SetSize(new System.Drawing.Size(this.tbcPageAreaLine.Width - 8, this.tbcPageAreaLine.Height - 32));
            linePallet.SetSize(new System.Drawing.Size(this.tbcPageAreaLine.Width - 8, this.tbcPageAreaLine.Height - 32));
        }

        private void setAxAreaSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int cntlLocX = this.tbcPageAreaLine.Location.X + this.tbcPageAreaLine.Width + 1;

            int cntlSizeX = formSizeX - cntlLocX - 32;
            int cntlSizeY = formSizeY - cntlPanesBaseLocY - 128;

            this.axDrawingControl.Location = new Point(cntlLocX, tlsBaseLocY);

            this.axDrawingControl.Size = new System.Drawing.Size(cntlSizeX, cntlSizeY);
        }

        private void setStatusBarSize()
        {
            this.tssFiller.Width = this.Width - this.tbcPageAreaLine.Width - this.tssLineSizeDecimal.Width - this.tssLineSizeEnglish.Width - 32;
            this.sssMainForm.BringToFront();
        }

        internal void SetLineLengthStatusStripDisplay(double length)
        {
            // Assume line length in inches.

            double feet = (int)Math.Floor(length / 12.0);

            double inch = length - (double)(feet * 12);

            this.tssLineSizeDecimal.Text = (length / 12.0).ToString("#,##0.00") + "'";
            this.tssLineSizeEnglish.Text = feet.ToString("#,##0") + "'-" + inch.ToString("0.0") + '"';
        }

        internal void ClearLineLengthStatusStripDisplay()
        {
            this.tssLineSizeDecimal.Text = string.Empty;
            this.tssLineSizeEnglish.Text = string.Empty;
        }
    }
}
