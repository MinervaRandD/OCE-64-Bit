//-------------------------------------------------------------------------------//
// <copyright file="FloorMaterialEstimatorBaseForm.Designer.cs"                  //
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
    public partial class FloorMaterialEstimatorBaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FloorMaterialEstimatorBaseForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadDrawingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripFitWidth = new System.Windows.Forms.ToolStripButton();
            this.toolStripFitHeight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripPanMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripDrawMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAreaMode = new System.Windows.Forms.ToolStripButton();
            this.btnLineMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbFilterAreas = new System.Windows.Forms.ToolStripButton();
            this.tlsFilterLines = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowAreas = new System.Windows.Forms.ToolStripButton();
            this.btnHideAreas = new System.Windows.Forms.ToolStripButton();
            this.toolStripGreyAreas = new System.Windows.Forms.ToolStripButton();
            this.btnShowImage = new System.Windows.Forms.ToolStripButton();
            this.btnHideImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripEditAreas = new System.Windows.Forms.ToolStripButton();
            this.toolStripEditLines = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSetCustomScale = new System.Windows.Forms.ToolStripButton();
            this.btnTapeMeasure = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripAlignToGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripCounters = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSelectColors = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsMainToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripZoomPercent = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCursorDefault = new System.Windows.Forms.ToolStripButton();
            this.btnDrawLine = new System.Windows.Forms.ToolStripButton();
            this.btnDrawRectangle = new System.Windows.Forms.ToolStripButton();
            this.btnDrawPolyline = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbGlobalSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.lblSeparator = new System.Windows.Forms.ToolStripLabel();
            this.lblCursorPosition = new System.Windows.Forms.ToolStripLabel();
            this.tbcPageAreaLine = new System.Windows.Forms.TabControl();
            this.tbpPages = new System.Windows.Forms.TabPage();
            this.tbpAreas = new System.Windows.Forms.TabPage();
            this.tbpLines = new System.Windows.Forms.TabPage();
            this.sssMainForm = new System.Windows.Forms.StatusStrip();
            this.tssFiller = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLineSizeDecimal = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLineSizeEnglish = new System.Windows.Forms.ToolStripStatusLabel();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.menuStrip1.SuspendLayout();
            this.tlsMainToolStrip.SuspendLayout();
            this.tbcPageAreaLine.SuspendLayout();
            this.sssMainForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDrawingToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(15, 4, 0, 4);
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(3285, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadDrawingToolStripMenuItem
            // 
            this.loadDrawingToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadDrawingToolStripMenuItem.Name = "loadDrawingToolStripMenuItem";
            this.loadDrawingToolStripMenuItem.Size = new System.Drawing.Size(92, 19);
            this.loadDrawingToolStripMenuItem.Text = "Load Drawing";
            this.loadDrawingToolStripMenuItem.ToolTipText = "Load a drawing";
            this.loadDrawingToolStripMenuItem.Click += new System.EventHandler(this.loadDrawingToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 19);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.ToolTipText = "Open an  existing project";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(43, 19);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(56, 19);
            this.saveToolStripMenuItem1.Text = "SaveAs";
            // 
            // toolStripZoomIn
            // 
            this.toolStripZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripZoomIn.Image")));
            this.toolStripZoomIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripZoomIn.Margin = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.toolStripZoomIn.Name = "toolStripZoomIn";
            this.toolStripZoomIn.Size = new System.Drawing.Size(36, 36);
            this.toolStripZoomIn.Text = "toolStripButton1";
            this.toolStripZoomIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripZoomIn.ToolTipText = "Zoom in";
            this.toolStripZoomIn.Click += new System.EventHandler(this.ToolStripZoomIn_Click);
            this.toolStripZoomIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolStripZoomIn_MouseDown);
            // 
            // toolStripZoomOut
            // 
            this.toolStripZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripZoomOut.Image")));
            this.toolStripZoomOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripZoomOut.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripZoomOut.Name = "toolStripZoomOut";
            this.toolStripZoomOut.Size = new System.Drawing.Size(36, 45);
            this.toolStripZoomOut.Text = "toolStripButton2";
            this.toolStripZoomOut.ToolTipText = "Zoom out";
            this.toolStripZoomOut.Click += new System.EventHandler(this.ToolStripZoomOut_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 48);
            // 
            // toolStripFitWidth
            // 
            this.toolStripFitWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripFitWidth.Image = ((System.Drawing.Image)(resources.GetObject("toolStripFitWidth.Image")));
            this.toolStripFitWidth.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripFitWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripFitWidth.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripFitWidth.Name = "toolStripFitWidth";
            this.toolStripFitWidth.Size = new System.Drawing.Size(36, 45);
            this.toolStripFitWidth.Text = "toolStripButton3";
            this.toolStripFitWidth.ToolTipText = "Fit to width";
            this.toolStripFitWidth.Click += new System.EventHandler(this.ToolStripFitWidth_Click);
            // 
            // toolStripFitHeight
            // 
            this.toolStripFitHeight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripFitHeight.Image = ((System.Drawing.Image)(resources.GetObject("toolStripFitHeight.Image")));
            this.toolStripFitHeight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripFitHeight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripFitHeight.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripFitHeight.Name = "toolStripFitHeight";
            this.toolStripFitHeight.Size = new System.Drawing.Size(36, 45);
            this.toolStripFitHeight.Text = "toolStripFitToHeight";
            this.toolStripFitHeight.ToolTipText = "Fit to height";
            this.toolStripFitHeight.Click += new System.EventHandler(this.ToolStripFitHeight_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 48);
            // 
            // toolStripPanMode
            // 
            this.toolStripPanMode.CheckOnClick = true;
            this.toolStripPanMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripPanMode.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPanMode.Image")));
            this.toolStripPanMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripPanMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPanMode.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripPanMode.Name = "toolStripPanMode";
            this.toolStripPanMode.Size = new System.Drawing.Size(36, 45);
            this.toolStripPanMode.Text = "toolStripPanMode";
            this.toolStripPanMode.ToolTipText = "Pan mode";
            this.toolStripPanMode.Click += new System.EventHandler(this.ToolStripPanMode_Click);
            // 
            // toolStripDrawMode
            // 
            this.toolStripDrawMode.CheckOnClick = true;
            this.toolStripDrawMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDrawMode.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDrawMode.Image")));
            this.toolStripDrawMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDrawMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDrawMode.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripDrawMode.Name = "toolStripDrawMode";
            this.toolStripDrawMode.Size = new System.Drawing.Size(36, 45);
            this.toolStripDrawMode.Text = "toolStripButton7";
            this.toolStripDrawMode.ToolTipText = "Draw mode";
            this.toolStripDrawMode.Click += new System.EventHandler(this.ToolStripDrawMode_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 48);
            // 
            // btnAreaMode
            // 
            this.btnAreaMode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAreaMode.CheckOnClick = true;
            this.btnAreaMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAreaMode.ForeColor = System.Drawing.Color.Black;
            this.btnAreaMode.Image = ((System.Drawing.Image)(resources.GetObject("btnAreaMode.Image")));
            this.btnAreaMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAreaMode.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.btnAreaMode.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnAreaMode.Name = "btnAreaMode";
            this.btnAreaMode.Size = new System.Drawing.Size(36, 45);
            this.btnAreaMode.Text = "toolStripButton8";
            this.btnAreaMode.ToolTipText = "Area mode";
            this.btnAreaMode.Click += new System.EventHandler(this.ToolStripAreaMode_Click);
            // 
            // btnLineMode
            // 
            this.btnLineMode.CheckOnClick = true;
            this.btnLineMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLineMode.Image = ((System.Drawing.Image)(resources.GetObject("btnLineMode.Image")));
            this.btnLineMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLineMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLineMode.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnLineMode.Name = "btnLineMode";
            this.btnLineMode.Size = new System.Drawing.Size(36, 45);
            this.btnLineMode.Text = "toolStripButton9";
            this.btnLineMode.ToolTipText = "Line mode";
            this.btnLineMode.Click += new System.EventHandler(this.ToolStripLineMode_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 48);
            // 
            // tlbFilterAreas
            // 
            this.tlbFilterAreas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbFilterAreas.Image = ((System.Drawing.Image)(resources.GetObject("tlbFilterAreas.Image")));
            this.tlbFilterAreas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlbFilterAreas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbFilterAreas.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.tlbFilterAreas.Name = "tlbFilterAreas";
            this.tlbFilterAreas.Size = new System.Drawing.Size(36, 45);
            this.tlbFilterAreas.Text = "toolStripButton10";
            this.tlbFilterAreas.ToolTipText = "Filter area types";
            // 
            // tlsFilterLines
            // 
            this.tlsFilterLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsFilterLines.Image = ((System.Drawing.Image)(resources.GetObject("tlsFilterLines.Image")));
            this.tlsFilterLines.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlsFilterLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsFilterLines.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.tlsFilterLines.Name = "tlsFilterLines";
            this.tlsFilterLines.Size = new System.Drawing.Size(36, 45);
            this.tlsFilterLines.Text = "toolStripButton11";
            this.tlsFilterLines.ToolTipText = "Filter line types";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 48);
            // 
            // btnShowAreas
            // 
            this.btnShowAreas.Checked = true;
            this.btnShowAreas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowAreas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowAreas.Image = ((System.Drawing.Image)(resources.GetObject("btnShowAreas.Image")));
            this.btnShowAreas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnShowAreas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowAreas.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnShowAreas.Name = "btnShowAreas";
            this.btnShowAreas.Size = new System.Drawing.Size(36, 45);
            this.btnShowAreas.Text = "toolStripButton12";
            this.btnShowAreas.ToolTipText = "Show areas";
            this.btnShowAreas.Click += new System.EventHandler(this.BtnShowAreas_Click);
            // 
            // btnHideAreas
            // 
            this.btnHideAreas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHideAreas.Image = ((System.Drawing.Image)(resources.GetObject("btnHideAreas.Image")));
            this.btnHideAreas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnHideAreas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHideAreas.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnHideAreas.Name = "btnHideAreas";
            this.btnHideAreas.Size = new System.Drawing.Size(36, 45);
            this.btnHideAreas.Text = "toolStripButton13";
            this.btnHideAreas.ToolTipText = "Hide areas";
            this.btnHideAreas.Click += new System.EventHandler(this.BtnHideAreas_Click);
            // 
            // toolStripGreyAreas
            // 
            this.toolStripGreyAreas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripGreyAreas.Image = ((System.Drawing.Image)(resources.GetObject("toolStripGreyAreas.Image")));
            this.toolStripGreyAreas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripGreyAreas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripGreyAreas.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripGreyAreas.Name = "toolStripGreyAreas";
            this.toolStripGreyAreas.Size = new System.Drawing.Size(36, 45);
            this.toolStripGreyAreas.Text = "toolStripButton14";
            this.toolStripGreyAreas.ToolTipText = "Show areas as greyed";
            this.toolStripGreyAreas.Click += new System.EventHandler(this.ToolStripGreyAreas_Click);
            // 
            // btnShowImage
            // 
            this.btnShowImage.Checked = true;
            this.btnShowImage.CheckOnClick = true;
            this.btnShowImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowImage.Image = ((System.Drawing.Image)(resources.GetObject("btnShowImage.Image")));
            this.btnShowImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnShowImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowImage.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnShowImage.Name = "btnShowImage";
            this.btnShowImage.Size = new System.Drawing.Size(36, 45);
            this.btnShowImage.Text = "toolStripButton15";
            this.btnShowImage.ToolTipText = "Show image";
            this.btnShowImage.Click += new System.EventHandler(this.BtnShowImage_Click);
            // 
            // btnHideImage
            // 
            this.btnHideImage.CheckOnClick = true;
            this.btnHideImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHideImage.Image = ((System.Drawing.Image)(resources.GetObject("btnHideImage.Image")));
            this.btnHideImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnHideImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHideImage.Name = "btnHideImage";
            this.btnHideImage.Size = new System.Drawing.Size(36, 45);
            this.btnHideImage.Text = "toolStripButton16";
            this.btnHideImage.ToolTipText = "Hide image";
            this.btnHideImage.Click += new System.EventHandler(this.BtnHideImage_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 48);
            // 
            // toolStripEditAreas
            // 
            this.toolStripEditAreas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEditAreas.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEditAreas.Image")));
            this.toolStripEditAreas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripEditAreas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEditAreas.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripEditAreas.Name = "toolStripEditAreas";
            this.toolStripEditAreas.Size = new System.Drawing.Size(36, 45);
            this.toolStripEditAreas.Text = "toolStripButton17";
            this.toolStripEditAreas.ToolTipText = "Area settings";
            this.toolStripEditAreas.Click += new System.EventHandler(this.ToolStripEditAreas_Click);
            // 
            // toolStripEditLines
            // 
            this.toolStripEditLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEditLines.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEditLines.Image")));
            this.toolStripEditLines.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripEditLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEditLines.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripEditLines.Name = "toolStripEditLines";
            this.toolStripEditLines.Size = new System.Drawing.Size(36, 45);
            this.toolStripEditLines.Text = "toolStripButton18";
            this.toolStripEditLines.ToolTipText = "Line settings";
            this.toolStripEditLines.Click += new System.EventHandler(this.ToolStripEditLines_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 48);
            // 
            // btnSetCustomScale
            // 
            this.btnSetCustomScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSetCustomScale.Image = ((System.Drawing.Image)(resources.GetObject("btnSetCustomScale.Image")));
            this.btnSetCustomScale.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSetCustomScale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetCustomScale.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnSetCustomScale.Name = "btnSetCustomScale";
            this.btnSetCustomScale.Size = new System.Drawing.Size(36, 45);
            this.btnSetCustomScale.Text = "btnSetScale";
            this.btnSetCustomScale.ToolTipText = "Set custom  scale";
            this.btnSetCustomScale.Click += new System.EventHandler(this.BtnSetCustomScale_Click);
            // 
            // btnTapeMeasure
            // 
            this.btnTapeMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTapeMeasure.Image = ((System.Drawing.Image)(resources.GetObject("btnTapeMeasure.Image")));
            this.btnTapeMeasure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnTapeMeasure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTapeMeasure.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnTapeMeasure.Name = "btnTapeMeasure";
            this.btnTapeMeasure.Size = new System.Drawing.Size(36, 45);
            this.btnTapeMeasure.ToolTipText = "Lline measuring tool";
            this.btnTapeMeasure.Click += new System.EventHandler(this.btnTapeMeasure_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 48);
            // 
            // toolStripAlignToGrid
            // 
            this.toolStripAlignToGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAlignToGrid.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAlignToGrid.Image")));
            this.toolStripAlignToGrid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripAlignToGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAlignToGrid.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripAlignToGrid.Name = "toolStripAlignToGrid";
            this.toolStripAlignToGrid.Size = new System.Drawing.Size(36, 45);
            this.toolStripAlignToGrid.Text = "toolStripButton21";
            this.toolStripAlignToGrid.ToolTipText = "Align to grid";
            this.toolStripAlignToGrid.Click += new System.EventHandler(this.ToolStripAlignToGrid_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 48);
            // 
            // toolStripCounters
            // 
            this.toolStripCounters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCounters.Image = ((System.Drawing.Image)(resources.GetObject("toolStripCounters.Image")));
            this.toolStripCounters.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripCounters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCounters.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripCounters.Name = "toolStripCounters";
            this.toolStripCounters.Size = new System.Drawing.Size(36, 45);
            this.toolStripCounters.Text = "toolStripButton22";
            this.toolStripCounters.ToolTipText = "Counters";
            this.toolStripCounters.Click += new System.EventHandler(this.ToolStripCounters_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 48);
            // 
            // toolStripSelectColors
            // 
            this.toolStripSelectColors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSelectColors.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSelectColors.Image")));
            this.toolStripSelectColors.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSelectColors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSelectColors.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripSelectColors.Name = "toolStripSelectColors";
            this.toolStripSelectColors.Size = new System.Drawing.Size(36, 45);
            this.toolStripSelectColors.Text = "toolStripButton23";
            this.toolStripSelectColors.ToolTipText = "Set area colors";
            this.toolStripSelectColors.Click += new System.EventHandler(this.ToolStripSelectColors_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 48);
            // 
            // tlsMainToolStrip
            // 
            this.tlsMainToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tlsMainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripZoomIn,
            this.toolStripZoomOut,
            this.toolStripZoomPercent,
            this.toolStripSeparator1,
            this.toolStripFitWidth,
            this.toolStripFitHeight,
            this.toolStripSeparator2,
            this.toolStripPanMode,
            this.toolStripDrawMode,
            this.toolStripSeparator3,
            this.btnAreaMode,
            this.btnLineMode,
            this.toolStripSeparator4,
            this.tlbFilterAreas,
            this.tlsFilterLines,
            this.toolStripSeparator5,
            this.btnShowAreas,
            this.btnHideAreas,
            this.toolStripGreyAreas,
            this.btnShowImage,
            this.btnHideImage,
            this.toolStripSeparator6,
            this.toolStripEditAreas,
            this.toolStripEditLines,
            this.toolStripSeparator7,
            this.btnSetCustomScale,
            this.btnTapeMeasure,
            this.toolStripSeparator8,
            this.toolStripAlignToGrid,
            this.toolStripSeparator9,
            this.toolStripCounters,
            this.toolStripSeparator10,
            this.toolStripSelectColors,
            this.toolStripSeparator11,
            this.btnCursorDefault,
            this.btnDrawLine,
            this.btnDrawRectangle,
            this.btnDrawPolyline,
            this.toolStripSeparator12,
            this.tlbSettings,
            this.lblSeparator,
            this.lblCursorPosition});
            this.tlsMainToolStrip.Location = new System.Drawing.Point(0, 27);
            this.tlsMainToolStrip.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.tlsMainToolStrip.Name = "tlsMainToolStrip";
            this.tlsMainToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.tlsMainToolStrip.Size = new System.Drawing.Size(3285, 48);
            this.tlsMainToolStrip.TabIndex = 1;
            this.tlsMainToolStrip.Text = "toolStrip1";
            // 
            // toolStripZoomPercent
            // 
            this.toolStripZoomPercent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripZoomPercent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.toolStripZoomPercent.Image = ((System.Drawing.Image)(resources.GetObject("toolStripZoomPercent.Image")));
            this.toolStripZoomPercent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripZoomPercent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripZoomPercent.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.toolStripZoomPercent.Name = "toolStripZoomPercent";
            this.toolStripZoomPercent.Size = new System.Drawing.Size(45, 45);
            this.toolStripZoomPercent.Text = "toolStripButton5";
            this.toolStripZoomPercent.ToolTipText = "Zoom percentage";
            this.toolStripZoomPercent.Click += new System.EventHandler(this.ToolStripZoomPercent_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem2.Text = "50";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem3.Text = "100";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem4.Text = "150";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem5.Text = "200";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem6.Text = "400";
            // 
            // btnCursorDefault
            // 
            this.btnCursorDefault.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCursorDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnCursorDefault.Image")));
            this.btnCursorDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCursorDefault.Name = "btnCursorDefault";
            this.btnCursorDefault.Size = new System.Drawing.Size(23, 45);
            this.btnCursorDefault.Text = "toolStripButton1";
            this.btnCursorDefault.Click += new System.EventHandler(this.btnCursorDefault_Click);
            // 
            // btnDrawLine
            // 
            this.btnDrawLine.AutoSize = false;
            this.btnDrawLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDrawLine.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawLine.Image")));
            this.btnDrawLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrawLine.Name = "btnDrawLine";
            this.btnDrawLine.Size = new System.Drawing.Size(23, 45);
            this.btnDrawLine.Text = "toolStripButton1";
            this.btnDrawLine.ToolTipText = "Select Line Mode";
            this.btnDrawLine.Click += new System.EventHandler(this.btnDrawLine_Click);
            // 
            // btnDrawRectangle
            // 
            this.btnDrawRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDrawRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawRectangle.Image")));
            this.btnDrawRectangle.ImageTransparentColor = System.Drawing.Color.White;
            this.btnDrawRectangle.Name = "btnDrawRectangle";
            this.btnDrawRectangle.Size = new System.Drawing.Size(23, 45);
            this.btnDrawRectangle.Text = "toolStripButton2";
            this.btnDrawRectangle.ToolTipText = "Rectangle Mode Not Activated";
            this.btnDrawRectangle.Click += new System.EventHandler(this.btnDrawRectangle_Click);
            // 
            // btnDrawPolyline
            // 
            this.btnDrawPolyline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDrawPolyline.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawPolyline.Image")));
            this.btnDrawPolyline.ImageTransparentColor = System.Drawing.Color.White;
            this.btnDrawPolyline.Name = "btnDrawPolyline";
            this.btnDrawPolyline.Size = new System.Drawing.Size(23, 45);
            this.btnDrawPolyline.Text = "toolStripButton3";
            this.btnDrawPolyline.Click += new System.EventHandler(this.btnDrawPolyline_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 48);
            // 
            // tlbSettings
            // 
            this.tlbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbGlobalSettings});
            this.tlbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tlbSettings.Image")));
            this.tlbSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbSettings.Name = "tlbSettings";
            this.tlbSettings.Size = new System.Drawing.Size(45, 45);
            this.tlbSettings.Text = "toolStripDropDownButton1";
            this.tlbSettings.ToolTipText = "Settings";
            this.tlbSettings.Click += new System.EventHandler(this.ToolStripSettings_Click);
            // 
            // tsbGlobalSettings
            // 
            this.tsbGlobalSettings.Name = "tsbGlobalSettings";
            this.tsbGlobalSettings.Size = new System.Drawing.Size(153, 22);
            this.tsbGlobalSettings.Text = "Global Settings";
            // 
            // lblSeparator
            // 
            this.lblSeparator.AutoSize = false;
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(512, 45);
            // 
            // lblCursorPosition
            // 
            this.lblCursorPosition.AutoSize = false;
            this.lblCursorPosition.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblCursorPosition.Name = "lblCursorPosition";
            this.lblCursorPosition.Size = new System.Drawing.Size(128, 45);
            // 
            // tbcPageAreaLine
            // 
            this.tbcPageAreaLine.Controls.Add(this.tbpPages);
            this.tbcPageAreaLine.Controls.Add(this.tbpAreas);
            this.tbcPageAreaLine.Controls.Add(this.tbpLines);
            this.tbcPageAreaLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcPageAreaLine.HotTrack = true;
            this.tbcPageAreaLine.Location = new System.Drawing.Point(0, 79);
            this.tbcPageAreaLine.Margin = new System.Windows.Forms.Padding(4);
            this.tbcPageAreaLine.Name = "tbcPageAreaLine";
            this.tbcPageAreaLine.SelectedIndex = 0;
            this.tbcPageAreaLine.Size = new System.Drawing.Size(280, 1208);
            this.tbcPageAreaLine.TabIndex = 2;
            // 
            // tbpPages
            // 
            this.tbpPages.Location = new System.Drawing.Point(4, 25);
            this.tbpPages.Margin = new System.Windows.Forms.Padding(4);
            this.tbpPages.Name = "tbpPages";
            this.tbpPages.Padding = new System.Windows.Forms.Padding(4);
            this.tbpPages.Size = new System.Drawing.Size(272, 1179);
            this.tbpPages.TabIndex = 0;
            this.tbpPages.Text = "Pages";
            this.tbpPages.UseVisualStyleBackColor = true;
            // 
            // tbpAreas
            // 
            this.tbpAreas.Location = new System.Drawing.Point(4, 25);
            this.tbpAreas.Margin = new System.Windows.Forms.Padding(4);
            this.tbpAreas.Name = "tbpAreas";
            this.tbpAreas.Padding = new System.Windows.Forms.Padding(4);
            this.tbpAreas.Size = new System.Drawing.Size(272, 1179);
            this.tbpAreas.TabIndex = 1;
            this.tbpAreas.Text = "Areas";
            this.tbpAreas.UseVisualStyleBackColor = true;
            // 
            // tbpLines
            // 
            this.tbpLines.Location = new System.Drawing.Point(4, 25);
            this.tbpLines.Margin = new System.Windows.Forms.Padding(4);
            this.tbpLines.Name = "tbpLines";
            this.tbpLines.Size = new System.Drawing.Size(272, 1179);
            this.tbpLines.TabIndex = 2;
            this.tbpLines.Text = "Lines";
            this.tbpLines.UseVisualStyleBackColor = true;
            // 
            // sssMainForm
            // 
            this.sssMainForm.BackColor = System.Drawing.Color.Transparent;
            this.sssMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssFiller,
            this.tssLineSizeDecimal,
            this.tssLineSizeEnglish});
            this.sssMainForm.Location = new System.Drawing.Point(0, 1262);
            this.sssMainForm.Name = "sssMainForm";
            this.sssMainForm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sssMainForm.Size = new System.Drawing.Size(3285, 26);
            this.sssMainForm.TabIndex = 4;
            // 
            // tssFiller
            // 
            this.tssFiller.AutoSize = false;
            this.tssFiller.Name = "tssFiller";
            this.tssFiller.Size = new System.Drawing.Size(1024, 21);
            // 
            // tssLineSizeDecimal
            // 
            this.tssLineSizeDecimal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tssLineSizeDecimal.Name = "tssLineSizeDecimal";
            this.tssLineSizeDecimal.Size = new System.Drawing.Size(157, 21);
            this.tssLineSizeDecimal.Text = "toolStripStatusLabel1";
            // 
            // tssLineSizeEnglish
            // 
            this.tssLineSizeEnglish.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tssLineSizeEnglish.Name = "tssLineSizeEnglish";
            this.tssLineSizeEnglish.Size = new System.Drawing.Size(157, 21);
            this.tssLineSizeEnglish.Text = "toolStripStatusLabel1";
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(307, 165);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(1480, 780);
            this.axDrawingControl.TabIndex = 3;
            // 
            // FloorMaterialEstimatorBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(3285, 1288);
            this.Controls.Add(this.sssMainForm);
            this.Controls.Add(this.axDrawingControl);
            this.Controls.Add(this.tbcPageAreaLine);
            this.Controls.Add(this.tlsMainToolStrip);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "FloorMaterialEstimatorBaseForm";
            this.Text = "Bruun Estimating Floor Materials Estimator";
            this.Load += new System.EventHandler(this.FloorMaterialEstimatorBaseForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tlsMainToolStrip.ResumeLayout(false);
            this.tlsMainToolStrip.PerformLayout();
            this.tbcPageAreaLine.ResumeLayout(false);
            this.sssMainForm.ResumeLayout(false);
            this.sssMainForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadDrawingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton toolStripZoomIn;
        private System.Windows.Forms.ToolStripButton toolStripZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripFitWidth;
        private System.Windows.Forms.ToolStripButton toolStripFitHeight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripPanMode;
        private System.Windows.Forms.ToolStripButton toolStripDrawMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnAreaMode;
        private System.Windows.Forms.ToolStripButton btnLineMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tlbFilterAreas;
        private System.Windows.Forms.ToolStripButton tlsFilterLines;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnShowAreas;
        private System.Windows.Forms.ToolStripButton btnHideAreas;
        private System.Windows.Forms.ToolStripButton toolStripGreyAreas;
        private System.Windows.Forms.ToolStripButton btnShowImage;
        private System.Windows.Forms.ToolStripButton btnHideImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripEditAreas;
        private System.Windows.Forms.ToolStripButton toolStripEditLines;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnSetCustomScale;
        private System.Windows.Forms.ToolStripButton btnTapeMeasure;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripAlignToGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton toolStripCounters;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton toolStripSelectColors;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStrip tlsMainToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton tlbSettings;
        private System.Windows.Forms.ToolStripDropDownButton toolStripZoomPercent;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.TabControl tbcPageAreaLine;
        private System.Windows.Forms.TabPage tbpPages;
        private System.Windows.Forms.TabPage tbpAreas;
        private System.Windows.Forms.TabPage tbpLines;
        private AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.ToolStripButton btnDrawLine;
        private System.Windows.Forms.ToolStripButton btnDrawRectangle;
        private System.Windows.Forms.ToolStripButton btnDrawPolyline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton btnCursorDefault;
        private System.Windows.Forms.StatusStrip sssMainForm;
        private System.Windows.Forms.ToolStripStatusLabel tssFiller;
        private System.Windows.Forms.ToolStripStatusLabel tssLineSizeDecimal;
        private System.Windows.Forms.ToolStripStatusLabel tssLineSizeEnglish;
        private System.Windows.Forms.ToolStripMenuItem tsbGlobalSettings;
        private System.Windows.Forms.ToolStripLabel lblSeparator;
        internal System.Windows.Forms.ToolStripLabel lblCursorPosition;
    }
}

