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
    partial class UCCutsOversUndersFormElement
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
            this.txbLnth = new System.Windows.Forms.TextBox();
            this.txbRepeats = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblNmbr
            // 
            this.lblNmbr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNmbr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNmbr.Location = new System.Drawing.Point(1, 0);
            this.lblNmbr.Name = "lblNmbr";
            this.lblNmbr.Size = new System.Drawing.Size(41, 24);
            this.lblNmbr.TabIndex = 0;
            this.lblNmbr.Text = "Extra";
            this.lblNmbr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWdth
            // 
            this.lblWdth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWdth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWdth.Location = new System.Drawing.Point(42, 0);
            this.lblWdth.Name = "lblWdth";
            this.lblWdth.Size = new System.Drawing.Size(46, 24);
            this.lblWdth.TabIndex = 1;
            this.lblWdth.Text = "100-12";
            this.lblWdth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblX
            // 
            this.lblX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblX.Location = new System.Drawing.Point(94, 0);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(17, 24);
            this.lblX.TabIndex = 2;
            this.lblX.Text = "X";
            this.lblX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbLnth
            // 
            this.txbLnth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbLnth.Location = new System.Drawing.Point(110, 0);
            this.txbLnth.MinimumSize = new System.Drawing.Size(42, 24);
            this.txbLnth.Multiline = true;
            this.txbLnth.Name = "txbLnth";
            this.txbLnth.Size = new System.Drawing.Size(42, 24);
            this.txbLnth.TabIndex = 3;
            this.txbLnth.TabStop = false;
            this.txbLnth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbRepeats
            // 
            this.txbRepeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbRepeats.Location = new System.Drawing.Point(178, 0);
            this.txbRepeats.MinimumSize = new System.Drawing.Size(20, 24);
            this.txbRepeats.Multiline = true;
            this.txbRepeats.Name = "txbRepeats";
            this.txbRepeats.Size = new System.Drawing.Size(22, 24);
            this.txbRepeats.TabIndex = 4;
            this.txbRepeats.TabStop = false;
            this.txbRepeats.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txbRepeats.TextChanged += new System.EventHandler(this.txbRepeats_TextChanged);
            // 
            // UCCutsOversUndersFormElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txbRepeats);
            this.Controls.Add(this.txbLnth);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.lblWdth);
            this.Controls.Add(this.lblNmbr);
            this.MaximumSize = new System.Drawing.Size(200, 24);
            this.MinimumSize = new System.Drawing.Size(172, 24);
            this.Name = "UCCutsOversUndersFormElement";
            this.Size = new System.Drawing.Size(200, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblWdth;
        private System.Windows.Forms.Label lblX;
        public System.Windows.Forms.TextBox txbLnth;
        private System.Windows.Forms.Label lblNmbr;
        public System.Windows.Forms.TextBox txbRepeats;
    }
}
