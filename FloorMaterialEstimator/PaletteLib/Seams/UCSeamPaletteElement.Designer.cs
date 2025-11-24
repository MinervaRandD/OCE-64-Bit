namespace PaletteLib
{
    partial class UCSeamPaletteElement
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
            this.pnlColor = new System.Windows.Forms.Panel();
            this.lblSeamName = new System.Windows.Forms.Label();
            this.lblWidthTitle = new System.Windows.Forms.Label();
            this.lblLengthTitle = new System.Windows.Forms.Label();
            this.lblLengthValue = new System.Windows.Forms.Label();
            this.lblWidthValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlColor
            // 
            this.pnlColor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColor.Location = new System.Drawing.Point(9, 32);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(58, 36);
            this.pnlColor.TabIndex = 25;
            // 
            // lblSeamName
            // 
            this.lblSeamName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeamName.Location = new System.Drawing.Point(9, 3);
            this.lblSeamName.Name = "lblSeamName";
            this.lblSeamName.Size = new System.Drawing.Size(221, 21);
            this.lblSeamName.TabIndex = 24;
            this.lblSeamName.Text = "Seam";
            // 
            // lblWidthTitle
            // 
            this.lblWidthTitle.AutoSize = true;
            this.lblWidthTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidthTitle.Location = new System.Drawing.Point(86, 30);
            this.lblWidthTitle.Name = "lblWidthTitle";
            this.lblWidthTitle.Size = new System.Drawing.Size(42, 15);
            this.lblWidthTitle.TabIndex = 26;
            this.lblWidthTitle.Text = "Width:";
            this.lblWidthTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLengthTitle
            // 
            this.lblLengthTitle.AutoSize = true;
            this.lblLengthTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLengthTitle.Location = new System.Drawing.Point(86, 51);
            this.lblLengthTitle.Name = "lblLengthTitle";
            this.lblLengthTitle.Size = new System.Drawing.Size(47, 15);
            this.lblLengthTitle.TabIndex = 27;
            this.lblLengthTitle.Text = "Length:";
            this.lblLengthTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLengthValue
            // 
            this.lblLengthValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLengthValue.Location = new System.Drawing.Point(144, 51);
            this.lblLengthValue.Name = "lblLengthValue";
            this.lblLengthValue.Size = new System.Drawing.Size(58, 15);
            this.lblLengthValue.TabIndex = 29;
            this.lblLengthValue.Text = "0\' - 0\"";
            this.lblLengthValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWidthValue
            // 
            this.lblWidthValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidthValue.Location = new System.Drawing.Point(144, 30);
            this.lblWidthValue.Name = "lblWidthValue";
            this.lblWidthValue.Size = new System.Drawing.Size(58, 15);
            this.lblWidthValue.TabIndex = 28;
            this.lblWidthValue.Text = "0\' - 0\"";
            this.lblWidthValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCSeamPaletteElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblLengthValue);
            this.Controls.Add(this.lblWidthValue);
            this.Controls.Add(this.lblLengthTitle);
            this.Controls.Add(this.lblWidthTitle);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.lblSeamName);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.Name = "UCSeamPaletteElement";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Size = new System.Drawing.Size(240, 78);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.Label lblWidthTitle;
        private System.Windows.Forms.Label lblLengthTitle;
        private System.Windows.Forms.Label lblLengthValue;
        private System.Windows.Forms.Label lblWidthValue;
        public System.Windows.Forms.Label lblSeamName;
    }
}
