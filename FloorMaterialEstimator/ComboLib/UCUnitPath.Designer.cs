namespace FloorMaterialEstimator
{
    partial class UCUnitPath
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
            this.lblPathLength = new System.Windows.Forms.Label();
            this.pnlPath = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblPathLength
            // 
            this.lblPathLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPathLength.Location = new System.Drawing.Point(1, 67);
            this.lblPathLength.Margin = new System.Windows.Forms.Padding(0);
            this.lblPathLength.Name = "lblPathLength";
            this.lblPathLength.Size = new System.Drawing.Size(142, 18);
            this.lblPathLength.TabIndex = 0;
            this.lblPathLength.Text = "Path Length";
            this.lblPathLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPath
            // 
            this.pnlPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPath.Location = new System.Drawing.Point(0, 0);
            this.pnlPath.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPath.Name = "pnlPath";
            this.pnlPath.Size = new System.Drawing.Size(147, 65);
            this.pnlPath.TabIndex = 1;
            // 
            // UCUnitPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPath);
            this.Controls.Add(this.lblPathLength);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCUnitPath";
            this.Size = new System.Drawing.Size(149, 88);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPathLength;
        private System.Windows.Forms.Panel pnlPath;
    }
}
