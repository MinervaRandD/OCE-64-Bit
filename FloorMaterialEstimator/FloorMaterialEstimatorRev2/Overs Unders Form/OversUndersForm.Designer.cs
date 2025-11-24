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

namespace FloorMaterialEstimator.OversUndersForm
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
            this.components = new System.ComponentModel.Container();
            this.pnlCuts = new System.Windows.Forms.Panel();
            this.pnlUndrs = new System.Windows.Forms.Panel();
            this.pnlOvers = new System.Windows.Forms.Panel();
            this.btnDoFills = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalNetArea = new System.Windows.Forms.Label();
            this.lblGrossArea = new System.Windows.Forms.Label();
            this.lblTotalLength = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbnUpdateFillsManually = new System.Windows.Forms.RadioButton();
            this.RbnUpdateFillsAutomatically = new System.Windows.Forms.RadioButton();
            this.grbStandardFills = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblLargeFillWastePercent = new System.Windows.Forms.Label();
            this.lblLargeFillNetArea = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblLargeAreaFillPiece = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSmallFillGrossArea = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txbSmallFillsWastePct = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSmallFillFillPiece = new System.Windows.Forms.Label();
            this.lblSmallFillNetArea = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotalWastePct = new System.Windows.Forms.Label();
            this.btnAddVirtualOver = new System.Windows.Forms.Button();
            this.btnAddVirtualUndr = new System.Windows.Forms.Button();
            this.btnResetOvers = new System.Windows.Forms.Button();
            this.btnDeleteSelectedOvers = new System.Windows.Forms.Button();
            this.btnDeleteSelectedUnders = new System.Windows.Forms.Button();
            this.grbUnders = new System.Windows.Forms.GroupBox();
            this.lblGrossAreaShortened = new System.Windows.Forms.Label();
            this.lblGrossAreaTextShortened = new System.Windows.Forms.Label();
            this.btnResetUnders = new System.Windows.Forms.Button();
            this.gbxOvers = new System.Windows.Forms.GroupBox();
            this.btnSwitchSize = new System.Windows.Forms.Button();
            this.grbCuts = new System.Windows.Forms.GroupBox();
            this.lblLengthRepeat = new System.Windows.Forms.Label();
            this.btnSwitchCutsSize = new System.Windows.Forms.Button();
            this.pnlCutsTotals = new System.Windows.Forms.Panel();
            this.btnResetCuts = new System.Windows.Forms.Button();
            this.btnDeleteSelectedCuts = new System.Windows.Forms.Button();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnExportReport = new System.Windows.Forms.Button();
            this.btnNestUndrs = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.grbStandardFills.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grbUnders.SuspendLayout();
            this.gbxOvers.SuspendLayout();
            this.grbCuts.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCuts
            // 
            this.pnlCuts.AutoScroll = true;
            this.pnlCuts.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlCuts.Location = new System.Drawing.Point(10, 29);
            this.pnlCuts.Name = "pnlCuts";
            this.pnlCuts.Size = new System.Drawing.Size(210, 405);
            this.pnlCuts.TabIndex = 15;
            // 
            // pnlUndrs
            // 
            this.pnlUndrs.AutoScroll = true;
            this.pnlUndrs.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlUndrs.Location = new System.Drawing.Point(17, 30);
            this.pnlUndrs.Name = "pnlUndrs";
            this.pnlUndrs.Size = new System.Drawing.Size(192, 404);
            this.pnlUndrs.TabIndex = 16;
            // 
            // pnlOvers
            // 
            this.pnlOvers.AutoScroll = true;
            this.pnlOvers.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlOvers.Location = new System.Drawing.Point(21, 30);
            this.pnlOvers.Name = "pnlOvers";
            this.pnlOvers.Size = new System.Drawing.Size(192, 404);
            this.pnlOvers.TabIndex = 17;
            // 
            // btnDoFills
            // 
            this.btnDoFills.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoFills.Location = new System.Drawing.Point(26, 101);
            this.btnDoFills.Name = "btnDoFills";
            this.btnDoFills.Size = new System.Drawing.Size(95, 23);
            this.btnDoFills.TabIndex = 21;
            this.btnDoFills.Text = "Update";
            this.btnDoFills.UseVisualStyleBackColor = true;
            this.btnDoFills.Click += new System.EventHandler(this.btnDoFills_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 18);
            this.label1.TabIndex = 22;
            this.label1.Text = "Net Area:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 18);
            this.label2.TabIndex = 23;
            this.label2.Text = "Gross Area:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 18);
            this.label3.TabIndex = 24;
            this.label3.Text = "Total:";
            // 
            // lblTotalNetArea
            // 
            this.lblTotalNetArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalNetArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalNetArea.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalNetArea.Location = new System.Drawing.Point(101, 25);
            this.lblTotalNetArea.Name = "lblTotalNetArea";
            this.lblTotalNetArea.Size = new System.Drawing.Size(66, 23);
            this.lblTotalNetArea.TabIndex = 31;
            this.lblTotalNetArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGrossArea
            // 
            this.lblGrossArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGrossArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrossArea.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGrossArea.Location = new System.Drawing.Point(101, 89);
            this.lblGrossArea.Name = "lblGrossArea";
            this.lblGrossArea.Size = new System.Drawing.Size(66, 23);
            this.lblGrossArea.TabIndex = 32;
            this.lblGrossArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalLength
            // 
            this.lblTotalLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLength.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalLength.Location = new System.Drawing.Point(101, 122);
            this.lblTotalLength.Name = "lblTotalLength";
            this.lblTotalLength.Size = new System.Drawing.Size(66, 23);
            this.lblTotalLength.TabIndex = 33;
            this.lblTotalLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.grbStandardFills);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(24, 732);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(749, 178);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbnUpdateFillsManually);
            this.groupBox6.Controls.Add(this.RbnUpdateFillsAutomatically);
            this.groupBox6.Controls.Add(this.btnDoFills);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(7, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(172, 156);
            this.groupBox6.TabIndex = 38;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Update Fills";
            // 
            // rbnUpdateFillsManually
            // 
            this.rbnUpdateFillsManually.AutoSize = true;
            this.rbnUpdateFillsManually.Location = new System.Drawing.Point(26, 57);
            this.rbnUpdateFillsManually.Name = "rbnUpdateFillsManually";
            this.rbnUpdateFillsManually.Size = new System.Drawing.Size(84, 22);
            this.rbnUpdateFillsManually.TabIndex = 23;
            this.rbnUpdateFillsManually.Text = "Manually";
            this.rbnUpdateFillsManually.UseVisualStyleBackColor = true;
            // 
            // RbnUpdateFillsAutomatically
            // 
            this.RbnUpdateFillsAutomatically.AutoSize = true;
            this.RbnUpdateFillsAutomatically.Checked = true;
            this.RbnUpdateFillsAutomatically.Location = new System.Drawing.Point(26, 29);
            this.RbnUpdateFillsAutomatically.Name = "RbnUpdateFillsAutomatically";
            this.RbnUpdateFillsAutomatically.Size = new System.Drawing.Size(113, 22);
            this.RbnUpdateFillsAutomatically.TabIndex = 22;
            this.RbnUpdateFillsAutomatically.TabStop = true;
            this.RbnUpdateFillsAutomatically.Text = "Automatically";
            this.RbnUpdateFillsAutomatically.UseVisualStyleBackColor = true;
            // 
            // grbStandardFills
            // 
            this.grbStandardFills.Controls.Add(this.label11);
            this.grbStandardFills.Controls.Add(this.label12);
            this.grbStandardFills.Controls.Add(this.lblLargeFillWastePercent);
            this.grbStandardFills.Controls.Add(this.lblLargeFillNetArea);
            this.grbStandardFills.Controls.Add(this.label15);
            this.grbStandardFills.Controls.Add(this.lblLargeAreaFillPiece);
            this.grbStandardFills.Enabled = false;
            this.grbStandardFills.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbStandardFills.Location = new System.Drawing.Point(372, 16);
            this.grbStandardFills.Name = "grbStandardFills";
            this.grbStandardFills.Size = new System.Drawing.Size(177, 156);
            this.grbStandardFills.TabIndex = 37;
            this.grbStandardFills.TabStop = false;
            this.grbStandardFills.Text = "Standard Fills";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 18);
            this.label11.TabIndex = 22;
            this.label11.Text = "Net Area:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 107);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 18);
            this.label12.TabIndex = 24;
            this.label12.Text = "Fill Waste:";
            // 
            // lblLargeFillWastePercent
            // 
            this.lblLargeFillWastePercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLargeFillWastePercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLargeFillWastePercent.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLargeFillWastePercent.Location = new System.Drawing.Point(102, 105);
            this.lblLargeFillWastePercent.Name = "lblLargeFillWastePercent";
            this.lblLargeFillWastePercent.Size = new System.Drawing.Size(66, 23);
            this.lblLargeFillWastePercent.TabIndex = 33;
            this.lblLargeFillWastePercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLargeFillNetArea
            // 
            this.lblLargeFillNetArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLargeFillNetArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLargeFillNetArea.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLargeFillNetArea.Location = new System.Drawing.Point(102, 30);
            this.lblLargeFillNetArea.Name = "lblLargeFillNetArea";
            this.lblLargeFillNetArea.Size = new System.Drawing.Size(66, 23);
            this.lblLargeFillNetArea.TabIndex = 31;
            this.lblLargeFillNetArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(6, 66);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 18);
            this.label15.TabIndex = 23;
            this.label15.Text = "Fill Piece:";
            // 
            // lblLargeAreaFillPiece
            // 
            this.lblLargeAreaFillPiece.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLargeAreaFillPiece.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLargeAreaFillPiece.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLargeAreaFillPiece.Location = new System.Drawing.Point(102, 64);
            this.lblLargeAreaFillPiece.Name = "lblLargeAreaFillPiece";
            this.lblLargeAreaFillPiece.Size = new System.Drawing.Size(66, 23);
            this.lblLargeAreaFillPiece.TabIndex = 32;
            this.lblLargeAreaFillPiece.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.lblSmallFillGrossArea);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txbSmallFillsWastePct);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lblSmallFillFillPiece);
            this.groupBox4.Controls.Add(this.lblSmallFillNetArea);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(188, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(177, 156);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Small Fills";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 18);
            this.label10.TabIndex = 46;
            this.label10.Text = "Gross Area:";
            // 
            // lblSmallFillGrossArea
            // 
            this.lblSmallFillGrossArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSmallFillGrossArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmallFillGrossArea.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSmallFillGrossArea.Location = new System.Drawing.Point(103, 89);
            this.lblSmallFillGrossArea.Name = "lblSmallFillGrossArea";
            this.lblSmallFillGrossArea.Size = new System.Drawing.Size(66, 23);
            this.lblSmallFillGrossArea.TabIndex = 47;
            this.lblSmallFillGrossArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 18);
            this.label6.TabIndex = 45;
            this.label6.Text = "Net Area:";
            // 
            // txbSmallFillsWastePct
            // 
            this.txbSmallFillsWastePct.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txbSmallFillsWastePct.Location = new System.Drawing.Point(122, 56);
            this.txbSmallFillsWastePct.Name = "txbSmallFillsWastePct";
            this.txbSmallFillsWastePct.Size = new System.Drawing.Size(44, 24);
            this.txbSmallFillsWastePct.TabIndex = 0;
            this.txbSmallFillsWastePct.TabStop = false;
            this.txbSmallFillsWastePct.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 18);
            this.label5.TabIndex = 24;
            this.label5.Text = "Fill Piece: *";
            // 
            // lblSmallFillFillPiece
            // 
            this.lblSmallFillFillPiece.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSmallFillFillPiece.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmallFillFillPiece.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSmallFillFillPiece.Location = new System.Drawing.Point(102, 122);
            this.lblSmallFillFillPiece.Name = "lblSmallFillFillPiece";
            this.lblSmallFillFillPiece.Size = new System.Drawing.Size(66, 23);
            this.lblSmallFillFillPiece.TabIndex = 33;
            this.lblSmallFillFillPiece.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSmallFillNetArea
            // 
            this.lblSmallFillNetArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSmallFillNetArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmallFillNetArea.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSmallFillNetArea.Location = new System.Drawing.Point(102, 25);
            this.lblSmallFillNetArea.Name = "lblSmallFillNetArea";
            this.lblSmallFillNetArea.Size = new System.Drawing.Size(66, 23);
            this.lblSmallFillNetArea.TabIndex = 31;
            this.lblSmallFillNetArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 18);
            this.label9.TabIndex = 23;
            this.label9.Text = "Waste %:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblTotalWastePct);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotalLength);
            this.groupBox2.Controls.Add(this.lblTotalNetArea);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblGrossArea);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(560, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(177, 157);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Totals";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 18);
            this.label7.TabIndex = 34;
            this.label7.Text = "Waste %:";
            // 
            // lblTotalWastePct
            // 
            this.lblTotalWastePct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalWastePct.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalWastePct.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalWastePct.Location = new System.Drawing.Point(101, 57);
            this.lblTotalWastePct.Name = "lblTotalWastePct";
            this.lblTotalWastePct.Size = new System.Drawing.Size(66, 23);
            this.lblTotalWastePct.TabIndex = 35;
            this.lblTotalWastePct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddVirtualOver
            // 
            this.btnAddVirtualOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddVirtualOver.Location = new System.Drawing.Point(41, 450);
            this.btnAddVirtualOver.Name = "btnAddVirtualOver";
            this.btnAddVirtualOver.Size = new System.Drawing.Size(152, 23);
            this.btnAddVirtualOver.TabIndex = 35;
            this.btnAddVirtualOver.Text = "+ Add Virtual Over";
            this.btnAddVirtualOver.UseVisualStyleBackColor = true;
            this.btnAddVirtualOver.Click += new System.EventHandler(this.btnAddVirtualOver_Click);
            // 
            // btnAddVirtualUndr
            // 
            this.btnAddVirtualUndr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddVirtualUndr.Location = new System.Drawing.Point(33, 450);
            this.btnAddVirtualUndr.Name = "btnAddVirtualUndr";
            this.btnAddVirtualUndr.Size = new System.Drawing.Size(152, 23);
            this.btnAddVirtualUndr.TabIndex = 36;
            this.btnAddVirtualUndr.Text = "+ Add Virtual Under";
            this.btnAddVirtualUndr.UseVisualStyleBackColor = true;
            this.btnAddVirtualUndr.Click += new System.EventHandler(this.btnAddVirtualUndr_Click);
            // 
            // btnResetOvers
            // 
            this.btnResetOvers.Location = new System.Drawing.Point(41, 517);
            this.btnResetOvers.Name = "btnResetOvers";
            this.btnResetOvers.Size = new System.Drawing.Size(152, 23);
            this.btnResetOvers.TabIndex = 37;
            this.btnResetOvers.Text = "+ Reset Overs";
            this.btnResetOvers.UseVisualStyleBackColor = true;
            this.btnResetOvers.Click += new System.EventHandler(this.btnResetOvers_Click);
            // 
            // btnDeleteSelectedOvers
            // 
            this.btnDeleteSelectedOvers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSelectedOvers.Location = new System.Drawing.Point(41, 482);
            this.btnDeleteSelectedOvers.Name = "btnDeleteSelectedOvers";
            this.btnDeleteSelectedOvers.Size = new System.Drawing.Size(152, 23);
            this.btnDeleteSelectedOvers.TabIndex = 38;
            this.btnDeleteSelectedOvers.Text = "- Delete Selected Overs";
            this.btnDeleteSelectedOvers.UseVisualStyleBackColor = true;
            this.btnDeleteSelectedOvers.Click += new System.EventHandler(this.btnDeleteSelectedOvers_Click);
            // 
            // btnDeleteSelectedUnders
            // 
            this.btnDeleteSelectedUnders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSelectedUnders.Location = new System.Drawing.Point(31, 482);
            this.btnDeleteSelectedUnders.Name = "btnDeleteSelectedUnders";
            this.btnDeleteSelectedUnders.Size = new System.Drawing.Size(152, 23);
            this.btnDeleteSelectedUnders.TabIndex = 39;
            this.btnDeleteSelectedUnders.Text = "- Delete Selected Unders";
            this.btnDeleteSelectedUnders.UseVisualStyleBackColor = true;
            this.btnDeleteSelectedUnders.Click += new System.EventHandler(this.btnDeleteSelectedUnders_Click);
            // 
            // grbUnders
            // 
            this.grbUnders.Controls.Add(this.lblGrossAreaShortened);
            this.grbUnders.Controls.Add(this.lblGrossAreaTextShortened);
            this.grbUnders.Controls.Add(this.btnResetUnders);
            this.grbUnders.Controls.Add(this.pnlUndrs);
            this.grbUnders.Controls.Add(this.btnDeleteSelectedUnders);
            this.grbUnders.Controls.Add(this.btnAddVirtualUndr);
            this.grbUnders.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbUnders.Location = new System.Drawing.Point(542, 13);
            this.grbUnders.Name = "grbUnders";
            this.grbUnders.Size = new System.Drawing.Size(229, 583);
            this.grbUnders.TabIndex = 40;
            this.grbUnders.TabStop = false;
            this.grbUnders.Text = "Unders";
            // 
            // lblGrossAreaShortened
            // 
            this.lblGrossAreaShortened.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGrossAreaShortened.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrossAreaShortened.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGrossAreaShortened.Location = new System.Drawing.Point(143, 551);
            this.lblGrossAreaShortened.Name = "lblGrossAreaShortened";
            this.lblGrossAreaShortened.Size = new System.Drawing.Size(66, 23);
            this.lblGrossAreaShortened.TabIndex = 41;
            this.lblGrossAreaShortened.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGrossAreaTextShortened
            // 
            this.lblGrossAreaTextShortened.AutoSize = true;
            this.lblGrossAreaTextShortened.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrossAreaTextShortened.Location = new System.Drawing.Point(31, 551);
            this.lblGrossAreaTextShortened.Name = "lblGrossAreaTextShortened";
            this.lblGrossAreaTextShortened.Size = new System.Drawing.Size(88, 18);
            this.lblGrossAreaTextShortened.TabIndex = 40;
            this.lblGrossAreaTextShortened.Text = "Gross Area:";
            // 
            // btnResetUnders
            // 
            this.btnResetUnders.Location = new System.Drawing.Point(32, 517);
            this.btnResetUnders.Name = "btnResetUnders";
            this.btnResetUnders.Size = new System.Drawing.Size(152, 23);
            this.btnResetUnders.TabIndex = 39;
            this.btnResetUnders.Text = "+ Reset Unders";
            this.btnResetUnders.UseVisualStyleBackColor = true;
            this.btnResetUnders.Click += new System.EventHandler(this.btnResetUnders_Click);
            // 
            // gbxOvers
            // 
            this.gbxOvers.Controls.Add(this.btnSwitchSize);
            this.gbxOvers.Controls.Add(this.pnlOvers);
            this.gbxOvers.Controls.Add(this.btnAddVirtualOver);
            this.gbxOvers.Controls.Add(this.btnDeleteSelectedOvers);
            this.gbxOvers.Controls.Add(this.btnResetOvers);
            this.gbxOvers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxOvers.Location = new System.Drawing.Point(270, 13);
            this.gbxOvers.Name = "gbxOvers";
            this.gbxOvers.Size = new System.Drawing.Size(229, 583);
            this.gbxOvers.TabIndex = 41;
            this.gbxOvers.TabStop = false;
            this.gbxOvers.Text = "Overs";
            // 
            // btnSwitchSize
            // 
            this.btnSwitchSize.Location = new System.Drawing.Point(134, 551);
            this.btnSwitchSize.Margin = new System.Windows.Forms.Padding(2);
            this.btnSwitchSize.Name = "btnSwitchSize";
            this.btnSwitchSize.Size = new System.Drawing.Size(59, 21);
            this.btnSwitchSize.TabIndex = 39;
            this.btnSwitchSize.Text = "O/U";
            this.toolTip1.SetToolTip(this.btnSwitchSize, "Show only Overs and Unders");
            this.btnSwitchSize.UseVisualStyleBackColor = true;
            this.btnSwitchSize.Click += new System.EventHandler(this.btnSwitchSize_Click);
            // 
            // grbCuts
            // 
            this.grbCuts.Controls.Add(this.lblLengthRepeat);
            this.grbCuts.Controls.Add(this.btnSwitchCutsSize);
            this.grbCuts.Controls.Add(this.pnlCutsTotals);
            this.grbCuts.Controls.Add(this.btnResetCuts);
            this.grbCuts.Controls.Add(this.btnDeleteSelectedCuts);
            this.grbCuts.Controls.Add(this.pnlCuts);
            this.grbCuts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbCuts.Location = new System.Drawing.Point(24, 13);
            this.grbCuts.Name = "grbCuts";
            this.grbCuts.Size = new System.Drawing.Size(229, 720);
            this.grbCuts.TabIndex = 42;
            this.grbCuts.TabStop = false;
            this.grbCuts.Text = "Cuts";
            // 
            // lblLengthRepeat
            // 
            this.lblLengthRepeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLengthRepeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLengthRepeat.Location = new System.Drawing.Point(10, 597);
            this.lblLengthRepeat.Name = "lblLengthRepeat";
            this.lblLengthRepeat.Size = new System.Drawing.Size(209, 23);
            this.lblLengthRepeat.TabIndex = 48;
            this.lblLengthRepeat.Text = "Length repeat:";
            this.lblLengthRepeat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSwitchCutsSize
            // 
            this.btnSwitchCutsSize.Location = new System.Drawing.Point(76, 688);
            this.btnSwitchCutsSize.Name = "btnSwitchCutsSize";
            this.btnSwitchCutsSize.Size = new System.Drawing.Size(70, 23);
            this.btnSwitchCutsSize.TabIndex = 47;
            this.btnSwitchCutsSize.Text = "Cuts";
            this.toolTip1.SetToolTip(this.btnSwitchCutsSize, "Show Cuts Only");
            this.btnSwitchCutsSize.UseVisualStyleBackColor = true;
            this.btnSwitchCutsSize.Click += new System.EventHandler(this.btnSwitchCutsSize_Click);
            // 
            // pnlCutsTotals
            // 
            this.pnlCutsTotals.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlCutsTotals.Location = new System.Drawing.Point(9, 440);
            this.pnlCutsTotals.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCutsTotals.Name = "pnlCutsTotals";
            this.pnlCutsTotals.Size = new System.Drawing.Size(210, 149);
            this.pnlCutsTotals.TabIndex = 46;
            // 
            // btnResetCuts
            // 
            this.btnResetCuts.Location = new System.Drawing.Point(42, 658);
            this.btnResetCuts.Name = "btnResetCuts";
            this.btnResetCuts.Size = new System.Drawing.Size(152, 23);
            this.btnResetCuts.TabIndex = 45;
            this.btnResetCuts.Text = "+ Reset Cuts";
            this.btnResetCuts.UseVisualStyleBackColor = true;
            this.btnResetCuts.Click += new System.EventHandler(this.btnResetCuts_Click);
            // 
            // btnDeleteSelectedCuts
            // 
            this.btnDeleteSelectedCuts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSelectedCuts.Location = new System.Drawing.Point(42, 627);
            this.btnDeleteSelectedCuts.Name = "btnDeleteSelectedCuts";
            this.btnDeleteSelectedCuts.Size = new System.Drawing.Size(152, 23);
            this.btnDeleteSelectedCuts.TabIndex = 39;
            this.btnDeleteSelectedCuts.Text = "- Delete Selected Cuts";
            this.btnDeleteSelectedCuts.UseVisualStyleBackColor = true;
            this.btnDeleteSelectedCuts.Click += new System.EventHandler(this.btnDeleteSelectedCuts_Click);
            // 
            // tbNotes
            // 
            this.tbNotes.Location = new System.Drawing.Point(270, 620);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(434, 104);
            this.tbNotes.TabIndex = 43;
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNotes.Location = new System.Drawing.Point(268, 599);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(52, 18);
            this.labelNotes.TabIndex = 44;
            this.labelNotes.Text = "Notes:";
            // 
            // btnExportReport
            // 
            this.btnExportReport.Location = new System.Drawing.Point(715, 625);
            this.btnExportReport.Name = "btnExportReport";
            this.btnExportReport.Size = new System.Drawing.Size(56, 38);
            this.btnExportReport.TabIndex = 45;
            this.btnExportReport.Text = "Export Report";
            this.btnExportReport.UseVisualStyleBackColor = true;
            this.btnExportReport.Click += new System.EventHandler(this.btnExportReport_Click);
            // 
            // btnNestUndrs
            // 
            this.btnNestUndrs.Location = new System.Drawing.Point(715, 686);
            this.btnNestUndrs.Name = "btnNestUndrs";
            this.btnNestUndrs.Size = new System.Drawing.Size(56, 38);
            this.btnNestUndrs.TabIndex = 46;
            this.btnNestUndrs.Text = "Nest\r\nUnders";
            this.btnNestUndrs.UseVisualStyleBackColor = true;
            this.btnNestUndrs.Click += new System.EventHandler(this.btnNestUndrs_Click);
            // 
            // OversUndersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 910);
            this.Controls.Add(this.btnNestUndrs);
            this.Controls.Add(this.btnExportReport);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.tbNotes);
            this.Controls.Add(this.grbCuts);
            this.Controls.Add(this.gbxOvers);
            this.Controls.Add(this.grbUnders);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "OversUndersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cuts, Unders and Overs";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.OversUndersForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.grbStandardFills.ResumeLayout(false);
            this.grbStandardFills.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grbUnders.ResumeLayout(false);
            this.grbUnders.PerformLayout();
            this.gbxOvers.ResumeLayout(false);
            this.grbCuts.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlCuts;
        private System.Windows.Forms.Panel pnlUndrs;
        private System.Windows.Forms.Panel pnlOvers;
        private System.Windows.Forms.Button btnDoFills;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalNetArea;
        private System.Windows.Forms.Label lblGrossArea;
        private System.Windows.Forms.Label lblTotalLength;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddVirtualOver;
        private System.Windows.Forms.Button btnAddVirtualUndr;
        private System.Windows.Forms.Button btnResetOvers;
        private System.Windows.Forms.Button btnDeleteSelectedOvers;
        private System.Windows.Forms.Button btnDeleteSelectedUnders;
        private System.Windows.Forms.GroupBox grbUnders;
        private System.Windows.Forms.GroupBox gbxOvers;
        private System.Windows.Forms.GroupBox grbCuts;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSmallFillFillPiece;
        private System.Windows.Forms.Label lblSmallFillNetArea;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grbStandardFills;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblLargeFillWastePercent;
        private System.Windows.Forms.Label lblLargeFillNetArea;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblLargeAreaFillPiece;
        private System.Windows.Forms.TextBox txbSmallFillsWastePct;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbnUpdateFillsManually;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalWastePct;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSmallFillGrossArea;
        private System.Windows.Forms.Button btnDeleteSelectedCuts;
        private System.Windows.Forms.Button btnResetCuts;
        private System.Windows.Forms.Button btnResetUnders;
        private System.Windows.Forms.Button btnSwitchSize;
        private System.Windows.Forms.Label lblGrossAreaShortened;
        private System.Windows.Forms.Label lblGrossAreaTextShortened;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnExportReport;
        private System.Windows.Forms.Panel pnlCutsTotals;
        private System.Windows.Forms.Button btnSwitchCutsSize;
        private System.Windows.Forms.Label lblLengthRepeat;
        public System.Windows.Forms.RadioButton RbnUpdateFillsAutomatically;
        private System.Windows.Forms.Button btnNestUndrs;
    }
}