namespace FloorMaterialEstimator
{
    partial class UCUndrElement
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
            this.lblUndrNbr = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblUndrNbr
            // 
            this.lblUndrNbr.Location = new System.Drawing.Point(3, 4);
            this.lblUndrNbr.Name = "lblUndrNbr";
            this.lblUndrNbr.Size = new System.Drawing.Size(60, 22);
            this.lblUndrNbr.TabIndex = 0;
            this.lblUndrNbr.Text = "Undr Nbr";
            this.lblUndrNbr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UCUndrElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblUndrNbr);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCUndrElement";
            this.Size = new System.Drawing.Size(60, 26);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblUndrNbr;
    }
}
