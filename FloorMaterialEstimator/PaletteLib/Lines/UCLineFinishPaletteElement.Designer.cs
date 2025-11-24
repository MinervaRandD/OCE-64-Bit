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
    partial class UCLineFinishPaletteElement
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
            this.lblTallyDisplay = new System.Windows.Forms.Label();
            this.lblPerimPoint = new System.Windows.Forms.Label();
            this.lblSelected = new System.Windows.Forms.Label();
            this.lblShortCutNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLineName
            // 
            this.lblLineName.BackColor = System.Drawing.Color.White;
            this.lblLineName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineName.Location = new System.Drawing.Point(26, 3);
            this.lblLineName.Name = "lblLineName";
            this.lblLineName.Size = new System.Drawing.Size(204, 21);
            this.lblLineName.TabIndex = 0;
            this.lblLineName.Text = "Line";
            this.lblLineName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTallyDisplay
            // 
            this.lblTallyDisplay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTallyDisplay.Location = new System.Drawing.Point(12, 48);
            this.lblTallyDisplay.Name = "lblTallyDisplay";
            this.lblTallyDisplay.Size = new System.Drawing.Size(174, 19);
            this.lblTallyDisplay.TabIndex = 9;
            this.lblTallyDisplay.Text = "0.0   l.f.";
            this.lblTallyDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.lblSelected.Location = new System.Drawing.Point(188, 26);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(21, 21);
            this.lblSelected.TabIndex = 12;
            this.lblSelected.Text = "<";
            this.lblSelected.Visible = false;
            // 
            // lblShortCutNumber
            // 
            this.lblShortCutNumber.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShortCutNumber.Location = new System.Drawing.Point(0, 3);
            this.lblShortCutNumber.Name = "lblShortCutNumber";
            this.lblShortCutNumber.Size = new System.Drawing.Size(21, 21);
            this.lblShortCutNumber.TabIndex = 13;
            this.lblShortCutNumber.Text = "0";
            this.lblShortCutNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCLineFinishPaletteElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblShortCutNumber);
            this.Controls.Add(this.lblSelected);
            this.Controls.Add(this.lblPerimPoint);
            this.Controls.Add(this.lblLineName);
            this.Controls.Add(this.lblTallyDisplay);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.Name = "UCLineFinishPaletteElement";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Size = new System.Drawing.Size(236, 110);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLineName;
        private System.Windows.Forms.Label lblTallyDisplay;
        private System.Windows.Forms.Label lblPerimPoint;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Label lblShortCutNumber;
    }
}
