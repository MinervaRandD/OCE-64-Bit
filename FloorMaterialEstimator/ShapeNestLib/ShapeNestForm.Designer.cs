namespace ShapeNestLib
{
    partial class ShapeNestForm
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
            this.pnlShapes = new System.Windows.Forms.Panel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDoNest = new System.Windows.Forms.Button();
            this.txbRollWidth = new System.Windows.Forms.TextBox();
            this.grbNestControls = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbMaterialMarginInches = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbUseFullRollWidth = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txbMaxRollLength = new System.Windows.Forms.TextBox();
            this.nudNmbrOfRefreshes = new System.Windows.Forms.NumericUpDown();
            this.lblNumberOfRefreshes = new System.Windows.Forms.Label();
            this.cmbRotations = new System.Windows.Forms.ComboBox();
            this.lblMaterialSpacing = new System.Windows.Forms.Label();
            this.txbMaterialSpacingInInches = new System.Windows.Forms.TextBox();
            this.lblRotations = new System.Windows.Forms.Label();
            this.lblNbrIterations = new System.Windows.Forms.Label();
            this.nudIterations = new System.Windows.Forms.NumericUpDown();
            this.lblRollWidth = new System.Windows.Forms.Label();
            this.pnlShapePaletteControls = new System.Windows.Forms.Panel();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.lblPctOfRollUsedValue = new System.Windows.Forms.Label();
            this.lblPctOfRollUsed = new System.Windows.Forms.Label();
            this.lblTotalAreaOfPlacedItemsValue = new System.Windows.Forms.Label();
            this.lblTotalAreaOfPlacedItems = new System.Windows.Forms.Label();
            this.lblNumberOfItemsPlacedValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUsedRollAreaValue = new System.Windows.Forms.Label();
            this.lblUsedRollArea = new System.Windows.Forms.Label();
            this.lblUsedRollLgthValue = new System.Windows.Forms.Label();
            this.lblUsedRollWidth = new System.Windows.Forms.Label();
            this.pcbNesting = new System.Windows.Forms.PictureBox();
            this.pnlNestPanel = new System.Windows.Forms.Panel();
            this.prbProgress = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.grbNestControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNmbrOfRefreshes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIterations)).BeginInit();
            this.pnlShapePaletteControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbNesting)).BeginInit();
            this.pnlNestPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prbProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlShapes
            // 
            this.pnlShapes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlShapes.Location = new System.Drawing.Point(12, 13);
            this.pnlShapes.Name = "pnlShapes";
            this.pnlShapes.Size = new System.Drawing.Size(1000, 100);
            this.pnlShapes.TabIndex = 0;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(90, 12);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(146, 23);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnDoNest
            // 
            this.btnDoNest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoNest.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDoNest.Location = new System.Drawing.Point(876, 534);
            this.btnDoNest.Name = "btnDoNest";
            this.btnDoNest.Size = new System.Drawing.Size(80, 40);
            this.btnDoNest.TabIndex = 3;
            this.btnDoNest.Text = "Do Nest";
            this.btnDoNest.UseVisualStyleBackColor = true;
            this.btnDoNest.Click += new System.EventHandler(this.btnDoNest_Click);
            // 
            // txbRollWidth
            // 
            this.txbRollWidth.Location = new System.Drawing.Point(20, 56);
            this.txbRollWidth.Name = "txbRollWidth";
            this.txbRollWidth.Size = new System.Drawing.Size(76, 20);
            this.txbRollWidth.TabIndex = 4;
            this.txbRollWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grbNestControls
            // 
            this.grbNestControls.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grbNestControls.Controls.Add(this.label4);
            this.grbNestControls.Controls.Add(this.txbMaterialMarginInches);
            this.grbNestControls.Controls.Add(this.label2);
            this.grbNestControls.Controls.Add(this.ckbUseFullRollWidth);
            this.grbNestControls.Controls.Add(this.label1);
            this.grbNestControls.Controls.Add(this.txbMaxRollLength);
            this.grbNestControls.Controls.Add(this.nudNmbrOfRefreshes);
            this.grbNestControls.Controls.Add(this.lblNumberOfRefreshes);
            this.grbNestControls.Controls.Add(this.cmbRotations);
            this.grbNestControls.Controls.Add(this.lblMaterialSpacing);
            this.grbNestControls.Controls.Add(this.txbMaterialSpacingInInches);
            this.grbNestControls.Controls.Add(this.lblRotations);
            this.grbNestControls.Controls.Add(this.lblNbrIterations);
            this.grbNestControls.Controls.Add(this.nudIterations);
            this.grbNestControls.Controls.Add(this.lblRollWidth);
            this.grbNestControls.Controls.Add(this.txbRollWidth);
            this.grbNestControls.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grbNestControls.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grbNestControls.Location = new System.Drawing.Point(73, 505);
            this.grbNestControls.Name = "grbNestControls";
            this.grbNestControls.Size = new System.Drawing.Size(778, 100);
            this.grbNestControls.TabIndex = 5;
            this.grbNestControls.TabStop = false;
            this.grbNestControls.Text = "Nest Controls";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(329, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 26);
            this.label4.TabIndex = 20;
            this.label4.Text = "Material Margin\r\n(Inches)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbMaterialMarginInches
            // 
            this.txbMaterialMarginInches.Location = new System.Drawing.Point(337, 58);
            this.txbMaterialMarginInches.Name = "txbMaterialMarginInches";
            this.txbMaterialMarginInches.Size = new System.Drawing.Size(73, 20);
            this.txbMaterialMarginInches.TabIndex = 19;
            this.txbMaterialMarginInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(669, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 26);
            this.label2.TabIndex = 18;
            this.label2.Text = "Try Use Full\r\nRoll Width";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ckbUseFullRollWidth
            // 
            this.ckbUseFullRollWidth.AutoSize = true;
            this.ckbUseFullRollWidth.Checked = true;
            this.ckbUseFullRollWidth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbUseFullRollWidth.Location = new System.Drawing.Point(697, 58);
            this.ckbUseFullRollWidth.Name = "ckbUseFullRollWidth";
            this.ckbUseFullRollWidth.Size = new System.Drawing.Size(15, 14);
            this.ckbUseFullRollWidth.TabIndex = 17;
            this.ckbUseFullRollWidth.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Max Roll Length";
            // 
            // txbMaxRollLength
            // 
            this.txbMaxRollLength.Location = new System.Drawing.Point(128, 56);
            this.txbMaxRollLength.Name = "txbMaxRollLength";
            this.txbMaxRollLength.Size = new System.Drawing.Size(72, 20);
            this.txbMaxRollLength.TabIndex = 15;
            this.txbMaxRollLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudNmbrOfRefreshes
            // 
            this.nudNmbrOfRefreshes.Location = new System.Drawing.Point(517, 58);
            this.nudNmbrOfRefreshes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNmbrOfRefreshes.Name = "nudNmbrOfRefreshes";
            this.nudNmbrOfRefreshes.Size = new System.Drawing.Size(58, 20);
            this.nudNmbrOfRefreshes.TabIndex = 14;
            this.nudNmbrOfRefreshes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudNmbrOfRefreshes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblNumberOfRefreshes
            // 
            this.lblNumberOfRefreshes.AutoSize = true;
            this.lblNumberOfRefreshes.Location = new System.Drawing.Point(512, 22);
            this.lblNumberOfRefreshes.Name = "lblNumberOfRefreshes";
            this.lblNumberOfRefreshes.Size = new System.Drawing.Size(56, 26);
            this.lblNumberOfRefreshes.TabIndex = 13;
            this.lblNumberOfRefreshes.Text = "Number of\r\nRefreshes";
            this.lblNumberOfRefreshes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbRotations
            // 
            this.cmbRotations.FormattingEnabled = true;
            this.cmbRotations.Location = new System.Drawing.Point(442, 56);
            this.cmbRotations.Name = "cmbRotations";
            this.cmbRotations.Size = new System.Drawing.Size(43, 21);
            this.cmbRotations.TabIndex = 12;
            // 
            // lblMaterialSpacing
            // 
            this.lblMaterialSpacing.AutoSize = true;
            this.lblMaterialSpacing.Location = new System.Drawing.Point(223, 22);
            this.lblMaterialSpacing.Name = "lblMaterialSpacing";
            this.lblMaterialSpacing.Size = new System.Drawing.Size(86, 26);
            this.lblMaterialSpacing.TabIndex = 11;
            this.lblMaterialSpacing.Text = "Material Spacing\r\n(Inches)";
            this.lblMaterialSpacing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbMaterialSpacingInInches
            // 
            this.txbMaterialSpacingInInches.Location = new System.Drawing.Point(232, 57);
            this.txbMaterialSpacingInInches.Name = "txbMaterialSpacingInInches";
            this.txbMaterialSpacingInInches.Size = new System.Drawing.Size(73, 20);
            this.txbMaterialSpacingInInches.TabIndex = 10;
            this.txbMaterialSpacingInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblRotations
            // 
            this.lblRotations.AutoSize = true;
            this.lblRotations.Location = new System.Drawing.Point(434, 22);
            this.lblRotations.Name = "lblRotations";
            this.lblRotations.Size = new System.Drawing.Size(53, 26);
            this.lblRotations.TabIndex = 9;
            this.lblRotations.Text = "Rotations\r\n(Degrees)";
            // 
            // lblNbrIterations
            // 
            this.lblNbrIterations.AutoSize = true;
            this.lblNbrIterations.Location = new System.Drawing.Point(594, 22);
            this.lblNbrIterations.Name = "lblNbrIterations";
            this.lblNbrIterations.Size = new System.Drawing.Size(69, 26);
            this.lblNbrIterations.TabIndex = 7;
            this.lblNbrIterations.Text = "Iterations Per\r\nRefresh";
            this.lblNbrIterations.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudIterations
            // 
            this.nudIterations.Location = new System.Drawing.Point(607, 56);
            this.nudIterations.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudIterations.Name = "nudIterations";
            this.nudIterations.Size = new System.Drawing.Size(58, 20);
            this.nudIterations.TabIndex = 6;
            this.nudIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudIterations.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblRollWidth
            // 
            this.lblRollWidth.AutoSize = true;
            this.lblRollWidth.Location = new System.Drawing.Point(25, 29);
            this.lblRollWidth.Name = "lblRollWidth";
            this.lblRollWidth.Size = new System.Drawing.Size(56, 13);
            this.lblRollWidth.TabIndex = 5;
            this.lblRollWidth.Text = "Roll Width";
            // 
            // pnlShapePaletteControls
            // 
            this.pnlShapePaletteControls.Controls.Add(this.btnSelectNone);
            this.pnlShapePaletteControls.Controls.Add(this.btnSelectAll);
            this.pnlShapePaletteControls.Location = new System.Drawing.Point(116, 129);
            this.pnlShapePaletteControls.Name = "pnlShapePaletteControls";
            this.pnlShapePaletteControls.Size = new System.Drawing.Size(735, 45);
            this.pnlShapePaletteControls.TabIndex = 6;
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(463, 12);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(146, 23);
            this.btnSelectNone.TabIndex = 3;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // lblPctOfRollUsedValue
            // 
            this.lblPctOfRollUsedValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPctOfRollUsedValue.Location = new System.Drawing.Point(852, 51);
            this.lblPctOfRollUsedValue.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblPctOfRollUsedValue.Name = "lblPctOfRollUsedValue";
            this.lblPctOfRollUsedValue.Size = new System.Drawing.Size(54, 16);
            this.lblPctOfRollUsedValue.TabIndex = 25;
            this.lblPctOfRollUsedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPctOfRollUsed
            // 
            this.lblPctOfRollUsed.AutoSize = true;
            this.lblPctOfRollUsed.Location = new System.Drawing.Point(756, 51);
            this.lblPctOfRollUsed.MinimumSize = new System.Drawing.Size(64, 16);
            this.lblPctOfRollUsed.Name = "lblPctOfRollUsed";
            this.lblPctOfRollUsed.Size = new System.Drawing.Size(86, 16);
            this.lblPctOfRollUsed.TabIndex = 24;
            this.lblPctOfRollUsed.Text = "Pct Of Roll Used";
            // 
            // lblTotalAreaOfPlacedItemsValue
            // 
            this.lblTotalAreaOfPlacedItemsValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalAreaOfPlacedItemsValue.Location = new System.Drawing.Point(662, 51);
            this.lblTotalAreaOfPlacedItemsValue.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblTotalAreaOfPlacedItemsValue.Name = "lblTotalAreaOfPlacedItemsValue";
            this.lblTotalAreaOfPlacedItemsValue.Size = new System.Drawing.Size(84, 16);
            this.lblTotalAreaOfPlacedItemsValue.TabIndex = 23;
            this.lblTotalAreaOfPlacedItemsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalAreaOfPlacedItems
            // 
            this.lblTotalAreaOfPlacedItems.AutoSize = true;
            this.lblTotalAreaOfPlacedItems.Location = new System.Drawing.Point(545, 51);
            this.lblTotalAreaOfPlacedItems.MinimumSize = new System.Drawing.Size(64, 16);
            this.lblTotalAreaOfPlacedItems.Name = "lblTotalAreaOfPlacedItems";
            this.lblTotalAreaOfPlacedItems.Size = new System.Drawing.Size(107, 16);
            this.lblTotalAreaOfPlacedItems.TabIndex = 22;
            this.lblTotalAreaOfPlacedItems.Text = "Area Of Placed Items";
            // 
            // lblNumberOfItemsPlacedValue
            // 
            this.lblNumberOfItemsPlacedValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNumberOfItemsPlacedValue.Location = new System.Drawing.Point(487, 51);
            this.lblNumberOfItemsPlacedValue.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblNumberOfItemsPlacedValue.Name = "lblNumberOfItemsPlacedValue";
            this.lblNumberOfItemsPlacedValue.Size = new System.Drawing.Size(48, 16);
            this.lblNumberOfItemsPlacedValue.TabIndex = 21;
            this.lblNumberOfItemsPlacedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(409, 51);
            this.label3.MinimumSize = new System.Drawing.Size(64, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Items Placed";
            // 
            // lblUsedRollAreaValue
            // 
            this.lblUsedRollAreaValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUsedRollAreaValue.Location = new System.Drawing.Point(315, 51);
            this.lblUsedRollAreaValue.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUsedRollAreaValue.Name = "lblUsedRollAreaValue";
            this.lblUsedRollAreaValue.Size = new System.Drawing.Size(84, 16);
            this.lblUsedRollAreaValue.TabIndex = 19;
            this.lblUsedRollAreaValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsedRollArea
            // 
            this.lblUsedRollArea.AutoSize = true;
            this.lblUsedRollArea.Location = new System.Drawing.Point(227, 51);
            this.lblUsedRollArea.MinimumSize = new System.Drawing.Size(64, 16);
            this.lblUsedRollArea.Name = "lblUsedRollArea";
            this.lblUsedRollArea.Size = new System.Drawing.Size(78, 16);
            this.lblUsedRollArea.TabIndex = 18;
            this.lblUsedRollArea.Text = "Used Roll Area";
            // 
            // lblUsedRollLgthValue
            // 
            this.lblUsedRollLgthValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUsedRollLgthValue.Location = new System.Drawing.Point(153, 51);
            this.lblUsedRollLgthValue.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUsedRollLgthValue.Name = "lblUsedRollLgthValue";
            this.lblUsedRollLgthValue.Size = new System.Drawing.Size(64, 16);
            this.lblUsedRollLgthValue.TabIndex = 17;
            this.lblUsedRollLgthValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsedRollWidth
            // 
            this.lblUsedRollWidth.AutoSize = true;
            this.lblUsedRollWidth.Location = new System.Drawing.Point(59, 51);
            this.lblUsedRollWidth.MinimumSize = new System.Drawing.Size(64, 16);
            this.lblUsedRollWidth.Name = "lblUsedRollWidth";
            this.lblUsedRollWidth.Size = new System.Drawing.Size(89, 16);
            this.lblUsedRollWidth.TabIndex = 16;
            this.lblUsedRollWidth.Text = "Used Roll Length";
            // 
            // pcbNesting
            // 
            this.pcbNesting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcbNesting.Location = new System.Drawing.Point(2, 86);
            this.pcbNesting.MinimumSize = new System.Drawing.Size(64, 16);
            this.pcbNesting.Name = "pcbNesting";
            this.pcbNesting.Size = new System.Drawing.Size(996, 400);
            this.pcbNesting.TabIndex = 15;
            this.pcbNesting.TabStop = false;
            // 
            // pnlNestPanel
            // 
            this.pnlNestPanel.Controls.Add(this.prbProgress);
            this.pnlNestPanel.Controls.Add(this.lblPctOfRollUsedValue);
            this.pnlNestPanel.Controls.Add(this.pcbNesting);
            this.pnlNestPanel.Controls.Add(this.grbNestControls);
            this.pnlNestPanel.Controls.Add(this.lblUsedRollWidth);
            this.pnlNestPanel.Controls.Add(this.lblPctOfRollUsed);
            this.pnlNestPanel.Controls.Add(this.lblUsedRollLgthValue);
            this.pnlNestPanel.Controls.Add(this.lblTotalAreaOfPlacedItemsValue);
            this.pnlNestPanel.Controls.Add(this.lblUsedRollArea);
            this.pnlNestPanel.Controls.Add(this.btnDoNest);
            this.pnlNestPanel.Controls.Add(this.lblTotalAreaOfPlacedItems);
            this.pnlNestPanel.Controls.Add(this.lblUsedRollAreaValue);
            this.pnlNestPanel.Controls.Add(this.lblNumberOfItemsPlacedValue);
            this.pnlNestPanel.Controls.Add(this.label3);
            this.pnlNestPanel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.pnlNestPanel.Location = new System.Drawing.Point(12, 304);
            this.pnlNestPanel.Name = "pnlNestPanel";
            this.pnlNestPanel.Size = new System.Drawing.Size(1000, 614);
            this.pnlNestPanel.TabIndex = 26;
            // 
            // prbProgress
            // 
            this.prbProgress.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.prbProgress.BackSegments = false;
            this.prbProgress.CustomText = null;
            this.prbProgress.CustomWaitingRender = false;
            this.prbProgress.ForeColor = System.Drawing.Color.DodgerBlue;
            this.prbProgress.ForegroundImage = null;
            this.prbProgress.Location = new System.Drawing.Point(62, 10);
            this.prbProgress.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.prbProgress.Name = "prbProgress";
            this.prbProgress.SegmentWidth = 12;
            this.prbProgress.Size = new System.Drawing.Size(844, 23);
            this.prbProgress.TabIndex = 26;
            this.prbProgress.Text = "progressBarAdv1";
            this.prbProgress.Value = 0;
            this.prbProgress.WaitingGradientWidth = 400;
            // 
            // ShapeNestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1053, 982);
            this.Controls.Add(this.pnlNestPanel);
            this.Controls.Add(this.pnlShapePaletteControls);
            this.Controls.Add(this.pnlShapes);
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Name = "ShapeNestForm";
            this.Text = "Shape Nest Form";
            this.grbNestControls.ResumeLayout(false);
            this.grbNestControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNmbrOfRefreshes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIterations)).EndInit();
            this.pnlShapePaletteControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbNesting)).EndInit();
            this.pnlNestPanel.ResumeLayout(false);
            this.pnlNestPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prbProgress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlShapes;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDoNest;
        private System.Windows.Forms.TextBox txbRollWidth;
        private System.Windows.Forms.GroupBox grbNestControls;
        private System.Windows.Forms.Label lblRollWidth;
        private System.Windows.Forms.Label lblNbrIterations;
        private System.Windows.Forms.NumericUpDown nudIterations;
        private System.Windows.Forms.Label lblRotations;
        private System.Windows.Forms.Panel pnlShapePaletteControls;
        private System.Windows.Forms.Label lblPctOfRollUsedValue;
        private System.Windows.Forms.Label lblPctOfRollUsed;
        private System.Windows.Forms.Label lblTotalAreaOfPlacedItemsValue;
        private System.Windows.Forms.Label lblTotalAreaOfPlacedItems;
        private System.Windows.Forms.Label lblNumberOfItemsPlacedValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUsedRollAreaValue;
        private System.Windows.Forms.Label lblUsedRollArea;
        private System.Windows.Forms.Label lblUsedRollLgthValue;
        private System.Windows.Forms.Label lblUsedRollWidth;
        private System.Windows.Forms.PictureBox pcbNesting;
        private System.Windows.Forms.Panel pnlNestPanel;
        private System.Windows.Forms.Label lblMaterialSpacing;
        private System.Windows.Forms.TextBox txbMaterialSpacingInInches;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.ComboBox cmbRotations;
        private System.Windows.Forms.Label lblNumberOfRefreshes;
        private System.Windows.Forms.NumericUpDown nudNmbrOfRefreshes;
        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv prbProgress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbMaxRollLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbUseFullRollWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbMaterialMarginInches;
    }
}