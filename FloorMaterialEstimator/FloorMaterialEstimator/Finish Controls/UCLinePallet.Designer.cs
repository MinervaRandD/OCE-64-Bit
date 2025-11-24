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

namespace FloorMaterialEstimator.Finish_Controls
{
    partial class UCLinePallet
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
            this.pnlLineList = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlLineList
            // 
            this.pnlLineList.AutoScroll = true;
            this.pnlLineList.Location = new System.Drawing.Point(0, 0);
            this.pnlLineList.Name = "pnlLineList";
            this.pnlLineList.Size = new System.Drawing.Size(178, 400);
            this.pnlLineList.TabIndex = 0;
            // 
            // UCLinePallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLineList);
            this.Name = "UCLinePallet";
            this.Size = new System.Drawing.Size(178, 403);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLineList;
    }
}
