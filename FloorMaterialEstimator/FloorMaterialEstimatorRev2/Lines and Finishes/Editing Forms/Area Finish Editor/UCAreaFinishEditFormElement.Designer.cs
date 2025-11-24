namespace FloorMaterialEstimator.Finish_Controls
{
    partial class UCAreaFinishEditFormElement
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
            this.txbFinishName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // pnlColor
            // 
            this.pnlColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColor.Location = new System.Drawing.Point(4, 3);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(40, 40);
            this.pnlColor.TabIndex = 0;
            // 
            // txbFinishName
            // 
            this.txbFinishName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbFinishName.Location = new System.Drawing.Point(50, 4);
            this.txbFinishName.Multiline = true;
            this.txbFinishName.Name = "txbFinishName";
            this.txbFinishName.Size = new System.Drawing.Size(195, 40);
            this.txbFinishName.TabIndex = 2;
            // 
            // UCAreaFinishEditFormElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txbFinishName);
            this.Controls.Add(this.pnlColor);
            this.Name = "UCAreaFinishEditFormElement";
            this.Size = new System.Drawing.Size(248, 50);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.TextBox txbFinishName;
    }
}
