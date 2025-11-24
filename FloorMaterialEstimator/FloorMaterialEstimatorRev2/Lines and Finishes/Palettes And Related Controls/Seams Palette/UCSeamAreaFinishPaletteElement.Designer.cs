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
    partial class UCSeamAreaFinishPaletteElement
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
            this.lblAreaText = new System.Windows.Forms.Label();
            this.lblPerimText = new System.Windows.Forms.Label();
            this.lblPerimValue = new System.Windows.Forms.Label();
            this.lblAreaValue = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlColor = new System.Windows.Forms.Panel();
            this.lblShortCut = new System.Windows.Forms.Label();
            this.lblSeamValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlRollSqrYrds = new System.Windows.Forms.Panel();
            this.lblRollSqrYrds = new System.Windows.Forms.Label();
            this.pnlRollSqrYrds.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAreaText
            // 
            this.lblAreaText.AutoSize = true;
            this.lblAreaText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaText.Location = new System.Drawing.Point(76, 28);
            this.lblAreaText.Name = "lblAreaText";
            this.lblAreaText.Size = new System.Drawing.Size(54, 13);
            this.lblAreaText.TabIndex = 15;
            this.lblAreaText.Text = "Net Area:";
            this.lblAreaText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPerimText
            // 
            this.lblPerimText.AutoSize = true;
            this.lblPerimText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerimText.Location = new System.Drawing.Point(76, 49);
            this.lblPerimText.Name = "lblPerimText";
            this.lblPerimText.Size = new System.Drawing.Size(38, 13);
            this.lblPerimText.TabIndex = 16;
            this.lblPerimText.Text = "Perim:";
            this.lblPerimText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPerimValue
            // 
            this.lblPerimValue.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerimValue.Location = new System.Drawing.Point(151, 49);
            this.lblPerimValue.Name = "lblPerimValue";
            this.lblPerimValue.Size = new System.Drawing.Size(80, 13);
            this.lblPerimValue.TabIndex = 21;
            this.lblPerimValue.Text = "0.0";
            this.lblPerimValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAreaValue
            // 
            this.lblAreaValue.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaValue.Location = new System.Drawing.Point(151, 28);
            this.lblAreaValue.Name = "lblAreaValue";
            this.lblAreaValue.Size = new System.Drawing.Size(80, 13);
            this.lblAreaValue.TabIndex = 19;
            this.lblAreaValue.Text = "0.0";
            this.lblAreaValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(26, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(204, 21);
            this.lblName.TabIndex = 22;
            this.lblName.Text = "Finish";
            // 
            // pnlColor
            // 
            this.pnlColor.BackColor = System.Drawing.Color.LawnGreen;
            this.pnlColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColor.Location = new System.Drawing.Point(4, 28);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(58, 31);
            this.pnlColor.TabIndex = 23;
            // 
            // lblShortCut
            // 
            this.lblShortCut.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShortCut.Location = new System.Drawing.Point(0, 3);
            this.lblShortCut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.lblShortCut.Name = "lblShortCut";
            this.lblShortCut.Size = new System.Drawing.Size(21, 21);
            this.lblShortCut.TabIndex = 30;
            this.lblShortCut.Text = "0";
            this.lblShortCut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSeamValue
            // 
            this.lblSeamValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeamValue.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblSeamValue.Location = new System.Drawing.Point(75, 110);
            this.lblSeamValue.Name = "lblSeamValue";
            this.lblSeamValue.Size = new System.Drawing.Size(62, 15);
            this.lblSeamValue.TabIndex = 35;
            this.lblSeamValue.Text = "0";
            this.lblSeamValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Location = new System.Drawing.Point(9, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 34;
            this.label2.Text = "Seam:";
            // 
            // pnlRollSqrYrds
            // 
            this.pnlRollSqrYrds.Controls.Add(this.lblRollSqrYrds);
            this.pnlRollSqrYrds.Location = new System.Drawing.Point(9, 71);
            this.pnlRollSqrYrds.Name = "pnlRollSqrYrds";
            this.pnlRollSqrYrds.Size = new System.Drawing.Size(217, 28);
            this.pnlRollSqrYrds.TabIndex = 32;
            // 
            // lblRollSqrYrds
            // 
            this.lblRollSqrYrds.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRollSqrYrds.Location = new System.Drawing.Point(0, 5);
            this.lblRollSqrYrds.Name = "lblRollSqrYrds";
            this.lblRollSqrYrds.Size = new System.Drawing.Size(184, 19);
            this.lblRollSqrYrds.TabIndex = 0;
            this.lblRollSqrYrds.Text = "12\' x 100\' = 400.00 S.Y.";
            // 
            // UCSeamAreaFinishPaletteElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblSeamValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblShortCut);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPerimValue);
            this.Controls.Add(this.lblAreaValue);
            this.Controls.Add(this.lblAreaText);
            this.Controls.Add(this.lblPerimText);
            this.Controls.Add(this.pnlRollSqrYrds);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.Name = "UCSeamAreaFinishPaletteElement";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Size = new System.Drawing.Size(238, 149);
            this.pnlRollSqrYrds.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblAreaText;
        private System.Windows.Forms.Label lblPerimText;
        private System.Windows.Forms.Label lblPerimValue;
        private System.Windows.Forms.Label lblAreaValue;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.Label lblShortCut;
        public System.Windows.Forms.Label lblSeamValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlRollSqrYrds;
        private System.Windows.Forms.Label lblRollSqrYrds;
    }
}
