namespace FloorMaterialEstimator.OversUndersForm
{
    partial class UndersNestingForm
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
            this.grbNestUnderSelection = new System.Windows.Forms.GroupBox();
            this.rbnNestNonCoveredUnders = new System.Windows.Forms.RadioButton();
            this.rbnNestAllUnders = new System.Windows.Forms.RadioButton();
            this.grbParameters = new System.Windows.Forms.GroupBox();
            this.nudNmbrOfIterations = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMaterialSpacing = new System.Windows.Forms.Label();
            this.txbMaterialSpacingInInches = new System.Windows.Forms.TextBox();
            this.ckbAllow90DegreeRotation = new System.Windows.Forms.CheckBox();
            this.lblRollWidth = new System.Windows.Forms.Label();
            this.txbRollWidth = new System.Windows.Forms.TextBox();
            this.dgvUnders = new System.Windows.Forms.DataGridView();
            this.btnDoNest = new System.Windows.Forms.Button();
            this.pcbNesting = new System.Windows.Forms.PictureBox();
            this.lblUsedRollWidth = new System.Windows.Forms.Label();
            this.lblUsedRollWidthValue = new System.Windows.Forms.Label();
            this.lblUsedRollAreaValue = new System.Windows.Forms.Label();
            this.lblUsedRollArea = new System.Windows.Forms.Label();
            this.lblNumberOfItemsPlacedValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalAreaOfPlacedItemsValue = new System.Windows.Forms.Label();
            this.lblTotalAreaOfPlacedItems = new System.Windows.Forms.Label();
            this.lblPctOfRollUsed = new System.Windows.Forms.Label();
            this.lblPctOfRollUsedValue = new System.Windows.Forms.Label();
            this.grbNestUnderSelection.SuspendLayout();
            this.grbParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNmbrOfIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbNesting)).BeginInit();
            this.SuspendLayout();
            // 
            // grbNestUnderSelection
            // 
            this.grbNestUnderSelection.Controls.Add(this.rbnNestNonCoveredUnders);
            this.grbNestUnderSelection.Controls.Add(this.rbnNestAllUnders);
            this.grbNestUnderSelection.Location = new System.Drawing.Point(28, 23);
            this.grbNestUnderSelection.Name = "grbNestUnderSelection";
            this.grbNestUnderSelection.Size = new System.Drawing.Size(364, 60);
            this.grbNestUnderSelection.TabIndex = 0;
            this.grbNestUnderSelection.TabStop = false;
            this.grbNestUnderSelection.Text = "Initial Under Set";
            // 
            // rbnNestNonCoveredUnders
            // 
            this.rbnNestNonCoveredUnders.AutoSize = true;
            this.rbnNestNonCoveredUnders.Checked = true;
            this.rbnNestNonCoveredUnders.Location = new System.Drawing.Point(151, 25);
            this.rbnNestNonCoveredUnders.Name = "rbnNestNonCoveredUnders";
            this.rbnNestNonCoveredUnders.Size = new System.Drawing.Size(174, 17);
            this.rbnNestNonCoveredUnders.TabIndex = 1;
            this.rbnNestNonCoveredUnders.TabStop = true;
            this.rbnNestNonCoveredUnders.Text = "Nest Only Non-Covered Unders";
            this.rbnNestNonCoveredUnders.UseVisualStyleBackColor = true;
            // 
            // rbnNestAllUnders
            // 
            this.rbnNestAllUnders.AutoSize = true;
            this.rbnNestAllUnders.Location = new System.Drawing.Point(33, 23);
            this.rbnNestAllUnders.Name = "rbnNestAllUnders";
            this.rbnNestAllUnders.Size = new System.Drawing.Size(98, 17);
            this.rbnNestAllUnders.TabIndex = 0;
            this.rbnNestAllUnders.Text = "Nest All Unders";
            this.rbnNestAllUnders.UseVisualStyleBackColor = true;
            // 
            // grbParameters
            // 
            this.grbParameters.Controls.Add(this.nudNmbrOfIterations);
            this.grbParameters.Controls.Add(this.label1);
            this.grbParameters.Controls.Add(this.lblMaterialSpacing);
            this.grbParameters.Controls.Add(this.txbMaterialSpacingInInches);
            this.grbParameters.Controls.Add(this.ckbAllow90DegreeRotation);
            this.grbParameters.Controls.Add(this.lblRollWidth);
            this.grbParameters.Controls.Add(this.txbRollWidth);
            this.grbParameters.Location = new System.Drawing.Point(28, 105);
            this.grbParameters.Name = "grbParameters";
            this.grbParameters.Size = new System.Drawing.Size(364, 123);
            this.grbParameters.TabIndex = 1;
            this.grbParameters.TabStop = false;
            this.grbParameters.Text = "Parameters";
            // 
            // nudNmbrOfIterations
            // 
            this.nudNmbrOfIterations.Location = new System.Drawing.Point(266, 46);
            this.nudNmbrOfIterations.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudNmbrOfIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNmbrOfIterations.Name = "nudNmbrOfIterations";
            this.nudNmbrOfIterations.Size = new System.Drawing.Size(47, 20);
            this.nudNmbrOfIterations.TabIndex = 7;
            this.nudNmbrOfIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudNmbrOfIterations.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Number Of\r\nIterations";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMaterialSpacing
            // 
            this.lblMaterialSpacing.AutoSize = true;
            this.lblMaterialSpacing.Location = new System.Drawing.Point(148, 14);
            this.lblMaterialSpacing.Name = "lblMaterialSpacing";
            this.lblMaterialSpacing.Size = new System.Drawing.Size(86, 26);
            this.lblMaterialSpacing.TabIndex = 4;
            this.lblMaterialSpacing.Text = "Material Spacing\r\n(Inches)";
            this.lblMaterialSpacing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbMaterialSpacingInInches
            // 
            this.txbMaterialSpacingInInches.Location = new System.Drawing.Point(151, 45);
            this.txbMaterialSpacingInInches.Name = "txbMaterialSpacingInInches";
            this.txbMaterialSpacingInInches.Size = new System.Drawing.Size(73, 20);
            this.txbMaterialSpacingInInches.TabIndex = 3;
            // 
            // ckbAllow90DegreeRotation
            // 
            this.ckbAllow90DegreeRotation.AutoSize = true;
            this.ckbAllow90DegreeRotation.Location = new System.Drawing.Point(115, 86);
            this.ckbAllow90DegreeRotation.Name = "ckbAllow90DegreeRotation";
            this.ckbAllow90DegreeRotation.Size = new System.Drawing.Size(147, 17);
            this.ckbAllow90DegreeRotation.TabIndex = 2;
            this.ckbAllow90DegreeRotation.Text = "Allow 90 Degree Rotation";
            this.ckbAllow90DegreeRotation.UseVisualStyleBackColor = true;
            // 
            // lblRollWidth
            // 
            this.lblRollWidth.AutoSize = true;
            this.lblRollWidth.Location = new System.Drawing.Point(41, 21);
            this.lblRollWidth.Name = "lblRollWidth";
            this.lblRollWidth.Size = new System.Drawing.Size(56, 13);
            this.lblRollWidth.TabIndex = 1;
            this.lblRollWidth.Text = "Roll Width";
            this.lblRollWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbRollWidth
            // 
            this.txbRollWidth.Location = new System.Drawing.Point(33, 45);
            this.txbRollWidth.Name = "txbRollWidth";
            this.txbRollWidth.Size = new System.Drawing.Size(73, 20);
            this.txbRollWidth.TabIndex = 0;
            this.txbRollWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dgvUnders
            // 
            this.dgvUnders.AllowUserToAddRows = false;
            this.dgvUnders.AllowUserToDeleteRows = false;
            this.dgvUnders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnders.Location = new System.Drawing.Point(28, 255);
            this.dgvUnders.Name = "dgvUnders";
            this.dgvUnders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvUnders.Size = new System.Drawing.Size(364, 260);
            this.dgvUnders.TabIndex = 2;
            // 
            // btnDoNest
            // 
            this.btnDoNest.Location = new System.Drawing.Point(187, 551);
            this.btnDoNest.Name = "btnDoNest";
            this.btnDoNest.Size = new System.Drawing.Size(75, 37);
            this.btnDoNest.TabIndex = 3;
            this.btnDoNest.Text = "Perform\r\nNesting\r\n";
            this.btnDoNest.UseVisualStyleBackColor = true;
            this.btnDoNest.Click += new System.EventHandler(this.btnDoNest_Click);
            // 
            // pcbNesting
            // 
            this.pcbNesting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcbNesting.Location = new System.Drawing.Point(430, 95);
            this.pcbNesting.MinimumSize = new System.Drawing.Size(64, 16);
            this.pcbNesting.Name = "pcbNesting";
            this.pcbNesting.Size = new System.Drawing.Size(840, 440);
            this.pcbNesting.TabIndex = 4;
            this.pcbNesting.TabStop = false;
            // 
            // lblUsedRollWidth
            // 
            this.lblUsedRollWidth.AutoSize = true;
            this.lblUsedRollWidth.Location = new System.Drawing.Point(430, 58);
            this.lblUsedRollWidth.MinimumSize = new System.Drawing.Size(64, 16);
            this.lblUsedRollWidth.Name = "lblUsedRollWidth";
            this.lblUsedRollWidth.Size = new System.Drawing.Size(84, 16);
            this.lblUsedRollWidth.TabIndex = 5;
            this.lblUsedRollWidth.Text = "Used Roll Width";
            // 
            // lblUsedRollWidthValue
            // 
            this.lblUsedRollWidthValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUsedRollWidthValue.Location = new System.Drawing.Point(522, 58);
            this.lblUsedRollWidthValue.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUsedRollWidthValue.Name = "lblUsedRollWidthValue";
            this.lblUsedRollWidthValue.Size = new System.Drawing.Size(64, 16);
            this.lblUsedRollWidthValue.TabIndex = 6;
            this.lblUsedRollWidthValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsedRollAreaValue
            // 
            this.lblUsedRollAreaValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUsedRollAreaValue.Location = new System.Drawing.Point(680, 58);
            this.lblUsedRollAreaValue.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUsedRollAreaValue.Name = "lblUsedRollAreaValue";
            this.lblUsedRollAreaValue.Size = new System.Drawing.Size(64, 16);
            this.lblUsedRollAreaValue.TabIndex = 8;
            this.lblUsedRollAreaValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsedRollArea
            // 
            this.lblUsedRollArea.AutoSize = true;
            this.lblUsedRollArea.Location = new System.Drawing.Point(594, 58);
            this.lblUsedRollArea.MinimumSize = new System.Drawing.Size(64, 16);
            this.lblUsedRollArea.Name = "lblUsedRollArea";
            this.lblUsedRollArea.Size = new System.Drawing.Size(78, 16);
            this.lblUsedRollArea.TabIndex = 7;
            this.lblUsedRollArea.Text = "Used Roll Area";
            // 
            // lblNumberOfItemsPlacedValue
            // 
            this.lblNumberOfItemsPlacedValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNumberOfItemsPlacedValue.Location = new System.Drawing.Point(828, 58);
            this.lblNumberOfItemsPlacedValue.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblNumberOfItemsPlacedValue.Name = "lblNumberOfItemsPlacedValue";
            this.lblNumberOfItemsPlacedValue.Size = new System.Drawing.Size(48, 16);
            this.lblNumberOfItemsPlacedValue.TabIndex = 10;
            this.lblNumberOfItemsPlacedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(752, 58);
            this.label3.MinimumSize = new System.Drawing.Size(64, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Items Placed";
            // 
            // lblTotalAreaOfPlacedItemsValue
            // 
            this.lblTotalAreaOfPlacedItemsValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalAreaOfPlacedItemsValue.Location = new System.Drawing.Point(999, 58);
            this.lblTotalAreaOfPlacedItemsValue.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblTotalAreaOfPlacedItemsValue.Name = "lblTotalAreaOfPlacedItemsValue";
            this.lblTotalAreaOfPlacedItemsValue.Size = new System.Drawing.Size(48, 16);
            this.lblTotalAreaOfPlacedItemsValue.TabIndex = 12;
            this.lblTotalAreaOfPlacedItemsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalAreaOfPlacedItems
            // 
            this.lblTotalAreaOfPlacedItems.AutoSize = true;
            this.lblTotalAreaOfPlacedItems.Location = new System.Drawing.Point(884, 58);
            this.lblTotalAreaOfPlacedItems.MinimumSize = new System.Drawing.Size(64, 16);
            this.lblTotalAreaOfPlacedItems.Name = "lblTotalAreaOfPlacedItems";
            this.lblTotalAreaOfPlacedItems.Size = new System.Drawing.Size(107, 16);
            this.lblTotalAreaOfPlacedItems.TabIndex = 11;
            this.lblTotalAreaOfPlacedItems.Text = "Area Of Placed Items";
            // 
            // lblPctOfRollUsed
            // 
            this.lblPctOfRollUsed.AutoSize = true;
            this.lblPctOfRollUsed.Location = new System.Drawing.Point(1055, 58);
            this.lblPctOfRollUsed.MinimumSize = new System.Drawing.Size(64, 16);
            this.lblPctOfRollUsed.Name = "lblPctOfRollUsed";
            this.lblPctOfRollUsed.Size = new System.Drawing.Size(86, 16);
            this.lblPctOfRollUsed.TabIndex = 13;
            this.lblPctOfRollUsed.Text = "Pct Of Roll Used";
            // 
            // lblPctOfRollUsedValue
            // 
            this.lblPctOfRollUsedValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPctOfRollUsedValue.Location = new System.Drawing.Point(1149, 58);
            this.lblPctOfRollUsedValue.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblPctOfRollUsedValue.Name = "lblPctOfRollUsedValue";
            this.lblPctOfRollUsedValue.Size = new System.Drawing.Size(54, 16);
            this.lblPctOfRollUsedValue.TabIndex = 14;
            this.lblPctOfRollUsedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UndersNestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 617);
            this.Controls.Add(this.lblPctOfRollUsedValue);
            this.Controls.Add(this.lblPctOfRollUsed);
            this.Controls.Add(this.lblTotalAreaOfPlacedItemsValue);
            this.Controls.Add(this.lblTotalAreaOfPlacedItems);
            this.Controls.Add(this.lblNumberOfItemsPlacedValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblUsedRollAreaValue);
            this.Controls.Add(this.lblUsedRollArea);
            this.Controls.Add(this.lblUsedRollWidthValue);
            this.Controls.Add(this.lblUsedRollWidth);
            this.Controls.Add(this.pcbNesting);
            this.Controls.Add(this.btnDoNest);
            this.Controls.Add(this.dgvUnders);
            this.Controls.Add(this.grbParameters);
            this.Controls.Add(this.grbNestUnderSelection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(64, 39);
            this.Name = "UndersNestingForm";
            this.Text = "Unders Nesting";
            this.grbNestUnderSelection.ResumeLayout(false);
            this.grbNestUnderSelection.PerformLayout();
            this.grbParameters.ResumeLayout(false);
            this.grbParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNmbrOfIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbNesting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbNestUnderSelection;
        private System.Windows.Forms.RadioButton rbnNestNonCoveredUnders;
        private System.Windows.Forms.RadioButton rbnNestAllUnders;
        private System.Windows.Forms.GroupBox grbParameters;
        private System.Windows.Forms.TextBox txbRollWidth;
        private System.Windows.Forms.NumericUpDown nudNmbrOfIterations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMaterialSpacing;
        private System.Windows.Forms.TextBox txbMaterialSpacingInInches;
        private System.Windows.Forms.CheckBox ckbAllow90DegreeRotation;
        private System.Windows.Forms.Label lblRollWidth;
        private System.Windows.Forms.DataGridView dgvUnders;
        private System.Windows.Forms.Button btnDoNest;
        private System.Windows.Forms.PictureBox pcbNesting;
        private System.Windows.Forms.Label lblUsedRollWidth;
        private System.Windows.Forms.Label lblUsedRollWidthValue;
        private System.Windows.Forms.Label lblUsedRollAreaValue;
        private System.Windows.Forms.Label lblUsedRollArea;
        private System.Windows.Forms.Label lblNumberOfItemsPlacedValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalAreaOfPlacedItemsValue;
        private System.Windows.Forms.Label lblTotalAreaOfPlacedItems;
        private System.Windows.Forms.Label lblPctOfRollUsed;
        private System.Windows.Forms.Label lblPctOfRollUsedValue;
    }
}