namespace FloorMaterialEstimator.CanvasManager
{
    partial class RotationBaseForm
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
            this.grbRotateFromReferenceLine = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCreateReferenceLine = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rbnRotateReferenceLineToVertical = new System.Windows.Forms.RadioButton();
            this.grbRotateBySpecifiedAmount = new System.Windows.Forms.GroupBox();
            this.grbxAddIncrementalRotation = new System.Windows.Forms.GroupBox();
            this.rbnRotateCounterClockwise1Degree = new System.Windows.Forms.RadioButton();
            this.rbnRotateClockwise1Degree = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRotateByDegrees = new System.Windows.Forms.Button();
            this.rbnRotateAbsolute = new System.Windows.Forms.RadioButton();
            this.rbnRotateIncremental = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txbRotationInDegrees = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.lblRotateBySpeciedAmountNote = new System.Windows.Forms.Label();
            this.lblCurrentRotation = new System.Windows.Forms.Label();
            this.btnResetToOriginalOrientation = new System.Windows.Forms.Button();
            this.grbRotateFromReferenceLine.SuspendLayout();
            this.grbRotateBySpecifiedAmount.SuspendLayout();
            this.grbxAddIncrementalRotation.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txbRotationInDegrees)).BeginInit();
            this.SuspendLayout();
            // 
            // grbRotateFromReferenceLine
            // 
            this.grbRotateFromReferenceLine.Controls.Add(this.button2);
            this.grbRotateFromReferenceLine.Controls.Add(this.btnCreateReferenceLine);
            this.grbRotateFromReferenceLine.Controls.Add(this.radioButton1);
            this.grbRotateFromReferenceLine.Controls.Add(this.rbnRotateReferenceLineToVertical);
            this.grbRotateFromReferenceLine.Location = new System.Drawing.Point(59, 58);
            this.grbRotateFromReferenceLine.Name = "grbRotateFromReferenceLine";
            this.grbRotateFromReferenceLine.Size = new System.Drawing.Size(580, 131);
            this.grbRotateFromReferenceLine.TabIndex = 0;
            this.grbRotateFromReferenceLine.TabStop = false;
            this.grbRotateFromReferenceLine.Text = " Rotate With Reference to A Reference Line";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(447, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Rotate";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnCreateReferenceLine
            // 
            this.btnCreateReferenceLine.Location = new System.Drawing.Point(62, 55);
            this.btnCreateReferenceLine.Name = "btnCreateReferenceLine";
            this.btnCreateReferenceLine.Size = new System.Drawing.Size(128, 23);
            this.btnCreateReferenceLine.TabIndex = 6;
            this.btnCreateReferenceLine.Text = "Create Reference Line";
            this.btnCreateReferenceLine.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(224, 71);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(184, 17);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.Text = "Rotate reference line to horizontal";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // rbnRotateReferenceLineToVertical
            // 
            this.rbnRotateReferenceLineToVertical.AutoSize = true;
            this.rbnRotateReferenceLineToVertical.Checked = true;
            this.rbnRotateReferenceLineToVertical.Location = new System.Drawing.Point(224, 33);
            this.rbnRotateReferenceLineToVertical.Name = "rbnRotateReferenceLineToVertical";
            this.rbnRotateReferenceLineToVertical.Size = new System.Drawing.Size(173, 17);
            this.rbnRotateReferenceLineToVertical.TabIndex = 4;
            this.rbnRotateReferenceLineToVertical.TabStop = true;
            this.rbnRotateReferenceLineToVertical.Text = "Rotate reference line to vertical";
            this.rbnRotateReferenceLineToVertical.UseVisualStyleBackColor = true;
            // 
            // grbRotateBySpecifiedAmount
            // 
            this.grbRotateBySpecifiedAmount.Controls.Add(this.grbxAddIncrementalRotation);
            this.grbRotateBySpecifiedAmount.Controls.Add(this.groupBox1);
            this.grbRotateBySpecifiedAmount.Controls.Add(this.lblRotateBySpeciedAmountNote);
            this.grbRotateBySpecifiedAmount.Location = new System.Drawing.Point(59, 195);
            this.grbRotateBySpecifiedAmount.Name = "grbRotateBySpecifiedAmount";
            this.grbRotateBySpecifiedAmount.Size = new System.Drawing.Size(580, 204);
            this.grbRotateBySpecifiedAmount.TabIndex = 1;
            this.grbRotateBySpecifiedAmount.TabStop = false;
            this.grbRotateBySpecifiedAmount.Text = "Rotate By Specified Amount";
            // 
            // grbxAddIncrementalRotation
            // 
            this.grbxAddIncrementalRotation.Controls.Add(this.rbnRotateCounterClockwise1Degree);
            this.grbxAddIncrementalRotation.Controls.Add(this.rbnRotateClockwise1Degree);
            this.grbxAddIncrementalRotation.Controls.Add(this.button1);
            this.grbxAddIncrementalRotation.Location = new System.Drawing.Point(372, 49);
            this.grbxAddIncrementalRotation.Name = "grbxAddIncrementalRotation";
            this.grbxAddIncrementalRotation.Size = new System.Drawing.Size(202, 126);
            this.grbxAddIncrementalRotation.TabIndex = 2;
            this.grbxAddIncrementalRotation.TabStop = false;
            this.grbxAddIncrementalRotation.Text = "Incremental (1 degree) rotation";
            // 
            // rbnRotateCounterClockwise1Degree
            // 
            this.rbnRotateCounterClockwise1Degree.AutoSize = true;
            this.rbnRotateCounterClockwise1Degree.Location = new System.Drawing.Point(29, 52);
            this.rbnRotateCounterClockwise1Degree.Name = "rbnRotateCounterClockwise1Degree";
            this.rbnRotateCounterClockwise1Degree.Size = new System.Drawing.Size(113, 17);
            this.rbnRotateCounterClockwise1Degree.TabIndex = 7;
            this.rbnRotateCounterClockwise1Degree.Text = "Counter Clockwise";
            this.rbnRotateCounterClockwise1Degree.UseVisualStyleBackColor = true;
            // 
            // rbnRotateClockwise1Degree
            // 
            this.rbnRotateClockwise1Degree.AutoSize = true;
            this.rbnRotateClockwise1Degree.Checked = true;
            this.rbnRotateClockwise1Degree.Location = new System.Drawing.Point(31, 24);
            this.rbnRotateClockwise1Degree.Name = "rbnRotateClockwise1Degree";
            this.rbnRotateClockwise1Degree.Size = new System.Drawing.Size(73, 17);
            this.rbnRotateClockwise1Degree.TabIndex = 6;
            this.rbnRotateClockwise1Degree.TabStop = true;
            this.rbnRotateClockwise1Degree.Text = "Clockwise";
            this.rbnRotateClockwise1Degree.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Rotate";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnRotateByDegrees);
            this.groupBox1.Controls.Add(this.rbnRotateAbsolute);
            this.groupBox1.Controls.Add(this.rbnRotateIncremental);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txbRotationInDegrees);
            this.groupBox1.Location = new System.Drawing.Point(24, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 130);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Manual Rotation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 39);
            this.label2.TabIndex = 5;
            this.label2.Text = "Negative values will\r\nresult in a counter\r\nclockwise rotation";
            // 
            // btnRotateByDegrees
            // 
            this.btnRotateByDegrees.Location = new System.Drawing.Point(172, 81);
            this.btnRotateByDegrees.Name = "btnRotateByDegrees";
            this.btnRotateByDegrees.Size = new System.Drawing.Size(75, 23);
            this.btnRotateByDegrees.TabIndex = 4;
            this.btnRotateByDegrees.Text = "Rotate";
            this.btnRotateByDegrees.UseVisualStyleBackColor = true;
            this.btnRotateByDegrees.Click += new System.EventHandler(this.btnRotateByDegrees_Click);
            // 
            // rbnRotateAbsolute
            // 
            this.rbnRotateAbsolute.AutoSize = true;
            this.rbnRotateAbsolute.Checked = true;
            this.rbnRotateAbsolute.Location = new System.Drawing.Point(124, 19);
            this.rbnRotateAbsolute.Name = "rbnRotateAbsolute";
            this.rbnRotateAbsolute.Size = new System.Drawing.Size(218, 17);
            this.rbnRotateAbsolute.TabIndex = 3;
            this.rbnRotateAbsolute.TabStop = true;
            this.rbnRotateAbsolute.Text = "Absolute: Rotate from original orientation.";
            this.rbnRotateAbsolute.UseVisualStyleBackColor = true;
            // 
            // rbnRotateIncremental
            // 
            this.rbnRotateIncremental.AutoSize = true;
            this.rbnRotateIncremental.Location = new System.Drawing.Point(124, 42);
            this.rbnRotateIncremental.Name = "rbnRotateIncremental";
            this.rbnRotateIncremental.Size = new System.Drawing.Size(194, 17);
            this.rbnRotateIncremental.TabIndex = 2;
            this.rbnRotateIncremental.Text = "Incremental: Add to current rotation.";
            this.rbnRotateIncremental.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rotation\r\nin Degrees";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbRotationInDegrees
            // 
            this.txbRotationInDegrees.BeforeTouchSize = new System.Drawing.Size(70, 20);
            this.txbRotationInDegrees.Location = new System.Drawing.Point(15, 53);
            this.txbRotationInDegrees.Name = "txbRotationInDegrees";
            this.txbRotationInDegrees.Size = new System.Drawing.Size(70, 20);
            this.txbRotationInDegrees.TabIndex = 0;
            this.txbRotationInDegrees.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txbRotationInDegrees.TextChanged += new System.EventHandler(this.txbRotationInDegrees_TextChanged);
            // 
            // lblRotateBySpeciedAmountNote
            // 
            this.lblRotateBySpeciedAmountNote.AutoSize = true;
            this.lblRotateBySpeciedAmountNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRotateBySpeciedAmountNote.Location = new System.Drawing.Point(50, 25);
            this.lblRotateBySpeciedAmountNote.Name = "lblRotateBySpeciedAmountNote";
            this.lblRotateBySpeciedAmountNote.Size = new System.Drawing.Size(507, 13);
            this.lblRotateBySpeciedAmountNote.TabIndex = 0;
            this.lblRotateBySpeciedAmountNote.Text = "Note: You can check alignment by holding down the B key and inspecting the alignm" +
    "ent";
            // 
            // lblCurrentRotation
            // 
            this.lblCurrentRotation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentRotation.Location = new System.Drawing.Point(69, 22);
            this.lblCurrentRotation.Name = "lblCurrentRotation";
            this.lblCurrentRotation.Size = new System.Drawing.Size(356, 23);
            this.lblCurrentRotation.TabIndex = 2;
            this.lblCurrentRotation.Text = "Current Aggregate Rotation Is 0 Degrees";
            this.lblCurrentRotation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnResetToOriginalOrientation
            // 
            this.btnResetToOriginalOrientation.Location = new System.Drawing.Point(431, 22);
            this.btnResetToOriginalOrientation.Name = "btnResetToOriginalOrientation";
            this.btnResetToOriginalOrientation.Size = new System.Drawing.Size(150, 23);
            this.btnResetToOriginalOrientation.TabIndex = 5;
            this.btnResetToOriginalOrientation.Text = "Reset to Original Orienation";
            this.btnResetToOriginalOrientation.UseVisualStyleBackColor = true;
            // 
            // RotationBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 438);
            this.Controls.Add(this.btnResetToOriginalOrientation);
            this.Controls.Add(this.lblCurrentRotation);
            this.Controls.Add(this.grbRotateBySpecifiedAmount);
            this.Controls.Add(this.grbRotateFromReferenceLine);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RotationBaseForm";
            this.Text = "Rotation Form";
            this.grbRotateFromReferenceLine.ResumeLayout(false);
            this.grbRotateFromReferenceLine.PerformLayout();
            this.grbRotateBySpecifiedAmount.ResumeLayout(false);
            this.grbRotateBySpecifiedAmount.PerformLayout();
            this.grbxAddIncrementalRotation.ResumeLayout(false);
            this.grbxAddIncrementalRotation.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txbRotationInDegrees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbRotateFromReferenceLine;
        private System.Windows.Forms.GroupBox grbRotateBySpecifiedAmount;
        private System.Windows.Forms.Label lblRotateBySpeciedAmountNote;
        private System.Windows.Forms.GroupBox groupBox1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txbRotationInDegrees;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbnRotateAbsolute;
        private System.Windows.Forms.RadioButton rbnRotateIncremental;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rbnRotateReferenceLineToVertical;
        private System.Windows.Forms.GroupBox grbxAddIncrementalRotation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnRotateByDegrees;
        private System.Windows.Forms.RadioButton rbnRotateCounterClockwise1Degree;
        private System.Windows.Forms.RadioButton rbnRotateClockwise1Degree;
        private System.Windows.Forms.Label lblCurrentRotation;
        private System.Windows.Forms.Button btnResetToOriginalOrientation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnCreateReferenceLine;
    }
}