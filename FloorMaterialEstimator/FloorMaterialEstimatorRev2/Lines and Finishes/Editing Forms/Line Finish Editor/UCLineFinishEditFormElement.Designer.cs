namespace FloorMaterialEstimator.Finish_Controls
{
    partial class UCLineFinishEditFormElement
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
            this.txbLineName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txbLineName
            // 
            this.txbLineName.Location = new System.Drawing.Point(-1, 1);
            this.txbLineName.Multiline = true;
            this.txbLineName.Name = "txbLineName";
            this.txbLineName.Size = new System.Drawing.Size(172, 20);
            this.txbLineName.TabIndex = 1;
            // 
            // UCLineFinishEditFormElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txbLineName);
            this.Name = "UCLineFinishEditFormElement";
            this.Size = new System.Drawing.Size(171, 37);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbLineName;
    }
}
