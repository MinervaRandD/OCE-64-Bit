#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: TextBoxEditForm.Designer.cs. Project: Graphics. Created: 6/10/2024         */
/*                                                                                                     */
/* Copyright (c) 2025, Minerva Research and Development, LLC. All rights reserved.                     */
/*                                                                                                     */
/* Not to be copied or distributed in any way without prior authorization. If provided with permission,*/
/* this software is provided without warranty of any kind, express or implied,                         */
/* including but not limited to the warranties of merchantability, fitness for a particular            */
/* purpose, and non-infringement. In no event shall the authors or copyright holders be liable         */
/* for any claim, damages, or other liability, whether in an action of contract, tort, or              */
/* otherwise, arising from, out of, or in connection with the software or the use or other             */
/* dealings in the software.                                                                           */
/*                                                                                                     */
/* Author: Marc Diamond, Minerva Research and Development, LLC                                         */
/*                                                                                                     */
/*******************************************************************************************************/
#endregion


namespace Graphics
{
    partial class TextBoxEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextBoxEditForm));
            this.txbTextBoxText = new System.Windows.Forms.TextBox();
            this.grbText = new System.Windows.Forms.GroupBox();
            this.btnSelectTextColor = new System.Windows.Forms.Button();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.lblTextBoxText = new System.Windows.Forms.Label();
            this.btnTextAlignLeft = new System.Windows.Forms.Button();
            this.btnTextAlignMiddle = new System.Windows.Forms.Button();
            this.btnTextAlignRight = new System.Windows.Forms.Button();
            this.btnTextAlignBottom = new System.Windows.Forms.Button();
            this.btnTextAlignCenter = new System.Windows.Forms.Button();
            this.btnTextAlignTop = new System.Windows.Forms.Button();
            this.btnUnderlineFont = new System.Windows.Forms.Button();
            this.btnItalicFont = new System.Windows.Forms.Button();
            this.btnBoldFont = new System.Windows.Forms.Button();
            this.grbText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // txbTextBoxText
            // 
            this.txbTextBoxText.Location = new System.Drawing.Point(76, 26);
            this.txbTextBoxText.Multiline = true;
            this.txbTextBoxText.Name = "txbTextBoxText";
            this.txbTextBoxText.Size = new System.Drawing.Size(254, 40);
            this.txbTextBoxText.TabIndex = 0;
            // 
            // grbText
            // 
            this.grbText.Controls.Add(this.btnUnderlineFont);
            this.grbText.Controls.Add(this.btnItalicFont);
            this.grbText.Controls.Add(this.btnBoldFont);
            this.grbText.Controls.Add(this.btnTextAlignBottom);
            this.grbText.Controls.Add(this.btnTextAlignCenter);
            this.grbText.Controls.Add(this.btnTextAlignTop);
            this.grbText.Controls.Add(this.btnTextAlignRight);
            this.grbText.Controls.Add(this.btnTextAlignMiddle);
            this.grbText.Controls.Add(this.btnTextAlignLeft);
            this.grbText.Controls.Add(this.btnSelectTextColor);
            this.grbText.Controls.Add(this.lblFontSize);
            this.grbText.Controls.Add(this.nudFontSize);
            this.grbText.Controls.Add(this.lblTextBoxText);
            this.grbText.Controls.Add(this.txbTextBoxText);
            this.grbText.Location = new System.Drawing.Point(43, 27);
            this.grbText.Name = "grbText";
            this.grbText.Size = new System.Drawing.Size(572, 178);
            this.grbText.TabIndex = 1;
            this.grbText.TabStop = false;
            this.grbText.Text = "Text";
            // 
            // btnSelectTextColor
            // 
            this.btnSelectTextColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectTextColor.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectTextColor.Image")));
            this.btnSelectTextColor.Location = new System.Drawing.Point(217, 90);
            this.btnSelectTextColor.Name = "btnSelectTextColor";
            this.btnSelectTextColor.Size = new System.Drawing.Size(47, 30);
            this.btnSelectTextColor.TabIndex = 6;
            this.btnSelectTextColor.Text = "A";
            this.btnSelectTextColor.UseVisualStyleBackColor = true;
            this.btnSelectTextColor.Click += new System.EventHandler(this.btnSelectTextColor_Click);
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(29, 101);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(77, 13);
            this.lblFontSize.TabIndex = 3;
            this.lblFontSize.Text = "Font Size (pts):";
            // 
            // nudFontSize
            // 
            this.nudFontSize.Location = new System.Drawing.Point(126, 94);
            this.nudFontSize.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.nudFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new System.Drawing.Size(59, 20);
            this.nudFontSize.TabIndex = 2;
            this.nudFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudFontSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFontSize.ValueChanged += new System.EventHandler(this.nudFontSize_ValueChanged);
            // 
            // lblTextBoxText
            // 
            this.lblTextBoxText.AutoSize = true;
            this.lblTextBoxText.Location = new System.Drawing.Point(29, 41);
            this.lblTextBoxText.Name = "lblTextBoxText";
            this.lblTextBoxText.Size = new System.Drawing.Size(31, 13);
            this.lblTextBoxText.TabIndex = 1;
            this.lblTextBoxText.Text = "Text:";
            // 
            // btnTextAlignLeft
            // 
            this.btnTextAlignLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnTextAlignLeft.Image")));
            this.btnTextAlignLeft.Location = new System.Drawing.Point(375, 60);
            this.btnTextAlignLeft.Name = "btnTextAlignLeft";
            this.btnTextAlignLeft.Size = new System.Drawing.Size(36, 36);
            this.btnTextAlignLeft.TabIndex = 7;
            this.btnTextAlignLeft.UseVisualStyleBackColor = true;
            this.btnTextAlignLeft.Click += new System.EventHandler(this.btnTextAlignLeft_Click);
            // 
            // btnTextAlignMiddle
            // 
            this.btnTextAlignMiddle.Image = ((System.Drawing.Image)(resources.GetObject("btnTextAlignMiddle.Image")));
            this.btnTextAlignMiddle.Location = new System.Drawing.Point(422, 60);
            this.btnTextAlignMiddle.Name = "btnTextAlignMiddle";
            this.btnTextAlignMiddle.Size = new System.Drawing.Size(36, 36);
            this.btnTextAlignMiddle.TabIndex = 8;
            this.btnTextAlignMiddle.UseVisualStyleBackColor = true;
            this.btnTextAlignMiddle.Click += new System.EventHandler(this.btnTextAlignMiddle_Click);
            // 
            // btnTextAlignRight
            // 
            this.btnTextAlignRight.Image = ((System.Drawing.Image)(resources.GetObject("btnTextAlignRight.Image")));
            this.btnTextAlignRight.Location = new System.Drawing.Point(469, 60);
            this.btnTextAlignRight.Name = "btnTextAlignRight";
            this.btnTextAlignRight.Size = new System.Drawing.Size(36, 36);
            this.btnTextAlignRight.TabIndex = 9;
            this.btnTextAlignRight.UseVisualStyleBackColor = true;
            this.btnTextAlignRight.Click += new System.EventHandler(this.btnTextAlignRight_Click);
            // 
            // btnTextAlignBottom
            // 
            this.btnTextAlignBottom.Image = ((System.Drawing.Image)(resources.GetObject("btnTextAlignBottom.Image")));
            this.btnTextAlignBottom.Location = new System.Drawing.Point(471, 13);
            this.btnTextAlignBottom.Name = "btnTextAlignBottom";
            this.btnTextAlignBottom.Size = new System.Drawing.Size(36, 36);
            this.btnTextAlignBottom.TabIndex = 12;
            this.btnTextAlignBottom.UseVisualStyleBackColor = true;
            this.btnTextAlignBottom.Click += new System.EventHandler(this.btnTextAlignBottom_Click);
            // 
            // btnTextAlignCenter
            // 
            this.btnTextAlignCenter.Image = ((System.Drawing.Image)(resources.GetObject("btnTextAlignCenter.Image")));
            this.btnTextAlignCenter.Location = new System.Drawing.Point(424, 13);
            this.btnTextAlignCenter.Name = "btnTextAlignCenter";
            this.btnTextAlignCenter.Size = new System.Drawing.Size(36, 36);
            this.btnTextAlignCenter.TabIndex = 11;
            this.btnTextAlignCenter.UseVisualStyleBackColor = true;
            this.btnTextAlignCenter.Click += new System.EventHandler(this.btnTextAlignCenter_Click);
            // 
            // btnTextAlignTop
            // 
            this.btnTextAlignTop.Image = ((System.Drawing.Image)(resources.GetObject("btnTextAlignTop.Image")));
            this.btnTextAlignTop.Location = new System.Drawing.Point(377, 13);
            this.btnTextAlignTop.Name = "btnTextAlignTop";
            this.btnTextAlignTop.Size = new System.Drawing.Size(36, 36);
            this.btnTextAlignTop.TabIndex = 10;
            this.btnTextAlignTop.UseVisualStyleBackColor = true;
            this.btnTextAlignTop.Click += new System.EventHandler(this.btnTextAlignTop_Click);
            // 
            // btnUnderlineFont
            // 
            this.btnUnderlineFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnderlineFont.Location = new System.Drawing.Point(466, 108);
            this.btnUnderlineFont.Name = "btnUnderlineFont";
            this.btnUnderlineFont.Size = new System.Drawing.Size(36, 36);
            this.btnUnderlineFont.TabIndex = 15;
            this.btnUnderlineFont.Text = "U";
            this.btnUnderlineFont.UseVisualStyleBackColor = true;
            this.btnUnderlineFont.Click += new System.EventHandler(this.btnUnderlineFont_Click);
            // 
            // btnItalicFont
            // 
            this.btnItalicFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItalicFont.Location = new System.Drawing.Point(419, 108);
            this.btnItalicFont.Name = "btnItalicFont";
            this.btnItalicFont.Size = new System.Drawing.Size(36, 36);
            this.btnItalicFont.TabIndex = 14;
            this.btnItalicFont.Text = "I";
            this.btnItalicFont.UseVisualStyleBackColor = true;
            this.btnItalicFont.Click += new System.EventHandler(this.btnItalicFont_Click);
            // 
            // btnBoldFont
            // 
            this.btnBoldFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBoldFont.Location = new System.Drawing.Point(372, 108);
            this.btnBoldFont.Name = "btnBoldFont";
            this.btnBoldFont.Size = new System.Drawing.Size(36, 36);
            this.btnBoldFont.TabIndex = 13;
            this.btnBoldFont.Text = "B";
            this.btnBoldFont.UseVisualStyleBackColor = true;
            this.btnBoldFont.Click += new System.EventHandler(this.btnBoldFont_Click);
            // 
            // TextBoxEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 450);
            this.Controls.Add(this.grbText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TextBoxEditForm";
            this.Text = "Text Box Edit Form";
            this.grbText.ResumeLayout(false);
            this.grbText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txbTextBoxText;
        private System.Windows.Forms.GroupBox grbText;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private System.Windows.Forms.Label lblTextBoxText;
        private System.Windows.Forms.Button btnSelectTextColor;
        private System.Windows.Forms.Button btnTextAlignRight;
        private System.Windows.Forms.Button btnTextAlignMiddle;
        private System.Windows.Forms.Button btnTextAlignLeft;
        private System.Windows.Forms.Button btnUnderlineFont;
        private System.Windows.Forms.Button btnItalicFont;
        private System.Windows.Forms.Button btnBoldFont;
        private System.Windows.Forms.Button btnTextAlignBottom;
        private System.Windows.Forms.Button btnTextAlignCenter;
        private System.Windows.Forms.Button btnTextAlignTop;
    }
}