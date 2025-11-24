namespace FloorMaterialEstimator.Finish_Controls
{
    partial class FieldGuideEditForm
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
            this.btnSaveAsDefault = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLineFinishEditorHide = new System.Windows.Forms.Button();
            this.trbLineWidth = new System.Windows.Forms.TrackBar();
            this.ucCustomDashType = new FloorMaterialEstimator.Finish_Controls.UCCustomDashType();
            this.ucCustomColorPallet = new FloorMaterialEstimator.Finish_Controls.UCCustomColorPallet();
            this.lblLineWidth = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trbOpacity = new System.Windows.Forms.TrackBar();
            this.lblOpacity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trbLineWidth)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveAsDefault
            // 
            this.btnSaveAsDefault.Location = new System.Drawing.Point(36, 481);
            this.btnSaveAsDefault.Name = "btnSaveAsDefault";
            this.btnSaveAsDefault.Size = new System.Drawing.Size(96, 23);
            this.btnSaveAsDefault.TabIndex = 7;
            this.btnSaveAsDefault.Text = "Save As Default";
            this.btnSaveAsDefault.UseVisualStyleBackColor = true;
            this.btnSaveAsDefault.Click += new System.EventHandler(this.btnSaveAsDefault_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Colors";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(238, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Line Styles";
            // 
            // btnLineFinishEditorHide
            // 
            this.btnLineFinishEditorHide.Location = new System.Drawing.Point(198, 481);
            this.btnLineFinishEditorHide.Name = "btnLineFinishEditorHide";
            this.btnLineFinishEditorHide.Size = new System.Drawing.Size(75, 23);
            this.btnLineFinishEditorHide.TabIndex = 15;
            this.btnLineFinishEditorHide.Text = "Hide";
            this.btnLineFinishEditorHide.UseVisualStyleBackColor = true;
            this.btnLineFinishEditorHide.Click += new System.EventHandler(this.btnLineFinishEditorHide_Click);
            // 
            // trbLineWidth
            // 
            this.trbLineWidth.Location = new System.Drawing.Point(6, 21);
            this.trbLineWidth.Maximum = 80;
            this.trbLineWidth.Name = "trbLineWidth";
            this.trbLineWidth.Size = new System.Drawing.Size(218, 45);
            this.trbLineWidth.TabIndex = 16;
            // 
            // ucCustomDashType
            // 
            this.ucCustomDashType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucCustomDashType.Location = new System.Drawing.Point(241, 30);
            this.ucCustomDashType.Name = "ucCustomDashType";
            this.ucCustomDashType.Size = new System.Drawing.Size(100, 266);
            this.ucCustomDashType.TabIndex = 2;
            // 
            // ucCustomColorPalette
            // 
            this.ucCustomColorPallet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucCustomColorPallet.Location = new System.Drawing.Point(21, 28);
            this.ucCustomColorPallet.Name = "ucCustomColorPalette";
            this.ucCustomColorPallet.Size = new System.Drawing.Size(203, 268);
            this.ucCustomColorPallet.TabIndex = 0;
            // 
            // lblLineWidth
            // 
            this.lblLineWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLineWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineWidth.Location = new System.Drawing.Point(230, 30);
            this.lblLineWidth.Name = "lblLineWidth";
            this.lblLineWidth.Size = new System.Drawing.Size(70, 24);
            this.lblLineWidth.TabIndex = 17;
            this.lblLineWidth.Text = "2.0";
            this.lblLineWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trbLineWidth);
            this.groupBox1.Controls.Add(this.lblLineWidth);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(21, 393);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 67);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Line Width In Pts";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trbOpacity);
            this.groupBox2.Controls.Add(this.lblOpacity);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(19, 310);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 67);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opacity";
            // 
            // trbOpacity
            // 
            this.trbOpacity.Location = new System.Drawing.Point(6, 21);
            this.trbOpacity.Maximum = 100;
            this.trbOpacity.Name = "trbOpacity";
            this.trbOpacity.Size = new System.Drawing.Size(218, 45);
            this.trbOpacity.TabIndex = 16;
            // 
            // lblOpacity
            // 
            this.lblOpacity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOpacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpacity.Location = new System.Drawing.Point(230, 30);
            this.lblOpacity.Name = "lblOpacity";
            this.lblOpacity.Size = new System.Drawing.Size(70, 24);
            this.lblOpacity.TabIndex = 17;
            this.lblOpacity.Text = "2.0";
            this.lblOpacity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FieldGuideEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 554);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLineFinishEditorHide);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSaveAsDefault);
            this.Controls.Add(this.ucCustomDashType);
            this.Controls.Add(this.ucCustomColorPallet);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FieldGuideEditForm";
            this.Text = "Edit Field Guides";
            ((System.ComponentModel.ISupportInitialize)(this.trbLineWidth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbOpacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCCustomColorPallet ucCustomColorPallet;
        private UCCustomDashType ucCustomDashType;
        private System.Windows.Forms.Button btnSaveAsDefault;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLineFinishEditorHide;
        private System.Windows.Forms.TrackBar trbLineWidth;
        private System.Windows.Forms.Label lblLineWidth;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar trbOpacity;
        private System.Windows.Forms.Label lblOpacity;
    }
}