//-------------------------------------------------------------------------------//
// <copyright file="GlobalSettingsForm.cs" company="Bruun Estimating, LLC">      // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ckbShowSeamEditFormAsModal = new System.Windows.Forms.CheckBox();
            this.ckbShowLineEditFormAsModal = new System.Windows.Forms.CheckBox();
            this.ckbShowAreaEditFormAsModal = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ckbShowRulers = new System.Windows.Forms.CheckBox();
            this.ckbShowGrid = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ckbKeepAllGuidesOnCanvas = new System.Windows.Forms.CheckBox();
            this.ckbKeepInitialGuideOnCanvas = new System.Windows.Forms.CheckBox();
            this.ckbShowGuides = new System.Windows.Forms.CheckBox();
            this.ckbShowMarker = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbMarkerWidth = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txbSnapToAxisResolutionInDegrees = new System.Windows.Forms.TextBox();
            this.ckbSnapToAxis = new System.Windows.Forms.CheckBox();
            this.grbLineDrawoutMode = new System.Windows.Forms.GroupBox();
            this.rbnHideLineDrawout = new System.Windows.Forms.RadioButton();
            this.rbnShowLineDrawout = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grbOperatingMode = new System.Windows.Forms.GroupBox();
            this.rbnDevelopmentOperatingMode = new System.Windows.Forms.RadioButton();
            this.rbnNormalOperatingMode = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbpMaterialsReconcilliation = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txbMinOverWidthInInches = new System.Windows.Forms.TextBox();
            this.txbMinOverLengthInInches = new System.Windows.Forms.TextBox();
            this.txbMinUnderWidthInInches = new System.Windows.Forms.TextBox();
            this.txbMinUnderLengthInInches = new System.Windows.Forms.TextBox();
            this.tbcSettings.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grbLineDrawoutMode.SuspendLayout();
            this.grbOperatingMode.SuspendLayout();
            this.tbpMaterialsReconcilliation.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(138, 609);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(304, 609);
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
            this.tbcSettings.Controls.Add(this.tbpMaterialsReconcilliation);
            this.tbcSettings.Location = new System.Drawing.Point(17, 12);
            this.tbcSettings.Name = "tbcSettings";
            this.tbcSettings.SelectedIndex = 0;
            this.tbcSettings.Size = new System.Drawing.Size(515, 572);
            this.tbcSettings.TabIndex = 4;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.groupBox6);
            this.tbpGeneral.Controls.Add(this.groupBox5);
            this.tbpGeneral.Controls.Add(this.groupBox4);
            this.tbpGeneral.Controls.Add(this.groupBox1);
            this.tbpGeneral.Controls.Add(this.grbLineDrawoutMode);
            this.tbpGeneral.Controls.Add(this.grbOperatingMode);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(507, 546);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ckbShowSeamEditFormAsModal);
            this.groupBox6.Controls.Add(this.ckbShowLineEditFormAsModal);
            this.groupBox6.Controls.Add(this.ckbShowAreaEditFormAsModal);
            this.groupBox6.Location = new System.Drawing.Point(19, 378);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 116);
            this.groupBox6.TabIndex = 11;
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ckbShowRulers);
            this.groupBox5.Controls.Add(this.ckbShowGrid);
            this.groupBox5.Location = new System.Drawing.Point(256, 275);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 84);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Grids and Rulers";
            // 
            // ckbShowRulers
            // 
            this.ckbShowRulers.AutoSize = true;
            this.ckbShowRulers.Location = new System.Drawing.Point(16, 61);
            this.ckbShowRulers.Name = "ckbShowRulers";
            this.ckbShowRulers.Size = new System.Drawing.Size(86, 17);
            this.ckbShowRulers.TabIndex = 15;
            this.ckbShowRulers.Text = "Show Rulers";
            this.ckbShowRulers.UseVisualStyleBackColor = true;
            // 
            // ckbShowGrid
            // 
            this.ckbShowGrid.AutoSize = true;
            this.ckbShowGrid.Location = new System.Drawing.Point(16, 29);
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
            this.groupBox4.Location = new System.Drawing.Point(19, 143);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 216);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Marker and Guides";
            // 
            // ckbKeepAllGuidesOnCanvas
            // 
            this.ckbKeepAllGuidesOnCanvas.AutoSize = true;
            this.ckbKeepAllGuidesOnCanvas.Location = new System.Drawing.Point(10, 175);
            this.ckbKeepAllGuidesOnCanvas.Name = "ckbKeepAllGuidesOnCanvas";
            this.ckbKeepAllGuidesOnCanvas.Size = new System.Drawing.Size(155, 17);
            this.ckbKeepAllGuidesOnCanvas.TabIndex = 13;
            this.ckbKeepAllGuidesOnCanvas.Text = "Keep All Guides on Canvas";
            this.ckbKeepAllGuidesOnCanvas.UseVisualStyleBackColor = true;
            // 
            // ckbKeepInitialGuideOnCanvas
            // 
            this.ckbKeepInitialGuideOnCanvas.AutoSize = true;
            this.ckbKeepInitialGuideOnCanvas.Location = new System.Drawing.Point(10, 132);
            this.ckbKeepInitialGuideOnCanvas.Name = "ckbKeepInitialGuideOnCanvas";
            this.ckbKeepInitialGuideOnCanvas.Size = new System.Drawing.Size(163, 17);
            this.ckbKeepInitialGuideOnCanvas.TabIndex = 12;
            this.ckbKeepInitialGuideOnCanvas.Text = "Keep Initial Guide on Canvas";
            this.ckbKeepInitialGuideOnCanvas.UseVisualStyleBackColor = true;
            // 
            // ckbShowGuides
            // 
            this.ckbShowGuides.AutoSize = true;
            this.ckbShowGuides.Location = new System.Drawing.Point(10, 95);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txbSnapToAxisResolutionInDegrees);
            this.groupBox1.Controls.Add(this.ckbSnapToAxis);
            this.groupBox1.Location = new System.Drawing.Point(253, 143);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 126);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Snap to Axis";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Snap to axis resolution in degrees";
            // 
            // txbSnapToAxisResolutionInDegrees
            // 
            this.txbSnapToAxisResolutionInDegrees.Location = new System.Drawing.Point(73, 89);
            this.txbSnapToAxisResolutionInDegrees.Name = "txbSnapToAxisResolutionInDegrees";
            this.txbSnapToAxisResolutionInDegrees.Size = new System.Drawing.Size(53, 20);
            this.txbSnapToAxisResolutionInDegrees.TabIndex = 1;
            this.txbSnapToAxisResolutionInDegrees.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ckbSnapToAxis
            // 
            this.ckbSnapToAxis.AutoSize = true;
            this.ckbSnapToAxis.Location = new System.Drawing.Point(19, 31);
            this.ckbSnapToAxis.Name = "ckbSnapToAxis";
            this.ckbSnapToAxis.Size = new System.Drawing.Size(84, 17);
            this.ckbSnapToAxis.TabIndex = 0;
            this.ckbSnapToAxis.Text = "Snap to axis";
            this.ckbSnapToAxis.UseVisualStyleBackColor = true;
            // 
            // grbLineDrawoutMode
            // 
            this.grbLineDrawoutMode.Controls.Add(this.rbnHideLineDrawout);
            this.grbLineDrawoutMode.Controls.Add(this.rbnShowLineDrawout);
            this.grbLineDrawoutMode.Controls.Add(this.groupBox3);
            this.grbLineDrawoutMode.Location = new System.Drawing.Point(253, 20);
            this.grbLineDrawoutMode.Name = "grbLineDrawoutMode";
            this.grbLineDrawoutMode.Size = new System.Drawing.Size(200, 100);
            this.grbLineDrawoutMode.TabIndex = 4;
            this.grbLineDrawoutMode.TabStop = false;
            this.grbLineDrawoutMode.Text = "Line Drawout Mode";
            // 
            // rbnHideLineDrawout
            // 
            this.rbnHideLineDrawout.AutoSize = true;
            this.rbnHideLineDrawout.Location = new System.Drawing.Point(19, 66);
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
            this.rbnShowLineDrawout.Location = new System.Drawing.Point(19, 29);
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
            this.grbOperatingMode.Controls.Add(this.rbnDevelopmentOperatingMode);
            this.grbOperatingMode.Controls.Add(this.rbnNormalOperatingMode);
            this.grbOperatingMode.Controls.Add(this.groupBox2);
            this.grbOperatingMode.Location = new System.Drawing.Point(16, 20);
            this.grbOperatingMode.Name = "grbOperatingMode";
            this.grbOperatingMode.Size = new System.Drawing.Size(203, 100);
            this.grbOperatingMode.TabIndex = 3;
            this.grbOperatingMode.TabStop = false;
            this.grbOperatingMode.Text = "Operating Mode";
            // 
            // rbnDevelopmentOperatingMode
            // 
            this.rbnDevelopmentOperatingMode.AutoSize = true;
            this.rbnDevelopmentOperatingMode.Location = new System.Drawing.Point(22, 64);
            this.rbnDevelopmentOperatingMode.Name = "rbnDevelopmentOperatingMode";
            this.rbnDevelopmentOperatingMode.Size = new System.Drawing.Size(88, 17);
            this.rbnDevelopmentOperatingMode.TabIndex = 5;
            this.rbnDevelopmentOperatingMode.Text = "Development";
            this.rbnDevelopmentOperatingMode.UseVisualStyleBackColor = true;
            // 
            // rbnNormalOperatingMode
            // 
            this.rbnNormalOperatingMode.AutoSize = true;
            this.rbnNormalOperatingMode.Checked = true;
            this.rbnNormalOperatingMode.Location = new System.Drawing.Point(22, 28);
            this.rbnNormalOperatingMode.Name = "rbnNormalOperatingMode";
            this.rbnNormalOperatingMode.Size = new System.Drawing.Size(58, 17);
            this.rbnNormalOperatingMode.TabIndex = 4;
            this.rbnNormalOperatingMode.TabStop = true;
            this.rbnNormalOperatingMode.Text = "Normal";
            this.rbnNormalOperatingMode.UseVisualStyleBackColor = true;
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
            // tbpMaterialsReconcilliation
            // 
            this.tbpMaterialsReconcilliation.Controls.Add(this.groupBox7);
            this.tbpMaterialsReconcilliation.Location = new System.Drawing.Point(4, 22);
            this.tbpMaterialsReconcilliation.Name = "tbpMaterialsReconcilliation";
            this.tbpMaterialsReconcilliation.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMaterialsReconcilliation.Size = new System.Drawing.Size(507, 546);
            this.tbpMaterialsReconcilliation.TabIndex = 1;
            this.tbpMaterialsReconcilliation.Text = "Materials Reconcilliation";
            this.tbpMaterialsReconcilliation.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txbMinUnderLengthInInches);
            this.groupBox7.Controls.Add(this.txbMinUnderWidthInInches);
            this.groupBox7.Controls.Add(this.txbMinOverLengthInInches);
            this.groupBox7.Controls.Add(this.txbMinOverWidthInInches);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Location = new System.Drawing.Point(22, 24);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(267, 156);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Underages and Overages";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Minimum Overage Width in Inches";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Minimum Overage Length in Inches";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Minimum Underage Length in Inches";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Minimum Underage Width in Inches";
            // 
            // txbMinOverWidthInInches
            // 
            this.txbMinOverWidthInInches.Location = new System.Drawing.Point(207, 27);
            this.txbMinOverWidthInInches.Name = "txbMinOverWidthInInches";
            this.txbMinOverWidthInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinOverWidthInInches.TabIndex = 4;
            this.txbMinOverWidthInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbMinOverLengthInInches
            // 
            this.txbMinOverLengthInInches.Location = new System.Drawing.Point(207, 58);
            this.txbMinOverLengthInInches.Name = "txbMinOverLengthInInches";
            this.txbMinOverLengthInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinOverLengthInInches.TabIndex = 5;
            this.txbMinOverLengthInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbMinUnderWidthInInches
            // 
            this.txbMinUnderWidthInInches.Location = new System.Drawing.Point(207, 89);
            this.txbMinUnderWidthInInches.Name = "txbMinUnderWidthInInches";
            this.txbMinUnderWidthInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinUnderWidthInInches.TabIndex = 6;
            this.txbMinUnderWidthInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbMinUnderLengthInInches
            // 
            this.txbMinUnderLengthInInches.Location = new System.Drawing.Point(207, 120);
            this.txbMinUnderLengthInInches.Name = "txbMinUnderLengthInInches";
            this.txbMinUnderLengthInInches.Size = new System.Drawing.Size(40, 20);
            this.txbMinUnderLengthInInches.TabIndex = 7;
            this.txbMinUnderLengthInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // GlobalSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 680);
            this.ControlBox = false;
            this.Controls.Add(this.tbcSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "GlobalSettingsForm";
            this.Text = "Settings";
            this.tbcSettings.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbLineDrawoutMode.ResumeLayout(false);
            this.grbLineDrawoutMode.PerformLayout();
            this.grbOperatingMode.ResumeLayout(false);
            this.grbOperatingMode.PerformLayout();
            this.tbpMaterialsReconcilliation.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
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
        public System.Windows.Forms.RadioButton rbnDevelopmentOperatingMode;
        public System.Windows.Forms.RadioButton rbnNormalOperatingMode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabPage tbpMaterialsReconcilliation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox ckbSnapToAxis;
        public System.Windows.Forms.TextBox txbSnapToAxisResolutionInDegrees;
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
        private System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.CheckBox ckbShowSeamEditFormAsModal;
        public System.Windows.Forms.CheckBox ckbShowLineEditFormAsModal;
        public System.Windows.Forms.CheckBox ckbShowAreaEditFormAsModal;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txbMinUnderLengthInInches;
        public System.Windows.Forms.TextBox txbMinUnderWidthInInches;
        public System.Windows.Forms.TextBox txbMinOverLengthInInches;
        public System.Windows.Forms.TextBox txbMinOverWidthInInches;
    }
}