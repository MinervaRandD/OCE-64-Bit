//-------------------------------------------------------------------------------//
// <copyright file="UCOversUndersFormElement.Designer.cs"                        //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
//-------------------------------------------------------------------------------//

namespace OversUndersForm
{
    partial class UCUndrsOversUndersFormElement
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
            this.lblNmbr = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.txbLnth = new System.Windows.Forms.TextBox();
            this.txbWdth = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblNmbr
            // 
            this.lblNmbr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNmbr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNmbr.Location = new System.Drawing.Point(0, 0);
            this.lblNmbr.Name = "lblNmbr";
            this.lblNmbr.Size = new System.Drawing.Size(32, 24);
            this.lblNmbr.TabIndex = 0;
            this.lblNmbr.Text = "100";
            this.lblNmbr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblX
            // 
            this.lblX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblX.Location = new System.Drawing.Point(86, 0);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(24, 24);
            this.lblX.TabIndex = 2;
            this.lblX.Text = "X";
            this.lblX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbLnth
            // 
            this.txbLnth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbLnth.Location = new System.Drawing.Point(108, 0);
            this.txbLnth.MinimumSize = new System.Drawing.Size(54, 24);
            this.txbLnth.Multiline = true;
            this.txbLnth.Name = "txbLnth";
            this.txbLnth.Size = new System.Drawing.Size(55, 24);
            this.txbLnth.TabIndex = 3;
            this.txbLnth.TabStop = false;
            this.txbLnth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbWdth
            // 
            this.txbWdth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbWdth.Location = new System.Drawing.Point(31, 0);
            this.txbWdth.MinimumSize = new System.Drawing.Size(54, 24);
            this.txbWdth.Multiline = true;
            this.txbWdth.Name = "txbWdth";
            this.txbWdth.Size = new System.Drawing.Size(56, 24);
            this.txbWdth.TabIndex = 4;
            this.txbWdth.TabStop = false;
            this.txbWdth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // UCUndrsOversUndersFormElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txbWdth);
            this.Controls.Add(this.txbLnth);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.lblNmbr);
            this.MaximumSize = new System.Drawing.Size(165, 24);
            this.MinimumSize = new System.Drawing.Size(165, 24);
            this.Name = "UCUndrsOversUndersFormElement";
            this.Size = new System.Drawing.Size(165, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNmbr;
        private System.Windows.Forms.Label lblX;
        public System.Windows.Forms.TextBox txbLnth;
        public System.Windows.Forms.TextBox txbWdth;
    }
}
