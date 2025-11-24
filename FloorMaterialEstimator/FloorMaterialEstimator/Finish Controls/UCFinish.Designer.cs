//-------------------------------------------------------------------------------//
// <copyright file="UCFinish.Designer.cs" company="Bruun Estimating, LLC">       //
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Finish_Controls
{
    partial class UCFinish
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWasteText = new System.Windows.Forms.Label();
            this.lblAreaText = new System.Windows.Forms.Label();
            this.lblPerimText = new System.Windows.Forms.Label();
            this.lblPerimValue = new System.Windows.Forms.Label();
            this.lblWasteValue = new System.Windows.Forms.Label();
            this.lblAreaValue = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlColor = new System.Windows.Forms.Panel();
            this.trbOpacity = new System.Windows.Forms.TrackBar();
            this.lblOpacity = new System.Windows.Forms.Label();
            this.lblOpacityValue = new System.Windows.Forms.Label();
            this.lblTrimFactor = new System.Windows.Forms.Label();
            this.cmbTrimFactor = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.trbOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWasteText
            // 
            this.lblWasteText.AutoSize = true;
            this.lblWasteText.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblWasteText.Location = new System.Drawing.Point(87, 51);
            this.lblWasteText.Name = "lblWasteText";
            this.lblWasteText.Size = new System.Drawing.Size(54, 13);
            this.lblWasteText.TabIndex = 17;
            this.lblWasteText.Text = "Waste %:";
            // 
            // lblAreaText
            // 
            this.lblAreaText.AutoSize = true;
            this.lblAreaText.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblAreaText.Location = new System.Drawing.Point(87, 8);
            this.lblAreaText.Name = "lblAreaText";
            this.lblAreaText.Size = new System.Drawing.Size(33, 13);
            this.lblAreaText.TabIndex = 15;
            this.lblAreaText.Text = "Area:";
            // 
            // lblPerimText
            // 
            this.lblPerimText.AutoSize = true;
            this.lblPerimText.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblPerimText.Location = new System.Drawing.Point(87, 29);
            this.lblPerimText.Name = "lblPerimText";
            this.lblPerimText.Size = new System.Drawing.Size(38, 13);
            this.lblPerimText.TabIndex = 16;
            this.lblPerimText.Text = "Perim:";
            // 
            // lblPerimValue
            // 
            this.lblPerimValue.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblPerimValue.Location = new System.Drawing.Point(140, 26);
            this.lblPerimValue.Name = "lblPerimValue";
            this.lblPerimValue.Size = new System.Drawing.Size(48, 18);
            this.lblPerimValue.TabIndex = 21;
            this.lblPerimValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWasteValue
            // 
            this.lblWasteValue.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblWasteValue.Location = new System.Drawing.Point(140, 50);
            this.lblWasteValue.Name = "lblWasteValue";
            this.lblWasteValue.Size = new System.Drawing.Size(48, 18);
            this.lblWasteValue.TabIndex = 20;
            this.lblWasteValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAreaValue
            // 
            this.lblAreaValue.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblAreaValue.Location = new System.Drawing.Point(140, 5);
            this.lblAreaValue.Name = "lblAreaValue";
            this.lblAreaValue.Size = new System.Drawing.Size(48, 18);
            this.lblAreaValue.TabIndex = 19;
            this.lblAreaValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(74, 21);
            this.lblName.TabIndex = 22;
            this.lblName.Text = "Finish";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlColor
            // 
            this.pnlColor.BackColor = System.Drawing.Color.LawnGreen;
            this.pnlColor.Location = new System.Drawing.Point(4, 29);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(58, 26);
            this.pnlColor.TabIndex = 23;
            // 
            // trbOpacity
            // 
            this.trbOpacity.Location = new System.Drawing.Point(4, 63);
            this.trbOpacity.Maximum = 100;
            this.trbOpacity.Name = "trbOpacity";
            this.trbOpacity.Size = new System.Drawing.Size(77, 45);
            this.trbOpacity.TabIndex = 24;
            this.trbOpacity.Value = 100;
            // 
            // lblOpacity
            // 
            this.lblOpacity.AutoSize = true;
            this.lblOpacity.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblOpacity.Location = new System.Drawing.Point(87, 76);
            this.lblOpacity.Name = "lblOpacity";
            this.lblOpacity.Size = new System.Drawing.Size(46, 13);
            this.lblOpacity.TabIndex = 25;
            this.lblOpacity.Text = "Opacity";
            // 
            // lblOpacityValue
            // 
            this.lblOpacityValue.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblOpacityValue.Location = new System.Drawing.Point(140, 73);
            this.lblOpacityValue.Name = "lblOpacityValue";
            this.lblOpacityValue.Size = new System.Drawing.Size(48, 18);
            this.lblOpacityValue.TabIndex = 26;
            this.lblOpacityValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTrimFactor
            // 
            this.lblTrimFactor.AutoSize = true;
            this.lblTrimFactor.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblTrimFactor.Location = new System.Drawing.Point(19, 111);
            this.lblTrimFactor.Name = "lblTrimFactor";
            this.lblTrimFactor.Size = new System.Drawing.Size(62, 13);
            this.lblTrimFactor.TabIndex = 27;
            this.lblTrimFactor.Text = "Trim Factor";
            // 
            // cmbTrimFactor
            // 
            this.cmbTrimFactor.FormattingEnabled = true;
            this.cmbTrimFactor.Location = new System.Drawing.Point(99, 107);
            this.cmbTrimFactor.Name = "cmbTrimFactor";
            this.cmbTrimFactor.Size = new System.Drawing.Size(58, 21);
            this.cmbTrimFactor.TabIndex = 28;
            // 
            // UCFinish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.cmbTrimFactor);
            this.Controls.Add(this.lblTrimFactor);
            this.Controls.Add(this.lblOpacityValue);
            this.Controls.Add(this.lblOpacity);
            this.Controls.Add(this.trbOpacity);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPerimValue);
            this.Controls.Add(this.lblWasteValue);
            this.Controls.Add(this.lblAreaValue);
            this.Controls.Add(this.lblWasteText);
            this.Controls.Add(this.lblAreaText);
            this.Controls.Add(this.lblPerimText);
            this.Name = "UCFinish";
            this.Size = new System.Drawing.Size(200, 143);
            ((System.ComponentModel.ISupportInitialize)(this.trbOpacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWasteText;
        private System.Windows.Forms.Label lblAreaText;
        private System.Windows.Forms.Label lblPerimText;
        private System.Windows.Forms.Label lblPerimValue;
        private System.Windows.Forms.Label lblWasteValue;
        private System.Windows.Forms.Label lblAreaValue;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.TrackBar trbOpacity;
        private System.Windows.Forms.Label lblOpacity;
        private System.Windows.Forms.Label lblOpacityValue;
        private System.Windows.Forms.Label lblTrimFactor;
        private System.Windows.Forms.ComboBox cmbTrimFactor;
    }
}
