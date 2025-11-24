//-------------------------------------------------------------------------------//
// <copyright file="UCLine.Designer.cs" company="Bruun Estimating, LLC">         // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace PaletteLib
{
    partial class UCLineFinishSummary
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
            this.lblLineName = new System.Windows.Forms.Label();
            this.lblPerimDecimal = new System.Windows.Forms.Label();
            this.lblPerimPoint = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLineName
            // 
            this.lblLineName.AutoSize = true;
            this.lblLineName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineName.Location = new System.Drawing.Point(8, 22);
            this.lblLineName.Name = "lblLineName";
            this.lblLineName.Size = new System.Drawing.Size(39, 21);
            this.lblLineName.TabIndex = 0;
            this.lblLineName.Text = "Line";
            // 
            // lblPerimDecimal
            // 
            this.lblPerimDecimal.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblPerimDecimal.Location = new System.Drawing.Point(164, 18);
            this.lblPerimDecimal.Name = "lblPerimDecimal";
            this.lblPerimDecimal.Size = new System.Drawing.Size(85, 28);
            this.lblPerimDecimal.TabIndex = 9;
            this.lblPerimDecimal.Text = "0";
            this.lblPerimDecimal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPerimPoint
            // 
            this.lblPerimPoint.BackColor = System.Drawing.Color.Black;
            this.lblPerimPoint.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblPerimPoint.Location = new System.Drawing.Point(173, 34);
            this.lblPerimPoint.Name = "lblPerimPoint";
            this.lblPerimPoint.Size = new System.Drawing.Size(1, 1);
            this.lblPerimPoint.TabIndex = 11;
            this.lblPerimPoint.Text = ".";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblLength.Location = new System.Drawing.Point(124, 22);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(35, 21);
            this.lblLength.TabIndex = 15;
            this.lblLength.Text = "Len";
            // 
            // UCLineFinishSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblLength);
            this.Controls.Add(this.lblPerimPoint);
            this.Controls.Add(this.lblPerimDecimal);
            this.Controls.Add(this.lblLineName);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UCLineFinishSummary";
            this.Size = new System.Drawing.Size(252, 88);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLineName;
        private System.Windows.Forms.Label lblPerimDecimal;
        private System.Windows.Forms.Label lblPerimPoint;
        private System.Windows.Forms.Label lblLength;
    }
}
