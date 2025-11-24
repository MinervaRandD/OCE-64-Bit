
//-------------------------------------------------------------------------------//
// <copyright file="GlobalSettingsForm.Designer.cs"                              //
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
namespace SettingsLib
{
    partial class GlobalSettingsForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbcSettings = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.grbSeamAreaLocks = new System.Windows.Forms.GroupBox();
            this.ckbLockAreaWhen100PctSeamed = new System.Windows.Forms.CheckBox();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.nudZoomInOutButtonPercent = new System.Windows.Forms.NumericUpDown();
            this.grbRolledProductDefaults = new System.Windows.Forms.GroupBox();
            this.label37 = new System.Windows.Forms.Label();
            this.NudExtraPerCutInches = new System.Windows.Forms.NumericUpDown();
            this.label38 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.NudDefaultOverlapInches = new System.Windows.Forms.NumericUpDown();
            this.label36 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.NudDefaultRollWidthInches = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.NudDefaultRollWidthFeet = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.nudArrowMoveIncrement = new System.Windows.Forms.NumericUpDown();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txbGridOffsetInInches = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txbGridSpacingInInches = new System.Windows.Forms.TextBox();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.rbnRightHanded = new System.Windows.Forms.RadioButton();
            this.rbnLeftHanded = new System.Windows.Forms.RadioButton();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.nudMouseWheelZoomInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.ckbKeystrokes = new System.Windows.Forms.CheckBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.ckbShowSetScaleReminder = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.ckbAutoUpdateSeamsAndCuts = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txbDefaultHeight = new System.Windows.Forms.TextBox();
            this.txbDefaultWidth = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txbDefaultDrawingScaleInInches = new System.Windows.Forms.TextBox();
            this.ckbLockScrollWhenDrawingSmallerThanCanvas = new System.Windows.Forms.CheckBox();
            this.ckbShowPanAndZoom = new System.Windows.Forms.CheckBox();
            this.ckbShowRulers = new System.Windows.Forms.CheckBox();
            this.ckbShowGrid = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ckbKeepAllGuidesOnCanvas = new System.Windows.Forms.CheckBox();
            this.ckbKeepInitialGuideOnCanvas = new System.Windows.Forms.CheckBox();
            this.ckbShowGuides = new System.Windows.Forms.CheckBox();
            this.ckbShowMarker = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbMarkerWidth = new System.Windows.Forms.TextBox();
            this.grbLineDrawoutMode = new System.Windows.Forms.GroupBox();
            this.rbnHideLineDrawout = new System.Windows.Forms.RadioButton();
            this.rbnShowLineDrawout = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grbOperatingMode = new System.Windows.Forms.GroupBox();
            this.ckbStartupFullScreenMode = new System.Windows.Forms.CheckBox();
            this.rbnLineModeInitialDesignState = new System.Windows.Forms.RadioButton();
            this.rbnAreaModeInitialDesignState = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbpAdministrative = new System.Windows.Forms.TabPage();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txbMinUnderLengthForCombosInInches = new System.Windows.Forms.TextBox();
            this.txbMinUnderWidthForCombosInInches = new System.Windows.Forms.TextBox();
            this.txbMinOverLengthForCombosInInches = new System.Windows.Forms.TextBox();
            this.txbMinOverWidthForCombosInInches = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.grbAutosave = new System.Windows.Forms.GroupBox();
            this.ckbValidateOnProjectSave = new System.Windows.Forms.CheckBox();
            this.txbAutosaveIntervalInSeconds = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.ckbAutosaveEnabled = new System.Windows.Forms.CheckBox();
            this.ckbAllowEditingOfShortcutKeys = new System.Windows.Forms.CheckBox();
            this.ckbAllowEditingOfToolTips = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txbMinUnderLengthInInches = new System.Windows.Forms.TextBox();
            this.txbMinUnderWidthInInches = new System.Windows.Forms.TextBox();
            this.txbMinOverLengthInInches = new System.Windows.Forms.TextBox();
            this.txbMinOverWidthInInches = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ckbShowSeamEditFormAsModal = new System.Windows.Forms.CheckBox();
            this.ckbShowLineEditFormAsModal = new System.Windows.Forms.CheckBox();
            this.ckbShowAreaEditFormAsModal = new System.Windows.Forms.CheckBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.ckbShowLineFilterFormAsModal = new System.Windows.Forms.CheckBox();
            this.ckbShowAreaFilterFormAsModal = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txbSnapDistance = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txbSnapToAxisResolutionInDegrees = new System.Windows.Forms.TextBox();
            this.ckbSnapToAxis = new System.Windows.Forms.CheckBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txbGraphicsPrecision = new System.Windows.Forms.TextBox();
            this.tbpOther = new System.Windows.Forms.TabPage();
            this.grpSeamsCuts = new System.Windows.Forms.GroupBox();
            this.nmCutIndexCircleRadius = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.ckbShowCutIndexCircles = new System.Windows.Forms.CheckBox();
            this.nmUnderageNumbersFontsize = new System.Windows.Forms.NumericUpDown();
            this.nmOverageNumbersFontsize = new System.Windows.Forms.NumericUpDown();
            this.nmCutNumbersFontsize = new System.Windows.Forms.NumericUpDown();
            this.nmAreaNumbersFontsize = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txbSmallCircleRadius = new System.Windows.Forms.TextBox();
            this.txbSmallFontSizeInPts = new System.Windows.Forms.TextBox();
            this.txbMediumCircleRadius = new System.Windows.Forms.TextBox();
            this.txbMediumFontSizeInPts = new System.Windows.Forms.TextBox();
            this.txbLargeCircleRadius = new System.Windows.Forms.TextBox();
            this.txbLargeFontSizeInPts = new System.Windows.Forms.TextBox();
            this.tbpDebug = new System.Windows.Forms.TabPage();
            this.ckbUpdateDebugFormDynamically = new System.Windows.Forms.CheckBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.ckbTraceLevelException = new System.Windows.Forms.CheckBox();
            this.ckbTraceLevelMethodCall = new System.Windows.Forms.CheckBox();
            this.ckbTraceLevelError = new System.Windows.Forms.CheckBox();
            this.ckbTraceLevelInfo = new System.Windows.Forms.CheckBox();
            this.ckbValidateRolloutAndCutWidths = new System.Windows.Forms.CheckBox();
            this.grbAreaOutlines = new System.Windows.Forms.GroupBox();
            this.CkbShowAreaOutlinesInLineMode = new System.Windows.Forms.CheckBox();
            this.tbcSettings.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.grbSeamAreaLocks.SuspendLayout();
            this.groupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZoomInOutButtonPercent)).BeginInit();
            this.grbRolledProductDefaults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudExtraPerCutInches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudDefaultOverlapInches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudDefaultRollWidthInches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudDefaultRollWidthFeet)).BeginInit();
            this.groupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArrowMoveIncrement)).BeginInit();
            this.groupBox16.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMouseWheelZoomInterval)).BeginInit();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grbLineDrawoutMode.SuspendLayout();
            this.grbOperatingMode.SuspendLayout();
            this.tbpAdministrative.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.grbAutosave.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tbpOther.SuspendLayout();
            this.grpSeamsCuts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmCutIndexCircleRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUnderageNumbersFontsize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmOverageNumbersFontsize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmCutNumbersFontsize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAreaNumbersFontsize)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tbpDebug.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.grbAreaOutlines.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(255, 635);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(423, 635);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbcSettings
            // 
            this.tbcSettings.Controls.Add(this.tbpGeneral);
            this.tbcSettings.Controls.Add(this.tbpAdministrative);
            this.tbcSettings.Controls.Add(this.tbpOther);
            this.tbcSettings.Controls.Add(this.tbpDebug);
            this.tbcSettings.Location = new System.Drawing.Point(17, 12);
            this.tbcSettings.Name = "tbcSettings";
            this.tbcSettings.SelectedIndex = 0;
            this.tbcSettings.Size = new System.Drawing.Size(745, 603);
            this.tbcSettings.TabIndex = 4;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.grbAreaOutlines);
            this.tbpGeneral.Controls.Add(this.grbSeamAreaLocks);
            this.tbpGeneral.Controls.Add(this.groupBox20);
            this.tbpGeneral.Controls.Add(this.grbRolledProductDefaults);
            this.tbpGeneral.Controls.Add(this.groupBox18);
            this.tbpGeneral.Controls.Add(this.groupBox16);
            this.tbpGeneral.Controls.Add(this.groupBox17);
            this.tbpGeneral.Controls.Add(this.groupBox14);
            this.tbpGeneral.Controls.Add(this.groupBox13);
            this.tbpGeneral.Controls.Add(this.groupBox12);
            this.tbpGeneral.Controls.Add(this.groupBox8);
            this.tbpGeneral.Controls.Add(this.groupBox5);
            this.tbpGeneral.Controls.Add(this.groupBox4);
            this.tbpGeneral.Controls.Add(this.grbLineDrawoutMode);
            this.tbpGeneral.Controls.Add(this.grbOperatingMode);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(737, 577);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // grbSeamAreaLocks
            // 
            this.grbSeamAreaLocks.Controls.Add(this.ckbLockAreaWhen100PctSeamed);
            this.grbSeamAreaLocks.Location = new System.Drawing.Point(240, 476);
            this.grbSeamAreaLocks.Name = "grbSeamAreaLocks";
            this.grbSeamAreaLocks.Size = new System.Drawing.Size(230, 59);
            this.grbSeamAreaLocks.TabIndex = 23;
            this.grbSeamAreaLocks.TabStop = false;
            this.grbSeamAreaLocks.Text = "Seam Area Locks";
            // 
            // ckbLockAreaWhen100PctSeamed
            // 
            this.ckbLockAreaWhen100PctSeamed.AutoSize = true;
            this.ckbLockAreaWhen100PctSeamed.Checked = true;
            this.ckbLockAreaWhen100PctSeamed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbLockAreaWhen100PctSeamed.Location = new System.Drawing.Point(19, 27);
            this.ckbLockAreaWhen100PctSeamed.Name = "ckbLockAreaWhen100PctSeamed";
            this.ckbLockAreaWhen100PctSeamed.Size = new System.Drawing.Size(178, 17);
            this.ckbLockAreaWhen100PctSeamed.TabIndex = 0;
            this.ckbLockAreaWhen100PctSeamed.Text = "Lock Area When 100% Seamed";
            this.ckbLockAreaWhen100PctSeamed.UseVisualStyleBackColor = true;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.nudZoomInOutButtonPercent);
            this.groupBox20.Location = new System.Drawing.Point(500, 404);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(225, 59);
            this.groupBox20.TabIndex = 22;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Zoom In / Zoom Out Button Increment (%)";
            // 
            // nudZoomInOutButtonPercent
            // 
            this.nudZoomInOutButtonPercent.Location = new System.Drawing.Point(67, 26);
            this.nudZoomInOutButtonPercent.Maximum = new decimal(new int[] {
            33,
            0,
            0,
            0});
            this.nudZoomInOutButtonPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudZoomInOutButtonPercent.Name = "nudZoomInOutButtonPercent";
            this.nudZoomInOutButtonPercent.Size = new System.Drawing.Size(49, 20);
            this.nudZoomInOutButtonPercent.TabIndex = 0;
            this.nudZoomInOutButtonPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudZoomInOutButtonPercent.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // grbRolledProductDefaults
            // 
            this.grbRolledProductDefaults.Controls.Add(this.label37);
            this.grbRolledProductDefaults.Controls.Add(this.NudExtraPerCutInches);
            this.grbRolledProductDefaults.Controls.Add(this.label38);
            this.grbRolledProductDefaults.Controls.Add(this.label35);
            this.grbRolledProductDefaults.Controls.Add(this.NudDefaultOverlapInches);
            this.grbRolledProductDefaults.Controls.Add(this.label36);
            this.grbRolledProductDefaults.Controls.Add(this.label34);
            this.grbRolledProductDefaults.Controls.Add(this.NudDefaultRollWidthInches);
            this.grbRolledProductDefaults.Controls.Add(this.label33);
            this.grbRolledProductDefaults.Controls.Add(this.NudDefaultRollWidthFeet);
            this.grbRolledProductDefaults.Controls.Add(this.label32);
            this.grbRolledProductDefaults.Location = new System.Drawing.Point(498, 254);
            this.grbRolledProductDefaults.Name = "grbRolledProductDefaults";
            this.grbRolledProductDefaults.Size = new System.Drawing.Size(227, 136);
            this.grbRolledProductDefaults.TabIndex = 21;
            this.grbRolledProductDefaults.TabStop = false;
            this.grbRolledProductDefaults.Text = "Rolled Product Defaults";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(170, 102);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(19, 13);
            this.label37.TabIndex = 11;
            this.label37.Text = "In.";
            // 
            // NudExtraPerCutInches
            // 
            this.NudExtraPerCutInches.Location = new System.Drawing.Point(127, 98);
            this.NudExtraPerCutInches.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.NudExtraPerCutInches.Name = "NudExtraPerCutInches";
            this.NudExtraPerCutInches.Size = new System.Drawing.Size(35, 20);
            this.NudExtraPerCutInches.TabIndex = 10;
            this.NudExtraPerCutInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(14, 102);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(106, 13);
            this.label38.TabIndex = 9;
            this.label38.Text = "Default Extra Per Cut";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(168, 68);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(19, 13);
            this.label35.TabIndex = 8;
            this.label35.Text = "In.";
            // 
            // NudDefaultOverlapInches
            // 
            this.NudDefaultOverlapInches.Location = new System.Drawing.Point(127, 68);
            this.NudDefaultOverlapInches.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.NudDefaultOverlapInches.Name = "NudDefaultOverlapInches";
            this.NudDefaultOverlapInches.Size = new System.Drawing.Size(35, 20);
            this.NudDefaultOverlapInches.TabIndex = 7;
            this.NudDefaultOverlapInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(15, 68);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(81, 13);
            this.label36.TabIndex = 6;
            this.label36.Text = "Default Overlap";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(206, 31);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(19, 13);
            this.label34.TabIndex = 5;
            this.label34.Text = "In.";
            // 
            // NudDefaultRollWidthInches
            // 
            this.NudDefaultRollWidthInches.Location = new System.Drawing.Point(166, 27);
            this.NudDefaultRollWidthInches.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.NudDefaultRollWidthInches.Name = "NudDefaultRollWidthInches";
            this.NudDefaultRollWidthInches.Size = new System.Drawing.Size(35, 20);
            this.NudDefaultRollWidthInches.TabIndex = 4;
            this.NudDefaultRollWidthInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(142, 31);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(19, 13);
            this.label33.TabIndex = 3;
            this.label33.Text = "Ft.";
            // 
            // NudDefaultRollWidthFeet
            // 
            this.NudDefaultRollWidthFeet.Location = new System.Drawing.Point(102, 27);
            this.NudDefaultRollWidthFeet.Name = "NudDefaultRollWidthFeet";
            this.NudDefaultRollWidthFeet.Size = new System.Drawing.Size(35, 20);
            this.NudDefaultRollWidthFeet.TabIndex = 2;
            this.NudDefaultRollWidthFeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(2, 31);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(96, 13);
            this.label32.TabIndex = 1;
            this.label32.Text = "Default Roll Width:";
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.nudArrowMoveIncrement);
            this.groupBox18.Location = new System.Drawing.Point(495, 176);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(230, 60);
            this.groupBox18.TabIndex = 20;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Arrow Move Increment";
            // 
            // nudArrowMoveIncrement
            // 
            this.nudArrowMoveIncrement.Location = new System.Drawing.Point(67, 26);
            this.nudArrowMoveIncrement.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudArrowMoveIncrement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudArrowMoveIncrement.Name = "nudArrowMoveIncrement";
            this.nudArrowMoveIncrement.Size = new System.Drawing.Size(49, 20);
            this.nudArrowMoveIncrement.TabIndex = 0;
            this.nudArrowMoveIncrement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudArrowMoveIncrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.label24);
            this.groupBox16.Controls.Add(this.txbGridOffsetInInches);
            this.groupBox16.Controls.Add(this.label23);
            this.groupBox16.Controls.Add(this.txbGridSpacingInInches);
            this.groupBox16.Location = new System.Drawing.Point(240, 374);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(230, 95);
            this.groupBox16.TabIndex = 19;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Grid Attributes";
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(7, 59);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(128, 20);
            this.label24.TabIndex = 27;
            this.label24.Text = "Grid offset in inches";
            // 
            // txbGridOffsetInInches
            // 
            this.txbGridOffsetInInches.Location = new System.Drawing.Point(162, 58);
            this.txbGridOffsetInInches.Name = "txbGridOffsetInInches";
            this.txbGridOffsetInInches.Size = new System.Drawing.Size(50, 20);
            this.txbGridOffsetInInches.TabIndex = 26;
            this.txbGridOffsetInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbGridOffsetInInches.TextChanged += new System.EventHandler(this.txbGridOffsetInInches_TextChanged);
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(7, 27);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(128, 20);
            this.label23.TabIndex = 25;
            this.label23.Text = "Grid spacing in inches";
            // 
            // txbGridSpacingInInches
            // 
            this.txbGridSpacingInInches.Location = new System.Drawing.Point(162, 26);
            this.txbGridSpacingInInches.Name = "txbGridSpacingInInches";
            this.txbGridSpacingInInches.Size = new System.Drawing.Size(50, 20);
            this.txbGridSpacingInInches.TabIndex = 22;
            this.txbGridSpacingInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbGridSpacingInInches.TextChanged += new System.EventHandler(this.txbGridSpacingInInches_TextChanged);
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.rbnRightHanded);
            this.groupBox17.Controls.Add(this.rbnLeftHanded);
            this.groupBox17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox17.Location = new System.Drawing.Point(498, 25);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(227, 55);
            this.groupBox17.TabIndex = 17;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Shortcut Orientation";
            // 
            // rbnRightHanded
            // 
            this.rbnRightHanded.AutoSize = true;
            this.rbnRightHanded.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnRightHanded.Location = new System.Drawing.Point(112, 19);
            this.rbnRightHanded.Name = "rbnRightHanded";
            this.rbnRightHanded.Size = new System.Drawing.Size(108, 20);
            this.rbnRightHanded.TabIndex = 1;
            this.rbnRightHanded.TabStop = true;
            this.rbnRightHanded.Text = "Right Handed";
            this.rbnRightHanded.UseVisualStyleBackColor = true;
            // 
            // rbnLeftHanded
            // 
            this.rbnLeftHanded.AutoSize = true;
            this.rbnLeftHanded.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnLeftHanded.Location = new System.Drawing.Point(11, 19);
            this.rbnLeftHanded.Name = "rbnLeftHanded";
            this.rbnLeftHanded.Size = new System.Drawing.Size(98, 20);
            this.rbnLeftHanded.TabIndex = 0;
            this.rbnLeftHanded.TabStop = true;
            this.rbnLeftHanded.Text = "Left Handed";
            this.rbnLeftHanded.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.nudMouseWheelZoomInterval);
            this.groupBox14.Location = new System.Drawing.Point(22, 477);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(200, 60);
            this.groupBox14.TabIndex = 16;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Mouse Wheel Zoom Interval";
            // 
            // nudMouseWheelZoomInterval
            // 
            this.nudMouseWheelZoomInterval.Location = new System.Drawing.Point(67, 26);
            this.nudMouseWheelZoomInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMouseWheelZoomInterval.Name = "nudMouseWheelZoomInterval";
            this.nudMouseWheelZoomInterval.Size = new System.Drawing.Size(49, 20);
            this.nudMouseWheelZoomInterval.TabIndex = 0;
            this.nudMouseWheelZoomInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudMouseWheelZoomInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.ckbKeystrokes);
            this.groupBox13.Location = new System.Drawing.Point(22, 383);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(200, 60);
            this.groupBox13.TabIndex = 15;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Mouse && Keyboard Keystrokes ";
            // 
            // ckbKeystrokes
            // 
            this.ckbKeystrokes.AutoSize = true;
            this.ckbKeystrokes.Location = new System.Drawing.Point(18, 26);
            this.ckbKeystrokes.Name = "ckbKeystrokes";
            this.ckbKeystrokes.Size = new System.Drawing.Size(114, 17);
            this.ckbKeystrokes.TabIndex = 18;
            this.ckbKeystrokes.Text = "Display keystrokes";
            this.ckbKeystrokes.UseVisualStyleBackColor = true;
            this.ckbKeystrokes.CheckedChanged += new System.EventHandler(this.ckbKeystrokes_CheckedChanged);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.ckbShowSetScaleReminder);
            this.groupBox12.Location = new System.Drawing.Point(22, 308);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(200, 60);
            this.groupBox12.TabIndex = 15;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Reminders";
            // 
            // ckbShowSetScaleReminder
            // 
            this.ckbShowSetScaleReminder.AutoSize = true;
            this.ckbShowSetScaleReminder.Location = new System.Drawing.Point(18, 26);
            this.ckbShowSetScaleReminder.Name = "ckbShowSetScaleReminder";
            this.ckbShowSetScaleReminder.Size = new System.Drawing.Size(150, 17);
            this.ckbShowSetScaleReminder.TabIndex = 18;
            this.ckbShowSetScaleReminder.Text = "Show Set Scale Reminder";
            this.ckbShowSetScaleReminder.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.ckbAutoUpdateSeamsAndCuts);
            this.groupBox8.Location = new System.Drawing.Point(495, 95);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(230, 66);
            this.groupBox8.TabIndex = 12;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Auto Update Seams and Cuts";
            // 
            // ckbAutoUpdateSeamsAndCuts
            // 
            this.ckbAutoUpdateSeamsAndCuts.AutoSize = true;
            this.ckbAutoUpdateSeamsAndCuts.Location = new System.Drawing.Point(16, 34);
            this.ckbAutoUpdateSeamsAndCuts.Name = "ckbAutoUpdateSeamsAndCuts";
            this.ckbAutoUpdateSeamsAndCuts.Size = new System.Drawing.Size(166, 17);
            this.ckbAutoUpdateSeamsAndCuts.TabIndex = 17;
            this.ckbAutoUpdateSeamsAndCuts.Text = "Auto Update Seams and Cuts";
            this.ckbAutoUpdateSeamsAndCuts.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.txbDefaultHeight);
            this.groupBox5.Controls.Add(this.txbDefaultWidth);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.txbDefaultDrawingScaleInInches);
            this.groupBox5.Controls.Add(this.ckbLockScrollWhenDrawingSmallerThanCanvas);
            this.groupBox5.Controls.Add(this.ckbShowPanAndZoom);
            this.groupBox5.Controls.Add(this.ckbShowRulers);
            this.groupBox5.Controls.Add(this.ckbShowGrid);
            this.groupBox5.Location = new System.Drawing.Point(240, 116);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(230, 243);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Grids, Rulers and Pan and Zoom";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(159, 183);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Height";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(100, 183);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "Width";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbDefaultHeight
            // 
            this.txbDefaultHeight.Location = new System.Drawing.Point(152, 199);
            this.txbDefaultHeight.Name = "txbDefaultHeight";
            this.txbDefaultHeight.Size = new System.Drawing.Size(51, 20);
            this.txbDefaultHeight.TabIndex = 22;
            this.txbDefaultHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbDefaultWidth
            // 
            this.txbDefaultWidth.Location = new System.Drawing.Point(90, 199);
            this.txbDefaultWidth.Name = "txbDefaultWidth";
            this.txbDefaultWidth.Size = new System.Drawing.Size(51, 20);
            this.txbDefaultWidth.TabIndex = 21;
            this.txbDefaultWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(13, 195);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 29);
            this.label13.TabIndex = 20;
            this.label13.Text = "Default new drawing size";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(13, 138);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(128, 29);
            this.label12.TabIndex = 19;
            this.label12.Text = "Default drawing scale in inches";
            // 
            // txbDefaultDrawingScaleInInches
            // 
            this.txbDefaultDrawingScaleInInches.Location = new System.Drawing.Point(144, 142);
            this.txbDefaultDrawingScaleInInches.Name = "txbDefaultDrawingScaleInInches";
            this.txbDefaultDrawingScaleInInches.Size = new System.Drawing.Size(53, 20);
            this.txbDefaultDrawingScaleInInches.TabIndex = 18;
            this.txbDefaultDrawingScaleInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbDefaultDrawingScaleInInches.TextChanged += new System.EventHandler(this.txbDefaultDrawingScaleInInches_TextChanged);
            // 
            // ckbLockScrollWhenDrawingSmallerThanCanvas
            // 
            this.ckbLockScrollWhenDrawingSmallerThanCanvas.AutoSize = true;
            this.ckbLockScrollWhenDrawingSmallerThanCanvas.Location = new System.Drawing.Point(16, 113);
            this.ckbLockScrollWhenDrawingSmallerThanCanvas.Name = "ckbLockScrollWhenDrawingSmallerThanCanvas";
            this.ckbLockScrollWhenDrawingSmallerThanCanvas.Size = new System.Drawing.Size(194, 17);
            this.ckbLockScrollWhenDrawingSmallerThanCanvas.TabIndex = 17;
            this.ckbLockScrollWhenDrawingSmallerThanCanvas.Text = "Lock Scroll On Undersized Drawing";
            this.ckbLockScrollWhenDrawingSmallerThanCanvas.UseVisualStyleBackColor = true;
            // 
            // ckbShowPanAndZoom
            // 
            this.ckbShowPanAndZoom.AutoSize = true;
            this.ckbShowPanAndZoom.Location = new System.Drawing.Point(16, 84);
            this.ckbShowPanAndZoom.Name = "ckbShowPanAndZoom";
            this.ckbShowPanAndZoom.Size = new System.Drawing.Size(127, 17);
            this.ckbShowPanAndZoom.TabIndex = 16;
            this.ckbShowPanAndZoom.Text = "Show Pan And Zoom";
            this.ckbShowPanAndZoom.UseVisualStyleBackColor = true;
            // 
            // ckbShowRulers
            // 
            this.ckbShowRulers.AutoSize = true;
            this.ckbShowRulers.Location = new System.Drawing.Point(16, 55);
            this.ckbShowRulers.Name = "ckbShowRulers";
            this.ckbShowRulers.Size = new System.Drawing.Size(86, 17);
            this.ckbShowRulers.TabIndex = 15;
            this.ckbShowRulers.Text = "Show Rulers";
            this.ckbShowRulers.UseVisualStyleBackColor = true;
            // 
            // ckbShowGrid
            // 
            this.ckbShowGrid.AutoSize = true;
            this.ckbShowGrid.Location = new System.Drawing.Point(16, 26);
            this.ckbShowGrid.Name = "ckbShowGrid";
            this.ckbShowGrid.Size = new System.Drawing.Size(75, 17);
            this.ckbShowGrid.TabIndex = 14;
            this.ckbShowGrid.Text = "Show Grid";
            this.ckbShowGrid.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ckbKeepAllGuidesOnCanvas);
            this.groupBox4.Controls.Add(this.ckbKeepInitialGuideOnCanvas);
            this.groupBox4.Controls.Add(this.ckbShowGuides);
            this.groupBox4.Controls.Add(this.ckbShowMarker);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txbMarkerWidth);
            this.groupBox4.Location = new System.Drawing.Point(19, 174);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 116);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Marker and Guides";
            // 
            // ckbKeepAllGuidesOnCanvas
            // 
            this.ckbKeepAllGuidesOnCanvas.AutoSize = true;
            this.ckbKeepAllGuidesOnCanvas.Location = new System.Drawing.Point(10, 157);
            this.ckbKeepAllGuidesOnCanvas.Name = "ckbKeepAllGuidesOnCanvas";
            this.ckbKeepAllGuidesOnCanvas.Size = new System.Drawing.Size(155, 17);
            this.ckbKeepAllGuidesOnCanvas.TabIndex = 13;
            this.ckbKeepAllGuidesOnCanvas.Text = "Keep All Guides on Canvas";
            this.ckbKeepAllGuidesOnCanvas.UseVisualStyleBackColor = true;
            // 
            // ckbKeepInitialGuideOnCanvas
            // 
            this.ckbKeepInitialGuideOnCanvas.AutoSize = true;
            this.ckbKeepInitialGuideOnCanvas.Location = new System.Drawing.Point(10, 122);
            this.ckbKeepInitialGuideOnCanvas.Name = "ckbKeepInitialGuideOnCanvas";
            this.ckbKeepInitialGuideOnCanvas.Size = new System.Drawing.Size(163, 17);
            this.ckbKeepInitialGuideOnCanvas.TabIndex = 12;
            this.ckbKeepInitialGuideOnCanvas.Text = "Keep Initial Guide on Canvas";
            this.ckbKeepInitialGuideOnCanvas.UseVisualStyleBackColor = true;
            // 
            // ckbShowGuides
            // 
            this.ckbShowGuides.AutoSize = true;
            this.ckbShowGuides.Location = new System.Drawing.Point(10, 86);
            this.ckbShowGuides.Name = "ckbShowGuides";
            this.ckbShowGuides.Size = new System.Drawing.Size(89, 17);
            this.ckbShowGuides.TabIndex = 11;
            this.ckbShowGuides.Text = "Show Guides";
            this.ckbShowGuides.UseVisualStyleBackColor = true;
            // 
            // ckbShowMarker
            // 
            this.ckbShowMarker.AutoSize = true;
            this.ckbShowMarker.Location = new System.Drawing.Point(10, 20);
            this.ckbShowMarker.Name = "ckbShowMarker";
            this.ckbShowMarker.Size = new System.Drawing.Size(89, 17);
            this.ckbShowMarker.TabIndex = 10;
            this.ckbShowMarker.Text = "Show Marker";
            this.ckbShowMarker.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Marker width";
            // 
            // txbMarkerWidth
            // 
            this.txbMarkerWidth.Location = new System.Drawing.Point(117, 47);
            this.txbMarkerWidth.Name = "txbMarkerWidth";
            this.txbMarkerWidth.Size = new System.Drawing.Size(47, 20);
            this.txbMarkerWidth.TabIndex = 8;
            // 
            // grbLineDrawoutMode
            // 
            this.grbLineDrawoutMode.Controls.Add(this.rbnHideLineDrawout);
            this.grbLineDrawoutMode.Controls.Add(this.rbnShowLineDrawout);
            this.grbLineDrawoutMode.Controls.Add(this.groupBox3);
            this.grbLineDrawoutMode.Location = new System.Drawing.Point(240, 24);
            this.grbLineDrawoutMode.Name = "grbLineDrawoutMode";
            this.grbLineDrawoutMode.Size = new System.Drawing.Size(230, 79);
            this.grbLineDrawoutMode.TabIndex = 4;
            this.grbLineDrawoutMode.TabStop = false;
            this.grbLineDrawoutMode.Text = "Line Drawout Mode";
            // 
            // rbnHideLineDrawout
            // 
            this.rbnHideLineDrawout.AutoSize = true;
            this.rbnHideLineDrawout.Location = new System.Drawing.Point(19, 52);
            this.rbnHideLineDrawout.Name = "rbnHideLineDrawout";
            this.rbnHideLineDrawout.Size = new System.Drawing.Size(107, 17);
            this.rbnHideLineDrawout.TabIndex = 7;
            this.rbnHideLineDrawout.Text = "Hide line drawout";
            this.rbnHideLineDrawout.UseVisualStyleBackColor = true;
            // 
            // rbnShowLineDrawout
            // 
            this.rbnShowLineDrawout.AutoSize = true;
            this.rbnShowLineDrawout.Checked = true;
            this.rbnShowLineDrawout.Location = new System.Drawing.Point(19, 23);
            this.rbnShowLineDrawout.Name = "rbnShowLineDrawout";
            this.rbnShowLineDrawout.Size = new System.Drawing.Size(112, 17);
            this.rbnShowLineDrawout.TabIndex = 6;
            this.rbnShowLineDrawout.TabStop = true;
            this.rbnShowLineDrawout.Text = "Show line drawout";
            this.rbnShowLineDrawout.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(3, 131);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // grbOperatingMode
            // 
            this.grbOperatingMode.Controls.Add(this.ckbStartupFullScreenMode);
            this.grbOperatingMode.Controls.Add(this.rbnLineModeInitialDesignState);
            this.grbOperatingMode.Controls.Add(this.rbnAreaModeInitialDesignState);
            this.grbOperatingMode.Controls.Add(this.groupBox2);
            this.grbOperatingMode.Location = new System.Drawing.Point(19, 22);
            this.grbOperatingMode.Name = "grbOperatingMode";
            this.grbOperatingMode.Size = new System.Drawing.Size(200, 128);
            this.grbOperatingMode.TabIndex = 3;
            this.grbOperatingMode.TabStop = false;
            this.grbOperatingMode.Text = "Initial Design State";
            // 
            // ckbStartupFullScreenMode
            // 
            this.ckbStartupFullScreenMode.AutoSize = true;
            this.ckbStartupFullScreenMode.Location = new System.Drawing.Point(21, 100);
            this.ckbStartupFullScreenMode.Name = "ckbStartupFullScreenMode";
            this.ckbStartupFullScreenMode.Size = new System.Drawing.Size(149, 17);
            this.ckbStartupFullScreenMode.TabIndex = 6;
            this.ckbStartupFullScreenMode.Text = "Open in Full Screen Mode";
            this.ckbStartupFullScreenMode.UseVisualStyleBackColor = true;
            // 
            // rbnLineModeInitialDesignState
            // 
            this.rbnLineModeInitialDesignState.AutoSize = true;
            this.rbnLineModeInitialDesignState.Location = new System.Drawing.Point(22, 64);
            this.rbnLineModeInitialDesignState.Name = "rbnLineModeInitialDesignState";
            this.rbnLineModeInitialDesignState.Size = new System.Drawing.Size(75, 17);
            this.rbnLineModeInitialDesignState.TabIndex = 5;
            this.rbnLineModeInitialDesignState.Text = "Line Mode";
            this.rbnLineModeInitialDesignState.UseVisualStyleBackColor = true;
            // 
            // rbnAreaModeInitialDesignState
            // 
            this.rbnAreaModeInitialDesignState.AutoSize = true;
            this.rbnAreaModeInitialDesignState.Checked = true;
            this.rbnAreaModeInitialDesignState.Location = new System.Drawing.Point(22, 28);
            this.rbnAreaModeInitialDesignState.Name = "rbnAreaModeInitialDesignState";
            this.rbnAreaModeInitialDesignState.Size = new System.Drawing.Size(77, 17);
            this.rbnAreaModeInitialDesignState.TabIndex = 4;
            this.rbnAreaModeInitialDesignState.TabStop = true;
            this.rbnAreaModeInitialDesignState.Text = "Area Mode";
            this.rbnAreaModeInitialDesignState.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(3, 131);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // tbpAdministrative
            // 
            this.tbpAdministrative.Controls.Add(this.groupBox19);
            this.tbpAdministrative.Controls.Add(this.grbAutosave);
            this.tbpAdministrative.Controls.Add(this.ckbAllowEditingOfShortcutKeys);
            this.tbpAdministrative.Controls.Add(this.ckbAllowEditingOfToolTips);
            this.tbpAdministrative.Controls.Add(this.groupBox7);
            this.tbpAdministrative.Controls.Add(this.groupBox6);
            this.tbpAdministrative.Controls.Add(this.groupBox10);
            this.tbpAdministrative.Controls.Add(this.groupBox1);
            this.tbpAdministrative.Controls.Add(this.groupBox11);
            this.tbpAdministrative.Location = new System.Drawing.Point(4, 22);
            this.tbpAdministrative.Name = "tbpAdministrative";
            this.tbpAdministrative.Size = new System.Drawing.Size(737, 577);
            this.tbpAdministrative.TabIndex = 3;
            this.tbpAdministrative.Text = "Administrative";
            this.tbpAdministrative.UseVisualStyleBackColor = true;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.label30);
            this.groupBox19.Controls.Add(this.txbMinUnderLengthForCombosInInches);
            this.groupBox19.Controls.Add(this.txbMinUnderWidthForCombosInInches);
            this.groupBox19.Controls.Add(this.txbMinOverLengthForCombosInInches);
            this.groupBox19.Controls.Add(this.txbMinOverWidthForCombosInInches);
            this.groupBox19.Controls.Add(this.label25);
            this.groupBox19.Controls.Add(this.label26);
            this.groupBox19.Controls.Add(this.label27);
            this.groupBox19.Controls.Add(this.label28);
            this.groupBox19.Location = new System.Drawing.Point(292, 356);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(267, 200);
            this.groupBox19.TabIndex = 20;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Minimum Underages and Overages For Combination";
            // 
            // label30
            // 
            this.label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label30.Location = new System.Drawing.Point(42, 23);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(197, 35);
            this.label30.TabIndex = 23;
            this.label30.Text = "Minimum values for using elements\r\nin the combinations";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txbMinUnderLengthForCombosInInches
            // 
            this.txbMinUnderLengthForCombosInInches.Location = new System.Drawing.Point(220, 162);
            this.txbMinUnderLengthForCombosInInches.Name = "txbMinUnderLengthForCombosInInches";
            this.txbMinUnderLengthForCombosInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinUnderLengthForCombosInInches.TabIndex = 7;
            this.txbMinUnderLengthForCombosInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbMinUnderLengthForCombosInInches.TextChanged += new System.EventHandler(this.txbMinUnderLengthForCombosInInches_TextChanged);
            // 
            // txbMinUnderWidthForCombosInInches
            // 
            this.txbMinUnderWidthForCombosInInches.Location = new System.Drawing.Point(220, 131);
            this.txbMinUnderWidthForCombosInInches.Name = "txbMinUnderWidthForCombosInInches";
            this.txbMinUnderWidthForCombosInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinUnderWidthForCombosInInches.TabIndex = 6;
            this.txbMinUnderWidthForCombosInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbMinUnderWidthForCombosInInches.TextChanged += new System.EventHandler(this.txbMinUnderWidthForCombosInInches_TextChanged);
            // 
            // txbMinOverLengthForCombosInInches
            // 
            this.txbMinOverLengthForCombosInInches.Location = new System.Drawing.Point(220, 100);
            this.txbMinOverLengthForCombosInInches.Name = "txbMinOverLengthForCombosInInches";
            this.txbMinOverLengthForCombosInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinOverLengthForCombosInInches.TabIndex = 5;
            this.txbMinOverLengthForCombosInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbMinOverLengthForCombosInInches.TextChanged += new System.EventHandler(this.txbMinOverLengthForCombosInInches_TextChanged);
            // 
            // txbMinOverWidthForCombosInInches
            // 
            this.txbMinOverWidthForCombosInInches.Location = new System.Drawing.Point(221, 69);
            this.txbMinOverWidthForCombosInInches.Name = "txbMinOverWidthForCombosInInches";
            this.txbMinOverWidthForCombosInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinOverWidthForCombosInInches.TabIndex = 4;
            this.txbMinOverWidthForCombosInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbMinOverWidthForCombosInInches.TextChanged += new System.EventHandler(this.txbMinOverWidthForCombosInInches_TextChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(5, 166);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(212, 13);
            this.label25.TabIndex = 3;
            this.label25.Text = "Min Underage Length for Combos in Inches";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(7, 135);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(207, 13);
            this.label26.TabIndex = 2;
            this.label26.Text = "Min Underage Width for Combos in Inches";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 104);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(206, 13);
            this.label27.TabIndex = 1;
            this.label27.Text = "Min Overage Length for Combos in Inches";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 73);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(201, 13);
            this.label28.TabIndex = 0;
            this.label28.Text = "Min Overage Width for Combos in Inches";
            // 
            // grbAutosave
            // 
            this.grbAutosave.Controls.Add(this.ckbValidateOnProjectSave);
            this.grbAutosave.Controls.Add(this.txbAutosaveIntervalInSeconds);
            this.grbAutosave.Controls.Add(this.label22);
            this.grbAutosave.Controls.Add(this.ckbAutosaveEnabled);
            this.grbAutosave.Location = new System.Drawing.Point(17, 413);
            this.grbAutosave.Name = "grbAutosave";
            this.grbAutosave.Size = new System.Drawing.Size(241, 143);
            this.grbAutosave.TabIndex = 22;
            this.grbAutosave.TabStop = false;
            this.grbAutosave.Text = "Autosave and Save Validation";
            // 
            // ckbValidateOnProjectSave
            // 
            this.ckbValidateOnProjectSave.AutoSize = true;
            this.ckbValidateOnProjectSave.Checked = true;
            this.ckbValidateOnProjectSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbValidateOnProjectSave.Location = new System.Drawing.Point(24, 107);
            this.ckbValidateOnProjectSave.Name = "ckbValidateOnProjectSave";
            this.ckbValidateOnProjectSave.Size = new System.Drawing.Size(162, 17);
            this.ckbValidateOnProjectSave.TabIndex = 10;
            this.ckbValidateOnProjectSave.Text = "Saved file validation enabled";
            this.ckbValidateOnProjectSave.UseVisualStyleBackColor = true;
            // 
            // txbAutosaveIntervalInSeconds
            // 
            this.txbAutosaveIntervalInSeconds.Location = new System.Drawing.Point(169, 64);
            this.txbAutosaveIntervalInSeconds.Name = "txbAutosaveIntervalInSeconds";
            this.txbAutosaveIntervalInSeconds.Size = new System.Drawing.Size(40, 20);
            this.txbAutosaveIntervalInSeconds.TabIndex = 9;
            this.txbAutosaveIntervalInSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(20, 64);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(143, 13);
            this.label22.TabIndex = 8;
            this.label22.Text = "Autosave interval in seconds";
            // 
            // ckbAutosaveEnabled
            // 
            this.ckbAutosaveEnabled.AutoSize = true;
            this.ckbAutosaveEnabled.Location = new System.Drawing.Point(24, 28);
            this.ckbAutosaveEnabled.Name = "ckbAutosaveEnabled";
            this.ckbAutosaveEnabled.Size = new System.Drawing.Size(112, 17);
            this.ckbAutosaveEnabled.TabIndex = 2;
            this.ckbAutosaveEnabled.Text = "Autosave enabled";
            this.ckbAutosaveEnabled.UseVisualStyleBackColor = true;
            // 
            // ckbAllowEditingOfShortcutKeys
            // 
            this.ckbAllowEditingOfShortcutKeys.AutoSize = true;
            this.ckbAllowEditingOfShortcutKeys.Location = new System.Drawing.Point(57, 384);
            this.ckbAllowEditingOfShortcutKeys.Name = "ckbAllowEditingOfShortcutKeys";
            this.ckbAllowEditingOfShortcutKeys.Size = new System.Drawing.Size(167, 17);
            this.ckbAllowEditingOfShortcutKeys.TabIndex = 21;
            this.ckbAllowEditingOfShortcutKeys.Text = "Allow Editing of Shortcut Keys";
            this.ckbAllowEditingOfShortcutKeys.UseVisualStyleBackColor = true;
            // 
            // ckbAllowEditingOfToolTips
            // 
            this.ckbAllowEditingOfToolTips.AutoSize = true;
            this.ckbAllowEditingOfToolTips.Location = new System.Drawing.Point(57, 351);
            this.ckbAllowEditingOfToolTips.Name = "ckbAllowEditingOfToolTips";
            this.ckbAllowEditingOfToolTips.Size = new System.Drawing.Size(138, 17);
            this.ckbAllowEditingOfToolTips.TabIndex = 20;
            this.ckbAllowEditingOfToolTips.Text = "Allow Editing of Tooltips";
            this.ckbAllowEditingOfToolTips.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label29);
            this.groupBox7.Controls.Add(this.txbMinUnderLengthInInches);
            this.groupBox7.Controls.Add(this.txbMinUnderWidthInInches);
            this.groupBox7.Controls.Add(this.txbMinOverLengthInInches);
            this.groupBox7.Controls.Add(this.txbMinOverWidthInInches);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Location = new System.Drawing.Point(292, 151);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(267, 195);
            this.groupBox7.TabIndex = 19;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Minimum Underages and Overages In System";
            // 
            // label29
            // 
            this.label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label29.Location = new System.Drawing.Point(42, 25);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(197, 35);
            this.label29.TabIndex = 8;
            this.label29.Text = "Minimum values for keeping elements\r\nin the system";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txbMinUnderLengthInInches
            // 
            this.txbMinUnderLengthInInches.Location = new System.Drawing.Point(213, 165);
            this.txbMinUnderLengthInInches.Name = "txbMinUnderLengthInInches";
            this.txbMinUnderLengthInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinUnderLengthInInches.TabIndex = 7;
            this.txbMinUnderLengthInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbMinUnderLengthInInches.TextChanged += new System.EventHandler(this.txbMinUnderLengthInInches_TextChanged);
            // 
            // txbMinUnderWidthInInches
            // 
            this.txbMinUnderWidthInInches.Location = new System.Drawing.Point(213, 134);
            this.txbMinUnderWidthInInches.Name = "txbMinUnderWidthInInches";
            this.txbMinUnderWidthInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinUnderWidthInInches.TabIndex = 6;
            this.txbMinUnderWidthInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbMinUnderWidthInInches.TextChanged += new System.EventHandler(this.txbMinUnderWidthInInches_TextChanged);
            // 
            // txbMinOverLengthInInches
            // 
            this.txbMinOverLengthInInches.Location = new System.Drawing.Point(213, 103);
            this.txbMinOverLengthInInches.Name = "txbMinOverLengthInInches";
            this.txbMinOverLengthInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinOverLengthInInches.TabIndex = 5;
            this.txbMinOverLengthInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbMinOverLengthInInches.TextChanged += new System.EventHandler(this.txbMinOverLengthInInches_TextChanged);
            // 
            // txbMinOverWidthInInches
            // 
            this.txbMinOverWidthInInches.Location = new System.Drawing.Point(213, 72);
            this.txbMinOverWidthInInches.Name = "txbMinOverWidthInInches";
            this.txbMinOverWidthInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinOverWidthInInches.TabIndex = 4;
            this.txbMinOverWidthInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbMinOverWidthInInches.TextChanged += new System.EventHandler(this.txbMinOverWidthInInches_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Minimum Underage Length in Inches";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Minimum Underage Width in Inches";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Minimum Overage Length in Inches";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Minimum Overage Width in Inches";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ckbShowSeamEditFormAsModal);
            this.groupBox6.Controls.Add(this.ckbShowLineEditFormAsModal);
            this.groupBox6.Controls.Add(this.ckbShowAreaEditFormAsModal);
            this.groupBox6.Location = new System.Drawing.Point(297, 22);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(262, 116);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Edit Forms";
            // 
            // ckbShowSeamEditFormAsModal
            // 
            this.ckbShowSeamEditFormAsModal.AutoSize = true;
            this.ckbShowSeamEditFormAsModal.Checked = true;
            this.ckbShowSeamEditFormAsModal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowSeamEditFormAsModal.Location = new System.Drawing.Point(18, 92);
            this.ckbShowSeamEditFormAsModal.Name = "ckbShowSeamEditFormAsModal";
            this.ckbShowSeamEditFormAsModal.Size = new System.Drawing.Size(177, 17);
            this.ckbShowSeamEditFormAsModal.TabIndex = 2;
            this.ckbShowSeamEditFormAsModal.Text = "Show Seam Edit Form As Modal";
            this.ckbShowSeamEditFormAsModal.UseVisualStyleBackColor = true;
            // 
            // ckbShowLineEditFormAsModal
            // 
            this.ckbShowLineEditFormAsModal.AutoSize = true;
            this.ckbShowLineEditFormAsModal.Checked = true;
            this.ckbShowLineEditFormAsModal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowLineEditFormAsModal.Location = new System.Drawing.Point(18, 60);
            this.ckbShowLineEditFormAsModal.Name = "ckbShowLineEditFormAsModal";
            this.ckbShowLineEditFormAsModal.Size = new System.Drawing.Size(170, 17);
            this.ckbShowLineEditFormAsModal.TabIndex = 1;
            this.ckbShowLineEditFormAsModal.Text = "Show Line Edit Form As Modal";
            this.ckbShowLineEditFormAsModal.UseVisualStyleBackColor = true;
            // 
            // ckbShowAreaEditFormAsModal
            // 
            this.ckbShowAreaEditFormAsModal.AutoSize = true;
            this.ckbShowAreaEditFormAsModal.Checked = true;
            this.ckbShowAreaEditFormAsModal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowAreaEditFormAsModal.Location = new System.Drawing.Point(18, 28);
            this.ckbShowAreaEditFormAsModal.Name = "ckbShowAreaEditFormAsModal";
            this.ckbShowAreaEditFormAsModal.Size = new System.Drawing.Size(172, 17);
            this.ckbShowAreaEditFormAsModal.TabIndex = 0;
            this.ckbShowAreaEditFormAsModal.Text = "Show Area Edit Form As Modal";
            this.ckbShowAreaEditFormAsModal.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.ckbShowLineFilterFormAsModal);
            this.groupBox10.Controls.Add(this.ckbShowAreaFilterFormAsModal);
            this.groupBox10.Location = new System.Drawing.Point(19, 246);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(241, 94);
            this.groupBox10.TabIndex = 17;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Filter Forms";
            // 
            // ckbShowLineFilterFormAsModal
            // 
            this.ckbShowLineFilterFormAsModal.AutoSize = true;
            this.ckbShowLineFilterFormAsModal.Checked = true;
            this.ckbShowLineFilterFormAsModal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowLineFilterFormAsModal.Location = new System.Drawing.Point(18, 60);
            this.ckbShowLineFilterFormAsModal.Name = "ckbShowLineFilterFormAsModal";
            this.ckbShowLineFilterFormAsModal.Size = new System.Drawing.Size(174, 17);
            this.ckbShowLineFilterFormAsModal.TabIndex = 1;
            this.ckbShowLineFilterFormAsModal.Text = "Show Line Filter Form As Modal";
            this.ckbShowLineFilterFormAsModal.UseVisualStyleBackColor = true;
            // 
            // ckbShowAreaFilterFormAsModal
            // 
            this.ckbShowAreaFilterFormAsModal.AutoSize = true;
            this.ckbShowAreaFilterFormAsModal.Checked = true;
            this.ckbShowAreaFilterFormAsModal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowAreaFilterFormAsModal.Location = new System.Drawing.Point(18, 28);
            this.ckbShowAreaFilterFormAsModal.Name = "ckbShowAreaFilterFormAsModal";
            this.ckbShowAreaFilterFormAsModal.Size = new System.Drawing.Size(176, 17);
            this.ckbShowAreaFilterFormAsModal.TabIndex = 0;
            this.ckbShowAreaFilterFormAsModal.Text = "Show Area Filter Form As Modal";
            this.ckbShowAreaFilterFormAsModal.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txbSnapDistance);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txbSnapToAxisResolutionInDegrees);
            this.groupBox1.Controls.Add(this.ckbSnapToAxis);
            this.groupBox1.Location = new System.Drawing.Point(18, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 133);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Snap to Axis";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 87);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(159, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Snap to guide minimum distance";
            // 
            // txbSnapDistance
            // 
            this.txbSnapDistance.Location = new System.Drawing.Point(192, 87);
            this.txbSnapDistance.Name = "txbSnapDistance";
            this.txbSnapDistance.Size = new System.Drawing.Size(38, 20);
            this.txbSnapDistance.TabIndex = 3;
            this.txbSnapDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Snap to guides resolution in degrees";
            // 
            // txbSnapToAxisResolutionInDegrees
            // 
            this.txbSnapToAxisResolutionInDegrees.Location = new System.Drawing.Point(192, 52);
            this.txbSnapToAxisResolutionInDegrees.Name = "txbSnapToAxisResolutionInDegrees";
            this.txbSnapToAxisResolutionInDegrees.Size = new System.Drawing.Size(38, 20);
            this.txbSnapToAxisResolutionInDegrees.TabIndex = 1;
            this.txbSnapToAxisResolutionInDegrees.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ckbSnapToAxis
            // 
            this.ckbSnapToAxis.AutoSize = true;
            this.ckbSnapToAxis.Location = new System.Drawing.Point(11, 24);
            this.ckbSnapToAxis.Name = "ckbSnapToAxis";
            this.ckbSnapToAxis.Size = new System.Drawing.Size(97, 17);
            this.ckbSnapToAxis.TabIndex = 0;
            this.ckbSnapToAxis.Text = "Snap to guides";
            this.ckbSnapToAxis.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label16);
            this.groupBox11.Controls.Add(this.txbGraphicsPrecision);
            this.groupBox11.Location = new System.Drawing.Point(17, 19);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(241, 81);
            this.groupBox11.TabIndex = 15;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Precisions";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(8, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(114, 24);
            this.label16.TabIndex = 21;
            this.label16.Text = "Graphics precision";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txbGraphicsPrecision
            // 
            this.txbGraphicsPrecision.Location = new System.Drawing.Point(125, 29);
            this.txbGraphicsPrecision.Name = "txbGraphicsPrecision";
            this.txbGraphicsPrecision.Size = new System.Drawing.Size(53, 20);
            this.txbGraphicsPrecision.TabIndex = 20;
            this.txbGraphicsPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbpOther
            // 
            this.tbpOther.Controls.Add(this.grpSeamsCuts);
            this.tbpOther.Controls.Add(this.groupBox9);
            this.tbpOther.Location = new System.Drawing.Point(4, 22);
            this.tbpOther.Name = "tbpOther";
            this.tbpOther.Size = new System.Drawing.Size(737, 577);
            this.tbpOther.TabIndex = 2;
            this.tbpOther.Text = "Other";
            this.tbpOther.UseVisualStyleBackColor = true;
            // 
            // grpSeamsCuts
            // 
            this.grpSeamsCuts.Controls.Add(this.nmCutIndexCircleRadius);
            this.grpSeamsCuts.Controls.Add(this.label31);
            this.grpSeamsCuts.Controls.Add(this.ckbShowCutIndexCircles);
            this.grpSeamsCuts.Controls.Add(this.nmUnderageNumbersFontsize);
            this.grpSeamsCuts.Controls.Add(this.nmOverageNumbersFontsize);
            this.grpSeamsCuts.Controls.Add(this.nmCutNumbersFontsize);
            this.grpSeamsCuts.Controls.Add(this.nmAreaNumbersFontsize);
            this.grpSeamsCuts.Controls.Add(this.label21);
            this.grpSeamsCuts.Controls.Add(this.label20);
            this.grpSeamsCuts.Controls.Add(this.label19);
            this.grpSeamsCuts.Controls.Add(this.label18);
            this.grpSeamsCuts.Location = new System.Drawing.Point(46, 249);
            this.grpSeamsCuts.Margin = new System.Windows.Forms.Padding(2);
            this.grpSeamsCuts.Name = "grpSeamsCuts";
            this.grpSeamsCuts.Padding = new System.Windows.Forms.Padding(2);
            this.grpSeamsCuts.Size = new System.Drawing.Size(366, 200);
            this.grpSeamsCuts.TabIndex = 1;
            this.grpSeamsCuts.TabStop = false;
            this.grpSeamsCuts.Text = "Seams & Cuts Settings";
            // 
            // nmCutIndexCircleRadius
            // 
            this.nmCutIndexCircleRadius.Location = new System.Drawing.Point(208, 81);
            this.nmCutIndexCircleRadius.Margin = new System.Windows.Forms.Padding(2);
            this.nmCutIndexCircleRadius.Name = "nmCutIndexCircleRadius";
            this.nmCutIndexCircleRadius.Size = new System.Drawing.Size(58, 20);
            this.nmCutIndexCircleRadius.TabIndex = 11;
            this.nmCutIndexCircleRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.Location = new System.Drawing.Point(46, 76);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(149, 30);
            this.label31.TabIndex = 10;
            this.label31.Text = "Cut Circle Radius (1/100 inch)";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ckbShowCutIndexCircles
            // 
            this.ckbShowCutIndexCircles.AutoSize = true;
            this.ckbShowCutIndexCircles.Location = new System.Drawing.Point(67, 175);
            this.ckbShowCutIndexCircles.Name = "ckbShowCutIndexCircles";
            this.ckbShowCutIndexCircles.Size = new System.Drawing.Size(106, 17);
            this.ckbShowCutIndexCircles.TabIndex = 9;
            this.ckbShowCutIndexCircles.Text = "Show Cut Circles";
            this.ckbShowCutIndexCircles.UseVisualStyleBackColor = true;
            // 
            // nmUnderageNumbersFontsize
            // 
            this.nmUnderageNumbersFontsize.Location = new System.Drawing.Point(208, 141);
            this.nmUnderageNumbersFontsize.Margin = new System.Windows.Forms.Padding(2);
            this.nmUnderageNumbersFontsize.Name = "nmUnderageNumbersFontsize";
            this.nmUnderageNumbersFontsize.Size = new System.Drawing.Size(58, 20);
            this.nmUnderageNumbersFontsize.TabIndex = 8;
            this.nmUnderageNumbersFontsize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nmOverageNumbersFontsize
            // 
            this.nmOverageNumbersFontsize.Location = new System.Drawing.Point(208, 111);
            this.nmOverageNumbersFontsize.Margin = new System.Windows.Forms.Padding(2);
            this.nmOverageNumbersFontsize.Name = "nmOverageNumbersFontsize";
            this.nmOverageNumbersFontsize.Size = new System.Drawing.Size(58, 20);
            this.nmOverageNumbersFontsize.TabIndex = 7;
            this.nmOverageNumbersFontsize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nmCutNumbersFontsize
            // 
            this.nmCutNumbersFontsize.Location = new System.Drawing.Point(208, 51);
            this.nmCutNumbersFontsize.Margin = new System.Windows.Forms.Padding(2);
            this.nmCutNumbersFontsize.Name = "nmCutNumbersFontsize";
            this.nmCutNumbersFontsize.Size = new System.Drawing.Size(58, 20);
            this.nmCutNumbersFontsize.TabIndex = 6;
            this.nmCutNumbersFontsize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nmAreaNumbersFontsize
            // 
            this.nmAreaNumbersFontsize.Location = new System.Drawing.Point(208, 21);
            this.nmAreaNumbersFontsize.Margin = new System.Windows.Forms.Padding(2);
            this.nmAreaNumbersFontsize.Name = "nmAreaNumbersFontsize";
            this.nmAreaNumbersFontsize.Size = new System.Drawing.Size(58, 20);
            this.nmAreaNumbersFontsize.TabIndex = 5;
            this.nmAreaNumbersFontsize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.Location = new System.Drawing.Point(13, 136);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(182, 34);
            this.label21.TabIndex = 4;
            this.label21.Text = "Underage Numbers Font Size (pts)";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.Location = new System.Drawing.Point(30, 106);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(165, 30);
            this.label20.TabIndex = 3;
            this.label20.Text = "Overage Numbers Font Size (pts)";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.Location = new System.Drawing.Point(46, 46);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(149, 30);
            this.label19.TabIndex = 2;
            this.label19.Text = "Cut Numbers Font Size (pts)";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.Location = new System.Drawing.Point(46, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(149, 30);
            this.label18.TabIndex = 1;
            this.label18.Text = "Area Numbers Font Size (pts)";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.tableLayoutPanel1);
            this.groupBox9.Location = new System.Drawing.Point(46, 36);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(366, 181);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Counter Settings";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbSmallCircleRadius, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbSmallFontSizeInPts, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbMediumCircleRadius, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbMediumFontSizeInPts, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbLargeCircleRadius, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txbLargeFontSizeInPts, 2, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(323, 118);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(3, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 30);
            this.label7.TabIndex = 0;
            this.label7.Text = "Small";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(3, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 30);
            this.label9.TabIndex = 2;
            this.label9.Text = "Large";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(3, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 30);
            this.label8.TabIndex = 1;
            this.label8.Text = "Medium";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Location = new System.Drawing.Point(67, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 30);
            this.label10.TabIndex = 3;
            this.label10.Text = "Circle Radius";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.Location = new System.Drawing.Point(195, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 30);
            this.label11.TabIndex = 4;
            this.label11.Text = "Font Size In Pts";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbSmallCircleRadius
            // 
            this.txbSmallCircleRadius.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txbSmallCircleRadius.Location = new System.Drawing.Point(104, 35);
            this.txbSmallCircleRadius.Name = "txbSmallCircleRadius";
            this.txbSmallCircleRadius.Size = new System.Drawing.Size(48, 20);
            this.txbSmallCircleRadius.TabIndex = 5;
            this.txbSmallCircleRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbSmallFontSizeInPts
            // 
            this.txbSmallFontSizeInPts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txbSmallFontSizeInPts.Location = new System.Drawing.Point(237, 35);
            this.txbSmallFontSizeInPts.Name = "txbSmallFontSizeInPts";
            this.txbSmallFontSizeInPts.Size = new System.Drawing.Size(40, 20);
            this.txbSmallFontSizeInPts.TabIndex = 6;
            this.txbSmallFontSizeInPts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbMediumCircleRadius
            // 
            this.txbMediumCircleRadius.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txbMediumCircleRadius.Location = new System.Drawing.Point(104, 65);
            this.txbMediumCircleRadius.Name = "txbMediumCircleRadius";
            this.txbMediumCircleRadius.Size = new System.Drawing.Size(48, 20);
            this.txbMediumCircleRadius.TabIndex = 7;
            this.txbMediumCircleRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbMediumFontSizeInPts
            // 
            this.txbMediumFontSizeInPts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txbMediumFontSizeInPts.Location = new System.Drawing.Point(237, 65);
            this.txbMediumFontSizeInPts.Name = "txbMediumFontSizeInPts";
            this.txbMediumFontSizeInPts.Size = new System.Drawing.Size(40, 20);
            this.txbMediumFontSizeInPts.TabIndex = 8;
            this.txbMediumFontSizeInPts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbLargeCircleRadius
            // 
            this.txbLargeCircleRadius.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txbLargeCircleRadius.Location = new System.Drawing.Point(104, 95);
            this.txbLargeCircleRadius.Name = "txbLargeCircleRadius";
            this.txbLargeCircleRadius.Size = new System.Drawing.Size(48, 20);
            this.txbLargeCircleRadius.TabIndex = 9;
            this.txbLargeCircleRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbLargeFontSizeInPts
            // 
            this.txbLargeFontSizeInPts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txbLargeFontSizeInPts.Location = new System.Drawing.Point(237, 95);
            this.txbLargeFontSizeInPts.Name = "txbLargeFontSizeInPts";
            this.txbLargeFontSizeInPts.Size = new System.Drawing.Size(40, 20);
            this.txbLargeFontSizeInPts.TabIndex = 10;
            this.txbLargeFontSizeInPts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbpDebug
            // 
            this.tbpDebug.Controls.Add(this.ckbUpdateDebugFormDynamically);
            this.tbpDebug.Controls.Add(this.groupBox15);
            this.tbpDebug.Controls.Add(this.ckbValidateRolloutAndCutWidths);
            this.tbpDebug.Location = new System.Drawing.Point(4, 22);
            this.tbpDebug.Name = "tbpDebug";
            this.tbpDebug.Size = new System.Drawing.Size(737, 577);
            this.tbpDebug.TabIndex = 4;
            this.tbpDebug.Text = "Debug";
            this.tbpDebug.UseVisualStyleBackColor = true;
            // 
            // ckbUpdateDebugFormDynamically
            // 
            this.ckbUpdateDebugFormDynamically.AutoSize = true;
            this.ckbUpdateDebugFormDynamically.Location = new System.Drawing.Point(49, 65);
            this.ckbUpdateDebugFormDynamically.Name = "ckbUpdateDebugFormDynamically";
            this.ckbUpdateDebugFormDynamically.Size = new System.Drawing.Size(181, 17);
            this.ckbUpdateDebugFormDynamically.TabIndex = 19;
            this.ckbUpdateDebugFormDynamically.Text = "Update Debug Form Dynamically";
            this.ckbUpdateDebugFormDynamically.UseVisualStyleBackColor = true;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.ckbTraceLevelException);
            this.groupBox15.Controls.Add(this.ckbTraceLevelMethodCall);
            this.groupBox15.Controls.Add(this.ckbTraceLevelError);
            this.groupBox15.Controls.Add(this.ckbTraceLevelInfo);
            this.groupBox15.Location = new System.Drawing.Point(42, 99);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(170, 161);
            this.groupBox15.TabIndex = 18;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Trace Level";
            // 
            // ckbTraceLevelException
            // 
            this.ckbTraceLevelException.AutoSize = true;
            this.ckbTraceLevelException.Checked = true;
            this.ckbTraceLevelException.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTraceLevelException.Location = new System.Drawing.Point(18, 90);
            this.ckbTraceLevelException.Name = "ckbTraceLevelException";
            this.ckbTraceLevelException.Size = new System.Drawing.Size(133, 17);
            this.ckbTraceLevelException.TabIndex = 3;
            this.ckbTraceLevelException.Text = "Trace Exception Level";
            this.ckbTraceLevelException.UseVisualStyleBackColor = true;
            // 
            // ckbTraceLevelMethodCall
            // 
            this.ckbTraceLevelMethodCall.AutoSize = true;
            this.ckbTraceLevelMethodCall.Checked = true;
            this.ckbTraceLevelMethodCall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTraceLevelMethodCall.Location = new System.Drawing.Point(18, 121);
            this.ckbTraceLevelMethodCall.Name = "ckbTraceLevelMethodCall";
            this.ckbTraceLevelMethodCall.Size = new System.Drawing.Size(142, 17);
            this.ckbTraceLevelMethodCall.TabIndex = 2;
            this.ckbTraceLevelMethodCall.Text = "Trace Method Call Level";
            this.ckbTraceLevelMethodCall.UseVisualStyleBackColor = true;
            // 
            // ckbTraceLevelError
            // 
            this.ckbTraceLevelError.AutoSize = true;
            this.ckbTraceLevelError.Checked = true;
            this.ckbTraceLevelError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTraceLevelError.Location = new System.Drawing.Point(18, 59);
            this.ckbTraceLevelError.Name = "ckbTraceLevelError";
            this.ckbTraceLevelError.Size = new System.Drawing.Size(108, 17);
            this.ckbTraceLevelError.TabIndex = 1;
            this.ckbTraceLevelError.Text = "Trace Error Level";
            this.ckbTraceLevelError.UseVisualStyleBackColor = true;
            // 
            // ckbTraceLevelInfo
            // 
            this.ckbTraceLevelInfo.AutoSize = true;
            this.ckbTraceLevelInfo.Checked = true;
            this.ckbTraceLevelInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTraceLevelInfo.Location = new System.Drawing.Point(18, 28);
            this.ckbTraceLevelInfo.Name = "ckbTraceLevelInfo";
            this.ckbTraceLevelInfo.Size = new System.Drawing.Size(104, 17);
            this.ckbTraceLevelInfo.TabIndex = 0;
            this.ckbTraceLevelInfo.Text = "Trace Info Level";
            this.ckbTraceLevelInfo.UseVisualStyleBackColor = true;
            // 
            // ckbValidateRolloutAndCutWidths
            // 
            this.ckbValidateRolloutAndCutWidths.AutoSize = true;
            this.ckbValidateRolloutAndCutWidths.Location = new System.Drawing.Point(49, 36);
            this.ckbValidateRolloutAndCutWidths.Name = "ckbValidateRolloutAndCutWidths";
            this.ckbValidateRolloutAndCutWidths.Size = new System.Drawing.Size(170, 17);
            this.ckbValidateRolloutAndCutWidths.TabIndex = 0;
            this.ckbValidateRolloutAndCutWidths.Text = "Validate roll-out and cut widths";
            this.ckbValidateRolloutAndCutWidths.UseVisualStyleBackColor = true;
            // 
            // grbAreaOutlines
            // 
            this.grbAreaOutlines.Controls.Add(this.CkbShowAreaOutlinesInLineMode);
            this.grbAreaOutlines.Location = new System.Drawing.Point(498, 477);
            this.grbAreaOutlines.Name = "grbAreaOutlines";
            this.grbAreaOutlines.Size = new System.Drawing.Size(200, 58);
            this.grbAreaOutlines.TabIndex = 24;
            this.grbAreaOutlines.TabStop = false;
            this.grbAreaOutlines.Text = "Area Outlines";
            // 
            // CkbShowAreaOutlinesInLineMode
            // 
            this.CkbShowAreaOutlinesInLineMode.AutoSize = true;
            this.CkbShowAreaOutlinesInLineMode.Location = new System.Drawing.Point(18, 26);
            this.CkbShowAreaOutlinesInLineMode.Name = "CkbShowAreaOutlinesInLineMode";
            this.CkbShowAreaOutlinesInLineMode.Size = new System.Drawing.Size(178, 17);
            this.CkbShowAreaOutlinesInLineMode.TabIndex = 0;
            this.CkbShowAreaOutlinesInLineMode.Text = "Show Area Outline in Line Mode";
            this.CkbShowAreaOutlinesInLineMode.UseVisualStyleBackColor = true;
            this.CkbShowAreaOutlinesInLineMode.CheckedChanged += new System.EventHandler(this.CkbShowAreaOutlinesInLineMode_CheckedChanged);
            // 
            // GlobalSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 689);
            this.ControlBox = false;
            this.Controls.Add(this.tbcSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GlobalSettingsForm";
            this.Text = "Settings";
            this.tbcSettings.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.grbSeamAreaLocks.ResumeLayout(false);
            this.grbSeamAreaLocks.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudZoomInOutButtonPercent)).EndInit();
            this.grbRolledProductDefaults.ResumeLayout(false);
            this.grbRolledProductDefaults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudExtraPerCutInches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudDefaultOverlapInches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudDefaultRollWidthInches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudDefaultRollWidthFeet)).EndInit();
            this.groupBox18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudArrowMoveIncrement)).EndInit();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudMouseWheelZoomInterval)).EndInit();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grbLineDrawoutMode.ResumeLayout(false);
            this.grbLineDrawoutMode.PerformLayout();
            this.grbOperatingMode.ResumeLayout(false);
            this.grbOperatingMode.PerformLayout();
            this.tbpAdministrative.ResumeLayout(false);
            this.tbpAdministrative.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.grbAutosave.ResumeLayout(false);
            this.grbAutosave.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.tbpOther.ResumeLayout(false);
            this.grpSeamsCuts.ResumeLayout(false);
            this.grpSeamsCuts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmCutIndexCircleRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUnderageNumbersFontsize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmOverageNumbersFontsize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmCutNumbersFontsize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAreaNumbersFontsize)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tbpDebug.ResumeLayout(false);
            this.tbpDebug.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.grbAreaOutlines.ResumeLayout(false);
            this.grbAreaOutlines.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tbcSettings;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.GroupBox grbLineDrawoutMode;
        public System.Windows.Forms.RadioButton rbnHideLineDrawout;
        public System.Windows.Forms.RadioButton rbnShowLineDrawout;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grbOperatingMode;
        public System.Windows.Forms.RadioButton rbnLineModeInitialDesignState;
        public System.Windows.Forms.RadioButton rbnAreaModeInitialDesignState;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbMarkerWidth;
        public System.Windows.Forms.CheckBox ckbShowGuides;
        public System.Windows.Forms.CheckBox ckbShowMarker;
        public System.Windows.Forms.CheckBox ckbKeepAllGuidesOnCanvas;
        public System.Windows.Forms.CheckBox ckbKeepInitialGuideOnCanvas;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.CheckBox ckbShowRulers;
        public System.Windows.Forms.CheckBox ckbShowGrid;
        public System.Windows.Forms.CheckBox ckbShowPanAndZoom;
        private System.Windows.Forms.GroupBox groupBox8;
        public System.Windows.Forms.CheckBox ckbAutoUpdateSeamsAndCuts;
        public System.Windows.Forms.CheckBox ckbLockScrollWhenDrawingSmallerThanCanvas;
        private System.Windows.Forms.TabPage tbpOther;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txbSmallCircleRadius;
        public System.Windows.Forms.TextBox txbSmallFontSizeInPts;
        public System.Windows.Forms.TextBox txbMediumCircleRadius;
        public System.Windows.Forms.TextBox txbMediumFontSizeInPts;
        public System.Windows.Forms.TextBox txbLargeCircleRadius;
        public System.Windows.Forms.TextBox txbLargeFontSizeInPts;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txbDefaultDrawingScaleInInches;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox txbDefaultHeight;
        public System.Windows.Forms.TextBox txbDefaultWidth;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.CheckBox ckbStartupFullScreenMode;
        private System.Windows.Forms.GroupBox groupBox12;
        public System.Windows.Forms.CheckBox ckbShowSetScaleReminder;
        private System.Windows.Forms.GroupBox groupBox13;
        public System.Windows.Forms.CheckBox ckbKeystrokes;
        private System.Windows.Forms.TabPage tbpAdministrative;
        private System.Windows.Forms.GroupBox groupBox7;
        public System.Windows.Forms.TextBox txbMinUnderLengthInInches;
        public System.Windows.Forms.TextBox txbMinUnderWidthInInches;
        public System.Windows.Forms.TextBox txbMinOverLengthInInches;
        public System.Windows.Forms.TextBox txbMinOverWidthInInches;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.CheckBox ckbShowSeamEditFormAsModal;
        public System.Windows.Forms.CheckBox ckbShowLineEditFormAsModal;
        public System.Windows.Forms.CheckBox ckbShowAreaEditFormAsModal;
        private System.Windows.Forms.GroupBox groupBox10;
        public System.Windows.Forms.CheckBox ckbShowLineFilterFormAsModal;
        public System.Windows.Forms.CheckBox ckbShowAreaFilterFormAsModal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox txbSnapDistance;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txbSnapToAxisResolutionInDegrees;
        public System.Windows.Forms.CheckBox ckbSnapToAxis;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox txbGraphicsPrecision;
        public System.Windows.Forms.CheckBox ckbAllowEditingOfShortcutKeys;
        public System.Windows.Forms.CheckBox ckbAllowEditingOfToolTips;
        private System.Windows.Forms.GroupBox groupBox14;
        public System.Windows.Forms.NumericUpDown nudMouseWheelZoomInterval;
        private System.Windows.Forms.TabPage tbpDebug;
        private System.Windows.Forms.GroupBox groupBox17;
        public System.Windows.Forms.RadioButton rbnRightHanded;
        public System.Windows.Forms.RadioButton rbnLeftHanded;
        public System.Windows.Forms.CheckBox ckbValidateRolloutAndCutWidths;
        private System.Windows.Forms.GroupBox grpSeamsCuts;
        public System.Windows.Forms.NumericUpDown nmUnderageNumbersFontsize;
        public System.Windows.Forms.NumericUpDown nmOverageNumbersFontsize;
        public System.Windows.Forms.NumericUpDown nmCutNumbersFontsize;
        public System.Windows.Forms.NumericUpDown nmAreaNumbersFontsize;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox grbAutosave;
        public System.Windows.Forms.CheckBox ckbAutosaveEnabled;
        public System.Windows.Forms.TextBox txbAutosaveIntervalInSeconds;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBox15;
        public System.Windows.Forms.CheckBox ckbTraceLevelError;
        public System.Windows.Forms.CheckBox ckbTraceLevelInfo;
        public System.Windows.Forms.CheckBox ckbTraceLevelMethodCall;
        public System.Windows.Forms.CheckBox ckbTraceLevelException;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.TextBox txbGridSpacingInInches;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.TextBox txbGridOffsetInInches;
        private System.Windows.Forms.GroupBox groupBox18;
        public System.Windows.Forms.NumericUpDown nudArrowMoveIncrement;
        public System.Windows.Forms.CheckBox ckbUpdateDebugFormDynamically;
        public System.Windows.Forms.CheckBox ckbShowCutIndexCircles;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.Label label30;
        public System.Windows.Forms.TextBox txbMinUnderLengthForCombosInInches;
        public System.Windows.Forms.TextBox txbMinUnderWidthForCombosInInches;
        public System.Windows.Forms.TextBox txbMinOverLengthForCombosInInches;
        public System.Windows.Forms.TextBox txbMinOverWidthForCombosInInches;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        public System.Windows.Forms.NumericUpDown nmCutIndexCircleRadius;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.GroupBox grbRolledProductDefaults;
        private System.Windows.Forms.Label label37;
        public System.Windows.Forms.NumericUpDown NudExtraPerCutInches;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label35;
        public System.Windows.Forms.NumericUpDown NudDefaultOverlapInches;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label34;
        public System.Windows.Forms.NumericUpDown NudDefaultRollWidthInches;
        private System.Windows.Forms.Label label33;
        public System.Windows.Forms.NumericUpDown NudDefaultRollWidthFeet;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.GroupBox groupBox20;
        public System.Windows.Forms.NumericUpDown nudZoomInOutButtonPercent;
        public System.Windows.Forms.CheckBox ckbValidateOnProjectSave;
        private System.Windows.Forms.GroupBox grbSeamAreaLocks;
        public System.Windows.Forms.CheckBox ckbLockAreaWhen100PctSeamed;
        private System.Windows.Forms.GroupBox grbAreaOutlines;
        public System.Windows.Forms.CheckBox CkbShowAreaOutlinesInLineMode;
    }
}