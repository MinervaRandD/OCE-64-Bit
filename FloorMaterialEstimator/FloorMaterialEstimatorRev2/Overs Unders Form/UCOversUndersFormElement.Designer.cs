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

namespace FloorMaterialEstimator.OversUndersForm
{
    partial class UCOversUndersFormElement
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
            this.lblWdth = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblLnth = new System.Windows.Forms.Label();
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
            // lblWdth
            // 
            this.lblWdth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWdth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWdth.Location = new System.Drawing.Point(32, 0);
            this.lblWdth.Name = "lblWdth";
            this.lblWdth.Size = new System.Drawing.Size(56, 24);
            this.lblWdth.TabIndex = 1;
            this.lblWdth.Text = "100-12";
            this.lblWdth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // lblLnth
            // 
            this.lblLnth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLnth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLnth.Location = new System.Drawing.Point(109, 0);
            this.lblLnth.Name = "lblLnth";
            this.lblLnth.Size = new System.Drawing.Size(56, 24);
            this.lblLnth.TabIndex = 3;
            this.lblLnth.Text = "100-12";
            this.lblLnth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCOversUndersFormElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLnth);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.lblWdth);
            this.Controls.Add(this.lblNmbr);
            this.MaximumSize = new System.Drawing.Size(165, 24);
            this.MinimumSize = new System.Drawing.Size(165, 24);
            this.Name = "UCOversUndersFormElement";
            this.Size = new System.Drawing.Size(165, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNmbr;
        private System.Windows.Forms.Label lblWdth;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblLnth;
    }
}
