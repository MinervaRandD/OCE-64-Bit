//-------------------------------------------------------------------------------//
// <copyright file="OversUndersForm.Designer.cs"                                 //
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

namespace MaterialsLayout
{
    partial class OversUndersForm
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
            this.pnlCuts = new System.Windows.Forms.Panel();
            this.pnlUndrs = new System.Windows.Forms.Panel();
            this.pnlOvers = new System.Windows.Forms.Panel();
            this.lblCuts = new System.Windows.Forms.Label();
            this.lblUnders = new System.Windows.Forms.Label();
            this.lblOvers = new System.Windows.Forms.Label();
            this.btnDoFills = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ckbDoFillsManual = new System.Windows.Forms.CheckBox();
            this.lblFill = new System.Windows.Forms.Label();
            this.lblFillWidth = new System.Windows.Forms.Label();
            this.lblFillHeight = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCutsArea = new System.Windows.Forms.Label();
            this.lblNetArea = new System.Windows.Forms.Label();
            this.lblFillPct = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlCuts
            // 
            this.pnlCuts.AutoScroll = true;
            this.pnlCuts.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlCuts.Location = new System.Drawing.Point(22, 49);
            this.pnlCuts.Name = "pnlCuts";
            this.pnlCuts.Size = new System.Drawing.Size(192, 529);
            this.pnlCuts.TabIndex = 15;
            // 
            // pnlUndrs
            // 
            this.pnlUndrs.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlUndrs.Location = new System.Drawing.Point(251, 49);
            this.pnlUndrs.Name = "pnlUndrs";
            this.pnlUndrs.Size = new System.Drawing.Size(192, 529);
            this.pnlUndrs.TabIndex = 16;
            // 
            // pnlOvers
            // 
            this.pnlOvers.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlOvers.Location = new System.Drawing.Point(482, 49);
            this.pnlOvers.Name = "pnlOvers";
            this.pnlOvers.Size = new System.Drawing.Size(192, 529);
            this.pnlOvers.TabIndex = 17;
            // 
            // lblCuts
            // 
            this.lblCuts.AutoSize = true;
            this.lblCuts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuts.Location = new System.Drawing.Point(26, 9);
            this.lblCuts.Name = "lblCuts";
            this.lblCuts.Size = new System.Drawing.Size(42, 20);
            this.lblCuts.TabIndex = 18;
            this.lblCuts.Text = "Cuts";
            // 
            // lblUnders
            // 
            this.lblUnders.AutoSize = true;
            this.lblUnders.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnders.Location = new System.Drawing.Point(255, 9);
            this.lblUnders.Name = "lblUnders";
            this.lblUnders.Size = new System.Drawing.Size(61, 20);
            this.lblUnders.TabIndex = 19;
            this.lblUnders.Text = "Unders";
            // 
            // lblOvers
            // 
            this.lblOvers.AutoSize = true;
            this.lblOvers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOvers.Location = new System.Drawing.Point(485, 9);
            this.lblOvers.Name = "lblOvers";
            this.lblOvers.Size = new System.Drawing.Size(50, 20);
            this.lblOvers.TabIndex = 20;
            this.lblOvers.Text = "Overs";
            // 
            // btnDoFills
            // 
            this.btnDoFills.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoFills.Location = new System.Drawing.Point(24, 617);
            this.btnDoFills.Name = "btnDoFills";
            this.btnDoFills.Size = new System.Drawing.Size(96, 32);
            this.btnDoFills.TabIndex = 21;
            this.btnDoFills.Text = "Do Fills";
            this.btnDoFills.UseVisualStyleBackColor = true;
            this.btnDoFills.Click += new System.EventHandler(this.btnDoFills_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(486, 618);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Cuts Area:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(486, 652);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 23;
            this.label2.Text = "Net Area:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(486, 689);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "Fill Percent:";
            // 
            // ckbDoFillsManual
            // 
            this.ckbDoFillsManual.AutoSize = true;
            this.ckbDoFillsManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbDoFillsManual.Location = new System.Drawing.Point(24, 669);
            this.ckbDoFillsManual.Name = "ckbDoFillsManual";
            this.ckbDoFillsManual.Size = new System.Drawing.Size(146, 24);
            this.ckbDoFillsManual.TabIndex = 25;
            this.ckbDoFillsManual.Text = "Do Fills Manually";
            this.ckbDoFillsManual.UseVisualStyleBackColor = true;
            // 
            // lblFill
            // 
            this.lblFill.AutoSize = true;
            this.lblFill.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFill.Location = new System.Drawing.Point(255, 617);
            this.lblFill.Name = "lblFill";
            this.lblFill.Size = new System.Drawing.Size(28, 20);
            this.lblFill.TabIndex = 27;
            this.lblFill.Text = "Fill";
            // 
            // lblFillWidth
            // 
            this.lblFillWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFillWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFillWidth.Location = new System.Drawing.Point(255, 652);
            this.lblFillWidth.Name = "lblFillWidth";
            this.lblFillWidth.Size = new System.Drawing.Size(55, 23);
            this.lblFillWidth.TabIndex = 28;
            this.lblFillWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFillHeight
            // 
            this.lblFillHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFillHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFillHeight.Location = new System.Drawing.Point(342, 652);
            this.lblFillHeight.Name = "lblFillHeight";
            this.lblFillHeight.Size = new System.Drawing.Size(53, 23);
            this.lblFillHeight.TabIndex = 29;
            this.lblFillHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(316, 653);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 20);
            this.label6.TabIndex = 30;
            this.label6.Text = "X";
            // 
            // lblCutsArea
            // 
            this.lblCutsArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCutsArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCutsArea.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCutsArea.Location = new System.Drawing.Point(607, 617);
            this.lblCutsArea.Name = "lblCutsArea";
            this.lblCutsArea.Size = new System.Drawing.Size(67, 23);
            this.lblCutsArea.TabIndex = 31;
            // 
            // lblNetArea
            // 
            this.lblNetArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNetArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetArea.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNetArea.Location = new System.Drawing.Point(607, 652);
            this.lblNetArea.Name = "lblNetArea";
            this.lblNetArea.Size = new System.Drawing.Size(67, 23);
            this.lblNetArea.TabIndex = 32;
            // 
            // lblFillPct
            // 
            this.lblFillPct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFillPct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFillPct.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFillPct.Location = new System.Drawing.Point(607, 689);
            this.lblFillPct.Name = "lblFillPct";
            this.lblFillPct.Size = new System.Drawing.Size(67, 23);
            this.lblFillPct.TabIndex = 33;
            // 
            // OversUndersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 719);
            this.Controls.Add(this.lblFillPct);
            this.Controls.Add(this.lblNetArea);
            this.Controls.Add(this.lblCutsArea);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblFillHeight);
            this.Controls.Add(this.lblFillWidth);
            this.Controls.Add(this.lblFill);
            this.Controls.Add(this.ckbDoFillsManual);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDoFills);
            this.Controls.Add(this.lblOvers);
            this.Controls.Add(this.lblUnders);
            this.Controls.Add(this.lblCuts);
            this.Controls.Add(this.pnlOvers);
            this.Controls.Add(this.pnlUndrs);
            this.Controls.Add(this.pnlCuts);
            this.Name = "OversUndersForm";
            this.Text = "Cuts, Unders and Overs";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlCuts;
        private System.Windows.Forms.Panel pnlUndrs;
        private System.Windows.Forms.Panel pnlOvers;
        private System.Windows.Forms.Label lblCuts;
        private System.Windows.Forms.Label lblUnders;
        private System.Windows.Forms.Label lblOvers;
        private System.Windows.Forms.Button btnDoFills;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckbDoFillsManual;
        private System.Windows.Forms.Label lblFill;
        private System.Windows.Forms.Label lblFillWidth;
        private System.Windows.Forms.Label lblFillHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCutsArea;
        private System.Windows.Forms.Label lblNetArea;
        private System.Windows.Forms.Label lblFillPct;
    }
}