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
    partial class UCAreaFinishPaletteElement
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
            this.components = new System.ComponentModel.Container();
            this.lblWasteTitle = new System.Windows.Forms.Label();
            this.lblNetAreaTitle = new System.Windows.Forms.Label();
            this.lblPerimTitle = new System.Windows.Forms.Label();
            this.lblPerimValue = new System.Windows.Forms.Label();
            this.lblNetAreaValue = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlColor = new System.Windows.Forms.Panel();
            this.trbOpacity = new System.Windows.Forms.TrackBar();
            this.lblTrimFactor = new System.Windows.Forms.Label();
            this.lblShortCut = new System.Windows.Forms.Label();
            this.lblTileTrimFactor = new System.Windows.Forms.Label();
            this.lblRollSqrYrds = new System.Windows.Forms.Label();
            this.lblWasteValue = new System.Windows.Forms.Label();
            this.pnlTileTrimFactor = new System.Windows.Forms.Panel();
            this.pnlRollSqrYrds = new System.Windows.Forms.Panel();
            this.lblCountTitle = new System.Windows.Forms.Label();
            this.lblCountValue = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trbOpacity)).BeginInit();
            this.pnlTileTrimFactor.SuspendLayout();
            this.pnlRollSqrYrds.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWasteTitle
            // 
            this.lblWasteTitle.AutoSize = true;
            this.lblWasteTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWasteTitle.Location = new System.Drawing.Point(81, 9);
            this.lblWasteTitle.Name = "lblWasteTitle";
            this.lblWasteTitle.Size = new System.Drawing.Size(42, 13);
            this.lblWasteTitle.TabIndex = 17;
            this.lblWasteTitle.Text = "Waste:";
            this.lblWasteTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNetAreaTitle
            // 
            this.lblNetAreaTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAreaTitle.Location = new System.Drawing.Point(72, 25);
            this.lblNetAreaTitle.Name = "lblNetAreaTitle";
            this.lblNetAreaTitle.Size = new System.Drawing.Size(76, 13);
            this.lblNetAreaTitle.TabIndex = 15;
            this.lblNetAreaTitle.Text = "Net Area:";
            this.lblNetAreaTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPerimTitle
            // 
            this.lblPerimTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerimTitle.Location = new System.Drawing.Point(72, 52);
            this.lblPerimTitle.Name = "lblPerimTitle";
            this.lblPerimTitle.Size = new System.Drawing.Size(68, 13);
            this.lblPerimTitle.TabIndex = 16;
            this.lblPerimTitle.Text = "Perimeter:";
            this.lblPerimTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPerimValue
            // 
            this.lblPerimValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerimValue.Location = new System.Drawing.Point(150, 52);
            this.lblPerimValue.Name = "lblPerimValue";
            this.lblPerimValue.Size = new System.Drawing.Size(86, 16);
            this.lblPerimValue.TabIndex = 21;
            this.lblPerimValue.Text = "0.0";
            this.lblPerimValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNetAreaValue
            // 
            this.lblNetAreaValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAreaValue.Location = new System.Drawing.Point(150, 25);
            this.lblNetAreaValue.Name = "lblNetAreaValue";
            this.lblNetAreaValue.Size = new System.Drawing.Size(86, 16);
            this.lblNetAreaValue.TabIndex = 19;
            this.lblNetAreaValue.Text = "0.0";
            this.lblNetAreaValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // trbOpacity
            // 
            this.trbOpacity.AutoSize = false;
            this.trbOpacity.Location = new System.Drawing.Point(-1, 67);
            this.trbOpacity.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.trbOpacity.Maximum = 100;
            this.trbOpacity.Name = "trbOpacity";
            this.trbOpacity.Size = new System.Drawing.Size(72, 36);
            this.trbOpacity.TabIndex = 24;
            this.toolTip1.SetToolTip(this.trbOpacity, "Opacity");
            this.trbOpacity.Value = 100;
            // 
            // lblTrimFactor
            // 
            this.lblTrimFactor.AutoSize = true;
            this.lblTrimFactor.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblTrimFactor.Location = new System.Drawing.Point(4, 9);
            this.lblTrimFactor.Name = "lblTrimFactor";
            this.lblTrimFactor.Size = new System.Drawing.Size(31, 13);
            this.lblTrimFactor.TabIndex = 27;
            this.lblTrimFactor.Text = "Trim:";
            // 
            // lblShortCut
            // 
            this.lblShortCut.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShortCut.Location = new System.Drawing.Point(0, 3);
            this.lblShortCut.Name = "lblShortCut";
            this.lblShortCut.Size = new System.Drawing.Size(21, 21);
            this.lblShortCut.TabIndex = 30;
            this.lblShortCut.Text = "0";
            this.lblShortCut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTileTrimFactor
            // 
            this.lblTileTrimFactor.AutoSize = true;
            this.lblTileTrimFactor.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTileTrimFactor.Location = new System.Drawing.Point(35, 9);
            this.lblTileTrimFactor.Name = "lblTileTrimFactor";
            this.lblTileTrimFactor.Size = new System.Drawing.Size(17, 13);
            this.lblTileTrimFactor.TabIndex = 28;
            this.lblTileTrimFactor.Text = "4\"";
            this.lblTileTrimFactor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRollSqrYrds
            // 
            this.lblRollSqrYrds.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRollSqrYrds.Location = new System.Drawing.Point(6, 11);
            this.lblRollSqrYrds.Name = "lblRollSqrYrds";
            this.lblRollSqrYrds.Size = new System.Drawing.Size(131, 19);
            this.lblRollSqrYrds.TabIndex = 0;
            this.lblRollSqrYrds.Text = "12\' x 100\' = 400.00 S.Y.";
            // 
            // lblWasteValue
            // 
            this.lblWasteValue.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWasteValue.Location = new System.Drawing.Point(136, 9);
            this.lblWasteValue.Name = "lblWasteValue";
            this.lblWasteValue.Size = new System.Drawing.Size(53, 13);
            this.lblWasteValue.TabIndex = 33;
            this.lblWasteValue.Text = "0%";
            this.lblWasteValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlTileTrimFactor
            // 
            this.pnlTileTrimFactor.Controls.Add(this.lblWasteValue);
            this.pnlTileTrimFactor.Controls.Add(this.lblWasteTitle);
            this.pnlTileTrimFactor.Controls.Add(this.lblTrimFactor);
            this.pnlTileTrimFactor.Controls.Add(this.lblTileTrimFactor);
            this.pnlTileTrimFactor.Location = new System.Drawing.Point(14, 108);
            this.pnlTileTrimFactor.Name = "pnlTileTrimFactor";
            this.pnlTileTrimFactor.Size = new System.Drawing.Size(221, 31);
            this.pnlTileTrimFactor.TabIndex = 34;
            // 
            // pnlRollSqrYrds
            // 
            this.pnlRollSqrYrds.Controls.Add(this.lblRollSqrYrds);
            this.pnlRollSqrYrds.Location = new System.Drawing.Point(90, 106);
            this.pnlRollSqrYrds.Name = "pnlRollSqrYrds";
            this.pnlRollSqrYrds.Size = new System.Drawing.Size(137, 37);
            this.pnlRollSqrYrds.TabIndex = 35;
            // 
            // lblCountTitle
            // 
            this.lblCountTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountTitle.Location = new System.Drawing.Point(72, 79);
            this.lblCountTitle.Name = "lblCountTitle";
            this.lblCountTitle.Size = new System.Drawing.Size(60, 13);
            this.lblCountTitle.TabIndex = 36;
            this.lblCountTitle.Text = "Count:";
            this.lblCountTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCountValue
            // 
            this.lblCountValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountValue.Location = new System.Drawing.Point(156, 79);
            this.lblCountValue.Name = "lblCountValue";
            this.lblCountValue.Size = new System.Drawing.Size(60, 13);
            this.lblCountValue.TabIndex = 37;
            this.lblCountValue.Text = "0";
            this.lblCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCAreaFinishPaletteElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblCountValue);
            this.Controls.Add(this.lblCountTitle);
            this.Controls.Add(this.pnlRollSqrYrds);
            this.Controls.Add(this.pnlTileTrimFactor);
            this.Controls.Add(this.lblShortCut);
            this.Controls.Add(this.trbOpacity);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPerimValue);
            this.Controls.Add(this.lblNetAreaValue);
            this.Controls.Add(this.lblNetAreaTitle);
            this.Controls.Add(this.lblPerimTitle);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.Name = "UCAreaFinishPaletteElement";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Size = new System.Drawing.Size(239, 149);
            ((System.ComponentModel.ISupportInitialize)(this.trbOpacity)).EndInit();
            this.pnlTileTrimFactor.ResumeLayout(false);
            this.pnlTileTrimFactor.PerformLayout();
            this.pnlRollSqrYrds.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblWasteTitle;
        private System.Windows.Forms.Label lblNetAreaTitle;
        private System.Windows.Forms.Label lblPerimTitle;
        private System.Windows.Forms.Label lblPerimValue;
        private System.Windows.Forms.Label lblNetAreaValue;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblTrimFactor;
        public System.Windows.Forms.TrackBar trbOpacity;
        private System.Windows.Forms.Label lblShortCut;
        private System.Windows.Forms.Label lblRollSqrYrds;
        public System.Windows.Forms.Label lblTileTrimFactor;
        public System.Windows.Forms.Label lblWasteValue;
        public System.Windows.Forms.Panel pnlColor;
        public System.Windows.Forms.Panel pnlTileTrimFactor;
        private System.Windows.Forms.Panel pnlRollSqrYrds;
        private System.Windows.Forms.Label lblCountTitle;
        private System.Windows.Forms.Label lblCountValue;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
