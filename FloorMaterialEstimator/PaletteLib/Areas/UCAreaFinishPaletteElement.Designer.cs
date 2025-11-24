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

namespace PaletteLib
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
            this.lblNetAreaValue = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlColor = new System.Windows.Forms.Panel();
            this.trbTransparency = new System.Windows.Forms.TrackBar();
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
            this.lblGrossAreaTitle = new System.Windows.Forms.Label();
            this.lblGrossAreaValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trbTransparency)).BeginInit();
            this.pnlTileTrimFactor.SuspendLayout();
            this.pnlRollSqrYrds.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWasteTitle
            // 
            this.lblWasteTitle.AutoSize = true;
            this.lblWasteTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWasteTitle.Location = new System.Drawing.Point(135, 73);
            this.lblWasteTitle.Name = "lblWasteTitle";
            this.lblWasteTitle.Size = new System.Drawing.Size(41, 13);
            this.lblWasteTitle.TabIndex = 17;
            this.lblWasteTitle.Text = "Waste:";
            this.lblWasteTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNetAreaTitle
            // 
            this.lblNetAreaTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAreaTitle.Location = new System.Drawing.Point(73, 51);
            this.lblNetAreaTitle.Name = "lblNetAreaTitle";
            this.lblNetAreaTitle.Size = new System.Drawing.Size(76, 13);
            this.lblNetAreaTitle.TabIndex = 15;
            this.lblNetAreaTitle.Text = "Net Area:";
            this.lblNetAreaTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNetAreaValue
            // 
            this.lblNetAreaValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAreaValue.Location = new System.Drawing.Point(148, 48);
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
            this.pnlColor.Location = new System.Drawing.Point(4, 34);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(58, 31);
            this.pnlColor.TabIndex = 23;
            // 
            // trbTransparency
            // 
            this.trbTransparency.AutoSize = false;
            this.trbTransparency.Location = new System.Drawing.Point(-1, 72);
            this.trbTransparency.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.trbTransparency.Maximum = 100;
            this.trbTransparency.Name = "trbTransparency";
            this.trbTransparency.Size = new System.Drawing.Size(63, 25);
            this.trbTransparency.TabIndex = 24;
            this.trbTransparency.TickFrequency = 10;
            this.trbTransparency.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip1.SetToolTip(this.trbTransparency, "Transparency");
            // 
            // lblTrimFactor
            // 
            this.lblTrimFactor.AutoSize = true;
            this.lblTrimFactor.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblTrimFactor.Location = new System.Drawing.Point(4, 10);
            this.lblTrimFactor.Name = "lblTrimFactor";
            this.lblTrimFactor.Size = new System.Drawing.Size(30, 13);
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
            this.lblRollSqrYrds.Location = new System.Drawing.Point(6, 0);
            this.lblRollSqrYrds.Name = "lblRollSqrYrds";
            this.lblRollSqrYrds.Size = new System.Drawing.Size(131, 19);
            this.lblRollSqrYrds.TabIndex = 0;
            // 
            // lblWasteValue
            // 
            this.lblWasteValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWasteValue.Location = new System.Drawing.Point(176, 72);
            this.lblWasteValue.Name = "lblWasteValue";
            this.lblWasteValue.Size = new System.Drawing.Size(50, 14);
            this.lblWasteValue.TabIndex = 33;
            this.lblWasteValue.Text = "0.0%";
            this.lblWasteValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlTileTrimFactor
            // 
            this.pnlTileTrimFactor.Controls.Add(this.lblTrimFactor);
            this.pnlTileTrimFactor.Controls.Add(this.lblTileTrimFactor);
            this.pnlTileTrimFactor.Location = new System.Drawing.Point(14, 99);
            this.pnlTileTrimFactor.Name = "pnlTileTrimFactor";
            this.pnlTileTrimFactor.Size = new System.Drawing.Size(221, 37);
            this.pnlTileTrimFactor.TabIndex = 34;
            // 
            // pnlRollSqrYrds
            // 
            this.pnlRollSqrYrds.Controls.Add(this.lblRollSqrYrds);
            this.pnlRollSqrYrds.Location = new System.Drawing.Point(48, 106);
            this.pnlRollSqrYrds.Name = "pnlRollSqrYrds";
            this.pnlRollSqrYrds.Size = new System.Drawing.Size(137, 27);
            this.pnlRollSqrYrds.TabIndex = 35;
            // 
            // lblCountTitle
            // 
            this.lblCountTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountTitle.Location = new System.Drawing.Point(73, 72);
            this.lblCountTitle.Name = "lblCountTitle";
            this.lblCountTitle.Size = new System.Drawing.Size(44, 14);
            this.lblCountTitle.TabIndex = 36;
            this.lblCountTitle.Text = "Count:";
            this.lblCountTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCountValue
            // 
            this.lblCountValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountValue.Location = new System.Drawing.Point(107, 73);
            this.lblCountValue.Name = "lblCountValue";
            this.lblCountValue.Size = new System.Drawing.Size(24, 13);
            this.lblCountValue.TabIndex = 37;
            this.lblCountValue.Text = "0";
            this.lblCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGrossAreaTitle
            // 
            this.lblGrossAreaTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrossAreaTitle.Location = new System.Drawing.Point(73, 29);
            this.lblGrossAreaTitle.Name = "lblGrossAreaTitle";
            this.lblGrossAreaTitle.Size = new System.Drawing.Size(76, 13);
            this.lblGrossAreaTitle.TabIndex = 38;
            this.lblGrossAreaTitle.Text = "Gross Area:";
            this.lblGrossAreaTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGrossAreaValue
            // 
            this.lblGrossAreaValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrossAreaValue.Location = new System.Drawing.Point(148, 29);
            this.lblGrossAreaValue.Name = "lblGrossAreaValue";
            this.lblGrossAreaValue.Size = new System.Drawing.Size(86, 16);
            this.lblGrossAreaValue.TabIndex = 39;
            this.lblGrossAreaValue.Text = "0.0";
            this.lblGrossAreaValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCAreaFinishPaletteElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblWasteTitle);
            this.Controls.Add(this.lblWasteValue);
            this.Controls.Add(this.lblGrossAreaValue);
            this.Controls.Add(this.lblGrossAreaTitle);
            this.Controls.Add(this.lblCountValue);
            this.Controls.Add(this.lblCountTitle);
            this.Controls.Add(this.pnlRollSqrYrds);
            this.Controls.Add(this.pnlTileTrimFactor);
            this.Controls.Add(this.lblShortCut);
            this.Controls.Add(this.trbTransparency);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblNetAreaValue);
            this.Controls.Add(this.lblNetAreaTitle);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.MaximumSize = new System.Drawing.Size(240, 190);
            this.Name = "UCAreaFinishPaletteElement";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Size = new System.Drawing.Size(238, 178);
            ((System.ComponentModel.ISupportInitialize)(this.trbTransparency)).EndInit();
            this.pnlTileTrimFactor.ResumeLayout(false);
            this.pnlTileTrimFactor.PerformLayout();
            this.pnlRollSqrYrds.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWasteTitle;
        private System.Windows.Forms.Label lblNetAreaTitle;
        private System.Windows.Forms.Label lblNetAreaValue;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblTrimFactor;
        public System.Windows.Forms.TrackBar trbTransparency;
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
        private System.Windows.Forms.Label lblGrossAreaTitle;
        private System.Windows.Forms.Label lblGrossAreaValue;
    }
}
