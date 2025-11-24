//-------------------------------------------------------------------------------//
// <copyright file="UCLinePallet.Designer.cs" company="Bruun Estimating, LLC">   // 
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
    partial class UCLineFinishPalette
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
            this.pnlLineFinish = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlLineFinish
            // 
            this.pnlLineFinish.BackColor = System.Drawing.SystemColors.Window;
            this.pnlLineFinish.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLineFinish.Location = new System.Drawing.Point(0, 421);
            this.pnlLineFinish.Name = "pnlLineFinish";
            this.pnlLineFinish.Size = new System.Drawing.Size(178, 128);
            this.pnlLineFinish.TabIndex = 2;
            // 
            // UCLineFinishPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLineFinish);
            this.Name = "UCLineFinishPalette";
            this.Size = new System.Drawing.Size(178, 549);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLineFinish;
    }
}
