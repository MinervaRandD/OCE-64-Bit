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
            this.musBaseMenuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExistingProject = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewBlankProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSaveProjectAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCreateImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLegend = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLegendLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLegendRight = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLegendNone = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnKeyboardAndMouseActions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblFinishName = new System.Windows.Forms.Label();
            this.pnlFinishColor = new System.Windows.Forms.Panel();
            this.btnChangeShapesToSelected = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFitCanvas = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPanMode = new System.Windows.Forms.ToolStripButton();
            this.btnDrawMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAreaDesignState = new System.Windows.Forms.ToolStripButton();
            this.btnLineDesignState = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFilterAreas = new System.Windows.Forms.ToolStripButton();
            this.btnFilterLines = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowAreas = new System.Windows.Forms.ToolStripButton();
            this.btnHideAreas = new System.Windows.Forms.ToolStripButton();
            this.btnShowImage = new System.Windows.Forms.ToolStripButton();
            this.btnHideImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditAreas = new System.Windows.Forms.ToolStripButton();
            this.btnEditLines = new System.Windows.Forms.ToolStripButton();
            this.btnSetCustomScale = new System.Windows.Forms.ToolStripButton();
            this.btnTapeMeasure = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCounters = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsMainToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnToolStripSave = new System.Windows.Forms.ToolStripButton();
            this.ddbZoomPercent = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSeamDesignState = new System.Windows.Forms.ToolStripButton();
            this.btnCombos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditSeams = new System.Windows.Forms.ToolStripButton();
            this.btnEditFieldGuides = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowFieldGuides = new System.Windows.Forms.ToolStripButton();
            this.btnHideFieldGuides = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteFieldGuides = new System.Windows.Forms.ToolStripButton();
            this.btnSnapToGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRedoSeamsAndCuts = new System.Windows.Forms.ToolStripButton();
            this.btnOversUnders = new System.Windows.Forms.ToolStripButton();
            this.btnSummaryReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDrawRectangle = new System.Windows.Forms.ToolStripButton();
            this.btnDrawPolyline = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFullScreen = new System.Windows.Forms.ToolStripButton();
            this.btnAreaEditSettings = new System.Windows.Forms.ToolStripButton();
            this.btnLineEditSettings = new System.Windows.Forms.ToolStripButton();
            this.btnShortcuts = new System.Windows.Forms.ToolStripButton();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDebug = new System.Windows.Forms.ToolStripButton();
            this.lblSeparator = new System.Windows.Forms.ToolStripLabel();
            this.lblCursorPosition = new System.Windows.Forms.ToolStripLabel();
            this.tbcPageAreaLine = new System.Windows.Forms.TabControl();
            this.tbpAreas = new System.Windows.Forms.TabPage();
            this.tbpLines = new System.Windows.Forms.TabPage();
            this.tbpSeams = new System.Windows.Forms.TabPage();
            this.sssMainForm = new System.Windows.Forms.StatusStrip();
            this.tlsDesignState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlsDrawingMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlsDrawingShape = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlsMouseXY = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCursorType = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssFiller = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLineSizeDecimal = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLineSizeEnglish = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlLineCommandPane = new System.Windows.Forms.Panel();
            this.grbLineModeCounter = new System.Windows.Forms.GroupBox();
            this.grbLayoutLineMode = new System.Windows.Forms.GroupBox();
            this.grbDoorTakeout = new System.Windows.Forms.GroupBox();
            this.btnDoorTakeoutShow = new System.Windows.Forms.Button();
            this.btnLayoutLineActivate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txbDoorTakeoutOther = new System.Windows.Forms.TextBox();
            this.rbnDoorTakeoutOther = new System.Windows.Forms.RadioButton();
            this.rbnDoorTakeout6Ft = new System.Windows.Forms.RadioButton();
            this.rbnDoorTakeout3Ft = new System.Windows.Forms.RadioButton();
            this.btnLayoutLineModeButtons = new System.Windows.Forms.Panel();
            this.btnLayoutLineDuplicate = new System.Windows.Forms.Button();
            this.btnLayoutLine2XMode = new System.Windows.Forms.Button();
            this.btnLayoutLineJump = new System.Windows.Forms.Button();
            this.btnLayoutLine1XMode = new System.Windows.Forms.Button();
            this.grbEditLineMode = new System.Windows.Forms.GroupBox();
            this.grbLineModeSelection = new System.Windows.Forms.GroupBox();
            this.btnLineDesignStateLayoutMode = new System.Windows.Forms.Button();
            this.btnLineDesignStateEditMode = new System.Windows.Forms.Button();
            this.btnSetLinesTo2X = new System.Windows.Forms.Button();
            this.btnEditLineApply = new System.Windows.Forms.Button();
            this.btnEditLineClear = new System.Windows.Forms.Button();
            this.btnEditLineUndo = new System.Windows.Forms.Button();
            this.ckbEditLineHighlightAndApply = new System.Windows.Forms.CheckBox();
            this.btnSetLinesTo1X = new System.Windows.Forms.Button();
            this.btnEditLineChangeLinesToSelected = new System.Windows.Forms.Button();
            this.btnEditLineDeleteLines = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAreaDesignStateLayoutMode = new System.Windows.Forms.Button();
            this.btnAreaDesignStateEditMode = new System.Windows.Forms.Button();
            this.grbEditAreaMode = new System.Windows.Forms.GroupBox();
            this.btnEditAreaApply = new System.Windows.Forms.Button();
            this.btnEditAreaClear = new System.Windows.Forms.Button();
            this.btnEditAreaUndo = new System.Windows.Forms.Button();
            this.ckbEditAreaHighlightAndApply = new System.Windows.Forms.CheckBox();
            this.btnEditShape = new System.Windows.Forms.Button();
            this.btnDeleteShapes = new System.Windows.Forms.Button();
            this.grbLayoutAreaMode = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ckbFixedWidth = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEmbeddLayoutAreas = new System.Windows.Forms.Button();
            this.btnLayoutAreaTakeOutAndFill = new System.Windows.Forms.Button();
            this.btnLayoutAreaTakeout = new System.Windows.Forms.Button();
            this.btnAreaDesignStateZeroLine = new System.Windows.Forms.Button();
            this.pnlSection1 = new System.Windows.Forms.Panel();
            this.btnCompleteShapeByIntersection = new System.Windows.Forms.Button();
            this.btnAreaDesignStateCompleteDrawing = new System.Windows.Forms.Button();
            this.pnlAreaCommandPane = new System.Windows.Forms.Panel();
            this.grbAreaModeCounter = new System.Windows.Forms.GroupBox();
            this.pnlSeamCommandPane = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ckbShowUnders = new System.Windows.Forms.CheckBox();
            this.ckbShowOvers = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckbShowCuts = new System.Windows.Forms.CheckBox();
            this.grbSeaming = new System.Windows.Forms.GroupBox();
            this.btnCenterSeamingToolInView = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nudMoveIncrement = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbnVirtualSeamsShowUnhideable = new System.Windows.Forms.RadioButton();
            this.rbnVirtualSeamsHideAll = new System.Windows.Forms.RadioButton();
            this.rbnVirtualSeamsShowAll = new System.Windows.Forms.RadioButton();
            this.grbNormalSeams = new System.Windows.Forms.GroupBox();
            this.rbnNormalSeamsHideAll = new System.Windows.Forms.RadioButton();
            this.rbnNormalSeamsShowUnHideable = new System.Windows.Forms.RadioButton();
            this.rbnNormalSeamsShowAll = new System.Windows.Forms.RadioButton();
            this.btnSubdivideRegion = new System.Windows.Forms.Button();
            this.btnSeamArea = new System.Windows.Forms.Button();
            this.btnSeamSingleLineToTool = new System.Windows.Forms.Button();
            this.btnAlignTool = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnShowSeamingTool = new System.Windows.Forms.Button();
            this.grbAreaSubdivisionControls = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbnSubdivideByArea = new System.Windows.Forms.RadioButton();
            this.rbnSubdivideByLine = new System.Windows.Forms.RadioButton();
            this.btnRemoveSubdivision = new System.Windows.Forms.Button();
            this.btnCancelSubdivision = new System.Windows.Forms.Button();
            this.btnCompleteSubdivision = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnSeamDesignStateSelectionMode = new System.Windows.Forms.Button();
            this.btnSeamDesignStateSubdivisionMode = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbnViewSeams = new System.Windows.Forms.RadioButton();
            this.rbnViewAreas = new System.Windows.Forms.RadioButton();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.pnlVisioPageControlCover = new System.Windows.Forms.Panel();
            this.tkbHScroll = new System.Windows.Forms.TrackBar();
            this.tkbVScroll = new System.Windows.Forms.TrackBar();
            this.ucSeamsView1 = new CanvasLib.Area_and_Seam_Views.UCSeamsView();
            this.ucAreasView = new CanvasLib.Area_and_Seam_Views.UCAreasView();
            this.cccLineMode = new CanvasLib.Counters.CounterControl();
            this.cccAreaMode = new CanvasLib.Counters.CounterControl();
            this.nudFixedWidthInches = new Utilities.NumericUpDownExtended();
            this.nudFixedWidthFeet = new Utilities.NumericUpDownExtended();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.musBaseMenuStrip.SuspendLayout();
            this.tlsMainToolStrip.SuspendLayout();
            this.tbcPageAreaLine.SuspendLayout();
            this.sssMainForm.SuspendLayout();
            this.pnlLineCommandPane.SuspendLayout();
            this.grbLineModeCounter.SuspendLayout();
            this.grbLayoutLineMode.SuspendLayout();
            this.grbDoorTakeout.SuspendLayout();
            this.btnLayoutLineModeButtons.SuspendLayout();
            this.grbLineModeSelection.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grbEditAreaMode.SuspendLayout();
            this.grbLayoutAreaMode.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlSection1.SuspendLayout();
            this.pnlAreaCommandPane.SuspendLayout();
            this.grbAreaModeCounter.SuspendLayout();
            this.pnlSeamCommandPane.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.grbSeaming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMoveIncrement)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.grbNormalSeams.SuspendLayout();
            this.grbAreaSubdivisionControls.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbHScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbVScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedWidthInches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedWidthFeet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.SuspendLayout();
            // 
            // musBaseMenuStrip
            // 
            this.musBaseMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.musBaseMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmLegend,
            this.btnHelp});
            this.musBaseMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.musBaseMenuStrip.Name = "musBaseMenuStrip";
            this.musBaseMenuStrip.Padding = new System.Windows.Forms.Padding(15, 4, 0, 4);
            this.musBaseMenuStrip.ShowItemToolTips = true;
            this.musBaseMenuStrip.Size = new System.Drawing.Size(3285, 29);
            this.musBaseMenuStrip.TabIndex = 0;
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewProject,
            this.btnExistingProject,
            this.btnNewBlankProject,
            this.toolStripSeparator14,
            this.btnSaveProject,
            this.btnSaveProjectAs,
            this.toolStripSeparator15,
            this.btnCreateImage,
            this.toolStripSeparator16,
            this.btnExit});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.ShowShortcutKeys = false;
            this.tsmFile.Size = new System.Drawing.Size(39, 21);
            this.tsmFile.Text = "File";
            // 
            // btnNewProject
            // 
            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.ShortcutKeyDisplayString = "Cntl+N";
            this.btnNewProject.Size = new System.Drawing.Size(213, 22);
            this.btnNewProject.Text = "New Project";
            this.btnNewProject.Click += new System.EventHandler(this.btnNewProject_Click);
            // 
            // btnExistingProject
            // 
            this.btnExistingProject.Name = "btnExistingProject";
            this.btnExistingProject.ShortcutKeyDisplayString = "Cntl+O";
            this.btnExistingProject.Size = new System.Drawing.Size(213, 22);
            this.btnExistingProject.Text = "Existing Project";
            this.btnExistingProject.Click += new System.EventHandler(this.btnExistingProject_Click);
            // 
            // btnNewBlankProject
            // 
            this.btnNewBlankProject.Name = "btnNewBlankProject";
            this.btnNewBlankProject.Size = new System.Drawing.Size(213, 22);
            this.btnNewBlankProject.Text = "New Blank Project";
            this.btnNewBlankProject.Click += new System.EventHandler(this.btnNewBlankProject_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(210, 6);
            // 
            // btnSaveProject
            // 
            this.btnSaveProject.Name = "btnSaveProject";
            this.btnSaveProject.ShortcutKeyDisplayString = "Cntl+S";
            this.btnSaveProject.Size = new System.Drawing.Size(213, 22);
            this.btnSaveProject.Text = "Save";
            this.btnSaveProject.ToolTipText = "Save Project";
            this.btnSaveProject.Click += new System.EventHandler(this.btnSaveProject_Click);
            // 
            // btnSaveProjectAs
            // 
            this.btnSaveProjectAs.Name = "btnSaveProjectAs";
            this.btnSaveProjectAs.Size = new System.Drawing.Size(213, 22);
            this.btnSaveProjectAs.Text = "Save As";
            this.btnSaveProjectAs.Click += new System.EventHandler(this.btnSaveProjectAs_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(210, 6);
            // 
            // btnCreateImage
            // 
            this.btnCreateImage.Name = "btnCreateImage";
            this.btnCreateImage.Size = new System.Drawing.Size(213, 22);
            this.btnCreateImage.Text = "Create Image";
            this.btnCreateImage.Click += new System.EventHandler(this.btnCreateImage_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(210, 6);
            // 
            // btnExit
            // 
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(213, 22);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tsmLegend
            // 
            this.tsmLegend.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLegendLeft,
            this.btnLegendRight,
            this.btnLegendNone});
            this.tsmLegend.Name = "tsmLegend";
            this.tsmLegend.Size = new System.Drawing.Size(63, 21);
            this.tsmLegend.Text = "Legend";
            // 
            // btnLegendLeft
            // 
            this.btnLegendLeft.Name = "btnLegendLeft";
            this.btnLegendLeft.Size = new System.Drawing.Size(108, 22);
            this.btnLegendLeft.Text = "Left";
            this.btnLegendLeft.Click += new System.EventHandler(this.BtnLegendLeft_Click);
            // 
            // btnLegendRight
            // 
            this.btnLegendRight.Name = "btnLegendRight";
            this.btnLegendRight.Size = new System.Drawing.Size(108, 22);
            this.btnLegendRight.Text = "Right";
            this.btnLegendRight.Click += new System.EventHandler(this.BtnLegendRight_Click);
            // 
            // btnLegendNone
            // 
            this.btnLegendNone.Name = "btnLegendNone";
            this.btnLegendNone.Size = new System.Drawing.Size(108, 22);
            this.btnLegendNone.Text = "None";
            this.btnLegendNone.Click += new System.EventHandler(this.BtnLegendNone_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAbout,
            this.btnKeyboardAndMouseActions});
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(47, 21);
            this.btnHelp.Text = "Help";
            // 
            // btnAbout
            // 
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(262, 22);
            this.btnAbout.Text = "About Floor Materials Estimator";
            // 
            // btnKeyboardAndMouseActions
            // 
            this.btnKeyboardAndMouseActions.Name = "btnKeyboardAndMouseActions";
            this.btnKeyboardAndMouseActions.Size = new System.Drawing.Size(262, 22);
            this.btnKeyboardAndMouseActions.Text = "Keyboard and Mouse Actions";
            this.btnKeyboardAndMouseActions.Click += new System.EventHandler(this.btnKeyboardAndMouseActions_Click);
            // 
            // lblFinishName
            // 
            this.lblFinishName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinishName.Location = new System.Drawing.Point(25, 50);
            this.lblFinishName.Name = "lblFinishName";
            this.lblFinishName.Size = new System.Drawing.Size(136, 22);
            this.lblFinishName.TabIndex = 2;
            this.lblFinishName.Text = "Finish-1";
            this.lblFinishName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.lblFinishName, "Current finish type");
            // 
            // pnlFinishColor
            // 
            this.pnlFinishColor.Location = new System.Drawing.Point(25, 77);
            this.pnlFinishColor.Name = "pnlFinishColor";
            this.pnlFinishColor.Size = new System.Drawing.Size(166, 26);
            this.pnlFinishColor.TabIndex = 1;
            this.toolTip1.SetToolTip(this.pnlFinishColor, "Current finish type");
            // 
            // btnChangeShapesToSelected
            // 
            this.btnChangeShapesToSelected.BackColor = System.Drawing.Color.Orange;
            this.btnChangeShapesToSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeShapesToSelected.Location = new System.Drawing.Point(25, 113);
            this.btnChangeShapesToSelected.Name = "btnChangeShapesToSelected";
            this.btnChangeShapesToSelected.Size = new System.Drawing.Size(169, 26);
            this.btnChangeShapesToSelected.TabIndex = 4;
            this.btnChangeShapesToSelected.Text = "Change finish type";
            this.toolTip1.SetToolTip(this.btnChangeShapesToSelected, "Change the finish type to the current finish type.");
            this.btnChangeShapesToSelected.UseVisualStyleBackColor = false;
            this.btnChangeShapesToSelected.Click += new System.EventHandler(this.btnEditAreaChangeShapesToSelected_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomIn.Image")));
            this.btnZoomIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Margin = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(36, 42);
            this.btnZoomIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnZoomIn.ToolTipText = "Zoom in";
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.Image")));
            this.btnZoomOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(36, 51);
            this.btnZoomOut.ToolTipText = "Zoom out";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // btnFitCanvas
            // 
            this.btnFitCanvas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFitCanvas.Image = ((System.Drawing.Image)(resources.GetObject("btnFitCanvas.Image")));
            this.btnFitCanvas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnFitCanvas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFitCanvas.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnFitCanvas.Name = "btnFitCanvas";
            this.btnFitCanvas.Size = new System.Drawing.Size(36, 51);
            this.btnFitCanvas.ToolTipText = "Fit to canvas";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // btnPanMode
            // 
            this.btnPanMode.CheckOnClick = true;
            this.btnPanMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPanMode.Image = ((System.Drawing.Image)(resources.GetObject("btnPanMode.Image")));
            this.btnPanMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPanMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPanMode.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnPanMode.Name = "btnPanMode";
            this.btnPanMode.Size = new System.Drawing.Size(36, 51);
            this.btnPanMode.ToolTipText = "Pan mode";
            this.btnPanMode.Click += new System.EventHandler(this.BtnPanMode_Click);
            // 
            // btnDrawMode
            // 
            this.btnDrawMode.Checked = true;
            this.btnDrawMode.CheckOnClick = true;
            this.btnDrawMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnDrawMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDrawMode.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawMode.Image")));
            this.btnDrawMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDrawMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrawMode.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnDrawMode.Name = "btnDrawMode";
            this.btnDrawMode.Size = new System.Drawing.Size(36, 51);
            this.btnDrawMode.ToolTipText = "Draw mode";
            this.btnDrawMode.Click += new System.EventHandler(this.BtnDrawMode_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // btnAreaDesignState
            // 
            this.btnAreaDesignState.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAreaDesignState.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAreaDesignState.ForeColor = System.Drawing.Color.Black;
            this.btnAreaDesignState.Image = ((System.Drawing.Image)(resources.GetObject("btnAreaDesignState.Image")));
            this.btnAreaDesignState.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAreaDesignState.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.btnAreaDesignState.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnAreaDesignState.Name = "btnAreaDesignState";
            this.btnAreaDesignState.Size = new System.Drawing.Size(36, 51);
            this.btnAreaDesignState.ToolTipText = "Select area design state";
            this.btnAreaDesignState.Click += new System.EventHandler(this.BtnAreaDesignState_Click);
            // 
            // btnLineDesignState
            // 
            this.btnLineDesignState.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLineDesignState.Image = ((System.Drawing.Image)(resources.GetObject("btnLineDesignState.Image")));
            this.btnLineDesignState.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLineDesignState.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLineDesignState.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnLineDesignState.Name = "btnLineDesignState";
            this.btnLineDesignState.Size = new System.Drawing.Size(36, 51);
            this.btnLineDesignState.ToolTipText = "Select line design state";
            this.btnLineDesignState.Click += new System.EventHandler(this.BtnLineDesignSate_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 54);
            // 
            // btnFilterAreas
            // 
            this.btnFilterAreas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterAreas.Image = ((System.Drawing.Image)(resources.GetObject("btnFilterAreas.Image")));
            this.btnFilterAreas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnFilterAreas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilterAreas.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnFilterAreas.Name = "btnFilterAreas";
            this.btnFilterAreas.Size = new System.Drawing.Size(36, 51);
            this.btnFilterAreas.ToolTipText = "Filter area types";
            // 
            // btnFilterLines
            // 
            this.btnFilterLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterLines.Image = ((System.Drawing.Image)(resources.GetObject("btnFilterLines.Image")));
            this.btnFilterLines.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnFilterLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilterLines.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnFilterLines.Name = "btnFilterLines";
            this.btnFilterLines.Size = new System.Drawing.Size(36, 51);
            this.btnFilterLines.ToolTipText = "Filter line types";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 54);
            // 
            // btnShowAreas
            // 
            this.btnShowAreas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowAreas.Enabled = false;
            this.btnShowAreas.Image = ((System.Drawing.Image)(resources.GetObject("btnShowAreas.Image")));
            this.btnShowAreas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnShowAreas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowAreas.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnShowAreas.Name = "btnShowAreas";
            this.btnShowAreas.Size = new System.Drawing.Size(36, 51);
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
            this.btnHideAreas.Size = new System.Drawing.Size(36, 51);
            this.btnHideAreas.ToolTipText = "Hide areas";
            this.btnHideAreas.Click += new System.EventHandler(this.BtnHideAreas_Click);
            // 
            // btnShowImage
            // 
            this.btnShowImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowImage.Enabled = false;
            this.btnShowImage.Image = ((System.Drawing.Image)(resources.GetObject("btnShowImage.Image")));
            this.btnShowImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnShowImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowImage.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnShowImage.Name = "btnShowImage";
            this.btnShowImage.Size = new System.Drawing.Size(36, 51);
            this.btnShowImage.ToolTipText = "Show image";
            this.btnShowImage.Click += new System.EventHandler(this.BtnShowImage_Click);
            // 
            // btnHideImage
            // 
            this.btnHideImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHideImage.Image = ((System.Drawing.Image)(resources.GetObject("btnHideImage.Image")));
            this.btnHideImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnHideImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHideImage.Name = "btnHideImage";
            this.btnHideImage.Size = new System.Drawing.Size(36, 51);
            this.btnHideImage.ToolTipText = "Hide image";
            this.btnHideImage.Click += new System.EventHandler(this.BtnHideImage_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 54);
            // 
            // btnEditAreas
            // 
            this.btnEditAreas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditAreas.Image = ((System.Drawing.Image)(resources.GetObject("btnEditAreas.Image")));
            this.btnEditAreas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditAreas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditAreas.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnEditAreas.Name = "btnEditAreas";
            this.btnEditAreas.Size = new System.Drawing.Size(36, 51);
            this.btnEditAreas.ToolTipText = "Edit finishes";
            this.btnEditAreas.Click += new System.EventHandler(this.btnEditAreas_Click);
            // 
            // btnEditLines
            // 
            this.btnEditLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditLines.Image = ((System.Drawing.Image)(resources.GetObject("btnEditLines.Image")));
            this.btnEditLines.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditLines.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnEditLines.Name = "btnEditLines";
            this.btnEditLines.Size = new System.Drawing.Size(36, 51);
            this.btnEditLines.ToolTipText = "Edit lines";
            this.btnEditLines.Click += new System.EventHandler(this.btnEditLines_Click);
            // 
            // btnSetCustomScale
            // 
            this.btnSetCustomScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSetCustomScale.ForeColor = System.Drawing.Color.Transparent;
            this.btnSetCustomScale.Image = ((System.Drawing.Image)(resources.GetObject("btnSetCustomScale.Image")));
            this.btnSetCustomScale.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSetCustomScale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetCustomScale.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnSetCustomScale.Name = "btnSetCustomScale";
            this.btnSetCustomScale.Size = new System.Drawing.Size(36, 51);
            this.btnSetCustomScale.ToolTipText = "Set custom  scale";
            this.btnSetCustomScale.Click += new System.EventHandler(this.btnSetScale_Click);
            // 
            // btnTapeMeasure
            // 
            this.btnTapeMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTapeMeasure.Image = ((System.Drawing.Image)(resources.GetObject("btnTapeMeasure.Image")));
            this.btnTapeMeasure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnTapeMeasure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTapeMeasure.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnTapeMeasure.Name = "btnTapeMeasure";
            this.btnTapeMeasure.Size = new System.Drawing.Size(36, 51);
            this.btnTapeMeasure.ToolTipText = "Lline measuring tool";
            this.btnTapeMeasure.Click += new System.EventHandler(this.btnTapeMeasure_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 54);
            // 
            // btnCounters
            // 
            this.btnCounters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCounters.Image = ((System.Drawing.Image)(resources.GetObject("btnCounters.Image")));
            this.btnCounters.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCounters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCounters.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnCounters.Name = "btnCounters";
            this.btnCounters.Size = new System.Drawing.Size(36, 51);
            this.btnCounters.Text = "toolStripButton22";
            this.btnCounters.ToolTipText = "Activate Counters";
            this.btnCounters.Click += new System.EventHandler(this.ToolStripCounters_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 54);
            // 
            // tlsMainToolStrip
            // 
            this.tlsMainToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tlsMainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnToolStripSave,
            this.btnZoomIn,
            this.btnZoomOut,
            this.ddbZoomPercent,
            this.toolStripSeparator1,
            this.btnFitCanvas,
            this.toolStripSeparator2,
            this.btnPanMode,
            this.btnDrawMode,
            this.toolStripSeparator3,
            this.btnAreaDesignState,
            this.btnLineDesignState,
            this.btnSeamDesignState,
            this.btnCombos,
            this.toolStripSeparator4,
            this.btnFilterAreas,
            this.btnFilterLines,
            this.toolStripSeparator5,
            this.btnShowAreas,
            this.btnHideAreas,
            this.toolStripSeparator9,
            this.btnEditAreas,
            this.btnEditLines,
            this.btnEditSeams,
            this.btnEditFieldGuides,
            this.toolStripSeparator10,
            this.btnShowImage,
            this.btnHideImage,
            this.toolStripSeparator17,
            this.btnShowFieldGuides,
            this.btnHideFieldGuides,
            this.btnDeleteFieldGuides,
            this.toolStripSeparator6,
            this.btnSnapToGrid,
            this.toolStripSeparator18,
            this.btnSetCustomScale,
            this.btnTapeMeasure,
            this.toolStripSeparator8,
            this.btnRedoSeamsAndCuts,
            this.btnOversUnders,
            this.btnSummaryReport,
            this.toolStripSeparator13,
            this.btnCounters,
            this.toolStripSeparator11,
            this.btnDrawRectangle,
            this.btnDrawPolyline,
            this.toolStripSeparator12,
            this.btnFullScreen,
            this.btnAreaEditSettings,
            this.btnLineEditSettings,
            this.btnShortcuts,
            this.btnSettings,
            this.toolStripSeparator7,
            this.btnDebug,
            this.lblSeparator,
            this.lblCursorPosition});
            this.tlsMainToolStrip.Location = new System.Drawing.Point(0, 29);
            this.tlsMainToolStrip.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.tlsMainToolStrip.Name = "tlsMainToolStrip";
            this.tlsMainToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.tlsMainToolStrip.Size = new System.Drawing.Size(3285, 54);
            this.tlsMainToolStrip.TabIndex = 1;
            // 
            // btnToolStripSave
            // 
            this.btnToolStripSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToolStripSave.Image = ((System.Drawing.Image)(resources.GetObject("btnToolStripSave.Image")));
            this.btnToolStripSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnToolStripSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToolStripSave.Name = "btnToolStripSave";
            this.btnToolStripSave.Size = new System.Drawing.Size(36, 51);
            this.btnToolStripSave.Text = "Save";
            this.btnToolStripSave.Click += new System.EventHandler(this.BtnToolStripSave_Click);
            // 
            // ddbZoomPercent
            // 
            this.ddbZoomPercent.AutoSize = false;
            this.ddbZoomPercent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ddbZoomPercent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem7,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem11,
            this.toolStripMenuItem10,
            this.customToolStripMenuItem});
            this.ddbZoomPercent.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddbZoomPercent.Image = ((System.Drawing.Image)(resources.GetObject("ddbZoomPercent.Image")));
            this.ddbZoomPercent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbZoomPercent.Name = "ddbZoomPercent";
            this.ddbZoomPercent.Size = new System.Drawing.Size(64, 45);
            this.ddbZoomPercent.ToolTipText = "Select zoom percent";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem1.Text = "25%";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem2.Text = "50%";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem3.Text = "75%";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem4.Text = "100%";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem7.Text = "115%";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem5.Text = "125%";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem6.Text = "150%";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem8.Text = "200%";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem9.Text = "400%";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem11.Text = "600%";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem10.Text = "800%";
            // 
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.customToolStripMenuItem.Text = "Custom";
            // 
            // btnSeamDesignState
            // 
            this.btnSeamDesignState.AutoSize = false;
            this.btnSeamDesignState.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSeamDesignState.Image = ((System.Drawing.Image)(resources.GetObject("btnSeamDesignState.Image")));
            this.btnSeamDesignState.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSeamDesignState.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSeamDesignState.Name = "btnSeamDesignState";
            this.btnSeamDesignState.Size = new System.Drawing.Size(36, 45);
            this.btnSeamDesignState.ToolTipText = "Select seam design state";
            this.btnSeamDesignState.Click += new System.EventHandler(this.BtnSeamDesignState_Click);
            // 
            // btnCombos
            // 
            this.btnCombos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCombos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCombos.Image = ((System.Drawing.Image)(resources.GetObject("btnCombos.Image")));
            this.btnCombos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCombos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCombos.Name = "btnCombos";
            this.btnCombos.Size = new System.Drawing.Size(36, 51);
            this.btnCombos.ToolTipText = "Create combinations";
            this.btnCombos.Click += new System.EventHandler(this.BtnCombos_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 54);
            // 
            // btnEditSeams
            // 
            this.btnEditSeams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditSeams.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditSeams.Image = ((System.Drawing.Image)(resources.GetObject("btnEditSeams.Image")));
            this.btnEditSeams.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditSeams.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditSeams.Name = "btnEditSeams";
            this.btnEditSeams.Size = new System.Drawing.Size(36, 51);
            this.btnEditSeams.ToolTipText = "Edit seams";
            this.btnEditSeams.Click += new System.EventHandler(this.btnEditSeams_Click);
            // 
            // btnEditFieldGuides
            // 
            this.btnEditFieldGuides.AutoSize = false;
            this.btnEditFieldGuides.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditFieldGuides.Image = ((System.Drawing.Image)(resources.GetObject("btnEditFieldGuides.Image")));
            this.btnEditFieldGuides.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditFieldGuides.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditFieldGuides.Name = "btnEditFieldGuides";
            this.btnEditFieldGuides.Size = new System.Drawing.Size(45, 45);
            this.btnEditFieldGuides.ToolTipText = "Edit Field Guides";
            this.btnEditFieldGuides.Click += new System.EventHandler(this.btnEditFieldGuides_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 54);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 54);
            // 
            // btnShowFieldGuides
            // 
            this.btnShowFieldGuides.AutoSize = false;
            this.btnShowFieldGuides.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowFieldGuides.Image = ((System.Drawing.Image)(resources.GetObject("btnShowFieldGuides.Image")));
            this.btnShowFieldGuides.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnShowFieldGuides.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowFieldGuides.Name = "btnShowFieldGuides";
            this.btnShowFieldGuides.Size = new System.Drawing.Size(45, 45);
            this.btnShowFieldGuides.ToolTipText = "Show Field Guides";
            // 
            // btnHideFieldGuides
            // 
            this.btnHideFieldGuides.AutoSize = false;
            this.btnHideFieldGuides.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHideFieldGuides.Image = ((System.Drawing.Image)(resources.GetObject("btnHideFieldGuides.Image")));
            this.btnHideFieldGuides.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnHideFieldGuides.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHideFieldGuides.Name = "btnHideFieldGuides";
            this.btnHideFieldGuides.Size = new System.Drawing.Size(44, 45);
            this.btnHideFieldGuides.ToolTipText = "Hide  Field Guides";
            // 
            // btnDeleteFieldGuides
            // 
            this.btnDeleteFieldGuides.AutoSize = false;
            this.btnDeleteFieldGuides.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteFieldGuides.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteFieldGuides.Image")));
            this.btnDeleteFieldGuides.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteFieldGuides.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteFieldGuides.Name = "btnDeleteFieldGuides";
            this.btnDeleteFieldGuides.Size = new System.Drawing.Size(45, 45);
            this.btnDeleteFieldGuides.ToolTipText = "Delete Field Guides";
            // 
            // btnSnapToGrid
            // 
            this.btnSnapToGrid.AutoSize = false;
            this.btnSnapToGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSnapToGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnSnapToGrid.Image")));
            this.btnSnapToGrid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSnapToGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSnapToGrid.Name = "btnSnapToGrid";
            this.btnSnapToGrid.Size = new System.Drawing.Size(37, 51);
            this.btnSnapToGrid.Text = "Snap to Grid";
            this.btnSnapToGrid.Click += new System.EventHandler(this.BtnSnapToGrid_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(6, 54);
            // 
            // btnRedoSeamsAndCuts
            // 
            this.btnRedoSeamsAndCuts.AutoSize = false;
            this.btnRedoSeamsAndCuts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRedoSeamsAndCuts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRedoSeamsAndCuts.Image = ((System.Drawing.Image)(resources.GetObject("btnRedoSeamsAndCuts.Image")));
            this.btnRedoSeamsAndCuts.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRedoSeamsAndCuts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRedoSeamsAndCuts.Name = "btnRedoSeamsAndCuts";
            this.btnRedoSeamsAndCuts.Size = new System.Drawing.Size(45, 45);
            this.btnRedoSeamsAndCuts.ToolTipText = "Redo seams and cuts";
            this.btnRedoSeamsAndCuts.Click += new System.EventHandler(this.btnRedoSeamsAndCuts_Click);
            // 
            // btnOversUnders
            // 
            this.btnOversUnders.AutoSize = false;
            this.btnOversUnders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOversUnders.Image = ((System.Drawing.Image)(resources.GetObject("btnOversUnders.Image")));
            this.btnOversUnders.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnOversUnders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOversUnders.Name = "btnOversUnders";
            this.btnOversUnders.Size = new System.Drawing.Size(52, 51);
            this.btnOversUnders.ToolTipText = "Overs / Unders";
            this.btnOversUnders.Click += new System.EventHandler(this.BtnOversUnders_Click);
            // 
            // btnSummaryReport
            // 
            this.btnSummaryReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSummaryReport.Image = ((System.Drawing.Image)(resources.GetObject("btnSummaryReport.Image")));
            this.btnSummaryReport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSummaryReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSummaryReport.Name = "btnSummaryReport";
            this.btnSummaryReport.Size = new System.Drawing.Size(36, 51);
            this.btnSummaryReport.Text = "Summary Report";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 54);
            // 
            // btnDrawRectangle
            // 
            this.btnDrawRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDrawRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawRectangle.Image")));
            this.btnDrawRectangle.ImageTransparentColor = System.Drawing.Color.White;
            this.btnDrawRectangle.Name = "btnDrawRectangle";
            this.btnDrawRectangle.Size = new System.Drawing.Size(23, 51);
            this.btnDrawRectangle.Text = "toolStripButton2";
            this.btnDrawRectangle.ToolTipText = "Rectangle Mode Not Activated";
            this.btnDrawRectangle.Click += new System.EventHandler(this.btnDrawRectangle_Click);
            // 
            // btnDrawPolyline
            // 
            this.btnDrawPolyline.Checked = true;
            this.btnDrawPolyline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnDrawPolyline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDrawPolyline.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawPolyline.Image")));
            this.btnDrawPolyline.ImageTransparentColor = System.Drawing.Color.White;
            this.btnDrawPolyline.Name = "btnDrawPolyline";
            this.btnDrawPolyline.Size = new System.Drawing.Size(23, 51);
            this.btnDrawPolyline.ToolTipText = "Polyline mode";
            this.btnDrawPolyline.Click += new System.EventHandler(this.btnDrawPolyline_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 54);
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.AutoSize = false;
            this.btnFullScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFullScreen.Image = ((System.Drawing.Image)(resources.GetObject("btnFullScreen.Image")));
            this.btnFullScreen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(48, 45);
            this.btnFullScreen.Text = "Full Screen";
            this.btnFullScreen.ToolTipText = "Full screen mode";
            this.btnFullScreen.Click += new System.EventHandler(this.BtnFullScreen_Click);
            // 
            // btnAreaEditSettings
            // 
            this.btnAreaEditSettings.AutoSize = false;
            this.btnAreaEditSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAreaEditSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnAreaEditSettings.Image")));
            this.btnAreaEditSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAreaEditSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAreaEditSettings.Name = "btnAreaEditSettings";
            this.btnAreaEditSettings.Size = new System.Drawing.Size(42, 42);
            this.btnAreaEditSettings.ToolTipText = "Edit area settings";
            this.btnAreaEditSettings.Click += new System.EventHandler(this.btnAreaEditSettings_Click);
            // 
            // btnLineEditSettings
            // 
            this.btnLineEditSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLineEditSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnLineEditSettings.Image")));
            this.btnLineEditSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLineEditSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLineEditSettings.Name = "btnLineEditSettings";
            this.btnLineEditSettings.Size = new System.Drawing.Size(36, 51);
            this.btnLineEditSettings.Text = "Line Edit Settings";
            this.btnLineEditSettings.Click += new System.EventHandler(this.btnLineEditSettings_Click);
            // 
            // btnShortcuts
            // 
            this.btnShortcuts.AutoSize = false;
            this.btnShortcuts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShortcuts.Image = ((System.Drawing.Image)(resources.GetObject("btnShortcuts.Image")));
            this.btnShortcuts.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnShortcuts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShortcuts.Name = "btnShortcuts";
            this.btnShortcuts.Size = new System.Drawing.Size(45, 45);
            this.btnShortcuts.ToolTipText = "Shortcuts";
            this.btnShortcuts.Click += new System.EventHandler(this.BtnShortcuts_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.AutoSize = false;
            this.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(45, 45);
            this.btnSettings.ToolTipText = "Settings";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 54);
            // 
            // btnDebug
            // 
            this.btnDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDebug.Image = ((System.Drawing.Image)(resources.GetObject("btnDebug.Image")));
            this.btnDebug.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(37, 51);
            this.btnDebug.Visible = false;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // lblSeparator
            // 
            this.lblSeparator.AutoSize = false;
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(340, 45);
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
            this.tbcPageAreaLine.Controls.Add(this.tbpAreas);
            this.tbcPageAreaLine.Controls.Add(this.tbpLines);
            this.tbcPageAreaLine.Controls.Add(this.tbpSeams);
            this.tbcPageAreaLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcPageAreaLine.HotTrack = true;
            this.tbcPageAreaLine.Location = new System.Drawing.Point(0, 79);
            this.tbcPageAreaLine.Margin = new System.Windows.Forms.Padding(4);
            this.tbcPageAreaLine.Name = "tbcPageAreaLine";
            this.tbcPageAreaLine.SelectedIndex = 0;
            this.tbcPageAreaLine.Size = new System.Drawing.Size(268, 1208);
            this.tbcPageAreaLine.TabIndex = 0;
            // 
            // tbpAreas
            // 
            this.tbpAreas.Location = new System.Drawing.Point(4, 25);
            this.tbpAreas.Margin = new System.Windows.Forms.Padding(4);
            this.tbpAreas.Name = "tbpAreas";
            this.tbpAreas.Padding = new System.Windows.Forms.Padding(4);
            this.tbpAreas.Size = new System.Drawing.Size(260, 1179);
            this.tbpAreas.TabIndex = 1;
            this.tbpAreas.Text = "Areas";
            this.tbpAreas.UseVisualStyleBackColor = true;
            // 
            // tbpLines
            // 
            this.tbpLines.Location = new System.Drawing.Point(4, 25);
            this.tbpLines.Margin = new System.Windows.Forms.Padding(4);
            this.tbpLines.Name = "tbpLines";
            this.tbpLines.Size = new System.Drawing.Size(260, 1179);
            this.tbpLines.TabIndex = 2;
            this.tbpLines.Text = "Lines";
            this.tbpLines.UseVisualStyleBackColor = true;
            // 
            // tbpSeams
            // 
            this.tbpSeams.Location = new System.Drawing.Point(4, 25);
            this.tbpSeams.Name = "tbpSeams";
            this.tbpSeams.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSeams.Size = new System.Drawing.Size(260, 1179);
            this.tbpSeams.TabIndex = 3;
            this.tbpSeams.Text = "Seams";
            this.tbpSeams.UseVisualStyleBackColor = true;
            // 
            // sssMainForm
            // 
            this.sssMainForm.BackColor = System.Drawing.Color.Transparent;
            this.sssMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsDesignState,
            this.tlsDrawingMode,
            this.tlsDrawingShape,
            this.tlsMouseXY,
            this.lblCursorType,
            this.tssFiller,
            this.tssLineSizeDecimal,
            this.tssLineSizeEnglish});
            this.sssMainForm.Location = new System.Drawing.Point(0, 1262);
            this.sssMainForm.Name = "sssMainForm";
            this.sssMainForm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sssMainForm.Size = new System.Drawing.Size(3285, 26);
            this.sssMainForm.TabIndex = 4;
            // 
            // tlsDesignState
            // 
            this.tlsDesignState.AutoSize = false;
            this.tlsDesignState.Name = "tlsDesignState";
            this.tlsDesignState.Size = new System.Drawing.Size(200, 21);
            this.tlsDesignState.Text = "Design State";
            // 
            // tlsDrawingMode
            // 
            this.tlsDrawingMode.AutoSize = false;
            this.tlsDrawingMode.Name = "tlsDrawingMode";
            this.tlsDrawingMode.Size = new System.Drawing.Size(200, 21);
            this.tlsDrawingMode.Text = "tlsDrawingShape";
            // 
            // tlsDrawingShape
            // 
            this.tlsDrawingShape.AutoSize = false;
            this.tlsDrawingShape.Name = "tlsDrawingShape";
            this.tlsDrawingShape.Size = new System.Drawing.Size(128, 21);
            this.tlsDrawingShape.Text = "toolStripStatusLabel1";
            // 
            // tlsMouseXY
            // 
            this.tlsMouseXY.AutoSize = false;
            this.tlsMouseXY.Name = "tlsMouseXY";
            this.tlsMouseXY.Size = new System.Drawing.Size(128, 21);
            this.tlsMouseXY.Text = "tlsMouseXY";
            // 
            // lblCursorType
            // 
            this.lblCursorType.AutoSize = false;
            this.lblCursorType.Name = "lblCursorType";
            this.lblCursorType.Size = new System.Drawing.Size(80, 21);
            // 
            // tssFiller
            // 
            this.tssFiller.AutoSize = false;
            this.tssFiller.Name = "tssFiller";
            this.tssFiller.Size = new System.Drawing.Size(300, 21);
            // 
            // tssLineSizeDecimal
            // 
            this.tssLineSizeDecimal.AutoSize = false;
            this.tssLineSizeDecimal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tssLineSizeDecimal.Name = "tssLineSizeDecimal";
            this.tssLineSizeDecimal.Size = new System.Drawing.Size(128, 21);
            this.tssLineSizeDecimal.Text = "toolStripStatusLabel1";
            // 
            // tssLineSizeEnglish
            // 
            this.tssLineSizeEnglish.AutoSize = false;
            this.tssLineSizeEnglish.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tssLineSizeEnglish.Name = "tssLineSizeEnglish";
            this.tssLineSizeEnglish.Size = new System.Drawing.Size(128, 21);
            this.tssLineSizeEnglish.Text = "toolStripStatusLabel1";
            // 
            // pnlLineCommandPane
            // 
            this.pnlLineCommandPane.AutoScroll = true;
            this.pnlLineCommandPane.AutoScrollMinSize = new System.Drawing.Size(268, 1400);
            this.pnlLineCommandPane.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlLineCommandPane.Controls.Add(this.grbLineModeCounter);
            this.pnlLineCommandPane.Controls.Add(this.grbLayoutLineMode);
            this.pnlLineCommandPane.Controls.Add(this.grbEditLineMode);
            this.pnlLineCommandPane.Controls.Add(this.grbLineModeSelection);
            this.pnlLineCommandPane.Location = new System.Drawing.Point(1331, 86);
            this.pnlLineCommandPane.Name = "pnlLineCommandPane";
            this.pnlLineCommandPane.Size = new System.Drawing.Size(268, 1124);
            this.pnlLineCommandPane.TabIndex = 6;
            // 
            // grbLineModeCounter
            // 
            this.grbLineModeCounter.Controls.Add(this.cccLineMode);
            this.grbLineModeCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbLineModeCounter.Location = new System.Drawing.Point(13, 756);
            this.grbLineModeCounter.Name = "grbLineModeCounter";
            this.grbLineModeCounter.Size = new System.Drawing.Size(231, 215);
            this.grbLineModeCounter.TabIndex = 12;
            this.grbLineModeCounter.TabStop = false;
            this.grbLineModeCounter.Text = "Counters";
            // 
            // grbLayoutLineMode
            // 
            this.grbLayoutLineMode.Controls.Add(this.grbDoorTakeout);
            this.grbLayoutLineMode.Controls.Add(this.btnLayoutLineModeButtons);
            this.grbLayoutLineMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbLayoutLineMode.Location = new System.Drawing.Point(8, 73);
            this.grbLayoutLineMode.Name = "grbLayoutLineMode";
            this.grbLayoutLineMode.Size = new System.Drawing.Size(228, 342);
            this.grbLayoutLineMode.TabIndex = 11;
            this.grbLayoutLineMode.TabStop = false;
            this.grbLayoutLineMode.Text = "Layout Line Mode";
            // 
            // grbDoorTakeout
            // 
            this.grbDoorTakeout.Controls.Add(this.btnDoorTakeoutShow);
            this.grbDoorTakeout.Controls.Add(this.btnLayoutLineActivate);
            this.grbDoorTakeout.Controls.Add(this.label2);
            this.grbDoorTakeout.Controls.Add(this.txbDoorTakeoutOther);
            this.grbDoorTakeout.Controls.Add(this.rbnDoorTakeoutOther);
            this.grbDoorTakeout.Controls.Add(this.rbnDoorTakeout6Ft);
            this.grbDoorTakeout.Controls.Add(this.rbnDoorTakeout3Ft);
            this.grbDoorTakeout.Location = new System.Drawing.Point(16, 176);
            this.grbDoorTakeout.Name = "grbDoorTakeout";
            this.grbDoorTakeout.Size = new System.Drawing.Size(198, 149);
            this.grbDoorTakeout.TabIndex = 7;
            this.grbDoorTakeout.TabStop = false;
            this.grbDoorTakeout.Text = "Door Take-Out";
            // 
            // btnDoorTakeoutShow
            // 
            this.btnDoorTakeoutShow.BackColor = System.Drawing.SystemColors.Control;
            this.btnDoorTakeoutShow.Location = new System.Drawing.Point(96, 70);
            this.btnDoorTakeoutShow.Name = "btnDoorTakeoutShow";
            this.btnDoorTakeoutShow.Size = new System.Drawing.Size(85, 23);
            this.btnDoorTakeoutShow.TabIndex = 6;
            this.btnDoorTakeoutShow.Text = "Hide";
            this.btnDoorTakeoutShow.UseVisualStyleBackColor = false;
            this.btnDoorTakeoutShow.Click += new System.EventHandler(this.btnLayoutLineHide_Click);
            // 
            // btnLayoutLineActivate
            // 
            this.btnLayoutLineActivate.BackColor = System.Drawing.SystemColors.Control;
            this.btnLayoutLineActivate.Location = new System.Drawing.Point(96, 32);
            this.btnLayoutLineActivate.Name = "btnLayoutLineActivate";
            this.btnLayoutLineActivate.Size = new System.Drawing.Size(85, 23);
            this.btnLayoutLineActivate.TabIndex = 5;
            this.btnLayoutLineActivate.Text = "Activate";
            this.btnLayoutLineActivate.UseVisualStyleBackColor = false;
            this.btnLayoutLineActivate.Click += new System.EventHandler(this.btnLayoutLineActivate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(150, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "ft";
            // 
            // txbDoorTakeoutOther
            // 
            this.txbDoorTakeoutOther.Location = new System.Drawing.Point(99, 113);
            this.txbDoorTakeoutOther.Name = "txbDoorTakeoutOther";
            this.txbDoorTakeoutOther.Size = new System.Drawing.Size(35, 22);
            this.txbDoorTakeoutOther.TabIndex = 3;
            this.txbDoorTakeoutOther.TextChanged += new System.EventHandler(this.txbDoorTakeoutOther_TextChanged);
            // 
            // rbnDoorTakeoutOther
            // 
            this.rbnDoorTakeoutOther.AutoSize = true;
            this.rbnDoorTakeoutOther.Location = new System.Drawing.Point(28, 112);
            this.rbnDoorTakeoutOther.Name = "rbnDoorTakeoutOther";
            this.rbnDoorTakeoutOther.Size = new System.Drawing.Size(58, 20);
            this.rbnDoorTakeoutOther.TabIndex = 2;
            this.rbnDoorTakeoutOther.Text = "Other";
            this.rbnDoorTakeoutOther.UseVisualStyleBackColor = true;
            // 
            // rbnDoorTakeout6Ft
            // 
            this.rbnDoorTakeout6Ft.AutoSize = true;
            this.rbnDoorTakeout6Ft.Location = new System.Drawing.Point(28, 72);
            this.rbnDoorTakeout6Ft.Name = "rbnDoorTakeout6Ft";
            this.rbnDoorTakeout6Ft.Size = new System.Drawing.Size(42, 20);
            this.rbnDoorTakeout6Ft.TabIndex = 1;
            this.rbnDoorTakeout6Ft.Text = "6 ft";
            this.rbnDoorTakeout6Ft.UseVisualStyleBackColor = true;
            // 
            // rbnDoorTakeout3Ft
            // 
            this.rbnDoorTakeout3Ft.AutoSize = true;
            this.rbnDoorTakeout3Ft.Checked = true;
            this.rbnDoorTakeout3Ft.Location = new System.Drawing.Point(28, 35);
            this.rbnDoorTakeout3Ft.Name = "rbnDoorTakeout3Ft";
            this.rbnDoorTakeout3Ft.Size = new System.Drawing.Size(42, 20);
            this.rbnDoorTakeout3Ft.TabIndex = 0;
            this.rbnDoorTakeout3Ft.TabStop = true;
            this.rbnDoorTakeout3Ft.Text = "3 ft";
            this.rbnDoorTakeout3Ft.UseVisualStyleBackColor = true;
            // 
            // btnLayoutLineModeButtons
            // 
            this.btnLayoutLineModeButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnLayoutLineModeButtons.Controls.Add(this.btnLayoutLineDuplicate);
            this.btnLayoutLineModeButtons.Controls.Add(this.btnLayoutLine2XMode);
            this.btnLayoutLineModeButtons.Controls.Add(this.btnLayoutLineJump);
            this.btnLayoutLineModeButtons.Controls.Add(this.btnLayoutLine1XMode);
            this.btnLayoutLineModeButtons.Location = new System.Drawing.Point(17, 24);
            this.btnLayoutLineModeButtons.Name = "btnLayoutLineModeButtons";
            this.btnLayoutLineModeButtons.Size = new System.Drawing.Size(196, 146);
            this.btnLayoutLineModeButtons.TabIndex = 1;
            // 
            // btnLayoutLineDuplicate
            // 
            this.btnLayoutLineDuplicate.BackColor = System.Drawing.SystemColors.Control;
            this.btnLayoutLineDuplicate.Enabled = false;
            this.btnLayoutLineDuplicate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLayoutLineDuplicate.Location = new System.Drawing.Point(36, 109);
            this.btnLayoutLineDuplicate.Name = "btnLayoutLineDuplicate";
            this.btnLayoutLineDuplicate.Size = new System.Drawing.Size(111, 26);
            this.btnLayoutLineDuplicate.TabIndex = 3;
            this.btnLayoutLineDuplicate.Text = "Duplicate";
            this.btnLayoutLineDuplicate.UseVisualStyleBackColor = false;
            this.btnLayoutLineDuplicate.Click += new System.EventHandler(this.btnLayoutLineDuplicate_Click);
            // 
            // btnLayoutLine2XMode
            // 
            this.btnLayoutLine2XMode.BackColor = System.Drawing.Color.Orange;
            this.btnLayoutLine2XMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLayoutLine2XMode.Location = new System.Drawing.Point(36, 73);
            this.btnLayoutLine2XMode.Name = "btnLayoutLine2XMode";
            this.btnLayoutLine2XMode.Size = new System.Drawing.Size(111, 26);
            this.btnLayoutLine2XMode.TabIndex = 2;
            this.btnLayoutLine2XMode.Text = "2X";
            this.btnLayoutLine2XMode.UseVisualStyleBackColor = false;
            this.btnLayoutLine2XMode.Click += new System.EventHandler(this.btnLayoutLine2XMode_Click);
            // 
            // btnLayoutLineJump
            // 
            this.btnLayoutLineJump.BackColor = System.Drawing.SystemColors.Control;
            this.btnLayoutLineJump.Enabled = false;
            this.btnLayoutLineJump.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLayoutLineJump.Location = new System.Drawing.Point(38, 3);
            this.btnLayoutLineJump.Name = "btnLayoutLineJump";
            this.btnLayoutLineJump.Size = new System.Drawing.Size(109, 26);
            this.btnLayoutLineJump.TabIndex = 0;
            this.btnLayoutLineJump.Text = "Jump";
            this.btnLayoutLineJump.UseVisualStyleBackColor = false;
            this.btnLayoutLineJump.Click += new System.EventHandler(this.btnLayoutLineJump_Click);
            // 
            // btnLayoutLine1XMode
            // 
            this.btnLayoutLine1XMode.BackColor = System.Drawing.SystemColors.Control;
            this.btnLayoutLine1XMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLayoutLine1XMode.Location = new System.Drawing.Point(36, 37);
            this.btnLayoutLine1XMode.Name = "btnLayoutLine1XMode";
            this.btnLayoutLine1XMode.Size = new System.Drawing.Size(111, 26);
            this.btnLayoutLine1XMode.TabIndex = 1;
            this.btnLayoutLine1XMode.Text = "1X";
            this.btnLayoutLine1XMode.UseVisualStyleBackColor = false;
            this.btnLayoutLine1XMode.Click += new System.EventHandler(this.btnLayoutLine1XMode_Click);
            // 
            // grbEditLineMode
            // 
            this.grbEditLineMode.Enabled = false;
            this.grbEditLineMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbEditLineMode.Location = new System.Drawing.Point(18, 432);
            this.grbEditLineMode.Name = "grbEditLineMode";
            this.grbEditLineMode.Size = new System.Drawing.Size(218, 318);
            this.grbEditLineMode.TabIndex = 10;
            this.grbEditLineMode.TabStop = false;
            this.grbEditLineMode.Text = "Edit Line Mode";
            // 
            // grbLineModeSelection
            // 
            this.grbLineModeSelection.Controls.Add(this.btnLineDesignStateLayoutMode);
            this.grbLineModeSelection.Controls.Add(this.btnLineDesignStateEditMode);
            this.grbLineModeSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbLineModeSelection.Location = new System.Drawing.Point(13, 7);
            this.grbLineModeSelection.Name = "grbLineModeSelection";
            this.grbLineModeSelection.Size = new System.Drawing.Size(218, 60);
            this.grbLineModeSelection.TabIndex = 9;
            this.grbLineModeSelection.TabStop = false;
            this.grbLineModeSelection.Text = "Line Mode";
            // 
            // btnLineDesignStateLayoutMode
            // 
            this.btnLineDesignStateLayoutMode.BackColor = System.Drawing.Color.Orange;
            this.btnLineDesignStateLayoutMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLineDesignStateLayoutMode.Location = new System.Drawing.Point(19, 20);
            this.btnLineDesignStateLayoutMode.Name = "btnLineDesignStateLayoutMode";
            this.btnLineDesignStateLayoutMode.Size = new System.Drawing.Size(84, 34);
            this.btnLineDesignStateLayoutMode.TabIndex = 7;
            this.btnLineDesignStateLayoutMode.Text = "Layout LInes";
            this.btnLineDesignStateLayoutMode.UseVisualStyleBackColor = false;
            // 
            // btnLineDesignStateEditMode
            // 
            this.btnLineDesignStateEditMode.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLineDesignStateEditMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLineDesignStateEditMode.Location = new System.Drawing.Point(126, 21);
            this.btnLineDesignStateEditMode.Name = "btnLineDesignStateEditMode";
            this.btnLineDesignStateEditMode.Size = new System.Drawing.Size(87, 29);
            this.btnLineDesignStateEditMode.TabIndex = 8;
            this.btnLineDesignStateEditMode.Text = "Edit Lines";
            this.btnLineDesignStateEditMode.UseVisualStyleBackColor = false;
            // 
            // btnSetLinesTo2X
            // 
            this.btnSetLinesTo2X.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSetLinesTo2X.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetLinesTo2X.Location = new System.Drawing.Point(25, 243);
            this.btnSetLinesTo2X.Name = "btnSetLinesTo2X";
            this.btnSetLinesTo2X.Size = new System.Drawing.Size(169, 26);
            this.btnSetLinesTo2X.TabIndex = 10;
            this.btnSetLinesTo2X.Text = "Set lines to 2X";
            this.btnSetLinesTo2X.UseVisualStyleBackColor = false;
            this.btnSetLinesTo2X.Click += new System.EventHandler(this.btnSetLinesTo2X_Click);
            // 
            // btnEditLineApply
            // 
            this.btnEditLineApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditLineApply.Location = new System.Drawing.Point(145, 283);
            this.btnEditLineApply.Name = "btnEditLineApply";
            this.btnEditLineApply.Size = new System.Drawing.Size(56, 26);
            this.btnEditLineApply.TabIndex = 9;
            this.btnEditLineApply.Text = "Apply";
            this.btnEditLineApply.UseVisualStyleBackColor = true;
            this.btnEditLineApply.Click += new System.EventHandler(this.btnEditLineApply_Click);
            // 
            // btnEditLineClear
            // 
            this.btnEditLineClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditLineClear.Location = new System.Drawing.Point(79, 283);
            this.btnEditLineClear.Name = "btnEditLineClear";
            this.btnEditLineClear.Size = new System.Drawing.Size(56, 26);
            this.btnEditLineClear.TabIndex = 8;
            this.btnEditLineClear.Text = "Clear";
            this.btnEditLineClear.UseVisualStyleBackColor = true;
            this.btnEditLineClear.Click += new System.EventHandler(this.btnEditLineClear_Click);
            // 
            // btnEditLineUndo
            // 
            this.btnEditLineUndo.Enabled = false;
            this.btnEditLineUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditLineUndo.Location = new System.Drawing.Point(13, 283);
            this.btnEditLineUndo.Name = "btnEditLineUndo";
            this.btnEditLineUndo.Size = new System.Drawing.Size(56, 26);
            this.btnEditLineUndo.TabIndex = 7;
            this.btnEditLineUndo.Text = "Undo";
            this.btnEditLineUndo.UseVisualStyleBackColor = true;
            this.btnEditLineUndo.Click += new System.EventHandler(this.btnEditLineUndo_Click);
            // 
            // ckbEditLineHighlightAndApply
            // 
            this.ckbEditLineHighlightAndApply.AutoSize = true;
            this.ckbEditLineHighlightAndApply.Checked = true;
            this.ckbEditLineHighlightAndApply.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbEditLineHighlightAndApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbEditLineHighlightAndApply.Location = new System.Drawing.Point(38, 92);
            this.ckbEditLineHighlightAndApply.Name = "ckbEditLineHighlightAndApply";
            this.ckbEditLineHighlightAndApply.Size = new System.Drawing.Size(144, 20);
            this.ckbEditLineHighlightAndApply.TabIndex = 3;
            this.ckbEditLineHighlightAndApply.Text = "Highlight then apply";
            this.ckbEditLineHighlightAndApply.UseVisualStyleBackColor = true;
            // 
            // btnSetLinesTo1X
            // 
            this.btnSetLinesTo1X.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSetLinesTo1X.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetLinesTo1X.Location = new System.Drawing.Point(25, 201);
            this.btnSetLinesTo1X.Name = "btnSetLinesTo1X";
            this.btnSetLinesTo1X.Size = new System.Drawing.Size(169, 26);
            this.btnSetLinesTo1X.TabIndex = 6;
            this.btnSetLinesTo1X.Text = "Set lines to 1X";
            this.btnSetLinesTo1X.UseVisualStyleBackColor = false;
            this.btnSetLinesTo1X.Click += new System.EventHandler(this.btnSetLineTo1X_Click);
            // 
            // btnEditLineChangeLinesToSelected
            // 
            this.btnEditLineChangeLinesToSelected.BackColor = System.Drawing.Color.Orange;
            this.btnEditLineChangeLinesToSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditLineChangeLinesToSelected.Location = new System.Drawing.Point(25, 127);
            this.btnEditLineChangeLinesToSelected.Name = "btnEditLineChangeLinesToSelected";
            this.btnEditLineChangeLinesToSelected.Size = new System.Drawing.Size(169, 26);
            this.btnEditLineChangeLinesToSelected.TabIndex = 4;
            this.btnEditLineChangeLinesToSelected.Text = "Change line to selected";
            this.btnEditLineChangeLinesToSelected.UseVisualStyleBackColor = false;
            this.btnEditLineChangeLinesToSelected.Click += new System.EventHandler(this.btnEditLineChangeLinesToSelected_Click);
            // 
            // btnEditLineDeleteLines
            // 
            this.btnEditLineDeleteLines.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEditLineDeleteLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditLineDeleteLines.Location = new System.Drawing.Point(26, 163);
            this.btnEditLineDeleteLines.Name = "btnEditLineDeleteLines";
            this.btnEditLineDeleteLines.Size = new System.Drawing.Size(168, 26);
            this.btnEditLineDeleteLines.TabIndex = 5;
            this.btnEditLineDeleteLines.Text = "Delete lines";
            this.btnEditLineDeleteLines.UseVisualStyleBackColor = false;
            this.btnEditLineDeleteLines.Click += new System.EventHandler(this.btnEditLineDeleteLines_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAreaDesignStateLayoutMode);
            this.groupBox1.Controls.Add(this.btnAreaDesignStateEditMode);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 60);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Area Mode";
            // 
            // btnAreaDesignStateLayoutMode
            // 
            this.btnAreaDesignStateLayoutMode.BackColor = System.Drawing.Color.Orange;
            this.btnAreaDesignStateLayoutMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAreaDesignStateLayoutMode.Location = new System.Drawing.Point(19, 20);
            this.btnAreaDesignStateLayoutMode.Name = "btnAreaDesignStateLayoutMode";
            this.btnAreaDesignStateLayoutMode.Size = new System.Drawing.Size(84, 34);
            this.btnAreaDesignStateLayoutMode.TabIndex = 7;
            this.btnAreaDesignStateLayoutMode.Text = "Layout Area";
            this.btnAreaDesignStateLayoutMode.UseVisualStyleBackColor = false;
            this.btnAreaDesignStateLayoutMode.Click += new System.EventHandler(this.BtnAreaDesignStateLayoutMode_Click);
            // 
            // btnAreaDesignStateEditMode
            // 
            this.btnAreaDesignStateEditMode.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAreaDesignStateEditMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAreaDesignStateEditMode.Location = new System.Drawing.Point(126, 21);
            this.btnAreaDesignStateEditMode.Name = "btnAreaDesignStateEditMode";
            this.btnAreaDesignStateEditMode.Size = new System.Drawing.Size(87, 29);
            this.btnAreaDesignStateEditMode.TabIndex = 8;
            this.btnAreaDesignStateEditMode.Text = "Edit Area";
            this.btnAreaDesignStateEditMode.UseVisualStyleBackColor = false;
            this.btnAreaDesignStateEditMode.Click += new System.EventHandler(this.BtnAreaDesignStateEditMode_Click);
            // 
            // grbEditAreaMode
            // 
            this.grbEditAreaMode.Controls.Add(this.btnEditAreaApply);
            this.grbEditAreaMode.Controls.Add(this.lblFinishName);
            this.grbEditAreaMode.Controls.Add(this.btnEditAreaClear);
            this.grbEditAreaMode.Controls.Add(this.pnlFinishColor);
            this.grbEditAreaMode.Controls.Add(this.btnEditAreaUndo);
            this.grbEditAreaMode.Controls.Add(this.ckbEditAreaHighlightAndApply);
            this.grbEditAreaMode.Controls.Add(this.btnEditShape);
            this.grbEditAreaMode.Controls.Add(this.btnChangeShapesToSelected);
            this.grbEditAreaMode.Controls.Add(this.btnDeleteShapes);
            this.grbEditAreaMode.Enabled = false;
            this.grbEditAreaMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbEditAreaMode.Location = new System.Drawing.Point(13, 434);
            this.grbEditAreaMode.Name = "grbEditAreaMode";
            this.grbEditAreaMode.Size = new System.Drawing.Size(232, 309);
            this.grbEditAreaMode.TabIndex = 10;
            this.grbEditAreaMode.TabStop = false;
            this.grbEditAreaMode.Text = "Edit Area Mode";
            // 
            // btnEditAreaApply
            // 
            this.btnEditAreaApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditAreaApply.Location = new System.Drawing.Point(145, 216);
            this.btnEditAreaApply.Name = "btnEditAreaApply";
            this.btnEditAreaApply.Size = new System.Drawing.Size(56, 26);
            this.btnEditAreaApply.TabIndex = 9;
            this.btnEditAreaApply.Text = "Apply";
            this.btnEditAreaApply.UseVisualStyleBackColor = true;
            this.btnEditAreaApply.Click += new System.EventHandler(this.btnEditAreaApply_Click);
            // 
            // btnEditAreaClear
            // 
            this.btnEditAreaClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditAreaClear.Location = new System.Drawing.Point(79, 216);
            this.btnEditAreaClear.Name = "btnEditAreaClear";
            this.btnEditAreaClear.Size = new System.Drawing.Size(56, 26);
            this.btnEditAreaClear.TabIndex = 8;
            this.btnEditAreaClear.Text = "Clear";
            this.btnEditAreaClear.UseVisualStyleBackColor = true;
            this.btnEditAreaClear.Click += new System.EventHandler(this.btnEditAreaClear_Click);
            // 
            // btnEditAreaUndo
            // 
            this.btnEditAreaUndo.Enabled = false;
            this.btnEditAreaUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditAreaUndo.Location = new System.Drawing.Point(13, 216);
            this.btnEditAreaUndo.Name = "btnEditAreaUndo";
            this.btnEditAreaUndo.Size = new System.Drawing.Size(56, 26);
            this.btnEditAreaUndo.TabIndex = 7;
            this.btnEditAreaUndo.Text = "Undo";
            this.btnEditAreaUndo.UseVisualStyleBackColor = true;
            this.btnEditAreaUndo.Click += new System.EventHandler(this.btnEditAreaUndo_Click);
            // 
            // ckbEditAreaHighlightAndApply
            // 
            this.ckbEditAreaHighlightAndApply.AutoSize = true;
            this.ckbEditAreaHighlightAndApply.Checked = true;
            this.ckbEditAreaHighlightAndApply.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbEditAreaHighlightAndApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbEditAreaHighlightAndApply.Location = new System.Drawing.Point(36, 26);
            this.ckbEditAreaHighlightAndApply.Name = "ckbEditAreaHighlightAndApply";
            this.ckbEditAreaHighlightAndApply.Size = new System.Drawing.Size(144, 20);
            this.ckbEditAreaHighlightAndApply.TabIndex = 3;
            this.ckbEditAreaHighlightAndApply.Text = "Highlight then apply";
            this.ckbEditAreaHighlightAndApply.UseVisualStyleBackColor = true;
            this.ckbEditAreaHighlightAndApply.CheckedChanged += new System.EventHandler(this.ckbHighlightAndApply_CheckedChanged);
            // 
            // btnEditShape
            // 
            this.btnEditShape.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEditShape.Enabled = false;
            this.btnEditShape.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditShape.Location = new System.Drawing.Point(25, 180);
            this.btnEditShape.Name = "btnEditShape";
            this.btnEditShape.Size = new System.Drawing.Size(169, 26);
            this.btnEditShape.TabIndex = 6;
            this.btnEditShape.Text = "Edit Shape";
            this.btnEditShape.UseVisualStyleBackColor = false;
            this.btnEditShape.Click += new System.EventHandler(this.btnEditAreaEditShape_Click);
            // 
            // btnDeleteShapes
            // 
            this.btnDeleteShapes.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDeleteShapes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteShapes.Location = new System.Drawing.Point(26, 146);
            this.btnDeleteShapes.Name = "btnDeleteShapes";
            this.btnDeleteShapes.Size = new System.Drawing.Size(168, 26);
            this.btnDeleteShapes.TabIndex = 5;
            this.btnDeleteShapes.Text = "Delete shapes";
            this.btnDeleteShapes.UseVisualStyleBackColor = false;
            this.btnDeleteShapes.Click += new System.EventHandler(this.btnEditAreaDeleteShapes_Click);
            // 
            // grbLayoutAreaMode
            // 
            this.grbLayoutAreaMode.Controls.Add(this.groupBox4);
            this.grbLayoutAreaMode.Controls.Add(this.panel2);
            this.grbLayoutAreaMode.Controls.Add(this.pnlSection1);
            this.grbLayoutAreaMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbLayoutAreaMode.Location = new System.Drawing.Point(13, 76);
            this.grbLayoutAreaMode.Name = "grbLayoutAreaMode";
            this.grbLayoutAreaMode.Size = new System.Drawing.Size(232, 352);
            this.grbLayoutAreaMode.TabIndex = 11;
            this.grbLayoutAreaMode.TabStop = false;
            this.grbLayoutAreaMode.Text = "Layout Area Mode";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.nudFixedWidthInches);
            this.groupBox4.Controls.Add(this.nudFixedWidthFeet);
            this.groupBox4.Controls.Add(this.ckbFixedWidth);
            this.groupBox4.Location = new System.Drawing.Point(18, 284);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 55);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            // 
            // ckbFixedWidth
            // 
            this.ckbFixedWidth.AutoSize = true;
            this.ckbFixedWidth.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ckbFixedWidth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckbFixedWidth.Location = new System.Drawing.Point(6, 22);
            this.ckbFixedWidth.Name = "ckbFixedWidth";
            this.ckbFixedWidth.Size = new System.Drawing.Size(94, 20);
            this.ckbFixedWidth.TabIndex = 0;
            this.ckbFixedWidth.Text = "Fixed Width";
            this.ckbFixedWidth.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnEmbeddLayoutAreas);
            this.panel2.Controls.Add(this.btnLayoutAreaTakeOutAndFill);
            this.panel2.Controls.Add(this.btnLayoutAreaTakeout);
            this.panel2.Controls.Add(this.btnAreaDesignStateZeroLine);
            this.panel2.Location = new System.Drawing.Point(19, 129);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(196, 152);
            this.panel2.TabIndex = 4;
            // 
            // btnEmbeddLayoutAreas
            // 
            this.btnEmbeddLayoutAreas.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEmbeddLayoutAreas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmbeddLayoutAreas.Location = new System.Drawing.Point(37, 113);
            this.btnEmbeddLayoutAreas.Name = "btnEmbeddLayoutAreas";
            this.btnEmbeddLayoutAreas.Size = new System.Drawing.Size(136, 26);
            this.btnEmbeddLayoutAreas.TabIndex = 3;
            this.btnEmbeddLayoutAreas.Text = "Embed Layout Areas";
            this.btnEmbeddLayoutAreas.UseVisualStyleBackColor = false;
            this.btnEmbeddLayoutAreas.Click += new System.EventHandler(this.BtnEmbeddLayoutAreas_Click);
            // 
            // btnLayoutAreaTakeOutAndFill
            // 
            this.btnLayoutAreaTakeOutAndFill.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLayoutAreaTakeOutAndFill.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLayoutAreaTakeOutAndFill.Location = new System.Drawing.Point(36, 78);
            this.btnLayoutAreaTakeOutAndFill.Name = "btnLayoutAreaTakeOutAndFill";
            this.btnLayoutAreaTakeOutAndFill.Size = new System.Drawing.Size(137, 26);
            this.btnLayoutAreaTakeOutAndFill.TabIndex = 2;
            this.btnLayoutAreaTakeOutAndFill.Text = "Take Out and Fill";
            this.btnLayoutAreaTakeOutAndFill.UseVisualStyleBackColor = false;
            this.btnLayoutAreaTakeOutAndFill.Click += new System.EventHandler(this.btnLayoutAreaTakeOutAndFill_Click);
            // 
            // btnLayoutAreaTakeout
            // 
            this.btnLayoutAreaTakeout.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLayoutAreaTakeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLayoutAreaTakeout.Location = new System.Drawing.Point(36, 44);
            this.btnLayoutAreaTakeout.Name = "btnLayoutAreaTakeout";
            this.btnLayoutAreaTakeout.Size = new System.Drawing.Size(137, 26);
            this.btnLayoutAreaTakeout.TabIndex = 1;
            this.btnLayoutAreaTakeout.Text = "Area Take Out";
            this.btnLayoutAreaTakeout.UseVisualStyleBackColor = false;
            this.btnLayoutAreaTakeout.Click += new System.EventHandler(this.btnLayoutAreaTakeout_Click);
            // 
            // btnAreaDesignStateZeroLine
            // 
            this.btnAreaDesignStateZeroLine.BackColor = System.Drawing.SystemColors.Control;
            this.btnAreaDesignStateZeroLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAreaDesignStateZeroLine.Location = new System.Drawing.Point(38, 9);
            this.btnAreaDesignStateZeroLine.Name = "btnAreaDesignStateZeroLine";
            this.btnAreaDesignStateZeroLine.Size = new System.Drawing.Size(135, 26);
            this.btnAreaDesignStateZeroLine.TabIndex = 0;
            this.btnAreaDesignStateZeroLine.Text = "Zero Line";
            this.btnAreaDesignStateZeroLine.UseVisualStyleBackColor = false;
            this.btnAreaDesignStateZeroLine.Click += new System.EventHandler(this.BtnAreaDesignModeZeroLine_Click);
            // 
            // pnlSection1
            // 
            this.pnlSection1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSection1.Controls.Add(this.btnCompleteShapeByIntersection);
            this.pnlSection1.Controls.Add(this.btnAreaDesignStateCompleteDrawing);
            this.pnlSection1.Location = new System.Drawing.Point(22, 30);
            this.pnlSection1.Name = "pnlSection1";
            this.pnlSection1.Size = new System.Drawing.Size(196, 91);
            this.pnlSection1.TabIndex = 1;
            // 
            // btnCompleteShapeByIntersection
            // 
            this.btnCompleteShapeByIntersection.BackColor = System.Drawing.SystemColors.Control;
            this.btnCompleteShapeByIntersection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompleteShapeByIntersection.Location = new System.Drawing.Point(39, 52);
            this.btnCompleteShapeByIntersection.Name = "btnCompleteShapeByIntersection";
            this.btnCompleteShapeByIntersection.Size = new System.Drawing.Size(109, 26);
            this.btnCompleteShapeByIntersection.TabIndex = 1;
            this.btnCompleteShapeByIntersection.Text = "Complete-L";
            this.btnCompleteShapeByIntersection.UseVisualStyleBackColor = false;
            this.btnCompleteShapeByIntersection.Click += new System.EventHandler(this.BtnAreaDesignStateCompleteDrawingByIntersection_Click);
            // 
            // btnAreaDesignStateCompleteDrawing
            // 
            this.btnAreaDesignStateCompleteDrawing.BackColor = System.Drawing.SystemColors.Control;
            this.btnAreaDesignStateCompleteDrawing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAreaDesignStateCompleteDrawing.Location = new System.Drawing.Point(38, 15);
            this.btnAreaDesignStateCompleteDrawing.Name = "btnAreaDesignStateCompleteDrawing";
            this.btnAreaDesignStateCompleteDrawing.Size = new System.Drawing.Size(109, 26);
            this.btnAreaDesignStateCompleteDrawing.TabIndex = 0;
            this.btnAreaDesignStateCompleteDrawing.Text = "Complete";
            this.btnAreaDesignStateCompleteDrawing.UseVisualStyleBackColor = false;
            this.btnAreaDesignStateCompleteDrawing.Click += new System.EventHandler(this.BtnAreaDesignStateCompleteDrawing_Click);
            // 
            // pnlAreaCommandPane
            // 
            this.pnlAreaCommandPane.AutoScroll = true;
            this.pnlAreaCommandPane.AutoScrollMinSize = new System.Drawing.Size(268, 1400);
            this.pnlAreaCommandPane.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlAreaCommandPane.Controls.Add(this.grbAreaModeCounter);
            this.pnlAreaCommandPane.Controls.Add(this.grbLayoutAreaMode);
            this.pnlAreaCommandPane.Controls.Add(this.grbEditAreaMode);
            this.pnlAreaCommandPane.Controls.Add(this.groupBox1);
            this.pnlAreaCommandPane.Location = new System.Drawing.Point(1613, 86);
            this.pnlAreaCommandPane.Name = "pnlAreaCommandPane";
            this.pnlAreaCommandPane.Size = new System.Drawing.Size(268, 1119);
            this.pnlAreaCommandPane.TabIndex = 5;
            // 
            // grbAreaModeCounter
            // 
            this.grbAreaModeCounter.Controls.Add(this.cccAreaMode);
            this.grbAreaModeCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbAreaModeCounter.Location = new System.Drawing.Point(14, 756);
            this.grbAreaModeCounter.Name = "grbAreaModeCounter";
            this.grbAreaModeCounter.Size = new System.Drawing.Size(231, 215);
            this.grbAreaModeCounter.TabIndex = 13;
            this.grbAreaModeCounter.TabStop = false;
            this.grbAreaModeCounter.Text = "Counters";
            // 
            // pnlSeamCommandPane
            // 
            this.pnlSeamCommandPane.AutoScroll = true;
            this.pnlSeamCommandPane.AutoScrollMinSize = new System.Drawing.Size(268, 1400);
            this.pnlSeamCommandPane.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlSeamCommandPane.Controls.Add(this.ucSeamsView1);
            this.pnlSeamCommandPane.Controls.Add(this.ucAreasView);
            this.pnlSeamCommandPane.Controls.Add(this.groupBox7);
            this.pnlSeamCommandPane.Controls.Add(this.grbSeaming);
            this.pnlSeamCommandPane.Controls.Add(this.grbAreaSubdivisionControls);
            this.pnlSeamCommandPane.Controls.Add(this.groupBox5);
            this.pnlSeamCommandPane.Controls.Add(this.groupBox2);
            this.pnlSeamCommandPane.Location = new System.Drawing.Point(1042, 91);
            this.pnlSeamCommandPane.Name = "pnlSeamCommandPane";
            this.pnlSeamCommandPane.Size = new System.Drawing.Size(268, 1119);
            this.pnlSeamCommandPane.TabIndex = 7;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ckbShowUnders);
            this.groupBox7.Controls.Add(this.ckbShowOvers);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.ckbShowCuts);
            this.groupBox7.Location = new System.Drawing.Point(22, 1104);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(215, 104);
            this.groupBox7.TabIndex = 19;
            this.groupBox7.TabStop = false;
            // 
            // ckbShowUnders
            // 
            this.ckbShowUnders.AutoSize = true;
            this.ckbShowUnders.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbShowUnders.Location = new System.Drawing.Point(106, 76);
            this.ckbShowUnders.Name = "ckbShowUnders";
            this.ckbShowUnders.Size = new System.Drawing.Size(75, 22);
            this.ckbShowUnders.TabIndex = 21;
            this.ckbShowUnders.Text = "Unders";
            this.ckbShowUnders.UseVisualStyleBackColor = true;
            this.ckbShowUnders.CheckedChanged += new System.EventHandler(this.ckbShowUnders_CheckedChanged);
            // 
            // ckbShowOvers
            // 
            this.ckbShowOvers.AutoSize = true;
            this.ckbShowOvers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbShowOvers.Location = new System.Drawing.Point(106, 50);
            this.ckbShowOvers.Name = "ckbShowOvers";
            this.ckbShowOvers.Size = new System.Drawing.Size(67, 22);
            this.ckbShowOvers.TabIndex = 20;
            this.ckbShowOvers.Text = "Overs";
            this.ckbShowOvers.UseVisualStyleBackColor = true;
            this.ckbShowOvers.CheckedChanged += new System.EventHandler(this.ckbShowOvers_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(28, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 18);
            this.label4.TabIndex = 19;
            this.label4.Text = "Show:";
            // 
            // ckbShowCuts
            // 
            this.ckbShowCuts.AutoSize = true;
            this.ckbShowCuts.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbShowCuts.Location = new System.Drawing.Point(106, 24);
            this.ckbShowCuts.Name = "ckbShowCuts";
            this.ckbShowCuts.Size = new System.Drawing.Size(58, 22);
            this.ckbShowCuts.TabIndex = 18;
            this.ckbShowCuts.Text = "Cuts";
            this.ckbShowCuts.UseVisualStyleBackColor = true;
            this.ckbShowCuts.CheckedChanged += new System.EventHandler(this.ckbShowCuts_CheckedChanged);
            // 
            // grbSeaming
            // 
            this.grbSeaming.Controls.Add(this.btnCenterSeamingToolInView);
            this.grbSeaming.Controls.Add(this.label3);
            this.grbSeaming.Controls.Add(this.nudMoveIncrement);
            this.grbSeaming.Controls.Add(this.groupBox6);
            this.grbSeaming.Controls.Add(this.grbNormalSeams);
            this.grbSeaming.Controls.Add(this.btnSubdivideRegion);
            this.grbSeaming.Controls.Add(this.btnSeamArea);
            this.grbSeaming.Controls.Add(this.btnSeamSingleLineToTool);
            this.grbSeaming.Controls.Add(this.btnAlignTool);
            this.grbSeaming.Controls.Add(this.label1);
            this.grbSeaming.Controls.Add(this.btnShowSeamingTool);
            this.grbSeaming.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSeaming.Location = new System.Drawing.Point(22, 638);
            this.grbSeaming.Name = "grbSeaming";
            this.grbSeaming.Size = new System.Drawing.Size(215, 461);
            this.grbSeaming.TabIndex = 17;
            this.grbSeaming.TabStop = false;
            this.grbSeaming.Text = "Seaming";
            // 
            // btnCenterSeamingToolInView
            // 
            this.btnCenterSeamingToolInView.Location = new System.Drawing.Point(24, 64);
            this.btnCenterSeamingToolInView.Name = "btnCenterSeamingToolInView";
            this.btnCenterSeamingToolInView.Size = new System.Drawing.Size(179, 27);
            this.btnCenterSeamingToolInView.TabIndex = 15;
            this.btnCenterSeamingToolInView.Text = "Center Tool In View";
            this.btnCenterSeamingToolInView.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Move Increment";
            // 
            // nudMoveIncrement
            // 
            this.nudMoveIncrement.Location = new System.Drawing.Point(143, 249);
            this.nudMoveIncrement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMoveIncrement.Name = "nudMoveIncrement";
            this.nudMoveIncrement.Size = new System.Drawing.Size(46, 22);
            this.nudMoveIncrement.TabIndex = 13;
            this.nudMoveIncrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbnVirtualSeamsShowUnhideable);
            this.groupBox6.Controls.Add(this.rbnVirtualSeamsHideAll);
            this.groupBox6.Controls.Add(this.rbnVirtualSeamsShowAll);
            this.groupBox6.Location = new System.Drawing.Point(7, 359);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 85);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Virtual Seams";
            // 
            // rbnVirtualSeamsShowUnhideable
            // 
            this.rbnVirtualSeamsShowUnhideable.AutoSize = true;
            this.rbnVirtualSeamsShowUnhideable.Location = new System.Drawing.Point(32, 48);
            this.rbnVirtualSeamsShowUnhideable.Name = "rbnVirtualSeamsShowUnhideable";
            this.rbnVirtualSeamsShowUnhideable.Size = new System.Drawing.Size(132, 20);
            this.rbnVirtualSeamsShowUnhideable.TabIndex = 2;
            this.rbnVirtualSeamsShowUnhideable.Text = "Show Unhideable";
            this.rbnVirtualSeamsShowUnhideable.UseVisualStyleBackColor = true;
            this.rbnVirtualSeamsShowUnhideable.CheckedChanged += new System.EventHandler(this.rbnVirtualSeamsShowUnhideable_CheckedChanged);
            // 
            // rbnVirtualSeamsHideAll
            // 
            this.rbnVirtualSeamsHideAll.AutoSize = true;
            this.rbnVirtualSeamsHideAll.Location = new System.Drawing.Point(109, 22);
            this.rbnVirtualSeamsHideAll.Name = "rbnVirtualSeamsHideAll";
            this.rbnVirtualSeamsHideAll.Size = new System.Drawing.Size(73, 20);
            this.rbnVirtualSeamsHideAll.TabIndex = 1;
            this.rbnVirtualSeamsHideAll.Text = "Hide All";
            this.rbnVirtualSeamsHideAll.UseVisualStyleBackColor = true;
            this.rbnVirtualSeamsHideAll.CheckedChanged += new System.EventHandler(this.rbnVirtualSeamsHideAll_CheckedChanged);
            // 
            // rbnVirtualSeamsShowAll
            // 
            this.rbnVirtualSeamsShowAll.AutoSize = true;
            this.rbnVirtualSeamsShowAll.Checked = true;
            this.rbnVirtualSeamsShowAll.Location = new System.Drawing.Point(24, 22);
            this.rbnVirtualSeamsShowAll.Name = "rbnVirtualSeamsShowAll";
            this.rbnVirtualSeamsShowAll.Size = new System.Drawing.Size(77, 20);
            this.rbnVirtualSeamsShowAll.TabIndex = 0;
            this.rbnVirtualSeamsShowAll.TabStop = true;
            this.rbnVirtualSeamsShowAll.Text = "Show All";
            this.rbnVirtualSeamsShowAll.UseVisualStyleBackColor = true;
            this.rbnVirtualSeamsShowAll.CheckedChanged += new System.EventHandler(this.rbnVirtualSeamsShowAll_CheckedChanged);
            // 
            // grbNormalSeams
            // 
            this.grbNormalSeams.Controls.Add(this.rbnNormalSeamsHideAll);
            this.grbNormalSeams.Controls.Add(this.rbnNormalSeamsShowUnHideable);
            this.grbNormalSeams.Controls.Add(this.rbnNormalSeamsShowAll);
            this.grbNormalSeams.Location = new System.Drawing.Point(9, 280);
            this.grbNormalSeams.Name = "grbNormalSeams";
            this.grbNormalSeams.Size = new System.Drawing.Size(200, 73);
            this.grbNormalSeams.TabIndex = 11;
            this.grbNormalSeams.TabStop = false;
            this.grbNormalSeams.Text = "Normal Seams";
            // 
            // rbnNormalSeamsHideAll
            // 
            this.rbnNormalSeamsHideAll.AutoSize = true;
            this.rbnNormalSeamsHideAll.Location = new System.Drawing.Point(106, 18);
            this.rbnNormalSeamsHideAll.Name = "rbnNormalSeamsHideAll";
            this.rbnNormalSeamsHideAll.Size = new System.Drawing.Size(73, 20);
            this.rbnNormalSeamsHideAll.TabIndex = 3;
            this.rbnNormalSeamsHideAll.Text = "Hide All";
            this.rbnNormalSeamsHideAll.UseVisualStyleBackColor = true;
            this.rbnNormalSeamsHideAll.CheckedChanged += new System.EventHandler(this.rbnNormalSeamsHideAll_CheckedChanged);
            // 
            // rbnNormalSeamsShowUnHideable
            // 
            this.rbnNormalSeamsShowUnHideable.AutoSize = true;
            this.rbnNormalSeamsShowUnHideable.Location = new System.Drawing.Point(20, 44);
            this.rbnNormalSeamsShowUnHideable.Name = "rbnNormalSeamsShowUnHideable";
            this.rbnNormalSeamsShowUnHideable.Size = new System.Drawing.Size(132, 20);
            this.rbnNormalSeamsShowUnHideable.TabIndex = 3;
            this.rbnNormalSeamsShowUnHideable.Text = "Show Unhideable";
            this.rbnNormalSeamsShowUnHideable.UseVisualStyleBackColor = true;
            this.rbnNormalSeamsShowUnHideable.CheckedChanged += new System.EventHandler(this.rbnNormalSeamsShowUnHideable_CheckedChanged);
            // 
            // rbnNormalSeamsShowAll
            // 
            this.rbnNormalSeamsShowAll.AutoSize = true;
            this.rbnNormalSeamsShowAll.Checked = true;
            this.rbnNormalSeamsShowAll.Location = new System.Drawing.Point(20, 19);
            this.rbnNormalSeamsShowAll.Name = "rbnNormalSeamsShowAll";
            this.rbnNormalSeamsShowAll.Size = new System.Drawing.Size(77, 20);
            this.rbnNormalSeamsShowAll.TabIndex = 1;
            this.rbnNormalSeamsShowAll.TabStop = true;
            this.rbnNormalSeamsShowAll.Text = "Show All";
            this.rbnNormalSeamsShowAll.UseVisualStyleBackColor = true;
            this.rbnNormalSeamsShowAll.CheckedChanged += new System.EventHandler(this.rbnNormalSeamsShowAll_CheckedChanged);
            // 
            // btnSubdivideRegion
            // 
            this.btnSubdivideRegion.Enabled = false;
            this.btnSubdivideRegion.Location = new System.Drawing.Point(24, 208);
            this.btnSubdivideRegion.Name = "btnSubdivideRegion";
            this.btnSubdivideRegion.Size = new System.Drawing.Size(179, 27);
            this.btnSubdivideRegion.TabIndex = 9;
            this.btnSubdivideRegion.Text = "Subdivide Region";
            this.btnSubdivideRegion.UseVisualStyleBackColor = true;
            this.btnSubdivideRegion.Click += new System.EventHandler(this.btnSubdivideRegion_Click);
            // 
            // btnSeamArea
            // 
            this.btnSeamArea.Enabled = false;
            this.btnSeamArea.Location = new System.Drawing.Point(24, 172);
            this.btnSeamArea.Name = "btnSeamArea";
            this.btnSeamArea.Size = new System.Drawing.Size(179, 27);
            this.btnSeamArea.TabIndex = 6;
            this.btnSeamArea.Text = "Seam Area";
            this.btnSeamArea.UseVisualStyleBackColor = true;
            this.btnSeamArea.Click += new System.EventHandler(this.btnSeamArea_Click);
            // 
            // btnSeamSingleLineToTool
            // 
            this.btnSeamSingleLineToTool.Enabled = false;
            this.btnSeamSingleLineToTool.Location = new System.Drawing.Point(24, 136);
            this.btnSeamSingleLineToTool.Name = "btnSeamSingleLineToTool";
            this.btnSeamSingleLineToTool.Size = new System.Drawing.Size(179, 27);
            this.btnSeamSingleLineToTool.TabIndex = 5;
            this.btnSeamSingleLineToTool.Text = "Seam Single Line To Tool";
            this.btnSeamSingleLineToTool.UseVisualStyleBackColor = true;
            this.btnSeamSingleLineToTool.Click += new System.EventHandler(this.btnSeamSingleLineToTool_Click);
            // 
            // btnAlignTool
            // 
            this.btnAlignTool.Enabled = false;
            this.btnAlignTool.Location = new System.Drawing.Point(24, 100);
            this.btnAlignTool.Name = "btnAlignTool";
            this.btnAlignTool.Size = new System.Drawing.Size(179, 27);
            this.btnAlignTool.TabIndex = 4;
            this.btnAlignTool.Text = "Align Tool";
            this.btnAlignTool.UseVisualStyleBackColor = true;
            this.btnAlignTool.Click += new System.EventHandler(this.btnAlignTool_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = " ";
            // 
            // btnShowSeamingTool
            // 
            this.btnShowSeamingTool.Location = new System.Drawing.Point(24, 28);
            this.btnShowSeamingTool.Name = "btnShowSeamingTool";
            this.btnShowSeamingTool.Size = new System.Drawing.Size(179, 27);
            this.btnShowSeamingTool.TabIndex = 0;
            this.btnShowSeamingTool.Text = "Show Seaming Tool";
            this.btnShowSeamingTool.UseVisualStyleBackColor = true;
            // 
            // grbAreaSubdivisionControls
            // 
            this.grbAreaSubdivisionControls.Controls.Add(this.groupBox3);
            this.grbAreaSubdivisionControls.Controls.Add(this.btnRemoveSubdivision);
            this.grbAreaSubdivisionControls.Controls.Add(this.btnCancelSubdivision);
            this.grbAreaSubdivisionControls.Controls.Add(this.btnCompleteSubdivision);
            this.grbAreaSubdivisionControls.Enabled = false;
            this.grbAreaSubdivisionControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbAreaSubdivisionControls.Location = new System.Drawing.Point(18, 449);
            this.grbAreaSubdivisionControls.Name = "grbAreaSubdivisionControls";
            this.grbAreaSubdivisionControls.Size = new System.Drawing.Size(219, 190);
            this.grbAreaSubdivisionControls.TabIndex = 16;
            this.grbAreaSubdivisionControls.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbnSubdivideByArea);
            this.groupBox3.Controls.Add(this.rbnSubdivideByLine);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(9, 132);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 46);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Subdivide By";
            // 
            // rbnSubdivideByArea
            // 
            this.rbnSubdivideByArea.AutoSize = true;
            this.rbnSubdivideByArea.Location = new System.Drawing.Point(124, 20);
            this.rbnSubdivideByArea.Name = "rbnSubdivideByArea";
            this.rbnSubdivideByArea.Size = new System.Drawing.Size(50, 19);
            this.rbnSubdivideByArea.TabIndex = 1;
            this.rbnSubdivideByArea.Text = "Area";
            this.rbnSubdivideByArea.UseVisualStyleBackColor = true;
            // 
            // rbnSubdivideByLine
            // 
            this.rbnSubdivideByLine.AutoSize = true;
            this.rbnSubdivideByLine.Checked = true;
            this.rbnSubdivideByLine.Location = new System.Drawing.Point(34, 20);
            this.rbnSubdivideByLine.Name = "rbnSubdivideByLine";
            this.rbnSubdivideByLine.Size = new System.Drawing.Size(49, 19);
            this.rbnSubdivideByLine.TabIndex = 0;
            this.rbnSubdivideByLine.TabStop = true;
            this.rbnSubdivideByLine.Text = "Line";
            this.rbnSubdivideByLine.UseVisualStyleBackColor = true;
            // 
            // btnRemoveSubdivision
            // 
            this.btnRemoveSubdivision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveSubdivision.Location = new System.Drawing.Point(35, 95);
            this.btnRemoveSubdivision.Name = "btnRemoveSubdivision";
            this.btnRemoveSubdivision.Size = new System.Drawing.Size(153, 28);
            this.btnRemoveSubdivision.TabIndex = 16;
            this.btnRemoveSubdivision.Text = "Remove Subdivision";
            this.btnRemoveSubdivision.UseVisualStyleBackColor = true;
            this.btnRemoveSubdivision.Click += new System.EventHandler(this.btnRemoveSubdivision_Click);
            // 
            // btnCancelSubdivision
            // 
            this.btnCancelSubdivision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelSubdivision.Location = new System.Drawing.Point(35, 58);
            this.btnCancelSubdivision.Name = "btnCancelSubdivision";
            this.btnCancelSubdivision.Size = new System.Drawing.Size(151, 28);
            this.btnCancelSubdivision.TabIndex = 15;
            this.btnCancelSubdivision.Text = "Cancel Subdivision";
            this.btnCancelSubdivision.UseVisualStyleBackColor = true;
            this.btnCancelSubdivision.Click += new System.EventHandler(this.btnCancelSubdivision_Click);
            // 
            // btnCompleteSubdivision
            // 
            this.btnCompleteSubdivision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompleteSubdivision.Location = new System.Drawing.Point(34, 20);
            this.btnCompleteSubdivision.Name = "btnCompleteSubdivision";
            this.btnCompleteSubdivision.Size = new System.Drawing.Size(153, 28);
            this.btnCompleteSubdivision.TabIndex = 14;
            this.btnCompleteSubdivision.Text = "Complete Subdivision";
            this.btnCompleteSubdivision.UseVisualStyleBackColor = true;
            this.btnCompleteSubdivision.Click += new System.EventHandler(this.btnCompleteSubdivision_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnSeamDesignStateSelectionMode);
            this.groupBox5.Controls.Add(this.btnSeamDesignStateSubdivisionMode);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(18, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(213, 72);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Operating Mode";
            // 
            // btnSeamDesignStateSelectionMode
            // 
            this.btnSeamDesignStateSelectionMode.BackColor = System.Drawing.Color.Orange;
            this.btnSeamDesignStateSelectionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeamDesignStateSelectionMode.Location = new System.Drawing.Point(13, 20);
            this.btnSeamDesignStateSelectionMode.Name = "btnSeamDesignStateSelectionMode";
            this.btnSeamDesignStateSelectionMode.Size = new System.Drawing.Size(91, 43);
            this.btnSeamDesignStateSelectionMode.TabIndex = 7;
            this.btnSeamDesignStateSelectionMode.Text = "Area Selection";
            this.btnSeamDesignStateSelectionMode.UseVisualStyleBackColor = false;
            this.btnSeamDesignStateSelectionMode.Click += new System.EventHandler(this.BtnSeamDesignStateSelectionMode_Click);
            // 
            // btnSeamDesignStateSubdivisionMode
            // 
            this.btnSeamDesignStateSubdivisionMode.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSeamDesignStateSubdivisionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeamDesignStateSubdivisionMode.Location = new System.Drawing.Point(120, 21);
            this.btnSeamDesignStateSubdivisionMode.Name = "btnSeamDesignStateSubdivisionMode";
            this.btnSeamDesignStateSubdivisionMode.Size = new System.Drawing.Size(87, 42);
            this.btnSeamDesignStateSubdivisionMode.TabIndex = 8;
            this.btnSeamDesignStateSubdivisionMode.Text = "Area Subdivision";
            this.btnSeamDesignStateSubdivisionMode.UseVisualStyleBackColor = false;
            this.btnSeamDesignStateSubdivisionMode.Click += new System.EventHandler(this.BtnSeamDesignStateSubdivisionMode_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbnViewSeams);
            this.groupBox2.Controls.Add(this.rbnViewAreas);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(22, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 46);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "View";
            // 
            // rbnViewSeams
            // 
            this.rbnViewSeams.AutoSize = true;
            this.rbnViewSeams.Location = new System.Drawing.Point(116, 20);
            this.rbnViewSeams.Name = "rbnViewSeams";
            this.rbnViewSeams.Size = new System.Drawing.Size(69, 20);
            this.rbnViewSeams.TabIndex = 1;
            this.rbnViewSeams.Text = "Seams";
            this.rbnViewSeams.UseVisualStyleBackColor = true;
            this.rbnViewSeams.CheckedChanged += new System.EventHandler(this.rbnViewSeams_CheckedChanged);
            // 
            // rbnViewAreas
            // 
            this.rbnViewAreas.AutoSize = true;
            this.rbnViewAreas.Checked = true;
            this.rbnViewAreas.Location = new System.Drawing.Point(29, 20);
            this.rbnViewAreas.Name = "rbnViewAreas";
            this.rbnViewAreas.Size = new System.Drawing.Size(62, 20);
            this.rbnViewAreas.TabIndex = 0;
            this.rbnViewAreas.TabStop = true;
            this.rbnViewAreas.Text = "Areas";
            this.rbnViewAreas.UseVisualStyleBackColor = true;
            this.rbnViewAreas.CheckedChanged += new System.EventHandler(this.rbnViewAreas_CheckedChanged);
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectName.Location = new System.Drawing.Point(2047, 617);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(0, 24);
            this.lblProjectName.TabIndex = 8;
            // 
            // pnlVisioPageControlCover
            // 
            this.pnlVisioPageControlCover.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlVisioPageControlCover.Location = new System.Drawing.Point(296, 842);
            this.pnlVisioPageControlCover.Name = "pnlVisioPageControlCover";
            this.pnlVisioPageControlCover.Size = new System.Drawing.Size(699, 30);
            this.pnlVisioPageControlCover.TabIndex = 9;
            // 
            // tkbHScroll
            // 
            this.tkbHScroll.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tkbHScroll.Location = new System.Drawing.Point(296, 886);
            this.tkbHScroll.Maximum = 100;
            this.tkbHScroll.Name = "tkbHScroll";
            this.tkbHScroll.Size = new System.Drawing.Size(693, 45);
            this.tkbHScroll.TabIndex = 0;
            this.tkbHScroll.TabStop = false;
            this.tkbHScroll.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // tkbVScroll
            // 
            this.tkbVScroll.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tkbVScroll.Location = new System.Drawing.Point(991, 96);
            this.tkbVScroll.Maximum = 100;
            this.tkbVScroll.Name = "tkbVScroll";
            this.tkbVScroll.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tkbVScroll.Size = new System.Drawing.Size(45, 733);
            this.tkbVScroll.TabIndex = 1;
            this.tkbVScroll.TabStop = false;
            this.tkbVScroll.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // ucSeamsView1
            // 
            this.ucSeamsView1.Location = new System.Drawing.Point(27, 155);
            this.ucSeamsView1.Name = "ucSeamsView1";
            this.ucSeamsView1.Size = new System.Drawing.Size(212, 302);
            this.ucSeamsView1.TabIndex = 21;
            // 
            // ucAreasView
            // 
            this.ucAreasView.Location = new System.Drawing.Point(27, 146);
            this.ucAreasView.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.ucAreasView.Name = "ucAreasView";
            this.ucAreasView.Size = new System.Drawing.Size(212, 302);
            this.ucAreasView.TabIndex = 20;
            // 
            // cccLineMode
            // 
            this.cccLineMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cccLineMode.Location = new System.Drawing.Point(26, 31);
            this.cccLineMode.Margin = new System.Windows.Forms.Padding(2);
            this.cccLineMode.Name = "cccLineMode";
            this.cccLineMode.Size = new System.Drawing.Size(176, 153);
            this.cccLineMode.TabIndex = 0;
            // 
            // cccAreaMode
            // 
            this.cccAreaMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cccAreaMode.Location = new System.Drawing.Point(28, 33);
            this.cccAreaMode.Margin = new System.Windows.Forms.Padding(2);
            this.cccAreaMode.Name = "cccAreaMode";
            this.cccAreaMode.Size = new System.Drawing.Size(176, 153);
            this.cccAreaMode.TabIndex = 0;
            // 
            // nudFixedWidthInches
            // 
            this.nudFixedWidthInches.Location = new System.Drawing.Point(153, 20);
            this.nudFixedWidthInches.Margin = new System.Windows.Forms.Padding(5);
            this.nudFixedWidthInches.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.nudFixedWidthInches.Name = "nudFixedWidthInches";
            this.nudFixedWidthInches.Size = new System.Drawing.Size(41, 22);
            this.nudFixedWidthInches.TabIndex = 2;
            this.nudFixedWidthInches.ValueChanged += new System.EventHandler(this.nudFixedWidthInches_ValueChanged);
            // 
            // nudFixedWidthFeet
            // 
            this.nudFixedWidthFeet.Location = new System.Drawing.Point(106, 20);
            this.nudFixedWidthFeet.Margin = new System.Windows.Forms.Padding(4);
            this.nudFixedWidthFeet.Name = "nudFixedWidthFeet";
            this.nudFixedWidthFeet.Size = new System.Drawing.Size(41, 22);
            this.nudFixedWidthFeet.TabIndex = 1;
            this.nudFixedWidthFeet.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFixedWidthFeet.ValueChanged += new System.EventHandler(this.nudFixedWidthFeet_ValueChanged);
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(288, 96);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(707, 780);
            this.axDrawingControl.TabIndex = 3;
            // 
            // FloorMaterialEstimatorBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(3285, 1288);
            this.Controls.Add(this.tkbHScroll);
            this.Controls.Add(this.tkbVScroll);
            this.Controls.Add(this.pnlVisioPageControlCover);
            this.Controls.Add(this.lblProjectName);
            this.Controls.Add(this.pnlSeamCommandPane);
            this.Controls.Add(this.pnlLineCommandPane);
            this.Controls.Add(this.pnlAreaCommandPane);
            this.Controls.Add(this.sssMainForm);
            this.Controls.Add(this.axDrawingControl);
            this.Controls.Add(this.tbcPageAreaLine);
            this.Controls.Add(this.tlsMainToolStrip);
            this.Controls.Add(this.musBaseMenuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MainMenuStrip = this.musBaseMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "FloorMaterialEstimatorBaseForm";
            this.Text = "Floor Materials Estimator";
            this.toolTip1.SetToolTip(this, "Snap To Grid");
            this.musBaseMenuStrip.ResumeLayout(false);
            this.musBaseMenuStrip.PerformLayout();
            this.tlsMainToolStrip.ResumeLayout(false);
            this.tlsMainToolStrip.PerformLayout();
            this.tbcPageAreaLine.ResumeLayout(false);
            this.sssMainForm.ResumeLayout(false);
            this.sssMainForm.PerformLayout();
            this.pnlLineCommandPane.ResumeLayout(false);
            this.grbLineModeCounter.ResumeLayout(false);
            this.grbLayoutLineMode.ResumeLayout(false);
            this.grbDoorTakeout.ResumeLayout(false);
            this.grbDoorTakeout.PerformLayout();
            this.btnLayoutLineModeButtons.ResumeLayout(false);
            this.grbLineModeSelection.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.grbEditAreaMode.ResumeLayout(false);
            this.grbEditAreaMode.PerformLayout();
            this.grbLayoutAreaMode.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlSection1.ResumeLayout(false);
            this.pnlAreaCommandPane.ResumeLayout(false);
            this.grbAreaModeCounter.ResumeLayout(false);
            this.pnlSeamCommandPane.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.grbSeaming.ResumeLayout(false);
            this.grbSeaming.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMoveIncrement)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.grbNormalSeams.ResumeLayout(false);
            this.grbNormalSeams.PerformLayout();
            this.grbAreaSubdivisionControls.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbHScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbVScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedWidthInches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedWidthFeet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip musBaseMenuStrip;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDrawMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnLineDesignState;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnFilterAreas;
        private System.Windows.Forms.ToolStripButton btnFilterLines;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnShowImage;
        private System.Windows.Forms.ToolStripButton btnHideImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnEditAreas;
        private System.Windows.Forms.ToolStripButton btnEditLines;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStrip tlsMainToolStrip;
        private System.Windows.Forms.TabPage tbpLines;
        private System.Windows.Forms.ToolStripButton btnDrawRectangle;
        private System.Windows.Forms.ToolStripButton btnDrawPolyline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.StatusStrip sssMainForm;
        private System.Windows.Forms.ToolStripStatusLabel tssFiller;
        private System.Windows.Forms.ToolStripStatusLabel tssLineSizeDecimal;
        private System.Windows.Forms.ToolStripStatusLabel tssLineSizeEnglish;
        private System.Windows.Forms.ToolStripLabel lblSeparator;
        internal System.Windows.Forms.ToolStripLabel lblCursorPosition;
        private System.Windows.Forms.ToolStripButton btnAreaEditSettings;
        private System.Windows.Forms.ToolStripButton btnLineEditSettings;
        public System.Windows.Forms.ToolStripStatusLabel tlsDrawingMode;
        public System.Windows.Forms.ToolStripStatusLabel tlsDrawingShape;
        private System.Windows.Forms.Panel pnlLineCommandPane;
        private System.Windows.Forms.GroupBox grbLayoutLineMode;
        private System.Windows.Forms.GroupBox grbEditLineMode;
        private System.Windows.Forms.Button btnEditLineApply;
        public System.Windows.Forms.Button btnEditLineClear;
        public System.Windows.Forms.Button btnEditLineUndo;
        public System.Windows.Forms.CheckBox ckbEditLineHighlightAndApply;
        private System.Windows.Forms.GroupBox grbLineModeSelection;
        private System.Windows.Forms.GroupBox grbDoorTakeout;
        private System.Windows.Forms.Button btnLayoutLineActivate;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnLayoutLineDuplicate;
        public System.Windows.Forms.RadioButton rbnDoorTakeoutOther;
        public System.Windows.Forms.RadioButton rbnDoorTakeout6Ft;
        public System.Windows.Forms.RadioButton rbnDoorTakeout3Ft;
        public System.Windows.Forms.TextBox txbDoorTakeoutOther;
        public System.Windows.Forms.Button btnDoorTakeoutShow;
        public System.Windows.Forms.ToolStripButton btnPanMode;
        public System.Windows.Forms.Button btnLineDesignStateLayoutMode;
        public System.Windows.Forms.Button btnLineDesignStateEditMode;
        public System.Windows.Forms.Button btnEditLineChangeLinesToSelected;
        public System.Windows.Forms.Button btnEditLineDeleteLines;
        public System.Windows.Forms.Button btnSetLinesTo1X;
        public System.Windows.Forms.Button btnSetLinesTo2X;
        public System.Windows.Forms.ToolStripButton btnSetCustomScale;
        public System.Windows.Forms.ToolStripButton btnTapeMeasure;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAreaDesignStateLayoutMode;
        private System.Windows.Forms.GroupBox grbEditAreaMode;
        private System.Windows.Forms.Button btnEditAreaApply;
        public System.Windows.Forms.Button btnEditAreaClear;
        public System.Windows.Forms.Button btnEditAreaUndo;
        public System.Windows.Forms.CheckBox ckbEditAreaHighlightAndApply;
        private System.Windows.Forms.Button btnEditShape;
        private System.Windows.Forms.Button btnChangeShapesToSelected;
        private System.Windows.Forms.Button btnDeleteShapes;
        private System.Windows.Forms.GroupBox grbLayoutAreaMode;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnLayoutAreaTakeout;
        public System.Windows.Forms.Button btnAreaDesignStateZeroLine;
        private System.Windows.Forms.Panel pnlSection1;
        private System.Windows.Forms.Panel pnlAreaCommandPane;
        public CanvasLib.Counters.CounterControl cccLineMode;
        public CanvasLib.Counters.CounterControl cccAreaMode;
        public System.Windows.Forms.ToolStripButton btnCounters;
        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        public System.Windows.Forms.TabControl tbcPageAreaLine;
        public System.Windows.Forms.Panel pnlFinishColor;
        public System.Windows.Forms.Label lblFinishName;
        private System.Windows.Forms.TabPage tbpSeams;
        private System.Windows.Forms.Panel pnlSeamCommandPane;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbnViewSeams;
        private System.Windows.Forms.RadioButton rbnViewAreas;
        private System.Windows.Forms.ToolStripButton btnEditSeams;
        public System.Windows.Forms.Button btnAreaDesignStateCompleteDrawing;
        public System.Windows.Forms.ToolStripButton btnShowAreas;
        public System.Windows.Forms.ToolStripButton btnHideAreas;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.Button btnSeamDesignStateSelectionMode;
        public System.Windows.Forms.Button btnSeamDesignStateSubdivisionMode;
        private System.Windows.Forms.ToolStripButton btnCombos;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem btnExistingProject;
        private System.Windows.Forms.ToolStripMenuItem btnSaveProject;
        private System.Windows.Forms.ToolStripMenuItem btnSaveProjectAs;
        private System.Windows.Forms.ToolStripMenuItem btnHelp;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox ckbFixedWidth;
        private System.Windows.Forms.ToolStripMenuItem btnNewProject;
        private System.Windows.Forms.Label lblProjectName;
        public System.Windows.Forms.ToolStripStatusLabel tlsDesignState;
        private System.Windows.Forms.ToolStripButton btnDebug;
        public System.Windows.Forms.Button btnAreaDesignStateEditMode;
        public System.Windows.Forms.Button btnLayoutLine2XMode;
        public System.Windows.Forms.Button btnLayoutLine1XMode;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
        public Utilities.NumericUpDownExtended nudFixedWidthInches;
        public Utilities.NumericUpDownExtended nudFixedWidthFeet;
        private System.Windows.Forms.Panel pnlVisioPageControlCover;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        public System.Windows.Forms.ToolStripStatusLabel tlsMouseXY;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        public System.Windows.Forms.Button btnCompleteShapeByIntersection;
        public System.Windows.Forms.TrackBar tkbHScroll;
        public System.Windows.Forms.TrackBar tkbVScroll;
        public System.Windows.Forms.ToolStripButton btnZoomIn;
        public System.Windows.Forms.ToolStripButton btnZoomOut;
        public System.Windows.Forms.ToolStripDropDownButton ddbZoomPercent;
        public System.Windows.Forms.ToolStripButton btnFitCanvas;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem btnCreateImage;
        public System.Windows.Forms.ToolStripButton btnSeamDesignState;
        private System.Windows.Forms.ToolStripMenuItem btnNewBlankProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripButton btnToolStripSave;
        private System.Windows.Forms.ToolStripStatusLabel lblCursorType;
        private System.Windows.Forms.ToolStripButton btnFullScreen;
        public System.Windows.Forms.Button btnLayoutLineJump;
        public System.Windows.Forms.Button btnLayoutAreaTakeOutAndFill;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        public System.Windows.Forms.ToolStripButton btnShowFieldGuides;
        public System.Windows.Forms.ToolStripButton btnHideFieldGuides;
        public System.Windows.Forms.ToolStripButton btnDeleteFieldGuides;
        private System.Windows.Forms.ToolStripButton btnEditFieldGuides;
        private System.Windows.Forms.GroupBox grbAreaSubdivisionControls;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.RadioButton rbnSubdivideByArea;
        public System.Windows.Forms.RadioButton rbnSubdivideByLine;
        private System.Windows.Forms.Button btnRemoveSubdivision;
        private System.Windows.Forms.Button btnCancelSubdivision;
        private System.Windows.Forms.Button btnCompleteSubdivision;
        private System.Windows.Forms.ToolStripMenuItem btnKeyboardAndMouseActions;
        private System.Windows.Forms.ToolStripButton btnShortcuts;
        public System.Windows.Forms.Button btnEmbeddLayoutAreas;
        private System.Windows.Forms.ToolStripButton btnOversUnders;
        private System.Windows.Forms.ToolStripMenuItem tsmLegend;
        private System.Windows.Forms.ToolStripMenuItem btnLegendLeft;
        private System.Windows.Forms.ToolStripMenuItem btnLegendRight;
        private System.Windows.Forms.ToolStripMenuItem btnLegendNone;
        private System.Windows.Forms.GroupBox grbLineModeCounter;
        private System.Windows.Forms.GroupBox grbAreaModeCounter;
        public System.Windows.Forms.Button btnShowSeamingTool;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnSeamArea;
        public System.Windows.Forms.Button btnSeamSingleLineToTool;
        public System.Windows.Forms.Button btnAlignTool;
        public System.Windows.Forms.GroupBox grbSeaming;
        public System.Windows.Forms.Button btnSubdivideRegion;
        private System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.RadioButton rbnVirtualSeamsShowUnhideable;
        public System.Windows.Forms.RadioButton rbnVirtualSeamsHideAll;
        public System.Windows.Forms.RadioButton rbnVirtualSeamsShowAll;
        private System.Windows.Forms.GroupBox grbNormalSeams;
        public System.Windows.Forms.RadioButton rbnNormalSeamsShowAll;
        public System.Windows.Forms.RadioButton rbnNormalSeamsShowUnHideable;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown nudMoveIncrement;
        private System.Windows.Forms.ToolStripButton btnSummaryReport;
        public System.Windows.Forms.Button btnCenterSeamingToolInView;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox ckbShowCuts;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.RadioButton rbnNormalSeamsHideAll;
        private System.Windows.Forms.CheckBox ckbShowUnders;
        private System.Windows.Forms.CheckBox ckbShowOvers;
        public System.Windows.Forms.ToolStripButton btnRedoSeamsAndCuts;
        public System.Windows.Forms.ToolStripButton btnAreaDesignState;
        public System.Windows.Forms.TabPage tbpAreas;
        public System.Windows.Forms.Panel btnLayoutLineModeButtons;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        public System.Windows.Forms.ToolStripButton btnSnapToGrid;
        public CanvasLib.Area_and_Seam_Views.UCAreasView ucAreasView;
        private Finish_Controls.UCLineLite ucLineLite1;
        public CanvasLib.Area_and_Seam_Views.UCSeamsView ucSeamsView1;
    }
}

