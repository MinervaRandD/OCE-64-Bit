namespace FloorMaterialEstimator.Finish_Controls
{
    partial class UCSeamPalletElement
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
            this.pnlColor.Location = new System.Drawing.Point(2, 32);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(58, 36);
            this.pnlColor.TabIndex = 25;
            // 
            // lblSeamName
            // 
            this.lblSeamName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeamName.Location = new System.Drawing.Point(-5, 6);
            this.lblSeamName.Name = "lblSeamName";
            this.lblSeamName.Size = new System.Drawing.Size(63, 21);
            this.lblSeamName.TabIndex = 24;
            this.lblSeamName.Text = "Seam";
            this.lblSeamName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWidthTitle
            // 
            this.lblWidthTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidthTitle.Location = new System.Drawing.Point(64, 30);
            this.lblWidthTitle.Name = "lblWidthTitle";
            this.lblWidthTitle.Size = new System.Drawing.Size(66, 21);
            this.lblWidthTitle.TabIndex = 26;
            this.lblWidthTitle.Text = "Width:";
            this.lblWidthTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLengthTitle
            // 
            this.lblLengthTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLengthTitle.Location = new System.Drawing.Point(64, 51);
            this.lblLengthTitle.Name = "lblLengthTitle";
            this.lblLengthTitle.Size = new System.Drawing.Size(66, 21);
            this.lblLengthTitle.TabIndex = 27;
            this.lblLengthTitle.Text = "Length:";
            this.lblLengthTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLengthValue
            // 
            this.lblLengthValue.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLengthValue.Location = new System.Drawing.Point(122, 51);
            this.lblLengthValue.Name = "lblLengthValue";
            this.lblLengthValue.Size = new System.Drawing.Size(58, 21);
            this.lblLengthValue.TabIndex = 29;
            this.lblLengthValue.Text = "0\' - 0\"";
            this.lblLengthValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWidthValue
            // 
            this.lblWidthValue.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidthValue.Location = new System.Drawing.Point(122, 30);
            this.lblWidthValue.Name = "lblWidthValue";
            this.lblWidthValue.Size = new System.Drawing.Size(58, 21);
            this.lblWidthValue.TabIndex = 28;
            this.lblWidthValue.Text = "0\' - 0\"";
            this.lblWidthValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCSeam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.lblLengthValue);
            this.Controls.Add(this.lblWidthValue);
            this.Controls.Add(this.lblLengthTitle);
            this.Controls.Add(this.lblWidthTitle);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.lblSeamName);
            this.Name = "UCSeam";
            this.Size = new System.Drawing.Size(189, 74);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.Label lblSeamName;
        private System.Windows.Forms.Label lblWidthTitle;
        private System.Windows.Forms.Label lblLengthTitle;
        private System.Windows.Forms.Label lblLengthValue;
        private System.Windows.Forms.Label lblWidthValue;
    }
}
