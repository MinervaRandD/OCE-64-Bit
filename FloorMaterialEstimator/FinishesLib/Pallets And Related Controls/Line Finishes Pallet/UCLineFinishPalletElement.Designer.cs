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

namespace FloorMaterialEstimator.Finish_Controls
{
    partial class UCLineFinishPalletElement
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
            this.lblSelected = new System.Windows.Forms.Label();
            this.lblShortCutNumber = new System.Windows.Forms.Label();
            this.lblGuid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLineName
            // 
            this.lblLineName.AutoSize = true;
            this.lblLineName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineName.Location = new System.Drawing.Point(8, 20);
            this.lblLineName.Name = "lblLineName";
            this.lblLineName.Size = new System.Drawing.Size(39, 21);
            this.lblLineName.TabIndex = 0;
            this.lblLineName.Text = "Line";
            // 
            // lblPerimDecimal
            // 
            this.lblPerimDecimal.AutoSize = true;
            this.lblPerimDecimal.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblPerimDecimal.Location = new System.Drawing.Point(173, 30);
            this.lblPerimDecimal.Name = "lblPerimDecimal";
            this.lblPerimDecimal.Size = new System.Drawing.Size(13, 13);
            this.lblPerimDecimal.TabIndex = 9;
            this.lblPerimDecimal.Text = "0";
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
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelected.Location = new System.Drawing.Point(188, 44);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(21, 21);
            this.lblSelected.TabIndex = 12;
            this.lblSelected.Text = "<";
            this.lblSelected.Visible = false;
            // 
            // lblShortCutNumber
            // 
            this.lblShortCutNumber.AutoSize = true;
            this.lblShortCutNumber.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShortCutNumber.Location = new System.Drawing.Point(3, 1);
            this.lblShortCutNumber.Name = "lblShortCutNumber";
            this.lblShortCutNumber.Size = new System.Drawing.Size(17, 20);
            this.lblShortCutNumber.TabIndex = 13;
            this.lblShortCutNumber.Text = "0";
            this.lblShortCutNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGuid
            // 
            this.lblGuid.AutoSize = true;
            this.lblGuid.Location = new System.Drawing.Point(12, 82);
            this.lblGuid.Name = "lblGuid";
            this.lblGuid.Size = new System.Drawing.Size(31, 13);
            this.lblGuid.TabIndex = 14;
            this.lblGuid.Text = "guid";
            // 
            // UCLineFinishPalletElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.lblGuid);
            this.Controls.Add(this.lblShortCutNumber);
            this.Controls.Add(this.lblSelected);
            this.Controls.Add(this.lblPerimPoint);
            this.Controls.Add(this.lblPerimDecimal);
            this.Controls.Add(this.lblLineName);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UCLineFinishPalletElement";
            this.Size = new System.Drawing.Size(245, 98);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLineName;
        private System.Windows.Forms.Label lblPerimDecimal;
        private System.Windows.Forms.Label lblPerimPoint;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Label lblShortCutNumber;
        private System.Windows.Forms.Label lblGuid;
    }
}
