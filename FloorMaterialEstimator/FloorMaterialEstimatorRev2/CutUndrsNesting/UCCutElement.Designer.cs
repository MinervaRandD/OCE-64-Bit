namespace FloorMaterialEstimator
{
    partial class UCCutElement
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
            this.lblCutNbr = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCutNbr
            // 
            this.lblCutNbr.Location = new System.Drawing.Point(3, 4);
            this.lblCutNbr.Name = "lblCutNbr";
            this.lblCutNbr.Size = new System.Drawing.Size(60, 22);
            this.lblCutNbr.TabIndex = 0;
            this.lblCutNbr.Text = "Cut Nbr";
            this.lblCutNbr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UCCutElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblCutNbr);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCCutElement";
            this.Size = new System.Drawing.Size(60, 26);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblCutNbr;
    }
}
