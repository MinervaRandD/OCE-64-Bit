//-------------------------------------------------------------------------------//
// <copyright file="AreaFilter.Designer.cs" company="Bruun Estimating, LLC">     // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Supporting_Forms
{
    partial class AreaFilter
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.clbAreaFinishes = new System.Windows.Forms.CheckedListBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnNone = new System.Windows.Forms.Button();
            this.pnlButtonPanel = new System.Windows.Forms.Panel();
            this.pnlButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter Areas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clbAreaFinishes
            // 
            this.clbAreaFinishes.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clbAreaFinishes.CheckOnClick = true;
            this.clbAreaFinishes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbAreaFinishes.FormattingEnabled = true;
            this.clbAreaFinishes.Location = new System.Drawing.Point(24, 55);
            this.clbAreaFinishes.Name = "clbAreaFinishes";
            this.clbAreaFinishes.Size = new System.Drawing.Size(177, 88);
            this.clbAreaFinishes.TabIndex = 2;
            this.clbAreaFinishes.ThreeDCheckBoxes = true;
            this.clbAreaFinishes.UseCompatibleTextRendering = true;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(15, 13);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(77, 23);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnNone
            // 
            this.btnNone.Location = new System.Drawing.Point(114, 13);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(75, 23);
            this.btnNone.TabIndex = 4;
            this.btnNone.Text = "None";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // pnlButtonPanel
            // 
            this.pnlButtonPanel.Controls.Add(this.btnSelectAll);
            this.pnlButtonPanel.Controls.Add(this.btnNone);
            this.pnlButtonPanel.Location = new System.Drawing.Point(12, 305);
            this.pnlButtonPanel.Name = "pnlButtonPanel";
            this.pnlButtonPanel.Size = new System.Drawing.Size(195, 49);
            this.pnlButtonPanel.TabIndex = 7;
            // 
            // AreaFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 515);
            this.Controls.Add(this.pnlButtonPanel);
            this.Controls.Add(this.clbAreaFinishes);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AreaFilter";
            this.pnlButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbAreaFinishes;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Panel pnlButtonPanel;
    }
}